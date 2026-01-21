// uncompressed.js 

; (function($) {
    var msOldDiv = ""; var dd = function(element, options) {
        var sElement = element; var $this = this; var options = $.extend({ height: 120, visibleRows: 7, rowHeight: 23, showIcon: true, zIndex: 9999, mainCSS: 'dd', useSprite: false, onInit: '', style: '' }, options); this.ddProp = new Object(); var selectedValue = ""; var actionSettings = {}; actionSettings.insideWindow = true; actionSettings.keyboardAction = false; actionSettings.currentKey = null; var ddList = false; var config = { postElementHolder: '_msddHolder', postID: '_msdd', postTitleID: '_title', postTitleTextID: '_titletext', postChildID: '_child', postAID: '_msa', postOPTAID: '_msopta', postInputID: '_msinput', postArrowID: '_arrow', postInputhidden: '_inp' }; var styles = { dd: options.mainCSS, ddTitle: 'ddTitle', arrow: 'arrow', ddChild: 'ddChild', ddTitleText: 'ddTitleText', disabled: .30, ddOutOfVision: 'ddOutOfVision' }; var attributes = { actions: "focus,blur,change,click,dblclick,mousedown,mouseup,mouseover,mousemove,mouseout,keypress,keydown,keyup", prop: "size,multiple,disabled,tabindex" }; this.onActions = new Object(); var elementid = $(sElement).attr("id"); var inlineCSS = $(sElement).attr("style"); options.style += (inlineCSS == undefined) ? "" : inlineCSS; var allOptions = $(sElement).children(); ddList = ($(sElement).attr("size") > 0 || $(sElement).attr("multiple") == true) ? true : false; if (ddList) { options.visibleRows = $(sElement).attr("size"); }; var a_array = {}; var getPostID = function(id) { return elementid + config[id]; }; var getOptionsProperties = function(option) { var currentOption = option; var styles = $(currentOption).attr("style"); return styles; }; var matchIndex = function(index) { var selectedIndex = $("#" + elementid + " option:selected"); if (selectedIndex.length > 1) { for (var i = 0; i < selectedIndex.length; i++) { if (index == selectedIndex[i].index) { return true; }; }; } else if (selectedIndex.length == 1) { if (selectedIndex[0].index == index) { return true; }; }; return false; }; var createA = function(currentOptOption, current, currentopt, tp) { var aTag = ""; var aidoptfix = (tp == "opt") ? getPostID("postOPTAID") : getPostID("postAID"); var aid = (tp == "opt") ? aidoptfix + "_" + (current) + "_" + (currentopt) : aidoptfix + "_" + (current); var arrow = ""; var clsName = ""; if (options.useSprite != false) { clsName = ' ' + options.useSprite + ' ' + currentOptOption.className; } else { arrow = $(currentOptOption).attr("title"); arrow = (arrow.length == 0) ? "" : '<img src="' + arrow + '" align="absmiddle" /> '; }; var sText = $(currentOptOption).text(); var sValue = $(currentOptOption).val(); var sEnabledClass = ($(currentOptOption).attr("disabled") == true) ? "disabled" : "enabled"; a_array[aid] = { html: arrow + sText, value: sValue, text: sText, index: currentOptOption.index, id: aid }; var innerStyle = getOptionsProperties(currentOptOption); if (matchIndex(currentOptOption.index) == true) { aTag += '<a href="javascript:void(0);" class="selected ' + sEnabledClass + clsName + '"'; } else { aTag += '<a  href="javascript:void(0);" class="' + sEnabledClass + clsName + '"'; }; if (innerStyle !== false && innerStyle !== undefined) { aTag += " style='" + innerStyle + "'"; }; aTag += ' id="' + aid + '">'; aTag += arrow + '<span class="' + styles.ddTitleText + '">' + sText + '</span></a>'; return aTag; }; var createATags = function() { var childnodes = allOptions; if (childnodes.length == 0) return ""; var aTag = ""; var aidfix = getPostID("postAID"); var aidoptfix = getPostID("postOPTAID"); childnodes.each(function(current) { var currentOption = childnodes[current]; if (currentOption.nodeName == "OPTGROUP") { aTag += "<div class='opta'>"; aTag += "<span style='font-weight:bold;font-style:italic; clear:both;'>" + $(currentOption).attr("label") + "</span>"; var optChild = $(currentOption).children(); optChild.each(function(currentopt) { var currentOptOption = optChild[currentopt]; aTag += createA(currentOptOption, current, currentopt, "opt"); }); aTag += "</div>"; } else { aTag += createA(currentOption, current, "", ""); }; }); return aTag; }; var createChildDiv = function() {
            var id = getPostID("postID"); var childid = getPostID("postChildID"); var sStyle = options.style; sDiv = ""; sDiv += '<div id="' + childid + '" class="' + styles.ddChild + '"'; if (!ddList) { sDiv += (sStyle != "") ? ' style="' + sStyle + '"' : ''; } else { sDiv += (sStyle != "") ? ' style="border-top:1px solid #c3c3c3;display:block;position:relative;' + sStyle + '"' : ''; }
            sDiv += '>'; return sDiv;
        }; var createTitleDiv = function() { var titleid = getPostID("postTitleID"); var arrowid = getPostID("postArrowID"); var titletextid = getPostID("postTitleTextID"); var inputhidden = getPostID("postInputhidden"); var sText = ""; var arrow = ""; if (document.getElementById(elementid).options.length > 0) { sText = $("#" + elementid + " option:selected").text(); arrow = $("#" + elementid + " option:selected").attr("title"); }; arrow = (arrow.length == 0 || arrow == undefined || options.showIcon == false || options.useSprite != false) ? "" : '<img src="' + arrow + '" align="absmiddle" /> '; var sDiv = '<div id="' + titleid + '" class="' + styles.ddTitle + '"'; sDiv += '>'; sDiv += '<span id="' + arrowid + '" class="' + styles.arrow + '"></span><span class="' + styles.ddTitleText + '" id="' + titletextid + '">' + arrow + '<span class="' + styles.ddTitleText + '">' + sText + '</span></span></div>'; return sDiv; }; var applyEventsOnA = function() { var childid = getPostID("postChildID"); $("#" + childid + " a.enabled").bind("click", function(event) { event.preventDefault(); manageSelection(this); if (!ddList) { $("#" + childid).unbind("mouseover"); setInsideWindow(false); var sText = (options.showIcon == false) ? $(this).text() : $(this).html(); setTitleText(sText); $this.close(); }; setValue(); }); }; var createDropDown = function() { var changeInsertionPoint = false; var id = getPostID("postID"); var titleid = getPostID("postTitleID"); var titletextid = getPostID("postTitleTextID"); var childid = getPostID("postChildID"); var arrowid = getPostID("postArrowID"); var iWidth = $("#" + elementid).width(); iWidth = iWidth + 2; var sStyle = options.style; if ($("#" + id).length > 0) { $("#" + id).remove(); changeInsertionPoint = true; }; var sDiv = '<div id="' + id + '" class="' + styles.dd + '"'; sDiv += (sStyle != "") ? ' style="' + sStyle + '"' : ''; sDiv += '>'; sDiv += createTitleDiv(); sDiv += createChildDiv(); sDiv += createATags(); sDiv += "</div>"; sDiv += "</div>"; if (changeInsertionPoint == true) { var sid = getPostID("postElementHolder"); $("#" + sid).after(sDiv); } else { $("#" + elementid).after(sDiv); }; if (ddList) { var titleid = getPostID("postTitleID"); $("#" + titleid).hide(); }; $("#" + id).css("width", iWidth + "px"); $("#" + childid).css("width", (iWidth - 0) + "px"); if (allOptions.length > options.visibleRows) { var margin = parseInt($("#" + childid + " a:first").css("padding-bottom")) + parseInt($("#" + childid + " a:first").css("padding-top")); var iHeight = ((options.rowHeight) * options.visibleRows) - margin; $("#" + childid).css("height", iHeight + "px"); } else if (ddList) { var iHeight = $("#" + elementid).height(); $("#" + childid).css("height", iHeight + "px"); }; if (changeInsertionPoint == false) { setOutOfVision(); addRefreshMethods(elementid); }; if ($("#" + elementid).attr("disabled") == true) { $("#" + id).css("opacity", styles.disabled); }; applyEvents(); $("#" + titleid).bind("mouseover", function(event) { hightlightArrow(1); }); $("#" + titleid).bind("mouseout", function(event) { hightlightArrow(0); }); applyEventsOnA(); $("#" + childid + " a.disabled").css("opacity", styles.disabled); if (ddList) { $("#" + childid).bind("mouseover", function(event) { if (!actionSettings.keyboardAction) { actionSettings.keyboardAction = true; $(document).bind("keydown", function(event) { var keyCode = event.keyCode; actionSettings.currentKey = keyCode; if (keyCode == 39 || keyCode == 40) { event.preventDefault(); event.stopPropagation(); next(); setValue(); }; if (keyCode == 37 || keyCode == 38) { event.preventDefault(); event.stopPropagation(); previous(); setValue(); }; }); } }); }; $("#" + childid).bind("mouseout", function(event) { setInsideWindow(false); $(document).unbind("keydown"); actionSettings.keyboardAction = false; actionSettings.currentKey = null; }); $("#" + titleid).bind("click", function(event) { setInsideWindow(false); if ($("#" + childid + ":visible").length == 1) { $("#" + childid).unbind("mouseover"); } else { $("#" + childid).bind("mouseover", function(event) { setInsideWindow(true); }); $this.open(); }; }); $("#" + titleid).bind("mouseout", function(evt) { setInsideWindow(false); }); if (options.showIcon && options.useSprite != false) { setTitleImageSprite(); }; }; var getByIndex = function(index) { for (var i in a_array) { if (a_array[i].index == index) { return a_array[i]; }; }; return -1; }; var manageSelection = function(obj) { var childid = getPostID("postChildID"); if (!ddList) { $("#" + childid + " a.selected").removeClass("selected"); }; var selectedA = $("#" + childid + " a.selected").attr("id"); if (selectedA != undefined) { var oldIndex = (actionSettings.oldIndex == undefined || actionSettings.oldIndex == null) ? a_array[selectedA].index : actionSettings.oldIndex; }; if (obj && !ddList) { $(obj).addClass("selected"); }; if (ddList) { var keyCode = actionSettings.currentKey; if ($("#" + elementid).attr("multiple") == true) { if (keyCode == 17) { actionSettings.oldIndex = a_array[$(obj).attr("id")].index; $(obj).toggleClass("selected"); } else if (keyCode == 16) { $("#" + childid + " a.selected").removeClass("selected"); $(obj).addClass("selected"); var currentSelected = $(obj).attr("id"); var currentIndex = a_array[currentSelected].index; for (var i = Math.min(oldIndex, currentIndex); i <= Math.max(oldIndex, currentIndex); i++) { $("#" + getByIndex(i).id).addClass("selected"); } } else { $("#" + childid + " a.selected").removeClass("selected"); $(obj).addClass("selected"); actionSettings.oldIndex = a_array[$(obj).attr("id")].index; }; } else { $("#" + childid + " a.selected").removeClass("selected"); $(obj).addClass("selected"); actionSettings.oldIndex = a_array[$(obj).attr("id")].index; }; }; }; var addRefreshMethods = function(id) { var objid = id; document.getElementById(objid).refresh = function(e) { $("#" + objid).msDropDown(options); }; }; var setInsideWindow = function(val) { actionSettings.insideWindow = val; }; var getInsideWindow = function() { return actionSettings.insideWindow; }; var applyEvents = function() { var mainid = getPostID("postID"); var actions_array = attributes.actions.split(","); for (var iCount = 0; iCount < actions_array.length; iCount++) { var action = actions_array[iCount]; var actionFound = has_handler(action); if (actionFound == true) { switch (action) { case "focus": $("#" + mainid).bind("mouseenter", function(event) { document.getElementById(elementid).focus(); }); break; case "click": $("#" + mainid).bind("click", function(event) { $("#" + elementid).trigger("click"); }); break; case "dblclick": $("#" + mainid).bind("dblclick", function(event) { $("#" + elementid).trigger("dblclick"); }); break; case "mousedown": $("#" + mainid).bind("mousedown", function(event) { $("#" + elementid).trigger("mousedown"); }); break; case "mouseup": $("#" + mainid).bind("mouseup", function(event) { $("#" + elementid).trigger("mouseup"); }); break; case "mouseover": $("#" + mainid).bind("mouseover", function(event) { $("#" + elementid).trigger("mouseover"); }); break; case "mousemove": $("#" + mainid).bind("mousemove", function(event) { $("#" + elementid).trigger("mousemove"); }); break; case "mouseout": $("#" + mainid).bind("mouseout", function(event) { $("#" + elementid).trigger("mouseout"); }); break; }; }; }; }; var setOutOfVision = function() { var sId = getPostID("postElementHolder"); $("#" + elementid).after("<div class='" + styles.ddOutOfVision + "' style='height:0px;overflow:hidden;position:absolute;' id='" + sId + "'></div>"); $("#" + elementid).appendTo($("#" + sId)); }; var setTitleText = function(sText) { var titletextid = getPostID("postTitleTextID"); $("#" + titletextid).html(sText); }; var next = function() { var titletextid = getPostID("postTitleTextID"); var childid = getPostID("postChildID"); var allAs = $("#" + childid + " a.enabled"); for (var current = 0; current < allAs.length; current++) { var currentA = allAs[current]; var id = $(currentA).attr("id"); if ($(currentA).hasClass("selected") && current < allAs.length - 1) { $("#" + childid + " a.selected").removeClass("selected"); $(allAs[current + 1]).addClass("selected"); var selectedA = $("#" + childid + " a.selected").attr("id"); if (!ddList) { var sText = (options.showIcon == false) ? a_array[selectedA].text : a_array[selectedA].html; setTitleText(sText); }; if (parseInt(($("#" + selectedA).position().top + $("#" + selectedA).height())) >= parseInt($("#" + childid).height())) { $("#" + childid).scrollTop(($("#" + childid).scrollTop()) + $("#" + selectedA).height() + $("#" + selectedA).height()); }; break; }; }; }; var previous = function() { var titletextid = getPostID("postTitleTextID"); var childid = getPostID("postChildID"); var allAs = $("#" + childid + " a.enabled"); for (var current = 0; current < allAs.length; current++) { var currentA = allAs[current]; var id = $(currentA).attr("id"); if ($(currentA).hasClass("selected") && current != 0) { $("#" + childid + " a.selected").removeClass("selected"); $(allAs[current - 1]).addClass("selected"); var selectedA = $("#" + childid + " a.selected").attr("id"); if (!ddList) { var sText = (options.showIcon == false) ? a_array[selectedA].text : a_array[selectedA].html; setTitleText(sText); }; if (parseInt(($("#" + selectedA).position().top + $("#" + selectedA).height())) <= 0) { $("#" + childid).scrollTop(($("#" + childid).scrollTop() - $("#" + childid).height()) - $("#" + selectedA).height()); }; break; }; }; }; var setTitleImageSprite = function() { if (options.useSprite != false) { var titletextid = getPostID("postTitleTextID"); var sClassName = document.getElementById(elementid).options[document.getElementById(elementid).selectedIndex].className; if (sClassName.length > 0) { var childid = getPostID("postChildID"); var id = $("#" + childid + " a." + sClassName).attr("id"); var backgroundImg = $("#" + id).css("background-image"); var backgroundPosition = $("#" + id).css("background-position"); var paddingLeft = $("#" + id).css("padding-left"); if (backgroundImg != undefined) { $("#" + titletextid).find("." + styles.ddTitleText).attr('style', "background:" + backgroundImg); }; if (backgroundPosition != undefined) { $("#" + titletextid).find("." + styles.ddTitleText).css('background-position', backgroundPosition); }; if (paddingLeft != undefined) { $("#" + titletextid).find("." + styles.ddTitleText).css('padding-left', paddingLeft); }; $("#" + titletextid).find("." + styles.ddTitleText).css('background-repeat', 'no-repeat'); $("#" + titletextid).find("." + styles.ddTitleText).css('padding-bottom', '2px'); }; }; }; var setValue = function() {
            var childid = getPostID("postChildID"); var allSelected = $("#" + childid + " a.selected"); if (allSelected.length == 1) {
                var sText = $("#" + childid + " a.selected").text(); var selectedA = $("#" + childid + " a.selected").attr("id"); if (selectedA != undefined) { var sValue = a_array[selectedA].value; document.getElementById(elementid).selectedIndex = a_array[selectedA].index; }; if (options.showIcon && options.useSprite != false)
                    setTitleImageSprite();
            } else if (allSelected.length > 1) { var alls = $("#" + elementid + " > option:selected").removeAttr("selected"); for (var i = 0; i < allSelected.length; i++) { var selectedA = $(allSelected[i]).attr("id"); var index = a_array[selectedA].index; document.getElementById(elementid).options[index].selected = "selected"; }; }; var sIndex = document.getElementById(elementid).selectedIndex; $this.ddProp["selectedIndex"] = sIndex;
        }; var has_handler = function(name) { if ($("#" + elementid).attr("on" + name) != undefined) { return true; }; var evs = $("#" + elementid).data("events"); if (evs && evs[name]) { return true; }; return false; }; var checkMethodAndApply = function() { var childid = getPostID("postChildID"); if (has_handler('change') == true) { var currentSelectedValue = a_array[$("#" + childid + " a.selected").attr("id")].text; if (selectedValue != currentSelectedValue) { $("#" + elementid).trigger("change"); }; }; if (has_handler('mouseup') == true) { $("#" + elementid).trigger("mouseup"); }; if (has_handler('blur') == true) { $(document).bind("mouseup", function(evt) { $("#" + elementid).focus(); $("#" + elementid)[0].blur(); setValue(); $(document).unbind("mouseup"); }); }; }; var hightlightArrow = function(ison) {
            var arrowid = getPostID("postArrowID"); if (ison == 1)
                $("#" + arrowid).css({ backgroundPosition: '0 100%' }); else
                $("#" + arrowid).css({ backgroundPosition: '0 0' });
        }; var setOriginalProperties = function() { for (var i in document.getElementById(elementid)) { if (typeof (document.getElementById(elementid)[i]) != 'function' && document.getElementById(elementid)[i] !== undefined && document.getElementById(elementid)[i] !== null) { $this.set(i, document.getElementById(elementid)[i], true); }; }; }; var setValueByIndex = function(prop, val) { if (getByIndex(val) != -1) { document.getElementById(elementid)[prop] = val; var childid = getPostID("postChildID"); $("#" + childid + " a.selected").removeClass("selected"); $("#" + getByIndex(val).id).addClass("selected"); var sText = getByIndex(document.getElementById(elementid).selectedIndex).html; setTitleText(sText); }; }; var addRemoveFromIndex = function(i, action) { if (action == 'd') { for (var key in a_array) { if (a_array[key].index == i) { delete a_array[key]; break; }; }; }; var count = 0; for (var key in a_array) { a_array[key].index = count; count++; }; }; this.open = function() { if (($this.get("disabled", true) == true) || ($this.get("options", true).length == 0)) return; var childid = getPostID("postChildID"); if (msOldDiv != "" && childid != msOldDiv) { $("#" + msOldDiv).slideUp("fast"); $("#" + msOldDiv).css({ zIndex: '0' }); }; if ($("#" + childid).css("display") == "none") { $(document).bind("keydown", function(event) { var keyCode = event.keyCode; if (keyCode == 39 || keyCode == 40) { event.preventDefault(); event.stopPropagation(); next(); }; if (keyCode == 37 || keyCode == 38) { event.preventDefault(); event.stopPropagation(); previous(); }; if (keyCode == 27 || keyCode == 13) { $this.close(); setValue(); }; if ($("#" + elementid).attr("onkeydown") != undefined) { document.getElementById(elementid).onkeydown(); }; }); $(document).bind("keyup", function(event) { if ($("#" + elementid).attr("onkeyup") != undefined) { document.getElementById(elementid).onkeyup(); }; }); $(document).bind("mouseup", function(evt) { if (getInsideWindow() == false) { $this.close(); }; }); $("#" + childid).css({ zIndex: options.zIndex }); $("#" + childid).slideDown("fast", function() { if ($this.onActions["onOpen"] != null) { eval($this.onActions["onOpen"])($this); }; }); if (childid != msOldDiv) { msOldDiv = childid; }; }; }; this.close = function() { var childid = getPostID("postChildID"); $(document).unbind("keydown"); $(document).unbind("keyup"); $(document).unbind("mouseup"); $("#" + childid).slideUp("fast", function(event) { checkMethodAndApply(); $("#" + childid).css({ zIndex: '0' }); if ($this.onActions["onClose"] != null) { eval($this.onActions["onClose"])($this); }; }); }; this.selectedIndex = function(i) { $this.set("selectedIndex", i); }; this.set = function(prop, val, isLocal) {
            if (prop == undefined || val == undefined) throw { message: "set to what?" }; $this.ddProp[prop] = val; if (isLocal != true) {
                switch (prop) {
                    case "selectedIndex": setValueByIndex(prop, val); break; case "disabled": $this.disabled(val, true); break; case "multiple": document.getElementById(elementid)[prop] = val; ddList = ($(sElement).attr("size") > 0 || $(sElement).attr("multiple") == true) ? true : false; if (ddList) { var iHeight = $("#" + elementid).height(); var childid = getPostID("postChildID"); $("#" + childid).css("height", iHeight + "px"); var titleid = getPostID("postTitleID"); $("#" + titleid).hide(); var childid = getPostID("postChildID"); $("#" + childid).css({ display: 'block', position: 'relative' }); applyEventsOnA(); }
                        break; case "size": document.getElementById(elementid)[prop] = val; if (val == 0) { document.getElementById(elementid).multiple = false; }; ddList = ($(sElement).attr("size") > 0 || $(sElement).attr("multiple") == true) ? true : false; if (val == 0) { var titleid = getPostID("postTitleID"); $("#" + titleid).show(); var childid = getPostID("postChildID"); $("#" + childid).css({ display: 'none', position: 'absolute' }); var sText = ""; if (document.getElementById(elementid).selectedIndex >= 0) { var aObj = getByIndex(document.getElementById(elementid).selectedIndex); sText = aObj.html; manageSelection($("#" + aObj.id)); }; setTitleText(sText); } else { var titleid = getPostID("postTitleID"); $("#" + titleid).hide(); var childid = getPostID("postChildID"); $("#" + childid).css({ display: 'block', position: 'relative' }); }; break; default: try { document.getElementById(elementid)[prop] = val; } catch (e) { }; break;
                };
            };
        }; this.get = function(prop, forceRefresh) { if (prop == undefined && forceRefresh == undefined) { return $this.ddProp; }; if (prop != undefined && forceRefresh == undefined) { return ($this.ddProp[prop] != undefined) ? $this.ddProp[prop] : null; }; if (prop != undefined && forceRefresh != undefined) { return document.getElementById(elementid)[prop]; }; }; this.visible = function(val) { var id = getPostID("postID"); if (val == true) { $("#" + id).show(); } else if (val == false) { $("#" + id).hide(); } else { return $("#" + id).css("display"); }; }; this.add = function(opt, index) { var objOpt = opt; var sText = objOpt.text; var sValue = (objOpt.value == undefined || objOpt.value == null) ? sText : objOpt.value; var img = (objOpt.title == undefined || objOpt.title == null) ? '' : objOpt.title; var i = (index == undefined || index == null) ? document.getElementById(elementid).options.length : index; document.getElementById(elementid).options[i] = new Option(sText, sValue); if (img != '') document.getElementById(elementid).options[i].title = img; var ifA = getByIndex(i); if (ifA != -1) { var aTag = createA(document.getElementById(elementid).options[i], i, "", ""); $("#" + ifA.id).html(aTag); } else { var aTag = createA(document.getElementById(elementid).options[i], i, "", ""); var childid = getPostID("postChildID"); $("#" + childid).append(aTag); applyEventsOnA(); }; }; this.remove = function(i) { document.getElementById(elementid).remove(i); if ((getByIndex(i)) != -1) { $("#" + getByIndex(i).id).remove(); addRemoveFromIndex(i, 'd'); }; if (document.getElementById(elementid).length == 0) { setTitleText(""); } else { var sText = getByIndex(document.getElementById(elementid).selectedIndex).html; setTitleText(sText); }; $this.set("selectedIndex", document.getElementById(elementid).selectedIndex); }; this.disabled = function(dis, isLocal) { document.getElementById(elementid).disabled = dis; var id = getPostID("postID"); if (dis == true) { $("#" + id).css("opacity", styles.disabled); $this.close(); } else if (dis == false) { $("#" + id).css("opacity", 1); }; if (isLocal != true) { $this.set("disabled", dis); }; }; this.form = function() { return (document.getElementById(elementid).form == undefined) ? null : document.getElementById(elementid).form; }; this.item = function() { if (arguments.length == 1) { return document.getElementById(elementid).item(arguments[0]); } else if (arguments.length == 2) { return document.getElementById(elementid).item(arguments[0], arguments[1]); } else { throw { message: "An index is required!" }; }; }; this.namedItem = function(nm) { return document.getElementById(elementid).namedItem(nm); }; this.multiple = function(is) { if (is == undefined) { return $this.get("multiple"); } else { $this.set("multiple", is); }; }; this.size = function(sz) { if (sz == undefined) { return $this.get("size"); } else { $this.set("size", sz); }; }; this.addMyEvent = function(nm, fn) { $this.onActions[nm] = fn; }; this.fireEvent = function(nm) { eval($this.onActions[nm])($this); }; var updateCommonVars = function() { $this.set("version", $.msDropDown.version); $this.set("author", $.msDropDown.author); }; var init = function() { createDropDown(); setOriginalProperties(); updateCommonVars(); if (options.onInit != '') { eval(options.onInit)($this); }; }; init();
    }; $.msDropDown = { version: 2.3, author: "Marghoob Suleman", create: function(id, opt) { return $(id).msDropDown(opt).data("dd"); } }; $.fn.extend({ msDropDown: function(options) {
        return this.each(function()
        { var mydropdown = new dd(this, options); $(this).data('dd', mydropdown); });
    } 
    });
})(jQuery);

