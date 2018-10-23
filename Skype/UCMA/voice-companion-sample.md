---
title: Voice companion (sample)
TOCTitle: Voice companion (sample)
ms:assetid: 4aa901e9-7de1-41cd-8978-49c832f15c07
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454820(v=office.16)
ms:contentKeyID: 65240107
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Voice companion (sample)

**Applies to**: Skype for Business 2015

**Sample name**: VoiceCompanion

**Sample location**: %ProgramFiles%\\Microsoft UCMA 5.0\\SDK\\Core\\Sample Applications\\Reference\\VoiceCompanion

## Description

The VoiceCompanion application provides PSTN phone users with access to unified communications capabilities such as conferencing, presence, and contact lists.

The following actions describe the main scenario:

1.  A PSTN phone user dials the application phone number.

2.  The application recognizes the phone number and prompts the user to enter his specific pin number.

3.  If successfully authenticated, the user is presented with several voice services.

For more information about provisioning trusted applications and endpoints in Skype for Business Server 2015, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md), as well as [General application activation](general-application-activation.md) and [Activating a manually-provisioned application](activating-a-manually-provisioned-application.md).

This sample demonstrates how to create a **CollaborationPlatform** instance using the **ServerPlatformSettings** constructor, establish a single **ApplicationEndpoint**, and monitor the state changes on the endpoint.

## Available voice services

- Contact list service
    
  Using this service, the user can be connected to users from his/her contact list via speech recognition. Before connecting the user to the contact, Voice Companion checks the contact presence status. If not available, the user is prompted with the option of setting a callback when the contact becomes available.

- International number dial-up service.
    
  The PSTN phone user can dial an international phone number through her corporate network thereby potentially leveraging cheaper rates for business calls.

- Conferencing service
    
  The PSTN phone user can setup an ad-hoc conference and invite users to this conference either by selecting them from her contact list or entering a phone number. The PSTN user can do so without interrupting the conference (the PSTN user is isolated from the conference while being prompted).

## Setting up the application

An application with ID "urn:application:voicecompanion" needs to be configured in the topology. For more information about provisioning trusted applications and endpoints, see [Activating a UCMA 5.0 trusted application](activating-a-ucma-5-0-trusted-application.md), as well as [General application activation](general-application-activation.md) and [Activating a manually-provisioned application](activating-a-manually-provisioned-application.md).

### PowerShell commands

The following PowerShell commands are relevant for setting up the application:

- New-CsTrustedApplication
    
  Create a new application in the topology. You need to assign a listening port for the application using this command.

- New-CsTrustedApplicationPool
    
  Create a pool for this application. This is useful if you intend to run multiple application instances as the demand increases.

- New-CsTrustedApplicationComputer
    
  Create a computer in the pool created by the previous cmdlet.

- New-CsTrustedApplicationEndpoint
    
  Create the application endpoint for the application. Note that the same instance of the application endpoint can be run from different computers. However, any number of endpoints could be assigned to this application to allow different numbers to be used for contacting the application.

### Firewall configuration

The application must listen for incoming connections from . This requires the port assigned to the application using the **New-CsTrustedApplication** cmdlet. The port specified in this cmdlet should be opened on the application computer by creating an inbound rule in the firewall configuration.

### Reverse number lookup

This sample program uses an XML file that stores the `<callerid, sipuri>` mappings. Initially, this file contains a sample entry. The reason for this design is to make user experience simpler by not having to enter the telephone number of the corresponding user. To simplify administration of this file, the sample uses subscription notification as a hint for adding the telephone numbers assigned to the user who added the application in his/her contact list. Users can also use IM calls with the application to add, remove, or list the telephone numbers. 

The application will update the file periodically (every hour) and when the application is shut down. If the application must be run on multiple computers, this file should be stored in a database so that any application can access it. This can be easily done by adding the required code to the sample.

### Logging

The application supports a simple logging mechanism. See Program.cs to see how it is used.

### User provisioning

To use the application, users must know their PINs to authenticate themselves. Normally, a website is used for setting up the PIN. Users can also use Microsoft Skype for Business 2015 to update their PIN. An administrator can use PowerShell to update the PIN as well, as in the following example: 

`Set-CSClientPin -identity sip:user1@contoso.com -Pin 23567` 

It is possible to add the ability for a user to set his or her PIN by using an IM call with the application. The application can use **UserEndpoint** to update the PIN. It is very important for the application to authenticate the user before updating the PIN.

## Program design

This section describes the basic infrastructure before the architecture is described. Note that this application is written with the goal of making the code readable and flexible. As a result, the application uses a simple implementation of utility classes to make it possible to manage asynchronous operations with minimal repetition of similar try-catch blocks throughout the code.

### ComponentBase

Every component in the application derives from the **ComponentBase** base class, which provides a mechanism to start and stop the component. All derived classes must implement **StartupCore** and **ShutdownCore** methods. The **CompleteStartup** and **CompleteShutdown** methods are provided by default for the class to indicate the corresponding operations are complete. It is the responsibility of the derived classes to call these methods when the startup or shutdown operations are complete. Every component can be given a unique name for identification purpose in log files.

### AsyncResult

There are a couple of classes that provide asynchronous implementation that are primarily used by the component base. Derived classes need not use them. Instead, it is much easier to use the **AsyncTask** class, which is described next.

### AsyncTask

The **AsyncTask** class represents an asynchronous task, such as establishing an endpoint, subscribing to presence, or joining a conference. The actual work is performed by a method with signature given by **AsyncTaskMethod**, and which is similar to the **AsyncCallback** class in the .NET framework. This method takes the owner task and a state. When the method completes the operation, it should complete the owner task.

