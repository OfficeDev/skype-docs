---
title: General application activation
TOCTitle: General application activation
ms:assetid: 029f3c80-dea6-4338-b622-b9f1338d2432
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466115(v=office.16)
ms:contentKeyID: 65240050
ms.date: 07/27/2015
mtps_version: v=office.16
---

# General application activation

**Applies to**: Skype for Business 2015

All trusted applications require entries in the Skype for Business Server 2015 topology document that specify the computers on which the application runs, as well as the trusted service ports required by the trusted applications. In addition, to communicate with Skype for Business Server 2015, an application computer also requires a machine certificate, because Skype for Business Server 2015 supports only Mutual Transport Layer Security (MTLS) with trusted servers. Optionally, an application can be associated with one or more trusted application endpoints that are bound to SIP URIs in the same way that a user is associated with a SIP URI.

All trusted applications must complete the steps shown in the following list:

1.  Create a pool of trusted application computers in the Skype for Business Server 2015 topology.

2.  Request or import a certificate for the application computer.

3.  Add a trusted service port for the application.

4.  Add the application and its port to the list of firewall exceptions.

5.  Optionally create one of more Active Directory contact objects for the application.

6.  Optionally create Active Directory user objects for the application.

All of these steps, except for the firewall exceptions, are discussed in detail in the following sections.

> [!IMPORTANT]
> Trusted application must have the appropriate firewall exceptions in Windows Firewall on the computer on which the UCMA-based applications are running, and on any applicable corporate firewalls.

## Create a pool of trusted application computers

All computers on which the application is to run must be added to the Skype for Business Server 2015 topology document. It is recommended that you create a separate computer pool for a trusted application instead of running the application in the same pool where other Skype for Business Server 2015 services are running. This step involves creating a new pool and adding application computers to it, and can be accomplished in either of two ways:

- Using Skype for Business Server 2015 Topology Builder

- Using Windows PowerShell cmdlets

> [!NOTE]
> Package product ISVs who intend to automate setup and deployment for the application are recommended to use PowerShell cmdlets.

### To create a trusted application pool using Skype for Business Server 2015 Topology Builder

1. Running in the Skype for Business Server 2015 Administrator role on the computer on which Skype for Business Server 2015 Topology Builder is installed, launch Skype for Business Server 2015 Topology Builder.
    
   On the **Start** menu, select **All Programs**, select **Skype for Business Server 2015**, and then click **Skype for Business Server 2015 Topology Builder**.

2. Import the current Skype for Business Server 2015 topology.
    
   If a prompt appears when Topology Builder starts, make sure that **Download Topology from existing deployment** is selected, and then click **OK**. Otherwise, click the **Download Topology** link on the right side of the window, and, after the topology has been downloaded, click **OK**.
    
   If a **Save As** dialog box appears, enter the name of the file in which to save the current topology document on the current user’s desktop, then click **Save**.

3. Expand the site on which you wish to create a pool of trusted application computers.

4. Define a new trusted application pool.
    
   Click the **New** link on the right side of the window, and then click **Trusted application Pool**. For application development, the pool can consist of only a single computer. If your requirements call for multiple computers in the application pool, additional steps, not covered in this documentation, are necessary. Enter the FQDN of the development computer, click **Single computer pool**, then click **Next**. Select a Next hop pool, and then click **Finish**.

5. Click the **Skype for Business Server 2015** node in the tree-view control.

6. On the right side of the window, click the **Publish...** button.

7. Click the **Next** button.

8. Click the **Finish** button.

### To create a trusted application pool using PowerShell cmdlets

1. Running in the Skype for Business Server 2015 Administrator role on the computer on which Skype for Business Server Management Shell is installed, launch Skype for Business ServerManagement Shell.
    
   On the **Start** menu, select **All Programs**. On the right-click menu, select **Skype for Business Server 2015**, and then select **Skype for Business Server Management Shell**.
    
   > [!NOTE]
   > If the application computer is joined to the domain, Skype for Business Server 2015 cmdlets can be run from this computer.

