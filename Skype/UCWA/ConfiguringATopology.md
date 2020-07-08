
# Configuring a topology
Learn how to configure a topology that supports Microsoft Unified Communications Web API 2.0.


 _**Applies to:** Skype for Business 2015_

## Allowed domains


### What is a domain?

Browser-based UCWA 2.0 applications require server-side configuration before they will work. For security, an allowed list is maintained on the server to protect Skype for Business Server from malicious third-party domains (see [Cross-domain IFRAME](Cross_domainIFRAME.md) for more information). Domains, or origins, are defined in [RFC 6454](http://tools.ietf.org/html/rfc6454) as scheme, host, and port.



|**Item**|**Example**|
|:-----|:-----|
|Scheme|https|
|Host|apps.contoso.com|
|Port|80|
According to the definition, all three of the following are different:


- https://contoso.com
 
- https://contoso.com:8080
 
- https://contoso.com
 
- https://app.contoso.com
 
Given these nuances, an admin must be careful when editing the allowed list.

The samples will indicate that the host domain is not on the allowed list by alerting the following string, sent by the server in the headers of a 403 response:




```
Service does not allow a cross domain request from this origin.
```


### Viewing the allowed list

From the Skype for Business Management Shell on each server (front end, edge, and director), execute the following command:


```
Get-CsWebServiceConfiguration | select CrossDomainAuthorizationList
```


### Editing the allowed list

From the Skype for Business Management Shell on each server (front end, edge, and director), execute the following commands (replacing the text in {} with your values):


```
$x = New-CsWebOrigin -Url "{https://apps.contoso.com}"
Set-CsWebServiceConfiguration -Identity "{YOUR_IDENTITY}" -CrossDomainAuthorizationList @{Add=$x}

```

If you do not know the value of **Identity** for your Skype for Business Server, you can run the following command to see all identities configured on the server:




```
Get-CsWebServiceConfiguration | select identity

```

