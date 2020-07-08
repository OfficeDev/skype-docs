
# Cross-domain communication
Learn how to enable cross-domain communication for web applications.


 _**Applies to:** Skype for Business 2015_

Browser-based web applications that are not loaded from the same domain as the UCWA 2.0 service are restricted from interacting with the API due to the same-origin policy of most browsers (see [RFC 6454 - The Web Origin Concept](http://tools.ietf.org/html/rfc6454)). To enable cross-domain communication for these web applications, UCWA 2.0 exposes an HTML iframe that allows web applications to securely interact with UCWA 2.0 resources that are located on another domain. 

The cross-domain iframe communicates with the web application by way of the [HTML5 postMessage method](https://msdn.microsoft.com/en-us/library/windows/apps/hh441295.aspx). The iframe should be embedded in the HTML document of the corresponding web application. All HTTP requests made to UCWA 2.0 should be sent using postMessage to the cross-domain iframe, which will then send the request on the wire. The cross-domain iframe will receive the response and send it to the parent source document using postMessage.
