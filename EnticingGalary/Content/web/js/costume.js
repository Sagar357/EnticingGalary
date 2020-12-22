$(window).scroll(function() {
    if ($(document).scrollTop() > 100) {
        $('.header-area ').css({"background": "#fff","box-shadow": "0px 2px 3px rgba(0,0,0,0.2)", "transition":".7s"});
        $('.search-top').show();
    } 
    else {
        
        $('#nav').css({"background": "transparent","box-shadow": "none"});
        $('.search-top').hide();
    }
});



    