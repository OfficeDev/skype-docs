$(function() {
    'use strict';

    Skype.initialize({
        apiKey: config.apiKey
    }, function(api) {
        window.skypeWebAppCtor = api.application;
        window.skypeWebApp = new api.application();
        //Make sign in table appear
        $(".menu #sign-in").click();
        // whenever client.state changes, display its value
        window.skypeWebApp.signInManager.state.changed(function(state) {
            $('#client_state').text(state);
        });

    }, function(err) {
        console.log(err);
        alert('Cannot load the SDK.');
    });

   window.codifyElement = codifyElement;

    //load template file
   
    ajaxrequest('get', 'content-template.html', '', 'text').done(function (result) {
        $("body").append(result);
    });

    // Load & render code snippets
    function codifyElement(element, file, isModule) {
        element.html($("#code-template").tmpl({
            file: file,
            isModule: isModule
        }));
        
        ajaxrequest('get', element.find(".codeHeader a").html(), '', 'text').done(function (result) {
            element.find("div").toggle();
            element.find(".codeBody pre").html(result.replace(/</g, "&lt;").replace(/>/g, "&gt;"));
        });
    }
    
    $("body").delegate(".code", "click", function () {
        
        var element = $(this).next("div"), value = $(this).text().split(" ")[0];

        if (element.is(":hidden")) {
            $(this).text(value + " - Click to Collapse");
        } else {
            $(this).text(value + " - Click to Expand");
        }

        $(this).next("div").toggle();
        $(this).next("div").find("div").toggle();
        
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