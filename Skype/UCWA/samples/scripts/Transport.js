/* Copyright (C) Microsoft 2014. All rights reserved. */
(function() {
    "use strict";

    var _generalHelper = new GeneralHelper();

    _generalHelper.namespace("microsoft.rtc.ucwa.samples");

    /// <summary>
    /// Transport is responsible for handling HTTP traffic between an application and UCWA. 
    /// </summary>
    /// <param name="targetOrigin">Origin to be used for postMessage.</param>
    /// <remarks>
    /// Transport leverages iframes and HTML5's postMessage for cross-domain 
    /// communication. If necessary, it will also handle cases where the domain changes by 
    /// injecting a new iframe into a container element.
    /// For more information about postMessage, see:
    ///     http://ucwa.lync.com/documentation/GettingStarted-CrossDomain
    ///     http://www.whatwg.org/specs/web-apps/current-work/multipage/web-messaging.html#web-messaging
    ///     https://msdn.microsoft.com/en-us/library/ie/cc197015(v=vs.85).aspx
    /// </remarks>
    microsoft.rtc.ucwa.samples.Transport = (function($) {
        var obj = function Transport(targetOrigin) {
            if (!this instanceof Transport) {
                return new Transport();
            }

            // The domain of the UCWA server
            var _domain = null,
            // The last xframe link provided by the server
            _xframe = null,
            // The default accept and content type to use for requests
            _defaultType = "application/json",
            // DOM element where the postMessage iframe resides.
            _container = null,
            // The iframe used for postMessage
            _element = null,
            // OAuth token attached to requests
            _accessToken = null,
            // The OAuth token type (also attached to requests)
            _tokenType = null,
            // Callback to execute when a request has started
            _requestStart = null,
            // Callback to execute when a request has stopped
            _requestStop = null,
            // A count of the number of outstanding requests
            _requestCounter = 0,
            // Array out outstanding handlers attached to window's message/onmessage
            _methods = [],
            // Boolean to indicate transport has failed the allowed list previously
            _failedAllowedList = false,
            // An internal reference to the current object. Used in case of changing scope.
            _scope = this;

            /// <summary>
            /// Prepares the supplied request object for transport via postMessage.
            /// </summary>
            /// <param name="request">Request object to process.</param>
            /// <remarks>
            /// Begin building the results object by setting basic data that is 
            /// uniform across HTTP Requests (type, url, basic headers). Check 
            /// to see if messageId is supplied and attach. Check to see if 
            /// accessToken exists otherwise delete the header as it will cause 
            /// conflicts if incorrectly used. Now begin handling the various type 
            /// states (get, post/put, delete). Ensure that JSON data is stringified 
            /// and the correct content length is used. Ensure that delete requests 
            /// only contain a header with authorization credentials. Stringify the 
            /// results so they can be passed to postMessage.
            /// </remarks>
            /// <returns>String representing the Request object.</returns>
            function createClientTransportData(request) {
                var accessToken = request.accessToken || _accessToken,
                result = {
                    type: request.type,
                    url: request.url,
                    headers: {
                        Accept: request.acceptType,
                        Authorization: _tokenType + " " + accessToken
                    }
                };

                if (request.messageId) {
                    result.messageId = request.messageId;
                }

                if (!accessToken) {
                    delete result.headers.Authorization;
                }

                switch (request.type) {
                    case "get":
                        // get is handled by the above code...
                        break;
                    case "post":
                        // intentional fall-through to "put"
                    case "put":
                        if (request.data && request.data.etag) {
                            result.headers["If-Match"] = '"' + request.data.etag + '"';
                        }

                        if (request.data) {
                            if (request.contentType.indexOf("json") !== -1) {
                                request.data = JSON.stringify(request.data);
                            }

                            result.headers["Content-Type"] = request.contentType;
                        } else {
                            request.data = "";
                            result.headers["Content-Type"] = "";
                        }

                        result.data = request.data;
                        break;
                    case "delete":
                        result.headers = {
                            Authorization: _tokenType + " " + accessToken
                        };
                        break;
                }

                return JSON.stringify(result);
            }

            /// <summary>
            /// Checks a URL to see if it contains enough information to be used as 
            /// the HTTP request URL.
            /// </summary>
            /// <param name="url">URL to check.</param>
            /// </param>
            /// <remarks>
            /// Check if supplied URL is an absolute path; if true the URL supplied is the 
            /// final URL. If not check if the URL is defined and prepend the internal 
            /// domain. If it is not defined use the internal domain.
            /// </remarks>
            /// <returns>String representing the HTTP request url.</returns>
            function handleUrl(url) {
                var targetUrl,
                index = url ? url.indexOf("://") : "",
                isAbsolutePath = index !== -1;

                if (isAbsolutePath) {
                    targetUrl = url;
                } else {
                    if (url) {
                        targetUrl = _domain + url;
                    } else {
                        targetUrl = _domain;
                    }
                }

                return targetUrl;
            }

            /// <summary>
            /// Checks for a reason string in the headers of a response.
            /// </summary>
            /// <param name="headerString">The headers as a single string.</param>
            /// <remarks>
            /// The reason string is used to convey important information, such as 
            /// the absence of the request's origin from the allowed list.
            /// </remarks>
            /// <returns>The reason string or null.</returns>
            function findReasonString(headerString) {
                var obj = _generalHelper.parseHeaders(headerString);

                for (var item in obj["X-Ms-diagnostics"]) {
                    return obj["X-Ms-diagnostics"][item].reason;
                }

                return null;
            }

            /// <summary>
            /// Checks to see if the request's origin domain is on the allowed list.
            /// </summary>
            /// <param name="result">A Transport result object.</param>
            /// <remarks>
            /// If the request's origin domain is not on the allowed list, the 
            /// application will be unable to proceed.
            /// </remarks>
            function handleAllowedList(result) {
                if (result.status === 403 && !_failedAllowedList) {
                    var reasonString = findReasonString(result.headers);

                    if (reasonString !== null) {
                        window.console.log(reasonString);
                        alert(reasonString);
                        _failedAllowedList = true;
                    }
                }
            }

            /// <summary>
            /// Handles callback logic for a specific message ID.
            /// </summary>
            /// <param name="callback">Callback to execute.</param>
            /// <param name="messageId">Unique identifier for this callback.</param>
            /// <remarks>
            /// Create a temporary method which has access to messageId to check the 
            /// incoming response data to see if messageIds match. This temporary 
            /// method will be added as a handler for window.onmessage. Upon 
            /// completion of execution of the temporary method it will remove itself 
            /// as a handler. Handle stopping the request display if no requests are 
            /// outstanding.
            /// </remarks>
            /// <returns>Method to be executed upon error/completion.</returns>
            function handleCallback(callback, messageId, requestUrl) {
                var method = function(data) {
                    var result = JSON.parse(data.data);
                    handleAllowedList(result);
                    if (result.messageId === messageId) {
                        if (_requestStop) {
                            _requestCounter--;

                            if (_requestCounter <= 0) {
                                _requestStop();
                            }
                        }

                        try {
                            var headers = _generalHelper.parseHeaders(result.headers);

                            if (headers["Content-Type"] && headers["Content-Type"].length && headers["Content-Type"].indexOf(_defaultType) !== -1) {
                                try {
                                    result.results = JSON.parse(result.responseText);

                                    var changed = testForDomainChanges(result.results, function() {
                                        _generalHelper.safeCallbackExec({
                                            callback: callback,
                                            params: [
                                                result
                                            ],
                                            error: "Encountered error executing transport callback"
                                        });

                                        removeHandler(method);
                                    });

                                    if (changed) {
                                        return;
                                    }
                                } catch(e) {
                                    window.console.error("Encountered error parsing response: " + e.message);
                                }
                            }
                        } catch(e) {
                            window.console.error("Encountered error handling response: " + e.message);
                        }

                        _generalHelper.safeCallbackExec({
                            callback: callback,
                            params: [
                                result
                            ],
                            error: "Encountered error executing transport callback"
                        });

                        removeHandler(method);
                    }
                }

                addHandler(method);

                return method;
            }

            /// <summary>
            /// Adds the supplied method as a handler for onmessage (message).
            /// </summary>
            /// <param name="method">Method to add as a handler.</param>
            /// <remarks>
            /// Depending on the browser, it will add a handler for onmessage 
            /// or message. When the cross-domain frame sends the response 
            /// back to the client via postMessage, method will be executed.
            /// </remarks>
            function addHandler(method) {
                if (window.attachEvent) {
                    window.attachEvent("onmessage", method)
                } else {
                    window.addEventListener("message", method);
                }

                _methods.push(method);
            }

            /// <summary>
            /// Removes the supplied method as a handler for onmessage (message).
            /// </summary>
            /// <param name="method">Method to remove as a handler.</param>
            /// <remarks>
            /// Depending on the browser it will remove a handler for onmessage 
            /// or message.
            /// </remarks>
            function removeHandler(method) {
                if (window.detachEvent) {
                    window.detachEvent("onmessage", method);
                } else {
                    window.removeEventListener("message", method);
                }

                var index = _methods.indexOf(method);

                if (index !== -1) {
                    _methods.splice(index, 1);
                }
            }

            /// <summary>
            /// Checks for frame changes and injects a new frame, if necessary.
            /// </summary>
            /// <param name="request">Data object to check.</param>
            /// <remarks>
            /// Check for the presence of an xframe link and compare it to the last seen 
            /// xframe. If the new xframe differs from the stored xframe, inject a new 
            /// frame using the new href.
            /// </remarks>
            /// <returns>Boolean indicating whether or not there was a frame change.</returns>
            function testForDomainChanges(request, callback) {
                if (request._links && request._links.xframe) {
                    if (_xframe !== request._links.xframe.href) {
                        _xframe = request._links.xframe.href;
                        _scope.injectFrame(_xframe, _container, callback);

                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Gets the domain that Transport is currently sending requests to.
            /// </summary>
            /// <returns>String representing the domain.</returns>
            obj.prototype.getDomain = function() {
                return _domain;
            }

            /// <summary>
            /// Sets the element and domain to be used for requests.
            /// </summary>
            /// <param name="element">Element that will receive requests.</param>
            /// <param name="xframe">Absolute URL of the iframe's target.</param>
            /// <remarks>
            /// With these two elements set, the Transport library can attempt 
            /// to make HTTP requests using postMessage on the element.
            /// </remarks>
            obj.prototype.setElement = function(element, xframe) {
                if (element) {
                    _element = element;
                    _domain = _generalHelper.extractOriginFromAbsoluteUrl(xframe);
                }
            }

            /// <summary>
            /// Sets authorization credentials to be used in requests.
            /// </summary>
            /// <param name="accessToken">Unique identifier.</param>
            /// <param name="tokenType">Type of access token.</param>
            obj.prototype.setAuthorization = function(accessToken, tokenType) {
                _accessToken = accessToken;
                _tokenType = tokenType;
            }

            /// <summary>
            /// Gets currently stored authorization credentials.
            /// </summary>
            /// <returns>Object containing accessToken and tokenType.</returns>
            obj.prototype.getAuthorization = function() {
                return {
                    accessToken: _accessToken,
                    tokenType: _tokenType
                };
            }

            /// <summary>
            /// Sets request callbacks to be executed when requests are started 
            /// and stopped.
            /// </summary>
            /// <param name="callbacks">Object containing callbacks.</param>
            /// <remarks>
            /// callbacks should an object in the form of:
            /// {
            ///     start: (may be omitted),
            ///     stop: (may be omitted)
            /// }
            /// </remarks>
            obj.prototype.setRequestCallbacks = function(callbacks) {
                if (callbacks) {
                    if (callbacks.start) {
                        _requestStart = callbacks.start;
                    }

                    if (callbacks.stop) {
                        _requestStop = callbacks.stop;
                    }
                }
            }

            /// <summary>
            /// Uses HTML5's postMessage to send a Request object to a remote location.
            /// </summary>
            /// <param name="request">Object containing request data.</param>
            /// <remarks>
            /// request should an object in the form of:
            /// {
            ///     url: "myLink" (Http request url),
            ///     type: "get" (get, post, put, delete),
            ///     acceptType: "application/json" (default, can be omitted),
            ///     contentType: "application/json" (default, can be omitted),
            ///     data: "hello world" (any kind of JSON data),
            ///     accessToken: override Transport's current access token for this request
            ///     callback: (may be omitted),
            ///     notifyAction: true/false (may be omitted)
            /// }
            /// Check if an internal element and domain have been set. Otherwise, no 
            /// remote communications will be possible. Next check to see if domain 
            /// changes have occurred, which might require a new iframe. Generate a UUID 
            /// and attach it to the Request object as it will be used to link 
            /// the response data to the correct callback. If a requestStart callback 
            /// has been set and notifyAction is not false the requestStart callback 
            /// will be executed. Finally use the internal element to post a message 
            /// by transforming the Request object into a request data.
            /// </remarks>
            obj.prototype.clientRequest = function(request) {
                if (_element && _domain) {
                    var messageId = _generalHelper.generateUUID(),
                    handler = handleCallback(request.callback, messageId, request.url);

                    if (_requestStart && request.notifyAction !== false) {
                        _requestStart();
                        _requestCounter++;
                    }

                    try {
                        _element.postMessage(createClientTransportData({
                            url: handleUrl(request.url),
                            type: request.type.toLocaleLowerCase(),
                            data: request.data,
                            acceptType: _generalHelper.getValue(_defaultType, request.acceptType),
                            contentType: _generalHelper.getValue(_defaultType, request.contentType),
                            messageId: messageId,
                            accessToken: request.accessToken
                        }), _generalHelper.getValue("*", targetOrigin));
                    } catch(e) {
                        window.console.log("Encountered error with clientRequest: " + e.message);

                        var response = {
                            messageId : messageId,
                            headers: "",
                            status: 400
                        };

                        handler({
                            data: JSON.stringify(response)
                        });
                    }
                }
            }

            /// <summary>
            /// Injects an iframe located on the domain into the supplied container.
            /// </summary>
            /// <param name="xframe">Absolute URL to the iframe's target.</param>
            /// <param name="container">DOM element that will contain the to-be-injected iframe.</param>
            /// <param name="callback">Callback to execute when the frame is loaded into the DOM.</param>
            /// <remarks>
            /// After the iframe is created, an event handler will be set up to run after the iframe is loaded. 
            /// This event handler sets the element and domain, and executes the supplied callback, 
            /// if defined.
            /// </remarks>
            obj.prototype.injectFrame = function(xframe, container, callback) {
                _container = container;

                var method = function() {
                    window.clearTimeout(loadId);

                    var id = window.setTimeout(function() {
                        window.console.log("Frame location not found within timeout (10000): " + xframe);

                        _generalHelper.safeCallbackExec({
                            callback: callback,
                            params: [
                                {
                                    status: 408,
                                    link: xframe
                                }
                            ],
                            error: "Encountered error executing frame injection callback"
                        });
                    }, 10000);

                    _scope.setElement(frame[0].contentWindow, xframe);
                    _scope.clientRequest({
                        url: xframe,
                        type: "get",
                        acceptType: "text/html",
                        notifyAction: false,
                        callback: function(data) {
                            window.clearTimeout(id);

                            _generalHelper.safeCallbackExec({
                                callback: callback,
                                params: [
                                    data
                                ],
                                error: "Encountered error executing frame injection callback"
                            });
                        }
                    });
                };

                var frame = $("<iframe></iframe>").load(method).attr("src", xframe),
                loadId = window.setTimeout(method, 10000);

                _container.html(frame);
            }

            /// <summary>
            /// Removes outstanding handlers still attached to window's (on)message event.
            /// </summary>
            obj.prototype.cleanup = function() {
                for (var i = 0; i < _methods.length; i++) {
                    removeHandler(_methods[i]);
                }
            }
        };

        return obj;
    }(jQuery));
}());