---
title: Trusted service discovery
TOCTitle: Trusted service discovery
ms:assetid: e9d21856-ce1b-4dfa-8817-fc5e604e974b
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465967(v=office.16)
ms:contentKeyID: 65239854
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Trusted service discovery


_**Applies to:** Skype for Business 2015_

An important feature of Microsoft Unified Communications Managed API 5.0 is the ability to create distributed and trusted custom Communications-Enabled business applications that can discover and then exchange information with one another at runtime, regardless of the deployment specifics. As an example, this ability to discover a remote trusted service and subsequently establish an inter-system communication channel allows a developer to create a distributed system consisting of four distinct services:

  - Service 1—A Web application that receives click-to-call or Web Chat requests from a Web user visiting an intranet or extranet site. These requests target the three following remote trusted services.

  - Service 2—An Instant Messaging automaton whose unique identity is, for example, "urn:application:contosoimbot".

  - Service 3—An Interactive Voice Response system whose unique identity is, for example, "urn:application:contosoivr".

  - Service 4—A Contact Center whose unique identity is, for example, "urn:application:contosocontactcenter".

In this example, the developer designs Service 1 so that the respective identities of Service 2, Service 3, and Service 4 are known by Service 1 in advance. Each service identity (Unique Resource Name (URN)) is supplied to UCMA 5.0 at runtime at the initialization of Service 1. The API will then attempt to resolve the identity to a SIP URI that can be used to address the relevant service.

In general, multiple instances of a given service are deployed in an application pool for load balancing and fail-over reasons. The SIP URI is a trusted GRUU returned by UCMA 5.0, that conceptually represents the service as an application pool rather than as a specific instance of the service. This implies the following points:

  - Service 2, Service 3, and Service 4 must be ready to receive incoming messages from Service 1 on any of their respective instances.

  - Service 2, Service 3, and Service 4 will need to use a UCMA 5.0 Default Routing Endpoint to consume the messages from Service 1.

  - Service 2, Service 3, and Service 4 will need to validate that the incoming request from Service 1 has a well-known content-type (chosen by the developer) and that its Contact header URI is a trusted GRUU (a trustworthy source).

By following these recommendations, Service 1 can have a proprietary convention with Service 2, Service 3, and Service 4 for Service 1 to discover their deployment-specific provisioning information, and proxy the communication requests coming from Web users to the appropriate endpoint of the service.

## Service discovery example code

For service discovery, first get the remote application’s pool GRUU using the application’s application ID.

``` csharp
string applicationGruu;
TopologyConfiguration topologyConfig = _collabPlatform.TopologyConfiguration;
ApplicationTopologyData applicationTopologyData = _collabPlatform.ApplicationTopologyData;
   
// Get the topology data for the specified application.
Collection<ApplicationTopologyData> applicationTopologyDataCollection = topologyConfig.GetApplicationTopologyData(applicationId);
if (applicationTopologyDataCollection.Count != 0)
{
  // Get the application instance that is deployed on the same site this application is on.
  foreach (ApplicationTopologyData appData in applicationTopologyDataCollection)
  {
    if (appData.SiteId.Equals(wcfApplicationTopologyData.SiteId))
    {
      applicationGruu = appData.PoolGruu;
      break;
    }
  }
 
}
```

Note that if the remote application has not yet been deployed the above code will not return the GRUU. You can choose to retry or gracefully terminate at this point. After the GRUU is retrieved you can exchange data with the remote application if the remote application has a default routing endpoint. The data generally will be in your own proprietary format. For example, it might be the list of running application endpoints.

Send a service request to get the data from the remote application.

``` csharp
RealTimeEndpoint innerEndpoint = _localEndpoint.InnerEndpoint;
SendMessageOptions options = new SendMessageOptions();
options.ContentDescription = GetDiscoveryRequestContentDescription();
RealTimeAddress targetAddress = new RealTimeAddress(remoteApplicationPoolGruu);
innerEndpoint.BeginSendMessage(MessageType.Service,
                    targetAddress,
                    options,
                    this.ServiceRequestCompleted,
                    innerEndpoint/*state*/);
```

In the *ServiceRequestCompleted* callback, process the body in the response to retrieve the list of application endpoints, for example. Again, note that if the remote application is not started you might need to retry.

On the remote application, send a suitable response, such as the list of application endpoints in our example, when the service request with the predetermined content type is received. Because such a request is expected only from the trusted application, you should verify it before responding.

In the **MessageReceived** event handler on the default routing endpoint check the contact URI and send a response.

    // Go through the list of headers in the request to get the contact header.
    listOfHeaders.ForEach(header=> 
    {
    ..if (header.Name.Equals("Contact", StringComparison.OrdinalIgnoreCase))
      {
        // Check whether the contact address is a trusted GRUU and thus a legitimate source.
        if (_platform.TopologyConfiguration.IsTrusted(args.Participant.ContactUri))
        {
          // Check the verb and content-type of the incoming request
          if (args.MessageType == MessageType.Service 
            && args.ContentType != null
            && args.ContentType.Equals(GetDiscoveryRequestContentDescription()))
          {
            // This is a legitimate request. Send the response.
            String discoveryResponse;
                  
            // Build the response from the list of application endpoints that the application supports.
            args.SendResponse(ResponseCode.Success,
                GetDiscoveryResponseContentDescription(),
                discoveryResponse, 
                null);
     
          }
        }
      }
    });

