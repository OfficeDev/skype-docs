
# Skype URI API reference

Learn how to use Skype URIs to add Skype functionality to your client applications.


 _**Applies to:** Skype_

This API reference details the functionality, options, and syntax of each supported Skype URI. Use this reference if you're 
interested in creating applications for Windows, Android, and iOS platforms and mobile devices, or creating Skype URI-enabled emails or signature blocks.

If you're interested in creating Skype URI-enabled webpages, see the [Skype.ui JavaScript function](SkypeURItutorial_Webpages.md).

## Supported Skype URI APIs

Currently supported Skype URIs include:

* Switching focus to the Skype client.
* Initiating audio calls to other Skype users, phones, or mobile devices—both one-to-one dialogs and multiparty conferences.
* Initiating video calls to another Skype user.
* Sending instant messages to an individual or establishing a group multi chat.


## Start/switch focus to the Skype client

 **Effect**

* If the Skype client is not running—starts the Skype client, and switches focus to the Skype main window.
* If the Skype client is already running—switches focus to the Skype client.
* If the Skype client is already running and has the current focus—no effect; the Skype client retains focus.
* If the Skype URI starts the Skype client, focus is set to the Skype client's main window (auto-login is enabled), 
or to its sign-in dialog box (auto-login is not enabled). If the Skype client is already running, the focus is set to the 
main Skype window, even if a Skype client dialog box, such as the  **Tools−>Options** dialog box, is open.

 **Syntax**

 `skype:`

 **Example**

 `skype:`

 **Caveats**

On iOS, this Skype URI not only switches focus to the Skype client, but also attempts to initiate a call.


## Audio calls

Call-related Skype URIs support one-to-one dialogs, conference calls, and video calls.

By default, initiating a call results in the local Skype client displaying a confirmation dialog before proceeding with the call. In that dialog, 
the user can choose to not display the confirmation in future.

If your call specifies multiple participants, the individual identities—Skype Names or phone numbers—must be separated with semicolons; for example:

 `participant1;participant2;participant3`

<a name="call"></a>

### Audio call—implicit

 **Effect**

Calls other people on Skype, phones, or mobile devices This is an implicit version of the  **skype:participantList?call** 
Skype URI. Unlike the explicit version, you cannot supply additional arguments—like **video** or **conference topic** —with 
the command. So, the implied value of the  **video** argument is **false** (audio call), and the implied value of the **topic** 
argument is **null**/none specified.

 **Syntax**

 `skype:participant1[;participant2;...participant9]`

 **Example: One-to-one dialog**

 `skype:skype.test.user.1`

 **Example: Conference**

 `skype:skype.test.user.1;skype.test.user.2;skype.test.user.3`

 **Caveats**

Mobile Skype clients (iOS and Android) do  _not_ support initiating/hosting conference calls.


### Audio call—explicit

 **Effect**

Calls other people on Skype, phones, or mobile devices This explicit version of the command enables you to 
supply additional arguments—like **conference topic**—with the command.

The conference topic argument (**topic**) enables you to specify a string for call participants to display as 
the conversation topic in place of the Skype Names or phone numbers of the call originator/conference participants.

You must escape certain special characters, such as whitespace. For example, specify:

 `My Conference Topic`

as:

 `My%20Conference%20Topic`

Otherwise, the Skype client will interpret the topic argument value as  **My**, and the following words might 
cause any subsequent arguments to be ignored or otherwise misinterpreted.

While your Skype URI can specify  **video=false** to specifically indicate an audio call, common practice is 
to simply omit the argument. See [Audio call—implicit](#call) and [Video call](#video).

 **Syntax**

 `skype:participant1[;participant2;...participant9]?call[&amp;topic=topicString]`

 **Example: One-to-one dialog**

 `skype:skype.test.user.1?call`

 **Example: Conference, no topic**

 `skype:skype.test.user.1;skype.test.user.2;skype.test.user.3?call`

 **Example: Conference, setting the topic**

 `skype:skype.test.user.1;skype.test.user.2;+16505550123?call&amp;topic=Geek%20Conspiracy`

 **Caveats**

* The optional topic argument applies to conference calls only.
* Special characters in the optional topic argument value—specifically whitespace—must be escaped.
* Mobile Skype clients (iOS and Android) do not support initiating/hosting conference calls.
<a name="video"></a>

### Video call

 **Effect**

Calls other people on Skype, and automatically turns on the call originator's video feed (if a local webcam is available).

 **Syntax**

 `skype:participant1[;participant2;...participant9]?call&amp;video=true`

 **Example: One-to-one dialog**

 `skype:skype.test.user.1?call&amp;video=true`

 **Example: Conference**

 `skype:skype.test.user.1;skype.test.user.2;skype.test.user.3?call&amp;video=true`

 **Caveats**


* Specifying multiple participants results in a [group video call](https://www.skype.com/go/groupvideocalling/).

  * The number of participants is limited to ten, with a recommended maximum of five for the best call quality.
  * Mobile Skype clients (iOS and Android) do not support initiating/hosting group video calls.

* Currently, participants on mobile devices can join group video calls via voice only.


## Chats

Chat-related Skype URIs support both one-to-one dialogs and group chats.

If your chat specifies multiple participants, the individual identities—Skype Names only—must be separated with semicolons; 
for example:

 `participant1;participant2;participant3`

If a conversation with the same participant or participants already exists, the Skype client or clients open the 
existing conversation. If the topic of an existing conversation differs from the one specified by the Skype URI, 
the Skype client changes the conversation topic to the new value.


### Open/create chat

 **Effect**

Opens the conversation that matches the specified list of participants, or creates a new chat with those participants 
if no matching conversation exists. For existing conversations, the specified optional topic argument value replaces 
the existing conversation name or names and title string.

 **Syntax**

 `skype:participant1[;participant2;...participant9]?chat[&amp;topic=topicString]`

 **Example: dialog**

 `skype:skype.test.user.1?chat`

 **Example: multichat; setting the topic**

 `skype:skype.test.user.1;skype.test.user.2?chat&amp;topic=Quantum%20Mechanics%20101`

 **Caveats**


* The optional topic argument applies to multi chats only.
* Special characters in the topic argument value—specifically whitespace—must be escaped.
* Mac OS X ignores any **topic** argument.
* iOS is not supported.
* Android recognizes the initial participant only; multi chats are not supported.


## See also


* [Skype URIs](SkypeURIs.md)
* [Skype URI API reference](SkypeURIAPIReference.md)
* [Skype URIs: Branding guidelines](SkypeURIs_BrandingGuidelines.md)
* [Skype URIs: FAQs](SkypeURIs_FAQs.md)
* [Skype URI tutorial: Windows 8 apps](SkypeURITutorial_Windows8Apps.md)
* [Skype URI tutorial: Email](SkypeURITutorial_Email.md)
* [Skype URI tutorial: Webpages](SkypeURItutorial_Webpages.md)
* [Skype URI tutorial: Android apps](SkypeURItutorial_AndroidApps.md)
* [Skype URI tutorial: iOS apps](SkypeURITutorial_iOSApps.md)

