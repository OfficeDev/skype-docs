---
title: Parsing and constructing SDP documents
TOCTitle: Parsing and constructing SDP documents
ms:assetid: 3294209a-0e26-4257-827a-868d6332311f
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn466060(v=office.16)
ms:contentKeyID: 65239996
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
---

# Parsing and constructing SDP documents


_**Applies to:** Skype for Business 2015_

Use the **Sdp** class in the UCMA 5.0 to parse and construct session descriptions to be carried in a Session Initiation Protocol (SIP) request or response. These session descriptions are defined by the Session Description Protocol (SDP). For more information about SDP, see the [SDP: Session Description Protocol](http://go.microsoft.com/fwlink/?linkid=83129) RFC.

An application working directly with the signaling layer of UCMA 5.0 needs to parse and serialize SDP session descriptions in order to establish an audio/video session with its peer. The application receives a media description from the remote, and verifies that the content type is application/sdp. At this point it is necessary to understand what the remote is offering, but the author has only an array of bytes. Use the **Sdp** class to parse the array of bytes, so that it can be validated and interpreted.

After the application determines an appropriate response, use the **Sdp** class to generate an array of bytes containing a properly formatted SDP to send as an answer.

## Parsing session description documents

Use the **TryParse()** method to determine whether the SDP is valid. Subclass the [SdpMediaDescription](https://msdn.microsoft.com/en-us/library/hh383755\(v=office.16\)) and [SdpGlobalDescription](https://msdn.microsoft.com/en-us/library/hh385124\(v=office.16\)) classes to get the values of session description attributes. See the following sample code.

``` csharp
public void SetAnswer(object sender, ContentDescription answer)
{
  SignalingSession Session = sender as SignalingSession;

  // Verify that the answer received is consistent
  byte[] Answer = answer.GetBody();

  if (Answer != null)
  {
    Sdp<SdpGlobalDescription, SdpMediaDescription> SessionDescription = new Sdp<SdpGlobalDescription, SdpMediaDescription>();

    if (!SessionDescription.TryParse(Answer))
    {
      Session.BeginTerminate(null, Session);
      return;
    }

    else
    {
      IList<SdpMediaDescription> ActiveMediaTypes = SessionDescription.MediaDescriptions;
      if ((ActiveMediaTypes.Count == 1) && (ActiveMediaTypes[0].MediaName.Equals("audio", StringComparison.Ordinal)) && (ActiveMediaTypes[0].Port > 0) && (ActiveMediaTypes[0].TransportProtocol.Equals("sip", StringComparison.OrdinalIgnoreCase)))
      {
      }

      else
      {
        Session.BeginTerminate(null, Session);
      }
    }
  }
}
```

## Constructing session description documents

Use the **Sdp**, [SdpMediaDescription](https://msdn.microsoft.com/en-us/library/hh383755\(v=office.16\)), and [SdpGlobalDescription](https://msdn.microsoft.com/en-us/library/hh385124\(v=office.16\)) classes to set the values of session description attributes. When the **Sdp** class is initially constructed it will have default values as specified in the documentation for this class. See the following sample code.

``` csharp
public ContentDescription GetOffer(object sender)
{
  SignalingSession Session = sender as SignalingSession;
  IPAddress IpAddress;

  // This method is called back every time an outbound INVITE is sent.
  if (Session.Connection != null)
  {
    IpAddress = Session.Connection.LocalEndpoint.Address;
  }

  else
  {
    IpAddress = IPAddress.Any;
  }

  Sdp<SdpGlobalDescription, SdpMediaDescription> SessionDescription = new Sdp<SdpGlobalDescription, SdpMediaDescription>();

  // Set the origin line of the SDP.
  // The s, t, and v lines are automatically constructed.
  SessionDescription.GlobalDescription.Origin.Version = 0;
  SessionDescription.GlobalDescription.Origin.SessionId = "0";
  SessionDescription.GlobalDescription.Origin.UserName = "-";
  SessionDescription.GlobalDescription.Origin.Connection.Set(IpAddress.ToString());

  // Set the connection line.
  SessionDescription.GlobalDescription.Connection.TrySet(IpAddress.ToString());

  SdpMediaDescription mditem = new SdpMediaDescription("message");

  mditem.Port = 5060;
  mditem.TransportProtocol = "sip";
  mditem.Formats = "null";

  SdpAttribute aitem = new SdpAttribute("accept-types", "text/plain");

  mditem.Attributes.Add(aitem);

  // Append the Media description to the Global description.
  SessionDescription.MediaDescriptions.Add(mditem);

  ContentType ct = new ContentType("application/sdp");

  return new ContentDescription(ct, SessionDescription.GetBytes());
}
```

