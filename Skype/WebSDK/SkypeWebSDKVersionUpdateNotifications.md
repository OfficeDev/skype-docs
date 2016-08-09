# Skype Web SDK Version Updates

## Skype Web SDK Version Update 8/9/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| -----:|----------:|
| Conversation Control DF     | 1.54.107 | 8/9/16 |1.53.59
| Web SDK DF    | 0.4.256      |  8/9/16|0.4.250
| Conversation Control Prod | 1.53.59     |   8/9/16|1.52.79
| Web SDK Prod| 0.4.250 |  8/9/16 |  0.4.245 |

**Bugs fixed in the new dogfood build:**

- **Bug 631786**: [jLync] Duplicate CallStarted and CallEnded activity items if caller cancels and then connects
- **Bug 491034**: IMs sent in quick succession often posted of order
- **Bug 632258**: [jLync] Implement multiple media streams in order to be able to view more than one remote participant's video at once
- **Bug 636852**: [jLync] Self video state messed up after escalation - streaming without appearing to stream

This release includes improvements to ORTC video reliability including support for multiple remote media streams in a group video call and a bug where video was streaming after escalation without it appearing so for the user. In addition, it includes a fix to prevent IMs sent rapidly from being sent out of order, a fix for duplicate activity items in certain audio call cases, and improvements to our telemetry for AV calls.

---
## Skype Web SDK Version Update 8/2/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| -----:|----------:|
| Conversation Control DF     | 1.53.59 | 8/2/16 |1.52.79
| Web SDK DF    | 0.4.250      |  8/2/16|0.4.245
| Conversation Control Prod | 1.52.79     |   8/2/16|1.51.69
| Web SDK Prod| 0.4.245 |  8/2/16 |  0.4.239 |

**Bugs fixed in the new dogfood build:**

- **Bug 575701**: [SWX] During conference calls IM modalities is not added, if someone sends a text no toast comes and IM is missed by all participants
- **Bug 550988**: [Plugin] Remote video is shown only if self video is on in a meeting
- **Bug 622759**: [jLync] Edge user has to click twice to establish a P2P call after user ignored the call the first time
- **Bug 621414**: [jLync] monitor the POST of diagnostics in telemetry
- **Bug 621477**: [jLync] adding more data to telemetry to debug call failures

The latest release includes changes to ensure that chat and video modalities are present in ‘receive’ mode in conversations even if the user has not started them explicitly, to prevent property changed events from firing when the reason is reset but value is unchanged, and to improve our telemetry in the case of failed calls. In addition, it includes improvements to app reliability in the case of refreshing a single tab app.

---
## Skype Web SDK Version Update 7/27/16

| Product        | New Version           | Last Updated  |Previous Version
| ------------- |:-------------:| -----:|----------:|
| Conversation Control DF     | 1.52.79 | 7/27/16 |1.51.69
| Web SDK DF    | 0.4.245      |  7/27/16|0.4.239
| Conversation Control Prod | 1.51.69     |   7/27/16|1.50.51
| Web SDK Prod| 0.4.239 |  7/27/16 |  0.4.232 |

**Bugs fixed in the new dogfood build:**
 
- **Bug 439898**: [Web SDK] Multi tab - incoming call toast doesn't cancel if accepted in another tab.
- **Bug 614711**: [Web SDK] Multi tab - Receive an incoming call on tab 1. Tab 2 then shows a missed call item.
- **Bug 619231**: [Web SDK][ORTC] Track and clean up non-responsive ("zombie") AudioVideoSession.
- **Bug 614938**: All telemetry properties empty when the "collect organizational identifiable information" (OII) feature is disabled
- **Bug 494271**: [ORTC] Conference video not working (Implement isVideoOn and isStarted)
 
The latest release includes fixes for incorrect behavior relating to audio calls when multiple tabs are active, improves application reliability 
after making and ending multiple A/V calls, and enables basic group video functionality for video calls in Edge.

---