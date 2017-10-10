# Implementing a Chat Client with the Skype Web SDK

An anonymous chat client can be implemented using either UCWA or Web SDK:
 
- Sign in to the Web SDK using the Anonymous Token and Discover URL in your sign-in parameters.

## A simple chat client implementation
The following examples show you how to sign an anonymous user into a Skype for Business tenant, get the chat history of a conversation, and then send a message.

 
### Sign in using the Web SDK 
Sign in to the Web SDK using the following parameters and code:

```JavaScript 
var options = {   name: "John Doe",
token: "Bearer psat=ey...Xg", // OAuth token
root: { user: "https://lync.com/platformService/discover?anonymousContext=psat%3dey...Xg" },
cors:true
}
 
window.skypeWebApp.signInManager.signIn(options).then(function() {
                            console.log('Signed in');
            }, function(error) {
                // if something goes wrong in either of the steps above,
                // display the error message
                $(".modal").hide();
                alert("Can't sign in, please check the user name and password.");
                console.log(error || 'Cannot sign in');
            });
        }
 ```
 
### Show the chat history for a conversation

The following client side JavaScript example queries the Web SDK conversation manager's collection of active conversations for
a conversation with the Contoso help desk. If the conversation is found, the state of the conversation is checked, UI updated, and conversation history is loaded. If the words 'bye' or 'goodbye' is
found in the chat history, the conversation is terminated.


```JavaScript
var conversation = client.conversationsManager.getConversation(“sip:helpdesk@contoso.com”);
                    chatService = conversation.chatService;
                    conversation.selfParticipant.chat.state.when("Connected", function (state) {
                        addNotification('Conversation state: ' + state);
                        addNotification('Now you can send messages');
                        $(".modal").hide();
                        conversation.historyService.activityItems.added(function(message) {
                            index++;
                            if (!(message.sender.id() == client.personsAndGroupsManager.mePerson.id())) {
                                historyAppend(XMessage(message));
                            } else {
                                if (index % 2 != 0) {
                                    historyAppend(XMessage(message));
                                }
                                if (message.text().toLowerCase().indexOf('bye') > -1 || message.text().toLowerCase().indexOf(' goodbye') > -1) {
                                    //terminate the conversation
                                    StoptMessageInvitationHandler();
                                    chatService.stop().then(function() {
                                        console.log('Chat service stopped');
                                    });
                                    //could not send message when conversation stoped.
                                    $("#input-message").hide();
                                }
                            }
                        });
```
 
### Send a message
The following example sends a chat message in a conversation. The **chatService** object is a child of a **conversation** object.
```JavaScript
chatService.sendMessage('How are you?');
 ```
 
 