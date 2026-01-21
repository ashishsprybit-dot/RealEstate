$.showprogress = function(progTit, progText, progImg)
{
    $.hideprogress();
    $("BODY").append('<div id="processing_overlay"></div>');
    $("BODY").append(
      '<div id="processing_container">' +
        '<h1 id="processing_title">' + progTit + '</h1>' +
        '<div id="processing_content">' +
          '<div id="processing_message">'+ progText + 
                      '<br/><br/>' + progImg + '</div>' +
        '</div>' +
      '</div>');
     
    var pos =  'fixed'; 
    
    $("#processing_container").css({
        position: pos,
        zIndex: 99999,
        padding: 0,
        margin: 0
    });
        
    $("#processing_container").css({
        minWidth: $("#processing_container").outerWidth(),
        maxWidth: $("#processing_container").outerWidth()
    });
      
    var top = (($(window).height() / 2) - 
      ($("#processing_container").outerHeight() / 2)) + (-75);
    var left = (($(window).width() / 2) - 
      ($("#processing_container").outerWidth() / 2)) + 0;
    if( top < 0 ) top = 0;
    if( left < 0 ) left = 0;
    
    // IE6 fix
    
    
    $("#processing_container").css({
        top: top + 'px',
        left: left + 'px'
    });
    $("#processing_overlay").height( $(document).height() );
}

$.hideprogress = function()
{
    $("#processing_container").remove();
    $("#processing_overlay").remove();
}