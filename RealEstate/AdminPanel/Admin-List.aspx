<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Admin-List.aspx.cs" Inherits="AdminPanel_Admin_List" %>

<%@ Register TagName="Paging" TagPrefix="Ctrl" Src="~/AdminPanel/includes/ListPagePagging.ascx" %>

<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <div class="d-flex justify-content-between">
            <h1 class="page-title">Real Estate Admins List </h1>
            <div class="btn-toolbar">
                <button type="button" class="btn btn-primary" onclick="window.location.href='admin-modify.aspx'">Add Real Estate Admin</button>
            </div>
        </div>
        <%-- <p class="text-muted">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>--%>
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
                                <option value="1">Full Name</option>
                                <option value="2">User Name</option>
                                <option value="3">Email Address</option>
                                <option value="4">Status</option>
                            </select>
                        </div>
                        <!-- /.input-group-prepend -->
                        <!-- .input-group -->
                        <div class="input-group has-clearable">
                            <button id="clear-search" type="button" class="close" aria-label="Close"><span aria-hidden="true"><i class="fa fa-times-circle"></i></span></button>
                            <div class="input-group-prepend">
                                <span class="input-group-text"><span class="oi oi-magnifying-glass"></span></span>
                            </div>
                            <input id="table-search" type="text" class="form-control" placeholder="Search Admins">
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
                            <%-- <th style="min-width: 135px;">
                                <div class="thead-dd dropdown">
                                    <span class="custom-control custom-control-nolabel custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" id="check-handle">
                                        <label class="custom-control-label" for="check-handle"></label>
                                    </span>
                                    <div class="thead-btn" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <span class="fa fa-caret-down1"></span>
                                    </div>--%>
                            <%--<div class="dropdown-menu">
                                        <div class="dropdown-arrow"></div>
                                        <a class="dropdown-item selectall" href="#">Select all</a> 
                                        <a class="dropdown-item unselectall" href="#">Unselect all</a>
                                        <div class="dropdown-divider"></div>
                                        <a class="dropdown-item" href="#">Bulk remove</a> 
                                    </div>--%>
                            <%--</div>
                            </th>--%>
                            <th>Full Name</th>
                            <th>User Name </th>
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

    <%--<div class="panel">
            <div class="panel panel-default">
                <div class="panel-heading">              
                    <div class="tools pull-right">
                        <a data-target="#collapseThree" data-toggle="collapse" href="javascript:;"><i class="icon-chevron-down text-transparent"></i></a>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body panel-collapse collapse collapse" id="collapseThree">
                    <form id="frmSearch" onsubmit="SubmitForm();return false;" action="admin-list.aspx">
                        <div class="form-group">
                            <label for="tbxFname">First Name</label>
                            <input type="text" class="form-control" id="tbxFname" name="tbxFname" maxlength="50" placeholder="Enter First Name" />
                        </div>
                        <div class="form-group">
                            <label for="tbxLname">Last Name</label>
                            <input type="text" class="form-control" id="tbxLname" name="tbxLname" maxlength="50" placeholder="Enter Last Name" />
                        </div>
                        <div class="form-group">
                            <label for="tbxUname">Username</label>
                            <input type="text" class="form-control" id="tbxUname" name="tbxUname" maxlength="50" placeholder="Enter Username" />
                        </div>
                        <div class="form-group">
                            <label for="tbxEmail">Email Address</label>
                            <input type="text" class="form-control" id="tbxEmail" name="tbxEmail" maxlength="50" placeholder="Enter Email Address" />
                        </div>
                        <img src="images/loading1.gif" alt="Loading..." id="ImgSrchLoad" class="hide" />
                        <input class="btn hide" type="submit" name="btnPage" value="Page Click" id="btnPageClick" />
                        <input type="hidden" id="hdnID" name="hdnID" />
                        <input type="hidden" id="hdnRecPerPage" value="<%= RecordPerPage %>" name="hdnRecPerPage" />
                        <div class="panel-footer">
                            <button type="submit" class="btn btn-info" onclick="page=1;return ValidateCtrl();">Search</button>
                            <button type="submit" id="btnReset" class="btn btn-default" onclick="page=1;ClearControls();" value="Reset">Reset</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>--%>

    <%--<div class="panel">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Admin List</h4>
            </div>
            <div class="pull-right padding-right"><a class="btn btn-info" href="admin-modify.aspx">Add New</a></div>
            <div class="clearfix"></div>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <div role="grid" class="dataTables_wrapper form-inline">
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="dataTables_length">
                                <label id="divRecSelect"></label>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="dataTables_filter">
                                <label>
                                    <select class="drp-border" onchange="javascript:PerformAction(this);">
                                        <option value="Action">Select Action</option>
                                        <option value="active">Activate</option>
                                        <option value="inactive">Deactivate</option>
                                        <option value="remove">Delete</option>
                                    </select>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="DivRender">
                        <div id="divList" runat="server">
                            <table class="dataTable table table-striped table-hover table-bordered custom-check">
                                <thead>
                                    <tr>
                                        <th class="check-header"><span class="check">
                                            <input class="checked" type="checkbox" onclick="CbxAll(this); GetSelRecord();" id="cbxAll" /></span></th>
                                        <th>Full Name</th>
                                        <th>Username</th>
                                        <th>Email Address</th>
                                        <th style="text-align: center">Status</th>
                                        <th style="text-align: center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="check">
                                                    <span class="check">
                                                        <input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td><span class="text-primary1"><%# ((System.Data.DataRowView)Container.DataItem)["FirstName"]%></span> <%# ((System.Data.DataRowView)Container.DataItem)["LastName"]%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["UserName"]%></td>
                                                <td><%# ((System.Data.DataRowView)Container.DataItem)["EmailId"]%></td>
                                                <td class="actions">
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'status','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                        <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i>
                                                    </a>
                                                </td>
                                                <td class="actions">
                                                    <a href="admin-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="false">
                                        <td colspan="8" align="center" style="text-align: center;">
                                            <b>No Records Exists.</b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <Ctrl:Paging runat="server" ID="CtrlPage1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = 'admin-list.aspx';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'tbxFname::FirstName@tbxLname::LastName@tbxUname::Username@tbxEmail::EmailId';
        var userdata = <%= data %>;
    </script>
    <!-- BEGIN PAGE LEVEL JS -->
    <script src="assets_1/javascript/pages/dataTables.bootstrap.js"></script>
    <script src="js/Page/datatables-user.js"></script>

</asp:Content>
