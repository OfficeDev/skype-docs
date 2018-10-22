---
title: Creating a generic bot
TOCTitle: Creating a generic bot
ms:assetid: 70eea19d-91ad-45ae-95b7-301919eb9633
ms:mtpsurl: https://msdn.microsoft.com/en-us/library/Dn454839(v=office.16)
ms:contentKeyID: 65240110
ms.date: 07/27/2015
mtps_version: v=office.16
dev_langs:
- csharp
- vb
---

# Creating a generic bot



You can create a generic bot in four steps.

1.  Create message handlers

2.  Add the bot to your host environment

3.  Handle feedback events (Optional)

4.  Test your bot

## Create message handlers

A bot's logic is implemented by composing *message handlers*. Each message handler is responsible for handling a category of user messages, such as handling questions related to the weather ("How is the weather?" or "Tell me the temperature out there\!"). After receiving a user message, the bot will query each of its message handlers for the confidence it can place on handling the message. The bot then asks the handler that provided the highest confidence to handle the message. The message handler then processes the user message and creates a reply that the bot sends back to the user.

Message handlers can store conversational state. For example, suppose that a message handler for handling simple mathematical questions (let's call it *CalculatorMessageHandler*) expects messages from the user telling the operation name, then the first number, then the second number, and so on. Each one of these steps, in which the *CalculatorMessageHandler* is waiting for some sort of input (the operation name or its operands) is a state. Stateful message handlers are represented in the Build a Bot framework by the *BuildABot.Core.MessageHandlers.MessageHandler* class.

In some other cases, a message handler might not need to store any conversation state at all - it can fully process the message in only one turn, then it's done. For example, a *TimeMessageHandler* might receive a message asking "What time is it?", put together a reply with the current time to the user, and be finished. Single-state message handlers are represented in the Build a Bot framework by the *BuildABot.Core.MessageHandlers.SingleStateMessageHandler* class. *SingleStateMessageHandler* actually inherits from *MessageHandler*, abstracting the conversation state logic to the developer. The *QAMessageHandler* is an even simpler implementation of a *SingleStateMessageHandler*, in which simple question/answer handling logic can be easily implemented.

### Setting up your message handler project and class

The following steps should be performed before you implement a single-state message handler.

### To set up your message handler project and class

1.  Create a new class library project in Visual Studio.

2.  Add a reference to System.ComponentModel.Composition.dll.

3.  Add a reference to BuildABot.Core.dll and BuildABot.Util.dll.

4.  Add a new class to your class library project. This will be your message handler class. Give it a meaningful name and make it inherit from either
    
      - *BuildABot.Core.MessageHandlers.MessageHandler*, if your message handler needs to support conversational state, or
    
      - *BuildABot.Core.MessageHandlers.SingleStateMessageHandler*, if your message handler doesn't need to support conversational state.

5.  Add the following attribute to your message handler class: \[Export(typeof(MessageHandler))\]. The Export attribute is defined in the System.ComponentModel.Composition namespace, which belongs to .NET 4.0 MEF (Managed Extensibility Framework). Using such an attribute will enable your bot to automatically consume the message handler you're developing, as long as the message handler assembly (DLL) is visible to the assembly where the bot is defined. For more information, see [Add the bot to your host environment](creating-a-generic-bot.md)).

For a single-state message handler, your code should look like this, at this point.

```csharp
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;
 
namespace BuildABot.Samples.MessageHandlers
{
    [Export(typeof(MessageHandler))]
    public class TimeMessageHandler : SingleStateMessageHandler
    {
    }
}
```


> [!NOTE]
> <P>Build a Bot also provides support for you to implement your message handler class in the same project/assembly in which your bot is defined. For more information, see <A href="creating-a-generic-bot.md">Add the bot to your host environment</A>). However, creating one or more class library projects for your message handlers is the recommended guideline.</P>



### Configuring your message handler

By optionally calling the *MessageHandler* (base class) constructor(s), as shown below, your message handler can be configured.

```csharp
public TimeMessageHandler()
            :base("time","Please wait while I get the current time...", true)
{
}
```

