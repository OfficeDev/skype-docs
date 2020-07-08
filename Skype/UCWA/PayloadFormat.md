
# Payload Format
Request and response payloads can be specified as either JSON or XML.


 _**Applies to:** Skype for Business 2015_


UCWA 2.0 supports two payload formats for HTTP requests and responses: 

- JSON, with content type application/json
 
- XML, with content type application/xml
 
Note that the more generic content types are used by popular convention. Clients that wish to be more precise can use the following:

- JSON: application/vnd.microsoft.com.ucwa+json 
 
- XML: application/vnd.microsoft.com.ucwa+xml
 
The body of a request or response often contains a resource, which can have properties, links to other resources, or embedded resources. For more information, see [Resources in UCWA](ResourcesInUCWA.md).

## JSON representation
<a name="sectionSection0"> </a>


```
{
 rel : 'resource',
 prop1 : 1,
 _links : {
 self : {href : 'http://ucwa.lync.com/'}
 },
 _embedded : {
 otherResource : {
 rel : 'otherResource',
 _links : {
 self : {href : 'http://ucwa.lync.com/otherResource/1'}
 }
 }
 }
}

```


### Resource format

A resource can have a property bag that contains a set of name-value pairs. Property names are strings; values can be strings, integers, dates, or arrays of strings, integers, or dates. Dates appear in ASP.NET AJAX format. For more information, see [ASP .NET AJAX: Inside JSON date and time string](https://msdn.microsoft.com/en-us/library/bb299886.aspx).

In a JSON representation, a resource is a JSON object with properties.

Each resource type is identified by its "rel" name. In the JSON representation, the name of the resource is always available in a special property named "rel" (short for relation type - see the [Web Linking RFT - 5988](http://tools.ietf.org/html/rfc5988).


### Links format

A link is a pointer to the URL of another, related, resource. A link has a "rel" (relationship) value, which specifies the type of the resource the link is pointing to, and an "href" (hypertext reference) value, which contains the relative URL of the target resource. A link can also have an optional "title" value, which holds a human-readable name of the target resource.

In the JSON representation, links are stored in a property named "_links"; the value of the property is an object, with the link "rel" as a key and an object of other properties (href, title) as a value. If there can be more than one link with a certain "rel", the property value will instead be an array of objects. It will be an array even if it currently contains only one such link, but it can potentially have more links. This way the client application can expect a certain "rel" to always be a single link or always an array, without having to handle both cases. The API reference gives information about each link that indicates whether it is a single link or an array.

Each resource returned by UCWA 2.0 always has a special link with rel="self", the "self link" that points to the URL of the resource. The "self" link associates a URL with each resource, even if it is embedded in another resource or arrives in the event channel. Note that in some rare cases a resource cannot be retrieved with a GET operation to that URL (the API reference will point that out). Either way the URL is a unique identifier for each resource.


### Embedded resource format

A resource can not only point to other resources with links, but it can include the complete contents of the target resource. This is called an embedded resource. UCWA 2.0 uses embedded resources in situations where the client application is highly likely to use the content immediately.

In JSON, embedded resources are stored in an object in a property named "_embedded". Just like links, the property name for the object is the "rel" name of the embedded resource. The value is either an object containing the entire resource (if there can be only one embedded resource with that name), or an array of objects (if there can be multiple embedded resources).

There can be as many levels of embedding as needed; however, in most cases UCWA 2.0 usually embeds resources only one or two levels deep.

> [!NOTE] 
> Changes in embedded resources do not affect the computation of the ETag for the containing resource, as they are technically separate resources, not part of the containing resource.


## XML representation
<a name="sectionSection1"> </a>


```XML
<resource rel="resource" href="http://ucwa.lync.com/resource/1/">
 <property name="prop1">1</property>
 <resource rel="otherResource" href="http://ucwa.lync.com/otherResource/1">
 </resource>
</resource>

```

The XML representation used by UCWA 2.0 resources takes advantage of XML attributes. Because of this, there is not a 
straightforward mapping between XML and JSON representations. The most significant differences are as follows:


- Dates in XML are supplied in ISO 8601 format. 
- A resource is enclosed in a `<resource xmlns="..." rel="..." href="...">` element, where "rel" and "href" form the self link to the resource.
 
- Links are represented by a `<link rel="..." href="..." [ title = "..."] />` element, child of `<resource>`
. There is no explicit element for a self link because it is part of the enclosing `<resource>` element. In case of multiple
 links with the same "rel" value, the `<link>` element is repeated multiple times. Unlike in JSON, there is no way to 
 distinguish a single link from a one-element collection of links.
 
- Properties are represented by a `<property name="..."> [value]</property>` element. Array-valued properties are 
represented by a `<propertyList name="...">` element, containing `<item> [value]</item>` elements for each array member 
in order.
 
- Embedded resources are represented by a `<resource rel="..." href="...">` element inside the containing `<resource>` element.
 
## Input formats
<a name="sectionSection2"> </a>

UCWA 2.0 accepts input for POST or PUT methods in the XML and JSON formats already listed. However, the manipulation of links 
and embedded resources in POST or PUT inputs is not supported. UCWA 2.0 will ignore them, so it is safe to send a GET request on a 
resource, change the necessary properties and then send the entire contents back to the server in a PUT request (including any 
links and embedded resources from the GET response).


## Data URI
<a name="sectionSection3"> </a>

Some UCWA 2.0 resources include content types that do not follow the JSON or XML resource. One example is an instant message, where the content can be a text/plain or text/html block. This type of resource is represented as a link, where the "href" is not an HTTP URL but a Data URI that contains the encoded non-UCWA 2.0 content.

A Data URI is a special type of URI that encodes the actual resource content, instead of just pointing to it. It looks like this:




```
data:text/plain,Hello+World
```

The Data URI syntax is defined in [RFC 2397](http://tools.ietf.org/html/rfc2397). UCWA 2.0 always uses URL encoding in data URIs.


## Required and optional parameters
<a name="sectionSection4"> </a>

Not all parameters are required in POST inputs. For each resource, the API reference provides details about the parameters that are required and other restrictions on their contents.

If a required parameter is missing, UCWA 2.0 will reject the request with HTTP code 400 / Bad Request, UCWA error subcode "ParameterValidationFailure", together with a list of missing required parameters.

For PUT requests, it is very important for the client application to include all properties, even those it does not intend to change. It is also suggested that a client application send a GET request first, in case the resource has changed since the last fetch. The PUT HTTP method literally means "replace the server's copy of the resource with the one provided in this request." If a new property is added in a new UCWA 2.0 release, an older application might accidentally erase the property if it does not include all properties in the request.

To ensure that applications include unknown properties, all resources that support the PUT method include a property with a randomly-generated name and a string value of "please pass me in a PUT request". UCWA 2.0 will check for the presence of this property in each PUT request. If it is missing, the request will be rejected with a validation error.

Updating a resource property is a read-modify-write operation with GET and PUT. If two applications are updating the same property at the same time they can overwrite each other's changes. To avoid this potential race condition, UCWA 2.0 requires that all PUT requests be conditional (with the If-Match: "{ETag value}" HTTP header). Requests without the header will be rejected with HTTP code 428 / Precondition Required. If the ETag does not match the server version, the request will be rejected with HTTP code 412 / Precondition Failed.

