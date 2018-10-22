---
title: VoiceXMLSample (QuickStart)
TOCTitle: VoiceXMLSample (QuickStart)
ms:assetid: e4b31dcb-3049-47b7-bdbe-a3cdc365b2b5
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454832(v=office.16)
ms:contentKeyID: 65240104
ms.date: 07/27/2015
mtps_version: v=office.16
---

# VoiceXMLSample (QuickStart)

**Applies to**: Skype for Business 2015

Sample name: VoiceXMLSample

Sample location: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\QuickStarts\\VoiceXMLSample

## Description

This sample shows how to run a Skype for Business Server 2015 VoiceXML Browser object against a VoiceXML page and a Microsoft Unified Communications Managed API 5.0 [AudioVideoCall](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideocall?view=ucma-api) instance. The major steps are as follows.

1.  Set up the UCMA 5.0 objects ([CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) and endpoints).

2.  Instantiate a [Browser](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.voicexml.browser?view=ucma-voice) object.

3.  Subscribe to **Browser** events.

4.  Set a handler for the **CallReceived()** event.

5.  Inside the **CallReceived** event handler, do the following:
    
    1.  Subscribe to the **StateChanged** event on the call object.
    
    2.  Start a session to run the call against the VoiceXML page, by calling **Browser.RunAsync**(callObject).

As events are raised by the **Browser** object, they are recorded in the Event Notification text box.

## Troubleshooting

- **Problem** - Files referenced from within a VoiceXML document might result in the browser object exiting with error.badfetch.
    
  **Solution** - To reference files from within a VoiceXML document, insure that one of the following is true:
    
  - The base directory is set using an **xml:base** attribute inside the \<**vxml** ..\> element. For example, xml:base="C:\\Grammars", and ensure that the referenced files are in that directory.
    
  - The fully qualified path to the file is given.
    
  - The file is referenced through a Web server.

- **Problem** - Cannot access VoiceXML documents or other files from a Web server.
    
  **Solution**: To access VoiceXml documents, grammar files, or other files from a Web server do the following.:
    
  - Using IIS Manager, set up a virtual directory and copy the files to that directory.
    
  - Using IIS Manager, set MIME types for all file extensions used. File extensions and the associated MIME types used in typical VoiceXML applications include .vxml (text/xml), .grxml (text/xml), .cfg (application/srgs+xml), and .ssml (application/ssml+xml).

## Notes

- There are two VoiceXML files and one grammar file included in the subfolder VoiceXMLSampleData below the folder of the main source files for use with the sample. You can add other files to this folder to use in the sample by using a relative path such as VoiceXMLSampleData\\filename.vxml. After adding files to the folder, be sure to add them to the project by clicking on the VoiceXMLSampleData node in Solution Explorer and right clicking on the files to be added. Also, make sure that the "Copy to Output Directory" property for each added file is set to "Copy always".

- As this sample illustrates, the VoiceXML **Browser** object can be used multiple times (sessions) as long as it is bound to a UCMA **AudioVideoCall** object for each session. To reuse a browser instance after a session completes, the application must call **SetAudioVideoCall** to bind the **AudioVideoCall** object to the **Browser** object.

- Unlike many VoiceXML interpreters in the industry, the VoiceXML **Browser** object that ships in the Microsoft Speech Platform and UCMA 4.0 does not automatically disconnect the associated call on session completion unless the VoiceXML interpreter processes a \<**disconnect**\> or \<**transfer**\> element. Thus, an application should check the state of the **AudioVideoCall** object on session completion and take appropriate action if it is still active, as noted in the comments in this sample.

