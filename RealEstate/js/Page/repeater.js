function BindCtrl() {
    $('.step').click(function () { page = parseInt($(this).attr('name')); $('#btnPageClick').click(); });
    $('th[column]').each(
        function (index) {
            $(this).css('cursor', 'pointer');
            $(this).click(function () {
                SortColumn = $(this).attr('column');
                if ($(this).attr('class') == "ASC" || $(this).attr('class') == '')
                    SortType = 'DESC';
                else
                    SortType = 'ASC';
                $(this).attr('class', SortType);
                $('#btnPageClick').click();
            });
        }
    );
    $('div[column]').each(
        function (index) {
            $(this).css('cursor', 'pointer');
            $(this).click(function () {
                SortColumn = $(this).attr('column');
                if ($(this).attr('class') == "ASC" || $(this).attr('class') == '')
                    SortType = 'DESC';
                else
                    SortType = 'ASC';
                $(this).attr('class', SortType);
                $('#btnPageClick').click();
            });
        }
    );

    GetSelRecord();
    SetSortCol();
    $('.ASC').attr('title', 'Click here for descending');
    $('.DESC').attr('title', 'Click here for ascending');
    BindDrop();
    $("#StrRecPerPage").html($('#hdnRecPerPage').val());
}

function GetSelRecord() {
    var total = 0;
    var selID = '0';
    $("input[type='checkbox']").each(
        function (index) {
            if ($(this).attr('id') != 'cbxAll') {

                if ($(this).prop('checked')) {
                    selID = selID + ',' + $(this).attr('name');
                    total = total + 1;
                }
                $(this).trigger('change');/*this is for ez checkbox*/
            }
        }
    );
    // alert(selID);
    $('#hdnID').attr('value', selID);
    $('#divRecSelect').html(total + ' Record(s) selected');

}
function SetCbxBox(ctrl) {
    if (!ctrl.checked) {
        $('#cbxAll').prop('checked', false);
    }
    else {

        var cnt = 0;
        $("input[type='checkbox']").each(
            function (index) {
                if ($(this).attr('id') != 'cbxAll') {
                    if (!$(this).prop('checked')) {
                        cnt = 1;
                    }
                }
            });
        $('#cbxAll').prop('checked', cnt == 0 ? true : false);
    }
    GetSelRecord();
}
function CbxAll(ctrl) {

    $('input[type=\'checkbox\']').prop('checked', ctrl.checked);



}
function ShowSearch() {
    if ($('#divSearch').attr('class') == 'hide-search button') {
        $('#divSearch').attr('class', 'show-search button');
        //$('#divSearch').html('Show Search');
        $('#divSearch').html('<span>Show Search&nbsp;<img src="images/show-search.png" width="12px" /></span>');
        $('#trSearch').slideUp('slow');
        ClearControls();
    }
    else {
        $('#divSearch').attr('class', 'hide-search button');
        //$('#divSearch').html('Hide Search');
        $('#divSearch').html('<span>Hide Search&nbsp;<img src="images/search-up-arrow.png" width="12px" /></span>');
        $('#trSearch').slideDown('slow');
    }
}
function SubmitForm() {

    var tmpstr = '';
    //$.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');

    if (PageUrl.indexOf('?') == -1)
        tmpstr = '?';
    else
        tmpstr = '&';

    $.ajax(
        {
            url: PageUrl + tmpstr + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
            success: function (response) {
                $('#' + RspCtrl).html(response);
                //$.hideprogress();
                $('.custom-check input').ezMark();
            }
        });

    return false;
}
function SubmitForm1() {

    var tmpstr = '';
    if (PageUrl.indexOf('?') == -1)
        tmpstr = '?';
    else
        tmpstr = '&';

    $.ajax(
        {
            url: PageUrl + tmpstr + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
            success: function (response) { $('#' + RspCtrl).html(response); $('.custom-check input').ezMark(); }
        });

    return false;
}

function PerformAction(Ctrl) {
    //alert($(Ctrl).val());
    if ($(Ctrl).val() != 'Action') {
        var Type = $(Ctrl).val();
        Operation(divMsg, Type);
        $(Ctrl).val('Action');
    }
}

function Operation(ctrlMsg, type) {
    if ($('#hdnID').val() == '0' || $('#hdnID').val().length == 0) {
        DisplMsg(ctrlMsg, 'Please select at least one record.', 'alert-message error');
        ScrollTop();
    }
    else {
        var typename = '';
        if (type == 'remove') {
            typename = "delete";
        }
        else if (type == 'active') {
            typename = "activate";
        }
        else if (type == 'inactive') {
            typename = "deactivate";
        }
        var messagetitle = '';
        messagetitle = "Are you sure you want to " + typename + " selected record(s)?"

        if (confirm(messagetitle)) {
            DisplMsg(ctrlMsg, '', '');
           // $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
            if (PageUrl.indexOf('?') == -1)
                PageUrl = PageUrl + '?';
            else
                PageUrl = PageUrl + '&';
            $.ajax(
                {
                    url: PageUrl + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&type=' + type + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
                    success: function (response) {
                        $('#' + RspCtrl).html(response);
                        //$.hideprogress(); $('.custom-check input').ezMark(); 
                        ScrollTop();
                    }
                });
        }
    }
}

