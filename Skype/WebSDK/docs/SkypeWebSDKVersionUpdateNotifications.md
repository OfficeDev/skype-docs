# Skype Web SDK Version Updates

## Skype Web SDK Version Update 4/13/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.87.47 | 4/13/17 | 1.86.57
| Skype Web SDK Preview    | 0.4.464 | 4/13/17 | 0.4.449
| Conversation Control Production | unchanged | 4/3/17 | 1.86.57
| Skype Web SDK Production| unchanged | 4/3/17 | 0.4.449 |

There is no new production release of either the WebSDK or Conversation Control.

**Bugs fixed in the new public preview build:**
* Serialization of outgoing renegotiations
* Sync person.status once connection is restored
* Ensure negotiation rejection if media agent fails to process the final answer

---

## Skype Web SDK Version Update 3/29/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.86.57 | 3/29/17 | 1.85.29
| Skype Web SDK Preview    | 0.4.449 | 3/29/17 | 0.4.440
| Conversation Control Production |  1.85.29 | 3/29/17 | 1.85.27
| Skype Web SDK Production| 0.4.440 | 3/29/17 | 0.4.438 |

The latest release fixes several bugs in relation to AV scnearios.

**Bugs fixed in the new public preview build:**
* Solved spinner issues for participants that don't have a camera
* Fixed camera issues when escalating to a meeting
* Fixed negotiation issue with WebRTC enabled clients

---

## Skype Web SDK Version Update 3/21/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.85.29 | 3/21/17 | 1.85.27
| Skype Web SDK Preview    | 0.4.440 | 3/21/17 | 0.4.438
| Conversation Control Production |  1.85.27 | 3/21/17 | 1.84.20
| Skype Web SDK Production| 0.4.438 | 3/21/17 | 0.4.436 |

The latest release fixes several bugs in relation to AV scnearios.

**Bugs fixed in the new public preview build:**
* audioService.stop will now be disabled when accepting a call fails because of plugin issues
* Re-joining a conversation after refreshing the page the application is hosted on is now possible
* Added various AV related fixes

---

## Skype Web SDK Version Update 3/14/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.85.27 | 3/14/17 | 1.84.20
| Skype Web SDK Preview    | 0.4.438 | 3/14/17 | 0.4.436
| Conversation Control Production |  1.84.20 | 3/14/17 | 1.81.43
| Skype Web SDK Production| 0.4.436 | 3/14/17 | 0.4.417 |

The latest release fixes several bugs in relation to AV scnearios.

**Bugs fixed in the new public preview build:**
* Improved reliability of connecting audio calls
* Fixed issues that prevented the active speaker indicator to show
* Fixed redundant notifications
* Resolved issue of where the local video would not display when resuming a held call
* Fixed issues where DTMF tones weren't sent

---

## Skype Web SDK Version Update 2/21/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | unchanged | 2/21/17 | 1.81.43
| Skype Web SDK Preview    | unchanged | 2/21/17 | 0.4.417
| Conversation Control Production |  1.81.43 | 2/21/17 | 1.80.33
| Skype Web SDK Production| 0.4.417 | 2/21/17 | 0.4.411 |

There is no new preview release of either the WebSDK or Conversation Control, however
last week's preview builds will be rolled into production.

**Bugs fixed in the new public preview build:**

None

---

## Skype Web SDK Version Update 2/14/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.81.43 | 2/14/17 | 1.80.33
| Skype Web SDK Preview    | 0.4.417 | 2/14/17 | 0.4.411
| Conversation Control Production | 1.80.33 | 2/14/17 | 1.79.32
| Skype Web SDK Production| 0.4.411 | 2/14/17 | 0.4.405 |

The latest preview release includes several fixes to improve the resilience of AV
calling with the SDK in the case of unexpected failures, and fixes a rare case
where the application will hang indefinitely while trying to sign in.

**Bugs fixed in the new public preview build:**

