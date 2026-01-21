<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="school-configuration.aspx.cs" Inherits="school_configuration" %>


<%@ Import Namespace="Utility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style type="text/css">
        .day-block {
            display: inline-block;
            margin: 3px 12px 3px -6px;
            background-color: #E6E8ED !important;
            border-color: #E6E8ED !important;
            padding: 0 5px;
            border-radius: 4px;
            cursor: default;
            float: left;
            list-style: none;
            box-sizing: border-box;
        }

        .selection__choice__remove {
            margin-right: auto;
            margin-left: .25rem;
            position: relative;
            top: 6px;
            width: 1rem;
            height: 1rem;
            font-size: 1rem;
            line-height: 1rem !important;
            float: right;
            color: #fff;
            background-color: #a6abbd;
            text-align: center;
            border-radius: .5rem;
            cursor: pointer;
        }

        .borderbox {
            border: 1px solid #ebebeb;
            display: table;
            width: 100%;
            padding: 4px 5px 4px 10px;
            margin-top: 5px;
            border-radius: 3px;
        }

            .borderbox span {
                line-height: 30px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">School Configuration</span></h1>
        <%-- <p class="text-muted">These settings can be set differently for each school. Please make sure the percentage box is set to a figure that has been decided by the appropriate Teaching & Learning Coordinators, as this may vary for each school. 
The notification days sets the amount of days before the end of the semester in which notifications will be emailed to coordinators listing classes with incomplete results.
</p>--%>
    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <form id="form_school_configuration" name="form_school_configuration" action="school-configuration.aspx/sc" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data" onsubmit="return ValidateForm();">

                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="row">
                                    <div class="col-md-10 col-lg-10">
                                        <label for="tbxDays">Notification Days<abbr title="Required">*</abbr></label><br />
                                        <table width="100%">
                                            <tr>
                                                <td width="150">
                                                    <input type="text" class="form-control numeric" id="tbxDays" name="tbxDays" maxlength="3" style="" />
                                                    <input type="hidden" id="hdnDays" name="hdnDays" class="hdnDays required form-control" runat="server" value="" /></td>
                                                <td width="10"></td>
                                                <td><a href="javascript:;" onclick="addDaysToDivisionSection()" name="add">
                                                    <i class="fa fa-plus-square fa-lg" aria-hidden="true"></i></a></td>
                                            </tr>
                                        </table>


                                    </div>

                                </div>
                                <div class="clearfix"></div>

                                <div class="row">
                                    <div class="col-md-12 col-lg-12">
                                        <div id="divDays" class="borderbox select2 select2-container select2-container--default select2-container--above" style="display: none;">
                                            <span>&nbsp;</span>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="card-body border-top">
                        <input class="btn btn-primary btnSaveNotification" runat="server" id="btnSave" type="button" value="Save Information" name="btnSave" onclick="SaveNotificationDays()" />
                        <button type="button" class="btn btn-default" onclick="window.location='school-configuration.aspx';">Cancel</button>
                    </div>
                    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                </form>
            </div>
        </div>
        <!-- /.card-deck-xl -->

    </div>
    <div class="clearfix"></div>
    <br />
    <br />
    <div id="divTemp" runat="server" visible="false">
        <header class="page-title-bar">
            <h1 class="page-title"><span id="Span1" runat="server">Semester Configuration</span></h1>
            <p class="text-muted">The school administrator must set the correct semester dates here. This allows the notifications to be sent to coordinators alerting them to any classes that have not been submitted. It will also lock changes to the semester after the end date.</p>
        </header>
        <div class="page-section" data-select2-id="166">
            <!-- .card-deck-xl -->
            <div class="card-deck-xl">
                <!-- .card -->
                <div class="card card-fluid" data-select2-id="165">
                    <!-- .card-body -->

                    <form id="form_school_Semester_configuration" name="form_school_Semester_configuration" action="school-configuration.aspx/ssc" method="post" target="ifrmForm2"
                        class="nice custom formlayout need-to-validate" enctype="multipart/form-data" onsubmit="return AddSemesterDetails();">
                        <div class="card-body" data-select2-id="164">
                            <div id="divMsgSemester" runat="server" class="alert alert-danger hide" style="display: none;"></div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>Sem-1 Start Date<abbr title="Required">*</abbr></label>
                                        <input id="tbxSem1StartDate" type="text" runat="server" style="width: 160px" class="form-control" data-toggle="flatpickr" placeholder="DD/MM/YYYY" />
                                        <input type="hidden" id="hdnSemesterID" name="hdnSemesterID" class="hdnSemesterID required form-control" runat="server" value="0" />
                                        <input type="hidden" id="hdnID" name="hdnID" />
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>End Date<abbr title="Required">*</abbr></label>
                                        <input id="tbxSem1EndDate" type="text" runat="server" style="width: 160px" class="form-control" data-toggle="flatpickr" placeholder="DD/MM/YYYY" />
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    &nbsp;
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>Sem-2 Start Date<abbr title="Required">*</abbr></label>
                                        <input id="tbxSem2StartDate" type="text" runat="server" style="width: 160px" class="form-control" data-toggle="flatpickr" placeholder="DD/MM/YYYY" />
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>End Date<abbr title="Required">*</abbr></label>
                                        <input id="tbxSem2EndDate" type="text" runat="server" style="width: 160px" class="form-control" data-toggle="flatpickr" placeholder="DD/MM/YYYY" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card-body" style="margin-top: 13px">
                                        <input class="btn btn-primary" runat="server" id="SubmitFormSemester" type="submit" value="Save" name="btnSaveSemester" />
                                        <button type="button" class="btn btn-default" onclick="window.location='school-configuration.aspx';">Cancel</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <iframe id="ifrmForm2" name="ifrmForm2" style="display: none; width: 0px; height: 0px;"></iframe>
                    </form>
                </div>
            </div>
            <div class="clearfix"></div>


            <div class="page-section">
                <!-- .card -->
                <div class="card card-fluid">
                    <!-- .card-body -->
                    <div class="card-body">
                        <!-- .form-group -->
                        <div class="form-group">
                            <!-- .input-group -->
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend hide">
                                    <select id="filterBy" class="custom-select">
                                        <option value='' selected>Filter By </option>
                                    </select>
                                </div>
                                <!-- .input-group-prepend -->

                                <!-- /.input-group-prepend -->
                                <!-- .input-group -->
                                <div class="input-group has-clearable">
                                    <button id="clear-search" type="button" class="close" aria-label="Close"><span aria-hidden="true"><i class="fa fa-times-circle"></i></span></button>
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><span class="oi oi-magnifying-glass"></span></span>
                                    </div>
                                    <input id="table-search" type="text" class="form-control" placeholder="Search Semester">
                                </div>
                                <!-- /.input-group -->
                            </div>
                            <!-- /.input-group -->
                        </div>
                        <!-- /.form-group -->

                        <table id="myTable" class="table">
                            <!-- thead -->
                            <thead>
                                <tr>
                                    <th>Sem-1 Start Date</th>
                                    <th>Sem-1 End Date</th>
                                    <th>Sem-2 Start Date</th>
                                    <th>Sem-2 End Date</th>
                                    <th width="70" class="text-center">Edit </th>
                                    <th width="70" class="text-center">Delete </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>


                    </div>
                    <!-- /.card-body -->
                </div>
                <!-- /.card -->
            </div>





            <div class="row">
                <div class="col-lg-12">
                    <!-- .table -->

                    <!-- /.table -->
                </div>
            </div>
            <!-- /.card-deck-xl -->
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <%--<script src="<%=Config.WebSiteUrl %>assets/vendor/flatpickr/flatpickr.min.js"></script>
    <script src="<%=Config.WebSiteUrl %>validation-lib/jquery.validate.min.js"></script>
    <script src="<%=Config.WebSiteUrl %>validation-lib/form-validation.js"></script>--%>
    <script type="text/javascript">
        var page = 1;
        var SortColumn = 'ID';
        var SortType = 'ASC';
        var PageUrl = 'school-configuration.aspx';
        var FormName = 'form_school_Semester_configuration';
        var RspCtrl = 'DivRender';
        var SearchControl = 'tbxFname::FirstName@tbxLname::LastName@tbxUname::Username@tbxEmail::EmailId';
        var userdata = '';
    </script>
    <%--<script src="<%=Config.WebSiteUrl %>assets/javascript/pages/dataTables.bootstrap.js"></script>--%>
    <%--<script src="<%=Config.VirtualDir %>js/Page/datatables-school-configuration.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").keydown(function (e) {
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    return;
                }

                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
            if ($('#<%=hdnDays.ClientID %>').val() != '') {
                var DayList = [];
                DayList = $('#<%=hdnDays.ClientID %>').val().split(",");
                $.each(DayList, function (index, obj) {
                    var employeeBlock = '<div class="day-block divSelectedDays"><span>'
                        + jQuery.trim(obj.replace('selection__choice__remove', '')) + '</span> <span class="selection__choice__remove ' + jQuery.trim(obj.replace('selection__choice__remove', '')) + '" onclick="removeDayElement(this)" >×</span></div>';

                    $('#divDays').append(employeeBlock);
                });
                $('#divDays').show();
            }
        });

        var divMsg = '<%= divMsg.ClientID %>';
        var divMsgSemester = '<%= divMsgSemester.ClientID %>';


        function addDaysToDivisionSection() {

            if ($('.divSelectedDays .selection__choice__remove').hasClass($('#tbxDays').val()) && $('#tbxDays').val() != '') {
                //var HeaderText = 'Please correct the following error(s):<br>';
                var HeaderText = '';
                ErrMsg = $('#tbxDays').val() + " is already selected.";
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
                return false;
            }
            else {
                if ($('#tbxDays').val() != '') {
                    var DayBlock = '<div class="day-block divSelectedDays"><span>'
                        + $('#tbxDays').val() + '</span><span class="selection__choice__remove ' + $('#tbxDays').val() + '" onclick="removeDayElement(this)" >×</span></div>';

                    $('#divDays').append(DayBlock);
                    $('#tbxDays').val('');
                    $('#divDays').show();

                    var selectedDay = [];
                    $('.divSelectedDays .selection__choice__remove').each(function () {
                        var val = $(this).attr('class').replace('selection__choice__remove', '');
                        val = jQuery.trim(val.replace('selection__choice__remove', ''));
                        selectedDay.push(val);
                    });

                    $('#<%=hdnDays.ClientID %>').val(selectedDay);
                }
                else {
                    //var HeaderText = 'Please correct the following error(s):<br>';
                    var HeaderText = '';
                    ErrMsg = "Please enter valid notification days.";
                    DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                    ScrollTop();
                    return false;
                }
            }
        }

        function removeDayElement(element) {
            $(element).parent().remove();

            if ($('.divSelectedDays').length == 0) {
                $('#divDays').hide();
            }

            var selectedDay = [];
            $('.divSelectedDays .selection__choice__remove').each(function () {
                selectedDay.push(jQuery.trim($(this).attr('class').replace('selection__choice__remove', '')));
            });

            $('#<%=hdnDays.ClientID %>').val(selectedDay);
        }

        function ValidateForm() {
            var ErrMsg = '';
            var hdnDays = '<%= hdnDays.ClientID %>';
            if (document.getElementById(hdnDays).value == '') {
                ErrMsg = ErrMsg + '<br> - ' + 'Please enter Notification Days.';
            }

            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
                return false;
            }
            else {
                $('#<%=divMsg.ClientID %>').hide();
                return true;
            }
            return true;
        }

        function AddSemesterDetails() {
            var ErrMsg = '';
            var tbxSem1StartDate = $('#<%= tbxSem1StartDate.ClientID %>').val();
            var tbxSem1EndDate = $('#<%= tbxSem1EndDate.ClientID %>').val();
            var tbxSem2StartDate = $('#<%= tbxSem2StartDate.ClientID %>').val();
            var tbxSem2EndDate = $('#<%= tbxSem2EndDate.ClientID %>').val();

            if (tbxSem1StartDate == '') {
                ErrMsg = ErrMsg + '<br> - ' + 'Please enter Sem1 Start Date.';
            }
            if (tbxSem1EndDate == '') {
                ErrMsg = ErrMsg + '<br> - ' + 'Please enter Sem1 End Date.';
            }
            if (tbxSem2StartDate == '') {
                ErrMsg = ErrMsg + '<br> - ' + 'Please enter Sem2 Start Date.';
            }
            if (tbxSem2EndDate == '') {
                ErrMsg = ErrMsg + '<br> - ' + 'Please enter Sem2 End Date.';
            }

            if (tbxSem1StartDate != '' && tbxSem1EndDate != '') {
                let Sem1StartDate = StringToDate(tbxSem1StartDate);
                let Sem1EndDate = StringToDate(tbxSem1EndDate);
                if (Sem1StartDate >= Sem1EndDate) {
                    ErrMsg = ErrMsg + '<br> - ' + 'Sem-1 start date should be smaller than the end date';
                }
            }

            if (tbxSem2StartDate != '' && tbxSem2EndDate != '') {
                let Sem2StartDate = StringToDate(tbxSem2StartDate);
                let Sem2EndDate = StringToDate(tbxSem2EndDate);
                if (Sem2StartDate >= Sem2EndDate) {
                    ErrMsg = ErrMsg + '<br> - ' + 'Sem-2 start date should be smaller than the end date';
                }
            }

            if (tbxSem2StartDate != '' && tbxSem1EndDate != '') {
                let Sem2StartDate = StringToDate(tbxSem2StartDate);
                let Sem1EndDate = StringToDate(tbxSem1EndDate);
                if (Sem1EndDate >= Sem2StartDate) {
                    ErrMsg = ErrMsg + '<br> - ' + 'Sem-2 start date should be greater than the sem-1 end date';
                }
            }

            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsgSemester, HeaderText + ErrMsg, 'alert-message error');
                //ScrollTop();
                return false;
            }
            else {
                $('#<%=divMsgSemester.ClientID %>').hide();
                return true;
            }
            return true;
        }

        function StringToDate(strDate) {
            let strDates = strDate.split('/');
            let day = strDates[0];
            let Month = 1;
            let year = strDates[2];
            let MonthName = strDates[1];
            if (MonthName == 'Jan') { Month = 1; }
            else if (MonthName == 'Feb') { Month = 2; }
            else if (MonthName == 'Mar') { Month = 3; }
            else if (MonthName == 'Apr') { Month = 4; }
            else if (MonthName == 'May') { Month = 5; }
            else if (MonthName == 'Jun') { Month = 6; }
            else if (MonthName == 'Jul') { Month = 7; }
            else if (MonthName == 'Aug') { Month = 8; }
            else if (MonthName == 'Sep') { Month = 9; }
            else if (MonthName == 'Oct') { Month = 10; }
            else if (MonthName == 'Nov') { Month = 11; }
            else if (MonthName == 'Dec') { Month = 12; }

            return new Date(year, Month - 1, day);
        }


        function removeSemester(selID) {
            $('[data-toggle="tooltip"]').tooltip("hide");
            let type = 'remove';
            var messagetitle = '';
            messagetitle = "Are you sure you want to delete selected record(s)?"

            if (confirm(messagetitle)) {
                if (PageUrl.indexOf('?') == -1)
                    PageUrl = PageUrl + '?';
                else
                    PageUrl = PageUrl + '&';

                $('#hdnID').attr('value', selID);
                $.ajax(
                    {
                        url: PageUrl + '&type=' + type,
                        data: $('#' + FormName).serialize(),
                        type: 'post',
                        success: function (response) {
                            ProcessData(selID, type);
                            DisplMsg(<%= divMsgSemester.ClientID  %>, response, 'alert-message success');
                        }
                    });
            }
        }

        function editSemester(ID, Sem1StartDate, Sem1EndDate, Sem2StartDate, Sem2EndDate) {
            $('#<%= tbxSem1StartDate.ClientID %>').flatpickr({
                defaultDate: Sem1StartDate
            });
            $('#<%= tbxSem1EndDate.ClientID %>').flatpickr({
                defaultDate: Sem1EndDate
            });
            $('#<%= tbxSem2StartDate.ClientID %>').flatpickr({
                defaultDate: Sem2StartDate
            });
            $('#<%= tbxSem2EndDate.ClientID %>').flatpickr({
                defaultDate: Sem2EndDate
            });
          //  $('#<%= tbxSem1StartDate.ClientID %>').val(Sem1StartDate);
            <%--$('#<%= tbxSem1EndDate.ClientID %>').val(Sem1EndDate);
            $('#<%= tbxSem2StartDate.ClientID %>').val(Sem2StartDate);
            $('#<%= tbxSem2EndDate.ClientID %>').val(Sem2EndDate);--%>
            $('#<%= hdnSemesterID.ClientID %>').val(ID);
        }

        function SaveNotificationDays() {
            $('.btnSaveNotification').attr('disabled', true);
            $.ajax(
                {
                    url: 'school-configuration.aspx',
                    data: { 'savenotificationdays': 'y', 'days': $('#<%=hdnDays.ClientID %>').val() },
                    type: 'post',
                    success: function (response) {
                        if (response == 'success') {
                               DisplMsg(divMsg, 'Configuration has been saved successfully.', 'alert-message success');
                        }
                        $('.btnSaveNotification').attr('disabled', false);
                    }
                });
        }
    </script>
</asp:Content>
