// we want to dispose all the previous conversations added event listeners because
// in this demo site, we don't want to samples interfere with each other.
var registeredListeners = registeredListeners || [];
registeredListeners.forEach(function (listener) {
    listener.dispose();
});
registeredListeners = [];
/**
 * This script demonstrates how to use audio/video along with messaging in an existing conversation
 */
$(function () {
    'use strict';
    window['conference_load'] = function () {
        if (window['noMeResource']) {
            $('.container .content .noMe').show();
        }
        var client = window.skypeWebApp,
            xHistory = $('#message-history'),
            chatservice,
            timerId;

        function registerAppListeners() {
            var convAddedListener = client.conversationsManager.conversations.added(function (conversation) {

                displayName(document.querySelector('.self'), conversation.selfParticipant);

                registeredListeners.push(subscribeToSelfAudioState());
                registeredListeners.push(subscribeToSelfVideoState());

                if (conversation.videoService.videoMode() === 'MultiView') {
                    subscribeToParticipantsAdded();
                } else if (conversation.videoService.videoMode() === 'ActiveSpeaker') {
                    subscribeToActiveSpeaker();
                }

                conversation.state.changed(function onDisconnect(state) {
                    if (state === 'Disconnected') {
                        conversation.state.changed.off(onDisconnect);
                        client.conversationsManager.conversations.remove(conversation);
                    }
                });


                function subscribeToSelfAudioState() {
                    return conversation.selfParticipant.audio.state.changed(function (newState, reason, oldState) {
                        if (newState === 'Notified' && !timerId)
                            timerId = setTimeout(onAudioVideoNotified, 0);
                    });
                }

                function subscribeToSelfVideoState() {
                    return conversation.selfParticipant.video.state.changed(function (newState, reason, oldState) {
                        var channel;
                        if (newState === 'Notified' && !timerId) {
                            timerId = setTimeout(onAudioVideoNotified, 0);
                        } else if (newState === 'Connected') {
                            channel = conversation.selfParticipant.video.channels(0);
                            channel.stream.source.sink.container.set(document.getElementById("previewWindow"));
                        }
                    });
                }

                function subscribeToParticipantsAdded() {

                    var participantsAddedListnr = conversation.participants.added(function (participant) {
                        participant.person.id.get().then(function (id) {
                            var className = 'participant' + id.match(/sip:(.*)@.*/)[1],
                                container = createContainer(className, 'Participant');

                            displayName(document.querySelector('.' + className), participant);

                            var videoStateListnr = participant.video.state.when('Connected', function () {

                                participant.video.channels(0).stream.source.sink.container(container.querySelector('#renderWindow'));

                                var videoOnListnr = participant.video.channels(0).isVideoOn.when(true, function () {
                                    participant.video.channels(0).isStarted(true);
                                });

                                var videoOffListnr = participant.video.channels(0).isVideoOn.when(false, function () {
                                    participant.video.channels(0).isStarted(false);
                                });

                                registeredListeners.push(videoOnListnr);
                                registeredListeners.push(videoOffListnr);
                            });

                            registeredListeners.push(videoStateListnr);
                        });
                    });

                    var participantsRemovedListnr = conversation.participants.removed(function (participant) {
                        var className = 'participant' + participant.person.id().match(/sip:(.*)@.*/)[1],
                            nodeToBeRemoved = document.querySelector('.' + className);

                        nodeToBeRemoved.parentNode.removeChild(nodeToBeRemoved);
                    });

                    registeredListeners.push(participantsAddedListnr);
                    registeredListeners.push(participantsRemovedListnr);
                }

                function subscribeToActiveSpeaker() {
                    var activeSpeaker = conversation.videoService.activeSpeaker,
                        className = 'activeSpeaker',
                        container = createContainer(className);

                    activeSpeaker.channel.stream.source.sink.container(container.querySelector('#renderWindow'));

                    var videoOnListener = activeSpeaker.channel.isVideoOn.when(true, function () {
                        activeSpeaker.channel.isStarted(true);
                        console.log('ActiveSpeaker video is available and has been turned on.');
                    });

                    var videoOffListener = activeSpeaker.channel.isVideoOn.when(false, function () {
                        activeSpeaker.channel.isStarted(false);
                        console.log('ActiveSpeaker video is not available anymore and has been turned off.');
                    });

                    // the .participant object changes when the active speaker changes
                    var participantChangedListener = activeSpeaker.participant.changed(function (newValue, reason, oldValue) {
                        console.log('The ActiveSpeaker has changed. Old ActiveSpeaker:', oldValue && oldValue.displayName(), 'New ActiveSpeaker:', newValue && newValue.displayName());

                        if (newValue) {
                            displayName(document.querySelector('.' + className), newValue);
                        }
                    });

                    registeredListeners.push(videoOnListener);
                    registeredListeners.push(videoOffListener);
                    registeredListeners.push(participantChangedListener);
                }
            });

            registeredListeners.push(convAddedListener);
        }

        function onAudioVideoNotified() {
            // AV invitation may come from a 1:1 conversation only, so the caller is
            // the single participant in the participants collection
            var name = conversation.participants(0).person.displayName();
            if (selfParticipant.video.state() == 'Notified') {
                if (confirm('Accept a video call from ' + name + '?')) {
                    console.log('accepting a video call');
                    // selfParticipant video stream container can be set before we 
                    // accept the incominng video call or after it is accepted or even 
                    // later, when the selfParticipant video state becomes "Connected"
                    dfdVideoAccept = videoService.accept();
                    monitor('Accepting video request from ' + name, dfdVideoAccept);
                }
                else if (confirm('Accept a video call from ' + name + ' with audio only?\n' +
                    '(You will still see the incoming video)')) {
                    console.log('accepting a video call with audio');
                    dfdAudioAccept = audioService.accept();
                    monitor('Accepting audio request from ' + name, dfdAudioAccept);
                }
                else {
                    console.log('declining the incoming video request');
                    videoService.reject();
                }
            }
            else if (selfParticipant.audio.state() == 'Notified') {
                if (confirm('Accept an audio call from ' + name + '?')) {
                    console.log('accepting the audio call');
                    dfdAudioAccept = audioService.accept();
                    monitor('Accepting audio call from ' + name, dfdAudioAccept);
                }
                else {
                    console.log('declining the incoming audio request');
                    audioService.reject();
                }
            }
            timerId = null;
        }

        function addParticipant(conv, uri) {
            var person, participant, searchQuery;
            searchQuery = client.personsAndGroupsManager.createPersonSearchQuery();
            searchQuery.text(uri);
            return searchQuery.getMore().then(function (results) {
                person = results[0].result;
                participant = conv.createParticipant(person);
                conv.participants.add(participant);
                conv.chatService.sendMessage('Hi, meeting now!');
            });
        }


        function XMessage(message) {
            var xTitle = $('<div>').addClass('sender');
            var xStatus = $('<div>').addClass('status');
            var xText = $('<div>').addClass('text').text(message.text());
            var xMessage = $('<div>').addClass('message');
            xMessage.append(xTitle, xStatus, xText);
            if (message.sender) {
                message.sender.displayName.get().then(function (displayName) {
                    xTitle.text(displayName);
                });

                if (message.sender.displayName()) {
                    xTitle.text(message.sender.displayName());
                }
            }
            message.status.changed(function (status) {
                //xStatus.text(status);
            });
            if (message.sender.id() == client.personsAndGroupsManager.mePerson.id()) xMessage.addClass("fromMe");
            return xMessage;
        }

        function historyAppend(message) {
            xHistory.append(message);
            xHistory.animate({
                "scrollTop": $('#message-history')[0].scrollHeight
            }, 'fast');
        }

        function sendMessage() {
            var message = $("#input-message").text();
            if (message) {
                chatservice.sendMessage(message).catch(function () {
                    console.log('Cannot send the message');
                });
            }
            $("#input-message").text("");
        }

        function loadconui() {
            var url = "samples/html/conversation.html";
            $.get(url, function (html) {
                $('#confcon').html(html);

                $('#input-message').on('keypress', function (evt) {
                    if (evt.keyCode == 13) {
                        evt.preventDefault();
                        sendMessage();
                    }
                });


                xHistory = $('#message-history');
            });
        }

        function startmodality(modality) {
            console.log(modality);

            var codesnip = ' conv = client.conversationsManager.conversations[0];';
            $('.codeBody pre').text(codesnip);

            switch (modality) {
                case 'chat':
                    $('#startChatMeeting').hide();
                    $('#startChatMeeting').trigger("click");

                    break;
                case 'audio':
                    $('#startAudioMeeting').hide();
                    $('#startAudioMeeting').trigger("click");

                    break;
                case 'video':
                    $('#startVideoMeeting').hide();
                    $('#startAudioMeeting').hide();
                    $('#startVideoMeeting').trigger("click");

                    break;
                default:
                    console.log('invalid modality');
            }
        }

        function displayName(container, person) {
            container.querySelector('.displayName').innerHTML = person.displayName();
        }

        function createContainer(className, heading) {
            var container = $('.conversation.template').clone();
            container.appendTo('.conversations');

            container.addClass(className);
            container.removeClass('template');
            container[0].style.display = '';

            if (heading) {
                container[0].querySelector('.heading').innerHTML = heading;
            }

            return container[0];
        }

        function reset() {
            $('#startVideoMeeting').show();
            $('#startAudioMeeting').show();
            $('#startChatMeeting').show();

            $('#endMeeting').hide();
        }

        function enableInCallButtons() {
            $('#startVideoMeeting').hide();
            $('#startAudioMeeting').hide();
            $('#startChatMeeting').hide();

            $('#endMeeting').show();
        }

        function handleMeetingStartError(error) {
            console.error('An error occured joining the conversation:', error);
            if (error.code && error.code == 'PluginNotInstalled') {
                console.log('You can install the plugin from:');
                console.log('(Windows) https://swx.cdn.skype.com/s4b-plugin/16.2.0.67/SkypeMeetingsApp.msi');
                console.log('(Mac) https://swx.cdn.skype.com/s4b-plugin/16.2.0.67/SkypeForBusinessPlugin.pkg');
            }
        }

        // join an online meeting and start chat
        $('#startChatMeeting').click(function () {

            loadconui();
            var uri = $('#meetingUri').text(), conv, dfd;
            conv = client.conversationsManager.conversations(0);

            conv.selfParticipant.chat.state.when("Connected", function (state) {
                $(".modal").hide();
                conv.historyService.activityItems.added(function (message) {

                    if (!(message.sender.id() == client.personsAndGroupsManager.mePerson.id())) {
                        historyAppend(XMessage(message));
                    } else {
                        historyAppend(XMessage(message));
                    }
                });
            });

            chatservice = conv.chatService;
            dfd = chatservice.start().then(function () {
                chatservice.sendMessage('Hi');
            });
        });

        // join an online meeting and start audio
        $('#startAudioMeeting').click(function () {
            var uri = $('#meetingUri').text(), conv, dfd;
            conv = client.conversationsManager.conversations(0);
            
            conv.audioService.start().then(null, handleMeetingStartError);
            enableInCallButtons();
        });

        // join an online meeting and start video
        $('#startVideoMeeting').click(function () {
            var uri = $('#meetingUri').text(), conv, dfd;
            conv = client.conversationsManager.conversations(0);

            conv.videoService.start().then(null, handleMeetingStartError);
            enableInCallButtons();
        });

        $('#endMeeting').click(function () {
            var conversation = client.conversationsManager.conversations(0);

            conversation.leave().then(function () {
                console.log('The conversation has ended.');
            }, function (error) {
                console.error('An error occured ending the conversation:', error);
            }).then(function () {
                reset();
            });
        });

        $(".contactAdd").click(function () {
            $(".add-p-container").toggleClass("hide");
        });

        $("#btn-add-participant").click(function () {
            var conv = client.conversationsManager.conversations(0), uri = $('#txt-contact').val(), dfd;
            if (conv) {
                dfd = addParticipant(conv, uri);
            }
            $(".av-controls").show();
            $(".add-p-container").hide();
        });


        registerAppListeners();

        //start modality automatically
        setTimeout(startmodality(window.skypeWebApp.modality), 20);
    };
});