2. Create the application pool. There are two ways to do this, depending on whether the application is auto-provisioned or manually provisioned:
    
   1. For an auto-provisioned application, create the trusted application pool by running the **New-CsTrustedApplicationPool** cmdlet. In the following example, the FQDN of the pool of trusted application computers is *trustedapps.contoso.com*, the Registrar pool FQDN is *atl-mcs-001.contoso.com*, and the site ID is *co1*.

      ```powershell     
        New-CsTrustedApplicationPool - Identity trustedapps.contoso.com -Registrar atl-mcs-001.contoso.com -Site co1
      ```

      The following PowerShell command is identical to the previous command, except that it adds another parameter. The **ComputerFqdn** parameter specifies the FQDN of the first computer in the trusted application pool. The FQDN of this computer is machine1.contoso.com.

      ```powershell        
        New-CsTrustedApplicationPool -Identity trustedapps.contoso.com -Registrar atl-mcs-001.contoso.com -Site co1 –ComputerFqdn machine1.contoso.com
      ```

   2. For a manually-provisioned application, create the trusted application pool by running the **New-CsTrustedApplicationPool** cmdlet. This is the same cmdlet that is used for auto-provisioned applications, but an additional cmdlet parameter is used. The **RequiresReplication** parameter with a value of $false indicates that Central Management Store replication is not required. In the following example, the FQDN of the pool of trusted application computers is *trustedapps.contoso.com*, the Registrar pool FQDN is *atl-mcs-001.contoso.com*, Central Management Store replication is set to false, and the site ID is *co1*.

      ```powershell    
          New-CsTrustedApplicationPool -Identity trustedapps.contoso.com -Registrar atl-mcs-001.contoso.com –RequiresReplication $false -Site co1
      ```    
      
      The following PowerShell command is identical to the previous command, except that it adds another parameter. The **ComputerFqdn** parameter specifies the FQDN of the first computer in the trusted application pool. FQDN of this computer is machine1.contoso.com.

      ```powershell        
        New-CsTrustedApplicationPool -Identity trustedapps.contoso.com -Registrar atl-mcs-001.contoso.com –RequiresReplication $false -Site co1 –ComputerFqdn machine1.contoso.com
      ```

      The FQDN of the application server should appear in the list of replicas.

3. (Optional) To add additional computers to the trusted application, run the **New-CsTrustedApplicationComputer** cmdlet. In the following example, a new trusted application computer with an FQDN of machine2.contoso.com is added to the trusted application pool whose FQDN is trustedapps.contoso.com.

   ```powershell    
    New-CsTrustedApplicationComputer -Identity machine2.contoso.com -Pool trustedapps.contoso.com
   ```

4. Run the **Enable-CsTopology** cmdlet to create the appropriate trusted service entries in Active Directory.

   ```powershell    
    Enable-CsTopology
   ```

## Trusted application pools and DNS load balancing

When a trusted application pool consists of more than one computer, load balancing can occur on the application through additional DNS configuration of round-robin DNS responses. Load balancing is not applicable to topologies in which a single-computer pool is used, such as for development scenarios.

In the deployment shown in the following illustration, there are multiple application computers. When multiple computers are configured with a single application as part of a trusted application pool, the application can be load-balanced in either of two ways: by hardware load balancing; by DNS-based load balancing.

To use DNS-based load balancing for an application pool, the application administrator must configure the application pool FQDN to resolve to multiple IP addresses using DNS A records, and these IP addresses must be associated with the computers in the application pool. All computers in the application pool must use the same certificate to connect; this requirement is identical to the hardware load-balanced case.

> [!NOTE]
> DNS A records are also required for application pools consisting of only a single computer. Without a DNS A record, the application will be unable to receive inbound requests.

In the following illustration, apppool1.contoso.com is the application pool FQDN, which should return 123.1.1.1, 123.1.1.2, and 123.1.1.3 if queried by the Windows command **nslookup**.

**Multiple computer pool**

![Redundant Deployment](images/Dn466115.MarkP1(Office.16).png "Redundant Deployment")

