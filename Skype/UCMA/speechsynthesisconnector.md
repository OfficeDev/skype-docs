---
title: SpeechSynthesisConnector
TOCTitle: SpeechSynthesisConnector
ms:assetid: 628b4dfe-7b0c-4588-a976-879187ac3b8b
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466042(v=office.16)
ms:contentKeyID: 65239977
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# SpeechSynthesisConnector


_**Applies to:** Skype for Business 2015_

A [SpeechSynthesisConnector](https://msdn.microsoft.com/en-us/library/hh349773\(v=office.16\)) instance provides a **Stream** interface to feed audio data to an attached [AudioVideoFlow](https://msdn.microsoft.com/en-us/library/hh383533\(v=office.16\)) instance. The **Stream** can be given to a **SpeechSynthesizer** object (in the **Microsoft.Speech.Synthesis** namespace) as an output destination, in order to play text-to-speech (TTS) over the wire.

Before an application can call the [Start](https://msdn.microsoft.com/en-us/library/hh383202\(v=office.16\)) method on a **SpeechSynthesisConnector** instance, an **AudioVideoFlow** instance must have been previously attached to the connector, and the **State** of the **AudioVideoFlow** instance must be **Active**. If both conditions are not met, **Start** throws an exception.

## Example

The following procedure describes the steps involved in creating and using a [SpeechSynthesisConnector](https://msdn.microsoft.com/en-us/library/hh349773\(v=office.16\)) instance.

### To use SpeechSynthesisConnector

1.  The application uses the constructor to create a **SpeechSynthesisConnector** instance.
    
    ``` csharp
    SpeechSynthesisConnector synthConnector = new SpeechSynthesisConnector();
    ```

2.  The application then calls [AttachFlow](https://msdn.microsoft.com/en-us/library/hh348319\(v=office.16\)) to bind the **SpeechSynthesisConnector** instance to an **AudioVideoFlow** instance. In this code example, it is assumed that *AVFlow* has already been constructed, and is in the **Active** state.
    
    ``` csharp
    synthConnector.AttachFlow(AVFlow);
    ```

3.  The application then calls **SetOutputToAudioStream(Stream, SpeechAudioFormatInfo)** on the **SpeechSynthesizer** instance (assumed to have been previously constructed) to identify the stream to use as well as how the audio data will be formatted.
    
    ``` csharp
    synthesizer.SetOutputToAudioStream(synthConnector.Stream, AudioFormat);
    ```

4.  The application then calls **Start** on a **SpeechSynthesisConnector** instance, causing this device to start.
    
    ``` csharp
    synthConnector.Start()
    ```

5.  The application can then use **Speak** to utter a prompt.
    
    ``` csharp
    synthesizer.Speak(prompt);
    ```

6.  When the **Speak** operation is complete or after the **SpeakCompleted** event is raised, the application uses **Stop**, to stop the **SpeechSynthesisConnector** instance.
    
    ``` csharp
    synthConnector.Stop();
    ```
    
    If another prompt is to be played, call **Start** again to restart the **SpeechSynthesisConnector**, and then call **Speak** again.

