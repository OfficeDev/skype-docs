---
title: Migration, coexistence, and upgrade of UCMA applications
TOCTitle: Migration, coexistence, and upgrade of UCMA applications
ms:assetid: 07b8c6c2-7cc7-4449-a6c2-3da177b34389
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466137(v=office.16)
ms:contentKeyID: 65240055
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Migration, coexistence, and upgrade of UCMA applications

**Applies to**: Skype for Business 2015

This section discusses the steps needed to enable UCMA 2.0, UCMA 3.0, and UCMA 4.0 applications to coexist in an environment with pools. It is expected that users homed on these different Communications Server/Lync Server pools will be able to communicate with these applications. This section also discusses the steps needed to migrate UCMA applications to newer versions of Lync Server, and to upgrade UCMA 2.0 or UCMA 3.0 applications to a UCMA 4.0 application.

Before going further in the section, read [Activation, Provisioning, and Deployment Changes in UCMA 4.0](https://msdn.microsoft.com/en-us/library/gg253592\(v=office.16\)), which should be considered a prerequisite to the material in this section. As applicable to your specific Lync Server topology, it is strongly recommended that you read one or both of the following topics in the Migration section of the Microsoft Lync Server 2013 documentation:

- [Migration from Office Communications Server 2007 R2 to Lync Server 2013](https://technet.microsoft.com/en-us/library/jj205375\(v=ocs.15\))

- [Migration from Lync Server 2010 to Lync Server 2013](https://technet.microsoft.com/en-us/library/jj205369\(v=ocs.15\))

An external UCMA 4.0 trusted application is an application developed by a third party, that is granted trusted status to run against Communications Server/Skype for Business Server 2015 and that runs on its own pool of computers. In this topic, the phrase UCMA 2.0 application should be interpreted to mean an external UCMA 2.0 trusted application. In the same way, a UCMA 3.0 application or UCMA 4.0 application should be interpreted to mean an external UCMA 3.0 or UCMA 4.0 trusted application, respectively. An application pool is the group of computers on which these external trusted applications run.

This section includes the following topics:

- [UCMA applications: Coexistence, migration, and upgrade scenarios](ucma-applications-coexistence-migration-and-upgrade-scenarios.md)
- [UCMA 3.0 Core applications: Coexistence and deployment scenarios](ucma-3-0-core-applications-coexistence-and-deployment-scenarios.md)
- [UCMA 2.0 Core applications: Coexistence, migration, and upgrade scenarios](ucma-2-0-core-applications-coexistence-migration-and-upgrade-scenarios.md)
- [Migration and coexistence details (UCMA 2.0 and UCMA 3.0)](migration-and-coexistence-details-ucma-2-0-and-ucma-3-0.md)
- [Modifying ApplicationProvisioner.exe sample code](modifying-applicationprovisioner-exe-sample-code.md)

