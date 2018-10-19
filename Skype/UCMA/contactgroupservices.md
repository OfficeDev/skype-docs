---
title: ContactGroupServices
TOCTitle: ContactGroupServices
ms:assetid: d52cb22d-c305-4463-b9f9-1ade281c0a53
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466041(v=office.16)
ms:contentKeyID: 65239956
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# ContactGroupServices


_**Applies to:** Skype for Business 2015_

**In this article**  
Subscribing to contacts and groups  
Creating and deleting contacts  
Managing contacts and groups  
ContactGroupServices state transitions  

A contact is a unified communication entity that is identified by a SIP URI. One or more contacts can be assembled into logical collections called groups. When a group is created, it is assigned a group ID, which is a server-assigned integer. The Contacts and Groups API in Microsoft Unified Communications Managed API 5.0 is available only for [UserEndpoint](https://msdn.microsoft.com/en-us/library/hh348819\(v=office.16\)) objects.


> [!NOTE]
> <P>The Contacts and Groups API is not available for <A href="https://msdn.microsoft.com/en-us/library/hh384825(v=office.16)">ApplicationEndpoint</A> objects.</P>



The Contacts and Groups API can be used to perform the following operations:

  - Create a new contact.

  - Update the details of a published contact.

  - Delete an existing contact.

  - Create a new group.

  - Associate contacts with one or more groups.

  - Delete an existing group.

  - Find the contacts and groups for a particular endpoint owner.

The [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh381099\(v=office.16\)) class provides a number of methods for managing contacts and groups.

## Subscribing to contacts and groups

The [BeginSubscribe(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh383375\(v=office.16\)) and [EndSubscribe(IAsyncResult)](https://msdn.microsoft.com/en-us/library/hh348322\(v=office.16\)) methods can be used to subscribe to and obtain the endpoint owner’s contacts and groups, and receive updates on any changes to them.

The state of the subscription can be tracked by the **CurrentState()** property and by the [SubscriptionStateChange](https://msdn.microsoft.com/en-us/library/hh382084\(v=office.16\)) event. Following a successful subscription, notifications can be received by registering a handler for the [NotificationReceived](https://msdn.microsoft.com/en-us/library/hh349964\(v=office.16\)) event. The first notification is a complete notification (the [IsFullNotification](https://msdn.microsoft.com/en-us/library/hh366286\(v=office.16\)) property on the [ContactGroupNotificationEventArgs](https://msdn.microsoft.com/en-us/library/hh365894\(v=office.16\)) object) indicates that this notification contains a complete snapshot of the endpoint owner’s contacts and groups. Subsequent notifications denote a change in the existing list of contacts and groups. If the application is to receive a full notification again it can do so by calling the [BeginRefresh](https://msdn.microsoft.com/en-us/library/hh350093\(v=office.16\)) and [EndRefresh](https://msdn.microsoft.com/en-us/library/hh383490\(v=office.16\)) methods.

## Creating and deleting contacts

The [ContactAddOptions](https://msdn.microsoft.com/en-us/library/hh382330\(v=office.16\)) class is new in UCMA 5.0. An application can use the members of this class to add a contact and fill in the following details for the contact:

  - Contact name

  - Additional data or description for the contact

  - An optional container ID where an equivalent Access Control Entry (ACE) will be added for this contact. If none are specified and the user is operating in the privacy mode, an ACE is automatically added.
    
    For more information, see the Relationship level management section in [LocalOwnerPresence](localownerpresence.md).

  - Any groups to which this contact belongs.

Adding a contact to your contact list does not automatically indicate that their presence is being subscribed to. This must be done separately using members of the [RemotePresenceView](https://msdn.microsoft.com/en-us/library/hh381152\(v=office.16\)) class. Deletion of a contact can result in the deletion of an ACE (if one exists) that belongs to that user, unless the user is blocked. If the user is blocked, the ACE is not removed.

## Managing contacts and groups

The [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh383122\(v=office.16\)) property on a [UserEndpoint](https://msdn.microsoft.com/en-us/library/hh348819\(v=office.16\)) instance provides access to a [ContactGroupServices](https://msdn.microsoft.com/en-us/library/hh381099\(v=office.16\)) instance. The methods and properties on the **ContactGroupServices** instance can be used to manage the endpoint owner’s contacts and groups.


> [!NOTE]
> <P>The <A href="https://msdn.microsoft.com/en-us/library/hh384825(v=office.16)">ApplicationEndpoint</A> class has no <STRONG>ContactGroupServices</STRONG> property. Consequently, only a <STRONG>UserEndpoint</STRONG> instance can be used to manage contacts and groups.</P>



### Starting a contacts and groups subscription

The following code example shows how to use the **ContactGroupServices** property on a **UserEndpoint** instance to start a contacts and groups subscription. Also shown in this example are implementations of a callback method named *ContactGroupSubscriptionCompleted* and an event handler for the [NotificationReceived](https://msdn.microsoft.com/en-us/library/hh349964\(v=office.16\)) event.

``` csharp
// Set up contact and group services.
ContactGroupServices cgServices = UserEndpoint.ContactGroupServices;
// Register for the NotificationReceived event.
cgServices.NotificationReceived += CGNotificationReceived;


try
{
  cgServices.BeginSubscribe(ContactGroupSubscriptionCompleted, cgServices);
}
catch(InvalidOperationException)
{
  Console.WriteLine("Contact Group Subscription failed.");
}
catch(RealTimeException exception)
{
  Console.WriteLine(
    "Contact Group Subscription failed. {0}",
    exception.ToString() );
}

void ContactGroupSubscriptionCompleted(IAsyncResult asyncResult)
{
  ContactGroupServices cgServices = asyncResult.AsyncState as ContactGroupServices;
  try
  {
    cgServices.EndSubscribe(asyncResult);
  }

  catch(PublishSubscribeException pse)
  {
    Console.WriteLine(
      "Contact Group Subscription failed. {0}",
      pse.ToString());
  }

  catch(RealTimeException exception)
  {
    Console.WriteLine(
      "Contact Group Subscription failed. {0}",
      exception.ToString());
  }
}

void CGNotificationReceived(object sender, ContactGroupNotificationEventArgs e)
{
  if (e.FullNotification)
  {
    Console.WriteLine("Got full notification of CG.");
  }
  Console.WriteLine("Delta number for this notification = {0}",
    e.DeltaN);
  foreach(NotificationItem<Contact> notification in e.Contacts)
  {
    if (notification.Operation == PublishOperation.Add)
    {
      Console.WriteLine("Contact Added. uri = {0} Name = [1}", 
        notification.Item.Uri, notification.Item.Name);
    }
    // notification.Item.GroupIDs indicate the list of groups this
    // contact is a member of.
  }
  // Similar code for Remove and Update operations. 


  foreach(NotificationItem<Group> notification in e.Groups)
  {
    if (notification.Operation == PublishOperation.Add)
    {
      Console.WriteLine("Group Added. Name = [0} Id = {1}", 
                         notification.Item.Name,
                         notification.Item.Groupid);
    }
  }
  // Similar code for Remove and Update operations.
}
```

After a **ContactGroupServices** instance is successfully initialized with a subscription, the application begins receiving notifications about the existing contacts and groups. After the subscription is completed, the application can add, remove, or update contacts or groups.

### Adding a new contact

The following code example shows the details involved in adding a new contact. The example also shows a callback method named *AddContactCompleted*.

``` csharp
try
{
  cgServices.BeginAddContact(
       "sip:mybuddy@contoso.com", AddContactCompleted, cgServices);
}
catch(InvalidOperationException)
{
  Console.WriteLine("BeginAddContact failed due to an invalid operation.");
}
catch(RealTimeException exception)
{
  Console.WriteLine("BeginAddContact failed.", exception.ToString());
}

void AddContactCompleted(IAsyncResult asyncResult)
{
  ContactGroupServices cgServices = 
asyncResult.AsyncState as ContactGroupServices;
  try
  {
    cgServices.EndAddContact(cgServices);
  }
  catch(PublishSubscribeException pse)
  {
    Console.WriteLine("EndAddContact failed.", pse.ToString());
  }

  catch(RealTimeException exception)
  {
    Console.WriteLine("EndAddContact failed.", exception.ToString());
  }
}
```

### Updating a contact

The application can update a contact by first retrieving the cached contact, updating its properties, and then updating the contact. The reason for this is that the contact already might have been updated due to a recent notification. The delta number of the contact is needed to successfully update the contact. If the delta number does not match, the operation fails. The retrieval of the contact is an asynchronous operation because the cache might not exist at that time, and a refresh operation might be needed from the server to obtain the contacts to satisfy the requested operation.

The cache is time-based and expires in five minutes if unused. On its next use it will be repopulated.

``` csharp
// Assume that the application wants to update the name of a contact. 
try
{
  // Assume that the application has a simple container class for storing information.
  MyContactOperation myContactOperation = new MyContactOperation();
  myContactOperation.Cgs = cgServices;
  myContactOperation.NewName = "updatedname";
  
  cgServices.BeginGetCachedContact(
                "sip:mybuddy@contoso.com", 
                GetCachedContactCompleted, myContactOperation);
}
catch(InvalidOperationException)
{
  Console.WriteLine("GetCachedContact operation failed.");
}
catch(RealTimeException ex)
{
  Console.WriteLine("GetCachedContact failed. {0}", ex.ToString());
}

// Callback passed as an argument in BeginGetCachedContact().
void GetCachedContactCompleted(IAsyncResult asyncResult)
{
  try
  {
    MyContactOperation myContactOperation = 
            asyncResult.AsyncState as MyContactOperation;
    Contact updateContact =   
            myContactOperation.Cgs.EndGetCachedContact(asyncResult);
    updateContact.Name = myContactOperation.NewName;
    try
    {
      myContactOperation.Cgs.BeginUpdateContact(
                updateContact, UpdateContactCompleted,
                myContactOperation);
    }
    catch(InvalidOperationException)
    {
      Console.WriteLine("UpdateContact operation failed.");
    }
    catch(RealTimeException ex)
    {
      Console.WriteLine("UpdateContact failed. {0}", ex.ToString());
    }
  }
  catch(PublishSubscribeException pse)
  {
    Console.WriteLine("GetCachedContact failed. {0}", pse.ToString());
  }
  catch(RealTimeException ex)
  {
    Console.WriteLine("GetCachedContact failed. {0}", ex.ToString());
  }
}

// Callback passed as an argument in BeginUpdateContact().
void UpdateContactCompleted(IAsyncResult asyncResult)
{
  try
  {
    MyContactOperation myContactOperation = 
            asyncResult.AsyncState as MyContactOperation;
    myContactOperation.Cgs.EndUpdateContact(asyncResult);
    Console.WriteLine("Contact Update succeeded.");       
  }
  catch(RealTimeException ex)
  {
    Console.WriteLine("UpdateContact failed. {0}", ex.ToString());
  }
}
```

### Tagging a contact

The following example demonstrates how to tag a contact by adding XML to the [Contact](https://msdn.microsoft.com/en-us/library/hh381065\(v=office.16\)) object, which can be obtained from the [NotificationReceived](https://msdn.microsoft.com/en-us/library/hh349964\(v=office.16\)) event handler, and then publishing the modified **Contact** object.

``` csharp
// If the ContactExtension property has existing XML, keep the existing XML 
// intact (to preserve information published by other endpoints), and
// ensure that the "contactSettings" node is present (adjacent to the root
// node) with a "tag" attribute with a value of "true".
contact.ContactExtension = "<contactSettings tag=\"true\" ></contactSettings>";
endpoint.ContactGroupServices.BeginUpdateContact(contact, UpdateContactCallback, endpoint);

...

private void UpdateContactCallback(IAsyncResult result)
{
  var endpoint = result.AsyncState as UserEndpoint;
  try
  {
    endpoint.ContactGroupServices.EndUpdateContact(result);
  }
  catch
  {
    // Report or handle error.
    ...
  }
}
```

### Untagging a contact

The following code demonstrates how to untag a contact by removing XML from the **Contact** object, which can be obtained from the **NotificationReceived** event handler, and then publishing the modified **Contact** object.

``` csharp
// Remove the "tag" attribute from the "contactSettings" node from the
// ContactExtension property.
string originalExtensionXml = _contact.ContactExtension;
XmlDocument xmlDoc = new XmlDocument();
xmlDoc.LoadXml(originalExtensionXml);
XmlElement root = xmlDoc.DocumentElement;
XmlNodeList nodeList = root.SelectNodes("/contactSettings");
if (nodeList != null && nodeList.Count > 0)
{
  XmlNode node = nodeList[0];
  node.Attributes.Remove(node.Attributes["tag"]); 
}
_contact.ContactExtension = xmlDoc.OuterXml;

_endpoint.ContactGroupServices.BeginUpdateContact(_contact, UpdateContactCallback, _endpoint);

...

private void UpdateContactCallback(IAsyncResult result)
{
  var endpoint = result.AsyncState as UserEndpoint;
  try
  {
    endpoint.ContactGroupServices.EndUpdateContact(result);
  }
  catch
  {
    // Report or handle error.
    ...
  }
}
```

### Adding a distribution list to the contact list

The following code demonstrates how to add a distribution list to the endpoint’s contact list.

``` csharp
// Add a group using the following XML as the groupData and provide the name
// and email address of the distribution group.
string groupDataXml = "<groupExtension groupType=\"dg\"><email>" + groupEmailAddress + "</email></groupExtension>";
endpoint.ContactGroupServices.BeginAddGroup(groupName, groupDataXml, AddGroupCallback, endpoint);

...

private void AddGroupCallback(IAsyncResult result)
{
  var endpoint = result.AsyncState as UserEndpoint;
  try
  {
    endpoint.ContactGroupServices.EndAddGroup(result);
  }
  catch
  {
    // Report or handler error.
    ...
  }
}
```

## ContactGroupServices state transitions

The **ContactGroupServices** state transitions are shown in the following illustration. The state values are the members of the [CollaborationSubscriptionState](https://msdn.microsoft.com/en-us/library/hh380964\(v=office.16\)) enumerated type.

![ContactGroupServices state transitions](images/Dn466041.StateMach_LocalOwnerPresence(Office.16).jpg "ContactGroupServices state transitions")

1.  The transition from **Idle** to **Subscribing** occurs when the application calls [BeginSubscribe(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh383375\(v=office.16\)).

2.  The transition from **Subscribing** to **Subscribed** occurs when the subscription succeeds.

3.  The transition from **Subscribing** to **Terminating** occurs when the subscription fails. This error occurs when the server is busy and cannot respond. The application can try to subscribe later.

4.  The transition from **Subscribing** to **WaitingForRetry** occurs when the subscription could not be successfully created, because the server is busy or the UserServices pool is offline. UCMA 5.0 will automatically refresh the subscription when it recovers.

5.  The transition from **WaitingForRetry** to **Subscribing** occurs when another subscription to the local presentity is attempted for the reason given in step 4.

6.  The transition from **WaitingForRetry** to **Terminating** occurs when the application calls [BeginUnsubscribe(AsyncCallback, Object)](https://msdn.microsoft.com/en-us/library/hh161891\(v=office.16\)) when this session is in the **WaitingForRetry** state.

7.  The transition from **Subscribed** to **WaitingForRetry** occurs for connectivity failures and reasons such as the following.
    
      - The server requests UCMA 5.0 to close this subscription and to create a new subscription by sending a NOTIFY request with an Expires:0 parameter. The server can make this request at any time.
    
      - Route-set recovery begins.
    
      - The server sends a "481 Call leg unavailable" response.
    
      - The endpoint owner’s home server UserServices pool is offline and cannot respond to service requests. UCMA 5.0 automatically recovers when the UserServices pool is online again.

8.  The transition from **Subscribed** to **Terminating** occurs when **BeginUnsubscribe** is called.

9.  The transition from **Terminating** to **Idle** occurs when the call to **BeginUnsubscribe** ends successfully. The subscription can be reused; that is, an application can call **BeginSubscribe** when the state is **Idle**.