<table>
<colgroup>
<col style="width: 50%" />
<col style="width: 50%" />
</colgroup>
<thead>
<tr class="header">
<th><p>Parameter order</p></th>
<th><p>Description</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>1st</p></td>
<td><p>The first parameter (<em>regexPattern</em>) is a regular expression pattern that the message handler will try to match with the user input. If there is a match, the message handler returns to the bot its default confidence (1.0, or 100%) in handling the message. The default confidence can be changed by setting the <em>DefaultConfidence</em> property. To define your own confidence logic, see Custom message handling confidence logic.</p></td>
</tr>
<tr class="even">
<td><p>2nd</p></td>
<td><p>The second parameter (<em>initialHandlingText</em>) is the &quot;initial handling text&quot;. If the bot selects this message handler to process the user message and create a reply, the bot will use the initial handling text for an immediate reply, even though the message handler might still be processing the user message to create the final reply. In the example, the initial handling text is a static string. To build a dynamic initial handling text that takes variables into account, see <a href="creating-a-generic-bot.md">Dynamic initial handling text</a>.</p></td>
</tr>
<tr class="odd">
<td><p>3rd</p></td>
<td><p>The third parameter (<em>requiresFeedback</em>) indicates whether feedback should be requested to the user after this message handler is done. For more information, see <a href="creating-a-generic-bot.md">Handle feedback events</a>.</p></td>
</tr>
<tr class="even">
<td><p>4th</p></td>
<td><p>The fourth parameter (abortOnNeverMind) is not used in the preceding example. It indicates whether a multi-state message handler (one that supports conversational state) should abort its state handling (that is, be considered finished) when the end user, at any time, sends a &quot;never mind&quot; or similar (&quot;cancel&quot;, &quot;stop&quot;, &quot;done&quot;, &quot;abort&quot;) messages.</p></td>
</tr>
</tbody>
</table>


### Custom message handling confidence logic

While using regular expression patterns is a very convenient way to specify whether a message handler can handle a given user message, you can implement your own logic to indicate that ability. Simply override the *CanHandle* method and return a *MessageHandlingResponse* object, which contains a confidence and initial handling text. The following code returns a response with zero confidence for requests made during weekends.

```csharp
public override MessageHandlingResponse CanHandle(Message message)
{
    MessageHandlingResponse response = new MessageHandlingResponse();
    if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday
        || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
    {
        response.Confidence = 0;
    }
    else
    {
        // We could have implemented more complex/specific
        // logic for handling the message here.
        response = base.CanHandle(message);
    }
 
    return response;
}
```

### Dynamic initial handling text

The base *MessageHandler* constructor enables you to specify static initial handling text. In some situations you might want to have dynamic text, which is created according to the user input or other non-static variable. An example follows in the following conversation.

User: "Please show details of bug Windows SE:12345"Bot: "Sure, retrieving details of bug \#12345 in the Windows SE bug database. I'm handling 34 requests right now so I expect you will get this response at 12:34pm..."Bot: "Here you have it: \[...\]"

As you might notice, the first reply message from the bot in this conversation is a string composed of dynamic elements (items in bold), such as the user input, the expected response time, and so on. To create dynamic initial handling text like this, you need to override the *GetInitialHandlingText* method, as shown in the next example.

```csharp
protected override string GetInitialHandlingText()
{
    string bugId = this.GetBugId();
    string bugDatabase = this.GetBugDatabase();
    int numberOfRequests = this.GetNumberOfRequests();
    System.DateTime expectedReplyTime = DateTime.Now.AddSeconds(numberOfRequests * averageResponseTime);
 
    string initialHandlingText = String.Format(
        "Sure, retrieving details of bug #{0} in the {1} bug database. "
        + "I'm handling {2} requests right now so I expect you will get this response at {3}...",
        bugId, bugDatabase, numberOfRequests, expectedReplyTime);
 
    return initialHandlingText;
}
```

