
# Search for persons and groups


 _**Applies to:** Skype for Business 2015_

**In this article**
- [Persons and groups](#overview)
- [Search for persons](#persons)
- [Search for groups](#groups)

<a name="overview"></a>
### Persons and groups

A Person represents a user. The <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.person.html" target="">Person</a> object can be queried for information about a person, such as their availability to join a conversation. The <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.person.html" target="">Person</a> object is passed to the conversation starting methods, such as the <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.conversationsmanager.html#getconversation" target="">ConversationsManager.getConversation</a> method, so that the conversation invitation is sent to the person represented by the <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.person.html" target="">Person</a> object.

A <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.group.html" target="">Group</a> can represent a distribution group, server-defined person set, or user-defined person set. If the <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.group.html" target="">Group</a> is a distribution group, it can also link to other distribution groups. Persons in a distribution group are represented by <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.person.html" target="">Person</a> objects. The arguments for the <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.personsandgroupsmanager.html#creategroupsearchquery" target="">PersonsAndGroupsManager.createGroupSearchQuery</a> method include a partial or full name query and a numeric limit to the size of the result sets. Results include a collection of distribution groups. To find persons, use the <a href="http://officedev.github.io/skype-docs/Skype/WebSDK/model/api/interfaces/jcafe.personsandgroupsmanager.html#createpersonsearchquery" target="">PersonsAndGroupsManager.createPersonSearchQuery</a> method.
The following procedure assumes that a user has signed in before searching for persons and groups.

<a name="persons"> </a>
### Search for persons


1. Create a  **SearchQuery** for person search: **personsAndGroupsManager.createPersonSearchQuery**.
    
2. Specify the search terms and a limit to the number of possible results in the  **SearchQuery**.
    
3. Execute the  **searchQuery.getMore** method and get the search **results** in the **onSuccess** method.
    
4. Call the  **forEach** method of the array of results. For each result, **Person** object is the result.result.
    
>**Note**:  The maximum number of results for a person search query is 50. 

  ```js
var personSearchQuery = application.personsAndGroupsManager.createPersonSearchQuery();
personSearchQuery.text('John Doe');
personSearchQuery.sources('AddressBook');  // To search outside the user's contacts, try 'SkypeDirectory' or 'All'
personSearchQuery.limit(50);
personSearchQuery.getMore().then(null, function (results) {
    results.forEach(function (result) {
      var person = result.result;
      console.log('Person ', person.displayName());
      // success - Contact Found            
    }, 
    function (error) {
     // handle error
    });
});

  ```

<a name="groups"> </a>
### Search for groups


1. Create a  **SearchQuery** for group search: **personsAndGroupsManager.createGroupSearchQuery**.
    
2. Specify the search terms a limit to the number of possible results in the  **SearchQuery**.
    
3. Execute the  **searchQuery.getMore** method and get the search **results** in the **onSuccess** method.
    
4. Call the  **forEach** method of the array of results. For each result, **Group** object is the **result.result**.


  ```js
var groupSearchQuery = application.personsAndGroupsManager.createGroupSearchQuery();
groupSearchQuery.text('mygroup');
personSearchQuery.sources('AddressBook');  // To search outside the user's contacts, try 'SkypeDirectory' or 'All'
groupSearchQuery.limit(50);
groupSearchQuery.getMore().then(null, function (results) {
    results.forEach(function (result) {
      var group = result.result;
      console.log('Distribution Group ', group.name());
      // success - Group Found
    }, 
    function (error) {
      // error
    });
});

  ```

## Additional resources

- <a href="https://msdn.microsoft.com/skype/websdk/docs/ListenForAvailability" target="">Get a person and listen for availability</a>

