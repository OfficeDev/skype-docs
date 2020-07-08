
# Skype URI tutorial: Web pages
Learn how to incorporate Skype communication functionality into your web pages. 

 _**Applies to:** Skype_

## Use Skype URIs in web pages

Skype provides two ways for using Skype URIs in your web pages:


* [Skype Buttons](#buttons)
* [Skype.ui JavaScript function](SkypeURItutorial_Webpages.md)

Each of these approaches additionally determines whether a Skype client is installed, and takes the appropriate action.

Of course, because using a Skype URI on a web page is really no different than using any other hyperlink, you can always 
just set the value of your anchor tag's *href* attribute to the Skype URI.

For example:

 `<a href="skype:echo123?call">Call</a> the Skype Echo / Sound Test Service`

However, your web page must determine whether a Skype client is installed, and take the appropriate action if it is not.

<a name="buttons"></a>

### Skype Buttons

Skype buttons provide you with a generated block of HTML that has a Skype URI at its core. If all you want to do is have 
people call or chat with you over Skype, then simply use the form to specify the type of button you want, and paste its 
code snippet into your web page. Skype buttons will attempt to determine if a Skype client is installed, and take 
appropriate action as required. 

If you're adding multiple Skype Buttons to your web page:


1. Remove the following line from all but the first instance:

     `<script type="text/javascript" src="http://www.skypeassets.com/i/scom/js/skype-uri.js"></script>`
1. Modify the  **div** element's **id** attribute and the **element** property so that each instance has matching unique values on your web page.

For example:

     `SkypeButton_Call_#echo123_01, SkypeButton_Call_#echo123_02`

<a name="skype.ui"></a>

### Skype.ui JavaScript function

The Skype.ui JavaScript function enables you to dynamically embed Skype URIs that have a predefined appearance and UI 
similar to Skype buttons, but provides additional formatting options and functionality, such as point size, color, 
video, conference calls, and multi chats. **Skype.ui** will attempt to determine if a Skype client is installed, and 
take appropriate action as required.

dThe basic design pattern for adding Skype URIs to your web pages involves:


* Importing the [skype-uri.js](http://www.skypeassets.com/i/scom/js/skype-uri.js) file from **http://www.skypeassets.com/i/scom/js/** into your web page.
* Using a  **div** tag to mark where you want place the Skype URI on your web page.
* Invoking the generator function, Skype.ui, to create and append an anchor element, based on a JSON representation of the Skype object, and append it to your web page.

For example, the following figure shows a code fragment that adds a Skype URI that initiates an audio call to the Skype 
Echo/Sound Test Service:

m
**Figure 1. JavaScript code example**

![Code adding Skype URI to initiate audio call](images/SkypeUriJavaScriptCallouts.png)


> [!NOTE] 
> The JavaScript objects and functions contained in  `skype-uri.js` are covered by the Apache License, 
 Version 2.0, which you can view at [http://www.apache.org/licenses/](http://www.apache.org/licenses/).


.#### Image assets

Embedded Skype URIs use buttons for shifting focus to the Skype client, starting a call, and starting or rejoining a 
chat. The button's associated link includes an **img** tag who's **src** attribute specifies the appropriate image asset.

The current button/image asset relationships are:


* Shift Focus
* Audio or video call
* Chat


Supported sizes (in pixels) are 10, 12, 14, 16, 24, and 32. Supported colors are Skype Blue and white, both on 
transparent backgrounds.

The Skype object defaults to using 16 pixel, Skype Blue images. You can change the size, color, or both by including 
the imageSize and/or imageColor properties. For example, the following code embeds a "call" Skype URI that uses 
the 32 pixel, white image asset:




```html
<div id="call_32" style="width:20%;background-color:#0094ff">
   <script type="text/javascript">
        Skype.ui({
            name: "call",
            element: "call_32",
            participants: ["echo123"],
            imageSize: 32,
            imageColor: "white"
        });
    </script>
</div>

```


#### Embedding the Skype URI

 The **Skype.ui** function generates and embeds a Skype URI link on your web page. The function accepts a single 
 argument—an instance of a Skype object, which you specify using JSON. On success, the function returns **true**. 
 On failure, the function returns  **false**.

The Skype object recognizes the properties listed in Table 1. Unless otherwise stated, the values  **null** and the 
empty string are equivalent to omitting the property. For a handy table detailing the property value combinations 
associated with the various types of Skype URIs, see the Skype.ui quick reference.


**Table 1. Properties that the Skype object recognizes**


|**Property**|**Description**|
|:-----|:-----|
|name|The name of the Skype URI. Currently recognized values are:<br/>* **call** - Place an audio or video call.<br/>* **chat** - Initiate/restart a chat.<br/>* **dropdown** - Dynamically choose whether to place a call or initiate/restart a chat. If omitted, "audio call" is implied if any participants are specified, and "switch focus to the Skype client" is implied if no participants are specified.|
|element|The **id** attribute value of the element that will contain the generated Skype URI link (typically a **div** tag).Always required.|
|participants|An array of one or more Skype Names (or phone numbers) that are the target or targets of the generated Skype URI.Required if name is specified as  **call** or **chat**. There is no predefined limit on the number of participants, but keep in mind that group video calls are currently limited to a maximum of 10 participants. The participants are also optionally formatted as a comma-separated list, immediately following the generated link. See [Skype URI API reference](SkypeURIAPIReference.md) for platform-specific caveats related to multiple participants; for example, mobile Skype clients (iOS and Android) do not support initiating/hosting conference calls.|
|listParticipants|Whether to list the participant Skype Names and/or phone numbers immediately following the generated link. Values are: <br/>* **true** - List the participants.<br/>* **false** (or any value other than **true**) - Do not list the participants. <br/>The default is  **false**. Ignored if there are no participants to list.|
|video|Whether this is a video call. Values are:<br/>* **true** - Video call. <br/>* **false** (or any value other than `true`) - Audio call.<br/>The default is **false**. Ignored if this Skype URI is not an explicit call; that is, **name** is omitted or specified as other than **call** or **dropdown**.|
|topic|The optional topic string for a conference call, group video chat, or multi chat. Special characters in the value—specifically spaces, colons, and slashes—are additionally escaped. For example:<br/> **topic: "Quantum Mechanics 401 (Room: 7; 02/17/2012)"** <br/>is automatically escaped to yield:<br/>**Quantum%20Mechanics%20401%20(Room%3A%207;%2002%2F17%2F2012)** <br/>Ignored if there are fewer than two participants, or if this Skype URI is not an explicit call or chat; that is, name is omitted or specified as other than  **call**,  **chat**, or **dropdown**. Like the participants, the topic string is also optionally included as text immediately following the generated link. |
|listTopic|Whether to list the topic string immediately following the generated link. Values are:<br/>* **true** - List the topic string.<br/>* **false** (or any value other than **true**) - Do not list the topic string.<br/>The default is **false**. Ignored if there is no topic string to list.|
|imageSize|Which size of the image asset to use. Values are:<br/>* **Omitted**, **null**, an empty string, or a non-supported size - Use the default size.<br/>* **999** - Use the specified size, which must be one of the supported sizes: 10, 12, 14, 16, 24, and 32.<br/>The default size is 16 pixels.
|imageColor|Which text color variant of the image asset to use, depending on your web page's background. Values are:<br/>* **omitted**, **null**, an empty string, or a non-supported color - Use the default color.<br/>* **skype** - Use the Skype Blue variant.<br/>* **white** - Use the white variant.<br/>The default color is Skype Blue. |



#### Skype.ui quick reference

 This quick reference details the Skype object property values you need to pass to `Skype.ui` in order to generate the 
 various types of Skype URIs.

Because  **element** is always required (and **listParticipants**,  **listTopic**,  **imageSize** and **imageColor** 
are always optional), they are not included in the table.


**Table 2. Skype object property values**


|**Skype URI**|**name**|**participants**|**video**|**topic**|
|:-----|:-----|:-----|:-----|:-----|
|Shift focus to the Skype client|**omitted**<br/>**null**<br/>**empty string**<br/>|**omitted**<br/>**null**<br/>**empty string**<br/>|Ignored|Ignored|
|Audio call-dialog|**call**<br/>**dropdown** (select **Call**)<br/>**omitted**<br/>**null**<br/>**empty string**<br/>|Exactly one|**false**<br/>**omitted**<br/>**null**<br/>**empty string**|Ignored|
|Audio call-conference|**call**<br/>**dropdown**  (select **Call**)<br/>**omitted**<br/>**null**<br/>**empty string**<br/>|Minimum of two|**false**<br/>**omitted**<br/>**null**<br/>**empty string**<br/>|Optional|
|Video call-dialog|**call**<br/>**dropdown**  (select **Call**)<br/>|Exactly one| **true**|Ignored|
|Group video call (GVC)|**call**<br/>**dropdown** (select **Call**)<br/>|Minimum of two (currently, maximum of ten)|**true**|Optional|
|Chat-dialog|**chat**<br/>**dropdown** (select **Call**)<br/>|Exactly one|Ignored|Ignored|
|Multi-chat|**chat**<br/>**dropdown** (select **Call**)|Minimum of two|Ignored|Optional|

## How can I determine if a Skype client is installed?

For web pages running in browsers, determining whether a Skype client is available varies from automatic to complex 
and unreliable. Some browsers on some platforms simply intercept the navigation failure, making it difficult, if not 
impossible, for your code to recover from the perceived error.

For versions starting with Internet Explorer 10 running on Windows 8, the browser detects that there is no application 
associated with the **skype:** scheme, and alerts the user.


**Figure 2. Windows Store dialog box**

![Windows 8 notification that no app is installed](images/skypeUri_Win8Store.png)

For most major browsers running on most major platforms, Skype buttons and the  *Skype.ui* function detect that the 
Skype client is not installed, and redirect the user to the platform-specific Skype client's 
[download page](https://www.skype.com/go/download) on skype.com.

Other browser/platform combinations might prompt you to choose an application, and might even list Skype—but fail 
silently even if you choose Skype. Still other browser/platform combinations might behave as if the Skype client 
is installed when it is not, or simply fail silently. 

Currently, the following browser/platform combinations fail to properly detect whether the Skype client is installed:


* Any browser running on Windows Phone 8
* Google Chrome running on Android (Gingerbread, Honeycomb, Ice Cream Sandwich, and Jelly Bean)
* Native Android browser ("globe" icon) running on Android (Gingerbread, Honeycomb, Ice Cream Sandwich, and Jelly Bean)


## What to do if a Skype client is not installed

Browser versions starting with Internet Explorer 10 running on Windows 8 include a link to the Store as part of its 
alert whenever it detects that no application is associated with the  *skype:* scheme. Simply click the Store icon to 
navigate to the Skype client's entry.


**Figure 3. Windows Store dialog box**

![Windows 8 notification highlighting store link](images/skypeUri_Win8StoreHighlight.png)

For most major browsers running on most major platforms, Skype buttons and the **Skype.ui** function automatically 
redirect the user to the platform-specific Skype client's [download page](https://www.skype.com/go/download) on 
skype.com whenever it detects that no Skype client is installed.

For all other browser/platform combinations—specifically Windows Phone 8 and Android platforms—you might consider 
including a note or FAQ entry alerting your users to the fact that they must have an installed Skype client on their 
mobile device, laptop, or desktop. You might also consider including a link to the mobile device's marketplace or 
the platform-specific Skype client's [download page](https://www.skype.com/go/download) download page on skype.com.


## See also


* [Skype URIs](SkypeURIs.md)
* [Skype URI API reference](SkypeURIAPIReference.md)
* [Skype URIs: Branding guidelines](SkypeURIs_BrandingGuidelines.md)
* [Skype URIs: FAQs](SkypeURIs_FAQs.md)
* [Skype URI tutorial: Windows 8 apps](SkypeURITutorial_Windows8Apps.md)
* [Skype URI tutorial: Email](SkypeURITutorial_Email.md)
* [Skype URI tutorial: Android apps](SkypeURITutorial_AndroidApps.md)
* [Skype URI tutorial: iOS apps](SkypeURITutorial_iOSApps.md)
