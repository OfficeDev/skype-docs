---
title: Activating applications programmatically
TOCTitle: Activating applications programmatically
ms:assetid: d75f69cf-5bdd-4194-9ebb-87a06978b5a0
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466133(v=office.16)
ms:contentKeyID: 65240053
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Activating applications programmatically


_**Applies to:** Skype for Business 2015_

Activating a Microsoft Unified Communications Managed API 5.0 application against Skype for Business Server 2015 typically involves setting a trust relationship between the application and Skype for Business Server 2015, and setting up routing-specific configurations. PowerShell-based programmable activation enables application developers to easily integrate the details of UCMA 5.0 activation into their overall application activation.

The cmdlets needed to create and manage Skype for Business Server 2015 trusted application pools, trusted applications, and trusted application endpoints can be invoked from C\# and other CLI-compliant languages, just as other Windows PowerShell cmdlets can be. For information about creating a PowerShell Runspace and executing PowerShell cmdlets, see [Writing a Windows PowerShell Host Application](http://msdn.microsoft.com/en-us/library/ee706563\(vs.85\).aspx).

The following example demonstrates how to invoke a Skype for Business Server 2015 trusted application cmdlet. Note that this code snippet should be compiled as a 64-bit application. Running Skype for Business Server 2015 Manageability cmdlets from 32-bit instances of Powershell is unsupported. If a 32-bit application is required, care must be taken to enable the execution of scripts for 32-bit PowerShell instances.

``` csharp
using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Microsoft.Rtc.Collaboration.Sample.ApplicationActivation
{
  class Program
  {
    static void Main(string[] args)
    {
      // Get the operating environment to run commands.
      InitialSessionState iss = InitialSessionState.CreateDefault();
      iss.ImportPSModule(new[] { "SkypeForBusiness" });
      using (Runspace myRunSpace = RunspaceFactory.CreateRunspace(iss))
      {
        myRunSpace.Open();

        // Execute the Get-CsTrustedApplication cmdlet.
        using (System.Management.Automation.PowerShell powershell = System.Management.Automation.PowerShell.Create())
        {
          powershell.Runspace = myRunSpace;
          powershell.AddCommand("Get-CsTrustedApplication");

          Collection<PSObject> results = null;
          Collection<ErrorRecord> errors = null;
          try
          {
            results = powershell.Invoke();
            errors = powershell.Streams.Error.ReadAll();
          }

          catch (Exception ex)
          {
            // An error occurred running the cmdlets.
            // TODO (Left to the reader): Error handling code.
            throw;
          }

          if (errors.Count != 0)
          {
            // Errors were reported by the cmdlets.
            // TODO (Left to the reader): Error handling code.
          }

          foreach (PSObject result in results)
          {
            Console.WriteLine("Identity\t: " + result.Members["Identity"].Value);
            Console.WriteLine("ApplicationId\t: " + result.Members["ApplicationId"].Value);
          }
          Console.WriteLine("Press any key to exit...");
          Console.ReadKey();
        }
      }
    }
  }
}
```

