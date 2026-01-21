function n() {
    var a = 0,
        n = setInterval(function () {
            a += 1, 5 === a && clearInterval(n), window.dispatchEvent(new Event("resize"));
        }, 62.5);
}
function ToggleChart() {
    ($('.app').toggleClass("has-compact-menu"), n()),
        $(this).hasClass("mobile-sidebar-toggler") && (a("body").toggleClass("sidebar-mobile-show"), n());
}
$(document).ready(function () {
    ToggleChart();
    $('.hamburger-box').click(function () {
        ToggleChart();
    });
})
