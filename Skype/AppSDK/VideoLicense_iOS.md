# Video license acceptance code

**Swift**

```Swift
    /**
     * Shows a video license acceptance dialog if user has not been prompted before. If user
     * accepts license, call is started. Else, user cannot start Audio/Video call.
     */

            let sfb: SfBApplication = SfBApplication.sharedApplication()
            let config: SfBConfigurationManager = sfb.configurationManager
            let key = "AcceptedVideoLicense"
            let defaults = NSUserDefaults.standardUserDefaults()
            
            if defaults.boolForKey(key) {
            /**
            * Notify that user has accepted the Video license.
            *
            * This is required to proceed with features that potentially use video codecs.
            * Until this method is called, any attempt to use those features will fail.
            */
                config.setEndUserAcceptedVideoLicense()
                self.performSegueWithIdentifier("joinOnlineAudioVideoChat", sender: nil)
                
            } else {
                
                /** Show video license acceptance view. 
                *   MicrosoftLicenseViewController is class that shows video license 
                *   and stores user's acceptance or rejection of the video license
                **/

                let vc = self.storyboard?.instantiateViewControllerWithIdentifier("MicrosoftLicenseViewController") as! MicrosoftLicenseViewController
                vc.delegate = self
                self.presentViewController(vc, animated: true, completion: nil)
            }

     /**
     * Writes the user's acceptance or rejection of the video license
     **/

        let key = "AcceptedVideoLicense"
        let defaults = NSUserDefaults.standardUserDefaults()
        defaults.setBool(true, forKey: key)
        let sfb: SfBApplication = SfBApplication.sharedApplication()
        let config: SfBConfigurationManager = sfb.configurationManager
        config.setEndUserAcceptedVideoLicense()
        self.performSegueWithIdentifier("joinOnlineAudioVideoChat", sender: nil)
        

```