//// jScrollPane.js 
//(function($){$.jScrollPane={active:[]};$.fn.jScrollPane=function(settings)
//{settings=$.extend({},$.fn.jScrollPane.defaults,settings);var rf=function(){return false;};return this.each(function()
//{var $this=$(this);var paneEle=this;var currentScrollPosition=0;var paneWidth;var paneHeight;var trackHeight;var trackOffset=settings.topCapHeight;var $container;if($(this).parent().is('.jScrollPaneContainer')){$container=$(this).parent();currentScrollPosition=settings.maintainPosition?$this.position().top:0;var $c=$(this).parent();paneWidth=$c.innerWidth();paneHeight=$c.outerHeight();$('>.jScrollPaneTrack, >.jScrollArrowUp, >.jScrollArrowDown, >.jScrollCap',$c).remove();$this.css({'top':0});}else{$this.data('originalStyleTag',$this.attr('style'));$this.css('overflow','hidden');this.originalPadding=$this.css('paddingTop')+' '+$this.css('paddingRight')+' '+$this.css('paddingBottom')+' '+$this.css('paddingLeft');this.originalSidePaddingTotal=(parseInt($this.css('paddingLeft'))||0)+(parseInt($this.css('paddingRight'))||0);paneWidth=$this.innerWidth();paneHeight=$this.innerHeight();$container=$('<div></div>').attr({'className':'jScrollPaneContainer'}).css({'height':paneHeight+'px','width':paneWidth+'px'});if(settings.enableKeyboardNavigation){$container.attr('tabindex',settings.tabIndex);}
//$this.wrap($container);$container=$this.parent();$(document).bind('emchange',function(e,cur,prev)
//{$this.jScrollPane(settings);});}
//trackHeight=paneHeight;if(settings.reinitialiseOnImageLoad){var $imagesToLoad=$.data(paneEle,'jScrollPaneImagesToLoad')||$('img',$this);var loadedImages=[];if($imagesToLoad.length){$imagesToLoad.each(function(i,val){$(this).bind('load readystatechange',function(){if($.inArray(i,loadedImages)==-1){loadedImages.push(val);$imagesToLoad=$.grep($imagesToLoad,function(n,i){return n!=val;});$.data(paneEle,'jScrollPaneImagesToLoad',$imagesToLoad);var s2=$.extend(settings,{reinitialiseOnImageLoad:false});$this.jScrollPane(s2);}}).each(function(i,val){if(this.complete||this.complete===undefined){this.src=this.src;}});});};}
//var p=this.originalSidePaddingTotal;var realPaneWidth=paneWidth-settings.scrollbarWidth-settings.scrollbarMargin-p;var cssToApply={'height':'auto','width':realPaneWidth+'px'}
//if(settings.scrollbarOnLeft){cssToApply.paddingLeft=settings.scrollbarMargin+settings.scrollbarWidth+'px';}else{cssToApply.paddingRight=settings.scrollbarMargin+'px';}
//$this.css(cssToApply);var contentHeight=$this.outerHeight();var percentInView=paneHeight/contentHeight;var isScrollable=percentInView<.99;$container[isScrollable?'addClass':'removeClass']('jScrollPaneScrollable');if(isScrollable){$container.append($('<div></div>').addClass('jScrollCap jScrollCapTop').css({height:settings.topCapHeight}),$('<div></div>').attr({'className':'jScrollPaneTrack'}).css({'width':settings.scrollbarWidth+'px'}).append($('<div></div>').attr({'className':'jScrollPaneDrag'}).css({'width':settings.scrollbarWidth+'px'}).append($('<div></div>').attr({'className':'jScrollPaneDragTop'}).css({'width':settings.scrollbarWidth+'px'}),$('<div></div>').attr({'className':'jScrollPaneDragBottom'}).css({'width':settings.scrollbarWidth+'px'}))),$('<div></div>').addClass('jScrollCap jScrollCapBottom').css({height:settings.bottomCapHeight}));var $track=$('>.jScrollPaneTrack',$container);var $drag=$('>.jScrollPaneTrack .jScrollPaneDrag',$container);var currentArrowDirection;var currentArrowTimerArr=[];var currentArrowInc;var whileArrowButtonDown=function()
//{if(currentArrowInc>4||currentArrowInc%4==0){positionDrag(dragPosition+currentArrowDirection*mouseWheelMultiplier);}
//currentArrowInc++;};if(settings.enableKeyboardNavigation){$container.bind('keydown.jscrollpane',function(e)
//{switch(e.keyCode){case 38:currentArrowDirection=-1;currentArrowInc=0;whileArrowButtonDown();currentArrowTimerArr[currentArrowTimerArr.length]=setInterval(whileArrowButtonDown,100);return false;case 40:currentArrowDirection=1;currentArrowInc=0;whileArrowButtonDown();currentArrowTimerArr[currentArrowTimerArr.length]=setInterval(whileArrowButtonDown,100);return false;case 33:case 34:return false;default:}}).bind('keyup.jscrollpane',function(e)
//{if(e.keyCode==38||e.keyCode==40){for(var i=0;i<currentArrowTimerArr.length;i++){clearInterval(currentArrowTimerArr[i]);}
//return false;}});}
//if(settings.showArrows){var currentArrowButton;var currentArrowInterval;var onArrowMouseUp=function(event)
//{$('html').unbind('mouseup',onArrowMouseUp);currentArrowButton.removeClass('jScrollActiveArrowButton');clearInterval(currentArrowInterval);};var onArrowMouseDown=function(){$('html').bind('mouseup',onArrowMouseUp);currentArrowButton.addClass('jScrollActiveArrowButton');currentArrowInc=0;whileArrowButtonDown();currentArrowInterval=setInterval(whileArrowButtonDown,100);};$container.append($('<a></a>').attr({'href':'javascript:;','className':'jScrollArrowUp','tabindex':-1}).css({'width':settings.scrollbarWidth+'px','top':settings.topCapHeight+'px'}).html('Scroll up').bind('mousedown',function()
//{currentArrowButton=$(this);currentArrowDirection=-1;onArrowMouseDown();this.blur();return false;}).bind('click',rf),$('<a></a>').attr({'href':'javascript:;','className':'jScrollArrowDown','tabindex':-1}).css({'width':settings.scrollbarWidth+'px','bottom':settings.bottomCapHeight+'px'}).html('Scroll down').bind('mousedown',function()
//{currentArrowButton=$(this);currentArrowDirection=1;onArrowMouseDown();this.blur();return false;}).bind('click',rf));var $upArrow=$('>.jScrollArrowUp',$container);var $downArrow=$('>.jScrollArrowDown',$container);}
//if(settings.arrowSize){trackHeight=paneHeight-settings.arrowSize-settings.arrowSize;trackOffset+=settings.arrowSize;}else if($upArrow){var topArrowHeight=$upArrow.height();settings.arrowSize=topArrowHeight;trackHeight=paneHeight-topArrowHeight-$downArrow.height();trackOffset+=topArrowHeight;}
//trackHeight-=settings.topCapHeight+settings.bottomCapHeight;$track.css({'height':trackHeight+'px',top:trackOffset+'px'})
//var $pane=$(this).css({'position':'absolute','overflow':'visible'});var currentOffset;var maxY;var mouseWheelMultiplier;var dragPosition=0;var dragMiddle=percentInView*paneHeight/2;var getPos=function(event,c){var p=c=='X'?'Left':'Top';return event['page'+c]||(event['client'+c]+(document.documentElement['scroll'+p]||document.body['scroll'+p]))||0;};var ignoreNativeDrag=function(){return false;};var initDrag=function()
//{ceaseAnimation();currentOffset=$drag.offset(false);currentOffset.top-=dragPosition;maxY=trackHeight-$drag[0].offsetHeight;mouseWheelMultiplier=2*settings.wheelSpeed*maxY/contentHeight;};var onStartDrag=function(event)
//{initDrag();dragMiddle=getPos(event,'Y')-dragPosition-currentOffset.top;$('html').bind('mouseup',onStopDrag).bind('mousemove',updateScroll).bind('mouseleave',onStopDrag)
//if($.browser.msie){$('html').bind('dragstart',ignoreNativeDrag).bind('selectstart',ignoreNativeDrag);}
//return false;};var onStopDrag=function()
//{$('html').unbind('mouseup',onStopDrag).unbind('mousemove',updateScroll);dragMiddle=percentInView*paneHeight/2;if($.browser.msie){$('html').unbind('dragstart',ignoreNativeDrag).unbind('selectstart',ignoreNativeDrag);}};var positionDrag=function(destY)
//{$container.scrollTop(0);destY=destY<0?0:(destY>maxY?maxY:destY);dragPosition=destY;$drag.css({'top':destY+'px'});var p=destY/maxY;$this.data('jScrollPanePosition',(paneHeight-contentHeight)*-p);$pane.css({'top':((paneHeight-contentHeight)*p)+'px'});$this.trigger('scroll');if(settings.showArrows){$upArrow[destY==0?'addClass':'removeClass']('disabled');$downArrow[destY==maxY?'addClass':'removeClass']('disabled');}};var updateScroll=function(e)
//{positionDrag(getPos(e,'Y')-currentOffset.top-dragMiddle);};var dragH=Math.max(Math.min(percentInView*(paneHeight-settings.arrowSize*2),settings.dragMaxHeight),settings.dragMinHeight);$drag.css({'height':dragH+'px'}).bind('mousedown',onStartDrag);var trackScrollInterval;var trackScrollInc;var trackScrollMousePos;var doTrackScroll=function()
//{if(trackScrollInc>8||trackScrollInc%4==0){positionDrag((dragPosition-((dragPosition-trackScrollMousePos)/2)));}
//trackScrollInc++;};var onStopTrackClick=function()
//{clearInterval(trackScrollInterval);$('html').unbind('mouseup',onStopTrackClick).unbind('mousemove',onTrackMouseMove);};var onTrackMouseMove=function(event)
//{trackScrollMousePos=getPos(event,'Y')-currentOffset.top-dragMiddle;};var onTrackClick=function(event)
//{initDrag();onTrackMouseMove(event);trackScrollInc=0;$('html').bind('mouseup',onStopTrackClick).bind('mousemove',onTrackMouseMove);trackScrollInterval=setInterval(doTrackScroll,100);doTrackScroll();return false;};$track.bind('mousedown',onTrackClick);$container.bind('mousewheel',function(event,delta){delta=delta||(event.wheelDelta?event.wheelDelta/120:(event.detail)?-event.detail/3:0);initDrag();ceaseAnimation();var d=dragPosition;positionDrag(dragPosition-delta*mouseWheelMultiplier);var dragOccured=d!=dragPosition;return!dragOccured;});var _animateToPosition;var _animateToInterval;function animateToPosition()
//{var diff=(_animateToPosition-dragPosition)/settings.animateStep;if(diff>1||diff<-1){positionDrag(dragPosition+diff);}else{positionDrag(_animateToPosition);ceaseAnimation();}}
//var ceaseAnimation=function()
//{if(_animateToInterval){clearInterval(_animateToInterval);delete _animateToPosition;}};var scrollTo=function(pos,preventAni)
//{if(typeof pos=="string"){try{$e=$(pos,$this);}catch(err){return;}
//if(!$e.length)return;pos=$e.offset().top-$this.offset().top;}
//ceaseAnimation();var maxScroll=contentHeight-paneHeight;pos=pos>maxScroll?maxScroll:pos;$this.data('jScrollPaneMaxScroll',maxScroll);var destDragPosition=pos/maxScroll*maxY;if(preventAni||!settings.animateTo){positionDrag(destDragPosition);}else{$container.scrollTop(0);_animateToPosition=destDragPosition;_animateToInterval=setInterval(animateToPosition,settings.animateInterval);}};$this[0].scrollTo=scrollTo;$this[0].scrollBy=function(delta)
//{var currentPos=-parseInt($pane.css('top'))||0;scrollTo(currentPos+delta);};initDrag();scrollTo(-currentScrollPosition,true);$('*',this).bind('focus',function(event)
//{var $e=$(this);var eleTop=0;var preventInfiniteLoop=100;while($e[0]!=$this[0]){eleTop+=$e.position().top;$e=$e.offsetParent();if(!preventInfiniteLoop--){return;}}
//var viewportTop=-parseInt($pane.css('top'))||0;var maxVisibleEleTop=viewportTop+paneHeight;var eleInView=eleTop>viewportTop&&eleTop<maxVisibleEleTop;if(!eleInView){var destPos=eleTop-settings.scrollbarMargin;if(eleTop>viewportTop){destPos+=$(this).height()+15+settings.scrollbarMargin-paneHeight;}
//scrollTo(destPos);}})
//if(settings.observeHash){if(location.hash&&location.hash.length>1){setTimeout(function(){scrollTo(location.hash);},$.browser.safari?100:0);}
//$(document).bind('click',function(e){$target=$(e.target);if($target.is('a')){var h=$target.attr('href');if(h&&h.substr(0,1)=='#'&&h.length>1){setTimeout(function(){scrollTo(h,!settings.animateToInternalLinks);},$.browser.safari?100:0);}}});}
//function onSelectScrollMouseDown(e)
//{$(document).bind('mousemove.jScrollPaneDragging',onTextSelectionScrollMouseMove);$(document).bind('mouseup.jScrollPaneDragging',onSelectScrollMouseUp);}
//var textDragDistanceAway;var textSelectionInterval;function onTextSelectionInterval()
//{direction=textDragDistanceAway<0?-1:1;$this[0].scrollBy(textDragDistanceAway/2);}
//function clearTextSelectionInterval()
//{if(textSelectionInterval){clearInterval(textSelectionInterval);textSelectionInterval=undefined;}}
//function onTextSelectionScrollMouseMove(e)
//{var offset=$this.parent().offset().top;var maxOffset=offset+paneHeight;var mouseOffset=getPos(e,'Y');textDragDistanceAway=mouseOffset<offset?mouseOffset-offset:(mouseOffset>maxOffset?mouseOffset-maxOffset:0);if(textDragDistanceAway==0){clearTextSelectionInterval();}else{if(!textSelectionInterval){textSelectionInterval=setInterval(onTextSelectionInterval,100);}}}
//function onSelectScrollMouseUp(e)
//{$(document).unbind('mousemove.jScrollPaneDragging').unbind('mouseup.jScrollPaneDragging');clearTextSelectionInterval();}
//$container.bind('mousedown.jScrollPane',onSelectScrollMouseDown);$.jScrollPane.active.push($this[0]);}else{$this.css({'height':paneHeight+'px','width':paneWidth-this.originalSidePaddingTotal+'px','padding':this.originalPadding});$this[0].scrollTo=$this[0].scrollBy=function(){};$this.parent().unbind('mousewheel').unbind('mousedown.jScrollPane').unbind('keydown.jscrollpane').unbind('keyup.jscrollpane');}})};$.fn.jScrollPaneRemove=function()
//{$(this).each(function()
//{$this=$(this);var $c=$this.parent();if($c.is('.jScrollPaneContainer')){$this.css({'top':'','height':'','width':'','padding':'','overflow':'','position':''});$this.attr('style',$this.data('originalStyleTag'));$c.after($this).remove();}});}
//$.fn.jScrollPane.defaults = { scrollbarWidth: 10, scrollbarMargin: 5, wheelSpeed: 18, showArrows: false, arrowSize: 0, animateTo: false, dragMinHeight: 1, dragMaxHeight: 99999, animateInterval: 100, animateStep: 3, maintainPosition: true, scrollbarOnLeft: false, reinitialiseOnImageLoad: false, tabIndex: 0, enableKeyboardNavigation: true, animateToInternalLinks: false, topCapHeight: 0, bottomCapHeight: 0, observeHash: true }; $(window).bind('unload', function() { var els = $.jScrollPane.active; for (var i = 0; i < els.length; i++) { els[i].scrollTo = els[i].scrollBy = null; } });
//})(jQuery);

