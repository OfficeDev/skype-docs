$(function() {
    'use strict';

    Skype.initialize({
        apiKey: config.apiKey
    }, function(api) {
        window.skypeWebAppCtor = api.application;
        window.skypeWebApp = new api.application();
        console.log('Skype web application created');
        //load sign-in module
        window['sign-in_load']();

        // whenever client.state changes, display its value
        window.skypeWebApp.signInManager.state.changed(function(state) {
            $('#client_state').text(state);
        });

    }, function(err) {
        console.log(err);
        alert('Cannot load the SDK.');
    });
});

function monitor(title, promise) {
    console.log(title);
    promise.then(function(res) {
        return console.log(title + '...done');
    }, function(err) {
        return console.log(title + '...failed', err && err.stack || err);
    });
}

function ajaxrequest(verb,url,data,datatype) {
    return $.ajax({
        url: url,
        type: verb,
        dataType: datatype,
        data:data
    });
}