*GetInitialHandlingText* is called by the default *MessageHandler.CanHandle* implementation (for more information, see [Custom message handling confidence logic](creating-a-generic-bot.md)). If you override *CanHandle* to provide your own *MessageHandlingResponse*, you might need to call *GetInitialHandlingText* explicitly to set the *InitialHandlingText* property of the *MessageHandlingResponse* object, as shown in the following example.

```csharp
public virtual MessageHandlingResponse CanHandle(Message message)
{
    MessageHandlingResponse response = new MessageHandlingResponse();
 
    // Some confidence logic...
    // Then the initial handling text for the response should be explicitly set!
    response.InitialHandlingText = this.GetInitialHandlingText();
    return response;
}
```

### Handling a single-state conversation

For single-state message handlers, handling a user message requires you only to override the *Handle* method. In such a method, you should build a *Reply* object, which contains a collection of *ReplyMessages*. The example below shows a typical message handling logic for the *TimeMessageHandler*.

```csharp
public override Reply Handle(Message message)
{
    Reply reply = new Reply();
    reply.Add("The current time is: " + DateTime.Now.ToShortTimeString());
    reply.Add("The current date is: " + DateTime.Now.ToShortDateString());
    return reply;
}
```

If your message handler needs to get additional information about the message that was sent (such as its content, sender name and alias, if available), just explore the properties of the *message* object.

The *Reply* object also has an *AddRtfMessage* method, that enables you to send formatted text to the user (bold, underline, and so on). You can use the *EncloseRtf* helper methods to make your string an RTF string and add the desired modifiers. The following example shows this.

```csharp
string text = "This " + "text".EncloseRtfBold() + " is in bold";
Reply reply = new Reply();
reply.AddRtfMessage(text);
```

### Handling a multi-state conversation

Multi-state message handlers should inherit from *BuildABot.Core.MessageHandlers.MessageHandler*. Each state is implemented as a method that receives a message and returns a reply. Two things need to be done to specify the state flow:

  - Override the *InitialStateHandler* property, which indicates what the starting state handler (method) is.

  - In the state handler methods, update the *nextStateHandler* delegate to point to the next state handler (method) accordingly.

The flow chart in the following illustration shows a conversation state machine in a helpdesk scenario.

Conversation state flow chart

  
![Build a Bot flow chart](images/Dn454839.BuildABot_FlowChart(Office.16).png "Build a Bot flow chart")

The next code sample implements the conversation state machine that is shown in the preceding flow chart.

```csharp
using System;
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;
 
namespace BuildABot.Samples.MessageHandlers
{
  [Export(typeof(MessageHandler))]
  public class HelpMessageHandler : MessageHandler
  {
     string ticketNumber;
     string problemDescription;
 
     public HelpMessageHandler()
         : base("help")
     {
     }
 
     protected override StateHandler InitialStateHandler
     {
        get { return AskForTicketStatus; }
     }
 
     public Reply AskForTicketStatus(Message message)
     {
        Reply reply = new Reply("Is this a new or an already opened ticket?");
        this.nextStateHandler = HandleTicketStatus;
        return reply;
     }
 
     public Reply HandleTicketStatus(Message message)
     {
        Reply reply = new Reply();
        if (message.Content.Contains("new"))
        {
           this.ticketNumber = this.CreateNewTicket();
           reply.Add("This is your new ticket number: " + this.ticketNumber);
           reply.Add("What's your problem?");
           this.nextStateHandler = this.GetUserIssue;
        }
        else
        {
           reply.Add("What's the ticket number?");
           this.nextStateHandler = this.GetTicketNumber;
        }
        return reply;
     }
 
     public Reply GetTicketNumber(Message message)
     {
        this.ticketNumber = message.Content;
        this.nextStateHandler = this.GetUserIssue;
        return new Reply("What's the update on your problem?");
     }
 
     public Reply GetUserIssue(Message message)
     {
        this.problemDescription = message.Content;
        string solution = this.GetSolution(problemDescription);
        this.nextStateHandler = this.GetSolutionFeedback;
        return new Reply("Will this work: " + solution);
     }
 
     public Reply GetSolutionFeedback(Message message)
     {
        Reply reply = new Reply();
        if (message.Content.Contains("yes"))
        {
           reply.Add("Great, good to know");
           this.Done = true;
        }
        else
        {
           string solution = this.GetAlternativeSolution(problemDescription);
           reply.Add("Sorry to hear that. What about this, will this work: " + solution);
           this.nextStateHandler = this.GetAlternativeSolutionFeedback;
        }
        return reply;
     }
 
     public Reply GetAlternativeSolutionFeedback(Message message)
     {
        Reply reply = new Reply();
        if (message.Content.Contains("yes"))
        {
           reply.Add("Great, good to know");
        }
        else
        {
           reply.Add("Sorry, I'll escalate this for you.");
           // escalation code...
        }
        this.Done = true;
        return reply;
     }
 
     private string CreateNewTicket()
     {
        return Guid.NewGuid().ToString();
     }
 
     private string GetSolution(string problemDescription)
     {
        return "https://www.letmebingthatforyou.com/?q=" + problemDescription;
     }
 
     private string GetAlternativeSolution(string problemDescription)
     {
        return "Ask your manager about " + problemDescription;
     }
  }
}
```

