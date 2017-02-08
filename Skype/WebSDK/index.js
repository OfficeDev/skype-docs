(function () {
    'use strict';

    var client_id, prevSelectedItem;

    if (window.sessionStorage) {
        client_id = window.sessionStorage.getItem('client_id');
    } else {
        window.console.log('Your browser does not support sessionStorage and will not work with OAuth login');
    }

    function createXHR() {
        if (window.XMLHttpRequest) {
            return new XMLHttpRequest();
        } else {
            return new ActiveXObject("MSXML2.XMLHTTP.3.0");
        }
    }

    function createFramework() {
        var statusQueue = [];
        function processStatus() {
            var item = statusQueue[0];

            window.setTimeout(function (item) {
                var status = item.status;
                var type = item.type;
                var callback = item.callback;

                if (type !== window.framework.status.info) {
                    var content = window.framework.findContentDiv();
                    if (content) {
                        var activeStep = content.querySelector('.step.activeStep');
                        var firstStep = content.querySelector('.step');
                        var nextStep = content.querySelector('.step.activeStep + .step.hidden');

                        if (type === window.framework.status.success) {
                            if (nextStep !== null) {
                                activeStep.className = activeStep.className.replace('activeStep', 'hidden');
                                nextStep.className = nextStep.className.replace('hidden', 'activeStep');
                            }
                        } else if (type === window.framework.status.error || type === window.framework.status.reset) {
                            if (activeStep !== firstStep) {
                                activeStep.className = activeStep.className.replace('activeStep', 'hidden');
                                firstStep.className = firstStep.className.replace('hidden', 'activeStep');
                            }
                        }
                    }
                }

                if (callback) {
                    callback(true);
                }

                statusQueue.shift();
                if (statusQueue.length > 0) {
                    processStatus();
                }
            }, 2000, item);
        }

        function pushStatus(status, type, callback) {
            statusQueue.push({
                status: status,
                type: type,
                callback: callback
            });

            if (statusQueue.length === 1) {
                processStatus();
            }
        }

        // window.onerror = function (msg, url, line, col, error) {
        //     console.log('error');
        // };

        window.framework = {
            popupResponse: 'undefined',
            processingStatus: 'undefined',
            convs: {},
            showModal: function (modalText) {
                var modalEl = document.createElement('div');
                modalEl.style.width = '25em';
                modalEl.style.height = '20em';
                modalEl.style.margin = '10em auto';
                modalEl.style.backgroundColor = '#fff';
                modalEl.style.textAlign = 'center';
                var div = document.createElement('div');
                div.style.display = 'inline-block';
                var yesBtn = document.createElement('span');
                yesBtn.innerHTML = '<button class="mui-btn mui-btn--raised mui-btn--primary" onclick="window.framework.acceptIncomingChat()">YES</button>';
                var noBtn = document.createElement('span');
                noBtn.innerHTML = '<button class="mui-btn mui-btn--raised mui-btn--danger" onclick="window.framework.rejectIncomingChat()">NO</button>';
                var text = document.createElement('p');
                text.innerHTML = modalText;
                text.className = 'mui--text-title';
                div.appendChild(text); div.appendChild(yesBtn); div.appendChild(noBtn);
                div.innerHTML = '<br/><br/><br/>' + div.innerHTML;
                modalEl.appendChild(div);
                mui.overlay('on', modalEl);
            },
            updateUserIdInput: function (userId) {
                if (!userId) return '';
                return userId.indexOf('@') > 0
                    ? userId.indexOf('sip:') === 0 ? userId : 'sip:' + userId
                    : userId.indexOf('tel:') === 0 ? userId : 'tel:' + userId;
            },
            updateUserIdOutput: function (userId) {
                if (!userId) return '';
                return userId.indexOf('sip:') === 0 ? userId.substring(4) : userId;
            },
            invokeHistory: function (convId) {
                window.framework.addNotification('info', 'Trying to fetch conversation logs...');
                window.framework.convs[convId] && window.framework.convs[convId].conv.historyService.getMoreActivityItems();
            },
            addEventListener: function (element, event, callback) {
                if (element.addEventListener) {
                    element.addEventListener(event, callback);
                } else if (element.attachEvent) {
                    element.attachEvent('on' + event, callback);
                }
            },
            removeEventListener: function (element, event, callback) {
                if (element.removeEventListener) {
                    element.removeEventListener(event, callback);
                } else if (element.detachEvent) {
                    element.detachEvent('on' + event, callback);
                }
            },
            bindInputToEnter: function (element) {
                window.framework.addEventListener(element, 'keyup', function (e) {
                    if (e.keyCode === 13) {
                        var id = e.target.getAttribute('data-enter-bind');
                        var button = window.framework.findContentDiv().querySelector('.' + id);

                        button.click();
                    }
                });
            },
            findContentDiv: function () {
                return document.querySelector('.content > div:not([style*="display: none"])');
            },
            getContentLocation: function () {
                return location_config.content;
            },
            status: {
                success: 'success',
                error: 'error',
                info: 'info',
                reset: 'reset'
            },
            reportStatus: function (status, type, callback) {
                pushStatus(status, type, callback);
            },
            acceptIncomingChat: function () {
                window.framework.popupResponse = 'yes';
                mui.overlay('off');
            },
            rejectIncomingChat: function () {
                window.framework.popupResponse = 'no';
                mui.overlay('off');
            },
            addNotification: function (iconType, text) {
                var content = window.framework.findContentDiv(),
                    notificationElement = document.createElement('p'),
                    iconClass = '';

                switch (iconType) {
                    case 'info':
                        iconClass = 'fa fa-info-circle info-notification';
                        break;
                    case 'error':
                        iconClass = 'fa fa-thumbs-down error-notification';
                        break;
                    case 'success':
                        iconClass = 'fa fa-thumbs-up success-notification';
                        break;
                    case 'alert':
                        iconClass = 'fa fa-exclamation-circle error-notification';
                        break;
                }

                notificationElement.innerHTML = '<span class="' + iconClass + '"></span> <text> ' + text + ' </text>';
                var firstChild = content.querySelector('.notification-bar p');
                var notificationBar = content.querySelector('.notification-bar');

                if (firstChild) {
                    firstChild.parentNode.insertBefore(notificationElement, firstChild);
                } else {
                    notificationBar.appendChild(notificationElement);
                }
            },
            updateNotification: function (iconType, text) {
                var content = window.framework.findContentDiv(),
                    notificationBar = content.querySelector('.notification-bar'),
                    notificationBarClass = 'notification-bar',
                    iconClass = '',
                    notificationStatus = '';

                switch (iconType) {
                    case 'info':
                        iconClass = 'fa fa-info-circle';
                        notificationStatus = ' info';
                        break;
                    case 'error':
                        iconClass = 'fa fa-thumbs-down';
                        notificationStatus = ' error';
                        break;
                    case 'success':
                        iconClass = 'fa fa-thumbs-up';
                        notificationStatus = ' success';
                        break;
                }

                notificationBar.getElementsByTagName('span')[0].className = iconClass;
                notificationBar.className = notificationBarClass + notificationStatus;
                notificationBar.querySelector('.message').innerHTML = text;
            },
            showNotificationBar: function () {
                var content = window.framework.findContentDiv();
                content.querySelector('.notification-bar').style.display = 'block';
            },
            hideNotificationBar: function () {
                var content = window.framework.findContentDiv();
                content.querySelector('.notification-bar').style.display = 'none';
            },
            reportError: function (error, callback) {
                var message = error;
                if (error.message) {
                    message = error.message;
                }

                window.framework.reportStatus('Failure: ' + message, window.framework.status.error, callback);
                window.console.log(error);
            },
            registerNavigation: function (callback) {
                var content = window.framework.findContentDiv();
                window.framework.navigationCallbacks[content.getAttribute('data-location')] = callback;
            },
            navigationCallbacks: {},
            populateContacts: function (contacts, container) {
                var content = window.framework.findContentDiv();

                function populateSingleContact(contact) {
                    var contactDiv = document.createElement('div');
                    contactDiv.className = 'contact';
                    container.appendChild(contactDiv);
                    var tableDiv = document.createElement('div');
                    tableDiv.className = 'table';
                    contactDiv.appendChild(tableDiv);
                    var rowDiv = document.createElement('div');
                    rowDiv.className = 'table-row';
                    tableDiv.appendChild(rowDiv);
                    var cellDivLeft = document.createElement('div');
                    cellDivLeft.className = 'table-cell';
                    rowDiv.appendChild(cellDivLeft);
                    var img = document.createElement('img');

                    var photoLoaded = false;
                    contact.avatarUrl.get().then(function () {
                        img.src = contact.avatarUrl();
                        photoLoaded = true;
                    });
                    var checkPhotoLoaded = window.setInterval(function () {
                        if (photoLoaded) {
                            clearInterval(checkPhotoLoaded);
                            window.setTimeout(function (img) {
                                // if the photo isn't set revert back to a default
                                if (img.naturalWidth === 0 || img.naturalHeight === 0) {
                                    console.log('could not load user photo while popultating contact details');
                                    img.src = window.framework.getContentLocation() + 'images/samples/default.png';
                                }
                                cellDivLeft.appendChild(img);
                                var cellDivRight = document.createElement('div');
                                cellDivRight.className = 'table-cell';
                                rowDiv.appendChild(cellDivRight);
                                var statusImg = document.createElement('img');
                                statusImg.src = getStatusPath(contact.status());
                                cellDivRight.appendChild(statusImg);
                                var nameDiv = document.createElement('div');
                                nameDiv.className = 'name';
                                nameDiv.innerHTML = contact.displayName();
                                contact.displayName() && cellDivRight.appendChild(nameDiv);
                                var noteDiv = document.createElement('div');
                                noteDiv.className = 'name';
                                noteDiv.innerHTML = contact.note.text();
                                contact.note.text() && cellDivRight.appendChild(noteDiv);
                                processing = false;
                            }, 1000, img);
                        }
                    }, 500);
                }

                function loopOverAllContacts() {
                    if (processing) {
                        return;
                    }

                    processing = true; i++;
                    if (i == contacts.length) {
                        clearInterval(loopOverAllContacts);
                        window.framework.processingStatus = 'complete';
                        return;
                    }
                    var contact = contacts[i].result ? contacts[i].result : contacts[i];
                    if (contact.type && contact.type() == 'Phone') {
                        populateSingleContact(contact);
                    } else {
                        contact.status.get().then(function () {
                            populateSingleContact(contact);
                        });
                    }
                }
                var processing = false, i = -1;
                setInterval(loopOverAllContacts, 10);
            },
            populateGroups: function (groups, container) {
                for (var i = 0; i < groups.length; i++) {
                    var group = groups[i];
                    var groupDiv = document.createElement('div');
                    groupDiv.className = 'group';
                    container.appendChild(groupDiv);
                    var tableDiv = document.createElement('div');
                    tableDiv.className = 'table';
                    groupDiv.appendChild(tableDiv);

                    var groupName = group.name() ? group.name() : group.relationshipLevel();
                    groupName += ' (Contacts: ' + group.persons().length + ')';

                    window.framework.addDetail(tableDiv, groupName, 'name');
                }
            },
            addDetail: function (container, value, valueClass, header) {
                if (value) {
                    var rowDiv = document.createElement('div');
                    rowDiv.className = 'table-row';
                    container.appendChild(rowDiv);
                    var cellDiv = document.createElement('div');
                    cellDiv.className = 'table-cell';
                    rowDiv.appendChild(cellDiv);
                    if (header) {
                        var cellHeaderDiv = document.createElement('div');
                        cellHeaderDiv.innerHTML = header;
                        cellDiv.appendChild(cellHeaderDiv);
                    }
                    var cellContentDiv = document.createElement('div');
                    if (valueClass) {
                        cellContentDiv.className = valueClass;
                    }
                    cellContentDiv.innerHTML = value;
                    cellDiv.appendChild(cellContentDiv);
                }
            },
            addContactCardDetail: function (header, value, container) {
                if (value) {
                    var rowDiv = document.createElement('div');
                    rowDiv.className = 'mui-row';
                    var colLeftDiv = document.createElement('div');
                    colLeftDiv.className = 'mui-col-md-6';
                    colLeftDiv.style.fontWeight = 'bold';
                    colLeftDiv.innerHTML = header;
                    var colRightDiv = document.createElement('div');
                    colRightDiv.className = 'mui-col-md-6';
                    colRightDiv.style.fontStyle = 'italic';
                    colRightDiv.style.wordWrap = 'break-word';
                    colRightDiv.innerHTML = value;
                    rowDiv.appendChild(colLeftDiv);
                    rowDiv.appendChild(colRightDiv);
                    container.appendChild(rowDiv);
                }
            },
            createContactCard: function (contact, container) {
                contact.company.get().then(function () {
                    var contactCardDiv = document.createElement('div');
                    contactCardDiv.className = 'contactCard table';
                    container.appendChild(document.createElement('br'));
                    container.appendChild(contactCardDiv);
                    contact.displayName() && window.framework.addContactCardDetail('Name', contact.displayName(), contactCardDiv);
                    contact.company() && window.framework.addContactCardDetail('Company', contact.company(), contactCardDiv);
                    contact.department() && window.framework.addContactCardDetail('Department', contact.department(), contactCardDiv);
                    contact.id() && window.framework.addContactCardDetail('IM', window.framework.updateUserIdOutput(contact.id()), contactCardDiv);
                    contact.emails().length !== 0 && window.framework.addContactCardDetail('Email', contact.emails()[0].emailAddress(), contactCardDiv);
                    contact.phoneNumbers().length !== 0 && window.framework.addContactCardDetail('Phone', contact.phoneNumbers()[0].displayString(), contactCardDiv);
                });
            },
            addMessage: function (item, container) {
                // todo: make this asynchronous to enable fetching of the user photos 
                var div = document.createElement('div');
                div.className = 'item';
                container.appendChild(div);
                var tableDiv = document.createElement('div');
                tableDiv.className = 'table';
                div.appendChild(tableDiv);
                var rowDiv = document.createElement('div');
                rowDiv.className = 'table-row';
                tableDiv.appendChild(rowDiv);
                var leftCellDiv = document.createElement('div');
                leftCellDiv.className = 'table-cell';
                rowDiv.appendChild(leftCellDiv);
                if (item.direction() === 'Incoming') {
                    var img = document.createElement('img');
                    var imgUrl = window.framework.getContentLocation() + 'images/samples/default.png';
                    img.src = imgUrl;
                    img.style.height = '3em';
                    img.style.width = '3em';
                    leftCellDiv.appendChild(img);
                    var leftMiddleCellDiv = document.createElement('div');
                    leftMiddleCellDiv.className = 'table-cell';
                    rowDiv.appendChild(leftMiddleCellDiv);
                    var nameDiv = document.createElement('div');
                    var name = item.sender.displayName();
                    if (name.indexOf('sip:') !== -1) {
                        name = name.slice(4);
                    }
                    nameDiv.innerHTML = name + ':';
                    leftMiddleCellDiv.appendChild(nameDiv);
                }
                var middleCellDiv = document.createElement('div');
                middleCellDiv.className = 'table-cell';
                rowDiv.appendChild(middleCellDiv);
                var messageDiv = document.createElement('div');
                messageDiv.className = 'message-small';
                messageDiv.innerHTML = item.text();
                middleCellDiv.appendChild(messageDiv);
                var rightCellDiv = document.createElement('div');
                rightCellDiv.className = 'table-cell';
                rowDiv.appendChild(rightCellDiv);
                var timeDiv = document.createElement('div');
                timeDiv.innerHTML = item.timestamp().toLocaleTimeString();
                rightCellDiv.appendChild(timeDiv);
                container.scrollTop = container.scrollHeight;
            },
            updateAuthenticationList: function () {
                var sidebar = document.getElementsByClassName('sidebar')[0];
                sidebar.childNodes[0].childNodes[0].childNodes[0].childNodes[1].childNodes[0].style.display = 'none';
                sidebar.childNodes[0].childNodes[0].childNodes[0].childNodes[1].childNodes[1].style.display = 'none';
                sidebar.childNodes[0].childNodes[0].childNodes[0].childNodes[1].childNodes[2].style.display = 'none';
                sidebar.childNodes[0].childNodes[0].childNodes[0].childNodes[1].childNodes[3].style.display = 'none';
                var name = document.createElement('div');
                name.appendChild(document.createTextNode('Signed in as: ' + window.framework.application.personsAndGroupsManager.mePerson.displayName()));
                sidebar.childNodes[0].childNodes[0].childNodes[0].childNodes[1].appendChild(name);
                var photo = document.createElement('img');
                var photoLoaded = false;
                window.framework.application.personsAndGroupsManager.mePerson.avatarUrl.get().then(function () {
                    photo.src = window.framework.application.personsAndGroupsManager.mePerson.avatarUrl();
                    photoLoaded = true;
                });
                var checkPhotoLoaded = window.setInterval(function () {
                    if (photoLoaded) {
                        clearInterval(checkPhotoLoaded);
                        window.setTimeout(function (photo) {
                            // if the photo isn't set revert back to a default
                            if (photo.naturalWidth === 0 || photo.naturalHeight === 0) {
                                console.log('could not load user photo');
                                photo.src = window.framework.getContentLocation() + 'images/samples/default.png';
                            }
                            sidebar.childNodes[0].childNodes[0].childNodes[0].childNodes[1].appendChild(photo);
                        }, 1000, photo);
                    }
                }, 500);
            },
            utils: {
                guid: function () {
                    function s4() {
                        return Math.floor((1 + Math.random()) * 0x10000)
                        .toString(16)
                        .substring(1);
                    }
                    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                        s4() + '-' + s4() + s4() + s4();
                }
            }
            // createVideoContainer: function (container, size, person) {
            //     var name = person.displayName();
            //     var div = container.querySelector('div div.name[data-name="' + name + '"]');
            //     if (!div) {
            //         div = document.createElement('div');
            //         div.className = size;
            //         container.appendChild(div);
            //         var nameDiv = document.createElement('div');
            //         nameDiv.className = 'name';
            //         nameDiv.setAttribute('data-name', name);
            //         nameDiv.innerHTML = name;
            //         div.appendChild(nameDiv);
            //     } else {
            //         div = null;
            //     }

            //     return div;
            // }
        };

        // check for hash containing access_token
        var index = location.href.indexOf('#');
        if (index !== -1 && location.href.length > index + 1) {
            window.framework.auth = {};
            var hash = location.href.slice(index + 1);
            var items = hash.split('&');
            for (var i = 0; i < items.length; i++) {
                var parts = items[i].split('=');
                window.framework.auth[parts[0]] = parts[1];
            }

            if (window.history && window.history.replaceState) {
                //window.history.replaceState({}, document.title, '');
            }
        }
    }

    function getStatusPath(value) {
        var path = window.framework.getContentLocation() + 'images/samples/status/';

        switch (value) {
            case 'Online':
                return path + 'available.png';
            case 'Away':
            case 'BeRightBack':
            case 'IdleOnline':
                return path + 'away.png';
            case 'Busy':
            case 'IdleBusy':
                return path + 'busy.png';
            case 'DoNotDisturb':
                return path + 'do-not-disturb.png';
            default:
                return path + 'unknown.png';
        }
    }

    function populateSidebar() {
        // load config file to get data used for samples
        var request = createXHR();
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status === 200) {
                    processConfig(request.responseText);

                    // hack open the sidebar on authentication
                    var auth = document.querySelector('.sidebar > div > ul > li > div');
                    if (auth) {
                        auth.click();
                    }
                } else {
                    // TODO: we probably need to navigate to an error page...
                    window.console.log('Encountered error requesting configuration...');
                }
            }
        };

        var configUrl = window.framework.getContentLocation() + 'samples/config.json';
        request.open('get', configUrl);
        request.send();
    }

    function processConfig(data) {
        var config = JSON.parse(data);
        var sidebar = document.querySelector('.sidebar');
        var div = document.createElement('div');
        var ul = document.createElement('ul');

        if (config) {
            for (var i = 0; i < config.categories.length; i++) {
                addSamples(config.categories[i], ul);
            }
        }

        div.appendChild(ul);
        sidebar.appendChild(div);
    }

    function addSamples(category, list) {
        var li = document.createElement('li');
        var div = document.createElement('div');
        var ol = document.createElement('ol');

        li.appendChild(div);
        li.appendChild(ol);
        list.appendChild(li);

        div.innerHTML = category.name;

        // add preview to category
        if (category.preview) {
            var span = document.createElement('span');
            span.innerHTML = ' (Preview)';
            div.appendChild(span);
        }

        window.framework.addEventListener(div, 'click', function (e) {
            document.getElementsByClassName('azuread-signin')[0].style.display = 'none';
            for (var i = 0; i < document.getElementsByClassName('notification-bar').length; i++) {
                document.getElementsByClassName('notification-bar')[i].style.display = 'none';
            }
            var ol;
            if (e.target.innerHTML === ' (Preview)') {
                ol = e.target.parentElement.parentElement.children[1];
            } else {
                ol = e.target.parentElement.children[1];
            }

            if (ol.style.display === '' || ol.style.display === 'none') {
                ol.style.display = 'block';
            } else {
                ol.style.display = 'none';
            }
        });

        for (var i = 0; i < category.items.length; i++) {
            addSample(category.items[i], ol);
        }
    }

    function addSample(item, list) {
        var element = document.createElement('li');
        var anchor = document.createElement('a');
        var itemUrl = window.framework.getContentLocation() + item.location;
        anchor.setAttribute('data-location', itemUrl);
        anchor.href = '#';
        anchor.innerHTML = item.name;
        window.framework.addEventListener(anchor, 'click', function (e) {
            e.preventDefault();
            // hide the landing if it's still visible
            var landing = document.querySelector('.landing');
            if (landing.style.display !== 'none') {
                landing.style.display = 'none';
            }

            var src = e.target.getAttribute('data-location');
            var content = document.querySelector('.content > div[data-location="' + src + '"]');
            if (window.framework.findContentDiv()) {
                var location = window.framework.findContentDiv().getAttribute('data-location');
                if (window.framework.navigationCallbacks[location] && window.framework.navigationCallbacks[location]() === false) {
                    // if the callback returns true we should prevent content navigation
                    return;
                }
            }

            // clean up any stray UI on previous samples 
            document.getElementsByClassName('azuread-signin')[0].style.display = 'none';
            for (var i = 0; i < document.getElementsByClassName('contacts').length; i++) {
                document.getElementsByClassName('contacts')[i].innerHTML = '';
            }
            for (var i = 0; i < document.getElementsByClassName('contactcard').length; i++) {
                document.getElementsByClassName('contactcard')[i].innerHTML = '';
            }
            for (var i = 0; i < document.getElementsByClassName('notification-bar').length; i++) {
                document.getElementsByClassName('notification-bar')[i].style.display = 'none';
            }
            if (prevSelectedItem) {
                prevSelectedItem.removeAttribute('style');
            }
            prevSelectedItem = e.target;
            e.target.style.fontSize = '15px';
            e.target.style.fontWeight = 'bold';
            e.target.style.textDecoration = 'none';

            if (!content) {
                populateSample(src, '.content', '.html');
            } else {
                var oldContent = window.framework.findContentDiv();
                var activeStep = oldContent.querySelector('.step.activeStep');
                var firstStep = oldContent.querySelector('.step');
                if (activeStep !== firstStep) {
                    activeStep.className = activeStep.className.replace('activeStep', 'hidden');
                    firstStep.className = firstStep.className.replace('hidden', 'activeStep');
                }

                hideAllExcept('.content > div', content);
            }
        });

        element.appendChild(anchor);
        list.appendChild(element);
    }

    function populateSample(location, container, type) {
        var request = createXHR();
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status === 200) {
                    if (type === '.js') {
                        var data = parseScript(request.responseText.replace(/</g, "&lt;").replace(/>/g, "&gt;"), 'ignore');

                        if (!document.querySelector('script[src="' + location + '/index' + '.js"]')) {
                            populateSnippets(data, document.querySelector('.content > div[data-location="' + location + '"]'));

                            var script = document.createElement('script');
                            script.type = 'text/javascript';
                            script.src = location + '/index' + type;
                            document.body.appendChild(script);
                        } else {
                            window.console.log('script already added...');
                        }
                    } else {
                        var element = document.querySelector(container);
                        var div = document.createElement('div');
                        div.setAttribute('data-location', location);
                        div.innerHTML = request.responseText;

                        // search div and hide other steps~
                        var items = div.querySelectorAll('.step');
                        for (var i = 0; i < items.length; i++) {
                            if (items[i] === div.querySelector('.step:first-child')) {
                                items[i].className += ' activeStep';
                            } else {
                                items[i].className += ' hidden';
                            }
                        }

                        // now that Html is loaded we can safely load the related JS
                        populateSample(location, null, '.js');
                        element.appendChild(div);
                        hideAllExcept(container + ' > div', div);
                    }
                } else {
                    // TODO: we probably need to display an error for this sample...
                    window.console.log('Encountered error requesting sample...');
                }
            }
        };
        request.open('get', location + '/index' + type);
        request.send();
    }

    function parseScript(script, type) {
        var snippets = [];
        var lines = script.split('\r\n');
        var indexes = [];
        var index = 0;

        for (var i = 0; i < lines.length; i++) {
            if (lines[i].indexOf('// @' + type) !== -1) {
                indexes.push({
                    start: i
                });
            } else if (lines[i].indexOf('// @end_' + type) !== -1) {
                indexes[index++].end = i;
            }
        }

        for (var i = indexes.length - 1; i >= 0; i--) {
            if (type === 'snippet') {
                var snippet = lines.slice(indexes[i].start + 1, indexes[i].end);
                var firstNonWhitespace = lines[indexes[i].start].search(/\S/);
                for (var j = 0; j < snippet.length; j++) {
                    snippet[j] = snippet[j].slice(firstNonWhitespace);
                }
                lines.splice(indexes[i].start, 1);
                lines.splice(indexes[i].end - 1, 1);
                snippets.push(snippet.join('\r\n'));
            } else if (type === 'ignore') {
                lines.splice(indexes[i].start, indexes[i].end - indexes[i].start + 1);
            }
        }

        if (type === 'ignore') {
            return parseScript(lines.join('\r\n'), 'snippet');
        }
        else if (snippets.length !== 0) {
            snippets.reverse();
        }

        return snippets;
    }

    function populateSnippets(snippets, content) {
        if (snippets.length !== 0) {
            var divs = content.querySelectorAll('.snippet');
            var index = 0;

            for (var i = 0; i < divs.length; i++) {
                var pre = document.createElement('pre');
                pre.innerHTML = snippets[index];
                divs[i].appendChild(pre);
                index++;
            }
        }
    }

    function hideAllExcept(selector, obj) {
        var items = document.querySelectorAll(selector);
        for (var i = 0; i < items.length; i++) {
            if (items[i] === obj) {
                items[i].style.display = '';
            } else {
                items[i].style.display = 'none';
            }
        }
    }

    function initializeSkype() {
        Skype.initialize({
            apiKey: config.apiKeyCC,
            supportsAudio: true,
            supportsVideo: true,
            convLogSettings: true
        }, function (api) {
            window.framework.api = api;

            if (client_id && window.framework.auth) {
                if (window.framework.auth.error) {
                    var error = window.framework.auth.error_description;
                    error = decodeURIComponent(error.replace(/\+/g, '%20'));
                    document.getElementsByClassName('azuread-signin')[0].getElementsByTagName('text')[0].innerHTML = "Error during OAuth sign-in: " + error;
                } else {
                    document.getElementsByClassName('azuread-signin')[0].getElementsByTagName('text')[0].innerHTML = "Detected OAuth credentials, signing-in...";
                    document.getElementsByClassName('azuread-signin')[0].style.display = 'block';
                    window.framework.application = window.framework.api.UIApplicationInstance;
                    window.framework.application.signInManager.signIn({
                        version: config.version,
                        client_id: client_id,
                        origins: ["https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root"],
                        cors: true,
                        redirect_uri: location.href + 'websdk/token.html'
                    }).then(function () {
                        document.getElementsByClassName('before-signin')[0].style.display = 'none';
                        document.getElementsByClassName('azuread-signin')[0].style.display = 'none';
                        document.getElementsByClassName('after-signin')[0].style.display = 'block';
                        window.framework.updateAuthenticationList();
                    });
                }
            }
        }, function (err) {
            alert('Error initializing Skype Web SDK: ' + err);
        });
    }

    function autoAuthenticate() {

    }

    createFramework();
    populateSidebar();
    initializeSkype();
    autoAuthenticate();

    document.querySelector('#wrapper') && document.querySelector('#wrapper').classList.add('SDKSamples');
})();
