
# Getting started with Skype Web SDK development


 _**Applies to:** Skype for Business_

 **In this article**  
[Using the Skype Web SDK in your application](#sectionSection0)  
[Skype for Business Web App Plug-in](#sectionSection1)  
[Bootstrapping the application](#sectionSection2)  
[Developing Skype Web SDK applications](#sectionSection3)  
[ Downloading and running the Skype Web SDK Samples](#sectionSection4)


This section shows how to get started developing web applications with the Skype Web SDK. It also provides guidance on using the Skype Web SDK samples.

## Using the Skype Web SDK in your application
<a name="sectionSection0"> </a>

>**Note** Please read the [release notes](ReleaseNotes.md) for the General Availability release of this SDK before you start a new production web app or update your existing Skype for Business Web SDK-powered app.

Because the Skype Web SDK is hosted through the Content Delivery Network (CDN), you do not have to install it. Instead, you add Skype for Business functionality to your web applications simply by adding a ```<script/>``` tag to your HTML file that points to the Skype Web SDK entry point (swx.cdn.skype.com). Doing so bootstraps the Skype Web SDK JavaScript libraries in your application. For more information, see [Retrieve the API entry point and sign in a user](GetAPIEntrySignIn.md)

## Skype for Business Web App Plug-in/ORTC Support
<a name="sectionSection1"> </a>

The Skype for Business Web App plugin, available for browsers IE 11 and Safari, provides audio/video media capabilities. It is available for Windows and Mac computers from the following download locations:

- [Windows Download](https://swx.cdn.skype.com/s4b-plugin/16.2.0.67/SkypeMeetingsApp.msi)
- [Mac Download](https://swx.cdn.skype.com/s4b-plugin/16.2.0.67/SkypeForBusinessPlugin.pkg)


 **Note**  To enable audio/video functionality, browser applications must install the Skype for Business Web App Plug-in. Note that this restricts media modalities to desktop browsers only.

ORTC support is available in the Microsoft Edge browser, which will allow audio/video calls without a plugin installed.  ORTC support for other browsers will be added in the near future, however the plugins are still available as a fallback.
 


## Bootstrapping the application
<a name="sectionSection2"> </a>

The programming environment for the Skype Web SDK is JavaScript. Your web application must have a reference to the Skype Web SDK JavaScript libraries in the form of a ```<script>``` tag that points to the Skype Web SDK service endpoint (swx.cdn.skype.com). Doing so enables your application to bootstrap with the Skype Web SDK JavaScript libraries.


 **Note**  BY USING THE SOFTWARE LOCATED HERE: [https://swx.cdn.skype.com](https://swx.cdn.skype.com), YOU ACCEPT THE TERMS OF THE  _[Microsoft Software License Terms](TermsOfService.md)_ IF YOU DO NOT ACCEPT THEM, DO NOT USE THE SOFTWARE.The aforementioned license terms apply to your use of content from the domain swx.cdn.skype.com.

Add a reference to the bootstrapper to your client application's HTML file by inserting a ```<script>``` tag as follows:




```html
<script src="https://swx.cdn.skype.com/shared/v/1.2.35/SkypeBootstrap.min.js"></script> 
```

Then initialize the bootstrapper with the appropriate [API key](APIProductKeys.md) as in the following example code:




```js
// Reference to SkypeBootstrap.min.js
// Implements the Skype object model via https://swx.cdn.skype.com/shared/v/1.2.35/SkypeBootstrap.min.js

// Call the application object
var config = {
 apiKey: 'a42fcebd-5b43-4b89-a065-74450fb91255', // SDK
 apiKeyCC: '9c967f6b-a846-4df2-b43d-5167e47d81e1' // SDK+UI
}; 
var Application

Skype.initialize({ apiKey: config.apiKey }, function (api) {
        window.skypeWebApp = new api.application();
        //Make sign in table appear
        $(".menu #sign-in").click();
        // whenever client.state changes, display its value
        window.skypeWebApp.signInManager.state.changed(function (state) {
            $('#client_state').text(state);
        });
    }, function (err) {
        console.log(err);
        alert('Cannot load the SDK.');
    });

// Sign-in code typically follows here.


```


## Developing Skype Web SDK applications
<a name="sectionSection3"> </a>

After you have run the samples and examined the sample code, and you are ready to develop applications using the Skype Web SDK, see the topics in [Developing applications with Skype Web SDK](DevelopApplications.md). These topics explain the scenarios for developing Skype Web SDK applications: In the online server scenario, your application authenticates against Azure Active Directory. In the on-premises server scenario, your application authenticates against your own server with its own Active Directory.


## Downloading and running the Skype Web SDK Samples
<a name="sectionSection4"> </a>

The Microsoft Skype Web SDK includes a set of samples hosted on github at [Skype Web SDK Samples](https://github.com/OfficeDev/skype-web-sdk-samples).
  For a detailed description of what the samples do, and an explanation of how to run them, see [Downloading and running the Skype Web SDK samples](DownloadRunSamples.md).

