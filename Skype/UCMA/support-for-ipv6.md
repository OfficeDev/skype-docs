---
title: Support for IPv6
TOCTitle: Support for IPv6
ms:assetid: 8bfa4980-a839-4cab-8a45-739d86d53bef
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn465946(v=office.16)
ms:contentKeyID: 65239787
ms.date: 07/27/2015
mtps_version: v=office.16
---

# Support for IPv6


**Applies to**: Skype for Business 2015

## IPv6 overview

The added support of Internet Protocol Version 6 (IPv6) addressed in Skype for Business Server 2015 has some implications for applications written on UCMA. Applications written for Skype for Business Server 2015 need to be IPv6-aware, and must use the new and updated APIs in UCMA 5.0 for this purpose. Any Skype for Business Server 2015 deployment could at some point become an IPv6-only deployment, and would break most existing applications if they have not been updated. 

In addition to the standard recommendations for writing applications to support IPv6, there are best practices for writing applications on UCMA 5.0 that should be followed when updating applications. Even though UCMA 5.0 supports IPv6, using IPv4 is preferred in most deployments, if available. This is because media quality is generally better over IPv4, and configuration and deployment become much more difficult when you add IPv6 support in your deployment.

### Support for IPv6 in Skype for Business Server 2015

Before attempting to integrate a UCMA application that supports IPv6 into a Skype for Business deployment, it is useful to have a basic understanding of possible Skype for Business Server 2015 deployments. (A discussion of how to deploy Skype for Business Server 2015 under all possible deployment scenarios is beyond the scope of this topic.) The focus in this topic is to provide some guidance on what to consider when you write your applications.

Skype for Business Server can be deployed with IPv4-only support, IPv6-only support, or Dual stack (IPv4 and IPv6) support. Because many organizations likely still have IPv4-only networks, the default is IPv4. There is a transition period during which organizations will need to support both IPv4 and IPv6 (i.e. dual stack) modes. For the customers whose deployments occur during this transition period, both Skype for Business Server and UCMA can be configured to support both stacks. At some point, customers will have updates for all of their applications to support IPv6 and can transition to an IPv6-only topology or there are customers that are required to exclusively use IPv6 in their deployments.

To simplify deployment, application development, and testing, Skype for Business Server 2015 supports IPv6/IPv4 configuration only at the pool level. Every application instance within the pool must be compatible with the pool level setting. Messages forwarded across pools, or between organizations can be sent over a protocol that the application is not configured for, but the Skype for Business Server infrastructure handles this automatically for applications in most cases.

## Creating or updating a UCMA application to support IPv6

If you intend to support IPv6 in a new UCMA 5.0 application or in an existing application, there are a few things to keep in mind. The two subtopics in this section provide advice on potential problem areas to look for in your code, and give guidance on testing your application after you get it running.

### Checking your code for necessary changes

There are a number of things to check for in your code when you update it to support IPv6. Many of these items come from general recommendations for updating code to support IPv6, and are repeated here for convenience. It is also recommended that you look at appropriate sections in MSDN for additional issues you should look for.

Check your code for the following:

