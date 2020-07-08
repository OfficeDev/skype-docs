
# Skype URIs: FAQs

Find answers to frequently asked questions about using Skype URIs in your applications and webpages.

 _**Applies to:** Skype_

## If I have more than one Skype Button on my webpage, why does only the first one work?

Each Skype Button must have a unique ID. The easiest way to create the right code is to use the [Skype Buttons generator](https://www.skype.com/en/features/skype-buttons/create-skype-buttons/). 
However, if you want to use a Skype Button with the same function and Skype Name more than once on the same webpage, 
you'll need to change the ID yourself to make it unique again. 

The format of the ID is:

 `SkypeButton_function_#username_sequence`

In this example:

* **function** is either "Call" or "Chat".
* **username** is the Skype Name of the person you want the Button to call or chat with.
* **sequence** is a sequence number to make the ID string unique within your webpage.

This ID appears in two places in the generated code. Both places must be the same, as shown in the following example.


```html
<div id="SkypeButton_Call_#echo123_1">
  ...
  "element": "SkypeButton_Call_#echo123_1",
  ...

```

 **Correct example**

Here, the sequence number portion of the div element's attribute has been incremented from 1 to 2 for both the  **id** 
attribute value and the **"element"** property value. The **div** element's ID is once again unique, and it matches 
the **"element"** property value.


```html
<div id="SkypeButton_Call_#echo123_2">
  ...
  "element": "SkypeButton_Call_#echo123_2",
  ...

```

 **Incorrect example**

Here, the  **div** element's **id** attribute has been incremented to make it unique, but the **"element"** property 
value has not been updated to match.


```html
"element" property value has not been updated to match.
<div id="SkypeButton_Call_#echo123_2">
  ...
  "element": "SkypeButton_Call_#echo123_1",
  ...

```


## On which Android devices are Skype URIs supported?

We actively work to ensure a consistent experience when using URIs across all supported platforms. However, we cannot 
always guarantee that URIs will work across all Android devices. The best experience will always be on the latest 
version of the Skype client.

You can download and install the Skype for Android client on your device from the Android Market 
or [Google play store](http://market.android.com/details?id=com.skype.raider).


## Can I embed a Skype Button on a secure page?

The [generated Skype Button](https://www.skype.com/en/features/skype-buttons/create-skype-buttons/) code is hard-linked 
to the Skype CDN (Content Delivery Network) using the HTTP protocol, but will also work over HTTPS.

**Existing code:**

```html
 <script type="text/javascript" src="http://cdn.dev.skype.com/uri/skype-uri.js">
```

If you are embedding Skype buttons on a page that uses SSL, you can change the CDN link from **HTTP** to **HTTPS** 
to avoid the browser warning message about potential insecure content when users view the page.

**Modified code:**

```html
 <script type="text/javascript" src="https://cdn.dev.skype.com/uri/skype-uri.js">
```


## What is the best way to integrate my app with Skype on Windows 8 and newer?

Skype has two different clients for Windows 8 users. The first, Skype for Desktop, will seem very similar to our 
existing Skype for Windows client. The second, however, Skype for Windows 8, is something completely new and 
visually stunning. We've built Skype for Windows 8 from the ground up to enable the best of what Skype and Windows 
8 have to offer.

The best way to integrate Skype into your Windows Store App is to use [Skype URIs](SkypeURIs.md), which are simple, 
cross-platform links that you can use to initiate Skype calls and chats in Skype clients.


## See also


* [Skype URIs](SkypeURIs.md)
* [Skype URI API reference](SkypeURIAPIReference.md)
* [Skype URIs: Branding guidelines](SkypeURIs_BrandingGuidelines.md)
* [Skype URI tutorial: Windows 8 apps](SkypeURITutorial_Windows8Apps.md)
* [Skype URI tutorial: Email](SkypeURITutorial_Email.md)
* [Skype URI tutorial: Webpages](SkypeURItutorial_Webpages.md)
* [Skype URI tutorial: Android apps](SkypeURITutorial_AndroidApps.md)
* [Skype URI tutorial: iOS apps](SkypeURITutorial_iOSApps.md)
