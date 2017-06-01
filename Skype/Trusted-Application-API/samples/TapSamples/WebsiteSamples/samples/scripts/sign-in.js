//sign in sample:
//if user has signed in give prompt, otherwise go to index page
$(function () {
    'use strict';
    window['sign-in_load'] = function () {
        if (window.skypeWebApp && window.skypeWebApp.signInManager.state() == "SignedIn") {
            $('.wrappingdiv .signed-in').show();
            $('.anonlogic').show();
            window.codifyElement($('#codeSnippet3'), 'rawdata/snippet3.txt');
            window.codifyElement($('#codeSnippet4'), 'rawdata/snippet4.txt');           
            return;
        }

        window.codifyElement($('#codeSnippet1'), 'rawdata/getToken.txt');
        window.codifyElement($('#codeSnippet2'), 'rawdata/snippet2.txt');
        window.codifyElement($('#codeSnippet3'), 'rawdata/snippet3.txt');
        window.codifyElement($('#codeSnippet4'), 'rawdata/snippet4.txt');
        window.codifyElement($('#getaadtoken'), 'rawdata/getaadtoken.txt', true);
        window.codifyElement($('#getdiscoverurl_1'), 'rawdata/getdiscoverurl_1.txt');
        window.codifyElement($('#getdiscoverurl_2'), 'rawdata/getdiscoverurl_2.txt');
        window.codifyElement($('#getapplication'), 'rawdata/getapplication.txt');
        window.codifyElement($('#getapplicationtoken'), 'rawdata/getapplicationtoken.txt');
        window.codifyElement($('#getdiscoverurl'), 'rawdata/getdiscoverurl.txt');
        window.codifyElement($('#ivr'), 'rawdata/ivr.txt');
        $('.ivrsample #ivr a').hide();
        window.codifyElement($('#notification'), 'rawdata/notification.txt');
        $('#notificationsample #notification a').hide();
        window.codifyElement($('#adhocmeeting-snippet'), 'rawdata/adhocmeeting.txt');
        window.codifyElement($('#adhocmeeting-token-snippet'), 'rawdata/adhocmeetingtoken.txt');

        var confid, meetingurl;
        var joinconf = false;
        var options = {};
        var anonmeetingsignin = {};
        var trustedappclient = trustedappwebclient();

        var adhocsignin = {
            origins: "",
            use_cwt: "",
            name: "",
            meeting: ""
        }

        function signin(options) {
            window.skypeWebApp.signInManager.signIn(options).then(function () {
                // when the sign in operation succeeds display the user name
                $(".modal").hide();
                console.log('Signed in as ' + window.skypeWebApp.personsAndGroupsManager.mePerson.displayName());
                if (!window.skypeWebApp.personsAndGroupsManager.mePerson.id() && !window.skypeWebApp.personsAndGroupsManager.mePerson.avatarUrl() && !window.skypeWebApp.personsAndGroupsManager.mePerson.email() && !window.skypeWebApp.personsAndGroupsManager.mePerson.displayName() && !window.skypeWebApp.personsAndGroupsManager.mePerson.title()) {
                    window['noMeResource'] = true;
                }
                $("#anonymous-join").addClass("disable");                
                $('.wrappingdiv .signed-in').show();
            }, function (error) {
                // if something goes wrong in either of the steps above,
                // display the error message
                $(".modal").hide();
                alert("Can't sign in, please check the user name and password.");
                console.log(error || 'Cannot sign in');
            });
        }

        function testForConfigAndSignIn(options) {
            if (document.getElementById('chk-useConvoControl') && document.getElementById('chk-useConvoControl').checked) {
                Skype.initialize({
                    apiKey: config.apiKeyCC
                }, function (api) {
                    window.skypeWebApp = api.UIApplicationInstance;
                    window.skypeApi = api;
                    window.skypeWebAppCtor = api.application;
                    signin(options);
                }, function (err) {
                    console.log(err);
                });
            } else {
                signin(options);
            }
        }

        //Imbridge sign in with token
        $('#btn-anon-sign-in').click(function () {
            $(".modal").show();
            if (options.token || options.root) {
                testForConfigAndSignIn(options);
                $('.anonlogic').show();
                window.codifyElement($('#codeSnippet3'), 'rawdata/snippet3.txt');
                window.codifyElement($('#codeSnippet4'), 'rawdata/snippet4.txt');
                window.scrollTo(0, document.body.scrollHeight);
                //start imbridge job .
                trustedappclient.startimbridge();
            }
        });

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
        };

        $('#link-startchat').click(function () {
            $(".menu #chat-service").click();
            $(".menu #sign-in").addClass("disable");
            $(".menu #conference").addClass("disable");            
        });

        //Get anon tokens
        $('#btn-get-anontoken').click(function (evt) {
            $(".modal").show();
            trustedappclient.getanontoken()
                .done(function (d) {
                    $(".modal").hide();
                    var token = JSON.parse(d);
                    window.skypeWebApp.TenantEndpointId = token.TenantEndpointId;
                    renderParameters(token);
                    //anonMeeting sign in option
                    options = {
                        name: "Guest user",
                        token: "Bearer " + token.Token,
                        root: {
                            user: token.DiscoverUri
                        },
                        cors: true
                    };
                })
            .fail(function () {
                $(".modal").hide();
                alert('Failed to get anon meeting token, please try again.');
            });
        });
        
        //anon meetiong token sign in
        $('#btn-anontoken-signin').click(function () {
            $(".modal").show();
            testForConfigAndSignIn(anonmeetingsignin);
            $('#btn-startmodality-conf').show();
            $('#anonstatus').text('Click below button to start IM or AV from meeting.');
            
            $('#confmodality').show();            
        });

        // join an online meeting and start chat
        $('#startChatMeeting').click(function () {
             $(".menu #sign-in").addClass("disable");
            $(".menu #chat-service").addClass("disable");
            $(".menu #conference").click();  
            window.skypeWebApp.modality = 'chat';  
        });

        // join an online meeting and start audio
        $('#startAudioMeeting').click(function () {
            $(".menu #sign-in").addClass("disable");
            $(".menu #chat-service").addClass("disable");
            $(".menu #conference").click();  
            window.skypeWebApp.modality = 'audio';  
        });

        // join an online meeting and start video
        $('#startVideoMeeting').click(function () {
           $(".menu #sign-in").addClass("disable");
            $(".menu #chat-service").addClass("disable");
            $(".menu #conference").click();  
            window.skypeWebApp.modality = 'video';  
        });


        //Get anon meetiong tokens based on meeting url
        $('#btn-get-anonmeetingtoken').click(function (evt) {
            //get token from web api and then use it to log in from ucwa
            if ($('#txt-meetingurl').val()) {
                meetingurl = $('#txt-meetingurl').val();
                //todo generate meeting url in other format
            }
            else {
                alert('Meeting url could not be null');
                return;
            }
            $(".modal").show();

            $('#anonstatus').text('Getting tokens...');
            trustedappclient.getanontoken(meetingurl)
                .done(function (d) {
                var token = JSON.parse(d);
                if (token) {
                    var tokenRaw = token.Token;
                    var user = token.DiscoverUri;

                    anonmeetingsignin = {
                        name: 'AnonUser',
                        cors: true,
                        root: { user: user },
                        auth: function (req, send) {
                            // the GET /discover must be sent without the token
                            if (req.url != user)
                                req.headers['Authorization'] = "Bearer " + tokenRaw;
                            return send(req);
                        }
                    };
                    if (user && tokenRaw) {
                        $('#token').text('token: ' + tokenRaw);
                        $('#discoveruri').text('discoverUri: ' + user);
                        $('#anonmeetingresource').show();
                        $('#btn-anontoken-signin').show();
                        $('#anonstatus').text('Got tokens, click to sign in.');
                    }

                    $(".modal").hide();

                }
                })
            .fail(function () {
                alert('Failed to get anon meeting token, please try again.');
                $(".modal").hide();
            });
        });    


        //Send out message to other user
        function sendNotification() {
            var msg = $('#inputmsg').val();
            var sip = $('#sendto').val();

            if ((sip == "") || (msg == "") || sip.indexOf('@') < 0 || sip.indexOf('sip:') < 0) {
                alert('Please input valid sip Uri or message');
                return;
            }

            var notifydata = {
                TargetUri: sip,
                NotificationMessage: msg
            }

            $('#status').html('sending..');

            trustedappclient.notification(sip, msg)
                .done(function (data) {
                    $('#status').html("Send out message successfully, agent will get message very soon.");
                })
            .fail(function () {
                alert('Failed to send out request, please try again.');
            });
        }

        function startIVR() {
            var sip = $('#ivrto').val();
            trustedappclient.deletjob().always(
                function () {
                    trustedappclient.startivr(sip).done(function (data) {
                        $('#status').html("IVR started, listening for incoming call.");
                    }).fail(function () {
                        alert('Failed to start AV bridge job, please try again.');
                    });
                });
        }

        //Place holder get adhocmeeting info from ucap
        function GetAdhocMeeting() {
            $(".modal").show();
            trustedappclient.getadhocmeeting()
            .done(function (d) {
                var data = JSON.parse(d);
                $(".modal").hide();

                if (data) {
                    $('#txt-meetingurl').val(data.JoinUrl);
                    $('#meetingUrl').text('meetingUrl: ' + data.JoinUrl);
                    $('#discoverUri').text('discoverUri: ' + data.DiscoverUri);
                    $('#onlineMeetingUri').text('onlineMeetingUri: ' + data.OnlineMeetingUri);
                    $('#adhocmeetingresources').show();

                    window.scrollTo(0, document.body.scrollHeight);
                }
            });
            
        }


        function renderParameters(data) {
            if (data.AnonymousToken || data.DiscoverUri) {
                $('#discoveryuri').text('DiscoveryUri is used for routing: ' + data.DiscoverUri);
                $('#anonymousetoken').text('AnonymousToken is used for authentication: ' + data.Token);
                $('#tenantEndpointid').text('TenantEndpointId is the application endpoint registered from platformservice: ' + data.TenantEndpointId);
                $('#anontokenresult').show();
            }
            window.scrollTo(0, document.body.scrollHeight);
        }

        $('#btn-get-adhoc').click(function () {            
            GetAdhocMeeting();
        });

        
        $('#btn-token-sign-in').click(function () {
            $(".modal").show();
            var domain = $("#txt-domain").val();
            var access_token = $("#txt-token").val();
            var Bearercwt = 'Bearer cwt=';
            var Bearer = 'Bearer ';
            var cwt = 'cwt';
            if (access_token.indexOf(cwt) == -1) {
                access_token = Bearercwt + access_token;
            }
            if (access_token.indexOf(Bearer) == -1) {
                access_token = Bearer + access_token;
            }
        });

        $('.topology-login').click(function () {
            $(".login-options").hide();
            $(".token-sign-in").hide();
            $('.useConvoControl').show();
            $(".sign-in").show();
        });
        $('.token-login').click(function () {
            $(".login-options").hide();
            $(".token-sign-in").show();
            $('.useConvoControl').show();
            $(".sign-in").hide();
        });
        $('.anon-login').click(function () {
            $(".login-options").hide();
            $(".sign-in").hide();
            $(".anon-sign-in").show();
            $('.useConvoControl').show();
            $('.signinwithtoken').show();
        });


        $('.adhoc-login').click(function () {
            $(".login-options").hide();
            $(".sign-in").hide();
            $('.anon-login').hide();
            $('#btn-get-adhoc').show();
            $('.get-adhocmeeting').show();
        });

        $('.anonmeeting-login').click(function () {
            $(".login-options").hide();
            $(".sign-in").hide();
            $('.anon-login').hide();
            $('#anonmeetingjoin').show();
        });

        $('.ivr').click(function () {
            $('.ivrsample').show();
            $(".login-options").hide();
            $(".sign-in").hide();
            $('.anon-login').hide();
            $('#anonmeetingjoin').hide();
        });
        
        $('#btnsend').click(sendNotification);
        $('#btnStartAVBridge').click(startIVR);
        
    };
});