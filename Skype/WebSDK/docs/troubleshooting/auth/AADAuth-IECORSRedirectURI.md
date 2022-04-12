# AAD Auth Failures - Invalid CORS redirect URI in Internet Explorer and Microsoft Edge

_**Applies to:** Skype for Business 2015_

**In this article**
- [Who is this article for?](#audience)
- [The Issue](#issue)
- [The Solution](#solution)
- [Related Topics](#related-topics)

<a name="audience"></a>
## Who is this article for?

This article applies only to user error experiences where the browser in use is Internet Explorer (IE) or Microsoft Edge (Edge). 

If you are using the Azure AD authentication option to sign into the Skype for Business (SfB) Web SDK in IE or Edge, you are successfully authenticating to Azure AD and getting redirected back to your web application, and then your signin fails silently or hangs indefinitely, then this article is for you. 

If the previous failure description seems to match your user's experience but the user is not on IE or Edge then you should return to [Troubleshooting Azure AD Authentication Failures for Skype Web SDK](./AADAuthFailures.md) for a list of other potential issues.

<a name="issue"></a>
## The Issue

Generally when authenticating against Azure AD, you are redirected to the Azure AD sign in page, enter your credentials, and are redirected back to a page specified during the initial redirect to the AAD sign in page by the **redirect_uri** query parameter in the URL. Upon redirection, the URL of the redirect page includes an access token in the fragment of the URL. Sometimes after authenticating against Azure AD in IE or Edge, this fragment is lost. Providing a valid empty html file where AAD can store and retrieve the token mitigates this issue. This issue is intermittent even when present, so it might be difficult to detect.

<a name="solution"></a>
## The Solution

If you don't specify a valid **redirect_uri** for CORS when making the call to **signInManager.signIn** after being redirected back to the main app page from AAD, IE or Edge can lose the token provided by AAD. If you don't specify a **redirect_uri** that points to an empty html file in a valid subfolder of your main app directory, when you attempt the actual signin the token will not be present and the signin will fail.

The solution is to create an empty html file (eg. **token.html**) in a valid subfolder of your hosted main app folder, and provide the path to that as the value of the **redirect_uri** parameter in the call to **signInManager.signIn**. For example, if your app is in a folder called **myapp**, create an empty file named **token.html** in the **myapp** folder on your machine, and make this path the value of the **redirect_uri** parameter when signing in.

As long as **token.html** is in the same folder or a subfolder of the root folder where your app is hosted, this should prevent you from experiencing this issue.

This failure is inconsistent, and you will not necessarily experience this failure all the time if you do not do this, but if you don't, signing into your app will be inconsistent in IE and Edge so it is strongly recommended.

---

<a name="related-topics"></a>
## Related Topics

- [Troubleshooting AAD Auth Failures for Skype Web SDK](./AADAuthFailures.md)
- [Integrating Applications with Azure Active Directory](/azure/active-directory/active-directory-integrating-applications)
- [Authentication in UCWA apps using Azure AD](../../../../UCWA/AuthenticationUsingAzureAD.md)
