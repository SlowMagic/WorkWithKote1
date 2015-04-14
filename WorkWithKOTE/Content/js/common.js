/**
 * Created by DencoDance on 01.03.2015.
 */
$(function () {
     switchLogo(0);
     $(window).scroll(function () {
         var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
         switchLogo(scrollTop);
     });
     $(document).ready(function () {
         $('a[href*=#]').bind("click", function (e) {
             var anchor = $(this);
             $('html, body').stop().animate({
                 scrollTop: $(anchor.attr('href')).offset().top - 200
             }, 1000);
             e.preventDefault();
         });
         return false;
     });
 });
function switchLogo(pos) {
    if ($('#big-logo').length == 0) {
        document.getElementById('small-logo').style.opacity = '1';
        $('#top-shadow').hide();
        $('#user').attr('src', '/Content/img/user-black.png');
        $('#Searching').attr('src', '/Content/img/Searching-black.png');
        $('.separator').attr('src', '/Content/img/separator-black.png');
        $('#small-logo').attr('src', '/Content/img/small-logo-red.png');
        document.getElementById('bar').style.backgroundColor = 'rgba(255,255,255, 1)';
        $('#username').addClass('username-other');
        $('nav').addClass('nav-other');
    }
    else {
        if (pos >= $('#big-logo').height() || pos > 200) {
            document.getElementById('small-logo').style.opacity = '1';
            document.getElementById('big-logo').style.opacity = '0';
            document.getElementById('bar').style.backgroundColor = 'rgba(0,0,0, 0.6)';
            $('#top-shadow').hide();
        }
        else {
            document.getElementById('bar').style.backgroundColor = 'transparent';
            document.getElementById('small-logo').style.opacity = '0';
            document.getElementById('big-logo').style.opacity = '1';
            $('#top-shadow').show();
        }
        if (document.getElementById('second-nav') && (document.getElementById('second-nav').getBoundingClientRect().top <= $('#bar').height())) {
            document.getElementById('second-nav').style.opacity = '0';
            document.getElementById('second-navigation').style.opacity = '1';
        }
        else {
            document.getElementById('second-nav').style.opacity = '1';
            document.getElementById('second-navigation').style.opacity = '0';
        }
    }
}