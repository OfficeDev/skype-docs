# distributionGroup

 _**Applies to:** Skype for Business 2015_


Represents a persistent, pre-existing group of contacts.
            

## Web Link
<a name = "sectionSection0"> </a>

For more on web links, see [Web links](WebLinks.md).


|**Name**|**Description**|
|:-----|:-----|
|rel|The resource that this link points to. In JSON, this is the outer container.|
|href|The location of this resource on the server, and the target of an HTTP operation.|

## Resource description
<a name = "sectionSection1"> </a>

A distribution group is a mail-enabled Active Directory group object.An application can subscribe to updates from member contacts. Updates include [presence](presence_ref.md), [location](location_ref.md),or [note](note_ref.md) changes for a specific contact.Currently, distributionGroup is a read-only resource.Unlike other group types, it cannot be managed by any Skype for Business endpoint.An application must call [startOrRefreshSubscriptionToContactsAndGroups](startOrRefreshSubscriptionToContactsAndGroups_ref.md) before it can receive eventswhen a distributionGroup is created, modified, or removed.

### Properties



|**Name**|**Description**|
|:-----|:-----|
|uri|The distribution group's address.|
|id|The group's ID.|
|name|The group's name.The maximum length is 256 characters.|

### Links



This resource can have the following relationships.

|**Link**|**Description**|
|:-----|:-----|
|self|The link to the current resource.|
|addToContactList|Adds a [distributionGroup](distributionGroup_ref.md) into contact list.|
|expandDistributionGroup|Expands a distribution group and returns the set of [contact](contact_ref.md) resources it contains.|
|removeFromContactList|Removes a [distributionGroup](distributionGroup_ref.md) from contact list.|
|subscribeToGroupPresence|Subscribes to the presence information of all the contacts in a particular group.|
|contact|Represents a person or service that the user can communicate and collaborate with.|
|distributionGroup|Represents a persistent, pre-existing group of contacts.|

### Azure Active Directory scopes for online applications



The user must have at least one of these scopes for operations on the resource to be allowed.
|**Scope**|**Permission**|**Description**|
|:-----|:-----|:-----|
|Contacts.ReadWrite|Read/write Skype user contacts and groups|Allows the app to read and write Skype user contacts and groups|

## Events
<a name="sectionSection2"></a>

### Updated



|**Resource**|**Priority**|**Sender**|**Reason**|
|:-----|:-----|:-----|:-----|
|distributionGroup|High|people|Indicates that the distribution group has been updated. The application can decide to fetch theupdated information.</p><p></p>|
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
            "rel" : "distributionGroup",
            "href" : "https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/distributionGroup"
          },
          "type" : "updated"
        }
      ]
    }
  ]
}


## Operations



<a name="sectionSection2"></a>

### GET




Returns a representation of a persistent, pre-existing group of contacts.

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




#### JSON Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/distributionGroup HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/json

```


#### JSON Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 5926
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
    ]
  }
}
```


#### XML Request




```
Get https://fe1.contoso.com:443/ucwa/v1/applications/192/groups/distributionGroup HTTP/1.1
Authorization: Bearer cwt=PHNhbWw6QXNzZXJ0aW9uIHhtbG5...uZm8
Host: fe1.contoso.com
Accept: application/xml

```


#### XML Response



This sample is given only as an illustration of response syntax. The semantic content is not guaranteed to correspond to a valid scenario.
```
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: 7690
<?xml version="1.0" encoding="utf-8"?>
<resource rel="distributionGroup" href="/ucwa/v1/applications/192/groups/distributionGroup" xmlns="https://schemas.microsoft.com/rtc/2012/03/ucwa">
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
</resource>
```


