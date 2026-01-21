<%@ Page Language="C#" AutoEventWireup="true" CodeFile="skill-order.aspx.cs" Inherits="AdminPanel_Skill_order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skill Order</title>
    <link href="css/dev.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets_1/stylesheets/theme.min.css" data-skin="default">
    <link href="js/DragDrop/jquery-ui.css" rel="stylesheet" />
    <script src="js/DragDrop/jquery.min.js"></script>
    <script src="js/DragDrop/jquery-ui.min.js"></script>
    <script src="assets/js/Common.js"></script>

    <style>
        #tblQuestions td {
            font-size: .875rem;
            padding-bottom: 3px;
        }

            #tblQuestions td span {
                cursor: move;
            }
    </style>
</head>
<body>
    <b>Questions</b>
    <p class="text-muted" style="font-size: 13px;">Drag & Drop subheading to change the order. </p>
    <hr />
    <div class="divQuestionRender">
        <div id="divQuestionRender" runat="server">
            <div class="alert alert-danger" id="divQuestionMsg" runat="server" style="display: none;"></div>
            <div class="clearfix"></div>
            <div style="min-height: 360px;">
                <table id="tblQuestions" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <th></th>
                    </tr>
                    <asp:Repeater ID="rptQuestionListOrderChange" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <span class="spnNumeric"><%#Container.ItemIndex + 1 %>)</span>
                                    <span class="spnQuestionID" id="<%#Eval("ID") %>"><%# Utility.Common.FixLengthString(Convert.ToString(Eval("Skill")),200)%></span>

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
                    <p>Skill Order Changed successfully.</p>
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
        var divQuestionMsg = '<%= divQuestionMsg.ClientID %>';
        $(function () {
            $("#tblQuestions").sortable({
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
            var Subheadings = '';
            $('.spnQuestionID').each(function () {
                Subheadings = Subheadings + $(this).attr('id') + ',';
            });

            Subheadings = Subheadings.substring(0, Subheadings.length - 1);

            $.ajax({
                type: 'POST',
                url: 'skill-order.aspx',
                data: { 'Subheadings': Subheadings, 'SaveOrder': 'Y', 'sid': <%=SubHeadingID%> , 'cid': <%=CategoryID%> , 'qid': <%=QuestionID%>  },
                success: function (response) {
                    //$('.divQuestionRender').html(response);
                    var sequence = "Subheadings Order has been Changed successfully.";
                    DisplMsg('<%= divQuestionMsg.ClientID %>', sequence, 'alert-message success');
                    var locationpath = window.location.href;
                    window.setTimeout("parent.window.location.href='sub-heading-skill-list.aspx?sid=<%=SubHeadingID%>&cid=<%=CategoryID%>&qid=<%=QuestionID%>'", 2000);

                }
            });
        }

        function DisplMsg(Ctrl, ErrMsg, Msgclass) { $('#' + Ctrl).show(); $('#' + Ctrl).html(ErrMsg); $('#' + Ctrl).attr('class', Msgclass); setTimeout('$(\'#' + Ctrl + '\').fadeOut(600);', 10000); }
    </script>
</body>
</html>