- Call does not end/disconnect call after losing network connection
- Set container on the sourced stream instead of the source stream if available, 
and distinguish stream of participant's video channels in conference call from 
P2P/unitialized streams
- makeMeAvailable should be done only on the current application
- UCWA returns an application with a new id after 404 causing sdk to go into an 
infinite loop of POST /applications
- Recovering from a plugin crash and cleaning up for subsequent AV calls
- Uninitialize devices manager when plugin manager becomes uninitialized
- Add debugging information for infinite 404s that we see in Prod due to user uri
changing and causing new application id
- add Utils.debug.dumpDebugInfo function to dump small amount of identifying info
to console

---

## Skype Web SDK Version Update 2/7/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.80.33 | 2/7/17 | 1.79.32
| Skype Web SDK Preview    | 0.4.411 | 2/7/17 | 0.4.405
| Conversation Control Production | 1.79.32 | 2/7/17 | 1.78.28
| Skype Web SDK Production| 0.4.405 | 2/7/17 | 0.4.397 |

The latest preview release includes a couple changes to make it easier for the
Web SDK team to debug reported issues.

**Bugs fixed in the new public preview build:**

- Add X-Ms-SDK-Version and X-Ms-SDK-Session headers to every request

---

## Skype Web SDK Version Update 1/31/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.79.32 | 1/31/17 | 1.78.28
| Skype Web SDK Preview    | 0.4.405 | 1/31/17 | 0.4.397
| Conversation Control Production |  1.78.28 | 1/31/17 | 1.78.19 
| Skype Web SDK Production| 0.4.397 | 1/31/17 | 0.4.394 |

The latest preview release includes numerous fixes to AV call issues, particularly
in group and p2p -> group escalation scenarios, and addresses some additional rare
cases when the application can become spontaneously signed out.

**Bugs fixed in the new public preview build:**

- Not able to restart video in a 3 user call escalated from IM -> audio -> video
- Black self video is seen on Mac when we turn video off and on in a P2P video call
- Set participant.audio.isSpeaking in WebRTC meetings
- No toast/notification when a participant adds audio to a group IM conversation
- Disconnect call sound always comes in conversation control after user accepts a
  video invitation
- Adding video to IM fails on IE (Call from IE to Microsoft Edge)
- Settings.logHttp now enables a console warning when Property.set is invoked twice 
  concurrently
- Manually set videoStream._isFlowing to account for auto-unloading of plugin in Safari
- After stopping and restarting video in a meeting, other participant's video does 
  not show until they toggle their video
- Incoming video freezes in conversation control when a remote user escalates a 
  P2P call to conference and then turns off video

---

## Skype Web SDK Version Update 1/24/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.78.28 | 1/24/17 | 1.78.19
| Skype Web SDK Preview    | 0.4.397 | 1/24/17 | 0.4.394
| Conversation Control Production |  1.78.19 | 1/24/17 | 1.72.36 
| Skype Web SDK Production| 0.4.394 | 1/24/17 | 0.4.385 |

The latest preview release includes an improvement to SDK telemetry and
fixes a couple edge cases where an SDK application can become incorrectly signed out.

**Bugs fixed in the new public preview build:**

- Fix telemetry to show various error properties in some correct form
  so querying later is easier
- Fix multiple bugs which can cause application to spontaneously sign out in
  rare cases

---

## Skype Web SDK Version Update 1/18/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.78.19 | 1/18/17 | 1.77.8
| Skype Web SDK Preview    | 0.4.394 | 1/18/17 | 0.4.385
| Conversation Control Production | unchanged  | 1/18/17 | 1.72.36 
| Skype Web SDK Production| 0.4.385 | 1/18/17 | 0.4.374 |

The latest preview release includes improvements to AV call reliability, and
fixes a rare case where the application can be spontaneously signed out.

**Bugs fixed in the new public preview build:**

- There should not be a missed call notification when call answered on SFB
- IE group call - Remote video just keeps spinning
- Ensure we stream own video when joining a video meeting

---

