---
title: In-call QoE algorithm and throttling
ms.assetid: 4f5c2ee2-2184-486b-bc63-5a30589cca8a
---


# In-call QoE algorithm and throttling
Learn about the algorithm that manages the number of InCallQoE messages so that they don't overload the network.
 **Last modified:** February 23, 2017
  
    
    

 * **Applies to:** Lync Server 2013 | Skype for Business 2015 
 
This section describes the algorithm used for throttling InCallQoE messages sent by a client regardless of the network conditions.
  
    
    


## In-call QoE algorithm and throttling

Learn about the algorithm that manages the number of InCallQoE messages so that they don't overload the network.
  
    
    
This section describes the algorithm used for throttling InCallQoE messages sent by a client regardless of the network conditions. 
  
    
    
The triggering of InCallQuality messages is based on an algorithm in the client media stack, which detects changes to call quality and raises various events in response. These events are raised separately for different call quality categories for each media stream, such that creating an InCallQuality message for each event can potentially flood a network. 
  
    
    
To prevent this, a throttling algorithm has been implemented, to ensure that InCallQuality messages are not sent with every occurrence of an event. Rather, the throttling period is the duration between at least two of these messages. A following message might report multiple quality changes to the stream since the previous message. The SDN Manager will determine the overall state of the stream from the values and history provided. 
  
    
    
Even without any quality issues, the client will send an InCallUpdate message after the first throttling period, which in the above example is set to 35 sec (the InCalQoSIntervalSeconds is set to 35 sec). InCallQoE updates will be sent when any one of the following conditions are true: 
  
    
    

- After the first "InCallQoSPeriodInSec" has passed
    
  
- After an AV call was de-escalated to an audio only call. (When InCallQoE isn't enabled, whenever an AV call is de-escalated to an audio call the video stream QoE metrics are lost)
    
  
- After 35 seconds has passed since the previous quality update and a call quality change event was raised during this period 
    
  
-  [Configuring InCallQoE Messages](configuring-incallqoe-messages.md)
    
  

## Additional resources
<a name="bk_addresources"> </a>


-  [Configuring InCallQoE Messages](configuring-incallqoe-messages.md)
    
  