// General.js
function ScrollTop() {
    $('body,html').animate({
        scrollTop: 150
    }, 800);
}
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
return ctrl;
}
function isPdf(obj) {    
    if (obj.value.length > 0) {
        if (obj.value.length > 4) {
            var tmpExt = obj.value.split('.'); var ext = tmpExt[tmpExt.length - 1]; if (ext.toLowerCase() == 'pdf' || ext.toLowerCase() == 'doc' || ext.toLowerCase() == 'docx') { return true; }
            else { return false; }
        }
        else { return false; }
    }
    return false;
}
function isImage(obj){if(obj.value.length>0){if(obj.value.length>4){var tmpExt=obj.value.split('.');var ext=tmpExt[tmpExt.length-1];if(ext.toLowerCase()=='jpg'||ext.toLowerCase()=='bmp'||ext.toLowerCase()=='tif'||ext.toLowerCase()=='tiff'||ext.toLowerCase()=='jpeg'||ext.toLowerCase()=='gif'||ext.toLowerCase()=='png'){return true;}
else{return false;}}
else{return false;}}
return false;}
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

// jquery.em.js 
//jQuery(function($){var eventName='emchange';$.em=$.extend({version:'1.0',delay:200,element:$('<div />').css({left:'-100em',position:'absolute',width:'100em'}).prependTo('body')[0],action:function(){var currentWidth=$.em.element.offsetWidth/100;if(currentWidth!=$.em.current){$.em.previous=$.em.current;$.em.current=currentWidth;$.event.trigger(eventName,[$.em.current,$.em.previous]);}}},$.em);$.fn[eventName]=function(fn){return fn?this.bind(eventName,fn):this.trigger(eventName);};$.em.current=$.em.element.offsetWidth/100;$.em.iid=setInterval($.em.action,$.em.delay);});

