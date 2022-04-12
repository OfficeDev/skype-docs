---
title: Update an online meeting for Skype for Business 2015
description: The steps here assume that you have already created an application and have received a response that contains the href for an application resource.
---

# Update an online meeting
Update the details of a "meet now" online meeting.


 _**Applies to:** Skype for Business 2015_

To schedule a meeting, see [Schedule an online meeting](ScheduleAnOnlineMeeting.md).

The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a PUT request on the myOnlineMeetings resource.
 
 A PUT request is used to update an existing online meeting. In contrast, a POST request is used to create a new online meeting. A sample request is shown here. Note that the etag is supplied in the If-Match header. If the meeting has changed on the server, this will trigger an error. The client should re-fetch the meeting and alert the user that the meeting has changed so she can resolve the changes.
 
```
PUT https://lyncweb.contoso.com/ucwa/oauth/v1/applications/103985322388/onlineMeetings/myOnlineMeetings/PYC58WSY HTTP/1.1
Accept: application/json
Content-Type: application/json
Authorization: Bearer cwt=AAEB...buHc
If-Match: "3109609770"
X-Ms-Origin: http://localhost
X-Requested-With: XMLHttpRequest
Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
Accept-Language: en-US
Accept-Encoding: gzip, deflate
User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; InfoPath.3)
Host: lyncweb.contoso.com
Content-Length: 947
DNT: 1
Connection: Keep-Alive
Cache-Control: no-cache

{
"accessLevel":"SameEnterprise", 
"entryExitAnnouncement":"Disabled",
"attendees": ["sip:Chris@contoso.com","sip:Alex@contoso.com"],
"automaticLeaderAssignment":"Disabled",
"description":"Weekly meeting",
"expirationTime":"2013-02-07T01:17:18.000Z",
"leaders": [],
"onlineMeetingId":"PYC58WSY",
"onlineMeetingUri":"sip:Dana@contoso.com;gruu;opaque=app:conf:focus:id:PYC58WSY",
"onlineMeetingRel":"myOnlineMeetings",
"organizerUri":"sip:Dana@contoso.com",
"phoneUserAdmission":"Disabled",
"lobbyBypassForPhoneUsers":"Disabled",
"subject":"Important details",
"joinUrl":"https://meet.contoso.com/danab/PYC58WSY",
"56de7bbf-1081-43e6-bbf2-1cabf3224c83":"please pass this in a PUT request",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/103985322388/onlineMeetings/myOnlineMeetings/PYC58WSY"},
"onlineMeetingExtensions":{"href":"/ucwa/oauth/v1/applications/103985322388/onlineMeetings/myOnlineMeetings/PYC58WSY/extensions"}
},
"rel":"myOnlineMeeting",
"etag":"3109609770",
"attendanceAnnouncementsStatus":"Disabled"
}
```

2. Process the response from the previous PUT request.
 
 The response you receive should be 200 OK. The body of the response contains a series of key-value pairs for the updated meeting. As was stated before, a 4xx response indicates that the meeting has changed on the server. The client should re-fetch the meeting and alert the user that the meeting has changed so she can resolve the changes.
 
```
HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: 905
Date: Thu, 17 Jan 2013 00:00:00 GMT
Content-Type: application/json; charset=utf-8
ETag: "346579618"
Server: Microsoft-IIS/7.5
Cache-Control: no-cache
X-AspNet-Version: 4.0.30319
X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
X-Powered-By: ASP.NET

{
"accessLevel":"SameEnterprise",
"entryExitAnnouncement":"Disabled",
"attendees": ["sip:Chris@contoso.com", "sip:Alex@contoso.com"],
"automaticLeaderAssignment":"Disabled",
"description":"Weekly meeting",
"expirationTime":"\/Date(1360199838000)\/",
"leaders": [],
"onlineMeetingId":"PYC58WSY",
"onlineMeetingUri":"sip:Dana@contoso.com;gruu;opaque=app:conf:focus:id:PYC58WSY",
"onlineMeetingRel":"myOnlineMeetings",
"organizerUri":"sip:Dana@contoso.com",
"phoneUserAdmission":"Disabled",
"lobbyBypassForPhoneUsers":"Disabled",
"subject":"Important details",
"joinUrl":"https://meet.contoso.com/danab/PYC58WSY",
"56de7bbf-1081-43e6-bbf2-1cabf3224c83":"please pass this in a PUT request",
"_links":{
"self":{"href":"/ucwa/oauth/v1/applications/103985322388/onlineMeetings/myOnlineMeetings/PYC58WSY"},
"onlineMeetingExtensions":{"href":"/ucwa/oauth/v1/applications/103985322388/onlineMeetings/myOnlineMeetings/PYC58WSY/extensions"}
},
"rel":"myOnlineMeeting",
"etag":"346579618"
}
```