An interaction example follows. Notice that during the conversation, the state (ticket number, user problem description, and other information) is saved. This saved data creates additional context each time for the conversation.

  -  
    User: help

  -  
    Bot: Is this a new or an already opened ticket?

  -  
    User: new

  -  
    Bot: This is your new ticket number: 7953cfce-bc60-4340-b86b-3399150c83e

  -  
    Bot: What’s your problem?

  -  
    User: no network connectivity

  -  
    Bot: Will this work: https://www.letmebingthatforyou.com/?q=no network connectivity

  -  
    User: no

  -  
    Bot: Sorry to hear that. What about this, will this work: Ask your manager about no network connectivity

  -  
    User: yes

  -  
    Bot: Great, good to know

### Retrieving parameters from user input messages

Often, your message handler logic will need to extract parameters from a user message. For example, the user might tell the bot "MSFT stocks" or "AAPL stocks". In such a case, the stock symbol (MSFT or AAPL) is the parameter we want to extract.

As introduced before, the Build a Bot framework supports bot developers by enabling them to use regular expressions to define the bot's message handlers. C\# has the concept of a regular expression group, which capture part of the regular expression in a variable. For example, the following regular expression pattern defines the "symbol" group: (?\<symbol\>\\w+) stocks.

You can now use the index operator (this\["..."\]) to retrieve the group value anywhere in your message handler code. An example follows.

```csharp
public override Reply Handle(Message message)
{
    Reply reply = new Reply();
    string symbol = this["symbol"];
    string stockInfo = this.GetStockInfo(symbol);
    reply.Add("Here is the stock information for symbol " + symbol);
    reply.Add(stockInfo);
    return reply;
}
```

### QAMessageHandler

Often, you need to add some simple question/answer logic to your bot, as in the following dialog.

  -  
    User: "Hi\!"

  -  
    Bot: "Hello there\!"

  -  
    User: "How are you?"

  -  
    Bot: "Fine, thanks\!"

The *BuildABot.Core.MessageHandlers.QAs.QAMessageHandler* class can be used to add question/answer logic. This class inherits from the *SingleStateMessageHandler* class, and can be used to set a collection of question/answer pairs (*BuildABot.Core.MessageHandlers.QAs.QA* objects).

To create a question/answer message handler, just inherit from *BuildABot.Core.MessageHandlers.QAs.QAMessageHandler* and, in the constructor, add *QA* objects to your message handler's *QAs* collection, as in the next sample.

```csharp
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;
using BuildABot.Core.MessageHandlers.QAs;
 
namespace BuildABot.Samples.MessageHandlers
{
  [Export(typeof(MessageHandler))]
  public class QASampleMessageHandler : QAMessageHandler
  {
     public QASampleMessageHandler()
     {
        // build the "qa" objects...
 
        this.QAs.Add(qa1);
        this.QAs.Add(qa2);
        this.QAs.Add(qa3);
     }
  }
}
```

