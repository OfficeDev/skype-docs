// we want to dispose all the previous conversations added event listeners because
// in this demo site, we don't want to samples interfere with each other.
var registeredListeners = registeredListeners || [];
registeredListeners.forEach(function(listener) {
    listener.dispose();
});
registeredListeners = [];
/**
 * This script demonstrates how to send instant messages
 * with the SkypeWeb Conversation model.
 */
$(function() {
    'use strict';
    window['chat-service_load'] = function() {
        if (window['noMeResource']) {
            $('.container .content .noMe').show();
        }
        if (window.skypeWebApp.TenantEndpointId) {
            $('#chat-to').text(window.skypeWebApp.TenantEndpointId);
        }

        var client = window.skypeWebApp;
        var chatService;
        var xHistory = $('#message-history');
        var incomingMessageCount = 0;
        var addedListener = client.conversationsManager.conversations.added(function(conversation) {
            var con = client.conversationsManager.conversations(0);
            if (con && con.selfParticipant.chat.state() == "Connected") return;
            chatService = conversation.chatService;
            chatService.accept.enabled.when(true, function() {
                // instead of using chatService.accept.enabled.changed, selfParticipant.chat.state.changed should also work.
                // conversation.selfParticipant.chat.state.changed(function (state) {
                var fAccept = confirm("Accept this IM invitation?");
                if (fAccept) {
                    incomingMessageCount = 0;
                    chatService.accept();
                    uiToChatState();
                    $(".chat-name").text(conversation.participants(0).person.displayName());
                } else {
                    chatService.reject();
                }
                conversation.historyService.activityItems.added(function(message) {
                    incomingMessageCount++;
                    if (incomingMessageCount != 2) {
                        historyAppend(XMessage(message));
                    }
                });
            });
        });
        registeredListeners.push(addedListener);
        var removedListener = client.conversationsManager.conversations.removed(function(conversation) {
            console.log('one conversation is removed');
        });
        registeredListeners.push(removedListener);
        //startInstantMessaging();
        startIMAgainstUcap();
        $('#btn-start-messaging').click(function() {
            //startInstantMessaging();
        });
        $('#chat-to').keypress(function(evt) {
            if (evt.keyCode == 13) {
                evt.preventDefault();
                //startInstantMessaging();
                startIMAgainstUcap();
            }
        });
        $("#btn-send-message").click(function() {
            sendMessage();
        });
        $('#input-message').on('keypress', function(evt) {
            if (evt.keyCode == 13) {
                evt.preventDefault();
                sendMessage();
            }
        });

        function sendMessage() {
            var message = $("#input-message").text();
            if (message) {
                chatService.sendMessage(message).catch(function() {
                    console.log('Cannot send the message');
                });
            }
            $("#input-message").text("");
        }

        var startMessageInput = {
            InviteTargetUri: "sip:toshm@metio.onmicrosoft.com",
            WelcomeMessage: "Welcome!",
            IsStart: false,
            Subject: "HelpDesk",
            InvitedTargetDisplayName: 'Agent'
        };

        function StoptMessageInvitationHandler() {            
            ajaxrequest('post', window.skypeWebApp.serviceUrl + '/IncomingMessagingBridgeJob',startMessageInput,'application/json').done(function () { console.log("Done"); });
        }

        function addNotification(text) {
            $(".notification").text(text);
        }

        function startIMAgainstUcap() {
            var index = 0;

            if (window.skypeWebApp.TenantEndpointId) {
                uiToChatState();
                $(".modal").show();

                var conversation = client.conversationsManager.getConversation(window.skypeWebApp.TenantEndpointId);
                conversation.topic.set('abc');
                chatService = conversation.chatService;
                conversation.selfParticipant.chat.state.when("Connected", function (state) {
                    addNotification('Conversation state: ' + state);
                    addNotification('Now you can send messages');
                    $(".modal").hide();
                    conversation.historyService.activityItems.added(function (message) {
                        index++;
                        if (!(message.sender.id() == client.personsAndGroupsManager.mePerson.id())) {
                            historyAppend(XMessage(message));
                        } else {
                            if (index % 2 != 0) {
                                historyAppend(XMessage(message));
                            }
                            if (message.text().toLowerCase().indexOf('bye') > -1 || message.text().toLowerCase().indexOf(' goodbye') > -1) {
                                //terminate the conversation
                                StoptMessageInvitationHandler();
                                chatService.stop().then(function () {
                                    console.log('Chat service stopped');
                                });
                                //could not send message when conversation stoped.
                                $("#input-message").hide();
                            }
                        }
                    });
                    //try to start audi here:
                    //var dfd = conversation.audioService.start();
                });
                // participant audio and video state changes
                conversation.participants.added(function (p) {
                    if (conversation.participants.size() == 1) {
                        $("#av-to").text(p.person.id());
                        $(".c-name").text(p.person.displayName());
                        p.person.location.changed(function (location) {
                            $(".location").text(location);
                        });
                        p.person.location.subscribe();
                    }
                    p.video.state.changed(function (newState, reason, oldState) {
                        // a convenient place to set the video stream container 
                        if (newState == 'Connected') {
                            if (conversation.participants.size() == 1) {
                                p.video.channels(0).stream.source.sink.container(document.getElementById("render-p-window"));
                            } else {
                                var partcipant = conversation.participants(0);
                                partcipant.video.channels(0).stream.source.sink.container(document.getElementById("render-p-window"));
                                partcipant.video.channels(0).isStarted.set(true);
                                p.video.channels(0).stream.source.sink.container($(".add-video-container")[0]);
                                p.video.channels(0).isStarted.set(true);
                            }
                        }
                    });
                });

                chatService.start().then(function () { console.log('conversation started');});
            }
        }

        function startInstantMessaging() {
            var pSearch = client.personsAndGroupsManager.createPersonSearchQuery();
            var index = 0;
            pSearch.limit(1);
            pSearch.text($('#chat-to').text());
            pSearch.getMore().then(function() {
                var sr = pSearch.results();
                if (sr.length < 1) throw new Error('Contact not found');
                return sr[0].result;
            }).then(function(contact) {
                uiToChatState();
                $(".chat-name").text(contact.displayName());
                var conversation = client.conversationsManager.getConversation(contact);
                chatService = conversation.chatService;
                conversation.selfParticipant.chat.state.when("Connected", function(state) {
                    addNotification('Conversation state: ' + state);
                    addNotification('Now you can send messages');
                    conversation.historyService.activityItems.added(function(message) {
                        historyAppend(XMessage(message));
                    });
                });
                chatService.start().then(function() {
                    chatService.sendMessage('How are you?');
                });
            }).then(null, function(error) {
                //this might be an Ucap application try to get application based on sip uri directly
                if (window.skypeWebApp.TenantEndpointId) {
                    uiToChatState();
                    $(".modal").show();

                    var conversation = client.conversationsManager.getConversation(window.skypeWebApp.TenantEndpointId);
                    conversation.topic.set('ABC');
                    chatService = conversation.chatService;
                    conversation.selfParticipant.chat.state.when("Connected", function (state) {
                        addNotification('Conversation state: ' + state);
                        addNotification('Now you can send messages');
                        $(".modal").hide();
                        conversation.historyService.activityItems.added(function(message) {
                            index++;
                            if (!(message.sender.id() == client.personsAndGroupsManager.mePerson.id())) {
                                historyAppend(XMessage(message));
                            } else {
                                if (index % 2 != 0) {
                                    historyAppend(XMessage(message));
                                }
                                if (message.text().toLowerCase().indexOf('bye') > -1 || message.text().toLowerCase().indexOf(' goodbye') > -1) {
                                    //terminate the conversation
                                    StoptMessageInvitationHandler();
                                    chatService.stop().then(function() {
                                        console.log('Chat service stopped');
                                    });
                                    //could not send message when conversation stoped.
                                    $("#input-message").hide();
                                }
                            }
                        });
                        //try to start audi here:
                        //var dfd = conversation.audioService.start();
                    });
                    // participant audio and video state changes
                    conversation.participants.added(function(p) {
                        if (conversation.participants.size() == 1) {
                            $("#av-to").text(p.person.id());
                            $(".c-name").text(p.person.displayName());
                            p.person.location.changed(function(location) {
                                $(".location").text(location);
                            });
                            p.person.location.subscribe();
                        }
                        p.video.state.changed(function(newState, reason, oldState) {
                            // a convenient place to set the video stream container 
                            if (newState == 'Connected') {
                                if (conversation.participants.size() == 1) {
                                    p.video.channels(0).stream.source.sink.container(document.getElementById("render-p-window"));
                                } else {
                                    var partcipant = conversation.participants(0);
                                    partcipant.video.channels(0).stream.source.sink.container(document.getElementById("render-p-window"));
                                    partcipant.video.channels(0).isStarted.set(true);
                                    p.video.channels(0).stream.source.sink.container($(".add-video-container")[0]);
                                    p.video.channels(0).isStarted.set(true);
                                }
                            }
                        });
                    });
                    chatService.start().then(function() {
                        //chatService.sendMessage('How are you?');
                    });
                } else {
                    console.error(error);
                    addNotification('Search failed ' + error);
                }
            });

            function addNotification(text) {
                $(".notification").text(text);
            }
        }
        // returns a DOM element attached to the Message model
        function XMessage(message) {
            var xTitle = $('<div>').addClass('sender');
            var xStatus = $('<div>').addClass('status');
            var xText = $('<div>').addClass('text').text(message.text());
            var xMessage = $('<div>').addClass('message');
            xMessage.append(xTitle, xStatus, xText);
            if (message.sender) {
                message.sender.displayName.get().then(function(displayName) {
                    xTitle.text(displayName);
                });

                if (message.sender.displayName()) {
                    xTitle.text(message.sender.displayName());
                }
            }
            message.status.changed(function(status) {
                //xStatus.text(status);
            });
            if (message.sender.id() == client.personsAndGroupsManager.mePerson.id()) xMessage.addClass("fromMe");
            return xMessage;
        }
        $('#btn-stop-messaging').click(function() {
            chatService.stop().then(function() {
                uiToStartState();
            });
        });

        function uiToChatState() {
            $("#input-message").show();
            $("#start").hide();
            $('#status-header').show();
        }

        function uiToStartState() {
            $("#message-history").empty();
            $("#input-message").hide();
            $("#start").show();
            $('#status-header').hide();
        }

        function historyAppend(message) {
            xHistory.append(message);
            xHistory.animate({
                "scrollTop": xHistory[0].scrollHeight
            }, 'fast');
        }

        window.codifyElement($('#codeSnippet5'), 'rawdata/Snippet5.txt');
    };
});