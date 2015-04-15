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
        $('#user_name').css('color', 'white');

        if ( pos >= $('#big-logo').height() || pos > 200 )
        {
            $('#small-logo').css('opacity','1');
            $('#big-logo').css('opacity', '0');
            $('#bar').css('backgroundColor', 'rgba(0,0,0, 0.6)');
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
             $('#second-navigation').css('border-top','1px solid #ffffff').slideDown();
        }
        else {
               $('#second-nav').css('opacity', '1');
               $('#second-navigation').css('border-top','1px solid transparent').slideUp(200);
        }
    }
}

$(function () {
    $('#user_name').click(function () {
        $('#block_links,#block_enter').toggleClass('hidden');
    });

    $(window).scroll(function () {
        $('#block_links').addClass('hidden');
    });

  //  $('body').click(function (event) {
   //     if ( $(event.target).attr('id') != 'user_name' ) {
    //         $('#block_links').addClass('hidden');
     //   }
   // });
});