<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Emailtemplate-Modify.aspx.cs" Inherits="AdminPanel_Emailtemplate_Modify" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Src="~/AdminPanel/includes/editor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
        <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Emailtemplate Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmEmailTemplate" action="emailtemplate-modify.aspx" onsubmit="ValidateForm();return false;">
                        <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxTemplateName.ClientID %>">Template Name</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxTemplateName" runat="server" name="tbxTemplateName" maxlength="50" />
                        </div>
                        <div class="form-group">
                            <label for="<%= tbxSubject.ClientID %>">Subject</label><span class="red">*</span>
                            <input id="tbxSubject" runat="server" name="tbxSubject" type="text" maxlength="500" class="form-control"  />
                        </div>
                        <div class="form-group">
                            <label for="<%= tbxTemplate.ClientID %>">Template</label><span class="red">*</span>
                            <uctrl:uctrlContent ID="tbxTemplate" runat="server" />
                            <span class="red">Note: Please do not remove any special words from subject or template. E.g. {###emailid###}, {###MemberName###} </span>
                        </div>
                        <div class="pull-right">
                            <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" >Save Information</button>
                            <button type="button" class="btn btn-default" onclick="window.location='emailtemplate-list.aspx';">Cancel</button>
                        </div>                         
                    </form>
                </div>
            </div>
            <!-- / panel  -->
        </div>
    </div>
    <!-- / row 2 -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
     <script type="text/javascript"  >
         var divMsg = '<%= divMsg.ClientID %>';

         $(document).ready(function () {
             $('#<%=tbxTemplateName.ClientID %>').focus();
        });
        function ValidateForm() {
            $('#<%= hdnContent.ClientID %>').val(FillTextArea());

            var ErrMsg = '';
            if (jQuery.trim($('#<%= tbxTemplateName.ClientID %>').val()) == '')
                ErrMsg += '<br />- Template Name';
            if (jQuery.trim($('#<%= tbxSubject.ClientID %>').val()) == '')
                ErrMsg += '<br />- Subject';

            var Description = $('#<%= hdnContent.ClientID %>').val();
            if (Description != '') {
                if (Description.substring(0, 3) == "<p>") {
                    Description = Description.substring(3, Description.lastIndexOf("</p>"));
                    Description = Description.replace(/&nbsp;/g, "");
                }

            }
            if (jQuery.trim(Description) == '') {
                ErrMsg = ErrMsg + '<br> - Template';
            }


            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
            }
            else {
                $('#' + divMsg).hide();
                $.showprogress('Please wait', 'Loading...', '<img src="images/loadingfinal.gif">');
                $.ajax({
                    url: '<%= System.IO.Path.GetFileName(Request.PhysicalPath) %>?id=<%= ID %>',
                    data: $('#frmEmailTemplate').serialize(),
                    type: 'POST',
                    success: function (resp) { $('#' + divMsg).html(resp); ScrollTop(); $.hideprogress(); }
                });
            }
        }
    </script>
</asp:Content>

