
// General.js

function msgboxTop(msg,type)
{var color='white';if(type=='success')
color='white';if(type=='error')
color='red';$.floatingMessage(msg,{show:"blind",align:"right",verticalAlign:"top",time:5000,color:color});}
function myAjax(strUrl,ctrlData,updateCtrl)
{$.ajax({url:strUrl,type:'POST',data:ctrlData,success:function(resp){if(updateCtrl!=null){$('#'+updateCtrl).replaceWith(resp);}}});}
function myAjaxHtml(strUrl,ctrlData,updateCtrl)
{$.ajax({url:strUrl,type:'POST',data:ctrlData,success:function(resp){if(updateCtrl!=null){$('#'+updateCtrl).html(resp);}}});}
function AjaxDataPostNoLoad(ctrldata,strUrl,ctrlUpdate,strType){$.ajax({url:strUrl,type:strType,data:ctrldata,success:function(resp){$('#'+ctrlUpdate).html(resp);}});}
function AjaxRequest(ctrlLoad,ctrlUpdate,strUrl,strType){$('#'+ctrlLoad).show();$.ajax({url:strUrl,type:strType,success:function(resp){$('#'+ctrlUpdate).html(resp);$('#'+ctrlLoad).hide();}});}
function AjaxFormPost(ctrlform,ctrlLoad,strUrl,ctrlUpdate){$('#'+ctrlLoad).show();$.ajax({url:strUrl,type:'POST',data:$('#'+ctrlform).serialize()+'&call=ajax',success:function(resp){$('#'+ctrlUpdate).html(resp);$('#'+ctrlLoad).hide();}});}
function AjaxDataPost(ctrldata,ctrlLoad,strUrl,ctrlUpdate,strType){$('#'+ctrlLoad).show();$.ajax({url:strUrl,type:strType,data:ctrldata,success:function(resp){$('#'+ctrlUpdate).html(resp);$('#'+ctrlLoad).hide();}});}
function DisableDropdownItem(drpCtrl,drpValue){for(i=0;i<drpCtrl.length;i++){if(parseInt(drpCtrl.options[i].value)==drpValue){drpCtrl.options[i].disabled="disabled";}}}
function ValidateDate(day,Month,year){var monthfield=Month;var dayfield=day;var yearfield=year;var dayobj=new Date(yearfield,monthfield-1,dayfield)
if((dayobj.getMonth()+1!=monthfield)||(dayobj.getDate()!=dayfield)||(dayobj.getFullYear()!=yearfield))
return false;return true;}
function ChngMemCountwiseState(CtrlCountry,CtrlUs,CtrlAus,CtrlCA,CtrlInd,TxtOther,drpAllState){CtrlUs.style.display='none';CtrlAus.style.display='none';CtrlCA.style.display='none';CtrlInd.style.display='none';TxtOther.style.display='none';drpAllState.style.display='none';TxtOther.value='';switch(parseInt(CtrlCountry.value)){case 226:CtrlUs.style.display='';break;case 14:CtrlAus.style.display='';break;case 37:CtrlCA.style.display='';break;case 103:CtrlInd.style.display='';break;case-1:drpAllState.style.display='';break;default:TxtOther.style.display='';break;}}
function ChangeCountrywiseState(CtrlCountry,CtrlUs,CtrlAus,CtrlCA,CtrlInd,TxtOther,LblTitle){$('#'+CtrlUs).hide();$('#'+CtrlAus).hide();$('#'+CtrlCA).hide();$('#'+CtrlInd).hide();$('#'+TxtOther).hide();$('#'+TxtOther).val('');$('#'+LblTitle).html('State/provinece');switch(parseInt($('#'+CtrlCountry).val())){case 226:$('#'+CtrlUs).show();break;case 14:$('#'+CtrlAus).show();break;case 37:$('#'+CtrlCA).show();break;case 103:$('#'+CtrlInd).show();break;default:$('#'+TxtOther).show();$('#'+LblTitle).html('Other State');break;}}
function trim(strComp){return jQuery.trim(strComp);}
function ValidateControl(formObject,fieldDescription,lblObject){var tempFormValue;var strError="";var iFocus=-1;var ErrCount=1;for(var i=0;i<ValidateControl.arguments.length;i=i+3){tempFormValue=trim($('#'+ValidateControl.arguments[i]).val());if(tempFormValue.length==0){if(strError!='')
strError=strError+"<br/> - "+ValidateControl.arguments[i+1];else
strError=" - "+ValidateControl.arguments[i+1];ErrCount=ErrCount+1;$('#'+ValidateControl.arguments[i+2]).attr('class','alert');if(iFocus==-1)
iFocus=i;}
else{$('#'+ValidateControl.arguments[i+2]).attr('class','');}}
if(strError.length!=0){return strError;}
else
return strError;}
function isEmailAddress(emField){var temField=emField.split('@');if(temField.length!=2){return false;}
else{if(temField[0].length==0){return false;}
else{var lstIndex=temField[1].lastIndexOf('.');if(lstIndex==-1){return false;}
else{if(temField[1].substring(lstIndex+1).length==2||temField[1].substring(lstIndex+1).length==3){return true;}
else{return false;}}}}
return true;}
function ShowHideCtrl(ShowCtrl,HideCtrl){$('#'+HideCtrl).slideUp('slow');$('#'+ShowCtrl).slideDown('slow');}
function ShowHideCtrlFade(ShowCtrl,HideCtrl){$('#'+HideCtrl).fadeOut('fast');$('#'+ShowCtrl).fadeIn('fast');}
function CharacterCount(obj,total){if(total==null)total=500;var len=obj.value.length;var newdiv;if(document.getElementById("note"+obj.id)==null){newdiv=document.createElement('div');newdiv.id="note"+obj.id;newdiv.className="";obj.parentNode.appendChild(newdiv);}
else{newdiv=document.getElementById("note"+obj.id);}
if(len==0){obj.parentNode.removeChild(newdiv);}
if(len>=total){var temp=obj.value.substring(0,total)
obj.value=temp;newdiv.innerHTML="You have reached maximum characters limit of <strong>"+total+"</strong>."}
else{newdiv.innerHTML="Your maximum characters limit is: <strong>"+total+"</strong>.<br> Current character count: <strong>"+len+"</strong>.";}}
function TrimStart(str,strchar){if(str.indexOf(strchar)==0){str=str.replace(strchar,'');}
return str;}
function PasswordChk(ctrl1,ctrl2,errMsg){var digit=0,chr=0;if($('#'+ctrl1).val()==$('#'+ctrl2).val()){if($('#'+ctrl1).val().length<6){return'Password must be at least 6 characters long and should contain at least one number or a letter.';}
else{var tmp=$('#'+ctrl1).val();for(var i=0;i<tmp.length;i++){if(tmp.charCodeAt(i)>=48&&tmp.charCodeAt(i)<=57){digit++;}
else if((tmp.charCodeAt(i)>=65&&tmp.charCodeAt(i)<=90)||(tmp.charCodeAt(i)>=97&&tmp.charCodeAt(i)<=122)){chr++;}}
if(!(digit>0&&chr>0)){return'Password must be at least 6 characters long and should contain at least one number or a letter.';}}}
else{return'Password and confirm passwords donâ€™t match. Please check both passwords and try again';}
return'';}
function DisplayMessage(ctrl,clsName,Msg){$('#'+ctrl).show();$('#'+ctrl).attr('class',clsName+' clear');$('#'+ctrl).html(Msg);setTimeout('$(\'#'+ctrl+'\').hide();',10000);}
function DisplMsg(Ctrl,ErrMsg,Msgclass){$('#'+Ctrl).show();$('#'+Ctrl).html(ErrMsg);$('#'+Ctrl).attr('class',Msgclass);}
function msgbox(ErrMsg,Title){jAlert(ErrMsg,Title);}
function DisplMsgFront(Ctrl,ErrMsg,Msgclass){$('#'+Ctrl).show();$('#'+Ctrl).css('color','#ff0000');$('#'+Ctrl).html(ErrMsg);$('#'+Ctrl).attr('class','');$('#'+Ctrl).addClass(Msgclass);fourPop(Ctrl);}
function ValidateControlAdmin(formObject,fieldDescription,lblObject){var tempFormValue;var strError="";var iFocus=-1;var ErrCount=1;for(var i=0;i<ValidateControlAdmin.arguments.length;i=i+3){if(typeof(ValidateControlAdmin.arguments[i])=='undefined'){return'Error';}
tempFormValue=jQuery.trim(ValidateControlAdmin.arguments[i].val());if(tempFormValue.length<15)
{deleteLoop=tempFormValue.length}
else
{deleteLoop=15}
for(var j=0;j<deleteLoop;j++){tempFormValue=tempFormValue.replace(/ /,"");}
if(tempFormValue.length==0){if(strError!='')
strError=strError+"<br/> - "+ValidateControlAdmin.arguments[i+1];else
strError=" - "+ValidateControlAdmin.arguments[i+1];ErrCount=ErrCount+1;ValidateControlAdmin.arguments[i+2].addClass('alert');if(iFocus==-1)
iFocus=i;}
else{ValidateControlAdmin.arguments[i+2].addClass('');}}
if(strError.length!=0){return strError;}
else
return strError;}
function DirValCtrl(concatStr){if(concatStr==undefined)
concatStr='<br/> - ';var lbl;var inctrl;var ErrMsg='';var IsFirst=0;$('span[class="red"]').each(function(){lbl=$(this).prev();if($(this).prev().length>0){inctrl=$(lbl).attr('for');if(inctrl.length>0&&$(lbl).attr('for').length>0){switch($('#'+inctrl).attr('type')){case"textarea":case"text":case"file":case"password":if($('#'+inctrl).val()==''&&$('#'+inctrl).is(':visible')==true){$('#'+inctrl).addClass('valerror');ErrMsg=ErrMsg+concatStr+stripHTML($(lbl).html());if(IsFirst==0)
$('#'+inctrl).focus();IsFirst=1;}
else
$('#'+inctrl).removeClass('valerror');$('#'+inctrl).attr('style','');break;case"select":if($('#'+inctrl).val()=='')
ErrMsg=ErrMsg+concatStr+stripHTML($(lbl).html());else
$('#'+inctrl).removeClass('valerror');break;}}}});return trim(ErrMsg);}
function isValidURL(url){var RegExp=/^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;if(RegExp.test(url)){ if(url.indexOf('http://') != -1 || url.indexOf('https://') != -1){return true;}else{return false;}}else{return false;}}
function findPosX(obj){var curleft=0;if(obj.offsetParent)
while(1){curleft+=obj.offsetLeft;if(!obj.offsetParent)
break;obj=obj.offsetParent;}
else if(obj.x)
curleft+=obj.x;return curleft;}
function findPosY(obj){var curtop=0;if(obj.offsetParent)
while(1){curtop+=obj.offsetTop;if(!obj.offsetParent)
break;obj=obj.offsetParent;}
else if(obj.y)
curtop+=obj.y;return curtop;}
function EncodeText(UrlText){UrlText=UrlText.replace(/%/g,'-per-');UrlText=UrlText.replace(/&/g,'-and-');UrlText=UrlText.replace(/\?/g,'-que-');UrlText=replaceCharacters(UrlText,'/','-sla-');return UrlText;}
function replaceCharacters(MainStr,ScanChar,RepChar){var newString=MainStr.split(ScanChar);newString=newString.join(RepChar);return newString;}
function CreateTag(TagName,TagProp,TagValue){var ctrl=document.createElement(TagName);for(var i=0;i<CreateTag.arguments.length-1;i++){if(document.all){if(CreateTag.arguments[i+1]=='style'){var tmpStyle=CreateTag.arguments[i+2].split(':');switch(tmpStyle[0]){case"display":ctrl.style.display=tmpStyle[1];break;}}
else if(CreateTag.arguments[i+1]=='class')
ctrl.setAttribute('className',CreateTag.arguments[i+2]);else
ctrl.setAttribute(CreateTag.arguments[i+1],CreateTag.arguments[i+2]);}
else{ctrl.setAttribute(CreateTag.arguments[i+1],CreateTag.arguments[i+2]);}
i=i+1;}
return ctrl;}
function isImage(obj){if(obj.value.length>0){if(obj.value.length>4){var tmpExt=obj.value.split('.');var ext=tmpExt[tmpExt.length-1];if(ext.toLowerCase()=='jpg'||ext.toLowerCase()=='bmp'||ext.toLowerCase()=='tif'||ext.toLowerCase()=='tiff'||ext.toLowerCase()=='jpeg'||ext.toLowerCase()=='gif'||ext.toLowerCase()=='png'){return true;}
else{return false;}}
else{return false;}}
return false;
}