A task can be further divided into smaller steps. This makes it easier to organize the program by focusing on macro tasks and letting each task manage smaller steps. For example, establishing an endpoint involves calling **BeginEstablish**, and when the **BeginEstablish** callback is invoked, calling **EndEstablish**. Execution of any step must handle exceptions properly. To make this simpler to deal with, the **AsyncTask** class provides **DoOneStep** and **DoFinalStep** methods. Each of them takes a delegate without parameters that will perform the step. **DoOneStep** will NOT complete the task after calling the method. **DoFinalStep** WILL complete the task after calling the method. Thus, an asynchronous task can use several **DoOneStep** methods followed by one final **DoFinalStep** method. Note that if-then-else syntax can be easily added to an **AsyncTask** implementation but this was not needed in this sample implementation.

### AsyncTaskSequence

A program normally must perform several asynchronous tasks to accomplish a given scenario. These tasks can be executed serially for some scenarios or in parallel for others. For example, an endpoint must be established before it can be joined to a conference; hence this situation calls for sequential execution. When the application is shut down, it can terminate all endpoints in parallel or terminate all customer sessions in parallel because these are independent tasks. The framework provides **AsyncTaskSequenceSerial** and **AsyncTaskSequenceParallel** classes to accomplish these sequences. For more information about these concepts, see the sample application.

The use of the **AsyncTask** and **AsyncTaskSequence** classes makes the code much simpler to follow. It should take only a few hours to learn and become comfortable with these concepts. These concepts provide a lot of flexibility to easily rearrange the way different scenarios are handled.

## Architecture

The application uses auto-provisioning to bootstrap itself based on the topology settings. The application platform is created first, that encapsulates the collaboration platform. After the application platform is created, an application front end for each **ApplicationEndpoint** in the topology is created. The application front end encapsulates a UCMA **ApplicationEndpoint**. The endpoint is responsible for handling incoming calls.

### AppFrontEnd

When the application front end is started, it will start the music provider component that can play music files for users and the callback manager that is responsible for maintaining the callback requests setup by the customers. Finally, the application front end will establish the application endpoint.

### Customer session

Each incoming audio call or a callback session is handled by the customer session. The incoming call is used to determine the caller id (phone number) so that the application can determine the sip uri of the user. This is done by using reverse number lookup from a dictionary that is populated by loading the rnl.xml file. If the mapping cannot be determined, the call is rejected. Else, the user is asked to authenticate by entering PIN. If the PIN validates the user, the user is given options.

### Service hub

For speech or DTMF recognition of the customer while in a conference, it is necessary to create a pipe between the conference and the customer in the conference. This is done by creating a service channel (an audio call for the purpose of intercepting speech or DTMF commands from the user). The service hub is a trusted conversation created by the application endpoint. The application joins an *ad hoc* conference. In addition, it creates a primary call (first class participation in the conference is needed by the AV MCU to carry out some trusted operations such as muting all or removing the user from the default mix) followed by a service channel call. The service channel is wired with the customer.

### Customer endpoint

The application is written to optimize the use of **UserEndpoint** objects to serve the customer. Only one endpoint is used so that users can use other endpoints (out of eight allowed endpoints per user in Skype for Business Server 2015) for their own use. The customer session uses the endpoint to publish online presence, followed by "InACall" presence. In addition, the user endpoint is used to create a conversation for joining the conference. This makes is possible to issue commands such as conference invitations to other contacts using the identity of the customer.

### Customer contact manager

This component is used to determine the list of contacts of the customer and their presence. For simplicity, it does not handle contacts added or removed dynamically (because the user is on the phone anyway).

### Voice services

There are there voice services available for the customer:

- Get Contact Service
    
  This service looks up the presence of the contact and offers a callback option to the customer if the contact is not immediately available.

- International Dial-in Service
    
  This service provides a way to connect the customer with another phone number. When the call is business related, this service makes it possible to make the call as if had been made from within the corporate network. Note that the user identity is used to make the call.

- Conference Service
    
  This service is used to make a conference call. The customer can bring a number of contacts and phone numbers into the conference by pressing \#1 and \#2 to perform these tasks. While performing these tasks, the application has the ability to interact only with the customer (others will not hear any of the interactions with the application).

### Workflows

There are various workflows used in the application to accomplish the user experience. The main workflow offers the three choices listed in the previous section. The authentication workflow is used initially to welcome the customer and ask for the PIN. The callback workflow is used to make the call to the customer to connect to the contact who just came online. There are many other workflows that can be seen from the application solution.

## Known issues

The Voice Companion sample has the following known issues:

- Voice Companion throws a **NullReferenceException** if it receives a phone call from a user whose phone number is not in the reverse-number lookup list.
    
  Workaround: Configure Voice Companion with as many phone numbers as feasible.

- Voice Companion throws an **ArgumentException** if it receives an instant message from a user whose IM URI is not in the reverse-number lookup list.
    
  Workaround: Configure Voice Companion with as many IM URIs as feasible.

- Voice Companion is unable to add a URI and then remove it, in some circumstances.
    
  If a user adds a URI to the reverse-number lookup list, and then removes a logically equivalent but not literally equivalent URI, Voice Companion is unable to complete the operation. For example, if a logged in user sends an instant message to Voice Companion with the text `add tel:+14255551212` and then later sends this text `remove 4255551212`, Voice Companion responds with this message: "Telephone number does not exist or you do not own the telephone number." Voice Companion is unable to determine that these two telephone numbers are the same.
    
  Workaround: Use literally equivalent URIs.

## Disclaimer

This is just a sample and is not meant to be deployed as such for business use. There are many improvements possible in the application before it can be deployed for a real production. The purpose of this sample is to illustrate the usage and power of UCMA and not for the purpose of running an application for real customer use.

