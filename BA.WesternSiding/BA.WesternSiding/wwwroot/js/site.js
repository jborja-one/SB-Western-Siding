// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(function () {
    //Make external links open in a new window.
    $("a").filter(function () {
        return this.hostname && this.hostname !== location.hostname;
    }).attr('target', '_blank');

    collapsedSubNav = false;
    function moveContent() {
        if (collapsedSubNav == false && $(document).width() < 900) {
            collapsedSubNav = true;
            $('input[type="checkbox"].activate').prop("checked", false);
        }
        else if (collapsedSubNav == true && $(document).width() >= 900) {
            collapsedSubNav = false;
            $('input[type="checkbox"].activate').prop("checked", true);
        }
    }
    $(window).resize(moveContent);
    moveContent();

    $(window).load(function () {
    });
});