function isPdf(obj) {
   
    if (obj.value.length > 0) {
        if (obj.value.length > 4) {
            var tmpExt = obj.value.split('.'); var ext = tmpExt[tmpExt.length - 1]; if (ext.toLowerCase() == 'pdf' || ext.toLowerCase() == 'doc' || ext.toLowerCase() == 'docx' ) { return true; }
            else { return false; } 
        }
        else { return false; } 
    }
    return false;
}
function NumericOnly(ctrl){var tmpValue='';var strValue=ctrl.value;for(var i=0;i<strValue.length;i++){if(strValue[i].charCodeAt(0)>=48&&strValue[i].charCodeAt(0)<=57){tmpValue+=strValue[i];}}
ctrl.value=tmpValue;}
function isNumberKey(evt)
{var charCode=(evt.which)?evt.which:event.keyCode
if(charCode>31&&(charCode<48||charCode>57))
return false;return true;}
function isNumberKey2(evt){var charCode=evt.which;if(charCode>31&&(charCode<48||charCode>57))
return false;return true;}
function stripHTML(data){var re=/<\S[^><]*>/g;return trim(data.replace(re,""));}
function ChangeCity(pageurl,ddlState,objddlCity,objimgLoading){ddlState.change(function(){objimgLoading.show();var Count=0;while(document.getElementById(objddlCity.attr('id')).length>0){document.getElementById(objddlCity.attr('id')).options[0]=null;}
var myval=(jQuery("#"+ddlState.attr('id')+" > option:selected").attr("value"));if(myval!=0){jQuery.getJSON(pageurl+'?StateID='+myval,function(options){jQuery.each(options,function(){objddlState.append(jQuery("<option></option>").val(this['ID']).html(this['Name']));Count++;});if(Count==1){objddlCity.css("display","none");}
else{objddlCity.css("display","");}});}
objimgLoading.hide();});}
function DirValCtrlFront()
{if(concatStr==undefined)
concatStr='<br/> - ';var lbl;var inctrl;var ErrMsg='';var IsFirst=0;$('span[class="red"]').each(function(){lbl=$(this).siblings();if($(this).siblings().length>0)
{inctrl=$(lbl).attr('for');if(inctrl.length>0&&$(lbl).attr('for').length>0)
{switch($('#'+inctrl).attr('type'))
{case"textarea":case"text":case"file":case"password":if(jQuery.trim($('#'+inctrl).val())==''&&$('#'+inctrl).is(':visible')==true)
{ErrMsg=ErrMsg+concatStr+$(lbl).html();if(IsFirst==0)
$('#'+inctrl).focus();IsFirst=1;}
else
$('#'+inctrl).attr('style','');break;case"select":if($('#'+inctrl).val()=='')
ErrMsg=ErrMsg+'<br/> - '+$(lbl).html();break;case"checkbox":if(!$('#'+inctrl).attr('checked'))
ErrMsg=ErrMsg+'<br/> - '+$(lbl).html();break;}}}});return ErrMsg;}
(function($){var map=new Array();$.Watermark={ShowAll:function(){for(var i=0;i<map.length;i++){if(map[i].obj.val()==""){map[i].obj.val(map[i].text);map[i].obj.css("color",map[i].WatermarkColor);}else{map[i].obj.css("color",map[i].DefaultColor);}}},HideAll:function(){for(var i=0;i<map.length;i++){if(map[i].obj.val()==map[i].text)
map[i].obj.val("");}}}
$.fn.Watermark=function(text,color){if(!color)
color="#aaa";return this.each(function(){var input=$(this);var defaultColor=input.css("color");map[map.length]={text:text,obj:input,DefaultColor:defaultColor,WatermarkColor:color};function clearMessage(){if(input.val()==text)
input.val("");input.css("color",defaultColor);}
function insertMessage(){if(input.val().length==0||input.val()==text){input.val(text);input.css("color",color);}else
input.css("color",defaultColor);}
input.focus(clearMessage);input.blur(insertMessage);input.change(insertMessage);insertMessage();});};})(jQuery);function isDate(txtDate){var objDate;var mSeconds;if(txtDate.length!=10)return false;var day=txtDate.substring(3,5)-0;var month=txtDate.substring(0,2)-1;var year=txtDate.substring(6,10)-0;if(txtDate.substring(2,3)!='/')return false;if(txtDate.substring(5,6)!='/')return false;if(year<999||year>3000)return false;mSeconds=(new Date(year,month,day)).getTime();objDate=new Date();objDate.setTime(mSeconds);if(objDate.getFullYear()!=year)return false;if(objDate.getMonth()!=month)return false;if(objDate.getDate()!=day)return false;return true;}

