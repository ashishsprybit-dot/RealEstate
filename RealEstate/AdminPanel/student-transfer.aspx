<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="student-transfer.aspx.cs" EnableEventValidation="false"
    Inherits="AdminPanel_Student_Transfer" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <link rel="stylesheet" href="<%=Config.VirtualDir %>assets/vendor/select2/css/select2.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">Student Transfer</span></h1>
    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <%-- <form id="form_Admin" action="category-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data">--%>
                <form id="form_sample_Forgot">
                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>


                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="<%=ddlFromSchool.ClientID%>">From School<abbr title="Required">*</abbr></label>
                                        <select id="ddlFromSchool" name="ddlFromSchool" runat="server" class="form-control ddlFromSchool required" rel="category" onchange="BindStudents()"></select>
                                    </div>

                                </div>
                                <div class="col-lg-6 divStudents">
                                    <div class="form-group" id="divStudents" runat="server">
                                        <label for="<%=ddlStudents.ClientID%>">Students<abbr title="Required">*</abbr></label>
                                        <select id="ddlStudents" name="ddlStudents" runat="server" class="form-control ddlStudents required" rel="category"></select>
                                    </div>

                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="<%=ddlToSchool.ClientID%>">To School<abbr title="Required">*</abbr></label>
                                        <select id="ddlToSchool" name="ddlToSchool" runat="server" class="form-control ddlToSchool required" rel="category"></select>
                                    </div>

                                </div>                               

                            </div>
                            <div class="col-lg-1"></div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                    </div>
                    <div class="card-body border-top">
                        <input class="btn btn-primary btnSave" runat="server" id="btnSave" type="submit" value="Save Information" name="btnSave" />
                        <button type="button" class="btn btn-default" onclick="Clear()">Clear</button>
                    </div>
                    <%--   <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>--%>
                </form>
            </div>

        </div>
        <!-- /.card-deck-xl -->

    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="<%=Config.VirtualDir %>validation-lib/jquery.validate.min.js"></script>
    <script src="<%=Config.VirtualDir %>validation-lib/form-validation.js"></script>
    <script src="<%=Config.VirtualDir %>assets/vendor/select2/js/select2.min.js"></script>
    <script type="text/javascript">
        var divMsg = '<%= divMsg.ClientID %>';

        $(document).ready(function () {
            FormValidation.init();
            $('.ddlStudents, .ddlFromSchool, .ddlToSchool').select2({
                selectOnClose: true
            });
        });

        function BindStudents() {
            if (jQuery.trim($('.ddlFromSchool').val()) != '') {
                $.ajax({
                    url: 'student-transfer.aspx',
                    type: 'POST',
                    data: { 'BindStudents': 'Y', 'SchoolID': jQuery.trim($('.ddlFromSchool').val()) },
                    success: function (resp) {
                        $('.divStudents').html(resp);
                        $('.ddlStudents').select2({
                            selectOnClose: true
                        });
                    }
                });
            } else {
                $('.ddlStudents').empty();
                $('.ddlStudents').select2({
                    selectOnClose: true
                });
            }
        }

       

        function Clear() {
            $('.ddlFromSchool').val('');
            $('.ddlToSchool').val('');
            $('.ddlStudents').empty();
            $('.ddlStudents, .ddlFromSchool, .ddlToSchool').select2({
                selectOnClose: true
            });
        }

        function Validateforgateform() {

            if (jQuery.trim($('.ddlFromSchool').val()) == jQuery.trim($('.ddlToSchool').val())) {
                DisplMsg(divMsg, 'From School & To School should not be same.', 'alert-message error');
            }
            else {
                $('.btnSave').attr('disabled', true);
                $.ajax({
                    url: 'student-transfer.aspx',
                    type: 'POST',
                    data: { 'StudentTransfer': 'Y', 'FromSchoolID': jQuery.trim($('.ddlFromSchool').val()), 'ToSchoolID': jQuery.trim($('.ddlToSchool').val()), 'StudentID': jQuery.trim($('.ddlStudents').val()) },
                    success: function (resp) {
                        $('.btnSave').attr('disabled', false);
                        if (resp == 'Success') {
                            DisplMsg(divMsg, 'Student transferred successfully.', 'alert-message success');
                        }
                        else {
                             DisplMsg(divMsg, resp, 'alert-message error');
                        }
                    }
                });
            }
        }
    </script>
</asp:Content>