//// search-box.js
//if(!window.Node){var Node={ELEMENT_NODE:1,ATTRIBUTE_NODE:2,TEXT_NODE:3,COMMENT_NODE:8,DOCUMENT_NODE:9,DOCUMENT_FRAGMENT_NODE:11}}
//var KEY_PAGEUP=33;var KEY_PAGEDOWN=34;var KEY_END=35;var KEY_HOME=36;var KEY_LEFT=37;var KEY_UP=38;var KEY_RIGHT=39;var KEY_DOWN=40;var KEY_SPACE=32;var KEY_TAB=9;var KEY_BACKSPACE=8;var KEY_DELETE=46;var KEY_ENTER=13;var KEY_INSERT=45;var KEY_ESCAPE=27;var KEY_F1=112;var KEY_F2=113;var KEY_F3=114;var KEY_F4=115;var KEY_F5=116;var KEY_F6=117;var KEY_F7=118;var KEY_F8=119;var KEY_F9=120;var KEY_F10=121;var KEY_M=77;var NS_XHTML="http://www.w3.org/1999/xhtml"
//var NS_STATE="http://www.w3.org/2005/07/aaa";function nextSiblingElement(node){var next_node=node.nextSibling;while(next_node&&(next_node.nodeType!=Node.ELEMENT_NODE)){next_node=next_node.nextSibling;}
//return next_node;}
//function previousSiblingElement(node){var next_node=node.previousSibling;while(next_node&&(next_node.nodeType!=Node.ELEMENT_NODE)){next_node=next_node.previousSibling;}
//return next_node;}
//function firstChildElement(node){var next_node=node.firstChild;while(next_node&&(next_node.nodeType!=Node.ELEMENT_NODE)){next_node=next_node.nextSibling;}
//return next_node;}
//function getTextContentOfNode(node){var next_node=node.firstChild;var str="";while(next_node){if((next_node.nodeType==Node.TEXT_NODE)&&(next_node.length>0))
//str+=next_node.data;next_node=next_node.nextSibling;}
//return str;}
//function setTextContentOfNode(node,text){var text_node=document.createTextNode(text);while(node.firstChild){node.removeChild(node.firstChild);}
//node.appendChild(text_node);}
//if(!window.Node){var Node={ELEMENT_NODE:1,ATTRIBUTE_NODE:2,TEXT_NODE:3,COMMENT_NODE:8,DOCUMENT_NODE:9,DOCUMENT_FRAGMENT_NODE:11}}
//var ARIA_STATE="aria-";function Widgets(){this.widgets=new Array();}
//Widgets.prototype.add=function(obj){this.widgets[this.widgets.length]=obj;}
//Widgets.prototype.init=function(){for(var i=0;i<this.widgets.length;i++)
//this.widgets[i].init();}
//function _$(id){return document.getElementById(id);}
//function WebBrowser(){}
//WebBrowser.prototype.keyCode=function(event){var e=event||window.event;return e.keyCode;}
//if(document.createEvent){WebBrowser.prototype.simulateOnClickEvent=function(node){var e=document.createEvent('MouseEvents');e.initEvent('click',true,true);node.dispatchEvent(e);}}else{WebBrowser.prototype.simulateOnClickEvent=function(node){var e=document.createEventObject();node.fireEvent("onclick",e);}}
//if(document.addEventListener){WebBrowser.prototype.setMouseCapture=function(node,clickHandler,downHandler,moveHandler,upHandler){if(clickHandler)
//document.addEventListener("click",clickHandler,true);if(downHandler)
//document.addEventListener("mousedown",downHandler,true);if(moveHandler)
//document.addEventListener("mousemove",moveHandler,true);if(upHandler)
//document.addEventListener("mouseup",upHandler,true);}
//WebBrowser.prototype.releaseMouseCapture=function(node,clickHandler,downHandler,moveHandler,upHandler){if(upHandler)
//document.removeEventListener("mouseup",upHandler,true);if(moveHandler)
//document.removeEventListener("mousemove",moveHandler,true);if(downHandler)
//document.removeEventListener("mousedown",downHandler,true);if(clickHandler)
//document.removeEventListener("click",clickHandler,true);}}else{WebBrowser.prototype.setMouseCapture=function(node,clickHandler,downHandler,moveHandler,upHandler){node.setCapture();if(clickHandler)
//node.attachEvent("onclick",clickHandler);if(downHandler)
//node.attachEvent("onmousedown",downHandler);if(moveHandler)
//node.attachEvent("onmousemove",moveHandler);if(upHandler)
//node.attachEvent("onmouseup",upHandler);}
//WebBrowser.prototype.releaseMouseCapture=function(node,clickHandler,downHandler,moveHandler,upHandler){if(upHandler)
//node.detachEvent("onmouseup",upHandler);if(moveHandler)
//node.detachEvent("onmousemove",moveHandler);if(downHandler)
//node.detachEvent("onmousedown",downHandler);if(clickHandler)
//node.detachEvent("onclick",clickHandler);node.releaseCapture();}}
//if(typeof document.documentElement.setAttributeNS!='undefined'){WebBrowser.prototype.stopPropagation=function(event){event.stopPropagation();event.preventDefault();return false;}
//WebBrowser.prototype.target=function(event){return event.target;}
//WebBrowser.prototype.attrName=function(event){return event.attrName}
//WebBrowser.prototype.charCode=function(event){return event.charCode;}
//WebBrowser.prototype.calculateOffsetLeft=function(node){return node.offsetLeft;}
//WebBrowser.prototype.calculateOffsetTop=function(node){return node.offsetTop;}
//WebBrowser.prototype.pageX=function(e){return e.pageX;}
//WebBrowser.prototype.pageY=function(e){return e.pageY;}
//WebBrowser.prototype.setNodePosition=function(node,left,top){node.style.left=left+"px";node.style.top=top+"px";}}else{WebBrowser.prototype.stopPropagation=function(event){event.cancelBubble=true;event.returnValue=false;return false;}
//WebBrowser.prototype.charCode=function(event){return window.browser.keyCode(event);}
//WebBrowser.prototype.target=function(event){return event.srcElement;}
//WebBrowser.prototype.attrName=function(event){return event.propertyName;}
//WebBrowser.prototype.calculateOffsetLeft=function(node){var offset=0;while(node){offset+=node.offsetLeft;node=node.offsetParent;}
//return offset;}
//WebBrowser.prototype.calculateOffsetTop=function(node){var offset=0;while(node){offset=offset+node.offsetTop;node=node.offsetParent;}
//return offset;}
//WebBrowser.prototype.pageX=function(e){return e.clientX+(document.documentElement.scrollLeft||document.body.scrollLeft);}
//WebBrowser.prototype.pageY=function(e){return e.clientY+(document.documentElement.scrollTop||document.body.scrollTop);}
//WebBrowser.prototype.setNodePosition=function(node,left,top){offsetx=0;offsety=0;nnode=node.offsetParent
//while(nnode){offsetx=offsetx+nnode.offsetLeft;offsety=offsety+nnode.offsetTop;nnode=nnode.offsetParent;}
//node.style.left=left-offsetx+"px";node.style.top=top-offsety+"px";}};if(document.addEventListener){WebBrowser.prototype.addEvent=function(elmTarget,sEventName,fCallback){elmTarget.addEventListener(sEventName,fCallback,false);returnValue=true;};WebBrowser.prototype.removeEvent=function(elmTarget,sEventName,fCallback){elmTarget.removeEventListener(sEventName,fCallback,false);returnValue=true;};WebBrowser.prototype.addChangeEvent=function(elmTarget,fCallback){elmTarget.addEventListener("DOMAttrModified",fCallback,false);returnValue=true;};}else{if(document.attachEvent){WebBrowser.prototype.addEvent=function(elmTarget,sEventName,fCallback){returnValue=elmTarget.attachEvent('on'+sEventName,fCallback);};WebBrowser.prototype.removeEvent=function(elmTarget,sEventName,fCallback){returnValue=elmTarget.detachEvent('on'+sEventName,fCallback);};WebBrowser.prototype.addChangeEvent=function(elmTarget,fCallback){returnValue=elmTarget.attachEvent("onpropertychange",fCallback);};}else{WebBrowser.prototype.addEvent=function(elmTarget,sEventName,fCallback){return false;};WebBrowser.prototype.removeEvent=function(elmTarget,sEventName,fCallback){return false;};WebBrowser.prototype.addChangeEvent=function(elmTarget,fCallback){return false;};}}
//widgets_flag=true;var widgets=new Widgets();var browser=new WebBrowser();function initApp(){widgets.init();}
//function Checkbox(id){this.id=id;}
//Checkbox.prototype.init=function(){this.node=document.getElementById(this.id);this.image_node=this.node.getElementsByTagName("img")[0];if(this.node.getAttribute("aria-checked")=="true")
//this.image_node.src="images/checked.gif";var obj=this;browser.addEvent(this.node,"keydown",function(event){handleCheckboxKeyDownEvent(event,obj);},false);browser.addEvent(this.node,"keypress",function(event){handleCheckboxKeyPressEvent(event,obj);},false);browser.addEvent(this.node,"click",function(event){handleCheckboxClickEvent(event,obj);},false);browser.addEvent(this.node,"focus",function(event){handleCheckboxFocusEvent(event,obj);},false);browser.addEvent(this.node,"blur",function(event){handleCheckboxBlurEvent(event,obj);},false);}
//toggleCheckbox=function(checkbox){if(checkbox.node.getAttribute("aria-checked")=="true"){checkbox.node.setAttribute("aria-checked","false");checkbox.image_node.src="images/unchecked.gif";}else{checkbox.node.setAttribute("aria-checked","true");checkbox.image_node.src="images/checked.gif";}}
//handleCheckboxKeyDownEvent=function(event,checkbox){var e=event||window.event;switch(browser.keyCode(e)){case KEY_SPACE:toggleCheckbox(checkbox);return browser.stopPropagation(e);break;}
//return true;}
//handleCheckboxKeyPressEvent=function(event,checkbox){var e=event||window.event;switch(browser.keyCode(e)){case KEY_SPACE:return browser.stopPropagation(e);break;}
//return true;}
//handleCheckboxFocusEvent=function(event,checkbox){checkbox.node.className+="";return true;}
//handleCheckboxBlurEvent=function(event,checkbox){checkbox.node.className+="";return true;}
//function handleCheckboxClickEvent(event,checkbox){var e=event||window.event;if((checkbox.node==browser.target(e))||(checkbox.image_node==browser.target(e))){toggleCheckbox(checkbox);checkbox.node.focus();return browser.stopPropagation(e);}
//return true;}
//$(document).ready(function() { $('a.poplight[href^=#]').click(function() { var popID = $(this).attr('rel'); var popURL = $(this).attr('href'); var query = popURL.split('?'); var dim = query[1].split('&'); var popWidth = dim[0].split('=')[1]; $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="'+ virtualDir +'images/close-ic.gif" class="btn_close" title="Close Window" alt="Close" /></a>'); var popMargTop = ($('#' + popID).height() + 80) / 2; var popMargLeft = ($('#' + popID).width() + 80) / 2; $('#' + popID).css({ 'margin-top': -popMargTop, 'margin-left': -popMargLeft }); $('body').append('<div id="fade"></div>'); $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); return false; }); $('a.close, #fade').live('click', function() { $('#fade , .popup_block').fadeOut(function() { $('#fade, a.close').remove(); }); return false; }); });

