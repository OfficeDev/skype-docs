# myGroups2

 _**Applies to:** Skype for Business 2015_


A collection of groups in the contact list of the logged-on user.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

This collection is read-only: it can be used only to view existing group resources. New groups can becreated using the Skype for Business desktop client.

### Properties



None

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|defaultGroup|Represents a persistent, system-created group where a user's contacts are placed by default.|
|delegatesGroup|Represents a group of users designated as delegates.|
|delegatorsGroup|Represents a group of users designated as delegates.|
|distributionGroup|Represents a persistent, pre-existing group of contacts.|
|group|Represents a user's persistent, personal group.|
|group|Represents a user's persistent, personal group.|
|myOrganizationGroup|Represents a group of user's organization contacts|
|pinnedGroup|Represents a system-created group of contacts that the user pins or that the user frequentlycommunicates and collaborates with.|
|distributionGroup|Represents a persistent, pre-existing group of contacts.|
|group|Represents a user's persistent, personal group.|
|group|Represents a user's persistent, personal group.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Events
<a name="sectionSection2"></a>

### Added



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|group|High|people|Delivered when a roaming group is added to my groups.</p><p></p>|
|defaultGroup|Low|people|Delivered when a default group is added to my groups.</p><p></p>|
|pinnedGroup|Low|people|Delivered when a pinned group is added to my groups.</p><p></p>|
|distributionGroup|High|people|Delivered when a distribution group is added to my groups.</p><p></p>|
|delegatesGroup|High|people|Delivered when a delegate group is added to my groups.</p><p></p>|
|delegatorsGroup|High|people|Delivered when a delegator group is added to my groups.</p><p></p>|
|myOrganizationGroup|Low|people|Delivered when MyOrganizationGroup is added to my groups.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "myOrganizationGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/myOrganizationGroup"
          },
          "in" : {
            "rel" : "myGroups",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups"
          },
          "type" : "added"
        }
      ]
    }
  ]
}


### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|delegatorsGroup|High|people|Delivered when a delegator group is updated in my groups.</p><p></p>|
|myOrganizationGroup|Low|people|Delivered when a MyOrganizationGroupis updated in my groups.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "myOrganizationGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/myOrganizationGroup"
          },
          "in" : {
            "rel" : "myGroups",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


### Deleted



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|group|High|people|Delivered when a roaming group is deleted from my groups.</p><p></p>|
|defaultGroup|Low|people|Delivered when a default group is deleted from my groups.</p><p></p>|
|pinnedGroup|Low|people|Delivered when a pinned group is deleted from my groups.</p><p></p>|
|distributionGroup|High|people|Delivered when a distribution group is deleted from my groups.</p><p></p>|
|delegatorsGroup|High|people|Delivered when a delegator group is removed from my groups.</p><p></p>|
Sample of returned event data.
This sample is given only as an illustration of event syntax. The semantic content is not guaranteed to correspond to a valid scenario.
{
  "_links" : {
    "self" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=1"
    },
    "next" : {
      "href" : "http://sample:80/ucwa/v1/applications/appId/events?ack=2"
    }
  },
  "sender" : [
    {
      "rel" : "people",
      "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/people",
      "events" : [
        {
          "link" : {
            "rel" : "delegatorsGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/delegatorsGroup"
          },
          "in" : {
            "rel" : "myGroups",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups"
          },
          "type" : "deleted"
        }
      ]
    }
  ]
}


## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of the collection of groups in embedded or link form. The default returns them in embedded form.

#### Query parameters




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|expand|Optional query parameter to override default behavior when returning a collection of groups. Bydefault, a collection of all groups will be returned with each group embedded inside. If thisquery parameter is set to "false" then the collection of groups will be returned with just links.|No|


#### Request body



None


#### Response body



The response from a GET request contains the properties and links shown in the Properties and Links sections at the top of this page.

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The user does not have sufficient privileges to access the contact list.|
|Forbidden|403|None|The user does not have sufficient privileges to access pending contacts|
|ServiceFailure|500|InvalidExchangeServerVersion|Invalid exchange server version.The exchange mailbox of the server might have moved to an unsupported version for the required feature.|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples



