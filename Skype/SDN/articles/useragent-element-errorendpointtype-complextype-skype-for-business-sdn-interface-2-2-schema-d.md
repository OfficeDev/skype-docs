---
title: UserAgent element (ErrorEndPointType complexType) 
TOCTitle: UserAgent element (ErrorEndPointType complexType)
ms:assetid: 3c027501-8a46-98d4-df03-29d8736db129
ms:mtpsurl: https://msdn.microsoft.com/library/Mt171027(v=office.16)
ms:contentKeyID: 65855600
ms.date: 08/24/2015
mtps_version: v=office.16
dev_langs:
- xml
---

# UserAgent element 

(ErrorEndPointType complexType) (Skype for Business SDN Interface 2.2, Schema "D")

Skype for Business client name and version.


**In this article**  
Element information  
Definition  
Elements and attributes  

## Element information

<table>
<colgroup>

</colgroup>
<tbody>
<tr class="odd">
<td><p><strong>Element type</strong></p></td>
<td><p><a href="useragenttype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">UserAgentType</a></p></td>
</tr>
<tr class="even">
<td><p><strong>Namespace</strong></p></td>
<td><p></p></td>
</tr>
<tr class="odd">
<td><p><strong>Schema file</strong></p></td>
<td><p>SDNInterface.Schema.D.XSD</p></td>
</tr>
</tbody>
</table>


## Definition

```xml

    <xs:element name="UserAgent"  type="UserAgentType" minOccurs="0">
    
    </xs:element>
  
```

## Elements and attributes

### Parent elements

<table>
<colgroup>

</colgroup>
<thead>
<tr class="header">
<th><p>Element</p></th>
<th><p>Type</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p><a href="endpoint-element-errortype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">EndPoint</a></p></td>
<td><p><a href="errorendpointtype-complextype-skype-for-business-sdn-interface-2-2-schema-d.md">ErrorEndPointType</a></p></td>
<td><p>Endpoint involved in the ended SIP call.</p></td>
</tr>
</tbody>
</table>


### Child elements

None.

### Attributes

<table>
<colgroup>

</colgroup>
<thead>
<tr class="header">
<th><p>Attribute</p></th>
<th><p>Type</p></th>
<th><p>Required</p></th>
<th><p>Description</p></th>
<th><p>Possible values</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>Type</p></td>
<td><p>xs:unsignedInt</p></td>
<td><p>optional</p></td>
<td><p>Number describing the user agent.</p></td>
<td><p>Values of the xs:unsignedInt type.</p></td>
</tr>
</tbody>
</table>

