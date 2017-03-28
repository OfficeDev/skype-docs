---
title: SDN Interface Bandwidth Estimates
ms.prod: SKYPE
ms.assetid: 35458e8f-29ee-4bc5-b2bc-51bbe390b8d5
---


# SDN Interface Bandwidth Estimates
Provides bandwidth estimate data for Skype for Business SDN Interface. 
 **Last modified:** January 28, 2016
  
    
    

**Applies to:** Lync Server 2013 | Skype for Business 2015
 
Skype for Business SDN Interface, versions 2.2 and later, provides some estimates on the bandwidth that will be used during a call for each stream as a consequence of the codecs being used. The following table shows the raw estimates for each codec. The Bandwidth xml tag contains an exponential running weighted average based on the codecs that might be used during the call. This Bandwidth tag shows 0 bandwidth if the given stream direction is inactive. 
  
    
    


## Bandwidth Estimates



|**Codec**|**Media**|**Typical**|**Maximum**|
|:-----|:-----|:-----|:-----|
|X-MSRTA/16000 |audio |49600 |91000 |
|X-MSRTA/8000 |audio |33100 |56600 |
|G722/8000 |audio |85400 |164600 |
|G722/8000/2 |audio |108400 |228600 |
|G722/16000/2 |audio |108400 |228600 |
|G7221/16000 |audio |44800 |81000 |
|PCMU/8000 |audio |83200 |161000 |
|PCMA/8000 |audio |83200 |161000 |
|SIREN/16000 |audio |39300 |68600 |
|SILK/16000 |audio |47700 |105000 |
|SILKNARROW/8000 |audio |31200 |59000 |
|SILKWIDE/16000 |audio |47700 |105000 |
|SILK/8000 |audio |31200 |59000 |
|X-ULPFECUC/90000 |audio |29000 |64000 |
|AAL2-G726-32/8000 |audio |38600 |62000 |
|X-RTVC1/90000 |video |500000 |2510000 |
|X-H264UC/90000 |video |500000 |4010000 |
|H264 |video |500000 |4010000 |
|X-ULPFECUC/90000 |video |29000 |64000 |
|X-DATA/90000 |applicationsharing |439000 |943000 |
   