function GridOperation(ctrlMsg, type, selID) {
    $('[data-toggle="tooltip"]').tooltip("hide");
    var typename = '';
    if (type == 'remove') {
        typename = "delete";
    }
    else if (type == 'active') {
        typename = "activate";
    }
    else if (type == 'inactive') {
        typename = "deactivate";
    }
    var messagetitle = '';
    messagetitle = "Are you sure you want to " + typename + " selected record(s)?"

    if (confirm(messagetitle)) {
        //  DisplMsg(ctrlMsg, '', '');
        //$.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
        if (PageUrl.indexOf('?') == -1)
            PageUrl = PageUrl + '?';
        else
            PageUrl = PageUrl + '&';

        $('#hdnID').attr('value', selID);

        $.ajax(
            {
                url: PageUrl + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&type=' + type + '&page=' + page,
                data: $('#' + FormName).serialize(),
                type: 'post',
                success: function (response) {
                    ProcessData(selID, type);
                    DisplMsg(divMsg, response, 'alert-message success');

                   // $.hideprogress();
                    ScrollTop();
                }
            });
    }
}

function SetSortCol() {
    $('th[column]').each(
        function (index) {
            if ($(this).attr('column') == SortColumn) {
                $(this).attr('class', SortType);
            }
            else {
                $(this).attr('class', 'ASC');
            }
        }
    );
    $('div[column]').each(
        function (index) {
            if ($(this).attr('column') == SortColumn) {
                $(this).attr('class', SortType);
            }
            else {
                $(this).attr('class', 'ASC');
            }
        }
    );
}
$(document).ready(
    function () { BindCtrl(); }
);
function BindDrop() {
    //$('.myrs-pagination-maxvalue').show();
    //$('.myrs-pagination-maxvalue-toggler').click(function () { $('.myrs-pagination-maxvalue-list').fadeIn('fast'); return false; });
    //$('.myrs-pagination-maxvalue').hoverIntent(function () { return false; }, function () { $('.myrs-pagination-maxvalue-list').fadeOut('fast'); });
    //$('div.myrs-pagination-maxvalue-list  ul li a').click(function (evt) {
    //    $('#hdnRecPerPage').attr('value', parseInt($(this).children().html()));
    //    $('#btnPageClick').click();
    //});
}
function ValidateCtrl() {

    var i = 0;
    var cnt = 0;
    var strMsg = '';
    var ctrlArr = SearchControl.split('@');
    for (i = 0; i < ctrlArr.length; i++) {

        var ctrl = ctrlArr[i].split(':');

        if (jQuery.trim($('#' + ctrl[0]).val()) == ctrl[1]) {
            //            strMsg=strMsg + '<br/> - ' + ctrl[2];
            cnt++;
        }
    }
    if (cnt == ctrlArr.length) {
        DisplMsg(divMsg, 'Please search with at least one criteria.', 'alert-message error');
        ScrollTop();
        return false;
    }
}
function ClearControls() {
    var i = 0;
    var strMsg = '';
    var ctrlArr = SearchControl.split('@');
    for (i = 0; i < ctrlArr.length; i++) {
        var ctrl = ctrlArr[i].split(':');
        $('#' + ctrl[0]).val(ctrl[1]);
    }
}

function ChangeRecord(ctrl, process, ID) {
    var type = '';
    $('#chk' + ID).prop('checked', true);

    GetSelRecord();

    if (process == 'status') {
        if ($(ctrl).attr('title') == 'Activate') {
            type = 'inactive';
        }
        else {
            type = 'active';
        }
    }

    if (process == 'remove') {
        type = 'remove';
        if (!confirm("Are you sure you want to delete selected record?")) {
            $('#chk' + ID).prop('checked', false);
            return;
        }
    }

    //$.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
    if (PageUrl.indexOf('?') == -1)
        PageUrl = PageUrl + '?';
    else
        PageUrl = PageUrl + '&';
    $.ajax(
        {
            url: PageUrl + 'sorttype=' + SortType + '&sortcol=' + SortColumn + '&type=' + type + '&page=' + page, data: $('#' + FormName).serialize(), type: 'post',
            success: function (response) {
                $('#' + RspCtrl).html(response);
                //$.hideprogress();
                $('.custom-check input').ezMark(); ScrollTop();
            }
        });
}

function ProcessData(strID, type) {
    if (strID.toString() != '') {        
        var splitid = strID.toString().split(",");
        for (var i = 0; i < splitid.length; i++) {
            if (jQuery.trim(type) == 'active') {
                $('#lnk' + splitid[i]).html('<span class="list-icon"><span class="oi oi-circle-check text-success"></span></span>');
                $('#lnk' + splitid[i]).attr("onclick", "GridOperation('" + divMsg + "', 'inactive','" + splitid[i] + "')");
            }
            if (jQuery.trim(type) == 'inactive') {
                $('#lnk' + splitid[i]).html('<span class="list-icon"><span class="oi oi-circle-x text-red"></span></span>');
                $('#lnk' + splitid[i]).attr("onclick", "GridOperation('" + divMsg + "', 'active','" + splitid[i] + "')");                
            }
            if (jQuery.trim(type) == 'remove') {                                
                $('.lnkdelete' + splitid[i]).click()[0];
            }
        }
    }
}
function DisplMsg(Ctrl, ErrMsg, Msgclass) {    
    $('#' + Ctrl).show();
    $('#' + Ctrl).html(ErrMsg);
    $('#' + Ctrl).attr('class', Msgclass);
    var fName = 'HideMSG('+Ctrl+')';
    window.setTimeout("HideMSG('" + Ctrl +"')", 6000);
}
function HideMSG(Ctrl) {   
    $('#' + Ctrl).hide();
    $('#' + Ctrl).html('');
}