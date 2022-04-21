---
title: Transport library
description: Transport.js is a JavaScript library that helps make HTTP requests and assists with cross-domain communication.
---
# Transport library
Transport.js is a JavaScript library that helps make HTTP requests and assists with cross-domain communication.


 _**Applies to:** Skype for Business 2015_

Use the functions in the Transport library to issue HTTP requests to a remote location using iframes and HTML 5's **postMessage** function. It also handles cases where the domain changes by injecting a new iframe into a container element, if necessary.

## Create a Transport object
<a name="sectionSection0"> </a>

Before the **Transport** constructor is called, its _targetOrigin_parameter must be set to the origin of the Web page to which messages are sent.


```
var domain = "https://www.example.com",
targetOrigin = "https://www.myDomain.com",
element = $("#frame") [0].contentWindow,
Transport = new microsoft.rtc.ucwa.samples.Transport(targetOrigin);
```

The variables declared in the preceding example are used in subsequent examples in this topic.


## clientRequest(request)
<a name="sectionSection1"> </a>

Uses the HTML 5 **postMessage** function to send a Request object to a remote location.

|**Parameter**|**Description**|
|:-----|:-----|
|request|Object containing request data.|

 **Syntax**

```
clientRequest(request )
```

 **Example**

In the following example, the request that is passed as a parameter in the **clientRequest** function consists of the URL, the request type, some data, the acceptType and contentType, and the name of a callback.




```
var data = {
 message: "Hello World",
 sender: "me"
};

Transport.clientRequest({
 url: "/ucwa/mylink",
 type: "post",
 data: data,
 acceptType: "application/json",
 contentType: "application/json",
 callback: handleSingleResponse
});

function handleSingleResponse(data) {
 if (data.status === 200 || data.status === 204 || data.statusText === "success") {
 // Probably a good request to handle...
 if (data.results !=== undefined) {
 // JSON data exists...
 }
 }
}
```


### Remarks

The request parameter should be an object in the form of:


```
{
 url: "myLink" (HTTP request URL),
 verb: "get" (get, post, put, delete),
 acceptType: "application/json" (default, optional),
 contentType: "application/json" (default, optional),
 data: "hello world" (any kind of JSON data),
 callback: (optional),
 notifyAction: true/false (optional)
}
```

The **clientRequest** function checks whether an internal element and domain are set; otherwise no remote communications are possible. It generates a UUID and attaches it to the Request object, as it will be used to identify the response data to the correct callback.

If _notifyAction_is set to false, **clientRequest** will not execute start or stop callbacks, if any, that were previously set when **setRequestCallbacks** was called.

Finally, it uses the internal element to post a message by transforming the Request object into request data.


## getDomain()
<a name="sectionSection2"> </a>

The **getDomain** function gets the domain that Transport is currently sending requests to.

 **Returns**: String representing the domain.

 **Syntax**




```
getDomain()
```

 **Example**

The following example uses the **getDomain** function to form the absolute URL for a contact's photo.




```
function processSingleContact(contactData) {
 var contact = {
 name: contactData.name,
 email: contactData.emailAddresses ? contactData.emailAddresses [0] : "(None)",
 image: contactData._links.contactPhoto ? ucwa.Transport.getDomain() + contactData._links.contactPhoto.href : null
 };
 return contact;
}
```


## injectFrame(xframe, container, callback)
<a name="sectionSection3"> </a>

The **injectFrame** function injects an iframe that is located on the domain into the supplied container.





|**Parameter**|**Description**|
|:-----|:-----|
|xframe|Absolute URL to the iframe's target.|
|container|DOM element that will contain the iframe.|
|callback|Callback to execute when the iframe is loaded into the DOM.|

**Syntax**

```
injectFrame(xframe , container , callback )
```

 **Example**

In the following example, the caller of the _startAutoDiscover_function shown here would supply values for the _domain_(such as contoso.com) and _container_(such as an DOM element on the page into which a cross-domain frame is injected), and the prefix (such as "https://lyncdiscoverinternal." or "https://lyncdiscover.").

The _handleFrameLoad_callback is not shown.




```
// The URL to use to start AutoDiscovery.
_discoveryLocation = null,
// The fully-qualified domain name (FQDN) derived from the user's sign-in address.
_domain = null,
// DOM element where Transport should inject the cross-domain frame.
_container = null;

/// The first location to check is: "https://lyncdiscover." + domain
/// Use the Transport object to insert a new iframe at the discovery location and
/// supply a callback to test it after load.

function startAutoDiscover(domain, container, prefix) {
 _domain = domain;
 _container = container;
 _discoveryLocation = prefix + _domain;
 var frameLoc = _discoveryLocation + "/xframe";
 transport. injectFrame(frameLoc, _container, handleFrameLoad);
}

```


### Remarks

After the iframe is created, an event handler is set up to run after the function loads. This event handler sets the element and domain, and executes the supplied callback, if the callback is defined.


## setAuthorization(accessToken, tokenType)
<a name="sectionSection4"> </a>


The **setAuthorization** function sets the authorization credentials to be used in requests.




|**Parameter**|**Description**|
|:-----|:-----|
|accessToken|Unique identifier.|
|tokenType|Type of access token.|

**Syntax**

```
setAuthorization(accessToken , tokenType )
```

 **Example**




```
Transport.setAuthorization("cwt=AAEBHAEFAAAAAAAFFQAAACZfw6hMpZ-w7RAMgdAEAACBEPDttJWHCThQn95OJLXgmL2CAvrFgyAMij1C0fFgd9JBx2_VpSjC0fUJFOK02BUWty33xAQH34YIAieH80cSzwg", "Bearer");
```


## setElement(element, xframe)
<a name="sectionSection5"> </a>

The **setElement** function sets the DOM element and domain to be used for requests.


|**Parameter**|**Description**|
|:-----|:-----|
|element|Element that will receive requests.|
|xframe|Absolute URL of the iframe's target.|

**Syntax**




```
setElement(element , xframe )
```

 **Example**




```
var domain = "https://www.example.com",
element = $("#frame") [0].contentWindow,

Transport.setElement(element, domain);

```


### Remarks

After the element and **xframe** are set, the Transport library can attempt to make HTTP requests using **postMessage** on the element.


## setRequestCallbacks(callbacks)
<a name="sectionSection6"> </a>

The **setRequestCallbacks** function sets the request callbacks that are to be executed when requests are started and stopped.


|**Parameter**|**Description**|
|:-----|:-----|
|callbacks|Object containing callbacks.|

The callbacks parameter should be an object in the form of:


```
{
 start: (optional),
 stop: (optional)
}
```

 **Syntax**




```
setRequestCallbacks(callbacks )
```

 **Example**

In this example, two callbacks are set: one that will run when an AJAX call starts, and another that will run at the completion of this call.




```
function beginDiscovery(domain) {
 site.ucwa.Transport.setRequestCallbacks({
 start: handleAjaxStart,
 stop: handleAjaxStop
 });
 site.ucwa.AutoDiscovery.startDiscovery(domain, $("#container"), handleAutoDiscovery);
}
```

