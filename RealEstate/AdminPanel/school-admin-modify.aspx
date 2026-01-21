<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="school-admin-modify.aspx.cs"
    Inherits="AdminPanel_SchoolAdmin_Modify" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">Add School Admin</span></h1>
    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <form id="form_Admin" action="tenant-admin-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data">

                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label for="<%=ddlSchool.ClientID%>">School<abbr title="Required">*</abbr></label>
                                    <select id="ddlSchool" nmae="ddlSchool" runat="server" class="form-control required" rel="category"></select>
                                </div>
                                <div class="form-group">
                                    <label for="<%=tbxFirstName.ClientID%>">First Name<abbr title="Required">*</abbr></label>
                                    <input type="text" class="form-control required" id="tbxFirstName" runat="server" name="tbxFirstName" maxlength="50" />
                                </div>
                                <div class="form-group">
                                    <label for="<%=tbxLastName.ClientID%>">Last Name</label>
                                    <input type="text" class="form-control " id="tbxLastName" runat="server" name="tbxLastName" maxlength="50" />
                                </div>

                                <div class="form-group">
                                    <label for="<%=tbxEmail.ClientID%>">Email Address<abbr title="Required">*</abbr></label>
                                    <input id="tbxEmail" runat="server" name="tbxEmail" type="text" size="30" maxlength="100" class="form-control required email" autocomplete="off" />
                                    <input type="hidden" id="hdnpwd" runat="server" name="hdnpwd" />
                                </div>
                                <div id="divPasswordSection" style="display: none">
                                    <div class="form-group" id="tbPassword" runat="server" visible="false">
                                        <label for="<%=tbxPassword.ClientID%>">
                                            Password<abbr title="Required">*</abbr>
                                        </label>
                                        <div id="divPwd" runat="server">
                                            <input id="tbxPassword" runat="server" name="tbxPassword" type="password" autocomplete="off" class="form-control password tbxPassword" rel="password" minlength="6" maxlength="20" />
                                            <div class="note"><b>Note :</b> Password must be at least 6 characters long</div>
                                            <a href="javascript:;" style="display: none;" id="hrefCancel" onclick="ShowHideCtrl('<%= divlblPwd.ClientID %>','<%= divPwd.ClientID %>');$('#ctl00_CPHContent_tbxPassword').attr('value','');">Cancel
                                            </a>
                                        </div>
                                        <div id="divlblPwd" runat="server" visible="false">
                                            <label id="lblPassword" runat="server"></label>
                                            <a href="javascript:;" onclick="ShowHideCtrl('<%= divPwd.ClientID %>','<%= divlblPwd.ClientID %>');$('#hrefCancel').show();">Edit</a>
                                        </div>
                                    </div>

                                    <div class="form-group" runat="server" id="trConfirmPwd">
                                        <label for="<%=tbxConfPassword.ClientID%>">
                                            Confirm Password<abbr title="Required">*</abbr>
                                        </label>
                                        <input name="tbxConfPassword" runat="server" id="tbxConfPassword" type="password" maxlength="20" rel="confirmpassword" autocomplete="off" class="form-control confirmpassword tbxConfPassword" minlength="6" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Phone Number</label>
                                    <input id="tbxphone" name="tbxphone" type="text" size="30" maxlength="20" runat="server" class="form-control" />
                                </div>
                                <div class="col-lg-12 row" style="padding-left: 0px;">

                                    <div class="col-lg-6">
                                        <div class="form-group" >
                                            <label>Status<abbr title="Required">*</abbr></label>
                                            <select id="ddlStatus" name="ddlStatus" runat="server" class="form-control" style="width: 200px;">
                                                <option value="1">Approved</option>
                                                <option value="0">Rejected</option>
                                                <option value="2">Under Review</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group" style="text-align:center">
                                            <label>API Access (Includes all data)?</label><br />
                                            <input type="checkbox" id="chkAPIAccess" name="chkAPIAccess" runat="server" />
                                        </div>
                                    </div>

                                </div>


                                <div class="formRow hide">
                                    <label for="<%= fupdImage.ClientID %>">
                                        Avtar
                                    </label>
                                    <div class="custom-file">
                                        <input id="fupdImage" runat="server" name="fupdImage" type="file" size="30" maxlength="500" class="input-text large custom-file-input" />
                                        <label class="custom-file-label" for="tf3">Choose file</label>
                                    </div>
                                    <div class="note"><b>Note:</b>Image size approx.(50px Width x 50px Height)</div>
                                </div>
                                <div class="formRow hide">
                                    <img id="imgImage" src="" alt="" runat="server" visible="false" />
                                    <img src="<%=Config.VirtualDir %>images/delete.gif" alt="Remove" id="imgCategory" title="Remove" onclick="RemoveCategoryImage(this)" style="cursor: pointer; display: none;" />
                                    <input type="hidden" runat="server" id="hdnCategoryFile" name="hdnCategoryFile" />
                                    <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                                </div>
                            </div>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-4">
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
                                <div class="formRow1 form-row1" style="height: 450px; overflow: auto; overflow-y: auto; border: 1px solid #c6c9d5; border-radius: .25rem; padding: 10px;">

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
                        <div class="clearfix"></div>
                        <br />
                    </div>
                    <div class="card-body border-top">
                        <input type="hidden" id="hdnPages" name="hdnPages" class="hdnPages" runat="server" value="" />
                        <%--<button type="button" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm()">Save Information</button>--%>
                        <input class="btn btn-primary" runat="server" id="btnSave" type="submit" value="Save Information" name="btnSave" onclick="validateForm()" />
                        <button type="button" class="btn btn-default" onclick="window.location='school-admin-list.aspx';">Cancel</button>
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

            if (parseInt('<%=GoogleUser%>') == 1) {
                $('#divPasswordSection').hide();
                $('.tbxPassword').removeClass('required');
                $('.tbxConfPassword').removeClass('required');

                $('.tbxPassword').removeAttr('rel');
                $('.tbxConfPassword').removeAttr('rel');
            }
            else {
                $('#divPasswordSection').show();
                $('.tbxPassword').addClass('required');
                $('.tbxConfPassword').addClass('required');
            }

            if (parseInt('<%=ID%>') == 0) {
                $('#<%=tbxEmail.ClientID %>').val('');
                $('#<%=tbxPassword.ClientID %>').val('');
                $('#<%=tbxConfPassword.ClientID %>').val('');
            }
            else {
                $('#<%=tbxPassword.ClientID %>').val('<%=strPasswordEdit%>');
                $('#<%=tbxConfPassword.ClientID %>').val('<%=strPasswordEdit%>');

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
