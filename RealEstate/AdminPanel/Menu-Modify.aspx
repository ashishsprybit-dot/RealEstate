<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Menu-Modify.aspx.cs" Inherits="AdminPanel_Menu_Modify" %>
<%@ Import Namespace="Utility" %>
<%@ Register Src="~/AdminPanel/includes/editor.ascx" TagName="uctrlContent" TagPrefix="uctrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" Runat="Server">
        <div class="row add-padding">
        <div class="pull-left">
            <h1 id="PageTitle" runat="server"></h1>
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx" class="text-transparent"><i class="icon-home"></i>&nbsp;&nbsp;Home</a></li>
                <li class="active"><a href="javascript:;" class="text-transparent" id="lnkSubmenu" runat="server">Menu Modify</a></li>
            </ol>
        </div>
    </div>
    <!-- row 2 -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body well-lg">
                    <form id="frmMenu" action="menu-modify.aspx" onsubmit="MenuSave(this);return false;">
                        <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
                        <div class="form-group">
                            <label for="<%= tbxMenuTitle.ClientID %>">Menu Title</label><span class="red">*</span>
                            <input type="text" class="form-control" id="tbxMenuTitle" runat="server" name="tbxMenuTitle" maxlength="50" />
                        </div>
                        <div class="form-group">
                            <label>URL</label>
                            <input type="text" class="form-control" id="tbxMenuURL" runat="server" name="tbxMenuURL" maxlength="250" />
                        </div>
                        <div class="form-group">
                            <label> Parent Menu</label>
                            <select id="ddlParentMenu" runat="server" class="drp-border-full"></select>
                        </div>
                        
                        
                        <div class="form-group">
                            <label>Link Type</label> &nbsp;
                            <input type="radio" runat="server" name="MenuType" id="rdNone" />None &nbsp;
                            <input type="radio" runat="server" name="MenuType" id="rdCMS" />Internal Link &nbsp;
                            <input type="radio" runat="server" name="MenuType" id="rdExternal" />External Link
                        </div>
                        <div class="form-group" id="trCMSURL" runat="server">
                            <div id="divSelectModule" style="overflow: auto; overflow-x: hidden; height: 200px; float: left; width: 100%; margin: 10px 0 10px 0; border: 1px solid #ABADB3;">
                                <asp:Repeater ID="rptModuleList" runat="server">
                                    <HeaderTemplate>
                                        <table class="table">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="5">
                                                <input type="radio" id='<%# Eval("ID") %>' rel='<%# Eval("PageUrl")%>' name="rbSelect" class="CMSID" onclick="AssignMenuLink(this);" />
                                            </td>
                                            <td align="left" class="tdColor">
                                                <%# Eval("PageTitle")%> <input type="hidden" id='hdnLink<%#  Eval("ID") %>' name="hdnLink<%# Eval("ID") %>" value="<%# Eval("PageUrl") %>" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="form-group hide">
                            <label for="<%= tbxCMSMenuLink.ClientID %>">URL</label>
                            <input class="form-control" id="tbxCMSMenuLink" runat="server" readonly="readonly" />
                            </div>
                        </div>
                        

                        <div class="form-group" id="trExternal" runat="server" >
                            <label> External URL</label> 
                            <input id="tbxExternalMenuLink" runat="server" class="form-control" maxlength="250"/>&nbsp;(e.g. http://www.example.com)
                        </div>

                        <div class="form-group">
                            <label>Position</label>
                            <input type="radio" name="rdoPosition" id="rdoNone" checked="true" value="-1" />None
                            <input type="radio" name="rdoPosition" id="rdoTop" value="0" />Top
                            <input type="radio" name="rdoPosition" id="rdoBottom" value="1" />Bottom
                            <input type="radio" name="rdoPosition" id="rdoBoth" value="2" /> Both
                        </div>
                        <div class="form-group" style="display:none;">
                            <label>Show in Menu?</label>
                            <input type="radio" name="rdoShowMenu" id="rdShowMenuYes" value="1" runat="server" />Yes
                            <input type="radio" name="rdoShowMenu" id="rdShowMenuNo" value="0" runat="server" />No
                         </div>

                         
                         
                        <div class="pull-right">   
                            <input type="hidden" runat="server" id="hdnLinkType" name="hdnLinkType" />
                            <input type="hidden" id="hdnShowInMenu" name="hdnShowInMenu" runat="server" />
                            <input type="hidden" id="hdnStaticUrl" name="hdnStaticUrl" runat="server" />
                            <input type="hidden" id="hdnParentID" name="hdnParentID" runat="server" />
                            <input type="hidden" id="hdnExternallink" name="hdnExternallink" runat="server" />
                            <input type="hidden" id="hdnCMSID" name="hdnCMSID" runat="server" />
                            <input type="hidden" id="hdnMenuPromptLink" name="hdnMenuPromptLink" runat="server" />  
                            <input type="hidden" id="hdnorderno" name="hdnorderno" runat="server" />
                            <input type="hidden" id="hdnlevelno" name="hdnlevelno" runat="server" />                   
                                
                            <button type="submit" class="btn btn-primary" runat="server" id="btnSave" onclick="ValidateForm(this);">Save Information</button>                             
                            <button type="button" class="btn btn-default" onclick="window.location='menu-list.aspx';" >Cancel</button>
                        </div>
                         
                    </form>
                    
                </div>

            </div>
            <!-- / panel  -->
        </div>
    </div>
    <!-- / row 2 -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
    <script type="text/javascript" >        
        var url = 'menu-modify.aspx?id=<%= ID %>&type=<%= Request["type"] %>&parentid=<%= Request["parentid"] %>&orderno=<%= Request["orderno"] %>';
        var divMsg = '<%= divMsg.ClientID %>';

        function AssignMenuLink(ctrl) {
            $('#<%=tbxCMSMenuLink.ClientID %>').val($(ctrl).attr('rel'));
            $('#<%=hdnMenuPromptLink.ClientID %>').val($('#<%=tbxCMSMenuLink.ClientID %>').val());
            $('#<%=hdnCMSID.ClientID %>').val($(ctrl).attr('id'));
            if ($('#<%=hdnMenuPromptLink.ClientID %>').val() == "Extenal Link") {
                $('#<%=tbxCMSMenuLink.ClientID %>').attr('readonly', '');
                $('#<%=tbxCMSMenuLink.ClientID %>').val('');
                $('#<%=hdnExternallink.ClientID %>').val('1');
            }
            else {
                $('#<%=tbxCMSMenuLink.ClientID %>').attr('readonly', 'readonly');
                $('#<%=hdnExternallink.ClientID %>').val('0');
            }
            AssignColor(ctrl);
        }
        function AssignColor(ctrl) {
            $('.tdColor').css('background-color', '#ffffff');
            $('.tdColor').css('color', '#3EB0AD');

            $(ctrl).parent().parent().find('td:last').css('background-color', 'green');
            $(ctrl).parent().parent().find('td:last').css('color', '#ffffff');

        }
        $(document).ready(function () {
            if (parseInt('<%=ID %>') > 0) {
                $('.CMSID').each(function () {
                    if ($('#<%=hdnCMSID.ClientID %>').val() == '-1') {
                        if ($(this).attr('rel') == $('#<%=hdnStaticUrl.ClientID %>').val()) {
                            $(this).prop('checked', true);
                            $('#<%=tbxCMSMenuLink.ClientID %>').val($(this).attr('rel'));
                            AssignColor(this);
                        }
                    }
                    else if ($(this).attr('id') == $('#<%=hdnCMSID.ClientID %>').val()) {
                        $(this).prop('checked', true);
                        $('#<%=tbxCMSMenuLink.ClientID %>').val($(this).attr('rel'));
                        AssignColor(this);
                    }
                });
            if (parseInt('<%=intPosition %>') == 0) {  $('#rdoTop').prop('checked', true);   }
            else if (parseInt('<%=intPosition %>') == 1) { $('#rdoBottom').prop('checked', true); }
            else if (parseInt('<%=intPosition %>') == -1) { $('#rdoNone').prop('checked', true); }
                else {  $('#rdoBoth').prop('checked', true);  }

            }
            else {
                $('#<%=hdnCMSID.ClientID %>').val('0');
            }

            if ($('#<%= rdNone.ClientID %>').prop('checked')) {
                $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                $('#<%=trExternal.ClientID %>').css('display', 'none');


            }
            if ($('#<%= rdCMS.ClientID %>').prop('checked')) {
                $('#<%=trCMSURL.ClientID %>').css('display', '');
                $('#<%=trExternal.ClientID %>').css('display', 'none');
                $('#<%=hdnExternallink.ClientID %>').val('0');
            }

            if ($('#<%= rdExternal.ClientID %>').prop('checked')) {
                $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                $('#<%=trExternal.ClientID %>').css('display', '');
                $('#<%=hdnExternallink.ClientID %>').val('1');


            }
            $('#<%= rdNone.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=hdnLinkType.ClientID %>').val('0');
                    $('#<%=hdnExternallink.ClientID %>').val('0');
                    $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                    $('#<%=trExternal.ClientID %>').css('display', 'none');
                }
            });

            $('#<%= rdCMS.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=hdnLinkType.ClientID %>').val('1');
                    $('#<%=trCMSURL.ClientID %>').css('display', '');
                    $('#<%=trExternal.ClientID %>').css('display', 'none');
                    $('#<%=hdnExternallink.ClientID %>').val('0');


                }
            });
            $('#<%= rdExternal.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=hdnLinkType.ClientID %>').val('2');
                    $('#<%=trCMSURL.ClientID %>').css('display', 'none');
                    $('#<%=trExternal.ClientID %>').css('display', '');
                    $('#<%=hdnExternallink.ClientID %>').val('1');

                }
            });

            $('#<%= rdShowMenuYes.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=hdnShowInMenu.ClientID %>').val('1');
                }
            });
            $('#<%= rdShowMenuNo.ClientID %>').change(function () {
                if ($(this).prop('checked')) {
                    $('#<%=hdnShowInMenu.ClientID %>').val('0');
                }
              });
        });

        function MenuSave(ctrl) {

            var ErrMsg = '';
            if (jQuery.trim($('#<%=tbxMenuTitle.ClientID %>').val()) == '') {
                ErrMsg += '<br/> - Menu Title';
            }

            if ($('#<%= rdCMS.ClientID %>').prop('checked')) {
                if (jQuery.trim($('#<%=tbxCMSMenuLink.ClientID %>').val()) == '') {
                    ErrMsg += '<br/> - Internal Link';
                }
            }
            if ($('#<%= rdExternal.ClientID %>').prop('checked')) {
                if (jQuery.trim($('#<%=tbxExternalMenuLink.ClientID %>').val()) == '') {
                    ErrMsg += '<br/> - External URL';
                }
                else {
                    if (!isValidURL(jQuery.trim($('#<%=tbxExternalMenuLink.ClientID %>').val()))) {
                        ErrMsg += '<br/> - Please Enter Valid External URL.';
                    }
                }
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
                    url: url,
                    data: $('#frmMenu').serialize(),
                    type: 'POST',
                    success: function (json) {
                        $('#' + divMsg).html(json); $.hideprogress();
                        ScrollTop();
                    }
                });
            }
        }
    </script>
</asp:Content>

