<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="tenant-modify.aspx.cs"
    Inherits="AdminPanel_Tenant_Modify" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
    <style>
        .spnTenantURL {
            float: right;
            padding: 2px 3px 2px 12px;
            background-color: #e6e2e2;
            font-weight: 500;
        }

            .spnTenantURL img {
                padding-left: 10px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">Add Tenant</span></h1>

    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <form id="form_Admin" action="tenant-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data">

                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label for="<%=tbxTenantName.ClientID%>">Name<abbr title="Required">*</abbr></label>
                                    <input type="text" class="form-control required" id="tbxTenantName" runat="server" name="tbxTenantName" maxlength="500" onblur="SetLowerTenantURL(this)" />
                                </div>
                               <%-- <div class="form-group">
                                    <label for="<%=tbxCasesCode.ClientID%>">Cases21 Code<abbr title="Required">*</abbr></label>
                                    <input type="text" class="form-control required" id="tbxCasesCode" runat="server" name="tbxCasesCode" maxlength="50" />
                                </div>--%>
                                <div class="form-group">
                                    <label for="<%=tbxTenantURL.ClientID%>">Tenant URL<abbr title="Required">*</abbr></label><span class="spnTenantURL"></span>

                                    <input type="text" class="form-control required mb-1 tbxTenantURL" id="tbxTenantURL" runat="server" name="tbxTenantURL" maxlength="15" onblur="SetTenantURL()" />
                                    <div class="note"><b>Note:</b> Do not include special characters and space.</div>
                                </div>
                                <br />
                                <%--<div class="formRow ">
                                    <label for="<%= fupdImage.ClientID %>">
                                        Background Image
                                    </label>
                                    <div class="custom-file">
                                        <input id="fupdImage" runat="server" name="fupdImage" type="file" size="30" maxlength="500" class="input-text mb-1 large custom-file-input" accept=".png, .jpg, .jpeg, .gif" onchange="ValidateSingleInput(this);" />
                                        <label class="custom-file-label mb-1" for="tf3">Choose file</label>
                                    </div>
                                    <div class="note"><b>Note:</b> Image size approx.(1130px Width x 928px Height) (.jpg, .jpeg, .gif, .png file only)</div>
                                </div>--%>
                               <%-- <div class="formRow ">
                                    <img id="imgImage" src="" alt="" runat="server" visible="false" />
                                    <img src="images/delete.png" alt="Remove" id="imgCategory" title="Remove" onclick="RemoveCategoryImage(this)" style="cursor: pointer; display: none;" />
                                    <input type="hidden" runat="server" id="hdnCategoryFile" name="hdnCategoryFile" />
                                    <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                                </div>--%>
                                <br />
                                <div class="formRow ">
                                    <label for="<%= fupdLogo.ClientID %>">
                                        Tenant Logo
                                    </label>
                                    <div class="custom-file">
                                        <input id="fupdLogo" runat="server" name="fupdLogo" type="file" size="30" maxlength="500" class="input-text mb-1 large custom-file-input" accept=".png, .jpg, .jpeg, .gif" onchange="ValidateSingleInput(this);" />
                                        <label class="custom-file-label" for="tf3">Choose file</label>
                                    </div>
                                    <div class="note"><b>Note:</b> Logo size approx.(100px Width x 72px Height) (.jpg, .jpeg, .gif, .png file only)</div>
                                </div>
                                <div class="formRow ">
                                    <img id="imgLogo" src="" alt="" runat="server" visible="false" />
                                    <img src="images/delete.png" alt="Remove" id="imgCategoryLogo" title="Remove" onclick="RemoveCategoryLogo(this)" style="cursor: pointer; display: none;" />
                                    <input type="hidden" runat="server" id="hdnCategoryLogo" name="hdnCategoryLogo" />
                                    <input type="hidden" id="hdnLogo" runat="server" name="hdnLogo" />
                                </div>

                            </div>
                            <div class="col-lg-1"></div>
                           <%-- <div class="col-lg-4">
                                <table width="100%">
                                    <tr>
                                        <td width="30">
                                            <div class="custom-control custom-checkbox mb-1">
                                                <input type="checkbox" class="custom-control-input" id="chkTenantsAll" onclick="SelectAllTenants(this)">
                                                <label class="custom-control-label" for="chkTenantsAll">
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <label>Category</label>
                                        </td>
                                    </tr>
                                </table>

                                <div class="formRow1 form-row1 " style="height: 170px; overflow: auto; overflow-y: auto; border: 1px solid #c6c9d5; border-radius: .25rem; padding: 10px;">

                                    <asp:Repeater ID="rptTenants" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-12 col">
                                                <div class="custom-control custom-checkbox mb-1">
                                                    <input type="checkbox" class="chkTenants custom-control-input" id="<%#Eval("ID") %>" value="<%#Eval("ID") %>">
                                                    <label class="custom-control-label" for="<%#Eval("ID") %>">
                                                        <%#Eval("Name") %>
                                                    </label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>--%>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                    </div>
                    <div class="card-body border-top">
                        <input type="hidden" id="hdnTenants" name="hdnTenants" class="hdnTenants" runat="server" value="" />
                        <input class="btn btn-primary" runat="server" id="btnSave" type="submit" value="Save Information" name="btnSave" onclick="validateForm()" />
                        <button type="button" class="btn btn-default" onclick="window.location='tenant-list.aspx';">Cancel</button>
                    </div>
                    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                </form>



            </div>

        </div>
        <!-- /.card-deck-xl -->

    </div>
    <button type="button" class="btn btn-secondary btnFileValidation" data-toggle="modal" data-target="#FileValidation" style="display: none"></button>

    <div id="FileValidation" class="modal fade" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                    <h5 class="modal-title">
                        <i class="fa fa-exclamation-triangle text-red mr-1"></i>Validation</h5>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body">
                    <p>Please upload .jpg, .jpeg, .gif, .png file only.</p>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <%--<script src="../js/jquery-1.10.2.min.js"></script>--%>
    <script src="js/clipboard.min.js"></script>
    <script type="text/javascript">


        var divMsg = '<%= divMsg.ClientID %>';
        $(document).ready(function () {
            $('#<%=tbxTenantName.ClientID %>').focus();
            SetTenantURL();

            var Tenants = jQuery.trim('<%=strTenants%>');
            if (Tenants != '') {
                var TenantsSplit = Tenants.split(',');
                for (var i = 0; i < TenantsSplit.length; i++) {
                    $('.chkTenants').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(TenantsSplit[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }

        });
        function setTooltip(message, elem) {
            $('#' + elem.id).tooltip('hide')
                .attr('data-original-title', message)
                .tooltip('show');
        }

        function hideTooltip(elem) {
            setTimeout(function () {
                $('#' + elem.id).tooltip('hide');
            }, 1000);
        }
        function SetTenantURL() {
            if (jQuery.trim($('.tbxTenantURL').val()) != '') {
                var websiteURL = '<%=Config.WebSiteUrl%>' + jQuery.trim($('.tbxTenantURL').val()) + '/login.aspx'
                var buttonHTML = "<button type='button' class='btn btn-sm btn-icon btn-secondary btnCopyClipBoard' id='btnCopyClipBoard' data-clipboard-text='" + websiteURL + "' style='background:none;margin-left:6px;'><i class='fa fa-copy'></i></button>";
                //<button type='button' class='btn btn-sm btn-icon btn-secondary btnCopyClipBoard' id='btnCopyClipBoard" + row.ID + "' data-clipboard-text='" + websiteURL + "' ><i class='fa fa-copy'></i></button>
                $('.spnTenantURL').html(websiteURL + buttonHTML);

                var clipboard = new ClipboardJS('.btnCopyClipBoard');

                clipboard.on('success', function (e) {
                    setTooltip('Copied!', e.trigger);
                    hideTooltip(e.trigger);
                    e.clearSelection();
                });

                $('.btnCopyClipBoard').tooltip({
                    trigger: 'click',
                    placement: 'bottom'
                });

            }
            else {
                $('.spnTenantURL').text('');
            }
        }

        var _validFileExtensions = [".jpg", ".jpeg", ".gif", ".png"];
        function ValidateSingleInput(oInput) {
            if (oInput.type == "file") {
                var sFileName = oInput.value;
                if (sFileName.length > 0) {
                    var blnValid = false;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            break;
                        }
                    }

                    if (!blnValid) {
                        $('.btnFileValidation').click();
                        oInput.value = "";
                        return false;
                    }
                }
            }
            return true;
        }

        function SelectAllTenants(ele) {

            if ($(ele).prop('checked')) {
                $('.chkTenants').prop('checked', true);
            }
            else {
                $('.chkTenants').prop('checked', false);
            }

        }

        function validateForm() {
            var assignedTenants = '';
            $('.chkTenants').each(function (index, obj) {
                if (obj.checked)
                    assignedTenants += obj.value + ",";
            });
            if (assignedTenants.length > 0) {
                assignedTenants = assignedTenants.substring(assignedTenants, assignedTenants.length - 1);
            }

            $('.hdnTenants').val(assignedTenants);
        }
         
        function SetLowerTenantURL(ele) {
            if (parseInt('<%=ID%>') == 0) {
                var title = $(ele).val();
                $('.tbxTenantURL').val(title.replace(' ', '-').toLowerCase());
                SetTenantURL();
            }
        }
    </script>
</asp:Content>
