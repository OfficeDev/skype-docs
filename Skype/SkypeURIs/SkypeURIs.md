
# Skype URIs

Learn about Skype development options, and using the Skype URIs to start calls and chats in the Skype clients.


 _**Applies to:** Skype_

Skype URIs provide a simple way for you to initiate Skype calls and chats directly from websites, desktop, and mobile apps.


## Introduction to Skype URIs

Skype URIs enable you to create innovative mobile, web, and desktop apps that initiate Skype calls and chats, 
enabling your users to reach their friends, family and businesses in a convenient yet familiar way. For example, 
if your mobile app presents a contact list that contains Skype names or phone numbers, your app can use a Skype URI to 
launch the official Skype client and initiate a call to a selected contact.

Skype URIs are the preferred mechanism for integrating with the Skype client, and are supported on iPhone, iPad, Android, 
versions of the Windows operating system starting with Windows XP, Linux, and Mac OS X.

For a Skype URI to work, a Skype client must be installed on the user's device, and the user must have an active Skype 
account. Certification of applications or webpages using Skype URIs is not required, because all interaction—specifically 
all audio and video—is through the Skype client.

Currently supported Skype URIs include:


* Switching focus to the Skype client. (See [Start/switch focus to the Skype client](SkypeURIAPIReference.md).)
* Initiating audio calls to other Skype users, phones, or mobiles—both one-to-one dialogs and multi-party conferences. (See [Audio call—implicit](SkypeURIAPIReference.md).)
* Initiating video calls to another Skype user. (See [Video call](SkypeURIAPIReference.md).)
* Sending instant messages to an individual or establishing a group multi-chat. (See [Chats](SkypeURIAPIReference.md).)

## How Skype URIs work

In its simplest form, you can embed a hyperlink referencing a Skype URI in a webpage to place a Skype call. For example, 
to initiate a call to the Skype Echo / Sound Test Service, the link would be:


```html
<a href="skype:echo123?call">Call the Skype Echo / Sound Test Service</a>
```

Clicking the link:


1. Brings the device's Skype client into focus, starting it as necessary.
1. Effects auto-login or prompts your users for their Skype Name and password.
1. Typically opens a confirmation dialog to authorize placing the call.
1. Places the call.