The qa1, qa2, and qa3 parameters in the three calls to the *Add* method are question/answer (*QA*) objects. There are two types of them: *RandomQA* objects and *ActionQA* objects.

#### RandomQA

A *BuildABot.Core.MessageHandlers.QAs.RandomQA* object contains a question and a set of (static) answers. If the question matches the user message content, one answer from the collection is chosen randomly. If only one answer is provided, that answer is always chosen. Here are some examples of *RandomQA* instances.

```csharp
RandomQA qa1 = new RandomQA("Who are you?", "Just a demo bot!");
RandomQA qa2 = new RandomQA("Hello", "Hi", "Howdy");
```

The first parameter of the *RandomQA* constructor is the question. The following parameters are the answers.

#### ActionQA

A *BuildABot.Core.MessageHandlers.QAs.ActionQA* contains a question and an action (method, pointed to by a delegate). If the question matches the user message content, the method is invoked to get the answer. This is useful when the answer for the question is not static. A complete example, merged with the preceding code, follows. If the user says "Excuse me", the bot will answer "good morning/afternoon/evening" according to the current time.

```csharp
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;
using BuildABot.Core.MessageHandlers.QAs;
using System;
 
namespace BuildABot.Samples.MessageHandlers
{
  [Export(typeof(MessageHandler))]
  public class QASampleMessageHandler : QAMessageHandler
  {
    public QASampleMessageHandler()
    {
       // build the "qa" objects
       RandomQA qa1 = new RandomQA("Who are you?", "Just a demo bot!");
       RandomQA qa2 = new RandomQA("Hello", "Hi", "Howdy");
       ActionQA qa3 = new ActionQA("Excuse me", this.GetSalute);

       this.QAs.Add(qa1);
       this.QAs.Add(qa2);
       this.QAs.Add(qa3);
    }

    private Reply GetSalute(Message message)
    {
       string salute = string.Empty;
       int hour = DateTime.Now.Hour;
       if (hour >= 0 && hour < 12)
       {
          salute = "Good morning!";
       }
       else if (hour >= 12 && hour < 18)
       {
          salute = "Good afternoon!";
       }
       else
       {
          salute = "Good evening!";
       }
       return new Reply(salute);
    }
  }
}
```

### Helper methods

The following extension methods are useful for adding QA objects to a *QAMessageHandler* object's *QAs* collection.

  - AddBatchRandomQAs  
    Adds a batch of *RandomQA* instances to a *QAs* list, as in this example.
    
    ```vb
    public QASampleMessageHandler()
    {
        // ...
        string[] byeMessages = { "bye", "good bye", "goodbye", "see you", };
        string[] byeReplies = { "Bye bye!", "Take care, see ya!", "Have a good one!"};
        this.QAs.AddBatchRandomQAs(byeMessages, byeReplies);
    }
    ```
    
    In the preceding sample, whenever the user sends a message that corresponds to any *byeMessages*, one reply from the *byeReplies* is randomly selected. This way, you do not need to manually add one *QA* object for each possible user message.

  - AddBatchActionQAs  
    Adds a batch of *ActionQA* instances to a *QAs* list.
    
    ```csharp
    public QASampleMessageHandler()
    {
      // ...
      this.QAs.AddBatchActionQAs(GetSalute, "good morning", "good night", "good evening", "good afternoon", "good day");
    }
    ```
    
    In the preceding code sample, whenever the user sends a message that corresponds to any of the string parameters (second parameter and beyond), the *GetSalute* method is called to get the reply. This way, you do not need to manually add one *QA* object for each possible user message.

## Add the bot to your host environment

After message handlers are implemented (see [Create message handlers](creating-a-generic-bot.md)), you are now left with the task of adding the bot to your host environment. The application can be a console (command prompt) application, Windows forms application, ASP.NET application, or other. If you want your bot to run as a Skype for Business agent, Build a Bot offers an even more specialized API for you to accomplish this. For more information, see [Creating a Skype for Business bot](creating-a-skype-for-business-bot.md).

### To add a bot to your host environment

