$(function () {
    $(window).scroll(function () {
        var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
        switchLogo(scrollTop);
    });
});
function switchLogo(pos) {
    if (pos >= $('#big-logo').height()) {
        $('#top-shadow').hide();
        document.getElementById('small-logo').style.opacity = '1';
        document.getElementById('big-logo').style.opacity = '0';
        document.getElementById('bar').style.backgroundColor = 'rgba(0,0,0, 0.8)';

    }
    else {
        $('#top-shadow').show();
        document.getElementById('bar').style.backgroundColor = 'transparent';
        document.getElementById('small-logo').style.opacity = '0';
        document.getElementById('big-logo').style.opacity = '1';
    }
}