// popup.js
var popupStatus=0;var scrollTop=0;function loadPopup(){if(popupStatus==0){$("#backgroundPopup").css({"opacity":"0.7"});$("#backgroundPopup").fadeIn("slow");$("#loginBlock").fadeIn("slow");popupStatus=1;}}
function disablePopup(){if(popupStatus==1){$("#backgroundPopup").fadeOut("slow");$("#loginBlock").fadeOut("slow");popupStatus=0;}}
function centerPopup(){var windowWidth=$(document).width();var windowHeight=$(document).height();var popupHeight=$("#loginBlock").height();var popupWidth=$("#loginBlock").width();$("#loginBlock").css({"position":"absolute","top":scrollTop+100,"left":(windowWidth/2)-(popupWidth/2)});$("#backgroundPopup").css({"height":windowHeight});}
$(document).ready(function() { $("#backgroundPopup").click(function() { disablePopup(); }); $(document).keypress(function(e) { if (e.keyCode == 27 && popupStatus == 1) { disablePopup(); } }); scrollTop = $(window).scrollTop(); $(window).scroll(function() { scrollTop = $(window).scrollTop(); centerPopup(); }); });

//// checkbox.js
//var elmHeight="25";jQuery.fn.extend({dgStyle:function()
//{$.each($(this),function(){var elm=$(this).children().get(0);elmType=$(elm).attr("type");$(this).data('type',elmType);$(this).data('checked',$(elm).attr("checked"));$(this).dgClear();});$(this).mousedown(function(){$(this).dgEffect();});$(this).mouseup(function(){$(this).dgHandle();});},dgClear:function()
//{if($(this).data("checked")==true)
//$(this).css("backgroundPosition","left -"+(elmHeight*2)+"px");else
//$(this).css("backgroundPosition","left 0");},dgEffect:function()
//{if($(this).data('type')=='radio'&&$(this).data("checked")==true)
//return;if($(this).data("checked")==true)
//$(this).css({backgroundPosition:"left -"+(elmHeight*3)+"px"});else
//$(this).css({backgroundPosition:"left -"+(elmHeight)+"px"});},dgHandle:function()
//{if($(this).data('type')=='radio'&&$(this).data("checked")==true)
//return;var elm=$(this).children().get(0);if($(this).data("checked")==true)
//$(elm).dgUncheck(this);else
//$(elm).dgCheck(this);if($(this).data('type')=='radio')
//{$.each($("input[name='"+$(elm).attr("name")+"']"),function()
//{if(elm!=this)
//$(this).dgUncheck(-1);});}},dgCheck:function(div)
//{$(this).attr("checked",true);$(div).data('checked',true).css({backgroundPosition:"left -"+(elmHeight*2)+"px"});},dgUncheck:function(div)
//{$(this).attr("checked",false);if(div!=-1)
//$(div).data('checked',false).css({backgroundPosition:"left 0"});else
//    $(this).parent().data("checked", false).css({ backgroundPosition: "left 0" });
//} 
//});

