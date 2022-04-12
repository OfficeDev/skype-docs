---
title: LocationRequiredEnum for Skype for Business 2015
description: "LocationRequiredEnum is an enumeration of location required values. The values include Disclaimer, No, and Yes."
---
# LocationRequiredEnum


_** Applies to: **Skype for Business 2015_

An enumeration of location required values.
            
## Members



|**Name**|**Description**|
|:-----|:-----|
|Disclaimer|If location is not specified in the E911 Invite then show an alertindicator and text about entering a location, and if the user clicksthrough then show the EmergencyServiceDisclaimer.|
|No|Location in the E911 Invite is optional. In case it is not specfiedjust ignore|
|Yes|Location is required to be specified in E911 Invite. In case it isnot specified then triggers the client to show an alert indicatorand text about entering a location|