## Skype Web SDK Version Update 1/10/17

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.77.8 | 1/10/17 | 1.72.36
| Skype Web SDK Preview    | 0.4.385 | 1/10/17 | 0.4.374
| Conversation Control Production | 1.72.36  | 1/10/17 | 1.71.32
| Skype Web SDK Production| 0.4.374 | 1/10/17 | 0.4.368 |

The latest preview release includes a number of fixes to AV call reliability, 
updates the SDK to typescript 2, and fixes a bug where IMs sometimes change 
order after being sent.

**Bugs fixed in the new public preview build:**

- Only one directional video in certain IE calls
- Audio fails in Microsoft Edge if user mutes, leaves the call and rejoins it again
- Simplify the timestamp logic to prevent reordering of IMs
- video.channels(0).isStarted incorrectly failing in Microsoft Edge group video calls

---

## Skype Web SDK Version Update 12/13/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.72.36 | 12/13/16 | 1.71.32
| Skype Web SDK Preview    | 0.4.374 | 12/13/16 | 0.4.368
| Conversation Control Production | 1.71.32  | 12/13/16 | 1.70.42
| Skype Web SDK Production| 0.4.368 | 12/13/16 | 0.4.361 |

The latest preview release fixes a bug where multiple AV renegotiations could
conflict with each other.

**Bugs fixed in the new public preview build:**

- Microsoft Edge clients experience outgoing/incoming renegotiation
conflicts when joining AV meetings 

---

## Skype Web SDK Version Update 12/6/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.71.32 | 12/6/16 | 1.70.42
| Skype Web SDK Preview    | 0.4.368 | 12/6/16 | 0.4.361
| Conversation Control Production | 1.70.42  | 12/6/16 | 1.70.4
| Skype Web SDK Production| 0.4.361 | 12/6/16 | 0.4.356 |

The latest preview release includes a fix for a bug with emojis in chat messages
on certain platforms.

**Bugs fixed in the new public preview build:**

- Set default character in case unparseable character is sent in chat message from certain platforms

---
## Skype Web SDK Version Update 11/29/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.70.42 | 11/29/16 | 1.70.4
| Skype Web SDK Preview    | 0.4.361 |  11/29/16 | 0.4.356
| Conversation Control Production | 1.70.4  | 11/29/16 | 1.67.37
| Skype Web SDK Production| 0.4.356 |  11/29/16 | 0.4.341 |

The latest preview release includes a minor correction to documentation and a small
telemetry fix, but no major changes.

**Bugs fixed in the new public preview build:**

- Fix signInManager’s documentation
- Telemetry update to determine number of unique searches

---
## Skype Web SDK Version Update 11/22/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.70.4 | 11/22/16 | 1.69.15
| Skype Web SDK Preview    | 0.4.356 |  11/22/16 | 0.4.341
| Conversation Control Production | unchanged  | 11/15/16 | 1.67.37
| Skype Web SDK Production| unchanged |  11/15/16 | 0.4.341 |


The latest preview release includes a number of improvements to telemetry
including eliminating false failures and ensuring that all debugging information
is present, a fix for the bug where one video container disappears in SWX upon
escalating a P2P video call to group, a couple of improvements to signin reliability,
and a security fix.

**Bugs fixed in the new public preview build:**

- Apply setWithCredentials on UCWA POSTs to fix 401 errors in CORS mode
- saveConsole exposes potential null ref exception
- Chat start invitation failed error with reason subcode Timeout has no response headers so cannot identify root cause
- One video lost on escalate p2p video to group video
- Retry sign in without snapshot on encountering a 410 Gone error during signin
- Guard against possible XSS attack
- Attempting to sign in after signin has failed will cause the signin promise to get resolved immediately with the previous failure

---
## Skype Web SDK Version Update 11/8/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.67.37 | 11/8/16 |1.65.51
| Skype Web SDK Preview    | 0.4.341 |  11/8/16| 0.4.327
| Conversation Control Production | 1.66.37  | 11/8/16 | 1.64.47
| Skype Web SDK Production| 0.4.336 |  11/8/16 | 0.4.325 |