[Skype buttons](https://www.skype.com/en/features/skype-buttons/create-skype-buttons/) provide you with a generated 
block of HTML that has a Skype URI at its core. Simply use the form to specify the type of button you want, then 
paste the code snippet into a webpage so people can easily call or chat with you over Skype.

The [Skype.ui JavaScript function](SkypeURItutorial_Webpages.md) enables you to dynamically embed Skype URIs 
that have a predefined appearance and user interface similar to [Skype buttons](https://www.skype.com/en/features/skype-buttons/create-skype-buttons/), 
but provides additional functionality, such as video, conference calls, and multi-chats.

Apps can construct and access an appropriate Skype URI in response to user actions. For example, tapping a contact's 
picture on your mobile device might construct and access a "call" Skype URI specifying that contact's Skype Name or 
phone number.

For information about the syntax of each currently supported Skype URIs, see [Skype URI API reference](SkypeURIAPIReference.md).

For information about and sample codes that illustrate how you can use each of the currently supported Skype URIs in 
your apps, emails, and webpages, see the following:


* [Skype URI tutorial: Windows 8 apps](SkypeURITutorial_Windows8Apps.md)
* [Skype URI tutorial: Email](SkypeURITutorial_Email.md)
* [Skype URI tutorial: Webpages](SkypeURItutorial_Webpages.md)
* [Skype URI tutorial: Android apps](SkypeURITutorial_AndroidApps.md)
* [Skype URI tutorial: iOS apps](SkypeURITutorial_iOSApps.md)


> [!NOTE] 
> Your use of Skype URIs implies access to and use of Skype software, as governed by Skype's 
[Terms of Use](https://www.skype.com/go/tou/).


## Interacting with the Skype client

Skype URIs depend on an installed and running Skype client—one on the sending device, and one on each recipient's device. 
The sending client must primarily concern itself with options related to start-up and login. The receiving client must 
primarily concern itself with options related to accepting calls and chats, particularly if your application or webpage 
is using Skype URIs to connect customers and other interested parties with businesses and organizations.


### Sending client

Skype URIs behave differently depending on browser options (web-based apps), the state of the Skype client, 
Skype client options, and user actions, as follows:


* The user might be prompted to authorize the Skype client if it is not already running (browser options), as in the following figures.

    **Figure 1. Internet Explorer dialog box**

    ![Dialog box prompting user to allow Skype to open](images/skypeUri_AllowProgramOpen_IE.png)

    **Figure 2. Firefox dialog box**

    ![Dialog box prompting user to allow Skype to open](images/skypeUri_AllowProgramOpen_Android.png)

* The user will have to log on if the Skype client is currently:

  * Not running and auto-login is either disabled or currently inactive due to explicit logout (Skype client options; user actions).
  * Running with no currently logged on user.
* The user might be prompted to authorize the client action; for example, a "confirm call" dialog might appear (Skype client options).

    **Figure 3. Confirm call dialog box**

    ![Dialog box prompting user to allow a Skype call](images/skypeUri_AllowCall.png)

The documentation for your app should encourage users to enable their Skype client's auto-logon option, and exit the Skype 
client rather than explicitly logging out. If the Skype URI references the currently logged-in user, some Skype clients 
simply shift focus, while others present a dialog indicating that you cannot start a conversation with yourself.

When the Skype client completes the requested, regardless of success, focus can remain with the Skype client rather than 
returning to your app. This depends on the type of action requested, as well as which platform the Skype client is running 
on. For example, chats are open-ended, so focus always stays with the Skype client. However, while calls terminate when all 
but one participant has hung up, focus always stays with the Skype for Windows client but returns to your app for the Skype 
for Android client.

Determining whether a Skype client is available on a mobile or desktop device—and what to do if it is not—depends on the 
platform and the accessing browser, webpage, or application. Ideally, you want to detect whether the Skype client is 
present, and direct the user to download and install the Skype client if it is not. In fact, your app should navigate 
directly to the mobile device's marketplace or the platform-specific Skype client's [download page](https://www.skype.com/go/download) 
on skype.com whenever it detects that a Skype client is not present. For information about and sample code for detecting 
and installing a missing Skype client, see the associated tutorial page; for example, [What to do if a Skype client is not installed](SkypeURITutorial_AndroidApps.md), 
if you are developing an Android app.

Keep in mind that installing the Skype client on the device might additionally require the user to create a Skype account 
and issue/accept one or more Contact requests before they can effectively use Skype URIs.


### Receiving client

Embedding Skype URIs in advertisements, search results, email signatures, "contact us" pages, and so forth enables customers 
and other interested parties to easily connect with your business or organization via Skype. However, because those callers 
are unlikely to be in your Contacts list, you must configure the receiving Skype client to accept calls and/or chats from 
"anyone". You might also want to consider automatically receiving video and/or showing your online presence if this is a 
web-based application or page.

**Figure 4. Skype options dialog box**

![Privacy settings in Skype options dialog box](images/skypeUri_PrivacySettings.png)


## Branding your app

Your use of the Skype logo, name, and other brand elements in the design, appearance, and marketing materials for your app 
are detailed in and governed by the [Skype Trade Mark Guidelines](https://www.skype.com/en/legal/brand-guidelines/). As a 
general rule, third parties  _may not_ use the Skype Brand Elements unless they have either received prior written 
permission, or the proposed use falls within certain limited exceptions.

See [Skype URIs: Branding guidelines](SkypeURIs_BrandingGuidelines.md) for complete guidelines regarding use of the 
Skype URI-specific brand elements.


## Need help?

Please contact [Skype Developer Support](mailto:SkypeDev@Microsoft.com?subject=Skype URIs).


## In this section


* [Skype URI API reference](SkypeURIAPIReference.md)
* [Skype URIs: Branding guidelines](SkypeURIs_BrandingGuidelines.md)
* [Skype URIs: FAQs](SkypeURIs_FAQs.md)
* [Skype URI tutorial: Windows 8 apps](SkypeURITutorial_Windows8Apps.md)
* [Skype URI tutorial: Email](SkypeURITutorial_Email.md)
* [Skype URI tutorial: Webpages](SkypeURItutorial_Webpages.md)
* [Skype URI tutorial: Android apps](SkypeURITutorial_AndroidApps.md)
* [Skype URI tutorial: iOS apps](SkypeURITutorial_iOSApps.md)


## See also

* [Office development](https://msdn.microsoft.com/library/7f24db34-c1ad-4a83-a9bd-3c85a39c0bd8%28Office.15%29.aspx)

