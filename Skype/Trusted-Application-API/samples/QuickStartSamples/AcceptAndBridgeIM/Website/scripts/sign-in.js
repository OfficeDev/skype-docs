//sign in sample:
//if user has signed in give prompt, otherwise go to index page
$(function () {
    'use strict';
    window['sign-in_load'] = function () {

        if (window.skypeWebApp && window.skypeWebApp.signInManager.state() === "SignedIn") {
            $('.wrappingdiv .signed-in').show();
            $('.anonlogic').show();
            return;
        }

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
                $('#signedinstatus').show();
            }, function (error) {
                // if something goes wrong in either of the steps above,
                // display the error message
                $(".modal").hide();
                alert("Can't sign in, please check the user name and password.");
                console.log(error || 'Cannot sign in');
            });
        }



        //Imbridge sign in with token
        $('#btn-anon-sign-in').click(function () {
            
            if (options.token || options.root) {
                $(".modal").show();
                signin(options);
                $('.anonlogic').show();
                window.scrollTo(0, document.body.scrollHeight);
                //start imbridge job .
                trustedappclient.startimbridge();
            }
            else {
                alert('Please get token first.');
            }
        });

        $('#btn-startchat').click(function () {
            window['chat-service_load']();
        });

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
        };


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
        

        function renderParameters(data) {
            if (data.AnonymousToken || data.DiscoverUri) {
                $('#discoveryuri').text('DiscoveryUri is used for routing: ' + data.DiscoverUri);
                $('#anonymousetoken').text('AnonymousToken is used for authentication: ' + data.Token);
                $('#tenantEndpointid').text('TenantEndpointId is the application endpoint registered from platformservice: ' + data.TenantEndpointId);
                $('#anontokenresult').show();
            }
            window.scrollTo(0, document.body.scrollHeight);
        }

    };
});