The latest preview release includes improvements to Audio/Video reliability, 
a fix to prevent PII from being collected inadvertently, and a fix for a bug
where starting a group conversation with Skype for Business desktop clients
sometimes resulted in an Audio/Video invitation instead.

**Bugs fixed in the new public preview build:**

- Replace usage of an internal Microsoft Edge media library method with the new public equivalent
- Errors thrown should hash or strip out the Auth header to protect PII
- Starting a group IM from SWX to multiple Skype for Business desktop client participants sometimes
 starts a group audio call instead of a group IM conversation
- Collect data when call ends due to missing devices

---
## Skype Web SDK Version Update 11/1/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | Unchanged | 10/25/16 |1.65.51
| Skype Web SDK Preview    | Unchanged |  10/25/16| 0.4.327
| Conversation Control Production | Unchanged  | 10/25/16 | 1.64.47
| Skype Web SDK Production| Unchanged |  10/25/16 | 0.4.325 |

New builds will not be rolled out to preview or production this week because of the release of Microsoft Teams.

**Bugs fixed in the new public preview build:**

None

---
## Skype Web SDK Version Update 10/25/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.65.51 | 10/25/16 |1.63.51
| Skype Web SDK Preview    | 0.4.327 |  10/25/16| 0.4.312
| Conversation Control Production | 1.64.47  | 10/25/16 | 1.63.51
| Skype Web SDK Production| 0.4.325 |  10/25/16 | 0.4.312 |

The latest preview release includes improvements to reliability on certain application failures,
improvements to AV call reliability in Microsoft Edge, and a fix to correctly handle device changes in plugin-based AV calls.

**Bugs fixed in the new public preview build:**

- Send conversation subject along with audioService invitation
- Retry on 483 “TooManyHops”
- Retry on 409 “PGetReplaced” in certain cases
- Cache UCWA FQDN to accelerate auto discovery
- Ensure `application.devicesManager.mediaCapabilities.pluginDownloadLinks` returns a valid array of links
- Add a console debugging command to quickly save all console output to a file
- Correctly handle microphone and speaker changes during plugin-based AV calls

---
## Skype Web SDK Version Update 10/19/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | Unchanged | 10/12/16 |1.63.51
| Skype Web SDK Preview    | Unchanged  |  10/12/16| 0.4.312
| Conversation Control Production | 1.63.51  |   10/19/16| 1.62.45
| Skype Web SDK Production| 0.4.312 |  10/19/16 |  0.4.306  |

There is no new preview version this week. There will be a new version next week that includes the changes that would have been released this week.

**Bugs fixed in the new public preview build:**

No new preview release

---
## Skype Web SDK Version Update 10/12/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.63.51 | 10/12/16 |1.62.45
| Skype Web SDK Preview    | 0.4.312  |  10/12/16| 0.4.306
| Conversation Control Production | 1.62.45  |   10/12/16| 1.61.68
| Skype Web SDK Production| 0.4.306 |  10/12/16 |  0.4.300  |

The latest preview release includes several small reliability improvements and fixes for unusual scenarios, but no major functional changes.

**Bugs fixed in the new public preview build:**

- Remove dependency on "MediaRelayAccessToken" to avoid issues where it cannot be found

---
## Skype Web SDK Version Update 10/4/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.62.45 | 10/4/16 |1.61.68 
| Skype Web SDK Preview    | 0.4.306  |  10/4/16| 0.4.300
| Conversation Control Production | 1.61.68   |   10/4/16|1.60.72
| Skype Web SDK Production| 0.4.300 |  10/4/16 |  0.4.293  |

The latest preview release includes a fix to accepting video calls with audio only in 
Microsoft Edge, a fix for receiving ‘Meet Now’ invites on applications that don’t support AV, 
and a fix for joining conferences anonymously in an ‘on prem’ topology.

**Bugs fixed in the new public preview build:**

-	Accepting incoming video call with only audio briefly broadcasts video
-	'Meet Now' invitations come as group call instead of group IM in platforms
    which don't support audio
