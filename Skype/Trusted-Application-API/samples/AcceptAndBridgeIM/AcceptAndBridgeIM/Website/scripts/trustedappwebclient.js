$(function () {
    'user strict';
        
    var serviceurl = window.location.origin; //UCAP cloud application URI
    
    var listeningjoburl = serviceurl + '/ListeningJob';
    var getanontokenurl = serviceurl + '/GetAnonToken';
    var getadhocmeetingurl = serviceurl + '/GetAdhocMeetingJob';
    var notifyurl = serviceurl + '/SimpleNotifyJob';
    var imbridgeurl = serviceurl + '/StartIMBridge';
    var avbridgeurl = serviceurl + '/IncomingAudioVedioBridgeJob';
    var ivrurl = serviceurl + "/AudioVideoIVRJob";
    var agentSip = "sip:liben@Metio.onmicrosoft.com";

    var anontokeninput = {
        ApplicationSessionId: guid(),
        AllowedOrigins: window.location.href
    };

    var getadhocmeetinginput = {
        Subject: 'adhocMeeting',
        Description: 'adhocMeeting',
        AccessLevel: ''
    };
    
    var imbridgeinput = {
        InviteTargetUri: "sip:toshm@metio.onmicrosoft.com",
        WelcomeMessage: "Welcome!",
        IsStart: true,
        Subject: "HelpDesk",
        InvitedTargetDisplayName: 'Agent',
        EnableMessageFilter:false
    };

    var notifydata = {
        TargetUri: 'sip:toshm@metio.onmicrosoft.com',
        NotificationMessage: 'I love the API.'
    }

    var ajaxrequest = function (verb, url, data, datatype) {
        var request = $.ajax({
            url: url,
            type: verb,
            dataType: datatype,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            async: true
        });
        return request;
    }

    function guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
    };

    function deleteexistedjob() {
        return ajaxrequest('delete', listeningjoburl);
    }

    function getadhocmeeting() {
        var tokens = {};
        return ajaxrequest('post', getadhocmeetingurl, getadhocmeetinginput, 'text');
    }

    function getanontoken(meetingurl) {

        if (meetingurl) {
            anontokeninput.MeetingUrl = meetingurl;
        }

        return ajaxrequest('post', getanontokenurl, anontokeninput, 'text');
    }

    function notification(sip, msg) {
        if (sip) {
            notifydata.TargetUri = sip;
        }
        if (msg) {
            notifydata.NotificationMessage = msg;
        }

        return ajaxrequest('post', notifyurl, notifydata, 'text');
    }

    function startimbridge(sip, isstop) {
        deleteexistedjob().always(function () {
            if (sip) {
                imbridgeinput.InviteTargetUri = sip;
            }

            if (isstop) {
                imbridgeinput.IsStart = true;
            }

            ajaxrequest('post', imbridgeurl, imbridgeinput, 'json').done(function (data) {
                console.log('started message invitation handler');
            });
        });
    }
    
    //Server require Json format in this job
    function startivr(sip) {


    }

    //Todo
    function starthuntgroup() {

    }
    
    var client = function () {
        return {
            deletjob:deleteexistedjob,
            getadhocmeeting: getadhocmeeting,
            getanontoken: getanontoken,
            notification: notification,
            startimbridge: startimbridge,
            startivr: startivr,
            starthuntgroup: starthuntgroup
        }
    }

    window['trustedappwebclient'] = client;
})