////jquery.mousewheel.js
//(function($) {
//    $.event.special.mousewheel = { setup: function() {
//        var handler = $.event.special.mousewheel.handler; if ($.browser.mozilla)
//            $(this).bind('mousemove.mousewheel', function(event) { $.data(this, 'mwcursorposdata', { pageX: event.pageX, pageY: event.pageY, clientX: event.clientX, clientY: event.clientY }); }); if (this.addEventListener)
//            this.addEventListener(($.browser.mozilla ? 'DOMMouseScroll' : 'mousewheel'), handler, false); else
//            this.onmousewheel = handler;
//    }, teardown: function() {
//        var handler = $.event.special.mousewheel.handler; $(this).unbind('mousemove.mousewheel'); if (this.removeEventListener)
//            this.removeEventListener(($.browser.mozilla ? 'DOMMouseScroll' : 'mousewheel'), handler, false); else
//            this.onmousewheel = function() { }; $.removeData(this, 'mwcursorposdata');
//    }, handler: function(event) { var args = Array.prototype.slice.call(arguments, 1); event = $.event.fix(event || window.event); $.extend(event, $.data(this, 'mwcursorposdata') || {}); var delta = 0, returnValue = true; if (event.wheelDelta) delta = event.wheelDelta / 120; if (event.detail) delta = -event.detail / 3; event.data = event.data || {}; event.type = "mousewheel"; args.unshift(delta); args.unshift(event); return $.event.handle.apply(this, args); } 
//    }; $.fn.extend({ mousewheel: function(fn) { return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel"); }, unmousewheel: function(fn) { return this.unbind("mousewheel", fn); } });
//})(jQuery);

////jquery.floatingmessage.js
//(function($) {
//    var range = { top: { left: 10, right: 10 }, bottom: { left: 10, right: 10} }; var right = 10; var fContainer = []; var scrollTop = 0; $(document).ready(function() { scrollTop = $(window).scrollTop(); $(window).scroll(function() { scrollTop = $(window).scrollTop(); for (i = 0; i < fContainer.length; i++) { var animate = {}; var e = fContainer[i]; animate[e.verticalAlign] = e.range; if (e.verticalAlign == "top") animate[e.verticalAlign] += scrollTop; else animate[e.verticalAlign] -= scrollTop; e.f.animate(animate, { duration: e.moveEaseTime, easing: e.moveEasing, queue: false }); } }); }); $.floatingMessage = function(message, options) {
//        options = options || {}; options = $.extend({ verticalAlign: "top", align: "left", width: 300, height: 17, color: 'white', time: false, show: "drop", hide: "drop", padding: 10, margin: 10, stuffEaseTime: 1000, stuffEasing: "easeOutBounce", moveEaseTime: 500, moveEasing: "easeInOutCubic", element: $("<div></div>"), onClose: false }, options); var f = $("<div></div>").attr({ id: "jqueryFloatingMessage" + new Date().getTime() }).addClass("float-msg")
//        var o = $.extend(true, {}, options); $.extend(o, { f: f, range: range[o.verticalAlign][o.align] }); if (message) o.element.html(message.replace(/\n/g, "<br />")); var msg = message.split('<br />'); var maxWidth = msg[0].length; for (var myx in msg) {
//            if (msg[myx].length > maxWidth)
//                maxWidth = msg[myx].length;
//        }
//        var mWidth = maxWidth * 7; var mHeight = (msg.length * 15) + 4; var css = { width: mWidth + 'px', height: mHeight + "px", position: "absolute", padding: o.padding + "px", color: o.color }; css[o.verticalAlign] = range[o.verticalAlign][o.align]; css[o.align] = right; if (o.verticalAlign == "top") { css[o.verticalAlign] += scrollTop; } else { css[o.verticalAlign] -= scrollTop; }
//        f.css(css).append(o.element); f.css('z-index', '9000'); var timerId = false; var remove = function() {
//            if (timerId) clearTimeout(timerId); var e; var animate = {}; var deleteIndex; var orange = (o.height + o.margin + (o.padding * 2)); for (i = 0; i < fContainer.length; i++) { e = fContainer[i]; if (o === e) deleteIndex = i; if (e.range > o.range && e.align == o.align && e.verticalAlign == o.verticalAlign) { e.range -= orange; if (e.range < 0) e.range = 0; animate[e.verticalAlign] = e.range; if (e.verticalAlign == "top") animate[e.verticalAlign] += scrollTop; else animate[e.verticalAlign] -= scrollTop; e.f.animate(animate, { duration: o.stuffEaseTime, easing: o.stuffEasing, queue: false }); } }
//            fContainer.splice(deleteIndex, 1); range[o.verticalAlign][o.align] -= (o.height + o.margin + (o.padding * 2)); if (range[o.verticalAlign][o.align] < 0) range[o.verticalAlign][o.align] = 10; f.hide(o.hide, function() { $(this).remove; if (o.onClose) o.onClose(); });
//        }; if (o.time) { timerId = setTimeout(remove, o.time); }
//        f.bind("click", remove); $(document.body).append(f); f.show(o.show); fContainer.push(o); range[o.verticalAlign][o.align] += (o.height + o.margin + (o.padding * 2));
//    }
//    $.fn.floatingMessage = function(options) { options = options || {}; options.element = this; $.floatingMessage(false, options); }
//})(jQuery);