-	Use /autodiscover/xframe/xframe.html in the 'on prem' join URL discovery

---
## Skype Web SDK Version Update 9/27/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.61.68 | 9/27/16 |1.60.72 
| Skype Web SDK Preview    | 0.4.300  |  9/27/16| 0.4.293
| Conversation Control Production | 1.60.72   |   9/27/16|1.59.79 
| Skype Web SDK Production| 0.4.293 |  9/27/16 |  0.4.288  |

The latest preview release includes an improvement to AV calls in Microsoft Edge, a fix for an edge case where suspended
multi-tab apps may not resume correctly, preview support for call transfer, and support for call hold and resume in Microsoft Edge.

**Bugs fixed in the new public preview build:**

- Implement basic call transfer
- Implement self hold/resume in P2P calls in Microsoft Edge
- Set person.type to phone if its sip uri ends with ;user=phone
- Fix bug in which SDK only relays the first SDP offer to media stack when accepting 
    an incoming AV call in Microsoft Edge

---
## Skype Web SDK Version Update 9/20/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.60.72 | 9/20/16 |1.59.79
| Skype Web SDK Preview    | 0.4.293  |  9/20/16| 0.4.288
| Conversation Control Production | 1.59.79   |   9/20/16|1.58.81 
| Skype Web SDK Production| 0.4.288 |  9/20/16 |  0.4.288  |

The latest release includes preview support for phone audio calling (PSTN) and improvements to AV calls in Microsoft Edge.

**Bugs fixed in the new public preview build:**

- Implement phone audio conversation modality
- Fix issue where SWX in Microsoft Edge doesn't process multiple media offers 

---
## Skype Web SDK Version Update 9/13/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.59.79 | 9/13/16 |1.58.81
| Skype Web SDK Preview    | 0.4.288  |  9/13/16| 0.4.281
| Conversation Control Production | 1.58.81   |   9/13/16|1.57.72 
| Skype Web SDK Production| 0.4.288 |  9/13/16 |  0.4.269  |

The latest release includes a critical fix to telemetry for the standalone SDK and improvements to AV calls in Microsoft Edge.

**Bugs fixed in the new public preview build:**

- Stop waiting for renegotiation in Microsoft Edge to declare call connected
- Remote user turning on video turns on self video if both users in Microsoft Edge
- Standalone telemetry manager sending events incorrectly

---
## Skype Web SDK Version Update 9/6/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.58.81 | 9/6/16 |1.57.70
| Skype Web SDK Preview    | 0.4.281      |  9/6/16| 0.4.275
| Conversation Control Production | 1.57.72    |   9/6/16|1.56.78
| Skype Web SDK Production| 0.4.275 |  9/6/16 |  0.4.269  |

The latest release includes improvements to group video calling in Chrome, the implementation of DevicesManager properties isMicrophoneEnabled and isCameraEnabled, and improvements to application telemetry, calling and overall reliability.

**Bugs fixed in the new public preview build:**

- Fix SfB native client and Microsoft Edge web client do not see remote video from Chrome client
- Implement isMicrophoneEnabled and isCameraEnabled for pluginless calling scenarios
- Delete minimum telemetry data necessary collectOII disabled
- Fix TypeError when stop is called while receiving an incoming call

---
## Skype Web SDK Version Update 8/30/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.57.70 | 8/30/16 |1.56.78
| Skype Web SDK Preview    | 0.4.275      |  8/30/16| 0.4.269
| Conversation Control Production | 1.56.78    |   8/30/16|1.55.101
| Skype Web SDK Production| 0.4.269 |  8/30/16 |  0.4.262  |

The latest release includes improvements to application telemetry and a fix for an audio bug that can arise when a p2p conversation is escalated to a group conversation.

**Bugs fixed in the new public preview build:**

- Add telemetry parameter to indicate whether sign in is online or onprem
- After escalating a call to group call mute/unmute feature/button no longer stops working for escalatee

