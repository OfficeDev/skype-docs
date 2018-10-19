---
title: Reference topology with high availability and multiple data center
TOCTitle: Reference topology with high availability and multiple data center
ms:assetid: 1e63f4ec-823a-4c39-b124-ea7ee729bb71
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465973(v=office.16)
ms:contentKeyID: 65239899
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Reference topology with high availability and multiple data center


_**Applies to:** Skype for Business 2015_

The reference topology for multiple data centers is for any size of organization with more than one central site. The exact topology in the following diagram is for an organization of 70,000 users, with 40,000 users at Central Site A and 30,000 at Central Site B. The type of topology shown in this diagram can accommodate organizations with any number of users.

This topology is shown in multiple diagrams, with an overview first followed by detailed views of the central sites.

Overview of the reference topology for multiple data centers

  
![LORG overview](images/Dn465973.LORG_overview(Office.16).png "LORG overview")

Reference topology for multiple data centers: Detailed view of Central Site A

  
![Site A reference](images/Dn465973.LORG_SiteA_Ref_Topology(Office.16).png "Site A reference")

Reference topology for multiple data centers: Detailed view of Central Site B

  
![Site B reference](images/Dn465973.LORG_SiteB_Ref_Topology(Office.16).png "Site B reference")

Reference topology for multiple data centers: Detailed view of Central Site C

  
![Site C reference](images/Dn465973.LORG_SiteC_Ref_Topology(Office.16).png "Site C reference")

  - **Active Directory deployment**   All Skype for Business Server 2015 deployments reside in a single Active Directory forest. For this topology, the customer has Skype for Business Server 2015 deployed in two child domains, retail.contoso.com and manufacturing.contoso.com.

  - **To accommodate more users, add more Front End Servers**   The organization in this diagram has five Front End Servers at Central Site A (for 40,000 users), and four Front End Servers at Central Site B (for 30,000 users). If either site needs to accommodate more users, you can simply add Front End Servers to the pool at that site. One Front End pool can have as many as 10 servers, so a single Front End pool at a single site can support up to 80,000 users.
    
    However, each site can support even more users by adding another Front End pool to the site. To support these extra users, you need to add only one additional Front End pool (that is, just single pools at each site of A/V Conferencing Servers, Edge Servers, and Directors are sufficient, although more servers may need to be added to these pools).

  - **Using Standard Edition Server at a branch site**    Aside from its use in Skype for Business Server 2015, this organization considers Site C as a branch site because it has only 600 employees. However, the users there have many A/V conferences among themselves. If it was deployed in Skype for Business Server 2015 as a branch site, the media for these conferences would run across the wide area network (WAN) to and from a central site with A/V Conferencing Server installed. To avoid this potential performance problem, they have installed a Standard Edition server at this site, which will host these conferences. And because a Standard Edition server is installed there, Skype for Business Server 2015 by definition considers it a central site, and it is treated as such in Skype for Business Server 2015 Topology Builder and the Skype for Business Server 2015 Planning Tool.
    
    As long as the users at this site have a pool in another site set as their backup registrar, they will have high availability for Enterprise Voice—voice support will fail over to the backup registrar site automatically. For a more complete high availability solution at this site, you could deploy a second Standard Edition server there.
    
    Although Site C is considered a central site, you do not have to deploy Edge Servers there. In this example, Site C will use the Edge Servers deployed at Site A.

  - **Monitoring Server and Archiving Server collocation**   This organization deploys both Monitoring Server and Archiving Server. For organizations that deploy both, we recommend that you collocate them to save server investment. One Monitoring Server can support up to 100,000 users, and one Archiving Server can support up to 300,000 users.
    
    Note that you need to deploy Monitoring Server and Archiving Server in only one central site. If the link between the two central sites goes down, the Message Queuing (also known as MSMQ) technology used by both Monitoring Server and Archiving Server helps preserve data while the link is down.
    
    In this topology, Monitoring Server and Archiving Server use a separate database server than any Front End pool. Topologies in which the Monitoring Server and Archiving Server share the same database servers as the Front End pool are also supported, although on large deployments such as this, separate database servers are recommended for performance.
    
    For the database servers for Monitoring Server and Archiving Server, we recommend a cluster of two servers for redundancy and failover.

  - **Branch Site deployment options**   The organization in this topology has Enterprise Voice deployed as their voice solution. Branch Sites 1 and 3 do not have a resilient WAN link to the central site, so they have Survivable Branch Appliances deployed to provide telephone service in case the WAN link to the central site goes down. Branch Site 2 however has a resilient WAN link, so you need only a public switched telephone network (PSTN) gateway. The PSTN gateway deployed there supports media bypass, so no Mediation Server is needed at Branch Site B.

  - **SIP trunking and Mediation Server**   Notice that at Site A, Mediation Server is not collocated with the Front End Servers. This is because sites that use SIP trunking must deploy Mediation Server in a separate pool from the Front End Servers. In all other instances, we recommend you collocate Mediation Server with Front End Server.

  - **DNS load balancing**   The Front End pool, Edge Server pool, and the Director pool have DNS load balancing for SIP traffic deployed. This eliminates the need for hardware load balancers for the internal interface of the Edge Servers, and significantly decreases the amount of time you have to spend on the setup and maintenance of the hardware load balancers for the other pools, as the hardware load balancers are needed only for HTTP traffic.

  - **Exchange UM Deployment**  Skype for Business Server 2015 works with both on-premise deployments of Exchange Unified Messaging (UM) and hosted Exchange UM. Central Site A includes an Exchange Unified Messaging (UM) Server, which runs Microsoft Exchange server, not Skype for Business Server 2015. The Exchange UM functionality for Skype for Business Server 2015 runs on the Front End pool.
    
    Central Site B uses hosted Exchange, so the Exchange UM Server functionality is also hosted.


> [!NOTE]
> <P>In Skype for Business Server 2015, UCMA applications should never be homed on the branch office appliance. Instead, they should be connected directly to the Skype for Business Server 2015 Front End computer or Front End pool.</P>


