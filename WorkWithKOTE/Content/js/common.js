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
        $('#bar').css('backgroundColor', 'rgba(255,255,255, 1)');
        $('nav').addClass('nav-other');
    }
    else 
    {
        $('nav').removeClass('nav-other');
        $('#user_name').css({ 'color': 'white' ,'background-image':'url("/Content/img/user.png")'});

        $('#Searching').attr('src', '/Content/img/Searching.png');
        $('.separator').attr('src', '/Content/img/separator.png');
        $('#small-logo').attr('src', '/Content/img/small-logo.png');
        if ( pos >= $('#big-logo').height() || pos > 200 )
        {
            $('#small-logo').css('opacity','1');
            $('#big-logo').css('opacity', '0');
            $('#bar').css('backgroundColor', 'rgba(0,0,0, 0.6)');
            $('#top-shadow').hide();
        }

        else {
            $('#bar').css('backgroundColor', 'rgba(0,0,0, 0.2)');
               $('#small-logo').css('opacity', '0');
               $('#big-logo').css('opacity', '1');
               $('#top-shadow').show();
        }

        if ( $('#second-nav') && ( document.getElementById('second-nav').getBoundingClientRect().top <= $('#bar').height() ) ) {
             $('#second-nav').css('opacity', '0');
             $('#second-navigation').css('border-top','1px solid #ffffff').slideDown();
        }
        else {
               $('#second-nav').css('opacity', '1');
               $('#second-navigation').css('border-top','1px solid transparent').slideUp(200);
        }
    }
}

$(function ()
{
    $('#block_links').css('width', $('#user_name').width() + 57 + 'px');
    $('#block_enter').css('width', $('#user_name').width() + 53 + 'px');

    $('#user_name').click(function () {
        $('#block_links,#block_enter').toggleClass('hidden');
    });

    $(window).scroll(function () {
        $('#block_links,#block_enter').addClass('hidden');
    });

    $('body').click(function (event) {
        if ( $(event.target).attr('id') != 'user_name' && $(event.target).attr('id') != 'signForm' && !$(event.target).hasClass('notHide') ) {
             $('#block_links,#block_enter').addClass('hidden');
       }
    });

    $('#enter_button').click(function(){
        $('#block_links,#block_enter').addClass('hidden');
    });
});