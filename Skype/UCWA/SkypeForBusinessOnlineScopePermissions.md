
# Skype for Business Online scope permissions
Provides a reference of permission scopes that a user can grant in UCWA 2.0.



## Use scopes to specify access to Skype for Business data

Permission scopes limit access to Skype for Business data to a specific level. A scope is a combination of a resource or capability and an operation in the format _resource.operation_. For example, `User.ReadWrite` specifies the resource [user](user_ref.md) and the operations Read and Write. There are no default scopes.

You can specify scopes for your application in the Azure Management Portal, or declare them in your application manifest. Scope information is stored in the application manifest. The format of the manifest is JSON. To modify the application manifest directly:


- Log on to the Azure Management Portal.
 
- View the application definition.
 
- Download the application manifest.
 
- Open the manifest and modify it according to the needs of the application.
 
To learn more about configuring applications, see [Integrating Applications with Azure Active Directory](https://azure.microsoft.com/en-us/documentation/articles/active-directory-integrating-applications/).


## Skype for Business scope permissions

The Skype for Business scope permissions are shown in the following table:



|**Scope**|**Permission**|**Description**|**Requires admin consent**|
|:-----|:-----|:-----|:-----|
|Contacts.ReadWrite|Read and manage user contacts and groups|Allows the application to read and update user contacts and groups|No|
|Conversations.Initiate|Initiate conversations and join meetings|Allows the application to initiate instant messages, audio, video, and desktop sharing conversations; and join meetings on-behalf of the signed-in user|No|
|Conversations.Receive|Receive conversation invites|Allows the app to receive instant messages, audio, video, and desktop sharing invitations on-behalf of the signed-in user|No|
|Meetings.ReadWrite|Create online meetings|Allows the application to create Online meetings on-behalf of the signed-in user|No|
|User.ReadWrite|Read/write Skype user information|Allows the app to read and update presence, photo, location, note, call forwarding settings of the signed-in user|No|

>Note: The adminstrator of a tenant where your application is registered can accept permission requests on behalf of all users, so users do not see the permissions screen. To learn about the tenant admin consent flow, read [Tenant Administrator Consent Flow](https://msdn.microsoft.com/en-us/skype/websdk/docs/developwebsdkappsforsfbonline#tenant-administrator-consent-flow)
