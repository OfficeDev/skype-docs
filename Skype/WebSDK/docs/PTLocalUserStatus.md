
# Status


 _**Applies to:** Skype for Business 2015_

## Local user - Set Status

We can make use of the mePerson object exposed by personsAndGroupsManager and from this object get access to the status property.  This property is a writeable allowing changes via a set(...) method that takes a provided string matching a status such as Online, Away, Busy.

### Select a status and click to change presence

```js
var mePerson = application.personsAndGroupsManager.mePerson;
mePerson.status.set(status).then(function () {
    // status changed succesfully 
}, function (error) {
    // handle rrror
});
```

#### Other resources

<a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.meperson.html" target="">MePerson</a>
<a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.status.html" target="">Status</a>
