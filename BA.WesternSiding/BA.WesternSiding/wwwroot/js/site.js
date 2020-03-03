// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(function () {
    //Make external links open in a new window.
    $("a").filter(function () {
        return this.hostname && this.hostname !== location.hostname;
    }).attr('target', '_blank');

    mobileLayout = false;
    function moveContent() {
        if (mobileLayout == false && $(document).width() < 960) {
            mobileLayout = true;
            $('header#header').after($('form#search')).after($('nav#nav-bottom'));
            $('article#content div.product h1:eq(0)').after($('article#content div.product div.images'));
            $("nav a[href*='/categories/']").filter("nav a[href!='/categories/products']").parent().addClass('hidden');
        }
        else if (mobileLayout == true && $(document).width() >= 960) {
            mobileLayout = false;
            $('div#middle').append($('nav#nav-bottom'));
            $('nav#nav-top').after($('form#search'));
            $('article#content div.product h1:eq(0)').before($('article#content div.product div.images'));
            $("nav a[href*='/categories/']").parent().removeClass('hidden');
        }
    }
    $(window).resize(moveContent);
    moveContent();

    $(window).load(function () {
    });
});
