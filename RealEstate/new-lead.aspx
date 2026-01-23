<%@ Page Title="New Lead" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="new-lead.aspx.cs" Inherits="new_lead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

    <h2>Add Lead</h2>

    <div id="divMsg" runat="server" visible="false"></div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Scheme Name *</label>
                <input type="text" id="txtSchemeName" runat="server" class="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Customer Name *</label>
                <input type="text" id="txtCustomerName" runat="server" class="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Contact Number *</label>
                <input type="text" id="txtContactNumber" runat="server" class="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Birth Date</label>
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
                <label>Loan Amount *</label>
                <input type="text" id="txtLoanAmount" runat="server" class="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Property Value</label>
                <input type="text" id="txtPropertyValue" runat="server" class="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Sale Deed Amount</label>
                <input type="text" id="txtSaledeedAmount" runat="server" class="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Status *</label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Requirement *</label>
                <asp:DropDownList ID="ddlRequirement" runat="server" CssClass="form-control" />
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

    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="btnSave"
                runat="server"
                Text="Save Information"
                CssClass="btn btn-primary"
                OnClick="btnSave_Click" />

            <asp:Button ID="btnCancel" 
    runat="server" 
    Text="Cancel" 
    CssClass="btn btn-default" 
    OnClientClick="window.location.href='lead-list.aspx'; return false;" 
    style="margin-left: 10px;" />
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHFooter" runat="server">
    <script>
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
