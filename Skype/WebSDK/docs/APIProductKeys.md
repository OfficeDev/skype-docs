# Skype Web SDK Production Use Capabilities


 _**Applies to:** Skype for Business, Skype for Business Online_

 **In this article**  
[Product keys](#product-keys)  
[Feature support matrix](#feature-support-matrix)  
[Supported Browsers](#supported-browsers)  
[Supported Server Versions](#supported-server-versions)



This section lists the API product keys and the supported capabilities that are available for your production use application. With each successive release of the Skype Web SDK, additional features are supported. Use the support matrix table in this article to find the appropriate product key to use for the features you implement in your app. 

## Product keys
<a name="sectionSection0"> </a>

|||
|:-----|:-----|
|**Release**|**Product keys**|
|Public Preview (PP)|API Key: 'a42fcebd-5b43-4b89-a065-74450fb91255' <br/> UI Key: '9c967f6b-a846-4df2-b43d-5167e47d81e1' |
|General Availability (GA)|API Key: '595a1aeb-e6dc-4e5b-be96-bb38adc83da1' <br/> UI Key: '08c97289-7d57-404f-bd97-a6047403e370'|




## Feature support matrix
<a name="sectionSection1"> </a>

The following table is a mapping of Features to the API version that they are supported in.
tst

||||
|:-----|:-----|:-----|
|**Feature**|**Public Preview**|**General Availability**|
|Sign in, Sign out. |X |X|
|View Signed in user’s information. |X|X|
|Update Note and Presence for the signed in user. |X|X|
|View the contact list of the signed in users. |X|X|
|Search for persons or groups. |X|X|
|Add/remove/Rename groups in the contact list. |X|X|
|Add/remove distribution groups in the contact list.   |X|X|
|Add/remove contacts to groups in the contact list.   |X|X|
|Add/remove telephone contacts to groups in the contact list. |X|X|
|Start and have P2P chat conversations as a signed in user with another signed in Skype or Skype for business user via Skype Web SDK API (outgoing invitations)  |X|X|
|Accept and have P2P chat conversations as a signed in user with another signed in Skype or Skype for business user via Skype Web SDK API. (incoming invitations) |X|X|
|Start and have meeting chat conversations as a signed in user via Skype Web SDK API.  |X|X|
|Start and have P2P or meeting audio/video conversations with the signed in users   |X|-|
|Invite more participant to a Skype for Business meeting Chat conversation, or to a P2P Chat conversation to escalate to meeting via the Skype Web SDK API |X|X|
|Invite more participant to an Skype for Business meeting AV conversation, or to a P2P AV conversation to escalate to meeting |X|-|
|Start and have PSTN audio conversations (outgoing PSTN calling) |X|-|
|Accept and have PSTN audio conversations (incoming PSTN calling) |X|-|
|Schedule a Skype for Business meeting. |X|X|
|Join a Skype for Business meeting and start chat and/or audio service in the conversation. |X|-|
|Join a Skype for Business meeting as a signed in user with audio and video. |X|-|
|Join a Skype for Business meeting as an anonymous user. |X|-|
|In a Skype for Business meeting, one can open up to four video windows from different remote participants at the same time.  |X|-|
|Retrieve the different devices, and select a specific device from the list.  |X|-|
|The application context can be passed to the remote party in the conversation invite.  |X|X|
|Use Skype Conversation UI control in your applications for P2P/Group IM  |X|X|
|Use Skype Conversation UI control in your applications for P2P/Group AV  |X|-|

## Supported Browsers

- IE 11
- Safari 8+
- Firefox 40+ (non Audio/Video functionality)
- Chrome 43+ (non Audio/Video functionality)
- Microsoft Edge 

## Supported Server versions 

- Lync 2013 cumulative update 6HF2 +, Skype for Business 2015 CU1+:
 
   - Scenario:
    - Sign in
    - Sign out
    - Presence
    - View Contacts
    - Groups
    - Chat services
    - Skype Conversation UI
    

- Skype for Business 2015 Cumulative Update 1+

    - Scenario: 
      - P2P AV
      - Group AV
      - Devices selection
      - Anonymous meeting join
      - Add/remove contacts and groups

