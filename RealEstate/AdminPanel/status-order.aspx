<%@ Page Language="C#" AutoEventWireup="true" CodeFile="status-order.aspx.cs" Inherits="AdminPanel_category_order" %>

<!DOCTYPE html>

<%@ Import Namespace="Utility" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Status Order</title>
      <link href="<%=Config.WebSiteUrl %>style/dev.css" rel="stylesheet" />
    <link rel="stylesheet" href="<%=Config.WebSiteUrl %>assets/stylesheets/theme.min.css" data-skin="default">
    <link href="<%=Config.WebSiteUrl %>js/DragDrop/jquery-ui.css" rel="stylesheet" />
    <script src="<%=Config.WebSiteUrl %>js/DragDrop/jquery.min.js"></script>
    <script src="<%=Config.WebSiteUrl %>js/DragDrop/jquery-ui.min.js"></script>
     <script src="<%=Config.WebSiteUrl %>js/Common.js"></script>

    <style>
        #tblStatus td {
            font-size: .875rem;
            padding-bottom: 3px;
        }

            #tblStatus td span {
                cursor: move;
            }
    </style>
</head>
<body>
    <b>Status</b>
    <p class="text-muted" style="font-size: 13px;">Drag & Drop status to change the order. </p>
    <hr />
    <div class="divStatusRender">
        <div id="divStatusRender" runat="server">
               <div class="alert alert-danger" id="divStatusMsg" runat="server" style="display: none;"></div>
            <div class="clearfix"></div>
            <div style="min-height:360px;">
            <table id="tblStatus" cellpadding="0" cellspacing="0" border="0" width="100%" >
                <tr>
                    <th></th>
                </tr>
                <asp:Repeater ID="rptStatusListOrderChange" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <span class="spnNumeric"><%#Container.ItemIndex + 1 %>)</span>
                                <span class="spnStatusID" id="<%#Eval("StatusID") %>"><%# Utility.Common.FixLengthString(Convert.ToString(Eval("Status")),350)%></span>

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
                </div>
            <div class="modal-footer" style="border-top: 1px solid rgba(34,34,48,.1)">
                <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="SubmitOrderChange()">Submit Changes</button>
            </div>
        </div>
    </div>

      <button type="button" class="btn btn-secondary btnDemonstrationSuccess" data-toggle="modal" data-target="#SaveSuccess" style="display: none"></button>

    <div id="SaveSuccess" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog modal-sm" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body">
                    <h6>Success</h6>
                    <p>Status Order Changed successfully.</p>
                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Ok</button>
                </div>
                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        var divStatusMsg = '<%= divStatusMsg.ClientID %>';
        $(function () {
            $("#tblStatus").sortable({
                items: 'tr:not(tr:first-child)',
                cursor: 'pointer',
                axis: 'y',
                dropOnEmpty: false,
                start: function (e, ui) {
                    ui.item.addClass("selected");
                },
                stop: function (e, ui) {
                    ui.item.removeClass("selected");
                    $(this).find("tr").each(function (index) {
                        SetSequence();
                        //if (index > 0) {
                        //    $(this).find("td").eq(2).html(index);
                        //}
                    });
                }
            });
        });

        function SetSequence() {
            $('.spnNumeric').each(function (index) {
                $(this).text((index + 1) + ')');
            });
        }
          function SubmitOrderChange() {
           
            var Status = '';
            $('.spnStatusID').each(function () {
                Status = Status + $(this).attr('id') + ',';
            });

            Status = Status.substring(0, Status.length - 1);

            $.ajax({
                type: 'POST',
                url: 'status-order.aspx',
                data: { 'Status': Status, 'SaveOrder': 'Y'},
                success: function (response) {                    
                    //$('.divStatusRender').html(response);
                    var sequence = "Status Order has been Changed successfully.";
                    DisplMsg('<%= divStatusMsg.ClientID %>', sequence, 'alert-message success');
                    var locationpath = window.location.href;
                    window.setTimeout("parent.window.location.href='statusmaster.aspx'", 2000);

                }
            });
        }

        function DisplMsg(Ctrl,ErrMsg,Msgclass){$('#'+Ctrl).show();$('#'+Ctrl).html(ErrMsg);$('#'+Ctrl).attr('class',Msgclass);setTimeout('$(\'#'+Ctrl+'\').fadeOut(600);',10000);}
    </script>
</body>
</html>

