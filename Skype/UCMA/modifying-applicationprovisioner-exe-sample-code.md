---
title: Modifying ApplicationProvisioner.exe sample code
TOCTitle: Modifying ApplicationProvisioner.exe sample code
ms:assetid: 3e9e49ba-651e-4a09-939f-37b863e4a415
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466118(v=office.16)
ms:contentKeyID: 65240060
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Modifying ApplicationProvisioner.exe sample code


**Applies to**: Skype for Business 2015

The ApplicationProvisioner.exe sample code that shipped with UCMA 2.0 SDK can be used to provision UCMA 2.0 applications, but you must make one modification to the code and then rebuild the application.

The file that must be changed is Application.cs, which is located in the %Program Files%\\Microsoft Office Communications Server 2007 R2\\UCMA SDK 2.0\\Sample Applications\\ApplicationProvisioner\\ApplicationProvisioning\\ directory. The code that must be changed is in the **GetApplication** method in Application.cs. The change entails adding a second Boolean expression in the second if statement. The **GetApplication** method, with modification, is shown in the following code example.

```csharp
/// <summary>
/// Gets the collection of TrustedApplication objects representing each pool for the application.
/// </summary>
/// <param name="applicationType">The unique type of the application.</param>
/// <returns></returns>
public static ICollection<TrustedApplicationPool> GetApplication(string applicationType)
{
  List<TrustedApplicationPool> trustedAppPools = new List<TrustedApplicationPool>();
 
  // Get all the trusted service entries for the application.
  ManagementObjectCollection appTrustedServices = Helper.GetTrustedServices(applicationType);
 
  // Map of tlsTarget to list of trusted services in the pool with that tlsTarget
  // The first element of the list is the trusted service in the pool whose FQDN matches
  // the tlsTarget of the pool.
  Dictionary<string, List<TrustedService>> appPools = new Dictionary<string, List<TrustedService>>();
 
  // List of all trusted services with non null tlsTargets
  List<TrustedService> trustedServicesWithTlsTargets = new List<TrustedService>();
  // Make first pass to get the pools.
  if (appTrustedServices != null && appTrustedServices.Count > 0)
  {
    foreach (ManagementObject trustedServiceObject in appTrustedServices)
    {
      TrustedService trustedService = new TrustedService(trustedServiceObject, null);

      // IMPORTANT! Change this statement.
      // Add the second Boolean expression in the following if statement. 
      if ( string.IsNullOrEmpty(trustedService.TlsTarget) || trustedService.TlsTarget.Equals(trustedService.Fqdn, StringComparison.OrdinalIgnoreCase) )
      {
        // We have a new pool.
        appPools.Add(trustedService.Fqdn, new List<TrustedService>(new TrustedService[] { trustedService }));
      }
      else
      {
        // We have a trusted service with a tlsTarget.
        trustedServicesWithTlsTargets.Add(trustedService);
      }
    }
  }
 
  // Go through to the trusted services to add them to the correct pool collection.
  if (trustedServicesWithTlsTargets.Count > 0)
  {
    foreach (TrustedService trustedService in trustedServicesWithTlsTargets)
    {
      List<TrustedService> pool;
 
      if (appPools.TryGetValue(trustedService.TlsTarget, out pool))
      {
        pool.Add(trustedService);
      }
      else
      {
        throw new Exception("The trusted service entries for this type are corrupted");
      }
    }
  }
 
  // Go through the list of trusted services for each pool to create
  // the TrustedApplication for each pool.
  foreach (List<TrustedService> appPoolTrustedServices in appPools.Values)
  {
    TrustedApplicationPool appPool = null;
 
    if (appPoolTrustedServices.Count > 1)
    {
      // Load balanced pool as there are two or more trusted service entries.
      appPool = new TrustedApplicationPool(appPoolTrustedServices[0], appPoolTrustedServices.GetRange(1, appPoolTrustedServices.Count - 1));
    }
    else
    {
      appPool = new TrustedApplicationPool(appPoolTrustedServices[0], false);
    }
 
    // Get the contact objects for the application. 
    appPool.PopulateContactObjects();
 
    trustedAppPools.Add(appPool);
  }
 
  return trustedAppPools.ToArray();
}
```

