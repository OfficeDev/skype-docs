# Video license acceptance dialog
```java
    /**
     * Shows a video license acceptance dialog if user has not been prompted before. If user
     * accepts license, call is started. Else, SkypeCallActivity is finished.
     * @param onlineMeetingFlag
     * @param discoveryUrl
     * @param authToken
     * @param meetingUrl
     * @return
     */
    private boolean checkVideoLicenseAcceptance(
        final Short onlineMeetingFlag
        , final String discoveryUrl
        , final String authToken
        , final String meetingUrl) {
        mApplication = Application.getInstance(this.getBaseContext());


        final Boolean canJoinCall = true;
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(this);

        if (!sharedPreferences.getBoolean(getString(R.string.acceptedVideoLicense),false)) {
            AlertDialog.Builder alertDialogBuidler = new AlertDialog.Builder(this);
            alertDialogBuidler.setTitle("Video License");
            alertDialogBuidler.setMessage(getString(R.string.videoCodecTerms));
            alertDialogBuidler.setPositiveButton("Accept", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    mApplication.getConfigurationManager().setEndUserAcceptedVideoLicense();
                    setLicenseAcceptance(true);
                    joinTheCall(onlineMeetingFlag,meetingUrl,discoveryUrl,authToken);

                }
            });
            alertDialogBuidler.setNegativeButton("Decline", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    setLicenseAcceptance(false);
                    finish();

                }
            });
            alertDialogBuidler.show();

        } else {
            joinTheCall(onlineMeetingFlag,meetingUrl,discoveryUrl,authToken);
        }

        return canJoinCall;
    }

        /**
     * Writes the user's acceptance or rejection of the video license
     * presented in the alert dialog
     * @param userChoice  Boolean, the user's license acceptance choice
     */
    private void setLicenseAcceptance(Boolean userChoice){
        SharedPreferences sharedPreferences = PreferenceManager
                .getDefaultSharedPreferences(this);
        sharedPreferences.edit()
                .putBoolean(
                        getString(
                                R.string.acceptedVideoLicense)
                        ,userChoice).apply();
        sharedPreferences.edit()
                .putBoolean(
                        getString(
                                R.string.promptedForLicense)
                        ,true).apply();


    }


```
