
# Authentication using an access token

 _**Applies to:** Skype for Business 2015_

After bootstrapping the Skype Web SDK we have access to an api object which exposes an application object.  If we create a new instance of an application we get access to a signInManager.  The signInManager provides us with a signIn(...) method that we can supply a domain and auth function that sets the request's Authorization header with a web ticket to authenticate.

### Static token

  ```js
    app.signInManager.signIn({
        version: version,        
        domain: "contoso.com",
        token: "Bearer ey..."
    });
  ```
  
Note, that an access token is usually valid for only one domain/FQDN, like pool-a.contoso.com, so if while discovering the UCWA server the websdk gets redirected to another domain, like pool-b.contoso.com, the token will be rejected with a 401 or 403.
  
### Getting tokens at runtime

  ```js
    app.signInManager.signIn({
        version: version,        
        domain: "contoso.com",
        auth: function (req, get) {
            var src = this.src(); // e.g. https://pool-a.contoso.com/xframe
        
            return get(req).then(function (rsp) {
                if (rsp.status != 401)
                    return rsp;
                    
                // note, that req.url may contain ony path, e.g. /ucwa/v1/oauth/applications/1132
                // and the getAccessTokenFor function needs to use the src value to get the token audience
                return getAccessTokenFor(req, rsp, src).then(function (token) {
                    req.headers["Authorization"] = "Bearer " + token;
                    return get(req);
                });
            });
        }
    });
  ```
  
Note, that there can be multiple requests sent at the same time and it would be inefficient to invoke getAccessTokenFor for all of them. A more efficient approach would be to hold all requests while getAccessTokenFor is getting a token and let them proceed once the token is obtained. Another observation is that in some cases there can be multiple domains/FQDNs that the websdk is talking to: different domains usually need different tokens.
  