## Create a certificate for the computers in a trusted application pool

MTLS requires a certificate to establish a mutual trust relationship. For computers in a trusted application pool, the certificate should meet the following criteria:

- Certificates should be stored in the application host computer's Console Root\\Certificates (Local Computer)\\Personal\\Certificates folder.

- The Subject Name (SN) of the certificate should be set to the trusted application pool FQDN.

- The Subject Alternative Name (SAN) of the certificate should list the trusted application pool FQDN and all of the trusted application computer FQDNs.

- The certificate must be trusted by a root certificate in the host computer’s Console Root\\Certificates (Local Computer)\\Trusted Root Certification Authorities\\Certificates folder.

- The account used to run the application must have read access to the certificate store and the private key.

The steps that follow list different ways of requesting a certificate that matches the given criteria. Certificates meeting all of the preceding requirements for multiple computers in a pool can be exported to other computers’ local certificate stores and used for trusted applications. However, users in the Trusted Application Service Account role must be given access to the certificates on all machines.

> [!NOTE]
> Offline requests to be sent to third-party certificate authorities can be generated using all of the following methods. Administrators should refer to Get-Help Request-CsCertificate –Full and Get-Help Import-CsCertificate –Full for additional information.
> `Request-CSCertificate -New -Type default -CA DomainController.contoso.com\CertificateAuthority`

### Requesting certificates from Active Directory Certificate Services

To request certificates as a user running in the Trusted Application Operator role, users or security groups running in that role must be given the appropriate permissions. These steps must be undertaken in the Domain Administrator role.

1.  On the **Start** menu, click **Server Manager**.

2.  Expand **Roles**.

3.  Expand **Active Directory Certificate Services**.

4.  Click **Certificate Templates**.

5.  Right-click the **Web Server** template, and then click **Properties**.

6.  Click the **Security** tab.

7.  Click the **Add** button.
    
    1.  If you are giving access to a user running in the Trusted Application Operator role, type the name of the user or security group to be given access.
    
    2.  If you are giving access to a trusted application computer, click the **Object Types** button, check **Computers**, click **OK**, and then type the computer name.

8.  Click **Check Names** to double-check.

9.  Click **OK**.

10. Click **OK**.

11. Verify that the trusted application operator is selected in the top pane.

12. Check the box corresponding to the **Allow** column and the **Enroll** row in the bottom pane.

13. Click **OK**.

14. On the **Start** menu, click **Run**.

15. Type net stop certsvc, and then click **OK**.

16. On the **Start** menu, click **Run**.

17. Type net start certsvc, and then click **OK**.

### Requesting certificates using the Request-CsCertificate cmdlet

To perform these actions using Skype for Business Server Management Shell, you must be in the Skype for Business Server 2015 Administrator or Trusted Application Operator role, on the computer where the certificate is required.

1. On the **Start** menu, select **All Programs**, select Skype for Business Server 2015, and then click **Skype for Business Server Management Shell**.

2. Request a certificate:
    
   1. For a trusted application pool consisting of only a single computer where the pool FQDN matches the computer FQDN, create a certificate using the **Request-CsCertificate** cmdlet. In the following example, the friendly name of the requested certificate will be set to trustedapps.contoso.com Pool, the certificate authority is on the machine ca.contoso.com with a name of ContosoCA, and the FQDN of the trusted application computer the cmdlet is being run on is machine1.contoso.com.

      ```powershell  
       Request-CsCertificate -New -Type default -FriendlyName "trustedapps.contoso.com Pool" -CA ca.contoso.com\\ContosoCA -ComputerFQDN machine1.contoso.com
      ```

   2. For a trusted application pool consisting of multiple computers, or a trusted application pool consisting of a single computer where the pool FQDN does not match the computer, the **Request-CsCertificate** cmdlet can still be used to create a certificate. However, the –**DomainName** argument must be used. In the following example, the friendly name of the requested certificate will be set to trustedapps.contoso.com Pool, the certificate authority is on the computer ca.contoso.com with a name of ContosoCA, the FQDN of the trusted application computer the cmdlet is being run on is machine1.contoso.com, and the trusted application pool consists of two computers: machine1.contoso.com and machine2.contoso.com.
       
      ```powershell
       Request-CsCertificate -New -Type default -FriendlyName "trustedapps.contoso.com Pool" -CA ca.contoso.com\\ContosoCA -ComputerFQDN machine1.contoso.com -DomainName "machine1.contoso.com,machine2.contoso.com"
      ```

