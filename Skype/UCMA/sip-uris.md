---
title: SIP URIs
TOCTitle: SIP URIs
ms:assetid: 44dc0702-5b75-4e67-a7ad-078d8e590903
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466057(v=office.16)
ms:contentKeyID: 65239989
ms.date: 07/27/2015
mtps_version: v=office.16
---

# SIP URIs


_**Applies to:** Skype for Business 2015_

A Session Initiation Protocol (SIP) uniform resource identifier (URI) identifies a communications resource, and contains enough information to initiate and continue a communication session with the resource identified in the URI.

The SIP URI parser (the [SipUriParser](https://msdn.microsoft.com/en-us/library/hh384267\(v=office.16\)) class) can be used to parse and construct SIP URIs in UCMA 5.0. The SIP URI parser consists of the **SipUriParser** class and its methods and properties. For more information, see [RealTimeAddress and SipUriParser classes](realtimeaddress-and-sipuriparser-classes.md).

## SIP URI

A SIP URI includes components such as the user and host, as well as optional headers and an optional parameters collection. For more information, see section 19 of RFC 3261 [SIP: Session Initiation Protocol](http://www.ietf.org/rfc/rfc3261.txt).

SIP has a mechanism by which a REFER request received by a User Agent (UA) on a given session triggers the sending of another SIP method, by default an INVITE, to the target SIP URI specified in a SIP signaling header, the Refer-To: header. The SIP URI often contains headers that are carried over from the SIP URI to the SIP request that is sent as a result when the REFER request is received. The SIP URI headers are actually converted into SIP signaling headers of the new request. For the convenience of authors, UCMA 5.0 has a SIP parser that escapes and unescapes the SIP URI header characters so that applications can simply use the SIP URI headers and convert them into SIP signaling headers.

### SIP URI components

A SIP URI has the following general form: sip:user:password@host:port;uri-parameters?headers

The table that follows describes the components of a SIP URI.

<table>
<colgroup>
<col style="width: 33%" />
<col style="width: 33%" />
<col style="width: 33%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Component</p></th>
<th><p>Example</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>scheme</p></td>
<td><p>sip</p></td>
<td><p>Identifies the scheme. Note that SIPS URIs follow the same form as SIP URIs.</p></td>
</tr>
<tr class="even">
<td><p>user</p></td>
<td><p>mary</p></td>
<td><p>The identifier of a particular resource at the host being addressed.</p></td>
</tr>
<tr class="odd">
<td><p>password</p></td>
<td><p>Xyz123</p></td>
<td><p>A password associated with the user. Including a password is not recommended, because passing authentication information in clear text is a security risk.</p></td>
</tr>
<tr class="even">
<td><p>host</p></td>
<td><p>contoso.com</p></td>
<td><p>The host providing the SIP resource. Required.</p></td>
</tr>
<tr class="odd">
<td><p>port</p></td>
<td><p>5151</p></td>
<td><p>The port number where the request is sent.</p></td>
</tr>
<tr class="even">
<td><p>URI parameters</p></td>
<td><p>user=phone</p></td>
<td><p>Parameters affecting a request constructed from the URI. URI parameters are added after the hostport components and are separated by semicolons.</p>
<p>URI parameters take the form parameter-name &quot;=&quot; parameter-value.</p></td>
</tr>
<tr class="odd">
<td><p>headers</p></td>
<td><p>priority=urgent</p></td>
<td><p>Additional information added after the URI parameters.</p></td>
</tr>
</tbody>
</table>


### SIP URI parameters

The following table describes members of the parameters collection of a SIP URI.

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
<td><p>transport</p></td>
<td><p>Determines the transport protocol to be used for sending SIP messages. For a SIPS URI, the transport parameter must indicate a reliable transport protocol.</p></td>
</tr>
<tr class="even">
<td><p>maddr</p></td>
<td><p>Indicates the server address to be contacted for this user, overriding any address derived from the host field.</p></td>
</tr>
<tr class="odd">
<td><p>ttl</p></td>
<td><p>Determines the time-to-live value of the UDP multicast packet. Valid only if <em>maddr</em> is a multicast address, and the transport protocol is UDP (User Datagram Protocol).</p></td>
</tr>
<tr class="even">
<td><p>user</p></td>
<td><p>Use the <em>user</em> parameter to distinguish telephone numbers from user names that happen to look like telephone numbers. If the user string contains a telephone number formatted as a telephone-subscriber, add the <em>user</em> parameter <strong>phone</strong> value to identify the value as a user name.</p></td>
</tr>
<tr class="odd">
<td><p>method</p></td>
<td><p>The method of the SIP request constructed from the URI may be specified with the <em>method</em> parameter.</p></td>
</tr>
<tr class="even">
<td><p>lr</p></td>
<td><p>Indicates that the element responsible for this resource implements the routing mechanisms specified in RFC 3261.</p></td>
</tr>
<tr class="odd">
<td><p>gruu</p></td>
<td><p>Several applications of SIP require a user agent (UA) to construct and distribute a URI that can be used by anyone on the Internet to route a call to that specific UA instance. A URI that routes to a specific UA instance is called a Globally Routable UA URI (GRUU).</p></td>
</tr>
<tr class="even">
<td><p>opaque</p></td>
<td><p>Identifies a specific endpoint.</p></td>
</tr>
</tbody>
</table>


### SIP URI header fields

Header fields in the SIP request can be specified with the ? mechanism within a URI. The header names and values are encoded in ampersand-separated name/value pairs. The special name *body* indicates that the associated value is the message-body of the SIP request.

