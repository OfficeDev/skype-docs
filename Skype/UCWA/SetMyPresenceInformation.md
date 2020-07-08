
# Set my presence information
Set the presence of the **me** user.


 _**Applies to:** Skype for Business 2015_

Setting a user's presence involves resource navigation from [application](application_ref.md) to [me](me_ref.md) to [presence](presence_ref.md). 

The steps here assume that you have already created an application and have received a response that contains the href for an [application](application_ref.md) resource. For more information, see [Create an application](CreateAnApplication.md).

1. Send a POST request on the **makeMeAvailable** resource.
 
 One of the hypermedia links that are served in the response for the [application](application_ref.md) resource is the href for the [makeMeAvailable](makeMeAvailable_ref.md) resource. Search for the [me](me_ref.md) embedded resource, and then locate the href of the **makeMeAvailable** link.
 
    ```
    POST https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101/me/makeMeAvailable HTTP/1.1
    Accept: application/json
    Content-Type: application/json
    Authorization: Bearer cwt=AAEB...buHc
    X-Ms-Origin: https://app.contoso.com
    X-Requested-With: XMLHttpRequest
    Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
    Accept-Language: en-us
    Accept-Encoding: gzip, deflate
    User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
    Host: lyncweb.contoso.com
    Content-Length: 37
    Connection: Keep-Alive
    Cache-Control: no-cache

    {"SupportedModalities": ["Messaging"]}
    ```

2. Process the response from the request in the previous step.
 
 The response you receive should be 204 No content, as shown here.
 
    ```
    HTTP/1.1 204 No Content
    Connection: Keep-Alive
    Date: Thu, 17 Jan 2013 00:00:00 GMT
    Server: Microsoft-IIS/7.5
    Cache-Control: no-cache
    X-AspNet-Version: 4.0.30319
    X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
    X-Powered-By: ASP.NET
    ```

3. Send a GET request on the **application** resource.
 
 A sample request is shown here.
 
    ```
    GET https://lyncweb.contoso.com/ucwa/oauth/v1/applications/101 HTTP/1.1
    Authorization: Bearer cwt=AAEB...buHc
    Accept: application/json
    X-Ms-Origin: https://app.contoso.com
    X-Requested-With: XMLHttpRequest
    Referer: https://lyncweb.contoso.com/Autodiscover/XFrame/XFrame.html
    Accept-Language: en-us
    Accept-Encoding: gzip, deflate
    User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)
    Host: lyncweb.contoso.com
    Connection: Keep-Alive
    ```

4. Process the response from the previous request.
 
 You should receive a response code of 200 OK. The following is a typical response to the previous GET request.
 
 Some links in the response will be used in subsequent steps, so it is important to cache portions of the response body. The link that will be used later is that for **presence**, which is a link in the **me** embedded resource.
 
 For brevity, some parts of the response body are omitted, and IDs and tokens are shortened.
 
    ```
    HTTP/1.1 200 OK
    Connection: Keep-Alive
    Content-Length: 3754
    Date: Thu, 17 Jan 2013 00:00:00 GMT
    Content-Type: application/json; charset=utf-8
    Server: Microsoft-IIS/7.5
    Cache-Control: no-cache
    P3P: CP="IDC CUR ADMa OUR BUS"
    X-AspNet-Version: 4.0.30319
    Set-Cookie: cwt_ucwa=AAEB...dG9z; path=/ucwa/oauth/v1/applications/104/photos; secure; HttpOnly
    X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
    X-Powered-By: ASP.NET

    {
    "culture":"en-US",
    "userAgent":"UCWA Samples","
    _links":{
    "self":{"href":"/ucwa/oauth/v1/applications/104"},
    "policies":{"href":"/ucwa/oauth/v1/applications/104/policies"},
    "batch":{"href":"/ucwa/oauth/v1/applications/104/batch"},
    "events":{"href":"/ucwa/oauth/v1/applications/104/events?ack=1"}
    },
    "_embedded":{
    "me":{
    "name":"Dana Birkby",
    "uri":"sip:Dana@contoso.com",
    "emailAddresses": ["Dana@contoso.com"],
    "_links":{
    "self":{"href":"/ucwa/oauth/v1/applications/104/me"},
    "note":{"href":"/ucwa/oauth/v1/applications/104/me/note"},
    "presence":{"href":"/ucwa/oauth/v1/applications/104/me/presence"},
    "location":{"href":"/ucwa/oauth/v1/applications/104/me/location"},
    "reportMyActivity":{"href":"/ucwa/oauth/v1/applications/104/me/reportMyActivity"},
    "callForwardingSettings":{"href":"/ucwa/oauth/v1/applications/104/me/callForwardingSettings"},
    "phones":{"href":"/ucwa/oauth/v1/applications/104/me/phones"},
    "photo":{"href":"/ucwa/oauth/v1/applications/104/photos/Dana@contoso.com"}
    },
    "rel":"me"
    },
    "people":{...},
    "onlineMeetings":{...},
    "communication":{...},
    }
    "rel":"application"
    }
    ```

5. Send a POST request on the **presence** resource.
 
 A sample request is shown here. Note the **availability** property at the bottom of the request.
 
    ```
    POST https://contoso.com/ucwa/oauth/v1/applications/102/me/presence HTTP/1.1
    Accept: application/json
    Content-Type: application/json
    Authorization: Bearer cwt=AAEB...uHc
    X-Ms-Origin: http://myServer
    X-Requested-With: XMLHttpRequest
    Referer: https://contoso.com/Autodiscover/XFrame/XFrame.html
    Accept-Language: en-US
    Accept-Encoding: gzip, deflate
    User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; InfoPath.3)
    Host: lyncweb.contoso.com
    Content-Length: 25
    DNT: 1
    Connection: Keep-Alive
    Cache-Control: no-cache
    {"availability":"Online"}
    ```

6. Process the response from the previous POST request.
 
 The response you receive should be 204 No Content. This means that your presence request has been received by the server. An event will confirm that your application is the most active and this presence is now being shared with other contacts.
 
    ```
    HTTP/1.1 204 No Content
    Connection: Keep-Alive
    Date: Thu, 17 Jan 2013 00:00:00 GMT
    Server: Microsoft-IIS/7.5
    Cache-Control: no-cache
    X-AspNet-Version: 4.0.30319
    X-MS-Server-Fqdn: W15-LYNC-SE1.contoso.com
    X-Powered-By: ASP.NET

    ```