> [!NOTE]
> If a local Central Management Store replica is installed on the computer on which the **Request-CsCertificate** cmdlet is being run, the -**ComputerFQDN** argument can be omitted. However, the requested certificate’s private key will not be marked exportable. To allow the private key to be exported and the certificate to be reused on other machines in the pool, append -PrivateKeyExportable $true to the command.

### Requesting certificates using the certificates snap-in

To perform these actions using the Microsoft Management Console Certificates Snap-in, you must be in the Skype for Business Server 2015 Administrator or Trusted Application Operator role, on the computer where the certificate is required.

In the following procedure, the friendly name of the requested certificate will be set to trustedapps.contoso.com Pool, the trusted application pool has an FQDN of trustedapps.contoso.com, and the trusted application pool consists of two computers: machine1.contoso.com and machine2.contoso.com.

1.  On the **Start** menu, click **Run**.

2.  Type **mmc**, and click **OK**.

3.  Click the **File** menu, click **Add/Remove Snap-in**, click **Certificates**, click **Add**, select **Computer account**, click **Next**, click **Finish**, and then click **OK**.

4.  Double-click **Certificates (Local Computer)**, and then double-click **Personal**.

5.  Right-click **Certificates**, select **All Tasks**, and then click **Request New Certificate**.

6.  Click **Next**.

7.  Check **Web Server**.

8.  Click the link displayed below **Web Server** that reads "More information is required to enroll for this certificate. Click here to configure settings."

9.  On the **Subject** tab, under **Subject name**, select **Full DN from the Type** drop-down menu, and enter the following into the **Value** textbox.
    
    `CN=trustedapps.contoso.com`
    
    The FQDN shown here for example purposes.

10. Click the **Add** button directly to the right.

11. Under **Alternative name**, select **DNS**, and then enter trustedapps.contoso.com and machine1.contoso.com and machine2.contoso.com. Click the **Add** button directly to the right after each.
    
    The FQDNs shown here are for example purposes.

12. Under the **General** tab, enter trustedapps.contoso.com Pool.

13. Under the **Private Key** tab, click the expand control to the right of **Key** options, and then check **Make private key exportable**.

14. Click **OK**.

15. Click **Enroll**.

### Requesting a certificate using the certsrv Web interface

To perform the steps in the following procedure, you must be a Domain Administrator on the computer running Active Directory Certificate Services. In this procedure you are configuring the certificate authority to allow Subject Alternative Names (SANs) in certificates.

1.  On the **Start** menu, click **Run**.

2.  Enter certutil -setreg policy\\EditFlags +EDITF\_ATTRIBUTESUBJECTALTNAME2, and then click **OK**.

3.  On the **Start** menu, click **Run**.
    
    Enter net stop certsvc, and then click **OK**.

4.  On the **Start** menu, click **Run**.
    
    Enter net start certsvc, and then click **OK**.

To perform the steps in the following procedure, you must be in the Skype for Business Server 2015 Administrator or Trusted Application Operator role. In this procedure you are requesting the certificate using the certsrv web interface. In the following example, the friendly name of the requested certificate will be set to trustedapps.contoso.com Pool, the certificate authority is on the machine ca.contoso.com.

1.  Launch Internet Explorer.

2.  Navigate to https://ca.contoso.com/certsrv.
    
    The URL shown here is for example purposes.

3.  If prompted for them, enter valid domain administrator credentials.

4.  Click the **Request a certificate** hyperlink.

5.  Click the **advanced certificate request** hyperlink.

6.  Click the **Create and submit a request to this CA.** hyperlink.

