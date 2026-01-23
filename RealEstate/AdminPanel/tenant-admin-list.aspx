<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="tenant-admin-list.aspx.cs" Inherits="AdminPanel_SchoolAdmin_List" %>

<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>

<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <div class="d-flex justify-content-between">
            <h1 class="page-title">Tenant Admin List </h1>
            <div class="btn-toolbar">
                <button type="button" class="btn btn-primary" onclick="window.location.href='tenant-admin-modify.aspx'">Add Tenant Admin</button>
            </div>
        </div>
         <%--<p class="text-muted">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>--%>
    </header>
    <!-- /.page-title-bar -->
    <div class="alert alert-danger hide" id="divMsg" runat="server" style="display: none;"></div>

    <div class="page-section">
        <!-- .card -->
        <div class="card card-fluid">
            <!-- .card-body -->
            <div class="card-body">
                <!-- .form-group -->
                <div class="form-group">
                    <!-- .input-group -->
                    <div class="input-group input-group-alt">
                        <!-- .input-group-prepend -->
                        <div class="input-group-prepend">
                            <select id="filterBy" class="custom-select">
                                <option value='' selected>Filter By </option>
                                <option value="0">Tenant</option>
                                <option value="1">Name</option>
                                <option value="2">Email Address</option>                                
                            </select>
                        </div>
                        <!-- /.input-group-prepend -->
                        <!-- .input-group -->
                        <div class="input-group has-clearable">
                            <button id="clear-search" type="button" class="close" aria-label="Close"><span aria-hidden="true"><i class="fa fa-times-circle"></i></span></button>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><span class="oi oi-magnifying-glass"></span></span>
                            </div>
                            <input id="table-search" type="text" class="form-control" placeholder="Search Tenant Admin">
                        </div>
                        <!-- /.input-group -->
                    </div>
                    <!-- /.input-group -->
                </div>
                <!-- /.form-group -->
                <form id="frmSearch" action="employee-list.aspx">
                    <input type="hidden" id="hdnID" name="hdnID" />
                    <input type="hidden" id="hdnSearch" name="hdnSearch" />
                </form>
                <!-- .table -->
                <table id="myTable" class="table">
                    <!-- thead -->
                    <thead>
                        <tr>                           
                            <th>Tenant</th>
                            <th>Full Name</th>
                            <th>Email Address </th>
                            <th width="70" class="text-center">Status </th>
                            <th width="70" class="text-center">Edit </th>
                            <th width="70" class="text-center">Delete </th>
                        </tr>
                    </thead>
                    <!-- /thead -->
                    <!-- tbody -->
                    <tbody>
                        <!-- create empty row to passing html validator -->
                    </tbody>
                    <!-- /tbody -->
                </table>
                <!-- /.table -->
            </div>
            <!-- /.card-body -->
        </div>
        <!-- /.card -->
    </div>

    

    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'tenant-admin-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'tbxFname::FirstName@tbxLname::LastName@tbxUname::Username@tbxEmail::EmailId';
        var userdata = <%= data %>;
    </script>
    <!-- BEGIN PAGE LEVEL JS -->
    <script src="assets_1/javascript/pages/dataTables.bootstrap.js"></script>
    <script src="js/Page/datatables-schooladmin.js"></script>

</asp:Content>
