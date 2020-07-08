---
title: Unattended installation of SDN Manager and Dialog Listener
TOCTitle: Unattended installation of SDN Manager and Dialog Listener
ms:assetid: f2eba9a3-9888-4a1b-b689-567a83ebcb90
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn785205(v=office.16)
ms:contentKeyID: 65258666
ms.date: 02/27/2017
mtps_version: v=office.16
---

# Unattended installation of SDN Manager and Dialog Listener


_**Applies to:** Lync Server 2010 | Lync Server 2013 | Skype for Business 2015_

In production, you often have to install the SDN Manager and Dialog Listener using installation scripts, and therefore in an unattended mode. This article describes the parameters that are used for an unattended installation.

## Unattended installation of the SDN Manager

The following table lists installation parameters for an unattended installation of the SDN Manager.


> [!NOTE]
> Using unattended installation is an advanced feature. Not all parameters may be validated or report detailed errors. Use a regular installation and log on to investigate any issues you may experience. 
> For example, include:
> `msiexec /lv* install.log /i SkypeForBusinessSDNManager.msi <your parameters>`.



Follow the guidelines for an unattended install:

  - DOMAIN and COMPUTERNAME are required for creating SQL Login and User objects for the local machine.

  - USE\_SQL\_AUTH=1 is necessary to declare that sql credentials need to be generated (0 is default).

  - SQLUSER and SQLPASSWORD are required if USE\_SQL\_AUTH=1, otherwise ignored.

  - SQLPASSWORD needs to follow SQL password length and complexity rules.

  - DATABASE\_SERVER is required, and cannot be 'localhost'.

  - DATABASE\_NAME can be defaulted (Default ='SDNManager22').

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Parameter</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>INSTALLDIR</p></td>
<td><p>Specifies the installation destination for the binaries.</p></td>
</tr>
<tr class="even">
<td><p>LOGPATH</p></td>
<td><p>Directory to contain the generated log files</p></td>
</tr>
<tr class="odd">
<td><p>TOPOLOGY</p></td>
<td><p>describes the desired deployment scenario:</p>
<div class="tableSection">
<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>0</p></td>
<td><p>(Cache) Use an in-memory cache for settings and call state</p></td>
</tr>
<tr class="even">
<td><p>1</p></td>
<td><p>Obsolete. Not used.</p></td>
</tr>
<tr class="odd">
<td><p>2</p></td>
<td><p>(Database) Use the SDN Manager database to store and share the settings as well as call state.</p></td>
</tr>
<tr class="even">
<td><p>3</p></td>
<td><p>(REDIS) Use a REDIS cache service as a data store for settings and call state.</p></td>
</tr>
</tbody>
</table>

</div></td>
</tr>
<tr class="even">
<td><p>DOMAIN</p></td>
<td><p>Domain name. This parameter is required if you set TOPOLOGY=2.</p></td>
</tr>
<tr class="odd">
<td><p>COMPUTERNAME</p></td>
<td><p>Name of the server that is hosting the SDN Manager. This parameter is required is you set TOPOLOGY=2. The parameter is used to create a machine account in the SQL Server database.</p></td>
</tr>
<tr class="even">
<td><p>DATABASE_SERVER</p></td>
<td><p>(e.g. DATABASE_SERVER=ContosoBE\Monitoring) specifies the SQL Server as well as SQL instance, if appropriate.</p></td>
</tr>
<tr class="odd">
<td><p>DATABASE</p></td>
<td><p>Name of the database on the SQL Server. The default name is &quot;SDNManager22&quot;.</p></td>
</tr>
<tr class="even">
<td><p>SQL_ADMIN_USER</p></td>
<td><p>Account to use to create and setup the database. Do not specify when using SQL integrated security with the current user running the setup.</p></td>
</tr>
<tr class="odd">
<td><p>SQL_ADMIN_PASSWORD</p></td>
<td><p>Password for the SQL admin account if SQL Authentication is used.</p></td>
</tr>
<tr class="even">
<td><p>SQLUSER</p></td>
<td><p>Account that is used by the SDN Manager to access the SDN Manager database.</p></td>
</tr>
<tr class="odd">
<td><p>SQLPASSWORD</p></td>
<td><p>Password for the SQL account if SQL Authentication is used</p></td>
</tr>
<tr class="even">
<td><p>USE_SQL_AUTH</p></td>
<td><p>Describes the authentication mode that should be used during run time.</p>
<div class="tableSection">
<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Value</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>0</p></td>
<td><p>Use integrated security based on the machine account.</p></td>
</tr>
<tr class="even">
<td><p>1</p></td>
<td><p>Use the SQL credentials provided.</p></td>
</tr>
</tbody>
</table>

</div></td>
</tr>
<tr class="odd">
<td><p>SETUPDB</p></td>
<td><p>Setting this parameter to 1 causes the installer to create the SDN ManagerSDN Manager database. This parameter must be set (SETUPDB=1) for the first installation when a database for call state or settings should be created.</p></td>
</tr>
<tr class="even">
<td><p>REDIS_CONNECTSTRING</p></td>
<td><p>The connect string to access a REDIS cache server. For example, to connect to Azure the connect string might appear as follows:<br />
<br />
myname.redis.cache.windows.net,ssl=true,password=VeryLongKeyCopiedFromPortal</p>
<p>For more information, see <a href="https://azure.microsoft.com/en-us/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache/">How to Use Azure Redis Cache</a></p></td>
</tr>
<tr class="odd">
<td><p>REDIS_PASSWORD</p></td>
<td><p>Password to access the REDIS server. For Microsoft Azure Redis Cache, enter the key provided on the Azure Management panel.</p></td>
</tr>
<tr class="even">
<td><p>USE_DAPI</p></td>
<td><p>When set to true (&quot;USE_DAPI=True&quot;), the text specified for REDIS_PASSWORD will be persisted in the SDNManager.exe.config file as a DPAPI encrypted string, and the SDNManager.exe.config file will contain &quot;&lt;add key=&quot;usedapi&quot; value=&quot;True&quot;/&gt;&quot;. Setting is ignored if TOPOLOGY is not ‘3’ – REDIS only.</p></td>
</tr>
</tbody>
</table>



