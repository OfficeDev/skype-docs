# Anonymous Meeting Common Errors
The most common errors that you may encounter in starting an anonymous meeting are the following:

## AllowedOrigins

  - The AllowedOrigins request parameter in POST on AnonApplicationTokens should be _HTTPS_.

  - Make sure that the Allowed Origins sent in the POST on AnonApplicationTokens is the same origin from which the meeting join is later attempted.

 
## MeetingUrl

  - The meetingUrl request parameter in POST on AnonApplicationTokens should be the meeting's http web url. It should NOT be the conferenceId ( which is a non http url string).
  - The response to the discover request sent by the Anonymous Meeting Join client, contains the conferenceId (a non https url string ) which is used in the POST on onlineMeetingInvitations later for joining the meeting.
  
## ApplicationSessionId:

  - The session id should be unique (you can use a guid).
  - A session id identifies an anonymous meeting join client. You can reuse the same session id only if a client has somehow dropped out of the meeting and needs to rejoin the same session.
  
## Token

  - A token obtained for a particular meeting url cannot be used to anonymously join a different meeting.
  - A token obtained for one tenant's meeting url cannot be used to join a meeting in another tenant.

## Consent

  - Make sure the tenant admin consented to the application before trying to fetch the anonymous meeting join token.
