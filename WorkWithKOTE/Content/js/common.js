/**
 * Created by DencoDance on 01.03.2015.
 */
$(function() {
 $(window).scroll(function(){
     var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
     makeFixed(scrollTop);
 });
});
function makeFixed(pos) {
    if (pos >= 100) {
        //document.getElementById('navigate').style.top = '0';
        document.getElementById('navigate').style.position = 'fixed';
        document.getElementById('small-logo').style.opacity = '1';
        document.getElementById('bar').style.backgroundColor = 'rgba(0,0,0, 0.6)';
        if (document.getElementById('big-logo')) {
            document.getElementById('big-logo').style.opacity = '0';
        }
    
    }
    else {
        document.getElementById('navigate').style.position = 'static';
        document.getElementById('bar').style.backgroundColor = 'transparent';
        document.getElementById('small-logo').style.opacity = '0';
        if (document.getElementById('big-logo')) {
            document.getElementById('big-logo').style.opacity = '1';
        }
    }
}