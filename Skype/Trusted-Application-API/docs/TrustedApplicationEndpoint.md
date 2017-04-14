# Set up a Trusted Application Endpoint 

Tenant Admin Provisioning includes setting up the trusted endpoints and tenant admin consent.
Please refer [Tenant Admin Consent](./TenantAdminConsent.md) for a tenant to consent to the application.

You can easily register **Trusted Application Endpoints** by using the PowerShell cmdlets.
General information about PowerShell cmdlets usage can be found in [Using Windows PowerShell to manage Skype for Business Online](https://technet.microsoft.com/en-us/library/dn362831.aspx).  You will need to complete the following steps to run the admin PowerShell:

1. [Download and install the Skype for Business Online Connector module](http://go.microsoft.com/fwlink/?LinkId=294688)
2. Open Windows PowerShell as Administrator and run the following:

```PowerShell
Import-PSSession (New-CsOnlineSession -Credential Get-Credential)
```

For more information see: [Connecting to Skype for Business Online by using Windows PowerShell](https://technet.microsoft.com/en-us/library/dn362795.aspx)

## Managing Trusted Application Endpoint With PowerShell

>Note: For assigning a PSTN phone number to a Trusted Endpoint, you will need to follow the steps below acquire a service number that will be assigned to Trusted Application Endpoint. **Assigning a phone number is optional.  If assigning a phone number to your endpoint, acquiring a service number should be completed before running the Powershell cmdlets.  See details below**.

 1. Follow the documentation to connect to the [Skype for Business PowerShell cmdlets](https://technet.microsoft.com/en-us/library/dn362831.aspx)
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

>Note: For PSTN, Assign the **service numbers** to the trusted application endpoint using _New-CsOnlineApplicationEndpoint PhoneNumber_ parameter. PhoneNumber is not required.
 
## Detailed Explanation of Parameters

- **ApplicationId**: The Azure ApplicationID/ClientID from the Azure portal registration steps.

- **Name**: A friendly name of your application within Skype for Business Online.

- **Tenant**: The Tenant ID of the tenant where you are registering a trusted application endpoint.

- **Uri**: Sip Uri that identifies the tenant specific endpoint for the application. This must be a unique URI that does not conflict with an existing user in the tenant. Requests sent to this endpoint will trigger the **Trusted Application API** sending an event to the application, indicating that someone has sent a request. Eg. helpdesk@contoso.com


### Example

The following PowerShell cmdlet creates a new application endpoint.

```PowerShell
New-CsOnlineApplicationEndpoint -Uri "sip:sample@domain.com" -ApplicationId "44ff763b-5d1f-40ab-95bf-f31kc8757998" -Name "SampleApp" -PhoneNumber "19841110909"
```