1.  Add a reference to the BuildABot.Core and BuildABot.Util assemblies to your project.

2.  Make sure that all of the desired message handler assemblies (DLLs) are in the same folder as the assembly (typically the .exe) in which the bot is created. Adding a reference to the message handler class library projects is sufficient. You can also copy the message handler DLLs manually to the folder in which the bot's assembly is located.

3.  Instantiate the *Bot* class and create an event handler for the bot's *Replied* event. This event is raised whenever the bot replies to a user message. In the following example, which is part of a command prompt application, the bot reply messages are printed to the console.
    
    ```csharp
    static void Main(string[] args)
    {
        Bot bot = new Bot();
        bot.Replied += new ReplyEventHandler(bot_Replied);
        
        // TODO: implement code to make the bot process user input.
    }
     
    static void bot_Replied(object sender, ReplyEventArgs e)
    {
        foreach (ReplyMessage replyMessage in e.Reply.Messages)
        {
            Console.WriteLine("Bot says: " + replyMessage.Content);
        }
    }
    ```
    
    Note that the *ReplyEventArgs* class provides additional information to help your host environment print the bot's replies, such as the original *Message* entered by the user as well as the *MessageHandler* that handled the reply. The *ReplyContext* property indicates whether the reply is actually a message handler's initial handling text (see [Create message handlers](creating-a-generic-bot.md)), a regular conversation reply message, a request for feedback, or a response after user feedback is provided. For more information about Bot feedback, see [Handle feedback events](creating-a-generic-bot.md).

4.  Get user input and call the bot's *ProcessMessage* method to make the bot invoke its message handlers in order to handle user messages. In the following example, the Main method repeatedly receives user messages and asks the bot to process them, stopping when the user enters "exit".
    
    ```csharp
    static void Main(string[] args)
    {
        Bot bot = new Bot();
        bot.Replied += new ReplyEventHandler(bot_Replied);
     
        string userMessage = Console.ReadLine();
        while (userMessage != "exit")
        {
            bot.ProcessMessage(userMessage);
            userMessage = Console.ReadLine();
        }
    }
    ```

The *Bot* class also exposes additional functionality that might be useful to you.

  - You can handle the bot's *FailedToUnderstand* event, which the bot raises when none of its message handlers is able to handle a user message. You can handle this event to show the bot's help text (such as "Sorry I didn't understand you. Say help to see what I can understand by default"), or an error in a red color or message box.

  - Set the static property *UseEmoticons* to enable or disable the use of emoticons in your bot's message handlers, as long as such emoticons were implemented using the *BuildABot.Util.Emoticons* class.

  - The *ConversationReplyCount* property indicates the interaction number (that is, how many times the bot replied) for the conversation being handled by the current message handler. If another handler starts handling the user message, the conversation count restarts. This way, this property is more useful for multi-state message handlers.

  - For more specialized behavior, you can implement your own bot by overriding the *Bot* class. Then, you can override the *PreProcessMessageContent* method if you always need to change or decorate user messages in any way (for example, by adding quotes to or trimming every user message).

## Handle feedback events

To improve the intelligence of your bot, you might want it to collect feedback from its users, so that they can tell whether the bot was helpful in handling a given user message. You can then store the user feedback anywhere you want, to process it later, or react as soon as you receive feedback.

### To enable feedback collection in your bot

1.  Feedback in the Build a Bot framework can be collected per message handler. Turn on feedback collection in the message handlers by setting the message handler's *RequiresFeedback* property. This can also be set through the constructors of the base *MessageHandler* and *SingleStateMessageHandler* classes.
    
    ```csharp
    public HelloWorldMessageHandler()
             : base(@"hello|(how are you\?)")
    {
       this.RequiresFeedback = true;
    }
    ```

