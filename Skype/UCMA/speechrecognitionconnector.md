---
title: SpeechRecognitionConnector
TOCTitle: SpeechRecognitionConnector
ms:assetid: a00c8aac-d040-4136-90ce-e65059cf3890
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466036(v=office.16)
ms:contentKeyID: 65239976
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# SpeechRecognitionConnector


**Applies to**: Skype for Business 2015

A [SpeechRecognitionConnector](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechrecognitionconnector?view=ucma-api) instance can transfer audio data from a **SpeechRecognitionEngine** instance (in the **Microsoft.Speech** namespace) to an [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) instance that is attached to the **SpeechRecognitionConnector** instance.

The stream of audio data produced by a **SpeechRecognitionConnector** instance is compatible with that produced by the **Microsoft.Speech** namespace in Microsoft .NET Framework 3.0 and later. One such stream is provided every time [Start](https://msdn.microsoft.com/en-us/library/hh349784\(v=office.16\)) is called on the **SpeechRecognitionConnector**. The stream contains the audio up to the point at which [Stop](https://msdn.microsoft.com/en-us/library/hh384349\(v=office.16\)) is called on the connector. Calling **Start** again then generates a new stream for the new interval.

Before an application can call the **Start** method on a **SpeechRecognitionConnector** instance, an **AudioVideoFlow** instance must have been previously attached to the connector, and the State of the **AudioVideoFlow** instance must be **Active**. If both conditions are not met, **Start** throws an exception.

## Example

The following procedure describes the steps involved in creating and using a [SpeechRecognitionConnector](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.speechrecognitionconnector?view=ucma-api) instance.

### To use SpeechRecognitionConnector

1.  Create an instance of this class.
    
    ```csharp
    SpeechRecognitionConnector recoConn = new SpeechRecognitionConnector();
    ```

2.  Attach an [AudioVideoFlow](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.audiovideo.audiovideoflow?view=ucma-api) instance (assumed to have been previously constructed and in the **Active** state).
    
    ```csharp
    recoConn.AttachFlow(AudioVideoFlow);
    ```

3.  Start the connector to initiate speech recognition.
    
    ```csharp
    SpeechRecognitionStream recoStream = SpeechRecognitionConnector.Start();
    ```
    
    This step alerts the **AudioVideoFlow** to the existence of the connector and supplies the application with a stream of audio data that begins at roughly the time that **Start** was called. **Start** throws an exception if another receiving device is already active. The stream that is supplied is of type [SpeechRecognitionStream](https://msdn.microsoft.com/en-us/library/hh349357\(v=office.16\)), a type that inherits from **Stream**.

4.  When the application is finished with its speech recognition, it calls **Stop**.
    
    ```csharp
    recoConn.Stop();
    ```
    
    The stream remains open-ended (that is, a call to [Read](https://msdn.microsoft.com/en-us/library/hh384278\(v=office.16\)) will block when there are no media samples returned by the connector, rather than return "End of Stream") until the application calls **Stop**.

5.  The application owns the stream as soon as it is returned by **Start**, so when it is finished with the stream, it should call **Dispose**.
    
    ```csharp
    recoStream.Dispose();
    ```
    
    This will return any buffers remaining in the stream to the **SpeechRecognitionConnector** connector.

6.  When the application is completely finished with the connector itself, it should call **Dispose** to free all unmanaged buffers.
    
    ```csharp
    recoConn.Dispose();
    ```

