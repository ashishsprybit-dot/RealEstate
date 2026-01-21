<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Cms-Modify.aspx.cs" Inherits="AdminPanel_Cms_Modify" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ Register Src="~/AdminPanel/includes/editor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">CMS Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmCMS" action="cms-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
                        <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxTitle.ClientID %>">Title</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxTitle" runat="server" name="tbxTitle" maxlength="25" />
                        </div>
                        <div class="form-group">
                            <label for="<%= txtDescription.ClientID %>">Page Content</label><span class="red">*</span>
                            <uctrl:uctrlContent ID="txtDescription" runat="server" />
                            <% if (ID == 5){ %>
                                <span style="line-height:32px;"><b>NOTE: Please upload image from image gallery for this function. Optimum width of image is 1409px.</b></span>
                            <%} %>
                        </div>
                        <div class="form-group hide">
                            <label>URL</label>
                            <input type="text" class="form-control" id="tbxURL" runat="server" name="tbxURL" maxlength="250" />
                        </div>
                        <div class="form-group">
                            <label for="exampleInputFile">Image</label>
                            <input type="file" id="fupdImage" runat="server" name="fupdImage" />                            
                            <br />
                            <b>Format :</b> .JPG, .JPEG, .PNG, .BMP, .GIF                                         
                        </div>
                        <div class="form-group">
                            <img id="imgImage" src="" alt="" runat="server" visible="false" />
                            <img src="<%=Config.VirtualDir %>adminpanel/images/delete.png" alt="Remove" id="imgCMS" title="Remove" onclick="RemoveCMSImage(this)" style="cursor: pointer; display: none;" />
                            <input type="hidden" runat="server" id="hdnCMSFile" name="hdnCMSFile" />
                            <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                        </div>
                        <div class="form-group" style="display:none">
                            <label for="<%= txtMetaTitle.ClientID %>">Meta Title</label>
                            <input type="text" class="form-control" id="txtMetaTitle" runat="server" maxlength="200" />
                        </div>

                        <div class="form-group" style="display:none">
                            <label for="<%= txtMetaKeyword.ClientID %>">Meta Keyword</label>
                            <input type="text" class="form-control" id="txtMetaKeyword" runat="server" maxlength="200" />
                        </div>
                        <div class="form-group" style="display:none">
                            <label for="<%= txtMetaDescription.ClientID %>">Meta Description</label>
                            <textarea class="form-control" id="txtMetaDescription" runat="server" style="resize: none;" />
                        </div>

                        <div class="form-group hide">
                            <label for="<%= txtHeadText.ClientID %>">Head Text</label>
                            <textarea id="txtHeadText" runat="server" rows="3" cols="50"></textarea>
                        </div>
                        <div class="form-group hide">
                            <label for="<%= txtBodyFooterText.ClientID %>">Body Footer Text</label>
                            <textarea id="txtBodyFooterText" runat="server" rows="3" cols="50"></textarea>
                        </div>
                        <div class="form-group hide">
                            <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
                        </div>
                        <div class="pull-right">
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm(this);">Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='cms-list.aspx';">Cancel</button>
                        </div>

                    </form>
                    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>
                </div>

            </div>
            <!-- / panel  -->
        </div>
    </div>
    <!-- / row 2 -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=tbxTitle.ClientID %>').focus();
             if ($('#<%=hdnImage.ClientID%>').val() == '') {
                 $('#imgCMS').hide();
             }
             else {
                 $('#imgCMS').show();
             }
         });
         var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {
            $('#<%= hdnContent.ClientID %>').val(FillTextArea());
             var ErrMsg = '';

             if (jQuery.trim($('#<%=tbxTitle.ClientID %>').val()) == '') {
                 ErrMsg += '<br />- Title';
             }

             if (jQuery.trim($('#<%= hdnContent.ClientID %>').val()) == '') {
                 ErrMsg += '<br />- Page Content';
             }

             var fupdImage = '<%= fupdImage.ClientID %>';
             if (document.getElementById(fupdImage).value != '') {
                 if (!isImage(document.getElementById(fupdImage))) {
                     ErrMsg = ErrMsg + '<br> - ' + 'Please upload only JPG, .JPEG, .PNG, .BMP, .GIF image files.';
                     document.getElementById(fupdImage).value = '';
                 }
             }

             if (ErrMsg.length != 0) {
                 var HeaderText = 'Please correct the following error(s):';
                 DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                 ScrollTop();
                 return false;
             }
             else {
                 $('#<%=divMsg.ClientID %>').hide();
                //$("#frmCMS").attr("action", $("#frmCMS").attr("action") + "&SaveAndCont=" + tab);
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

