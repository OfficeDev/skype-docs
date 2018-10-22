---
title: RealTimeAddress and SipUriParser classes
TOCTitle: RealTimeAddress and SipUriParser classes
ms:assetid: bfd16534-406d-4703-b04f-bb0e067f2fdd
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466054(v=office.16)
ms:contentKeyID: 65239990
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# RealTimeAddress and SipUriParser classes


**Applies to**: Skype for Business 2015

The UCMA platform exposes two classes to handle URI processing: the [RealTimeAddress](https://msdn.microsoft.com/en-us/library/hh348792\(v=office.16\)) class and the [SipUriParser](https://msdn.microsoft.com/en-us/library/hh384267\(v=office.16\)) class. The **RealTimeAddress** class is primarily used for passing a valid URI to methods that accept a URI as a string or **RealTimeAddress** instance. Some API in the platform may also expose properties using the **RealTimeAddress** class. This **RealTimeAddress** class supports processing of both tel URIs and sip URIs.

The **SipUriParser** class is a standalone class that can be used to assist in processing a SIP URI. The primary use of this class is to build a SIP URI or to process one to access individual parts of the URI. Unlike the **RealTimeAddress** class, the **SipUriParser** class does not handle processing of **tel** URIs except possibly for determining the scheme of the URI.

Many methods in the UCMA platform accept URIs as strings rather than as **RealTimeAddress** instances. For example, some methods are lenient in accepting schemes other than sip or tel. Some methods even accept strings of the form "4255551212" as a **tel** URI by automatically prepending "tel:" to the string. The string is more convenient to pass as a parameter than a **RealTimeAddress** but has the potential of containing invalid URI values that can cause some methods to throw [ArgumentException](https://msdn.microsoft.com/en-us/library/3w1b3114). An application should always validate URI values obtained from other untrusted sources before passing them to the platform APIs.

## RealTimeAddress class

The **RealTimeAddress** class is easy to use. The following are the available constructors to create an instance of this class:

  - [RealTimeAddress(String)](https://msdn.microsoft.com/en-us/library/hh381970\(v=office.16\))

  - [RealTimeAddress(String, String)](https://msdn.microsoft.com/en-us/library/hh384252\(v=office.16\))

  - [RealTimeAddress(String, String, String)](https://msdn.microsoft.com/en-us/library/hh349339\(v=office.16\))

The second constructor is used mainly for creating an endpoint URI to address a specific endpoint of a user corresponding to an older version of products that did not support the use of a GRUU. This is needed only for specific interoperability with endpoints that work with endpoint IDs but not GRUUs. Many Public Cloud clients, such as Windows Live Messenger, might not understand both the endpoint ID and GRUU concepts. The third constructor is useful when the URI parameter might be a **tel** URI. If so, the default domain and phone context are used to convert a **tel** URI to a **sip** URI. If the URI parameter is a SIP URI, the additional parameters are not used. An application usually does not need this constructor; the platform uses this constructor internally, as necessary, by taking the additional parameters from the endpoint itself.

The **RealTimeAddress** class exposes several useful properties, all of which are self-explanatory. The [IsEndpointTarget](https://msdn.microsoft.com/en-us/library/hh349106\(v=office.16\)) property returns true if the URI is a GRUU or contains an endpoint ID and thus targets a specific endpoint belonging to a user rather than a specific user. An INVITE message that targets a user will be forked by Skype for Business Server 2015 (or Lync Server 2013/Lync Server 2010) to all available endpoints of that user. A message that targets one of several endpoints belonging to a user will be delivered only to that specific endpoint.

The **RealTimeAddress** class provides methods to compare URI values. One URI instance can be compare to another using the inequality (==) or inequality (\!=) operators, or by using the **Equals** instance method. The comparison can be used with **sip** URI or **tel** URI values. For a **sip** URI, the class uses case-insensitive comparison of the user parts even though this part is considered case-sensitive by RFC 3261. This is done to accommodate the case insensitivity of user parts in Active Directory. If case sensitivity is important, consider using the **SipUriParser** class. This class is optimized for comparison by leveraging string comparison and then falling back to URI comparison by creating URI parsers as needed, behind the scenes.

It is highly recommended that applications refrain from comparing URI values using string comparison. A URI can contain parameters in any order; hence string comparison may not be able to properly determine whether two URIs are equal. The **RealTimeAddress** class is especially useful when the application wants to create a dictionary with the key being a URI. For example, an application can declare a variable with the generic type **Dictionary**, where *YourType* can be any type of interest to the application.

## SipUriParser class

The [SipUriParser](https://msdn.microsoft.com/en-us/library/hh384267\(v=office.16\)) class is a standalone class that is designed to build or parse a SIP URI. This class does not support handling of a **tel** URI except for determining the scheme. To process a **tel** URI, consider using the **RealTimeAddress** class.

The **SipUri** class uses the RFC 3261 standard to build or parse the URI. The class supports processing of GRUU URIs as well. There are numerous properties and methods which are self-explanatory. This memory consumption of an instance of this class can be significantly more than that of **RealTimeAddress** and hence this class is not recommended for storing large number of URI instances. This class is primarily intended for "parse and forget" scenarios.

This class provides two static methods for comparison, to enable equality checks with or without case-sensitive comparison of the user parts.

The **SipUriParser** class provides properties and methods to access data contained in SIP URIs.

### Constructing a SIP URI

The following example constructs a SIP URI using the constructor on the **SipUriParser** class.

```csharp
SipUriParser p = new SipUriParser("sip:someone@example.com:2020;maddr=10.0.0.20?replaces=somebodyelse%40example.com%3Btotag%3Dadf1713ab");
```

### Parsing components

In the following example, the [Host](https://msdn.microsoft.com/en-us/library/hh349142\(v=office.16\)), [HostAndPort](https://msdn.microsoft.com/en-us/library/hh381626\(v=office.16\)), [Port](https://msdn.microsoft.com/en-us/library/hh384973\(v=office.16\)), [User](https://msdn.microsoft.com/en-us/library/hh384004\(v=office.16\)), and [UserAtHost](https://msdn.microsoft.com/en-us/library/hh349890\(v=office.16\)) properties on the **SipUriParser** instance are used to extract the similarly named components of the URI specified in the **SipUriParser** constructor.

```csharp
SipUriParser p = new SipUriParser("sip:someone@example.com:2020");
string uriHost = p.Host;
string uriHostAndPort = p.HostAndPort;
int uriPort = p.Port;
string uriUser = p.User;
string uriUserAtHost = p.UserAtHost;
```

### Parsing parameters

The following example uses the [FindParameter(String)](https://msdn.microsoft.com/en-us/library/hh384282\(v=office.16\)) method to access the *maddr* parameter value in the specified URI.

```csharp
SipUriParser p = new SipUriParser("sip:someone@example.com:2020;maddr=10.0.0.20");
SipUriParameter param = p.FindParameter("maddr");
```

### Parsing headers

The following example uses the [FindHeader(String)](https://msdn.microsoft.com/en-us/library/hh349374\(v=office.16\)) method to access the *Replaces* header in the specified URI.

```csharp
SipUriParser p = new SipUriParser("sip:someone@example.com:2020;maddr=10.0.0.20?replaces=somebodyelse%40example.com%3Btotoag%3Dadf1713ab");
SignalingHeader header = p.FindHeader("replaces");
```

