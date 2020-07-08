# addToPresenceSubscription

 _**Applies to:** Skype for Business 2015_


Represents the capability to add contacts to a [presenceSubscription](presenceSubscription_ref.md).
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This resource is used to add specified contacts to the presence subscription.The operation takes an array of contact URIs as input.

### Properties



None

### Links



None

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|

## Operations



<a name="sectionSection2"></a>

### POST




Add contacts to a [presenceSubscription](presenceSubscription_ref.md).

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|contactUris|The URIs of the users whose presence is to be subscribed to.Array of String|Yes|

#### Response body



|**Item**|**Description**|
|:-----|:-----|
|[presenceSubscriptionMemberships](PresenceSubscriptionMemberships_ref.md)|A collection of [presenceSubscriptionMembership](presenceSubscriptionMembership_ref.md) resources.|

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/presenceSubscription/addToPresenceSubscription HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Accept: application/json
Content-Length: 77
{
  &quot;contactUris&quot; : [
    &quot;\&quot;sip : user2@microsoft.com\&quot;&quot;,
    &quot;\&quot;sip : user3@microsoft.com\&quot;&quot;
  ]
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/json
Content-Length: 473
{
  "rel" : "presenceSubscriptionMemberships",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/presenceSubscriptionMemberships"
    }
  },
  "_embedded" : {
    "presenceSubscriptionMembership" : [
      {
        "rel" : "presenceSubscriptionMembership",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/presenceSubscriptionMemberships/ads-bes2asd,john@contoso.com"
          },
          "contact" : {
            "href" : "/ucwa/v1/applications/192/people/282"
          },
          "presenceSubscription" : {
            "href" : "/ucwa/v1/applications/192/presenceSubscription"
          }
        }
      }
    ]
  }
}
```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/presenceSubscription/addToPresenceSubscription HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Accept: application/xml
Content-Length: 231
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;propertyList name=&quot;contactUris&quot;&gt;
    &lt;item&gt;&quot;sip:user2@microsoft.com&quot;&lt;/item&gt;
    &lt;item&gt;&quot;sip:user3@microsoft.com&quot;&lt;/item&gt;
  &lt;/propertyList&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 201 Created
Content-Type: application/xml
Content-Length: 648
<?xml version="1.0" encoding="utf-8"?>
<resource rel="presenceSubscriptionMemberships" href="/ucwa/v1/applications/192/presenceSubscriptionMemberships" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <property name="rel">presenceSubscriptionMemberships</property>
  <resource rel="presenceSubscriptionMembership" href="/ucwa/v1/applications/192/presenceSubscriptionMemberships/ads-bes2asd,john@contoso.com">
    <link rel="contact" href="/ucwa/v1/applications/192/people/282" />
    <link rel="presenceSubscription" href="/ucwa/v1/applications/192/presenceSubscription" />
    <property name="rel">presenceSubscriptionMembership</property>
  </resource>
</resource>
```