2.  Use the *BuildABot.Core.Feedback* namespace in the class where the bot (or your UC bot host) is instantiated and set up.
    

    > [!NOTE]
    > <P><STRONG>For Skype for Business bot developers</STRONG>: this note defines feedback behavior for a single bot instance. For Skype for Businessbots, the Build a Bot framework instantiates multiple bots (one per conversation). The <EM>UCBotHost</EM> class exposes a <EM>FeedbackEngine</EM> property, in the same way that the <EM>Bot</EM> class does. This way, whenever the code that follows refers to setting a <EM>bot.FeedbackEngine</EM> property, your code should have <EM>ucBotHost.FeedbackEngine</EM> if you are implementing a UC bot.</P>

    
    By default, after a feedback-enabled message handler handles a request, your bot will ask "Did I understand you correctly?" and expect the following as possible answers: yes, yep, y, sure, no, nope, n, not at all. You can change these default settings after instantiating your bot (or UC bot host), as follows:
    
    ```csharp
    static void Main(string[] args)
    {
        Bot bot = new Bot();
        // Changing bot's default feedback request question.
        bot.FeedbackEngine.FeedbackRequest = new Reply("how did I do?");
        // Changing bot's default feedback answer. Notice those are regular
        // expressions patterns that are matched against user input.
        bot.FeedbackEngine.PositiveFeedbackPattern = "well|ok|(not bad)";
        bot.FeedbackEngine.NegativeFeedbackPattern = "bad|terrible";
        // ...
    }
    ```

3.  Handle the bot's (or the UC bot host's) *FeedbackEngine.FeedbackCollected* event. This event handler has a *FeedbackCollectedEventArgs* parameter, through which you can get the feedback type (positive, negative, or not provided), the user's feedback message and the user's original message for whose handling the bot is requesting feedback. Finally, this method returns a *Reply* object, which is what the bot will say to the user to acknowledge that feedback was received.
    
    ```csharp
    static void Main(string[] args)
    {
        Bot bot = new Bot(); 
        // ... 
        bot.FeedbackEngine.FeedbackCollected += new FeedbackEventHandler(FeedbackEngine_FeedbackCollected); 
        // ...
    }
    static Reply FeedbackEngine_FeedbackCollected(object sender, FeedbackCollectedEventArgs e)
    {
        Reply reply = new Reply();
        switch (e.FeedbackType)
        {
           case FeedbackType.Positive:
              reply.Add("Great, good to know!");
              // Store positive feedback.
              break; 
           case FeedbackType.Negative:
              reply.Add("Sorry for that...");
              // Store negative feedback.
              break; 
           case FeedbackType.NotProvided:
              // Probably don't do anything in this case.
              break;
        } 
        return reply;
    }
    ```
    

    > [!NOTE]
    > If the user replied to the bot's feedback request with a message that does not match the positive or negative feedback patterns, the bot will still raise the *FeedbackCollected* event (with the *NotProvided* value as its feedback type). The bot will behave as if a new conversation has started; that is, the bot will understand the user message not as a feedback response but as a new message that must be handled.

    
    By default, when your bot receives negative feedback, it will try to handle the *original* user message again (the first message that started the current conversation), this time with another message handler. For that, the bot will invoke the message handler that reported the next highest confidence level (as long as the level is greater than zero) for the original user message. If no more message handlers remain, or if all remaining message handlers reported zero confidence, the bot will raise the *FailedToUnderstand* event. If you want to prevent the bot from trying other message handlers when it receives negative feedback, set its *GiveUpOnNegativeFeedback* property to true.

## Test your bot

It is a good idea for you to create test automation to ensure that your bot is working correctly, especially the message handlers. You can also add a Visual Studio Test project to your solution (for more information, see [A Unit Testing Walkthrough with Visual Studio Team Test](https://msdn.microsoft.com/en-us/library/ms379625\(vs.80\).aspx)) and instantiate a bot in your test class/methods to test the behavior of your message handlers. Make sure that all the desired message handler assemblies (DLLs) or projects are referenced from the test project.

For some integrated manual testing, the Build a Bot framework comes with two sample host environments in which test bots are instantiated: *BuildABot.Samples.WindowsForms* and *BuildABot.Samples.CommandPrompt*. Just copy your message handler DLLs to the same folder where the sample executable is located. When running the executable, you will be able to test the logic of your message handlers by interacting with test (console/windows) bots.