Only server-supplied query parameters, if any, are shown in the request sample.

#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 5190
{
  "rel" : "myGroups",
  "_links" : {
    "self" : {
      "href" : "/ucwa/v1/applications/192/groups"
    },
    "distributionGroup" : [
      {
        "href" : "/ucwa/v1/applications/192/groups/distributionGroup"
      }
    ],
    "group" : [
      {
        "href" : "/ucwa/v1/applications/192/groups/group"
      },
      {
        "href" : "/ucwa/v1/applications/192/groups/group"
      }
    ],
    "defaultGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/defaultGroup"
    },
    "delegatesGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/delegatesGroup"
    },
    "delegatorsGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/delegatorsGroup"
    },
    "myOrganizationGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/myOrganizationGroup"
    },
    "pinnedGroup" : {
      "href" : "/ucwa/v1/applications/192/groups/pinnedGroup"
    }
  },
  "_embedded" : {
    "distributionGroup" : [
      {
        "rel" : "distributionGroup",
        "uri" : "sip:mypersonalgroup@contoso.com",
        "id" : "7",
        "name" : "MyPersonalGroup",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/groups/distributionGroup"
          },
          "addToContactList" : {
            "href" : "/ucwa/v1/applications/192/groups/addToContactList"
          },
          "expandDistributionGroup" : {
            "href" : "/ucwa/v1/applications/192/groups/distributionGroup/expandDistributionGroup"
          },
          "removeFromContactList" : {
            "href" : "/ucwa/v1/applications/192/groups/removeFromContactList"
          },
          "subscribeToGroupPresence" : {
            "href" : "/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence"
          }
        },
        "_embedded" : {
          "contact" : [
            {
              "rel" : "contact",
              "company" : "Contoso Corp.",
              "department" : "Engineering",
              "emailAddresses" : [
                "Alex.Doe@contoso.com"
              ],
              "homePhoneNumber" : "tel:+19185550107",
              "sourceNetworkIconUrl" : "https://images.contoso.com/logo_16x16.png",
              "mobilePhoneNumber" : "tel:4255551212;phone-context=defaultprofile",
              "name" : "Alex Doe",
              "office" : "tel:+1425554321;ext=54321",
              "otherPhoneNumber" : "tel:+19195558194",
              "sourceNetwork" : "SameEnterprise",
              "title" : "Engineer 2",
              "type" : "User",
              "uri" : "sip:alex@contoso.com",
              "workPhoneNumber" : "tel:+1425554321;ext=54321",
              "_links" : {
                "self" : {
                  "href" : "/ucwa/v1/applications/192/people/282"
                },
                "contactLocation" : {
                  "href" : "/ucwa/v1/applications/192/people/282/contactLocation"
                },
                "contactNote" : {
                  "href" : "/ucwa/v1/applications/192/people/282/contactNote"
                },
                "contactPhoto" : {
                  "href" : "/ucwa/v1/applications/192/people/282/contactPhoto"
                },
                "contactPresence" : {
                  "href" : "/ucwa/v1/applications/192/people/282/contactPresence"
                },
                "contactPrivacyRelationship" : {
                  "href" : "/ucwa/v1/applications/192/people/282/contactPrivacyRelationship"
                },
                "contactSupportedModalities" : {
                  "href" : "/ucwa/v1/applications/192/people/282/contactSupportedModalities"
                }
              }
            }
          ],
          "distributionGroup" : [
            {
              "rel" : "distributionGroup",
              "uri" : "sip:mypersonalgroup@contoso.com",
              "id" : "7",
              "name" : "MyPersonalGroup",
              "_links" : {
                "self" : {
                  "href" : "/ucwa/v1/applications/192/groups/distributionGroup"
                },
                "addToContactList" : {
                  "href" : "/ucwa/v1/applications/192/groups/addToContactList"
                },
                "expandDistributionGroup" : {
                  "href" : "/ucwa/v1/applications/192/groups/distributionGroup/expandDistributionGroup"
                },
                "removeFromContactList" : {
                  "href" : "/ucwa/v1/applications/192/groups/removeFromContactList"
                },
                "subscribeToGroupPresence" : {
                  "href" : "/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence"
                }
              },
              "_embedded" : {
                "contact" : [
                  {
                    "rel" : "contact",
                    "company" : "Contoso Corp.",
                    "department" : "Engineering",
                    "emailAddresses" : [
                      "Alex.Doe@contoso.com"
                    ],
                    "homePhoneNumber" : "tel:+19185550107",
                    "sourceNetworkIconUrl" : "https://images.contoso.com/logo_16x16.png",
                    "mobilePhoneNumber" : "tel:4255551212;phone-context=defaultprofile",
                    "name" : "Alex Doe",
                    "office" : "tel:+1425554321;ext=54321",
                    "otherPhoneNumber" : "tel:+19195558194",
                    "sourceNetwork" : "SameEnterprise",
                    "title" : "Engineer 2",
                    "type" : "User",
                    "uri" : "sip:alex@contoso.com",
                    "workPhoneNumber" : "tel:+1425554321;ext=54321",
                    "_links" : {
                      "self" : {
                        "href" : "/ucwa/v1/applications/192/people/282"
                      },
                      "contactLocation" : {
                        "href" : "/ucwa/v1/applications/192/people/282/contactLocation"
                      },
                      "contactNote" : {
                        "href" : "/ucwa/v1/applications/192/people/282/contactNote"
                      },
                      "contactPhoto" : {
                        "href" : "/ucwa/v1/applications/192/people/282/contactPhoto"
                      },
                      "contactPresence" : {
                        "href" : "/ucwa/v1/applications/192/people/282/contactPresence"
                      },
                      "contactPrivacyRelationship" : {
                        "href" : "/ucwa/v1/applications/192/people/282/contactPrivacyRelationship"
                      },
                      "contactSupportedModalities" : {
                        "href" : "/ucwa/v1/applications/192/people/282/contactSupportedModalities"
                      }
                    }
                  }
                ],
                "distributionGroup" : [
                  {
                    "rel" : "distributionGroup",
                    "uri" : "sip:mypersonalgroup@contoso.com",
                    "id" : "7",
                    "name" : "MyPersonalGroup",
                    "_links" : {
                      "self" : {
                        "href" : "/ucwa/v1/applications/192/groups/distributionGroup"
                      },
                      "addToContactList" : {
                        "href" : "/ucwa/v1/applications/192/groups/addToContactList"
                      },
                      "expandDistributionGroup" : {
                        "href" : "/ucwa/v1/applications/192/groups/distributionGroup/expandDistributionGroup"
                      },
                      "removeFromContactList" : {
                        "href" : "/ucwa/v1/applications/192/groups/removeFromContactList"
                      },
                      "subscribeToGroupPresence" : {
                        "href" : "/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence"
                      }
                    },
                    "_embedded" : {
                      "contact" : [
                        {
                          "_links" : {
                            "self" : {
                              "href" : "/ucwa/v1/applications/192/people/282"
                            }
                          }
                        }
                      ],
                      "distributionGroup" : [
                        {
                          "_links" : {
                            "self" : {
                              "href" : "/ucwa/v1/applications/192/groups/distributionGroup"
                            }
                          }
                        }
                      ]
                    }
                  }
                ]
              }
            }
          ]
        }
      }
    ],
    "group" : [
      {
        "rel" : "group",
        "id" : "7",
        "name" : "MyPersonalGroup",
        "_links" : {
          "self" : {
            "href" : "/ucwa/v1/applications/192/groups/group"
          },
          "groupContacts" : {
            "href" : "/ucwa/v1/applications/192/contacts"
          },
          "subscribeToGroupPresence" : {
            "href" : "/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence"
          }
        }
      }
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 6585
<?xml version="1.0" encoding="utf-8"?>
<resource rel="myGroups" href="/ucwa/v1/applications/192/groups" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
  <link rel="distributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup" />
  <link rel="group" href="/ucwa/v1/applications/192/groups/group" />
  <link rel="group" href="/ucwa/v1/applications/192/groups/group" />
  <link rel="defaultGroup" href="/ucwa/v1/applications/192/groups/defaultGroup" />
  <link rel="delegatesGroup" href="/ucwa/v1/applications/192/groups/delegatesGroup" />
  <link rel="delegatorsGroup" href="/ucwa/v1/applications/192/groups/delegatorsGroup" />
  <link rel="myOrganizationGroup" href="/ucwa/v1/applications/192/groups/myOrganizationGroup" />
  <link rel="pinnedGroup" href="/ucwa/v1/applications/192/groups/pinnedGroup" />
  <property name="rel">myGroups</property>
  <resource rel="distributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup">
    <link rel="addToContactList" href="/ucwa/v1/applications/192/groups/addToContactList" />
    <link rel="expandDistributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup/expandDistributionGroup" />
    <link rel="removeFromContactList" href="/ucwa/v1/applications/192/groups/removeFromContactList" />
    <link rel="subscribeToGroupPresence" href="/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence" />
    <property name="rel">distributionGroup</property>
    <property name="uri">sip:mypersonalgroup@contoso.com</property>
    <property name="id">7</property>
    <property name="name">MyPersonalGroup</property>
    <resource rel="contact" href="/ucwa/v1/applications/192/people/282">
      <link rel="contactLocation" href="/ucwa/v1/applications/192/people/282/contactLocation" />
      <link rel="contactNote" href="/ucwa/v1/applications/192/people/282/contactNote" />
      <link rel="contactPhoto" href="/ucwa/v1/applications/192/people/282/contactPhoto" />
      <link rel="contactPresence" href="/ucwa/v1/applications/192/people/282/contactPresence" />
      <link rel="contactPrivacyRelationship" href="/ucwa/v1/applications/192/people/282/contactPrivacyRelationship" />
      <link rel="contactSupportedModalities" href="/ucwa/v1/applications/192/people/282/contactSupportedModalities" />
      <property name="rel">contact</property>
      <property name="company">Contoso Corp.</property>
      <property name="department">Engineering</property>
      <propertyList name="emailAddresses">
        <item>Alex.Doe@contoso.com</item>
      </propertyList>
      <property name="homePhoneNumber">tel:+19185550107</property>
      <property name="sourceNetworkIconUrl">https://images.contoso.com/logo_16x16.png</property>
      <property name="mobilePhoneNumber">tel:4255551212;phone-context=defaultprofile</property>
      <property name="name">Alex Doe</property>
      <property name="office">tel:+1425554321;ext=54321</property>
      <property name="otherPhoneNumber">tel:+19195558194</property>
      <property name="sourceNetwork">SameEnterprise</property>
      <property name="title">Engineer 2</property>
      <property name="type">User</property>
      <property name="uri">sip:alex@contoso.com</property>
      <property name="workPhoneNumber">tel:+1425554321;ext=54321</property>
    </resource>
    <resource rel="distributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup">
      <link rel="addToContactList" href="/ucwa/v1/applications/192/groups/addToContactList" />
      <link rel="expandDistributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup/expandDistributionGroup" />
      <link rel="removeFromContactList" href="/ucwa/v1/applications/192/groups/removeFromContactList" />
      <link rel="subscribeToGroupPresence" href="/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence" />
      <property name="rel">distributionGroup</property>
      <property name="uri">sip:mypersonalgroup@contoso.com</property>
      <property name="id">7</property>
      <property name="name">MyPersonalGroup</property>
      <resource rel="contact" href="/ucwa/v1/applications/192/people/282">
        <link rel="contactLocation" href="/ucwa/v1/applications/192/people/282/contactLocation" />
        <link rel="contactNote" href="/ucwa/v1/applications/192/people/282/contactNote" />
        <link rel="contactPhoto" href="/ucwa/v1/applications/192/people/282/contactPhoto" />
        <link rel="contactPresence" href="/ucwa/v1/applications/192/people/282/contactPresence" />
        <link rel="contactPrivacyRelationship" href="/ucwa/v1/applications/192/people/282/contactPrivacyRelationship" />
        <link rel="contactSupportedModalities" href="/ucwa/v1/applications/192/people/282/contactSupportedModalities" />
        <property name="rel">contact</property>
        <property name="company">Contoso Corp.</property>
        <property name="department">Engineering</property>
        <propertyList name="emailAddresses">
          <item>Alex.Doe@contoso.com</item>
        </propertyList>
        <property name="homePhoneNumber">tel:+19185550107</property>
        <property name="sourceNetworkIconUrl">https://images.contoso.com/logo_16x16.png</property>
        <property name="mobilePhoneNumber">tel:4255551212;phone-context=defaultprofile</property>
        <property name="name">Alex Doe</property>
        <property name="office">tel:+1425554321;ext=54321</property>
        <property name="otherPhoneNumber">tel:+19195558194</property>
        <property name="sourceNetwork">SameEnterprise</property>
        <property name="title">Engineer 2</property>
        <property name="type">User</property>
        <property name="uri">sip:alex@contoso.com</property>
        <property name="workPhoneNumber">tel:+1425554321;ext=54321</property>
      </resource>
      <resource rel="distributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup">
        <link rel="addToContactList" href="/ucwa/v1/applications/192/groups/addToContactList" />
        <link rel="expandDistributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup/expandDistributionGroup" />
        <link rel="removeFromContactList" href="/ucwa/v1/applications/192/groups/removeFromContactList" />
        <link rel="subscribeToGroupPresence" href="/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence" />
        <property name="rel">distributionGroup</property>
        <property name="uri">sip:mypersonalgroup@contoso.com</property>
        <property name="id">7</property>
        <property name="name">MyPersonalGroup</property>
        <resource rel="contact" href="/ucwa/v1/applications/192/people/282" />
        <resource rel="distributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup" />
      </resource>
    </resource>
  </resource>
  <resource rel="group" href="/ucwa/v1/applications/192/groups/group">
    <link rel="groupContacts" href="/ucwa/v1/applications/192/contacts" />
    <link rel="subscribeToGroupPresence" href="/ucwa/v1/applications/192/groups/group/subscribeToGroupPresence" />
    <property name="rel">group</property>
    <property name="id">7</property>
    <property name="name">MyPersonalGroup</property>
  </resource>
</resource>
```



### POST




Creates new contact group

#### Request body




|**Name**|**Description**|**Required?**|
|:-----|:-----|:-----|
|displayName|Group display nameThe maximum length is 256 characters. String|Yes|

#### Response body



None

#### Synchronous errors



The errors below (if any) are specific to this resource. Generic errors that can apply to any resource are covered in [Generic synchronous errors](GenericSynchronousErrors.md).

|**Error**|**Code**|**Subcode**|**Description**|
|:-----|:-----|:-----|:-----|
|Forbidden|403|None|The user does not have sufficient privileges to modify the contact list.|
|Conflict|409|None|Conflict occurs when the resource is not in the proper state to accept the request.|
|Forbidden|403|None|The user does not have sufficient privileges to access or set delegates|
|Forbidden|403|None|The user does not have sufficient privileges to access or expand Distribution Groups|
|ServiceFailure|500|CallbackChannelError|The remote event channel is not reachable|
|Conflict|409|AlreadyExists|The already exists error.|
|Conflict|409|TooManyGroups|The too many groups error.|
|Conflict|409|None|Un-supported Service/Resource/API error.|
|Gone|410|CannotRedirect|Cannot redirect since there is no back up pool configured.|

#### Examples




#### JSON Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/groups HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/json
Content-Length: 29
{
  &quot;displayName&quot; : &quot;samplevalue&quot;
}
```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


#### XML Request




```
Post https://fe1.contoso.com:443/ucwa/v1/applications/192/groups HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Content-Type: application/xml
Content-Length: 158
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;input xmlns=&quot;https://schemas.microsoft.com/rtc/2012/03/ucwa&quot;&gt;
  &lt;property name=&quot;displayName&quot;&gt;samplevalue&lt;/property&gt;
&lt;/input&gt;
```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 204 No Content

```


