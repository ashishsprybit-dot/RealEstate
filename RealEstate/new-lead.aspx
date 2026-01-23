<%@ Page Title="New Lead" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="new-lead.aspx.cs" Inherits="new_lead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">Add User</span></h1>
        <p class="text-muted">This menu is used to approve Teacher accounts and to assign them classes and permissions. Under Home Groups Access only select the home groups that the teacher needs to be able to make changes to. All users will be able to view scores for ALL students (as long as the View Student Assessments module is assigned). Under Module Access most teachers should only be assigned the Standard Features unless they specifically require the use of any of the Advanced Features.</p>
    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <form id="form_Admin" action="new-lead.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data">

                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Scheme Name <span class="text-danger">*</span></label>

                                    <input type="text" id="txtSchemeName" runat="server" class="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Customer Name <span class="text-danger">*</span></label>
                                    <input type="text" id="txtCustomerName" runat="server" class="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Contact Number <span class="text-danger">*</span></label>
                                    <input type="text"
                                        id="txtContactNumber"
                                        runat="server"
                                        class="form-control"
                                        maxlength="10"
                                        onkeypress="return isNumberKey(event);"
                                        oninput="this.value = this.value.replace(/[^0-9]/g, '');" />

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Birth Date<span class="text-danger">*</span></label>
                                    <input type="text"
                                        id="txtBirthDate"
                                        runat="server"
                                        class="form-control datepicker"
                                        placeholder="dd-mm-yyyy" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Loan Amount <span class="text-danger">*</span></label>
                                    <input type="text"
                                        id="txtLoanAmount"
                                        runat="server"
                                        class="form-control"
                                        onkeypress="return isNumberOrDecimal(event);"
                                        oninput="this.value = this.value.replace(/[^0-9.]/g, '');" />

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Property Value</label>
                                    <input type="text"
                                        id="txtPropertyValue"
                                        runat="server"
                                        class="form-control"
                                        onkeypress="return isNumberOrDecimal(event);"
                                        oninput="this.value = this.value.replace(/[^0-9.]/g, '');" />

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Sale Deed Amount</label>
                                    <input type="text"
                                        id="txtSaledeedAmount"
                                        runat="server"
                                        class="form-control"
                                        onkeypress="return isNumberOrDecimal(event);"
                                        oninput="this.value = this.value.replace(/[^0-9.]/g, '');" />

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Status <span class="text-danger">*</span></label>
                                    <select id="ddlStatus" name="ddlStatus" runat="server" class="form-control"></select>
                                    <%--     <asp:DropDownList ID="ddlStatus" runat="server" Class="form-control" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Requirement <span class="text-danger">*</span></label>
                                    <select id="ddlRequirement" name="ddlRequirement" runat="server" class="form-control"></select>
                                    <%--<asp:DropDownList ID="ddlRequirement" runat="server" CssClass="form-control" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Description</label>
                                    <textarea id="txtDescription" runat="server" class="form-control" rows="4"></textarea>
                                </div>
                            </div>
                        </div>


                        <div class="card-body border-top">
                            <input type="hidden" id="hdnSchoolURL" name="hdnSchoolURL" class="hdnSchoolURL" runat="server" value="" />
                            <input class="btn btn-primary" runat="server" id="Submit1" type="submit" value="Save Information" name="btnSave" onclick="validateForm()" />
                            <button type="button" class="btn btn-default" onclick="window.location='user-management-list.aspx';">Cancel</button>
                        </div>
                        <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>

                        </div>


                        <%--  <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnSave"
                                runat="server"
                                Text="Save Information"
                                CssClass="btn btn-primary"
                                OnClick="btnSave_Click"
                                OnClientClick="return validateForm();" />

                            <asp:Button ID="btnCancel"
                                runat="server"
                                Text="Cancel"
                                CssClass="btn btn-default"
                                OnClientClick="window.location.href='lead-list.aspx'; return false;"
                                Style="margin-left: 10px;" />
                        </div>
                    </div>--%>
                </form>



            </div>

        </div>
        <!-- /.card-deck-xl -->

    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHFooter" runat="server">
    <script>

        function validateForm() {

          <%--  if (!$('#<%=txtSchemeName.ClientID%>').val().trim()) {
                alert('Scheme Name is required');
                return false;
            }

            if (!$('#<%=txtCustomerName.ClientID%>').val().trim()) {
                alert('Customer Name is required');
                return false;
            }

            if (!$('#<%=txtContactNumber.ClientID%>').val().trim()) {
                alert('Contact Number is required');
                return false;
            }

            if ($('#<%=txtContactNumber.ClientID%>').val().length !== 10) {
                alert('Contact Number must be exactly 10 digits');
                return false;
            }


            if (!$('#<%=txtBirthDate.ClientID%>').val().trim()) {
                alert('Birth Date is required');
                return false;
            }

            if (!$('#<%=txtLoanAmount.ClientID%>').val().trim()) {
                alert('Loan Amount is required');
                return false;
            }

            if ($('#<%=ddlStatus.ClientID%>').val() === "0") {
                alert('Please select Status');
                return false;
            }

            if ($('#<%=ddlRequirement.ClientID%>').val() === "") {
                alert('Please select Requirement');
                return false;
            }

            return true;--%>
        }

        function isNumberKey(evt) {
            var charCode = evt.which ? evt.which : evt.keyCode;

            if (charCode === 8 || charCode === 9 || charCode === 46)
                return true;

            if (charCode < 48 || charCode > 57)
                return false;

            return true;
        }


        var isSubmitting = false;

        function onSeClick(btn) {

            if (isSubmitting) return false;

            if (!document.getElementById('<%=txtSchemeName.ClientID%>').value.trim()) {
                alert('Scheme Name is required');
                return false;
            }

            isSubmitting = true;
            btn.disabled = true;
            btn.value = 'Saving...';
            return true;
        }

        $(function () {
            $('.datepicker').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:+0',
                maxDate: 0
            });
        });
    </script>
</asp:Content>
