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
        var startMessageInput = {
            InviteTargetUri: "sip:liben@metio.onmicrosoft.com",
            WelcomeMessage: "Welcome!",
            IsStart: false,
            Subject: "HelpDesk",
            InvitedTargetDisplayName: 'Agent',
            EnableMessageFilter: false
        };
        
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

        
        function StoptMessageInvitationHandler() {            
            ajaxrequest('post', window.skypeWebApp.serviceUrl + '/IncomingMessagingBridgeJob',startMessageInput,'application/json').done(function () { console.log("Done"); });
        }
        
        function startIMAgainstUcap() {
            var index = 0;

            if (!window.skypeWebApp.TenantEndpointId || !window.skypeWebApp.personsAndGroupsManager || !window.skypeWebApp.personsAndGroupsManager.mePerson) {
                alert("Please sign in first");
                return;
            }

            uiToChatState();
            $(".modal").show();

            var conversation = client.conversationsManager.getConversation(window.skypeWebApp.TenantEndpointId);
            chatService = conversation.chatService;
            conversation.selfParticipant.chat.state.when("Connected", function (state) {
                $(".modal").hide();
                conversation.historyService.activityItems.added(function (message) {
                    index++;
                    if (!(message.sender.id() == client.personsAndGroupsManager.mePerson.id())) {
                        historyAppend(XMessage(message));
                    } else {
                        historyAppend(XMessage(message));
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
            });

            //customContent to ucap application endpoint
            var costumContext = {
                context: 'This is a custom message',
                contextType: 'foo/foo',//must be foo/foo
            };

            chatService.start().then(function () {
                console.log('conversation started');
                $('.chat-service').show();
            }, function (error) {
                alert('Failed to start chat service');
                $(".modal").hide();
            });
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

        //startInstantMessaging();
        startIMAgainstUcap();
    };
});