7.  If prompted, install the ActiveX control, if required (check near the top of the Internet Explorer page).

8.  In the **Certificate Template** dropdown menu, select **Web Server**.
    
    > [!NOTE]
    > If the **Web Server** template does not appear in the dropdown menu, the domain credentials used to connect to certsrv might lack Request permissions for the **Web Server** template. For information about adding the required permissions, see "Requesting Certificates from Active Directory Certificate Services" elsewhere in this topic.

9.  In the **Name** field, enter trustedapps.contoso.com. This FQDN is for example purposes.

10. Optionally, fill in the other **Identifying Information For Offline Template** fields.

11. Check **Mark keys as exportable**.

12. In **Additional Options** section, for **Request Format**, select **PKCS10**.

13. In the **Attributes** text-box, enter SAN:DNS= trustedapps.contoso.com\&DNS=machine1.contoso.com\&DNS=machine2.contoso.com.
    
    These FQDNs are shown for example purposes.

14. In the **Friendly Name** text box, enter trustedapps.contoso.com Pool.
    
    This friendly name is shown for example purposes.

15. Click **Submit**.

16. Click **Yes**.

17. Click the **Install this certificate** hyperlink.
    
    The certificate can be found in the current user's Personal store. It must be moved to the local machine's Personal store to be used by trusted applications.

### Enabling a trusted application service account to access certificates in a local certificate store