//jquery.numeric.js
(function($) {
    $.fn.numeric = function(decimal, callback)
    { decimal = (decimal === false) ? "" : decimal || "."; callback = typeof callback == "function" ? callback : function() { }; return this.data("numeric.decimal", decimal).data("numeric.callback", callback).keypress($.fn.numeric.keypress).blur($.fn.numeric.blur); }
    $.fn.numeric.keypress = function(e) {
        var decimal = $.data(this, "numeric.decimal"); var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0; if (key == 13 && this.nodeName.toLowerCase() == "input")
        { return true; }
        else if (key == 13)
        { return false; }
        var allow = false; if ((e.ctrlKey && key == 97) || (e.ctrlKey && key == 65)) return true; if ((e.ctrlKey && key == 120) || (e.ctrlKey && key == 88)) return true; if ((e.ctrlKey && key == 99) || (e.ctrlKey && key == 67)) return true; if ((e.ctrlKey && key == 122) || (e.ctrlKey && key == 90)) return true; if ((e.ctrlKey && key == 118) || (e.ctrlKey && key == 86) || (e.shiftKey && key == 45)) return true; if (key < 48 || key > 57) {
            if (key == 45 && this.value.length == 0) return true; if (decimal && key == decimal.charCodeAt(0) && this.value.indexOf(decimal) != -1)
            { allow = false; }
            if (key != 8 && key != 9 && key != 13 && key != 35 && key != 36 && key != 37 && key != 39 && key != 46)
            { allow = false; }
            else {
                if (typeof e.charCode != "undefined") {
                    if (e.keyCode == e.which && e.which != 0)
                    { allow = true; if (e.which == 46) allow = false; }
                    else if (e.keyCode != 0 && e.charCode == 0 && e.which == 0)
                    { allow = true; } 
                } 
            }
            if (decimal && key == decimal.charCodeAt(0)) {
                if (this.value.indexOf(decimal) == -1)
                { allow = true; }
                else
                { allow = false; } 
            } 
        }
        else
        { allow = true; }
        return allow;
    }
    $.fn.numeric.blur = function() {
        var decimal = $.data(this, "numeric.decimal"); var callback = $.data(this, "numeric.callback"); var val = $(this).val(); if (val != "") {
            var re = new RegExp("^\\d+$|\\d*" + decimal + "\\d+"); if (!re.exec(val))
            { callback.apply(this); } 
        } 
    }
    $.fn.removeNumeric = function()
    { return this.data("numeric.decimal", null).data("numeric.callback", null).unbind("keypress", $.fn.numeric.keypress).unbind("blur", $.fn.numeric.blur); }
})(jQuery);

