function GetString(txtCode)
{
    document.getElementById(txtCode).value=RamdomString(10);
    return false;
}
function RamdomString(intLen)
{
	var strRet = "";
	var iCntr  = 0;
	var rndNo  = 0;
	var arrCharacters = new Array("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
	for (iCntr = 0; iCntr < intLen; iCntr++)
	{
		rndNo = Math.floor((61 - 1 + 1) * Math.random() + 1)
		strRet = strRet + arrCharacters[rndNo];
	}
	return strRet;
	//alert(strRet)
}
// Float Message Call

function msgboxTop(msg,type)
{
    var color = 'white';
    
    if (type == 'success')
        color = 'white';
    if (type == 'error')
        color = 'red';
    
    $.floatingMessage(msg,{
        show : "blind",
        align:"right",  
        verticalAlign:"top",
        time:5000,
        color:color
    });
}


function myAjax(strUrl, ctrlData, updateCtrl)
{
    $.ajax({
        url: strUrl,
        type: 'POST',
        data: ctrlData,
        success: function(resp) { if (updateCtrl != null) { $('#' + updateCtrl).replaceWith(resp); }}
    });
}

function myAjaxHtml(strUrl, ctrlData, updateCtrl)
{
    $.ajax({
        url: strUrl,
        type: 'POST',
        data: ctrlData,
        success: function(resp) { if (updateCtrl != null) { $('#' + updateCtrl).html(resp); }}
    });
}

function AjaxDataPostNoLoad(ctrldata,strUrl, ctrlUpdate, strType) {    
    $.ajax({
        url: strUrl,
        type: strType,
        data: ctrldata,
        success: function(resp) { $('#' + ctrlUpdate).html(resp);}
    });
}
function AjaxRequest(ctrlLoad, ctrlUpdate, strUrl, strType) {
    $('#' + ctrlLoad).show();
    $.ajax({
        url: strUrl,
        type: strType,
        success: function(resp) { $('#' + ctrlUpdate).html(resp); $('#' + ctrlLoad).hide(); }
    });
}
function AjaxFormPost(ctrlform, ctrlLoad, strUrl, ctrlUpdate) {   
    $('#' + ctrlLoad).show();
    $.ajax({
        url: strUrl,
        type: 'POST',
        data: $('#' + ctrlform).serialize()+'&call=ajax',
        success: function(resp) { $('#' + ctrlUpdate).html(resp); $('#' + ctrlLoad).hide(); }
    });
}
function AjaxDataPost(ctrldata, ctrlLoad,strUrl, ctrlUpdate,  strType) {
    $('#' + ctrlLoad).show();
    $.ajax({
        url: strUrl,
        type: strType,
        data: ctrldata,
        success: function(resp) { $('#' + ctrlUpdate).html(resp); $('#' + ctrlLoad).hide(); }
    });
}

function DisableDropdownItem(drpCtrl, drpValue) {
    for (i = 0; i < drpCtrl.length; i++) {
        if (parseInt(drpCtrl.options[i].value) == drpValue) {
            drpCtrl.options[i].disabled = "disabled";
        }
    }
}

function ValidateDate(day, Month, year) {
    var monthfield = Month;
    var dayfield = day;
    var yearfield = year;
    var dayobj = new Date(yearfield, monthfield - 1, dayfield)
    if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
        return false;
    return true;
}

function ChngMemCountwiseState(CtrlCountry, CtrlUs, CtrlAus, CtrlCA, CtrlInd, TxtOther, drpAllState) {
    CtrlUs.style.display = 'none';
    CtrlAus.style.display = 'none';
    CtrlCA.style.display = 'none';
    CtrlInd.style.display = 'none';
    TxtOther.style.display = 'none';
    drpAllState.style.display = 'none';
    TxtOther.value = '';
    switch (parseInt(CtrlCountry.value)) {
        case 226:
            CtrlUs.style.display = '';
            break;
        case 14:
            CtrlAus.style.display = '';
            break;
        case 37:
            CtrlCA.style.display = '';
            break;
        case 103:
            CtrlInd.style.display = '';
            break;
        case -1:
            drpAllState.style.display = '';
            break;
        default:

            TxtOther.style.display = '';
            break;
    }

}

function ChangeCountrywiseState(CtrlCountry, CtrlUs, CtrlAus, CtrlCA, CtrlInd, TxtOther, LblTitle) {
    $('#' + CtrlUs).hide();
    $('#' + CtrlAus).hide();
    $('#' + CtrlCA).hide();
    $('#' + CtrlInd).hide();
    $('#' + TxtOther).hide();
    $('#' + TxtOther).val('');
    $('#' + LblTitle).html('State/provinece');
    switch (parseInt($('#' + CtrlCountry).val())) {
        case 226:
            $('#' + CtrlUs).show();
            break;
        case 14:
            $('#' + CtrlAus).show();
            break;
        case 37:
            $('#' + CtrlCA).show();
            break;
        case 103:
            $('#' + CtrlInd).show();
            break;
        default:
            $('#' + TxtOther).show();
            $('#' + LblTitle).html('Other State');
            break;
    }
}
function trim(strComp) {
    return jQuery.trim(strComp);
}

function ValidateControl(formObject, fieldDescription, lblObject) {
    var tempFormValue;
    var strError = "";
    var iFocus = -1;
    var ErrCount = 1;

    for (var i = 0; i < ValidateControl.arguments.length; i = i + 3) {
        tempFormValue = trim($('#' + ValidateControl.arguments[i]).val());
        if (tempFormValue.length == 0) {
            if (strError != '')
                strError = strError + "<br/> - " + ValidateControl.arguments[i + 1];
            else
                strError = " - " + ValidateControl.arguments[i + 1];
            ErrCount = ErrCount + 1;
            $('#' + ValidateControl.arguments[i + 2]).attr('class', 'alert');
            if (iFocus == -1)
                iFocus = i;
        }
        else {
            $('#' + ValidateControl.arguments[i + 2]).attr('class', '');
        }
    }

    if (strError.length != 0) {
        return strError;
    }
    else
        return strError;
}
 function isEmailAddress(emField) { //reference to email field passed as argument
 
        var temField = emField.split('@');
        if (temField.length != 2) {
            return false;
        }
        else {
            if (temField[0].length == 0) {
                return false;
            }
            else {
                var lstIndex = temField[1].lastIndexOf('.');
                if (lstIndex == -1) {
                    return false;
                }
                else {
                    if (temField[1].substring(lstIndex + 1).length == 2 || temField[1].substring(lstIndex + 1).length == 3) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        return true;
    } 

function ShowHideCtrl(ShowCtrl, HideCtrl) {
    $('#' + HideCtrl).slideUp('slow');
    $('#' + ShowCtrl).slideDown('slow');
}

function CharacterCount(obj, total) {
    if (total == null) total = 500;
    var len = obj.value.length;
    var newdiv;

    if (document.getElementById("note" + obj.id) == null) {
        newdiv = document.createElement('div');
        newdiv.id = "note" + obj.id;
        newdiv.className = "CharacterCount";
        obj.parentNode.appendChild(newdiv);
    }
    else {
        newdiv = document.getElementById("note" + obj.id);
    }

    if (len == 0) {
        obj.parentNode.removeChild(newdiv);
    }

    if (len >= total) {
        var temp = obj.value.substring(0, total)
        obj.value = temp;
        newdiv.innerHTML = "You have reached maximum characters limit of <strong>" + total + "</strong>."
    }
    else {
        newdiv.innerHTML = "Your maximum characters limit is: <strong>" + total + "</strong>.<br> Current character count: <strong>" + len + "</strong>.";
    }
}
function TrimStart(str, strchar) {
    if (str.indexOf(strchar) == 0) {
        str = str.replace(strchar, '');
    }
    return str;
}
function PasswordChk(ctrl1, ctrl2,errMsg) {
    var digit = 0, chr = 0;    
    if ($('#' + ctrl1).val() == $('#' + ctrl2).val()) {
        if ($('#' + ctrl1).val().length < 6) {
            return 'Password must be at least 6 characters long and should contain at least one number or a letter.';
        }
        else {
            var tmp = $('#' + ctrl1).val();
            for (var i = 0; i < tmp.length; i++) {
                if (tmp.charCodeAt(i) >= 48 && tmp.charCodeAt(i) <= 57) {
                    digit++;
                }
                else if ((tmp.charCodeAt(i) >= 65 && tmp.charCodeAt(i) <= 90) || (tmp.charCodeAt(i) >= 97 && tmp.charCodeAt(i) <= 122)) {
                    chr++;
                }
            }
            if (!(digit > 0 && chr > 0)) {
                return 'Password must be at least 6 characters long and should contain at least one number or a letter.';
            }
        }
    }
    else {
        return 'Password and confirm passwords don’t match. Please check both passwords and try again';
    }
    return '';
}
function DisplayMessage(ctrl, clsName, Msg) {
    $('#' + ctrl).show();
    $('#' + ctrl).attr('class', clsName + ' clear');
    $('#' + ctrl).html(Msg);
    setTimeout('$(\'#' + ctrl + '\').hide();', 10000);
}
function DisplMsg(Ctrl, ErrMsg, Msgclass) {
    $('#' + Ctrl).show();
    $('#' + Ctrl).html(ErrMsg);
    $('#' + Ctrl).attr('class', Msgclass);
}

function msgbox(ErrMsg, Title) {
    jAlert(ErrMsg, Title);
}

function DisplMsgFront(Ctrl, ErrMsg, Msgclass) {
    $('#' + Ctrl).show();
    $('#' + Ctrl).css('color', '#ff0000');
    $('#' + Ctrl).html(ErrMsg);
    //$('#' + Ctrl).removeClass('msgerror').removeClass('msgsuccess');
    $('#' + Ctrl).attr('class','');
    $('#' + Ctrl).addClass(Msgclass);
    fourPop(Ctrl);
}

//--to show the error message of required valiation--//
function ValidateControlAdmin(formObject, fieldDescription, lblObject) {
    var tempFormValue;
    var strError = "";
    var iFocus = -1;
    var ErrCount = 1;
    for (var i = 0; i < ValidateControlAdmin.arguments.length; i = i + 3) {
        if (typeof (ValidateControlAdmin.arguments[i]) == 'undefined') {
            return 'Error';
        }
        tempFormValue = jQuery.trim(ValidateControlAdmin.arguments[i].val());

        if (tempFormValue.length < 15)
        { deleteLoop = tempFormValue.length }
        else
        { deleteLoop = 15 }

        for (var j = 0; j < deleteLoop; j++) {
            tempFormValue = tempFormValue.replace(/ /, "");
        }

        if (tempFormValue.length == 0) {
            if (strError != '')
                strError = strError + "<br/> - " + ValidateControlAdmin.arguments[i + 1];
            else
                strError = " - " + ValidateControlAdmin.arguments[i + 1];
            ErrCount = ErrCount + 1;
            ValidateControlAdmin.arguments[i + 2].addClass('alert');
            if (iFocus == -1)
                iFocus = i;
        }
        else {
            ValidateControlAdmin.arguments[i + 2].addClass('');
        }
    }
    if (strError.length != 0) {
        return strError;
    }
    else
        return strError;
}

function DirValCtrl(concatStr) {
    
    if (concatStr == undefined)
        concatStr = '<br/> - ';
    var lbl;
    var inctrl;
    var ErrMsg = '';
    var IsFirst = 0;
    $('span[class="red"]').each(function() {
        lbl = $(this).prev();
        if ($(this).prev().length > 0) {
            inctrl = $(lbl).attr('for');
            
            if (inctrl.length > 0 && $(lbl).attr('for').length > 0) {
                switch ($('#' + inctrl).attr('type')) {
                
                    case "textarea":
                    case "text":
                    case "file":
                    case "password":
                        if ($('#' + inctrl).val() == '' && $('#' + inctrl).is(':visible') == true) {
                            $('#' + inctrl).addClass('valerror');
                            ErrMsg = ErrMsg + concatStr + stripHTML($(lbl).html());
                            if (IsFirst == 0)
                                $('#' + inctrl).focus();
                            IsFirst = 1;
                        }
                        else
                            $('#' + inctrl).removeClass('valerror');
                        $('#' + inctrl).attr('style', '');
                        break;
                    case "select":
                        if ($('#' + inctrl).val() == '')
                            ErrMsg = ErrMsg + concatStr + stripHTML($(lbl).html());
                        else
                            $('#' + inctrl).removeClass('valerror');
                        break;
                }
            }
        }
    });
    return trim(ErrMsg);
}

function isValidURL(url) {
    var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;
    if (RegExp.test(url)) {
         if(url.indexOf('http://') != -1 || url.indexOf('https://') != -1)
        {
            return true;
        }
        else
            return false;
    } else {
        return false;
    }
}

function findPosX(obj) {
    var curleft = 0;
    if (obj.offsetParent)
        while (1) {
        curleft += obj.offsetLeft;
        if (!obj.offsetParent)
            break;
        obj = obj.offsetParent;
    }
    else if (obj.x)
        curleft += obj.x;
    return curleft;
}

function findPosY(obj) {
    var curtop = 0;
    if (obj.offsetParent)
        while (1) {
        curtop += obj.offsetTop;
        if (!obj.offsetParent)
            break;
        obj = obj.offsetParent;
    }
    else if (obj.y)
        curtop += obj.y;
    return curtop;
}
function EncodeText(UrlText) {
    UrlText = UrlText.replace(/%/g, '-per-');
    UrlText = UrlText.replace(/&/g, '-and-');
    UrlText = UrlText.replace(/\?/g, '-que-');
    UrlText = replaceCharacters(UrlText, '/', '-sla-');
    return UrlText;
}
function replaceCharacters(MainStr, ScanChar, RepChar) {
    var newString = MainStr.split(ScanChar);
    newString = newString.join(RepChar);
    return newString;
}
function CreateTag(TagName, TagProp, TagValue) {
    var ctrl = document.createElement(TagName);
    for (var i = 0; i < CreateTag.arguments.length - 1; i++) {
        if (document.all) {
            if (CreateTag.arguments[i + 1] == 'style') {
                var tmpStyle = CreateTag.arguments[i + 2].split(':');
                switch (tmpStyle[0]) {
                    case "display":
                        ctrl.style.display = tmpStyle[1];
                        break;
                }
            }
            else if (CreateTag.arguments[i + 1] == 'class')
                ctrl.setAttribute('className', CreateTag.arguments[i + 2]);
            else
                ctrl.setAttribute(CreateTag.arguments[i + 1], CreateTag.arguments[i + 2]);
        }
        else {
            ctrl.setAttribute(CreateTag.arguments[i + 1], CreateTag.arguments[i + 2]);
        }
        i = i + 1;
    }
    return ctrl;
}

function isImage(obj) {
    if (obj.value.length > 0) {
        if (obj.value.length > 4) {
            var tmpExt = obj.value.split('.');
            var ext = tmpExt[tmpExt.length - 1];
            if (ext.toLowerCase() == 'jpg' || ext.toLowerCase() == 'bmp' || ext.toLowerCase() == 'tif' || ext.toLowerCase() == 'tiff' || ext.toLowerCase() == 'jpeg' || ext.toLowerCase() == 'gif' || ext.toLowerCase() == 'png') {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    return false;
}

function NumericOnly(ctrl) {
    var tmpValue = '';
    var strValue = ctrl.value;
    for (var i = 0; i < strValue.length; i++) {
        if (strValue[i].charCodeAt(0) >= 48 && strValue[i].charCodeAt(0) <= 57) {
            tmpValue += strValue[i];
        }
    }
    ctrl.value = tmpValue;
}
function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

         return true;
     }
     function isNumberKey2(evt) {
         var charCode = evt.which;
         if (charCode > 31 && (charCode < 48 || charCode > 57))
             return false;

         return true;
     }
function stripHTML(data) {
    var re = /<\S[^><]*>/g;
    return trim(data.replace(re, ""));
}


 function ChangeCity(pageurl,ddlState,objddlCity,objimgLoading){
    ddlState.change(function(){
        objimgLoading.show(); var Count = 0;
         while (document.getElementById(objddlCity.attr('id')).length > 0) {
            document.getElementById(objddlCity.attr('id')).options[0] = null;
        } 
        var myval = (jQuery("#"+ ddlState.attr('id') +" > option:selected").attr("value"));
        if (myval != 0){
            jQuery.getJSON(pageurl + '?StateID=' + myval, function(options) {
                jQuery.each(options, function() {
                    objddlState.append(jQuery("<option></option>").val(this['ID']).html(this['Name']));
                    Count++;
                });
                if (Count == 1){
                    objddlCity.css("display", "none"); 
//                    if(objrfvtxtState != null)
//                        ValidatorEnable(document.getElementById(objrfvtxtState.attr('id')), true);
//                    if(objrfvState != null)
//                        ValidatorEnable(document.getElementById(objrfvState.attr('id')), false);
                }
                else {
                    objddlCity.css("display", "");
//                    if(objrfvtxtState != null)
//                        ValidatorEnable(document.getElementById(objrfvtxtState.attr('id')), false);
//                    if(objrfvState != null)
//                        ValidatorEnable(document.getElementById(objrfvState.attr('id')), true);
                }
            });
        }
        objimgLoading.hide();
    });
} 

function DirValCtrlFront()
{
 if (concatStr == undefined)
        concatStr = '<br/> - ';
    var lbl;
    var inctrl;
    var ErrMsg='';
    var IsFirst=0;
   $('span[class="red"]').each(function(){
        lbl=$(this).siblings();                
        if($(this).siblings().length>0)
        {                          
            inctrl=$(lbl).attr('for');             
            if(inctrl.length >0 && $(lbl).attr('for').length>0)  
            {                          
                switch($('#' + inctrl).attr('type'))
                {
                    case "textarea":
                    case "text":
                    case "file":
                    case "password":      
                                          
                            if(jQuery.trim($('#' + inctrl).val())=='' && $('#' + inctrl).is(':visible')==true)
                            {
                                ErrMsg=ErrMsg + concatStr + $(lbl).html();                                
                                if(IsFirst==0)
                                    $('#' + inctrl).focus();
                                IsFirst=1;
                            }
                            else
                                $('#' + inctrl).attr('style','');
                            break;
                    case "select":
                        if($('#' + inctrl).val()=='')
                            ErrMsg=ErrMsg + '<br/> - ' + $(lbl).html();
                        break;
                    case "checkbox":
                        if (!$('#' + inctrl).attr('checked'))
                            ErrMsg = ErrMsg + '<br/> - ' + $(lbl).html();
                        break;
                }                
            }
        }
   });   
   return ErrMsg;   
} 


// Water Mark Efect

(function($) {
	var map=new Array();
	$.Watermark = {
		ShowAll:function(){
			for (var i=0;i<map.length;i++){
				if(map[i].obj.val()==""){
					map[i].obj.val(map[i].text);					
					map[i].obj.css("color",map[i].WatermarkColor);
				}else{
				    map[i].obj.css("color",map[i].DefaultColor);
				}
			}
		},
		HideAll:function(){
			for (var i=0;i<map.length;i++){
				if(map[i].obj.val()==map[i].text)
					map[i].obj.val("");					
			}
		}
	}
	
	$.fn.Watermark = function(text,color) {
		if(!color)
			color="#aaa";
		return this.each(
			function(){		
				var input=$(this);
				var defaultColor=input.css("color");
				map[map.length]={text:text,obj:input,DefaultColor:defaultColor,WatermarkColor:color};
				function clearMessage(){
					if(input.val()==text)
						input.val("");
					input.css("color",defaultColor);
				}

				function insertMessage(){
					if(input.val().length==0 || input.val()==text){
						input.val(text);
						input.css("color",color);	
					}else
						input.css("color",defaultColor);				
				}

				input.focus(clearMessage);
				input.blur(insertMessage);								
				input.change(insertMessage);
				
				insertMessage();
			}
		);
	};
})(jQuery);

// end water mark efect


// check date JavaScript function
// if date is valid then function returns true, otherwise returns false
function isDate(txtDate){
  var objDate;  // date object initialized from the txtDate string
  var mSeconds; // milliseconds from txtDate

	// date length should be 10 characters - no more, no less
  if (txtDate.length != 10) return false;

	// extract day, month and year from the txtDate string
	// expected format is mm/dd/yyyy
	// subtraction will cast variables to integer implicitly
  var day   = txtDate.substring(3,5)  - 0;
  var month = txtDate.substring(0,2)  - 1; // because months in JS start with 0
  var year  = txtDate.substring(6,10) - 0;

	// third and sixth character should be /
	if (txtDate.substring(2,3) != '/') return false;
	if (txtDate.substring(5,6) != '/') return false;

  // test year range
  if (year < 999 || year > 3000) return false;

  // convert txtDate to the milliseconds
  mSeconds = (new Date(year, month, day)).getTime();

  // initialize Date() object from calculated milliseconds
  objDate = new Date();
  objDate.setTime(mSeconds);

  // compare input parameter date and created Date() object
  // if difference exists then date isn't valid
  if (objDate.getFullYear() != year)  return false;
  if (objDate.getMonth()    != month) return false;
  if (objDate.getDate()     != day)   return false;

	// otherwise return true
  return true;
}
/* validate textbox values */
var objUsername = ". 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz";
var objNumber = ".0123456789";
var objMoney = ".,0123456789";
var objWholeNumber = "0123456789";
var objPhone = "-()0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz_";

function isRule(oComp, sRule, nLength, fdecimal){
	if(fdecimal == "" || typeof(fdecimal) == "undefined"){ fdecimal = false; }

	//If the object is not specified return false
	if (typeof(oComp) == 'undefined' || oComp == null || oComp == ''){
		alert('Error: Input object not specified.');
		return false;
	}
	//If neither rule nor max length is specified, return false
	else if (typeof(sRule) == 'undefined' && typeof(nLength) == 'undefined'){
		alert('Error: No rule/maximum length for input object specified.');
		return false;
	}

	var noErrorFlg = true;

	//If object is specified and either of rule is specified,
	if(typeof(sRule) != 'undefined' && sRule != null){
		var temp;
		sRule = sRule + "";
		var discardChars = false;
		if(sRule.length > 0 && sRule.charAt(0) == "~"){ sRule = sRule.substring(1); discardChars = true; }

		if(typeof(oComp) == "undefined" || typeof(sRule) == "undefined"){ return false; }

		for (var i = 0;i < oComp.value.length;i++){
			temp = oComp.value.charAt(i);

			if((!discardChars && sRule.indexOf(temp) == -1) || (discardChars && sRule.indexOf(temp) >= 0)){
				//alert("Field disobeys entry rule.  Following are the valid characters:\n" + sRule);
				//alert("Invalid Character!");
				oComp.value = oComp.value.substring(0,i);// + (oComp.value.length > i ? oComp.value.substring(i+1):"");
				noErrorFlg = false;
				break;
			}
		}
	}
	
	if(nLength){
		if(fdecimal){
			nLength -= fdecimal;
			var dp = oComp.value.indexOf(".");
			var p1;
			var p2 = "";
			if(dp >= 0){ p1 = oComp.value.substring(0,dp); p2 = oComp.value.substring(dp+1); }
			else{ p1 = oComp.value; }
			
			if(p1.length > nLength){
				oComp.value = oComp.value.substring(0,nLength);
				return noErrorFlg;
			}
			for(var i = 0; i < p2.length;i++){
				var ch = p2.charAt(i);
				if(ch < '0' || ch > '9'){ oComp.value = p1 + "." + p2.substring(0,i); return noErrorFlg; }
			}
			if(p2.length > fdecimal){ oComp.value = p1 + "." + p2.substring(0,fdecimal); }
		} else if(oComp.value.length > nLength){
			oComp.value = oComp.value.substring(0,nLength);
		}
	}
	return noErrorFlg;
}
function IsValidZip(ZipValue){
    var regex = /^[\d]{5}$/;
    if (regex.test(ZipValue)) return true;
    else return false;
}  
   function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
            return pattern.test(emailAddress);
        }