- Any uses of the [Dns](https://msdn.microsoft.com/en-us/library/b8hth2dy) class. It is very possible that there might be built-in assumptions about the number of addresses that this class will return, that the first address is an IPv4 address, or there is some logic that filters out IPv6 addresses. In addition, some code might assume that the results from a DNS query are separate physical machines and thus some form of resiliency or load balancing can be accomplished across the addresses being returned. This is no longer a safe assumption.

- Hard-coded IP addresses. This might be [Any](https://msdn.microsoft.com/en-us/library/hdk35zc9) or [Loopback](https://msdn.microsoft.com/en-us/library/8e4b4zeh). Supporting IPv6 means that there are now two **Any** addresses and two **Loopback** addresses that must be considered—the two fields already listed, as well as the [IPv6Any](https://msdn.microsoft.com/en-us/library/h2ca14s4) and [IPv6Loopback](https://msdn.microsoft.com/en-us/library/052fbtx7) fields in your code. For best results, hosts should be specified by their FQDN and not by address.

- Dual stack mode requires configuring at least two listening addresses (**IPAddress.Any** and **IPAddress.IPv6Any**). Check that your code can support configuring two addresses.

After searching your code for occurrences of these items, you should have a good idea of the changes you might need in your application to get it ready for Skype for Business Server 2015.

### Testing changes to your application

After you have identified areas where changes are required and you are ready to test, here is some additional information that will help you test your application.

To be certain your changes are working properly, it is important to test on an IPv6-only Lync Server topology. Because the default is to prefer IPv4 if available, you will not know if your application works in an IPv6 environment because it will not use IPv6.

You can perform additional verification that your application is working properly by capturing protocol logs. In these logs you should see that connections are being made using the IPv6 protocol, and that protocol messages identify destinations in the From/to/contact headers using FQDNs, rather than IP addresses.

### Additional design considerations

In addition to the points already discussed, there are two more items to consider. This first discusses IPv6 implications for the [CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) instance for your application, and the second discusses how the addition of IPv6 can affect load balancing and resiliency for your application.

#### Configuring the CollaborationPlatform

If you are using [ServerPlatformSettings](https://msdn.microsoft.com/en-us/library/hh382156\(v=office.16\)) to configure your application, consider provisioning your application provisioning by the use of [ProvisionedApplicationPlatformSettings](https://msdn.microsoft.com/en-us/library/hh385058\(v=office.16\)). Provisioning in this manner makes it possible for the platform to automatically configure the IPv4/IPv6/dual listening addresses at start up.

> [!NOTE]
> Note that Skype for Business Server components currently check the pool setting once at start up. A change to the pool stack mode requires restarting all the services. Although it is good to plan ahead to support this pool setting being changed dynamically during runtime, it is not strictly necessary to immediately implement this capability. Provisioned applications require a restart to pick up a new configuration of stack mode.

If you cannot use provisioning in your application and must use **ServerPlatformSettings**, it is best to always run in a dual stack mode unless you can detect that there is no operating system support for one of the protocols. This allows a Skype for Business Server administrator to change the stack mode, and other components in the server can still contact your application.

#### Load balancing and resiliency

Although there are no significant changes required to applications to achieve a similar level of resiliency as was achieved in previous versions, it is good to understand how IPv6 affects load balancing and resiliency in Skype for Business Server 2015.

With the addition of IPv6, FQDNs of remote machines can resolve to multiple IP addresses that are routed to the same physical machine. This means care must be taken to identify code that assumes that each address routes to a separate machine and gives a false sense of resiliency. For this reason, resiliency algorithms have been updated in UCMA 5.0 to try connecting across IPv4 and a list of the same IPv6 address types when attempting to make a connection to a specific FQDN.

While this is fine in most cases, it is important to be as specific as possible when configuring your application and DNS as to which protocols it should use to connect. Retrying a connection is expensive (possibly 21 seconds), and there are cases where some portions of call establishment have only a 30 second maximum time limit. This means that there are cases where only the first two addresses will be tried, and calls might fail even though there was an address in the DNS that would have connected immediately. This limitation is inherent in the protocol and means that in some configurations that are not optimal, the first few calls at application startup may fail while the server learns which remote addresses are operational.

To give the best experience possible, follow these recommendations.

- Ensure that each instance of your application running in the pool is configured to listen on the same address families.

- Do not configure extra DNS entries that are not needed.

- Use DNS instead of the hosts file to increase your chances of a consistent configuration.

- Configure your application to use dual stack mode (if you are using **ServerPlatformSettings**) and so that it is consistently configured with the Skype for Business Server pool it is running in.

- Use the new APIs and hints as appropriate to specify exceptions to the default configuration. The [AddressFamilyHint](https://msdn.microsoft.com/en-us/library/jj728964\(v=office.16\)) enumeration, which is new in UCMA 5.0, can be used to configure [ConnectionContext](https://msdn.microsoft.com/en-us/library/hh366132\(v=office.16\)) instances that are passed in various places in the API to give more detailed information on how to connect to differently configured hosts.

- If you have tried everything else and you need compatibility with the previous version of the UCMA stack, there are two alternatives. Note that either alternative should be used as a last resort as your application will not function properly with an IPv6-only Skype for Business Server 2015 pool.
    
  1.  If you are working with [RealTimeEndpoint](https://msdn.microsoft.com/en-us/library/hh366081\(v=office.16\)) objects in your application, you can use the **\[M:Microsoft.Rtc.Signaling.RealTimeConnectionManager.DisableIpV6Support()\]** method.
    
  2.  If you are using a [CollaborationPlatform](https://docs.microsoft.com/dotnet/api/microsoft.rtc.collaboration.collaborationplatform?view=ucma-api) object in your application, you can disable IPv6 support by setting the [IpV6SupportDisabled](https://msdn.microsoft.com/en-us/library/jj728958\(v=office.16\)) property on a [ServerPlatformSettings](https://msdn.microsoft.com/en-us/library/hh382156\(v=office.16\)) instance to true.

### Using IP Addresses instead of FQDNs; Configuring application instances consistently

Configuring UCMA applications with IP addresses and connecting to Skype for Business Server 2015 can be the source of many problems, and is therefore strongly discouraged. Creating an application with IP addresses might be necessary at times, though, such as when you create an application that connects to other SIP-enabled components such as gateways. If this is the case, it is important that you take advantage of the [AddressFamilyHint](https://msdn.microsoft.com/en-us/library/jj728964\(v=office.16\)) mechanisms and carefully configure DNS so that connections can be made, and connection retries are avoided when possible.

If you have multiple application instances that are configured differently, it is usually beneficial to store that configuration data in a central location so that connections for each application instance can be configured properly with a minimum of effort.

### Compatibility with previous versions of UCMA

For a UCMA 5.0 application installed on an IPv6-only Skype for Business Server 2015 pool to reliably communicate with a Lync Server 2010 pool, it is important that the Lync Server 2010 pool be updated with UCMA 3.0, CU6 or later. This is because some message content coming from an IPv6-enabled pool cannot be interpreted correctly by older versions.

