<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Admin-Modify.aspx.cs"
    Inherits="AdminPanel_Admin_Modify" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">Add Admin</span></h1>
    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <form id="form_Admin" action="admin-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data">

                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group hide">
                                    <label for="<%= ddlMemberType.ClientID %>">
                                        User Type<abbr title="Required">*</abbr>
                                    </label>
                                    <select runat="server" id="ddlMemberType" class="drp-border-full custom-select d-block w-100">
                                        <%--<option value="0">Select User Type</option>--%>
                                        <option value="2">Team Manager</option>
                                        <%--<option value="3">Agent</option>--%>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label for="<%=tbxFirstName.ClientID%>">First Name<abbr title="Required">*</abbr></label>
                                    <input type="text" class="form-control required" id="tbxFirstName" runat="server" name="tbxFirstName" maxlength="50" />
                                </div>
                                <div class="form-group">
                                    <label for="<%=tbxLastName.ClientID%>">Last Name<abbr title="Required">*</abbr></label>
                                    <input type="text" class="form-control required" id="tbxLastName" runat="server" name="tbxLastName" maxlength="50" />
                                </div>

                                <div class="form-group">
                                    <label for="<%=tbxEmail.ClientID%>">Email Address<abbr title="Required">*</abbr></label>
                                    <input id="tbxEmail" runat="server" name="tbxEmail" type="text" size="30" maxlength="100" class="form-control required email" autocomplete="off" />
                                    <input type="hidden" id="hdnpwd" runat="server" name="hdnpwd" />
                                </div>

                                <div class="form-group">
                                    <label for="<%=tbxUserName.ClientID%>">
                                        Username<abbr title="Required">*</abbr>
                                    </label>
                                    <input id="tbxUserName" runat="server" name="tbxUserName" type="text" class="form-control required" maxlength="50" />
                                </div>

                                <div class="form-group" id="tbPassword" runat="server" visible="false">
                                    <label for="<%=tbxPassword.ClientID%>">
                                        Password<abbr title="Required">*</abbr>
                                    </label>
                                    <div id="divPwd" runat="server">
                                        <input id="tbxPassword" runat="server" name="tbxPassword" type="password" autocomplete="off" class="form-control required password" rel="password" minlength="6" maxlength="20" />
                                        <div class="note"><b>Note :</b> Password must be at least 6 characters long</div>
                                    <a href="javascript:;" style="display: none;" id="hrefCancel" onclick="ShowHideCtrl('<%= divlblPwd.ClientID %>','<%= divPwd.ClientID %>');$('#ctl00_CPHContent_tbxPassword').attr('value','');">Cancel
                                    </a>
                                    </div>
                                    <div id="divlblPwd" runat="server" visible="false">
                                        <label id="lblPassword" runat="server"></label>
                                        <a href="javascript:;" onclick="ShowHideCtrl('<%= divPwd.ClientID %>','<%= divlblPwd.ClientID %>');$('#hrefCancel').show();">Reset</a>
                                    </div>
                                </div>

                                <div class="form-group" runat="server" id="trConfirmPwd">
                                    <label for="<%=tbxConfPassword.ClientID%>">
                                        Confirm Password<abbr title="Required">*</abbr>
                                    </label>
                                    <input name="tbxConfPassword" runat="server" id="tbxConfPassword" type="password" maxlength="20" rel="confirmpassword" autocomplete="off" class="form-control required confirmpassword" minlength="6" />
                                </div>

                                <div class="form-group hide">
                                    <label>Phone Number</label>
                                    <input id="tbxphone" name="tbxphone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                </div>

                                <div class="formRow hide">
                                    <label for="<%= fupdImage.ClientID %>">
                                        Avtar
                                    </label>
                                    <div class="custom-file">
                                        <input id="fupdImage" runat="server" name="fupdImage" type="file" size="30" maxlength="500" class="input-text large custom-file-input" />
                                        <label class="custom-file-label" for="tf3">Choose file</label>
                                    </div>
                                    <b>Note:</b>Image size approx.(50px Width x 50px Height)
                                </div>
                                <div class="formRow hide">
                                    <img id="imgImage" src="" alt="" runat="server" visible="false" />
                                    <img src="<%=Config.VirtualDir %>images/delete.gif" alt="Remove" id="imgCategory" title="Remove" onclick="RemoveCategoryImage(this)" style="cursor: pointer; display: none;" />
                                    <input type="hidden" runat="server" id="hdnCategoryFile" name="hdnCategoryFile" />
                                    <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                                </div>
                            </div>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-3">
                                <div class="formRow">
                                    <table width="100%">
                                        <tr>
                                            <td width="30">
                                                <div class="custom-control custom-checkbox mb-1">
                                                    <input type="checkbox" class="custom-control-input" id="chkModuleAll" onclick="SelectAllModules(this)">
                                                    <label class="custom-control-label" for="chkModuleAll">
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <h4 class="card-title" style="margin-bottom: 10px;">Module Access</h4>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="formRow1 form-row1" style="height: 200px; overflow: auto; overflow-y: auto; border:1px solid #c6c9d5;border-radius:.25rem;padding: 10px;">
                                        <asp:Repeater ID="rptPages" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-12 col">
                                                    <div class="custom-control custom-checkbox mb-1">
                                                        <input type="checkbox" class="chkPages custom-control-input" id="<%#Eval("ID") %>" value="<%#Eval("ID") %>">
                                                        <label class="custom-control-label" for="<%#Eval("ID") %>">
                                                            <%#Eval("PageName") %>
                                                        </label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                    </div>
                    <div class="card-body border-top">
                        <input type="hidden" id="hdnPages" name="hdnPages" class="hdnPages" runat="server" value="" />
                        <%--<button type="button" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm()">Save Information</button>--%>
                        <input class="btn btn-primary" runat="server" id="btnSave" type="submit" value="Save Information" name="btnSave" onclick="validateForm()" />
                        <button type="button" class="btn btn-default" onclick="window.location='admin-list.aspx';">Cancel</button>
                    </div>
                    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                </form>



            </div>

        </div>
        <!-- /.card-deck-xl -->

    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <%--<script src="../js/jquery-1.10.2.min.js"></script>--%>
    <script type="text/javascript">
        var divMsg = '<%= divMsg.ClientID %>';

        $(document).ready(function () {
            if (parseInt('<%=ID%>') == 0) {
                $('#<%=tbxEmail.ClientID %>').val('');
                $('#<%=tbxPassword.ClientID %>').val('');
                $('#<%=tbxConfPassword.ClientID %>').val('');
                $('#<%=tbxUserName.ClientID %>').val('');
            }

            if ('<%=strPwd%>' != '') {
                $('#<%=tbxPassword.ClientID%>').val('<%=strPwd%>');
            }

            $('#<%=tbxFirstName.ClientID %>').focus();

            var Pages = jQuery.trim('<%=strPages%>');

            if (Pages != '') {
                var Pages = Pages.split(',');
                for (var i = 0; i < Pages.length; i++) {
                    $('.chkPages').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(Pages[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }
        });

        function validateForm() {
            var assignedModules = '';
            $('.chkPages').each(function (index, obj) {
                if (obj.checked)
                    assignedModules += obj.value + ",";
            });
            if (assignedModules.length > 0) {
                assignedModules = assignedModules.substring(assignedModules, assignedModules.length - 1);
            }

            $('.hdnPages').val(assignedModules);
            //$(".need-to-validate").submit();
        }
        function callSubmit() {

        }
        function callSubmit1() {
            var frmData = new FormData();
            var assignedModules = "";

            var avtarFileControlId = $("#<%=fupdImage.ClientID%>");

            if (avtarFileControlId[0].files.length > 0) {
                frmData.append("avtarFile", avtarFileControlId[0].files[0]);
            }
            else {
                frmData.append("avtarFile", null);
            }

            frmData.append("ddlMemberType", $('#<%=ddlMemberType.ClientID%>').val());
            frmData.append("tbxFirstName", $('#<%=tbxFirstName.ClientID%>').val());
            frmData.append("tbxLastName", $('#<%=tbxFirstName.ClientID%>').val());
            frmData.append("tbxEmail", $('#<%=tbxEmail.ClientID%>').val());
            frmData.append("tbxUserName", $('#<%=tbxUserName.ClientID%>').val());

            if ($('#<%=tbxPassword.ClientID%>').val() == undefined)
                frmData.append("tbxPassword", "");
            else
                frmData.append("tbxPassword", $('#<%=tbxPassword.ClientID%>').val());

            frmData.append("hdnpwd", $('#<%=hdnpwd.ClientID%>').val());
            frmData.append("tbxphone", $('#<%=tbxphone.ClientID%>').val());
            frmData.append("tbxphone", $('#<%=tbxphone.ClientID%>').val());
            frmData.append("hdnImage", $('#<%=hdnImage.ClientID%>').val());
            frmData.append("hdnPages", $('#<%=hdnPages.ClientID%>').val());

            $('.chkPages').each(function (index, obj) {
                if (obj.checked)
                    assignedModules += obj.value + ",";
            });

            assignedModules = assignedModules.slice(0, assignedModules.length - 1);

            frmData.append("assignedModules", assignedModules);

            $.ajax({
                url: '<%=Config.VirtualDir%>adminpanel/admin-modify.aspx?id=<%=ID%>',
                type: 'POST',
                data: frmData,//$('#frmAdmin').serialize(),
                processData: false,
                contentType: false,
                success: function (resp) {
                }
            });
        }

        function SelectAllModules(ele) {
            console.log('called');
            if ($(ele).prop('checked')) {
                $('.chkPages').prop('checked', true);
            }
            else {
                $('.chkPages').prop('checked', false);
            }

        }
    </script>
</asp:Content>