To perform the steps in the following procedure, you must be in Trusted Application Operator role. In this procedure, you are granting the intended user or security group access to the newly requested or imported certificate. One of the actions that must be performed is to find the certificate, which can be done using FindPrivateKey.exe. For more information, see [Find Private Key Tool (FindPrivateKey.exe)](https://msdn.microsoft.com/en-us/library/ms732026.aspx).

1. On the **Start** menu, select **All Programs**, select **Accessories**, right-click **Command-Prompt**, and then click **Run as administrator**.

2. Navigate to the directory that contains FindPrivateKey.exe.

3. Run FindPrivateKey.exe to find the location of the certificate. In the following example, the certificate has a thumbprint of 2d 44 c7 d7 fe 6e aa 80 8b e3 1c ee 8f c4 f1 68 21 38 e9 40.
    
   `FindPrivateKey.exe My LocalMachine -t "2d 44 c7 d7 fe 6e aa 80 8b e3 1c ee 8f c4 f1 68 21 38 e9 40" –a`

4. From the same command prompt, change the permissions on the returned file to allow the user or security group running in the trusted application service account role to access the certificate. In the following example, the certificate private key is in this located in C:\\ProgramData\\Microsoft\\Crypto\\RSA\\MachineKeys\\f56fa47eb8664da68dfb4b51ac8902b0\_eb9db29e-9ba0-4da7-9e31-53d7e8f95130, and the user running in the trusted application service account role is CONTOSO\\AppDev.

   `cacls` `"C:\\ProgramData\\Microsoft\\Crypto\\RSA\\MachineKeys\\f56fa47eb8664da68dfb4b51ac8902b0\_eb9db29e-9ba0-4da7-9e31-53d7e8f95130" /E /G "CONTOSO\\AppDev":R`

## Add a trusted service port for the application

To perform the steps of the following procedure, you must be in the Skype for Business Server 2015 Administrator role on the computer where Skype for Business Server Management Shell is installed.

### To add a trusted service port for the application

1. On the **Start** menu, select **All Programs**, select **Skype for Business Server 2015**, and then select **Skype for Business Server Management Shell**.

2. Add your application to the application pool.
    
   The following PowerShell cmdlet adds an application to the *trustedapps.contoso.com* application pool, using port 6000.
    
   ```powershell
    New-CsTrustedApplication -ApplicationId applicationID -TrustedApplicationPoolFqdn trustedapps.contoso.com -Port 6000
   ```

   > [!IMPORTANT]
   > In the UCMA 5.0 release, the administrator must run the **Enable-CsTopology** cmdlet after running the **New-CsTrustedApplicationPool** or **New-CsTrustedApplication** cmdlet. This creates the appropriate trusted service entries in Active Directory.

   When you run this PowerShell cmdlet, replace *applicationID* with the application ID for your application, and replace *trustedapps.contoso.com* with the FQDN of your application pool.

3. Run the Enable-CsTopology cmdlet to create the appropriate trusted service entries in Active Directory.
    
   ```powershell
      Enable-CsTopology
   ```

Using Skype for Business Server 2015 Control Panel you can view the application name, trusted application pool FQDN, and application port.

## Create Active Directory contact objects (optional)

Some UCMA 5.0 trusted applications, such as query-response "bots" and Helpdesk or contact center applications, also need an Active Directory contact object. The Active Directory contact object is similar to an Active Directory user object. This contact object gives applications a virtual identity in the form of a SIP URI or phone number. To create an Active Directory contact object, carry out the following steps.

To perform the steps of the following procedure, you must be in the Skype for Business Server Administrator role or Trusted Application Operator role, on a computer on which Skype for Business Server Management Shell is installed.

### To create Active Directory contact objects

1. On the computer where Skype for Business Server Management Shell is installed, launch Skype for Business Server Management Shell.
    
   On the **Start** menu, select **All Programs**, select **Skype for Business Server 2015**, and then click **Skype for Business Server Management Shell**.

2. Add an endpoint for the trusted application. In the following example, a new trusted application endpoint is added to the trusted application with an ID of applicationID, running on the trusted application pool whose FQDN is trustedapps.contoso.com. The endpoint is assigned a SIP URI of sip:MyTestBot@contoso.com and a display name of MyTestBot.

   ```powershell    
     New-CsTrustedApplicationEndpoint -SipAddress sip:MyTestBot@contoso.com -DisplayName "MyTestBot" -TrustedApplicationPoolFqdn trustedapps.contoso.com -ApplicationId applicationID
   ```

3. You can optionally change some of the values of the contact object by running the **Set-CsTrustedApplicationEndpoint** cmdlet. The following example changes the display name of the trusted application endpoint with a SIP URI of sip:MyTestBot@contoso.com to My New Test Bot.

   ```powershell 
    Set-CsTrustedApplicationEndpoint -Identity sip:MyTestBot@contoso.com -DisplayName "My New Test Bot"
   ```

## Create Active Directory user objects for the application (optional)

To perform the steps of this procedure, you must be in the Skype for Business Server Administrator role or Trusted Application Operator role, on a computer on which Skype for Business Server Management Shell is installed.

1.  Create an Active Directory Domain Services user object. The steps are not shown in this documentation.

2.  On the **Start** menu, select **All Programs**, select **Skype For Business Server 2015**, and then click **Skype for Business Server Management Shell**.

3.  Use the Microsoft PowerShell **Enable-CsUser** cmdlet to enable the user object created in the first step. The following example enables the user whose identity is Pilar Ackerman, assigning her the SIP address pilar@contoso.com. This user's home server is atl-mcs-001.contoso.com. Enable-CsUser –Identity "Pilar Ackerman" –SipAddress "sip:pilar@contoso.com" –RegistrarPool atl-mcs-001.contoso.com

The **Get-CsAdUser** cmdlet can be used to determine the Active Directory user objects that have not yet been enabled for use with Skype for Business Server. In the following example, all users who have not yet been enabled for use with Skype for Business Server 2015 are enabled and bound to the home server atl-mcs-001.contoso.com.

```powershell
  Get-CsAdUser -Filter { Enabled -ne $true } | Enable-CsUser -RegistrarPool atl-mcs-001.contoso.com -SipAddressType SamAccountName -SipDomain contoso.com
```
The **Filter** parameter in the preceding command causes **Get-CsAdUser** to return a collection of users who have not been enabled for Skype for Business Server (the users whose **Enabled** attribute is equal to (-eq) False (**$False**). The collection is then piped to the **Enable-CsUser** cmdlet, which enables each of these accounts, and auto-generates a SIP address for each user.

For help on the **Enable-CsUser** cmdlet, run the following command.

Get-Help Enable-CsUser –Full

