/**
 * Created by DencoDance on 01.03.2015.
 */
$(function ()
{
    switchLogo(0);
     $(window).scroll(function () {
         var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
         switchLogo(scrollTop);
     }); 
});

$(function () {
    $('a[href*=#]').bind("click", function (e) {
        var anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $(anchor.attr('href')).offset().top - 200
        }, 1000);
        e.preventDefault();
    });
    return false;
});

function switchLogo(pos)
{
    if ($('#big-logo').length == 0)
    {
        $('#small-logo').css('opacity','1');
        $('#top-shadow').hide();
        $('#user').attr('src', '/Content/img/user-black.png');
        $('#Searching').attr('src', '/Content/img/Searching-black.png');
        $('.separator').attr('src', '/Content/img/separator-black.png');
        $('#small-logo').attr('src', '/Content/img/small-logo-red.png');
        $('#bar').css('backgroundColor','rgba(255,255,255, 1)');
        $('#username').addClass('username-other');
        $('nav').addClass('nav-other');
    }
    else 
    {
        if (pos >= $('#big-logo').height() || pos > 200)
        {
            $('#small-logo').css('opacity','1');
            $('#big-logo').css('opacity', '0');
            $('#bar').css('backgroundColor','rgba(0,0,0, 0.6)');
            $('#top-shadow').hide();
        }

        else {
               $('#bar').css('backgroundColor', 'transparent');
               $('#small-logo').css('opacity', '0');
               $('#big-logo').css('opacity', '1');
               $('#top-shadow').show();
        }

        if ( $('#second-nav') && ( document.getElementById('second-nav').getBoundingClientRect().top <= $('#bar').height() ) ) {
             $('#second-nav').css('opacity', '0');
             $('#second-navigation').css('opacity', '1');
        }
        else {
            $('#second-nav').css('opacity', '1');
            $('#second-navigation').css('opacity', '0');
        }
    }
}

$(function () {
    $('#user_name').click(function () {
        $('#block_links').toggleClass('hidden');
    });
});