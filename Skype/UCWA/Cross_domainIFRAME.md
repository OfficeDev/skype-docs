
# Cross-domain IFRAME
Learn about how cross-domain **iframe** can be used to safely circumvent browser restrictions on scripts that process code in a different domain.


 _**Applies to:** Skype for Business 2015_

Web applications that interact with UCWA 2.0 resources require a cross-domain **iframe** for all HTTP requests sent to UCWA 2.0. The cross-domain **iframe** is needed to securely bypass the same-origin policy that is enforced by most modern browsers.

## Embedding the cross-domain frame
<a name="sectionSection0"> </a>

The cross-domain **iframe** must be embedded in the parent HTML document as shown in this example.


```XML
<!doctype html> 
<html lang="en"> 
 <head> </head> 
 <body> 
 <iframe id="xFrame" src="about:blank" style="display:none;"></iframe> 
 </body> 
</html> 

```


## postMessage API
<a name="sectionSection1"> </a>

The HTML 5 **postMessage** function is used to send HTTP requests to the **iframe**, and to send HTTP responses back to the source document. The **postMessage** input and output formats are described next.


### Request format

The **postMessage** request to the cross-domain frame accepts a JSON string with the following key-value pairs that map closely to those of jQuery.


```
{ 
 accepts: [string containing the accept header of the HTTP request], 
 cache: [Boolean indicating whether browser should cache HTTP response], 
 contentType: [string containing the content-type header of the HTTP request], 
 data: [string containing HTTP body of request], 
 headers: [object containing custom HTTP request headers], 
 processData: [Boolean indicating whether input data should be passed as a query string], 
 type: [string containing the HTTP verb], 
 url: [string containing target URL for the request], 
 xhrFields: [object containing name-value pairs to set on the native xhr object], 
 messageId: [object containing any needed context to identify the request] 
}
```


### Response format

The response that is sent to the source frame using **postMessage** contains most standard XHR object keys.


```
{ 
 status: [integer containing the HTTP response code], 
 statusText: [string containing the HTTP response text], 
 responseText: [string containing the HTTP response body] 
 headers: [string containing response headers], 
 messageId: [object containing any needed context to identify the request] 
}
```

The following code sample shows **postMessage** being called to send a GET request.




```
var request = { 
 accepts: 'application/json', 
 type: 'get', 
 url: 'https://lyncdiscover.sipdomain.com/', 
 xhrFields: { 
 withCredentials: true 
 }, 
 messageId: { 
 Id: Id, 
 callback: callback, 
 } 
}; 
document.getElementById('xFrame').contentWindow.postMessage(JSON.stringify({request}), url);
```

The next example shows code that will handle the response.




```
this.receiveMessage = function(message) { 
 // handle response from iframe origin 
}; 
window.addEventListener('message', this.receiveMessage, false)
```


## Web application allow list
<a name="sectionSection2"> </a>

Web applications that take a dependency on the cross-domain **iframe** are required to get IT Admin approval for their domain. Administrators will add the source domain of your web application to the company's list of allowed domains.


## Browser compatibility
<a name="sectionSection3"> </a>

Although Internet Explorer 8 has an implementation of the HTML 5 **postMessage** function, it is not compatible with the cross-domain frame that UCWA 2.0 uses. Internet Explorer 9 and Internet Explorer 10 are both supported, as are the latest versions of Chrome, Safari, and Firefox. For more details on browser compatibility, see [Supported Platforms for Lync Web App for Lync 2013](https://technet.microsoft.com/en-us/library/gg425820.aspx).

