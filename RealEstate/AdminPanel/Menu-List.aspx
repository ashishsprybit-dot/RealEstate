<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Menu-List.aspx.cs" Inherits="AdminPanel_Menu_List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style type="text/css">
        .table tbody > tr > td {
            line-height: 1.754;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Menu List</a></li>
            </ol>
        </div>
    </div>
    <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
    <div class="panel">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Menu List</h4>
            </div>
            <div class="pull-right padding-right"><a class="btn btn-info" href="menu-modify.aspx">Add New</a></div>
            <div class="clearfix"></div>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <div role="grid" class="dataTables_wrapper form-inline">
                    <div class="row">
                        <div class="col-xs-6">
                        </div>
                    </div>
                    <div id="DivRender">
                        <div id="divList" runat="server">
                            <table class="dataTable table table-striped table-hover table-bordered custom-check tablesorter" id="tblsortable">
                                <thead>
                                    <tr>
                                        <th>Menu Title</th>
                                        <th>URL</th>
                                        <th></th>
                                        <th>Rank</th>
                                        <th>Status</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody class="content">
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr class="sequence" style="cursor: move;" id='<%#((System.Data.DataRowView)Container.DataItem)["id"]%>' name='<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>'>
                                                <td><span class="text-primary"><strong><%#((System.Data.DataRowView)Container.DataItem)["TreeSubList"]%></strong></span></td>
                                                <td><%#((System.Data.DataRowView)Container.DataItem)["MenuURL"]%></td>
                                                <td style="width: 100px;"></td>
                                                <td class="actions"><%#((System.Data.DataRowView)Container.DataItem)["SequenceNo"]%></td>
                                                <td class="actions text-center no-padding-left"><a href="javascript:;" onclick="ChangeStatus('<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "inactive" : "active"%>','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                    <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i></a></td>
                                                <td class="actions" style="width: 110px;">
                                                    <a href="menu-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeStatus('remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptSubMenuParent" runat="server" DataSource='<%#GetSubMenu(Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["ID"])) %>'>
                                                <ItemTemplate>
                                                    <tr class="nosort">
                                                        <td><%#((System.Data.DataRowView)Container.DataItem)["TreeSubList"]%></td>
                                                        <td><%#((System.Data.DataRowView)Container.DataItem)["MenuURL"]%></td>
                                                        <td class="no-padding text-center">
                                                            <a class="btn no-padding-bottom" onclick="SetMenuLevel('<%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ParentID"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>','-', '<%#Container.ItemIndex %>')"><i class="icon-arrow-up"></i></a>
                                                            <a class="btn no-padding-bottom" onclick="SetMenuLevel('<%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ParentID"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>','+', '<%# Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["Count"]) == Container.ItemIndex + 1 ? 1 : 0 %>')"><i class="icon-arrow-down"></i></a>
                                                        </td>
                                                        <td class="actions">
                                                            <%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>
                                                        </td>
                                                        <td class="center">
                                                            <a href="javascript:;" onclick="ChangeStatus('<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "inactive" : "active"%>','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')"><i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i></a>
                                                        </td>
                                                        <td class="actions" style="width: 110px;">
                                                            <a href="menu-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>"><i class="icon-pencil"></i></a><a href="javascript:;" onclick="ChangeStatus('remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')"><i class="icon-trash"></i></a>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="rptSubMenuChild" runat="server" DataSource='<%#GetSubMenu(Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["ID"])) %>'>
                                                        <ItemTemplate>
                                                            <tr class="menurow lightbg nosort">
                                                                <td>
                                                                    <%#((System.Data.DataRowView)Container.DataItem)["TreeSubList"]%>
                                                                </td>
                                                                <td>
                                                                    <%#((System.Data.DataRowView)Container.DataItem)["MenuURL"]%>
                                                                </td>
                                                                <td class="no-padding text-center">
                                                                    <a class="btn no-padding-bottom" onclick="SetMenuLevel('<%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ParentID"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>','-', '<%#Container.ItemIndex %>')"><i class="icon-arrow-up"></i></a>
                                                                    <a class="btn no-padding-bottom" onclick="SetMenuLevel('<%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ParentID"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>','+', '<%# Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["Count"]) == Container.ItemIndex + 1 ? 1 : 0 %>')"><i class="icon-arrow-down"></i></a>
                                                                </td>

                                                                <td class="actions">
                                                                    <%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>
                                                                </td>
                                                                <td class="actions">
                                                                    <a href="javascript:;" onclick="ChangeStatus('<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "inactive" : "active"%>','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')">
                                                                        <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i></a>
                                                                </td>
                                                                <td class="actions">
                                                                    <a href="menu-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>"><i class="icon-pencil"></i></a>
                                                                    <a href="javascript:;" onclick="ChangeStatus('remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')"><i class="icon-trash"></i></a>
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="rptSubMenu" runat="server" DataSource='<%#GetSubMenu(Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["ID"])) %>'>
                                                                <ItemTemplate>
                                                                    <tr class="menurow nosort">
                                                                        <td>
                                                                            <%#((System.Data.DataRowView)Container.DataItem)["TreeSubList"]%>
                                                                        </td>
                                                                        <td>
                                                                            <%#((System.Data.DataRowView)Container.DataItem)["MenuURL"]%>
                                                                        </td>
                                                                        <td class="actions">
                                                                            <div style="width: 20px; float: left; height: 20px; line-height: 2px;">
                                                                                &nbsp;
                                                                                <img src="images/arrow-up.png" style="cursor: pointer;" onclick="SetMenuLevel('<%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ParentID"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>','-', '<%#Container.ItemIndex %>')" />
                                                                            </div>
                                                                            <div style="width: 20px; float: left; height: 20px; line-height: 2px;">
                                                                                &nbsp;
                                                                                <img src="images/arrow-down.png" style="cursor: pointer;" onclick="SetMenuLevel('<%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ParentID"]%>','<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>','+', '<%# Convert.ToInt32(((System.Data.DataRowView)Container.DataItem)["Count"]) == Container.ItemIndex + 1 ? 1 : 0 %>')" />
                                                                            </div>
                                                                        </td>
                                                                        <td class="actions">
                                                                            <%#((System.Data.DataRowView)Container.DataItem)["LevelNo"]%>
                                                                        </td>
                                                                        <td class="actions">
                                                                            <a href="javascript:;" onclick="ChangeStatus('remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')"><i class="icon-trash"></i></a>
                                                                        </td>
                                                                        <td class="actions">
                                                                            <a href="javascript:;" onclick="ChangeStatus('<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "inactive" : "active"%>','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')">
                                                                                <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i></a>
                                                                        </td>
                                                                        <td class="actions">
                                                                            <a href="menu-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>"><i class="icon-pencil"></i></a>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr runat="server" id="trNoRecords" visible="false">
                                        <td colspan="6" align="center" style="text-align: center;">
                                            <b>No Records Exists.</b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">

    <script type="text/javascript">
        var page = 1;
        var SortColumn = '';
        var SortType = '';
        var PageUrl = 'menu-list.aspx';
        var FormName = '';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = '';
    </script>

    <script type="text/javascript">
        $(function () {
            SortTable();
            //SetOddEvenColor();
            //$("#page-content-wrapper").removeClass("rotateInDownLeft");
        });
        function SortTable() {
            $("#tblsortable tbody.content").not('.nosort').sortable({
                items: 'tr:not(.nosort)',
                update: function (event, ui) {
                    var result = $(this).sortable('toArray');

                    var ID = '';
                    var Seq = '';
                    $('.sequence').each(function (index) {
                        ID = ID + $(this).attr('id') + ',';
                        Seq = Seq + (parseInt(index) + 1) + ',';
                    });
                    if (ID.length > 0)
                        ID = ID.substring(0, ID.length - 1);
                    if (Seq.length > 0)
                        Seq = Seq.substring(0, Seq.length - 1);

                    $.ajax({
                        type: 'POST',
                        url: PageUrl + '?resequence=y&call=ajax',
                        data: { 'MenuID': ID, 'Seq': Seq },
                        success: function (response) {
                            $('#DivRender').html(response);
                            SortTable();
                            SetOddEvenColor();
                            var sequence = "Menu Sequence has been Changed successfully.";
                            DisplMsg(divMsg, sequence, 'alert-message success');
                        }
                    });
                }
            });
            $("#tblsortable tbody.content").disableSelection();
        }

        function SetMenuLevel(LevelNo, ParentID, MenuID, LevelType, index) {
            if (LevelType == '-' && index == 0) {

                return;
            }
            if (LevelType == '+' && index == 1) {

                return;
            }
            $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
            $.ajax({
                type: 'POST',
                url: PageUrl + '?SetLevel=y&call=ajax',
                data: { 'LevelNo': LevelNo, 'ParentID': ParentID, 'MenuID': MenuID, 'LevelType': LevelType },
                success: function (response) {
                    $('#DivRender').html(response);
                    SortTable();
                    SetOddEvenColor();
                    var sequence = "Menu Sequence has been Changed successfully.";
                    DisplMsg(divMsg, sequence, 'alert-message success');
                    $.hideprogress();

                }
            });
        }

        function ChangeStatus(Type, ID) {

            if (Type == 'remove') {
                if (!confirm('Are you sure you want to delete selected record?')) {
                    return;
                }
            }
            $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
            $.ajax({
                type: 'POST',
                url: PageUrl + '?ChangeStatus=y&call=ajax',
                data: { 'ID': ID, 'type': Type },
                success: function (response) {
                    if (response == 'refexist') {
                        DisplMsg(divMsg, 'Child menu exists. Please remove it first.', 'alert-message error');
                    }
                    else {
                        $('#DivRender').html(response);
                        var Msg = '';
                        if (Type == 'remove')
                            Msg = 'Menu has been deleted successfully.';
                        else
                            Msg = 'Menu has been Changed successfully.';

                        DisplMsg(divMsg, Msg, 'alert-message success');
                    }
                    SortTable();
                    //SetOddEvenColor();
                },
                complete: function () {

                    $.hideprogress();
                }
            });
        }

        function SetOddEvenColor() {
            $('.menurow').removeClass('odd');
            $('.menurow').removeClass('even');
            $('.menurow').each(function (index) {

                if ((index % 2) == 0) {
                    $(this).addClass('even');
                }
                else {
                    $(this).addClass('odd');
                }

            });
        }
    </script>

</asp:Content>