function isNameWithSpace(Name)
{
  var regex = /^[a-zA-Z\s]+$/;
    if (regex.test(Name)) return true;
 else return false;       
    
}
function isName(Name)
{
  var regex = /^[a-zA-Z]+$/;
    if (regex.test(Name)) return true;
 else return false;       
    
}       
function isAddress(Name)
{
  var regex = /^[a-zA-Z0-9\s\#\.\-]+$/;
    if (regex.test(Name)) return true;
 else return false;       
    
}
function EncodeSearchText(strSearchText){
    var aryCharacters = ["%",".","#","&","*",":","<",">","?"," "];
    var aryEncodedCharacters = ["x25","x2E", "x23", "x26", "x2A", "x3A", "x3C", "x3E", "x3F","x20"]; 
    for(var index = 0;index<aryCharacters.length;index++)
    {
        var intIndexOfMatch = strSearchText.indexOf(aryCharacters[index]);
        while (intIndexOfMatch != -1){
            strSearchText = strSearchText.replace(aryCharacters[index], aryEncodedCharacters[index] )
            intIndexOfMatch = strSearchText.indexOf(aryCharacters[index]); 
        }
    }
   return strSearchText;
}
 
function slideSwitch() {
    var $active = $('#slider-home IMG.active');

    if ( $active.length == 0 ) $active = $('#slider-home IMG:last');

    // use this to pull the images in the order they appear in the markup
    var $next =  $active.next().length ? $active.next()
        : $('#slider-home IMG:first');

    // uncomment the 3 lines below to pull the images in random order
    
    // var $sibs  = $active.siblings();
    // var rndNum = Math.floor(Math.random() * $sibs.length );
    // var $next  = $( $sibs[ rndNum ] );


    $active.addClass('last-active');

    $next.css({opacity: 0.0})
        .addClass('active')
        .animate({opacity: 1.0}, 1000, function() {
            $active.removeClass('active last-active');
        });
}
        
//jquery-alerts/jquery.alerts.js
(function($) {
    $.alerts = { verticalOffset: -75, horizontalOffset: 0, repositionOnResize: true, overlayOpacity: .01, overlayColor: '#FFF', draggable: true, okButton: '&nbsp;OK&nbsp;', cancelButton: '&nbsp;Cancel&nbsp;', dialogClass: null, alert: function(message, title, callback) { if (title == null) title = 'Alert'; $.alerts._show(title, message, null, 'alert', function(result) { if (callback) callback(result); }); }, confirm: function(message, title, callback) { if (title == null) title = 'Confirm'; $.alerts._show(title, message, null, 'confirm', function(result) { if (callback) callback(result); }); }, prompt: function(message, value, title, callback) { if (title == null) title = 'Prompt'; $.alerts._show(title, message, value, 'prompt', function(result) { if (callback) callback(result); }); }, _show: function(title, msg, value, type, callback) {
    $.alerts._hide(); $.alerts._overlay('show'); $("BODY").append('<div id="popup_container">' + '<h1 id="popup_title"></h1>' + '<div id="popup_content">' + '<div id="popup_message"></div>' + '</div>' + '</div>'); if ($.alerts.dialogClass) $("#popup_container").addClass($.alerts.dialogClass); var pos = ($.browser.msie && parseInt($.browser.version) <= 6) ? 'absolute' : 'fixed'; $("#popup_container").css({ position: pos, zIndex: 99999, padding: 0, margin: 0 }); $("#popup_title").text(title); $("#popup_content").addClass(type); $("#popup_message").text(msg); $("#popup_message").html($("#popup_message").text().replace(/\n/g, '<br />')); $("#popup_container").css({ minWidth: $("#popup_container").outerWidth(), maxWidth: $("#popup_container").outerWidth() }); $.alerts._reposition(); $.alerts._maintainPosition(true); switch (type) { case 'alert': $("#popup_message").after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /></div>'); $("#popup_ok").click(function() { $.alerts._hide(); callback(true); }); $("#popup_ok").focus().keypress(function(e) { if (e.keyCode == 13 || e.keyCode == 27) $("#popup_ok").trigger('click'); }); break; case 'confirm': $("#popup_message").after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>'); $("#popup_ok").click(function() { $.alerts._hide(); if (callback) callback(true); }); $("#popup_cancel").click(function() { $.alerts._hide(); if (callback) callback(false); }); $("#popup_ok").focus(); $("#popup_ok, #popup_cancel").keypress(function(e) { if (e.keyCode == 13) $("#popup_ok").trigger('click'); if (e.keyCode == 27) $("#popup_cancel").trigger('click'); }); break; case 'prompt': $("#popup_message").append('<br /><input type="text" size="30" id="popup_prompt" />').after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>'); $("#popup_prompt").width($("#popup_message").width()); $("#popup_ok").click(function() { var val = $("#popup_prompt").val(); $.alerts._hide(); if (callback) callback(val); }); $("#popup_cancel").click(function() { $.alerts._hide(); if (callback) callback(null); }); $("#popup_prompt, #popup_ok, #popup_cancel").keypress(function(e) { if (e.keyCode == 13) $("#popup_ok").trigger('click'); if (e.keyCode == 27) $("#popup_cancel").trigger('click'); }); if (value) $("#popup_prompt").val(value); $("#popup_prompt").focus().select(); break; }
        if ($.alerts.draggable) { try { $("#popup_container").draggable({ handle: $("#popup_title") }); $("#popup_title").css({ cursor: 'move' }); } catch (e) { } } 
    }, _hide: function() { $("#popup_container").remove(); $.alerts._overlay('hide'); $.alerts._maintainPosition(false); }, _overlay: function(status) { switch (status) { case 'show': $.alerts._overlay('hide'); $("BODY").append('<div id="popup_overlay"></div>'); $("#popup_overlay").css({ position: 'absolute', zIndex: 99998, top: '0px', left: '0px', width: '100%', height: $(document).height(), background: $.alerts.overlayColor, opacity: $.alerts.overlayOpacity }); break; case 'hide': $("#popup_overlay").remove(); break; } }, _reposition: function() { var top = (($(window).height() / 2) - ($("#popup_container").outerHeight() / 2)) + $.alerts.verticalOffset; var left = (($(window).width() / 2) - ($("#popup_container").outerWidth() / 2)) + $.alerts.horizontalOffset; if (top < 0) top = 0; if (left < 0) left = 0; if ($.browser.msie && parseInt($.browser.version) <= 6) top = top + $(window).scrollTop(); $("#popup_container").css({ top: top + 'px', left: left + 'px' }); $("#popup_overlay").height($(document).height()); }, _maintainPosition: function(status) { if ($.alerts.repositionOnResize) { switch (status) { case true: $(window).bind('resize', $.alerts._reposition); break; case false: $(window).unbind('resize', $.alerts._reposition); break; } } } 
    }
    jAlert = function(message, title, callback) { $.alerts.alert(message, title, callback); }
    jConfirm = function(message, title, callback) { $.alerts.confirm(message, title, callback); }; jPrompt = function(message, value, title, callback) { $.alerts.prompt(message, value, title, callback); };
})(jQuery);


//mask-input.js
(function($){var pasteEventName=($.browser.msie?'paste':'input')+".mask";var iPhone=(window.orientation!=undefined);$.mask={definitions:{'9':"[0-9]",'a':"[A-Za-z]",'*':"[A-Za-z0-9]"}};$.fn.extend({caret:function(begin,end){if(this.length==0)return;if(typeof begin=='number'){end=(typeof end=='number')?end:begin;return this.each(function(){if(this.setSelectionRange){this.focus();this.setSelectionRange(begin,end);}else if(this.createTextRange){var range=this.createTextRange();range.collapse(true);range.moveEnd('character',end);range.moveStart('character',begin);range.select();}});}else{if(this[0].setSelectionRange){begin=this[0].selectionStart;end=this[0].selectionEnd;}else if(document.selection&&document.selection.createRange){var range=document.selection.createRange();begin=0-range.duplicate().moveStart('character',-100000);end=begin+range.text.length;}
return{begin:begin,end:end};}},unmask:function(){return this.trigger("unmask");},mask:function(mask,settings){if(!mask&&this.length>0){var input=$(this[0]);var tests=input.data("tests");return $.map(input.data("buffer"),function(c,i){return tests[i]?c:null;}).join('');}
settings=$.extend({placeholder:" ",completed:null},settings);var defs=$.mask.definitions;var tests=[];var partialPosition=mask.length;var firstNonMaskPos=null;var len=mask.length;$.each(mask.split(""),function(i,c){if(c=='?'){len--;partialPosition=i;}else if(defs[c]){tests.push(new RegExp(defs[c]));if(firstNonMaskPos==null)
firstNonMaskPos=tests.length-1;}else{tests.push(null);}});return this.each(function(){var input=$(this);var buffer=$.map(mask.split(""),function(c,i){if(c!='?')return defs[c]?settings.placeholder:c});var ignore=false;var focusText=input.val();input.data("buffer",buffer).data("tests",tests);function seekNext(pos){while(++pos<=len&&!tests[pos]);return pos;};function shiftL(pos){while(!tests[pos]&&--pos>=0);for(var i=pos;i<len;i++){if(tests[i]){buffer[i]=settings.placeholder;var j=seekNext(i);if(j<len&&tests[i].test(buffer[j])){buffer[i]=buffer[j];}else
break;}}
writeBuffer();input.caret(Math.max(firstNonMaskPos,pos));};function shiftR(pos){for(var i=pos,c=settings.placeholder;i<len;i++){if(tests[i]){var j=seekNext(i);var t=buffer[i];buffer[i]=c;if(j<len&&tests[j].test(t))
c=t;else
break;}}};function keydownEvent(e){var pos=$(this).caret();var k=e.keyCode;ignore=(k<16||(k>16&&k<32)||(k>32&&k<41));if((pos.begin-pos.end)!=0&&(!ignore||k==8||k==46))
clearBuffer(pos.begin,pos.end);if(k==8||k==46||(iPhone&&k==127)){shiftL(pos.begin+(k==46?0:-1));return false;}else if(k==27){input.val(focusText);input.caret(0,checkVal());return false;}};function keypressEvent(e){if(ignore){ignore=false;return(e.keyCode==8)?false:null;}
e=e||window.event;var k=e.charCode||e.keyCode||e.which;var pos=$(this).caret();if(e.ctrlKey||e.altKey||e.metaKey){return true;}else if((k>=32&&k<=125)||k>186){var p=seekNext(pos.begin-1);if(p<len){var c=String.fromCharCode(k);if(tests[p].test(c)){shiftR(p);buffer[p]=c;writeBuffer();var next=seekNext(p);$(this).caret(next);if(settings.completed&&next==len)
settings.completed.call(input);}}}
return false;};function clearBuffer(start,end){for(var i=start;i<end&&i<len;i++){if(tests[i])
buffer[i]=settings.placeholder;}};function writeBuffer(){return input.val(buffer.join('')).val();};function checkVal(allow){var test=input.val();var lastMatch=-1;for(var i=0,pos=0;i<len;i++){if(tests[i]){buffer[i]=settings.placeholder;while(pos++<test.length){var c=test.charAt(pos-1);if(tests[i].test(c)){buffer[i]=c;lastMatch=i;break;}}
if(pos>test.length)
break;}else if(buffer[i]==test[pos]&&i!=partialPosition){pos++;lastMatch=i;}}
if(!allow&&lastMatch+1<partialPosition){input.val("");clearBuffer(0,len);}else if(allow||lastMatch+1>=partialPosition){writeBuffer();if(!allow)input.val(input.val().substring(0,lastMatch+1));}
return(partialPosition?i:firstNonMaskPos);};if(!input.attr("readonly"))
input.one("unmask",function(){input.unbind(".mask").removeData("buffer").removeData("tests");}).bind("focus.mask",function(){focusText=input.val();var pos=checkVal();writeBuffer();setTimeout(function(){if(pos==mask.length)
input.caret(0,pos);else
input.caret(pos);},0);}).bind("blur.mask",function(){checkVal();if(input.val()!=focusText)
    input.change();
}).bind("keydown.mask", keydownEvent).bind("keypress.mask", keypressEvent).bind(pasteEventName, function() { setTimeout(function() { input.caret(checkVal(true)); }, 0); }); checkVal();
});
} 
});
})(jQuery);