////jquery.multiselect.js
//(function($) {
//    var multiselectID = 0; $.widget("ech.multiselect", { options: { header: true, height: 175, minWidth: 225, classes: '', checkAllText: 'Check all', uncheckAllText: 'Uncheck all', noneSelectedText: 'Select options', selectedText: '# selected', selectedList: 0, show: '', hide: '', autoOpen: false, multiple: true, position: {} }, _create: function() {
//        var self = this, el = this.element, o = this.options, html = [], optgroups = [], title = el.attr('title'), id = el.id || multiselectID++; this.speed = $.fx.speeds._default; this._isOpen = false; html.push('<button type="button" class="ui-multiselect ui-widget ui-state-default ui-corner-all'); if (o.classes.length) { html.push(' ' + o.classes); }
//        html.push('"'); if (title.length) { html.push(' title="' + title + '"'); }
//        html.push('><span class="ui-icon ui-icon-triangle-2-n-s"></span><span>' + o.noneSelectedText + '</span></button>'); html.push('<div class="ui-multiselect-menu ui-widget ui-widget-content ui-corner-all ' + (o.classes.length ? o.classes : '') + '">'); html.push('<div class="ui-widget-header ui-corner-all ui-multiselect-header ui-helper-clearfix">'); html.push('<ul class="ui-helper-reset">'); if (o.header === true && o.multiple) { html.push('<li><a class="ui-multiselect-all" href="#"><span class="ui-icon ui-icon-check"></span><span>' + o.checkAllText + '</span></a> &nbsp;</li>'); html.push('<li><a class="ui-multiselect-none" href="#"><span class="ui-icon ui-icon-closethick"></span><span>' + o.uncheckAllText + '</span></a></li>'); } else if (typeof o.header === "string") { html.push('<li>' + o.header + '</li>'); }
//        html.push('<li class="ui-multiselect-close"><a href="#" class="ui-multiselect-close"><span class="ui-icon ui-icon-circle-close"></span></a></li>'); html.push('</ul>'); html.push('</div>'); html.push('<ul class="ui-multiselect-checkboxes ui-helper-reset">'); el.find('option').each(function(i) {
//            var $this = $(this), title = $this.html(), value = this.value, inputID = this.id || "ui-multiselect-" + id + "-option-" + i, $parent = $this.parent(), isDisabled = $this.is(':disabled'), labelClasses = ['ui-corner-all']; if ($parent.is('optgroup')) { var label = $parent.attr('label'); if ($.inArray(label, optgroups) === -1) { html.push('<li class="ui-multiselect-optgroup-label"><a href="#">' + label + '</a></li>'); optgroups.push(label); } }
//            if (value.length > 0) {
//                if (isDisabled) { labelClasses.push('ui-state-disabled'); }
//                html.push('<li id="lbl-' + inputID + '" class="' + (isDisabled ? 'ui-multiselect-disabled' : '') + '">'); html.push('<label for="' + inputID + '" class="' + labelClasses.join(' ') + '"><em><input id="' + inputID + '" type="' + (o.multiple ? "checkbox" : "radio") + '" value="' + value + '" title="' + title + '"'); if ($this.is(':selected')) { html.push(' checked="checked"'); }
//                if (isDisabled) { html.push(' disabled="disabled"'); }
//                html.push(' /></em><b class=bpadremove>' + title + '</b></label></li>');
//            } 
//        }); html.push('</ul></div>'); this.button = el.hide().after(html.join('')).next('button'); this.menu = this.button.next('div.ui-multiselect-menu'); this.labels = this.menu.find('label'); this.buttonlabel = this.button.find('span').eq(-1); if (!o.multiple) { this.radios = this.menu.find(":radio"); }
//        this._setButtonWidth(); this._setMenuWidth(); this._bindEvents(); this.button[0].defaultValue = this.update();
//    }, _init: function() {
//        if (!this.options.header) { this.menu.find('div.ui-multiselect-header').hide(); }
//        if (this.options.autoOpen) { this.open(); }
//        if (this.element.is(':disabled')) { this.disable(); } 
//    }, _bindEvents: function() {
//        var self = this, button = this.button; function clickHandler(e) { self[self._isOpen ? 'close' : 'open'](); return false; }
//        button.find('span').bind('click.multiselect', clickHandler); button.bind({ click: clickHandler, keypress: function(e) { switch (e.keyCode) { case 27: case 38: case 37: self.close(); break; case 39: case 40: self.open(); break; } }, mouseenter: function() { if (!button.hasClass('ui-state-disabled')) { $(this).addClass('ui-state-hover'); } }, mouseleave: function() { $(this).removeClass('ui-state-hover'); }, focus: function() { if (!button.hasClass('ui-state-disabled')) { $(this).addClass('ui-state-focus'); } }, blur: function() { $(this).removeClass('ui-state-focus'); } }); this.menu.find('div.ui-multiselect-header a').bind('click.multiselect', function(e) {
//            if ($(this).hasClass('ui-multiselect-close')) { self.close(); } else { self[$(this).hasClass('ui-multiselect-all') ? 'checkAll' : 'uncheckAll'](); }
//            e.preventDefault();
//        }).end().find('li.ui-multiselect-optgroup-label a').bind('click.multiselect', function(e) { var $this = $(this), $inputs = $this.parent().nextUntil('li.ui-multiselect-optgroup-label').find('input:visible'); self._toggleChecked($inputs.filter(':checked').length !== $inputs.length, $inputs); self._trigger('optgrouptoggle', e, { inputs: $inputs.get(), label: $this.parent().text(), checked: $inputs[0].checked }); e.preventDefault(); }).end().delegate('label', 'mouseenter', function() { if (!$(this).hasClass('ui-state-disabled')) { self.labels.removeClass('ui-state-hover'); $(this).addClass('ui-state-hover').find('input').focus(); } }).delegate('label', 'keydown', function(e) { switch (e.keyCode) { case 9: case 27: self.close(); break; case 38: case 40: case 37: case 39: self._traverse(e.keyCode, this); break; case 13: e.preventDefault(); $(this).find('input').trigger('click'); break; } }).delegate(':checkbox, :radio', 'click', function(e) {
//            var $this = $(this), val = this.value, checked = this.checked; if ($this.is(':disabled') || self._trigger('click', e, { value: this.value, text: this.title, checked: checked }) === false) { e.preventDefault(); return; }
//            if (!self.options.multiple) { self.radios.not(this).removeAttr('checked'); }
//            self.element.find('option').filter(function() { return this.value === val; }).attr('selected', (checked ? 'selected' : '')); self.update(!e.originalEvent ? checked ? -1 : 1 : 0);
//        }); $(document).bind('click.multiselect', function(e) { var $target = $(e.target); if (self._isOpen && !$target.closest('div.ui-multiselect-menu').length && !$target.is('button.ui-multiselect')) { self.close(); } }); this.element.closest('form').bind('reset', function() { setTimeout($.proxy(self, 'update'), 1); });
//    }, _setButtonWidth: function() {
//        var width = this.element.outerWidth(), o = this.options; if (/\d/.test(o.minWidth) && width < o.minWidth) { width = o.minWidth; }
//        this.button.width(width);
//    }, _setMenuWidth: function() {
//        var m = this.menu, width = this.button.outerWidth()
//- parseInt(m.css('padding-left'), 10)
//- parseInt(m.css('padding-right'), 10)
//- parseInt(m.css('border-right-width'), 10)
//- parseInt(m.css('border-left-width'), 10); m.width(width || this.button.outerWidth());
//    }, _traverse: function(keycode, start) { var $start = $(start), moveToLast = (keycode === 38 || keycode === 37) ? true : false, $next = $start.parent()[moveToLast ? 'prevAll' : 'nextAll']('li:not(.ui-multiselect-disabled, .ui-multiselect-optgroup-label)')[moveToLast ? 'last' : 'first'](); if (!$next.length) { var $container = this.menu.find('ul:last'); this.menu.find('label')[moveToLast ? 'last' : 'first']().trigger('mouseover'); $container.scrollTop(moveToLast ? $container.height() : 0); } else { $next.find('label').trigger('mouseover'); } }, _toggleChecked: function(flag, group) {
//        var $inputs = (group && group.length) ? group : this.labels.find('input'); if (!this.options.multiple && flag) { $inputs = $inputs.eq(0); }
//        $inputs.not(':disabled').attr('checked', (flag ? 'checked' : '')); this.update(); this.element.find('option').not(':disabled').attr('selected', (flag ? 'selected' : ''));
//    }, _toggleDisabled: function(flag) { this.button.attr('disabled', (flag ? 'disabled' : ''))[flag ? 'addClass' : 'removeClass']('ui-state-disabled'); this.menu.find('input').attr('disabled', (flag ? 'disabled' : '')).parent()[flag ? 'addClass' : 'removeClass']('ui-state-disabled'); this.element.attr('disabled', (flag ? 'disabled' : '')); }, update: function(offset) {
//        var o = this.options, $inputs = this.labels.find('input'), $checked = $inputs.filter(':checked'), numChecked = $checked.length, value; if (numChecked === 0) { value = o.noneSelectedText; } else { if ($.isFunction(o.selectedText)) { value = o.selectedText.call(this, numChecked, $inputs.length, $checked.get()); } else if (/\d/.test(o.selectedList) && o.selectedList > 0 && numChecked <= o.selectedList) { value = $checked.map(function() { return this.title; }).get().join(', '); } else { value = o.selectedText.replace('#', numChecked).replace('#', $inputs.length); } }
//        this.buttonlabel.html(value); return value;
//    }, open: function(e) {
//        var self = this, button = this.button, menu = this.menu, speed = this.speed, o = this.options; if (this._trigger('beforeopen') === false || button.hasClass('ui-state-disabled') || this._isOpen) { return; }
//        $(':ech-multiselect').not(this.element).each(function() { var $this = $(this); if ($this.multiselect('isOpen')) { $this.multiselect('close'); } }); var $container = menu.find('ul:last'), effect = o.show, pos = button.position(); if ($.isArray(o.show)) { effect = o.show[0]; speed = o.show[1] || self.speed; }
//        $container.scrollTop(0).height(o.height); if ($.ui.position && !$.isEmptyObject(o.position)) { o.position.of = o.position.of || button; menu.show().position(o.position).hide().show(effect, speed); } else { menu.css({ top: pos.top + button.outerHeight(), left: pos.left }).show(effect, speed); }
//        this.labels.eq(0).trigger('mouseover').trigger('mouseenter').find('input').trigger('focus'); button.addClass('ui-state-active'); this._isOpen = true; this._trigger('open');
//    }, close: function() {
//        if (this._trigger('beforeclose') === false) { return; }
//        var self = this, o = this.options, effect = o.hide, speed = this.speed; if ($.isArray(o.hide)) { effect = o.hide[0]; speed = o.hide[1] || this.speed; }
//        this.menu.hide(effect, speed); this.button.removeClass('ui-state-active').trigger('blur').trigger('mouseleave'); this._trigger('close'); this._isOpen = false;
//    }, enable: function() { this._toggleDisabled(false); }, disable: function() { this._toggleDisabled(true); }, checkAll: function(e) { this._toggleChecked(true); this._trigger('checkAll'); }, uncheckAll: function() { this._toggleChecked(false); this._trigger('uncheckAll'); }, getChecked: function() { return this.menu.find('input').filter(':checked'); }, destroy: function() { $.Widget.prototype.destroy.call(this); this.button.remove(); this.menu.remove(); this.element.show(); return this; }, isOpen: function() { return this._isOpen; }, widget: function() { return this.menu; }, _setOption: function(key, value) {
//        var menu = this.menu; switch (key) { case "header": menu.find('div.ui-multiselect-header')[value ? 'show' : 'hide'](); break; case "checkAllText": menu.find('a.ui-multiselect-all span').eq(-1).text(value); break; case "uncheckAllText": menu.find('a.ui-multiselect-none span').eq(-1).text(value); break; case "height": menu.find('ul:last').height(parseInt(value, 10)); break; case "minWidth": this.options[key] = parseInt(value, 10); this._setButtonWidth(); this._setMenuWidth(); break; case "selectedText": case "selectedList": case "noneSelectedText": this.options[key] = value; this.update(); break; case "classes": menu.add(this.button).removeClass(this.options.classes).addClass(value); break; }
//        $.Widget.prototype._setOption.apply(this, arguments);
//    } 
//    });
//})(jQuery);


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


///*
//* jQuery autoResize (textarea auto-resizer)
//* @copyright James Padolsey http://james.padolsey.com
//* @version 1.04
//*/

//(function(a) { a.fn.autoResize = function(j) { var b = a.extend({ onResize: function() { }, animate: true, animateDuration: 150, animateCallback: function() { }, extraSpace: 20, limit: 1000 }, j); this.filter('textarea').each(function() { var c = a(this).css({ resize: 'none', 'overflow-y': 'hidden' }), k = c.height(), f = (function() { var l = ['height', 'width', 'lineHeight', 'textDecoration', 'letterSpacing'], h = {}; a.each(l, function(d, e) { h[e] = c.css(e) }); return c.clone().removeAttr('id').removeAttr('name').css({ position: 'absolute', top: 0, left: -9999 }).css(h).attr('tabIndex', '-1').insertBefore(c) })(), i = null, g = function() { f.height(0).val(a(this).val()).scrollTop(10000); var d = Math.max(f.scrollTop(), k) + b.extraSpace, e = a(this).add(f); if (i === d) { return } i = d; if (d >= b.limit) { a(this).css('overflow-y', ''); return } b.onResize.call(this); b.animate && c.css('display') === 'block' ? e.stop().animate({ height: d }, b.animateDuration, b.animateCallback) : e.height(d) }; c.unbind('.dynSiz').bind('keyup.dynSiz', g).bind('keydown.dynSiz', g).bind('change.dynSiz', g) }); return this } })(jQuery);

//(function($) {
//    $.fn.numeric = function(decimal, callback)
//    { decimal = (decimal === false) ? "" : decimal || "."; callback = typeof callback == "function" ? callback : function() { }; return this.data("numeric.decimal", decimal).data("numeric.callback", callback).keypress($.fn.numeric.keypress).blur($.fn.numeric.blur); }
//    $.fn.numeric.keypress = function(e) {
//        var decimal = $.data(this, "numeric.decimal"); var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0; if (key == 13 && this.nodeName.toLowerCase() == "input")
//        { return true; }
//        else if (key == 13)
//        { return false; }
//        var allow = false; if ((e.ctrlKey && key == 97) || (e.ctrlKey && key == 65)) return true; if ((e.ctrlKey && key == 120) || (e.ctrlKey && key == 88)) return true; if ((e.ctrlKey && key == 99) || (e.ctrlKey && key == 67)) return true; if ((e.ctrlKey && key == 122) || (e.ctrlKey && key == 90)) return true; if ((e.ctrlKey && key == 118) || (e.ctrlKey && key == 86) || (e.shiftKey && key == 45)) return true; if (key < 48 || key > 57) {
//            if (key == 45 && this.value.length == 0) return true; if (decimal && key == decimal.charCodeAt(0) && this.value.indexOf(decimal) != -1)
//            { allow = false; }
//            if (key != 8 && key != 9 && key != 13 && key != 35 && key != 36 && key != 37 && key != 39 && key != 46)
//            { allow = false; }
//            else {
//                if (typeof e.charCode != "undefined") {
//                    if (e.keyCode == e.which && e.which != 0)
//                    { allow = true; if (e.which == 46) allow = false; }
//                    else if (e.keyCode != 0 && e.charCode == 0 && e.which == 0)
//                    { allow = true; } 
//                } 
//            }
//            if (decimal && key == decimal.charCodeAt(0)) {
//                if (this.value.indexOf(decimal) == -1)
//                { allow = true; }
//                else
//                { allow = false; } 
//            } 
//        }
//        else
//        { allow = true; }
//        return allow;
//    }
//    $.fn.numeric.blur = function() {
//        var decimal = $.data(this, "numeric.decimal"); var callback = $.data(this, "numeric.callback"); var val = $(this).val(); if (val != "") {
//            var re = new RegExp("^\\d+$|\\d*" + decimal + "\\d+"); if (!re.exec(val))
//            { callback.apply(this); } 
//        } 
//    }
//    $.fn.removeNumeric = function()
//    { return this.data("numeric.decimal", null).data("numeric.callback", null).unbind("keypress", $.fn.numeric.keypress).unbind("blur", $.fn.numeric.blur); } 
//})(jQuery);