---
## Skype Web SDK Version Update 8/23/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.56.78 | 8/23/16 |1.55.101
| Skype Web SDK Preview    | 0.4.269      |  8/23/16| 0.4.262
| Conversation Control Production | 1.55.101    |   8/23/16|1.54.107
| Skype Web SDK Production| 0.4.262 |  8/23/16 |  0.4.256 |

The latest release includes improvements to video calls in Edge, allows AV calls in Safari to proceed after the plugin is installed without refreshing, and allows video calls to continue when the video container is nulled and restored.

**Bugs fixed in the new public preview build:**

- A/V plugin installation flow now completes successfully on Safari
- During Edge audio calls, self video is no longer turned on automatically when remote turns on video
- Video no longer dropped after nulling and restoring the video container during a video call

---
## Skype Web SDK Version Update 8/16/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.55.101 | 8/16/16 |1.54.107
| Skype Web SDK Preview    | 0.4.262      |  8/16/16|0.4.256
| Conversation Control Production | 1.54.107     |   8/16/16|1.53.59
| Skype Web SDK Production| 0.4.256 |  8/16/16 |  0.4.250 |

The latest release includes fixes for activity items and improvements to video calls in Edge.

**Bugs fixed in the new public preview build:**

- Remove duplicate call ended activity items in group calls escalated from p2p
- Fix certain behavioral bugs of p2p video calls in Edge

---
## Skype Web SDK Version Update 8/9/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.54.107 | 8/9/16 |1.53.59
| Skype Web SDK Preview    | 0.4.256      |  8/9/16|0.4.250
| Conversation Control Production | 1.53.59     |   8/9/16|1.52.79
| Skype Web SDK Production| 0.4.250 |  8/9/16 |  0.4.245 |

This release includes improvements to video calls in Edge including support for multiple remote media streams in a group video call. In addition, it includes a fix to prevent IMs sent rapidly from being sent out of order, a fix for duplicate activity items in certain audio call scenarios, and improvements to our telemetry for AV calls.

**Bugs fixed in the new public preview build:**

-  Duplicate CallStarted and CallEnded activity items if caller cancels and then connects
-  IMs sent in quick succession often posted of order
-  Implement multiple media streams in order to be able to view more than one remote participant's video at once
-  Self video state after escalation unreliable

---
## Skype Web SDK Version Update 8/2/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.53.59 | 8/2/16 |1.52.79
| Skype Web SDK Preview    | 0.4.250      |  8/2/16|0.4.245
| Conversation Control Production | 1.52.79     |   8/2/16|1.51.69
| Skype Web SDK Production| 0.4.245 |  8/2/16 |  0.4.239 |

The latest release includes changes to ensure that chat and video modalities are present in ‘receive’ mode in conversations even if the user has not started them explicitly and to improve our telemetry in the case of failed calls. In addition, it includes improvements to app reliability in the case of refreshing a single tab app.

**Bugs fixed in the new public preview build:**

-  Remote video is shown only if self video is on in a meeting
-  Edge user has to click twice to establish a P2P call after user ignored the call the first time
-  Adding more data to telemetry to debug call failures

---
## Skype Web SDK Version Update 7/27/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| :-----:|:----------:|
| Conversation Control Preview     | 1.52.79 | 7/27/16 |1.51.69
| Skype Web SDK Preview    | 0.4.245      |  7/27/16|0.4.239
| Conversation Control Production | 1.51.69     |   7/27/16|1.50.51
| Skype Web SDK Production| 0.4.239 |  7/27/16 |  0.4.232 |

The latest release includes fixes for incorrect behavior relating to audio calls when multiple tabs are active, improves application reliability 
after making and ending multiple A/V calls, and enables basic group video functionality for video calls in Edge.

**Bugs fixed in the new public preview build:**
 
-  Incoming call toast doesn't cancel if accepted in another tab.
-  Receive an incoming call on tab 1. Tab 2 then shows a missed call item.
-  Conference video not working in Edge
 
---
