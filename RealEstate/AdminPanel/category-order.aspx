<%@ Page Language="C#" AutoEventWireup="true" CodeFile="category-order.aspx.cs" Inherits="AdminPanel_category_order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Category Order</title>
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

        .ddlCategoryOrder {
            font-size: 12px;
            width: 60%;
            margin-left: 10px;
        }

        #divCategory {
            padding: 10px;
        }

        #divQuestionMsg {
            font-size: 13px !important;
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
</head>
<body>
    <b>Category</b>
    <p class="text-muted" style="font-size: 13px;">Drag & Drop category to change the order. </p>
    <hr />
    <div class="divQuestionRender">
        <div id="divQuestionRender" runat="server">
            <div class="alert alert-danger" id="divQuestionMsg" runat="server" style="display: none;"></div>
            <div class="clearfix"></div>

            <select id="ddlCategoryOrder" name="ddlCategoryOrder" runat="server" class="form-control ddlCategoryOrder" onchange="CategoryValueChange(this)">
            </select>


            <div style="min-height: 325px;" class="divCategory">
                <div id="divCategory" runat="server">
                    <table id="tblQuestions" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <th></th>
                        </tr>
                        <asp:Repeater ID="rptQuestionListOrderChange" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td width="30">
                                        <span class="spnNumeric"><%#Container.ItemIndex + 1 %>)</span>
                                    </td>
                                    <td>
                                        <span class="spnQuestionID" id="<%#Eval("ID") %>"><%#  Convert.ToString(Eval("Name")) %></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="modal-footer" style="border-top: 1px solid rgba(34,34,48,.1)">
                <button type="button" class="btn btn-primary" style="font-size: 13px;" data-dismiss="modal" onclick="SubmitOrderChange()">Submit Changes</button>
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
                    <p>Questions Order Changed successfully.</p>
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

            var Questions = '';
            $('.spnQuestionID').each(function () {
                Questions = Questions + $(this).attr('id') + ',';
            });

            Questions = Questions.substring(0, Questions.length - 1);

            $.ajax({
                type: 'POST',
                url: 'category-order.aspx',
                data: { 'Questions': Questions, 'SaveOrder': 'Y' },
                success: function (response) {
                    //$('.divQuestionRender').html(response);
                    var sequence = "Category Order has been Changed successfully.";
                    DisplMsg('<%= divQuestionMsg.ClientID %>', sequence, 'alert-message success');
                    $('.divCategory').html('');

                    $('.ddlCategoryOrder').val('0');
                    //var locationpath = window.location.href;
                    //window.setTimeout("parent.window.location.href='category-list.aspx'", 3000);

                }
            });
        }
        function CategoryValueChange(ele) {

            if ($(ele).val() != '0') {

                $.ajax({
                    type: 'POST',
                    url: 'category-order.aspx',
                    data: { 'GetSubcategory': 'Y', 'cid': $(ele).val() },
                    success: function (response) {
                        $('.divCategory').html(response);
                        SetCategorySequence();
                    }
                });
            }
            else {
                $('.divCategory').html('');
            }
        }

        function SetCategorySequence() {
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
        }

        function DisplMsg(Ctrl, ErrMsg, Msgclass) { $('#' + Ctrl).show(); $('#' + Ctrl).html(ErrMsg); $('#' + Ctrl).attr('class', Msgclass); setTimeout('$(\'#' + Ctrl + '\').fadeOut(600);', 10000); }
    </script>
</body>
</html>
