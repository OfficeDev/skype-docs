---
title: Reference topology with high availability and a single data center
TOCTitle: Reference topology with high availability and a single data center
ms:assetid: 4fd9ab0c-46e9-4d64-aa87-74e4dca802e4
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465972(v=office.16)
ms:contentKeyID: 65239888
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Reference topology with high availability and a single data center


_**Applies to:** Skype for Business 2015_

The reference topology with high availability and a single data center is designed for a small-to-medium size organization with one central site. The exact topology in the following diagram is for an organization of 15,000 users. Typically, the type of topology shown in the following diagram is recommended for organizations with 5,000 to 30,000 users, but it can support over 30,000 users.

Reference topology with high availability and a single data center

  
![MORG reference](images/Dn465972.MORG_Ref_Topology(Office.16).png "MORG reference")

  - **Active Directory deployment**   All Skype for Business Server 2015 deployments reside in a single Active Directory forest. For this topology, the customer has Skype for Business Server 2015 deployed in the child domain, retail.contoso.com.

  - **To accommodate more users, add more Front End Servers**   The exact topology in this diagram has two Front End Servers, so it supports up to 16,000 users. If you have a single central site and more users, you can simply add Front End Servers to the pool. One Front End pool can have as many as 10 servers, so a single Front End pool at a single site can support up to 80,000 users.
    
    However, the single site topology can support even more users by adding another Front End pool to the site. To support these extra users, you need to add only one additional Front End pool (that is, just one pool each of A/V Conferencing Servers, Edge Servers, and Directors are sufficient, although more servers may need to be added to these pools).

  - **A/V conferencing can be collocated in smaller organizations**   If this organization has 10,000 or fewer users, you could decide to collocate A/V Conferencing Services with the Front End pool, instead of deploying a separate A/V Conferencing pool. For organizations with 10,000 or more users, collocating these services is not recommended for performance reasons.

  - **Monitoring Server database options**   In this topology, the Monitoring Server is using the same pair of database servers that the Front End pool uses. A topology in which the Monitoring Server has its own database servers is also supported. In each case, we recommend a cluster of more than one database server, for high availability.

  - **High availability for all server pools**   In this example organization with 15,000 users, just one Director server, Edge Server and A/V Conferencing server would be sufficient for performance. However, there are pools of two servers of each type deployed to provide high availability for each feature.

  - **Branch Site deployment options**   The organization in this topology has Enterprise Voice deployed as their voice solution. Branch Site 1 does not have a resilient wide area network (WAN) link to the central site, so it has a Survivable Branch Appliance deployed to provide telephone service in case the WAN link to the central site goes down. Branch Site 2 however has a resilient WAN link, so only a public switched telephone network (PSTN) gateway is needed. The PSTN gateway deployed there supports media bypass, so no Mediation Server is needed at Branch Site B.

  - **DNS load balancing**   The Front End pool, Edge Server pool, and the Director pool have DNS load balancing for SIP traffic deployed. This eliminates the need for hardware load balancers for the internal interface of the Edge Servers, and significantly lessens the setup and maintenance of the hardware load balancers for the other pools, as the hardware load balancers are needed only for HTTP traffic.

  - **Exchange UM deployment** This reference topology includes an Exchange Unified Messaging (UM) Server, which runs Microsoft Exchange Server, not Skype for Business Server 2015. The Exchange UM routing functionality for Skype for Business Server 2015 runs on the Front End pool.

  - **Edge Servers recommended**   Although deploying an Edge Server is not required, it is recommended for any size of deployment. It is also recommended when external or federated access is required. You can maximize your Skype for Business Server 2015 investment by deploying an Edge Server to provide service to users currently outside your organization’s firewalls. The benefits include the following:
    
      - Your organization’s own users can use Skype for Business Server 2015 functionality, if they are working from home or are out on the road.
    
      - Your users can invite outside users to participate in meetings.
    
      - If you have a partner, vendor or customer organization that also uses Skype for Business Server 2015, you can form a federated relationship with that organization. Your Skype for Business Server 2015 deployment would then recognize users from that federated organization, leading to better collaboration.
    
      - Your users can exchange instant messages with users of public instant messaging (IM) services (including Windows Live, AOL, and Yahoo\!) and XMPP-based providers and servers (for example, Google Talk and Jabber). A separate license might be required for public IM connectivity with Windows Live, AOL, and Yahoo\!


> [!NOTE]
> <P>In Skype for Business Server 2015, UCMA applications should never be homed on the branch office appliance. Instead, they should be connected directly to the Skype for Business Server 2015 Front End computer or Front End pool.</P>


