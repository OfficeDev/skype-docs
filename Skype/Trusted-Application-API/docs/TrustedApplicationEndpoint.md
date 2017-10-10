# Set up a Trusted Application Endpoint 

Tenant Admin Provisioning includes both setting up the **trusted endpoints** and **tenant admin consent**. This article will   help you set up  the **Trusted Application Endpoints**. 
Please refer [Tenant Admin Consent](./TenantAdminConsent.md) for a tenant to consent to the application.

You can easily register **Trusted Application Endpoints** by using the PowerShell cmdlets.
General information about PowerShell cmdlets usage can be found in [Using Windows PowerShell to manage Skype for Business Online](https://technet.microsoft.com/en-us/library/dn362831.aspx). 

## Prerequisite

1. [Download and install the Skype for Business Online Connector module](http://go.microsoft.com/fwlink/?LinkId=294688)

2. Please make sure that the execution policy of PowerShell is not set to **Restricted**. Open Windows PowerShell and run **Get-ExecutionPolicy** cmdlet to get the current execution policy. Please read [Execution Policies](https://msdn.microsoft.com/en-us/powershell/reference/5.1/microsoft.powershell.core/about/about_execution_policies) page to set appropriate PowerShell policies. 

## Managing Trusted Application Endpoint With PowerShell

1. Open Windows PowerShell as an administrator and run the following:

```PowerShell
Import-PSSession (New-CsOnlineSession -Credential (Get-Credential))
```
2. You will be prompted with a windows PowerShell credential dialog box. Sign in using the tenant credentials. 

Successful completion of the above mentioned steps will create a Windows PowerShell session that makes a connection to Skype for Business Online. Now you can use PowerShell cmdlets to set up the application endpoint. 

For more information see: [Connecting to Skype for Business Online by using Windows PowerShell](https://technet.microsoft.com/en-us/library/dn362795.aspx)

## PowerShell cmdlets to setup Trusted Application Endpoint

>Note: **Assigning a service phone number to the Trusted Application Endpoint is optional**, If you want to assign a service number to the Trusted Application Endpoint,  make sure that you complete the steps mentioned in [Acquire the service phone number
 ](./AquireServiceNumber.md) to acquire a service phone number before proceeding to the following section. 
For **PSTN** or samples, assign the **service numbers** to the trusted application endpoint using cmdlets with **PhoneNumber parameter**, which is otherwise optional. Please refer examples at the end of this page. 

 The following cmdlets can be used to setup trusted application endpoints:

- **New-CsOnlineApplicationEndpoint** - It creates a new application endpoint.


|**Parameters**|**Required**|**Type**|**Description**|
|:-----|:-----|:-----|:-----|
|Name|Required|String|Friendly name for Application endpoint|
|ApplicationId|Required|Guid|Unique application Id that this endpoint will use.|
|Uri|Required|String|The SipUri for the Endpoint. SIP Uri must be lowercase.|
|PhoneNumber|Optional|String|Phone number for the endpoint.|


- **Get-CsOnlineApplicationEndpoint** - It is used to fetch the application endpoints for the tenants.

| Parameters     | Required | Type   | Description                                       |
| ---------------|:---------|:------:| -------------------------------------------------:|
| Uri           | Required | String | The SipUri for the Endpoint.        |

- **Set-CsOnlineApplicationEndpoint** - It is used to update the application endpoint.

| Parameters     | Required | Type   | Description                                       |
| ---------------|:---------|:------:| -------------------------------------------------:|
| Uri            | Required | String | The SipUri for the Endpoint. SIP Uri must be lowercase. |
| PhoneNumber    | Optional | String |    Phone number for the endpoint.    |

- **Remove-CsOnlineApplicationEndpoint** - It is used to remove the application endpoint.

| Parameters     | Required | Type   | Description                                       |
| ---------------|:---------|:------:| -------------------------------------------------:|
| Uri            | Required | String | The SipUri for the Endpoint. SIP Uri must be lowercase.        |

## Detailed Explanation of Parameters

- **ApplicationId**: The Azure ApplicationID/ClientID from the Azure portal registration steps.

- **Name**: A friendly name of your application within Skype for Business Online.

- **Uri**: Sip Uri that identifies the tenant specific endpoint for the application. This must be a unique URI that does not conflict with an existing user in the tenant. Requests sent to this endpoint will trigger the **Trusted Application API** sending an event to the application, indicating that someone has sent a request. Eg. helpdesk@contoso.com


### Example

- The following PowerShell cmdlet creates a new application endpoint and assigns a service phone number as well.

```PowerShell
 New-CsOnlineApplicationEndpoint -Uri "sip:avbridgetest@metio.onmicrosoft.com" -ApplicationId "8f851ab7-e245-4ec2-ac8f-7db14cc8fcf7" -Name "AVBridgeTest" -PhoneNumber +14256160796
```
- The following PowerShell cmdlet updates the exsisting application endpoint by assigning new service phone number.

```PowerShell
Set-CsOnlineApplicationEndpoint -PhoneNumber +14256160796
  ```
