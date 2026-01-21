<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Banner-Modify.aspx.cs" Inherits="AdminPanel_Banner_Modify" EnableEventValidation="false"  ValidateRequest="false" %>
<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
      <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Banner Modify</a></li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg" id="trBannerDetail">
                    <form id="frmBanner" action="banner-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxTitle.ClientID %>">Title</label>
                            <input type="text" class="form-control" id="tbxTitle" runat="server" name="tbxTitle" maxlength="500" />
                        </div>
                        <div class="form-group">
                            <label for="<%= tbxTitle2.ClientID %>">Description</label>
                            <input type="text" class="form-control" id="tbxTitle2" runat="server" name="tbxTitle" maxlength="2000" />
                        </div>
                         <div class="form-group">
                            <label for="exampleInputFile">Banner Image</label>
                            <input type="file" id="fupdImage" runat="server" name="fupdImage" />
                            <b>Note:</b>Image size approx.(376px Width x 402px Height)                                           
                        </div>
                        <div class="form-group">
                            <img id="imgImage" src="" alt="" runat="server" visible="false" />
                            <img src="<%=Config.VirtualDir %>adminpanel/images/delete.png" alt="Remove" id="imgCMS" title="Remove" onclick="RemoveCMSImage(this)" style="cursor: pointer; display: none;" />
                            <input type="hidden" runat="server" id="hdnCMSFile" name="hdnCMSFile" />
                            <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                        </div>
                        <div class="form-group">
                            <label>Link Type</label>&nbsp;&nbsp;
                            <input type="radio" runat="server" name="LinkType" id="rdNone" />&nbsp;None&nbsp;&nbsp;
                            <input type="radio" runat="server" name="LinkType" id="rdCMS" />&nbsp;Internal Link&nbsp;&nbsp;
                            <input type="radio" runat="server" name="LinkType" id="rdExternal" />&nbsp;External Link  &nbsp;&nbsp;                                        
                        </div>
                        <div class="form-group" id="trCMSURL" runat="server">
                            <div id="divSelectModule" style="overflow: auto; overflow-x: hidden; height: 200px;float: left; width: 100%; margin: 10px 0 10px 0; border: 1px solid #ABADB3;">
                                <asp:Repeater ID="rptModuleList" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered" >
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width:5px;">
                                                <input type="radio" id='<%# Eval("ID") %>' rel='<%# Eval("MenuUrl")%>' name="rbSelect" class="MenuID" onclick="AssignLink(this);" />
                                            </td>
                                            <td>
                                                <%# Eval("Name")%><input type="hidden" id='hdnLink<%#  Eval("ID") %>' name="hdnLink<%# Eval("ID") %>" value="<%# Eval("MenuUrl") %>" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div><label>URL:</label> <input id="txtCMSLink" runat="server" name="txtCMSLink" type="text" maxlength="200"  class="form-control" readonly="readonly" /></div>
                        </div>
                        <div class="form-group"  id="trExternal" runat="server">
                            <label>External Link</label>
                            <input type="text" class="form-control"  id="txtExternalLink" runat="server" name="txtExternalLink" maxlength="250" />&nbsp;(e.g. http://www.example.com)
                        </div>

                         <div class="form-group hide">
                            <label for="<%= txtDesc.ClientID %>">Description</label><span class="red">*</span>
                            <textarea type="text" class="form-control" id="txtDesc" runat="server" name="txtDesc" maxlength="250" style="resize: none;"/>
                        </div>
                        <div class="pull-right">
                            <input type="hidden" runat="server" id="hdnBannerFile" name="hdnBannerFile" />
                            <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
                            <input type="hidden" id="hdnLinkType" name="hdnLinkType" runat="server" />
                            <input type="hidden" id="hdnExternallink" name="hdnExternallink" runat="server" />
                            <input type="hidden" id="hdnMenuID" name="hdnMenuID" runat="server" />
                            <input type="hidden" id="hdnMenuPromptLink" name="hdnMenuPromptLink" runat="server" />
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" >Save Information</button>                             
                            <button type="button" class="btn btn-default" onclick="window.location='banner-list.aspx';" >Cancel</button> 
                        </div>
                    </form>
                    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript" >
        var page = 1;
        var FormName = 'frmBanner';
        var PageUrl = 'banner-modify.aspx';
        var divMsg = '<%= divMsg.ClientID %>';
        var RspCtrl = 'trBannerDetail';
        function AssignLink(ctrl) {
            $('#<%=txtCMSLink.ClientID %>').val($('#hdnLink' + $(ctrl).attr('id')).val());
            $('#<%=hdnMenuPromptLink.ClientID %>').val($('#<%=txtCMSLink.ClientID %>').val());
            $('#<%=hdnMenuID.ClientID %>').val($(ctrl).attr('id'));
            if ($('#<%=hdnMenuPromptLink.ClientID %>').val() == "Extenal Link") {
                $('#<%=txtCMSLink.ClientID %>').attr('readonly', '');
                $('#<%=txtCMSLink.ClientID %>').val('');
                $('#<%=hdnExternallink.ClientID %>').val('1');
            }
            else {
                $('#<%=txtCMSLink.ClientID %>').attr('readonly', 'readonly');
                $('#<%=hdnExternallink.ClientID %>').val('0');
            }
        }
        $(document).ready(function () {
            $('#<%=tbxTitle.ClientID %>').focus();
            if ($('#<%=hdnImage.ClientID%>').val() == '') {
                $('#imgCMS').hide();
            }
            else {
                $('#imgCMS').show();
            }
        });

        $(document).ready(function () {
            if (parseInt('<%=ID %>') > 0) {
                $('.MenuID').each(function () {
                    if ($(this).attr('id') == $('#<%=hdnMenuID.ClientID %>').val()) {
                        $(this).prop('checked', true);
                        $('#<%=txtCMSLink.ClientID %>').val($(this).attr('rel'));
                    }
                });
            }
            else {
                $('#<%=hdnMenuID.ClientID %>').val('0');
            }

            if ($('#<%= rdCMS.ClientID %>').prop('checked')) {

                $('#<%=trCMSURL.ClientID %>').css('display', '');
                $('#<%=trExternal.ClientID %>').css('display', 'none');
                $('#<%=hdnExternallink.ClientID %>').val('0');
                $('#<%=hdnLinkType.ClientID %>').val('1');

            }
            if ($('#<%= rdNone.ClientID %>').prop('checked')) {
                $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                $('#<%=trExternal.ClientID %>').css('display', 'none');
                $('#<%=hdnExternallink.ClientID %>').val('0');
                $('#<%=hdnMenuID.ClientID %>').val('0');
                $('#<%=hdnLinkType.ClientID %>').val('0');
            }
            if ($('#<%= rdExternal.ClientID %>').prop('checked')) {
                $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                $('#<%=trExternal.ClientID %>').css('display', '');
                $('#<%=hdnExternallink.ClientID %>').val('1');
                $('#<%=hdnLinkType.ClientID %>').val('2');

            }
            $('#<%= rdNone.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                    $('#<%=trExternal.ClientID %>').css('display', 'none');
                    $('#<%=hdnExternallink.ClientID %>').val('0');
                    $('#<%=hdnMenuID.ClientID %>').val('0');
                    $('#<%=hdnLinkType.ClientID %>').val('0');
                }
            });

            $('#<%= rdCMS.ClientID %>').change(function () {

                if ($(this).prop('checked')) {
                    $('#<%=trCMSURL.ClientID %>').css('display', '');
                    $('#<%=trExternal.ClientID %>').css('display', 'none');
                    $('#<%=hdnExternallink.ClientID %>').val('0');
                    $('#<%=hdnMenuID.ClientID %>').val('0');
                    $('#<%=hdnLinkType.ClientID %>').val('1');

                }
            });
            $('#<%= rdExternal.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                    $('#<%=trExternal.ClientID %>').css('display', '');
                    $('#<%=hdnExternallink.ClientID %>').val('1');
                    $('#<%=hdnLinkType.ClientID %>').val('2');
                }
            });
        });
        function ValidateForm() {
            //$('#<%= hdnContent.ClientID %>').val(FillTextArea());
            var ErrMsg = '';
            var fupdImage = '<%= fupdImage.ClientID %>';
            if (document.getElementById(fupdImage).value != '') {
                if (!isImage(document.getElementById(fupdImage))) {
                    ErrMsg = ErrMsg + '<br> - ' + 'Please upload only JPG, .JPEG, .PNG, .BMP, .GIF image files.';                    
                    document.getElementById(fupdImage).value = '';
                }
            }
            else {

                if (parseInt('<%=ID %>') == 0 || $('#<%=hdnImage.ClientID %>').val() == '') {
                    ErrMsg += '<br/> - Banner Image';
                }
            }
            if ($('#<%= rdExternal.ClientID %>').prop('checked')) {
                if (jQuery.trim($('#<%=txtExternalLink.ClientID %>').val()) != '') {
                    if (!isValidURL(jQuery.trim($('#<%=txtExternalLink.ClientID %>').val()))) {
                        ErrMsg += '<br/> - Please Enter Valid External Link.';
                    }
                }
                else {
                    ErrMsg += '<br/> - External Link.';
                }
            }
            if ($('#<%= rdCMS.ClientID %>').is(':checked')) {
                var flag = 0;
                $(".MenuID").each(function () {
                    if ($(this).is(":checked")) {
                        flag = 1;
                    }
                });

                if (flag == 0) {
                    ErrMsg += '<br/> - Internal Link.';
                }
            }
            $(document).ready(function () {
                $('#<%=tbxTitle.ClientID %>').focus();
                if ($('#<%=hdnImage.ClientID%>').val() == '') {
                    $('#imgCMS').hide();
                }
                else {
                    $('#imgCMS').show();
                }
            });

            if (ErrMsg.length != 0) {                
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
                return false;
            }
            else {
                $('#<%=divMsg.ClientID %>').hide();
                return true;
            }
            return true;
        }
        function RemoveCMSImage(imgCMS) {
            if (confirm('Are you sure you want to remove image?')) {
                $(imgCMS).hide();
                $('#<%=imgImage.ClientID %>').hide();
                $('#<%=hdnImage.ClientID %>').val('');
            }
        }
    </script>
</asp:Content>

