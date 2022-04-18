---
title: Manage devices in devicesManager
---

 _**Applies to:** Skype for Business | Skype for Business 2015_

 **In this article**<br/>
[Subscribing to Device Changes](#sectionSection0)<br/>
[Enumerating Available Devices](#sectionSection1)<br/>
[Selected Devices](#sectionSection2)



## Subscribing to Device Changes
<a name="sectionSection0"> </a>

Before accessing the device lists in devicesManager, the client must call that respective list's subscribe() function. After this function is called changes to the collection are exposed to the client, and the client may enumerate the devices in that list.


```js

client.devicesManager.cameras.subscribe();
client.devicesManager.cameras.added(function (camera) { … });
client.devicesManager.cameras.removed(function (camera) { … });

client.devicesManager.microphones.subscribe();
client.devicesManager.microphones.added(function (microphone) { … });
client.devicesManager.microphones.removed(function (microphone) { … });

client.devicesManager.speakers.subscribe();
client.devicesManager.speakers.added(function (speaker) { … });
client.devicesManager.speakers.removed(function (speaker) { … });

```


## Enumerating Available Devices
<a name="sectionSection1"> </a>

The devicesManager object has three collections for available devices: cameras, microphones, and speakers. Each collection can be iterated over to get a reference to each device:


```js

client.devicesManager.cameras.subscribe();

console.log("Available cameras:");
for(var i = 0; i < client.devicesManager.cameras.size(); i++) {
	var camera = client.devicesManager.cameras(i);
	console.log(camera.name());
}

client.devicesManager.microphones.subscribe();

console.log("Available microphones");
for(var i = 0; i < client.devicesManager.microphones.size(); i++) {
	var microphone = client.devicesManager.microphones(i);
	console.log(microphone.name());
}

client.devicesManager.speakers.subscribe();

console.log("Available speakers:");
for(var i = 0; i < client.devicesManager.speakers.size(); i++) {
	var speakers = client.devicesManager.speakers(i);
	console.log(speaker.name());
}

```


## Selected Devices
<a name="sectionSection2"> </a>

The devicesManager object has a reference to each currently selected device: selectedCamera, selectedMicrophone, and selectedSpeaker. Each reference can be changed with their respective set() function. (Note that this function will appear enabled but will have no effect if the device is already in use.) The client can subscribe to changes to the selected devices by calling their respective changed() functions.


```js

client.devicesManager.selectedCamera.changed(function (newCamera) {
	console.log("The selected camera is now " + newCamera.name());
});
var otherCamera = client.devicesManager.cameras(1);
client.devicesManager.selectedCamera.set(otherCamera);

client.devicesManager.selectedMicrophone.changed(function (newMicrophone) {
	console.log("The selected microphone is now " + newMicrophone.name());
});
var otherMicrophone = client.devicesManager.microphones(1);
client.devicesManager.selectedMicrophone.set(otherMicrophone);

client.devicesManager.selectedSpeaker.changed(function (newSpeaker) {
	console.log("The selected speaker is now " + newSpeaker.name());
});
var otherSpeaker = client.devicesManager.speakers(1);
client.devicesManager.selectedSpeaker.set(otherSpeaker);

```

## Testing for WebRTC/ORTC support

Using the devicesManager object you can test whether the browser you are running on supports WebRTC or ORTC based media.


```js

// true indicates that the browser supports either WebRTC or ORTC
var isBrowserMediaSupported = client.devicesManager.mediaCapabilities.isBrowserMediaSupported();

```

## Testing for installed plugins and retrieving plugin download links

If your web application is running on a browser that does not support WebRTC or ORTC (e.g. Microsoft Internet Explorer) you can check to see if the user has installed the Skype for Business Web App Plug-in. Once you know this you can prompt the user to download the plugin using the plugin download links that the Web SDK provides you.


```js

var mc = app.devicesManager.mediaCapabilities;

mc.isPluginInstalled.get().then(function(isInstalled) {
	if (!isInstalled) {
		mc.pluginDownloadLinks.get().then(function(pluginDownloadLinks) {
			Var msiLink = pluginDownloadLinks(‘msi’); // for Windows
			Var macPkgLink = pluginDownloadLinks(‘pkg’); // for Mac
		}
	} else {
		mc.installedVersion.get().then(function(version) {
			console.log('Plugin version: ', version);
		});
	}
});


```

