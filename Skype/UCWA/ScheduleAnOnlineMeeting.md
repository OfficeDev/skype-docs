---
title: Schedule an online meeting for Skype for Business 2015
description: Scheduling a meeting involves resource navigation from application to myOnlineMeetings.
---

# Schedule an online meeting
Schedule a "meet now" online meeting.


 _**Applies to:** Skype for Business 2015_

Scheduling a meeting involves resource navigation from [application](application_ref.md) to [myOnlineMeetings](myOnlineMeetings_ref.md). 

The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a POST request on the **myOnlineMeetings** resource.
 
 A sample request is shown here. The data include meeting-specific information such as subject and attendees.
 
```
POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/103...740/onlineMeetings/myOnlineMeetings HTTP/1.1
Accept: application/json
Content-Type: application/json
Authorization: Bearer cwt=AAEB...buHc
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
Accept-Language: en-US
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; InfoPath.3)
Host: lyncweb.contoso.com
Content-Length: 185
DNT: 1
Connection: Keep-Alive
Cache-Control: no-cache

{
"attendanceAnnouncementsStatus":"Disabled",
"description":"hey guys let's do a musical!",
"subject":"holiday party",
"attendees": ["sip:Chris@contoso.com","sip:Alex@contoso.com"],
"leaders": []
}
```

2. Process the response from the previous POST request.
 
 The response you receive should be 200 OK. The body of the response contains meeting settings such as subject, description, invitees and organizer, and meeting ID.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 926
Date: Thu, 14 Feb 2013 20:42:23 GMT
Content-Type: application/json; charset=utf-8
ETag: "891...351"
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"accessLevel":"SameEnterprise",
"entryExitAnnouncement":"Disabled",
"attendees": ["sip:Chris@contoso.com","sip:Alex@contoso.com"],
"automaticLeaderAssignment":"Disabled",
"description":"hey guys let's do a musical!",
"expirationTime":"\/Date(136...000)\/",
"leaders": [],
"onlineMeetingId":"DED...367",
"onlineMeetingUri":"sip:Dana@contoso.com;gruu;opaque=app:conf:focus:id:DED...367",
"onlineMeetingRel":"myOnlineMeetings",
"organizerUri":"sip:Dana@contoso.com",
"phoneUserAdmission":"Disabled",
"lobbyBypassForPhoneUsers":"Disabled",
"subject":"holiday party",
"joinUrl":"https://meet.contoso.com/dana/DED...367","56de...4c83":"please pass this in a PUT request",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/103...740/onlineMeetings/myOnlineMeetings/DEDX9367"},
"onlineMeetingExtensions":{"href":"/ucwa/oauth/v1/applications/103...740/onlineMeetings/myOnlineMeetings/DED...367/extensions"}
},
"rel":"myOnlineMeeting",
"etag":"891...351"
}
```