> [!NOTE]
> Most settings provided during the setup can be modified after installation by editing the SDNManager.exe.config file.



The USE\_DAPI parameter is used to secure the Redis password contained in the SDNManager.exe.config file with DPAPI encryption, and corresponds to the checkbox in figure 1:

Figure 1. Skype for Business SDN Manager Setup

  
![Skype for Business SDN Manager REDIS Settings](../images/Dn785205.18aa5963-b41b-449e-978f-4c8b91ef2520(Office.16).png "Skype for Business SDN Manager REDIS Settings")

## Example unattended installations of the SDN Manager

**Database install**

    msiexec /i SkypeForBusinessSDNManager.msi /quiet  /lv* install.log LOGPATH=c:\LogDir TOPOLOGY=2 DATABASE_SERVER=mysqlserver DEBUGLOGGING=All SETUPDB=1 SQL_ADMIN_USER=sa SQL_ADMIN_PASSWORD=mypwd SQLUSER=simpleuser SQLPASSWORD=simplepassword DOMAIN=contoso COMPUTERNAME=thiscomputername USE_SQL_AUTH=1 DATABASE_NAME=sdndb

**REDIS install**

    msiexec /i SkypeForBusinessSDNManager.msi /quiet  /lv* install.log LOGPATH=c:\LogDir TOPOLOGY=3 REDIS_CONNECTSTRING=redis.contoso.com,ssl=true REDIS_PASSWORD=myredispwd 

## Unattended installation of the Dialog Listener

Following are unattended installation parameters for the Dialog Listener.

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Parameter</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>INSTALLDIR</p></td>
<td><p>Specifies the installation destination for the binaries.</p></td>
</tr>
<tr class="even">
<td><p>CONFIGURATIONURI</p></td>
<td><p>URI to locate the SDN Manager or pool that serves the configuration service http://localhost:9333/DL.</p></td>
</tr>
<tr class="odd">
<td><p>ACCOUNTYPE</p></td>
<td><p>Type of account to run Dialog Listener: &quot;0&quot; for Network Service account and &quot;1&quot; for a user account.</p></td>
</tr>
<tr class="even">
<td><p>APPLIED_SERVICE_USERNAME</p></td>
<td><p>Domain/username for the Dialog Listener Windows Service account.</p></td>
</tr>
<tr class="odd">
<td><p>APPLIED_SERVICE_PASSWORD</p></td>
<td><p>Password for the Dialog Listener Windows Service account.</p></td>
</tr>
<tr class="even">
<td><p>THUMBPRINT</p></td>
<td><p>Thumbprint of the client certificate needed to contact the SDN Manager.</p></td>
</tr>
<tr class="odd">
<td><p>USE_SRV_RECORD_BOOL</p></td>
<td><p>If true, then instead of using the configuration service URI, retrieve the location from the DNS SRV record.</p></td>
</tr>
<tr class="even">
<td><p>SKIPREGISTRATION</p></td>
<td><p>During the installation, a Windows PowerShell script is executed to register the Dialog Listener with the Skype for Business Server. To skip executing this script, set SKIPREGISTRATION=1.</p>
<p>You must manually perform this registration and start the Dialog Listener Windows service. An example of this script can be found under C:\ProgramData\Microsoft\Skype for Business Dialog Listener\Register.ps1.</p></td>
</tr>
</tbody>
</table>



> [!NOTE]
> Most settings provided during the setup can be modified after the installation by editing the DialogListener.exe.config file.




> [!NOTE]
> When DNS Service Location (SRV) records are used by the Dialog Listener, any configured value for the parameter CONFIGURATIONURI is ignored. Instead, the URI specified in the SRV record is used.



An installation using the SKIPREGISTRATION=1 option offers an interesting alternative installation. This script executed has three major purposes: Perform the registration of Skype for Business SDN Interface with Skype for Business, so that it forwards request and response messages to the Dialog Listener service, attempt to activate sending of Incall Quality messages (if available), and to start the service itself.

The registration and activation are only required once for the entire pool and the Dialog Listener Windows service can be started manually as well as using several other options. As the script also uses some lengthy verifications whether the Skype for Business Server is ready and set up correctly, this step could be skipped, speeding up deployment and specifically upgrade processes for experienced administrators.

## Doing an unattended uninstall

To do an unattended uninstall, do the following:

  - **For SDN Manager**: In the command line, type C:\\Windows\\System32\\msiexec.exe /X{60A580F2-7B7A-4665-9696-07BE5D9AF15F}

  - **For Dialog Listener**: In the command line, type C:\\Windows\\System32\\msiexec.exe /X{640967B9-CD8D-455F-BAB1-463EBFC51C38}


> [!NOTE]
> The product code for the Dialog Listener has changed since the previous release.



## See also

- [Installing the SDN Manager](installing-the-sdn-manager.md)
- [Skype for Business SDN Interface Schema Reference](skype-for-business-sdn-interface-schema-reference.md)

