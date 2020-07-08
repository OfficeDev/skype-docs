/* Copyright (C) Microsoft 2014. All rights reserved. */
(function() {
    "use strict";

    var _generalHelper = new GeneralHelper();

    _generalHelper.namespace("microsoft.rtc.ucwa.samples");

    /// <summary>
    /// The functions in Mime.js can be used for processing multipart messages, as used by Batch,
    /// and translating them into Message objects, like those in Transport or JavaScript's XHR.
    /// </summary>
    /// <param name="defaultBoundary">Default value to use as a boundary.</param>
    /// <remarks>
    /// For more information about multipart, see the RFC.
    ///     https://www.ietf.org/rfc/rfc2046
    /// For more information about Message objects, see
    ///     https://msdn.microsoft.com/en-us/library/ie/ms535874(v=vs.85).aspx
    /// </remarks>
    microsoft.rtc.ucwa.samples.Mime = (function($) {
        var obj = function Mime(defaultBoundary) {
            if (!this instanceof Mime) {
                return new Mime();
            }

            /// <summary>
            /// Processes the header, looking for the boundary.
            /// </summary>
            /// <param name="header">Header data to process.</param>
            /// <remarks>
            /// Split the header on '\n' and begin searching for "boundary"
            /// After this is found, take the result. If it was not found, 
            /// return the default boundary.
            /// </remarks>
            /// <returns>String representation of the boundary.</returns>
            function processHeader(header) {
                var data = $.trim(header).split("\n");

                for (var line in data) {
                    if (data[line].indexOf("boundary") !== -1) {
                        var temp = data[line].split(";");

                        for (var item in temp) {
                            if (temp[item].indexOf("boundary") !== -1) {
                                var result = $.trim(temp[item].split("=")[1]);

                                if (result[0] === '"' && result[result.length - 1] === '"') {
                                    return result.slice(1, -1);
                                }

                                return result;
                            }
                        }
                    }
                }

                // No boundary was found in the results...
                return defaultBoundary;
            }

            /// <summary>
            /// Processes the body using the boundary.
            /// </summary>
            /// <param name="boundary">Boundary to use in processing.</param>
            /// <param name="body">Body data to process.</param>
            /// <remarks>
            /// Split the body on the boundary + "--" and for each part found, 
            /// begin building Message objects to hold the data. Add these objects 
            /// to an array.
            /// </remarks>
            /// <returns>An array of Message objects.</returns>
            function processBody(boundary, body) {
                var data = body.split("--" + boundary),
                parsed = [],
                messages = [];

                for (var part in data) {
                    if ($.trim(data[part]) !== "" && $.trim(data[part]) !== "--") {
                        var partData = $.trim(data[part]).split("\r\n"),
                        message = {
                            status: null,
                            statusText: null,
                            responseText: "",
                            header: "",
                            messageId: null
                        },
                        contentType = null;

                        for (var item in partData) {
                            if (contentType === null && partData[item].indexOf("Content-Type") !== -1) {
                                contentType = determineContentType(partData[item]);

                                if (contentType !== null && message.header === "") {
                                    message.header = readHeader(partData, item);
                                }
                            } else if (partData[item].indexOf("HTTP/1.1") !== -1) {
                                message.status = partData[item].split(" ")[1];
                                message.header = readHeader(partData, ++item);
                            } else if (contentType !== null && $.trim(partData[item]) !== "") {
                                message.responseText = partData[item];
                                if (contentType === "json") {
                                    message.results = JSON.parse($.trim(partData[item]));
                                }
                            }
                        }

                        messages.push(message);
                    }
                }

                return messages;
            }

            /// <summary>
            /// Determines the content type of the supplied data.
            /// </summary>
            /// <param name="data">Data to check.</param>
            /// <remarks>
            /// Search for type (application/json) 
            /// and if found, return the type. If not found, attempt to split the result 
            /// to get type.
            /// </remarks>
            /// <returns>String representation of the content type.</returns>
            function determineContentType(data) {
                var contentType = null,
                expectedType = "json";

                if (data.indexOf("http; msgtype=response") === -1) {
                    var index = data.indexOf(expectedType);

                    if (index !== -1) {
                        contentType = expectedType;
                    } else {
                        contentType = $.trim(data).split('/')[1];
                    }
                }

                return contentType;
            }

            /// <summary>
            /// Reads header data from the parts object based on index.
            /// </summary>
            /// <param name="parts">Array of strings to process.</param>
            /// <param name="index">Index into the parts array.</param>
            /// <remarks>
            /// While not at the end of the parts array, concatenate the 
            /// parts into a multi-line header.
            /// </remarks>
            /// <returns>String representation of the complete header.</returns>
            function readHeader(parts, index) {
                var header = "";
                while ($.trim(parts[index]) !== "" && index < parts.length) {
                    header = header.concat(parts[index], "\n");
                    index++;
                }

                return header;
            }

            /// <summary>
            /// Begins processing of data into an array of Message objects.
            /// </summary>
            /// <param name="data">Data containing MIME messages.</param>
            /// <returns>Returns an array of Message objects in the form of:
            /// {
            ///     status: 200,
            ///     statusText: "success",
            ///     responseText: "{data: "hello world"}",
            ///     header: "Cache-Control: no-store",
            ///     messageId: null
            /// }
            /// </returns>
            obj.prototype.processMessage = function(data) {
                var boundary = processHeader(data.headers),
                result = processBody(boundary, data.responseText);

                return result;
            }
        };

        return obj;
    }(jQuery));
}());