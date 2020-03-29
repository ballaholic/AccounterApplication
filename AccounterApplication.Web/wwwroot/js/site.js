// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Language Cookies
$(function () {
    var url;
    var redirectUrl = window.location.href;
    var languageCheckbox = $("#switchLang");

    languageCheckbox.change(function (event) {
        if (event.target.checked) {
            url = "/Home/SetCulture?culture=" + "en-US";
        } else {
            url = "/Home/SetCulture?culture=" + "bg-BG";
        }

        $.get(url)
            .done((result) => {
                setTimeout(function () { window.location.href = redirectUrl; }, 300);
            })
            .fail((error) => {
                window.location.href = redirectUrl;
            });
    });
}());