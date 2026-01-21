<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Banner-List.aspx.cs" Inherits="AdminPanel_Banner_List" %>

<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
    <div class="panel hide">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="pull-left">
                    <h4><i class="icon-th-large"></i>Search</h4>
                </div>
                <div class="tools pull-right">
                    <a data-target="#collapseThree" data-toggle="collapse" href="javascript:;"><i class="icon-chevron-down text-transparent"></i></a>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body panel-collapse collapse collapse" id="collapseThree">
                <form id="frmSearch" onsubmit="SubmitForm();return false;" action="banner-list.aspx">
                    <div class="form-group">
                        <label for="tbxName">Title</label>
                        <input type="text" class="form-control" name="tbxName" maxlength="50" placeholder="Enter Title" id="tbxName" />                         
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
    </div>

    <div class="panel">
        <div class="panel-heading">
            <div class="pull-left">
                <h4><i class="icon-table"></i>Banner List</h4>
            </div>
            <div class="pull-right padding-right"><a class="btn btn-info" href="banner-modify.aspx">Add New</a></div>

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
                            <table id="tblsortable" class="dataTable table table-striped table-hover table-bordered custom-check tablesorter">
                                <thead>
                                    <tr>
                                        <th class="check-header"><span class="check">
                                            <input class="checked" type="checkbox" onclick="CbxAll(this); GetSelRecord();" id="cbxAll" /></span></th>
                                        <th>Image</th>
                                        <th>Page URL</th>
                                        <th>Rank</th>
                                        <th>Status</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody class="content">
                                    <asp:Repeater runat="server" ID="rptRecord">
                                        <ItemTemplate>
                                            <tr class="sequence" style="cursor: move;" id='<%#((System.Data.DataRowView)Container.DataItem)["id"]%>' name='<%#((System.Data.DataRowView)Container.DataItem)["ID"]%>'>
                                                <td class="check"><span class="check"><input class="checked" type="checkbox" onclick="SetCbxBox(this);" id='chk<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>' name="<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" /></span></td>
                                                <td><img src='<%#Config.VirtualDir %>thumb.aspx?path=<%#Config.Banner %><%# ((System.Data.DataRowView)Container.DataItem)["BannerFile"]%>&width=150&height=75' /></td>
                                                <td><%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["ExternalLink"]) == true ? ((System.Data.DataRowView)Container.DataItem)["ExternalLinkUrl"] : ((System.Data.DataRowView)Container.DataItem)["MenuUrl"]%></td>
                                                <td class="actions"><%#((System.Data.DataRowView)Container.DataItem)["SequenceNo"]%></td>
                                                <td class="actions">
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'status','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "Activate" : "Deactivate"%>'>
                                                    <i class="<%# Convert.ToBoolean(((System.Data.DataRowView)Container.DataItem)["Status"])==true ? "icon-ok" : "icon-remove"%>"></i></a>
                                                </td>
                                                <td class="actions">
                                                    <a href="banner-modify.aspx?id=<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>" title="Edit"><i class="icon-pencil"></i></a>
                                                    <a href="javascript:;" onclick="ChangeRecord(this,'remove','<%# ((System.Data.DataRowView) Container.DataItem)["ID"] %>')" title='Delete'><i class="icon-trash"></i></a></td>
                                            </tr>
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
        <input type="hidden" id="hdnCount" value="0" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    
    <script type="text/javascript">
        var page = 1;
        var SortColumn = '<%= SortColumn %>';
        var SortType = '<%= SortType %>';
        var PageUrl = '<%= System.IO.Path.GetFileName(Request.PhysicalPath) %>';
        var FormName = 'frmSearch';
        var RspCtrl = 'DivRender';
        var divMsg = '<%= divMsg.ClientID %>';
        var SearchControl = 'tbxName::Title';
    </script>

    <script  type="text/javascript">
        $(function () {
            SortTable();
        });
        function SortTable() {
            $("#tblsortable tbody.content").sortable({
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
                        data: { 'BannerID': ID, 'Seq': Seq },
                        success: function (response) {
                            $('#DivRender').html(response);
                            SortTable();

                            Msg = 'Banner sequence has been changed successfully.';
                            DisplMsg(divMsg, Msg, 'alert-message success');
                        }
                    });
                }
            });
            $("#tblsortable tbody.content").disableSelection();
        }

        function SetAddBannerLink() {

        }
    </script>
</asp:Content>

