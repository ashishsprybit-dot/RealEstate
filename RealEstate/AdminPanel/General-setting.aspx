<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="General-setting.aspx.cs" Inherits="General_setting" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <form id="frmGeneralSetting" action="general-setting.aspx" method="post" target="ifrmForm" enctype="multipart/form-data" onsubmit="return ValidateForm(this);">
        <div class="row">
            <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4><i class="icon-th-large"></i>Site Settings</h4>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body well-lg">
                        <div class="form-group" style="display: none">
                            <label for="<%= txtSiteUrl.ClientID %>">Site URL</label><span class="red">*</span>
                            <input type="text" class="form-control" id="txtSiteUrl" runat="server" name="tbxTitle" maxlength="50" />
                        </div>
                        <div class="form-group col-lg-12">
                            <label for="<%= txtInfoEmail.ClientID %>">
                                User Enquiry/Signup Email</label><span class="red">*</span>
                            <input id="txtInfoEmail" runat="server" name="txtInfoEmail" type="text" maxlength="250" class="form-control" />
                        </div>
                        <%--For Email--%>
                        <div class="form-group">
                            <label for="<%= txtabnno.ClientID %>">Vendor Enquiry/Signup Email</label>
                            <input id="txtabnno" runat="server" type="text" maxlength="50" class="form-control" />
                        </div>


                        <div class="form-group col-lg-12">
                            <label for="<%= txtMailto.ClientID %>">Support & Tech Issue Email</label>
                            <input id="txtMailto" runat="server" name="txtMailto" type="text" maxlength="250" class="form-control" />
                        </div>

                        <%--For Email--%>
                        <div class="form-group" style="display: none">
                            <label for="<%= txtWebsiteName.ClientID %>">
                                Website Name</label><span class="red">*</span>
                            <input id="txtWebsiteName" runat="server" name="txtWebsiteName" type="text" maxlength="250" class="form-control" />
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="<%= txtExternalVedio.ClientID %>">Android Current Version</label>
                                <input id="txtExternalVedio" runat="server" name="txtExternalVedio" type="text" maxlength="100" class="form-control" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="<%= txtcopyright.ClientID %>">
                                    IOS Current Version</label><span></span>
                                <input id="txtcopyright" runat="server" type="text" maxlength="100" class="form-control" />
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="<%= txtExternalLink.ClientID %>">Total Distance Covered in search(KM)</label><span class="red">*</span>
                                <input id="txtExternalLink" runat="server" name="txtExternalLink" type="text" maxlength="10" class="form-control" />
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="<%= txtSite.ClientID %>">Global flat bring me home fees(%)</label>
                                <input id="txtSite" runat="server" name="txtSite" type="text" maxlength="1000" class="form-control" />
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="form-group col-lg-12">
                            <label for="<%= txttwittername.ClientID %>">Flat Transaction Fee($)</label>
                            <input id="txttwittername" runat="server" name="txttwittername" type="text" maxlength="50" class="form-control" />
                        </div>


                        <div class="form-group col-lg-12">
                            <label for="<%= txttwitterfeedurl.ClientID %>">FAQ URL</label>
                            <input id="txttwitterfeedurl" runat="server" name="txttwitterfeedurl" type="text" maxlength="100" class="form-control" />
                        </div>
                        <div class="form-group col-lg-12">
                            <label for="<%= txttelephoneno.ClientID %>">Privay Policy URL</label>
                            <input id="txttelephoneno" runat="server" name="txttelephoneno" type="text" maxlength="50" class="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="<%= txtTradingHrs.ClientID %>">Reedeem Popup text</label><span></span>
                            <textarea name="txtTradingHrs" id="txtTradingHrs" runat="server" style="resize: none;" class="form-control" rows="3"></textarea>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="col-lg-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <h4><i class="icon-th-large"></i>Email Settings</h4>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body well-lg">
                            <div class="form-group">
                                <label for="<%= txtEmailaddress.ClientID %>">Email Address</label><span class="red">*</span>
                                <input id="txtEmailaddress" runat="server" name="txtEmailaddress" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtPassword.ClientID %>">
                                    Password</label><span class="red">*</span>
                                <input id="txtPassword" runat="server" name="txtPassword" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="<%= txtHostName.ClientID %>">
                                    Host Name</label><span class="red">*</span>
                                <input id="txtHostName" runat="server" name="txtHostName" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group custom-check">
                                <label for="<%= chkssl.ClientID %>">Enable SSL</label>
                                <input type="checkbox" runat="server" id="chkssl" />
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <h4><i class="icon-th-large"></i>Rewards Points Settings</h4>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body well-lg">
                            <div class="col-lg-2">
                                <label for="<%= txtPinterest.ClientID %>" style="font-size: 20px">$1 Spent = </label>
                            </div>
                            <div class="col-lg-2">
                                <input id="txtPinterest" runat="server" name="txtPinterest" type="text" maxlength="5" class="form-control" />
                            </div>
                             <div class="col-lg-2">
                                <label style="font-size: 20px">Point</label>
                            </div>


                            <div class="clearfix"></div>
                            <br />
                            <div class="col-lg-2">
                                <label for="<%= txtin.ClientID %>" style="font-size: 20px">50 Points = </label>
                            </div>

                            <div class="col-lg-2">
                                <input id="txtin" runat="server" name="txtin" type="text" maxlength="5" class="form-control" />
                            </div>
                            <div class="col-lg-2">
                                <label style="font-size: 20px">$ Cashback</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-lg-12" style="display: none">
                <div class="panel">
                    <div class="panel-heading">
                        <div class="pull-left">
                            <h4><i class="icon-th-large"></i>Site Settings</h4>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body well-lg">

                        <div class="form-group">
                            <label for="<%= txtYoutube.ClientID %>">Flicker Link</label>
                            <input id="txtYoutube" runat="server" name="txtYoutube" type="text" maxlength="250" class="form-control" />
                        </div>
                        <div class="form-group hide">
                            <label for="<%= txtFacebook.ClientID %>">Facebook Link</label>
                            <input id="txtFacebook" runat="server" name="txtFacebook" type="text" maxlength="250" class="form-control" />
                        </div>
                        <div class="form-group hide">
                            <label for="<%= txtTwitter.ClientID %>">Twitter Link</label>
                            <input id="txtTwitter" runat="server" name="txtTwitter" type="text" maxlength="250" class="form-control" />
                        </div>

                        <div class="form-group hide hide">

                            <label for="<%= txtFooterText.ClientID %>"></label>
                            <input id="txtFooterText" runat="server" name="txtFooterText" type="text" maxlength="250" class="form-control" />


                        </div>


                        <div class="col-lg-6">
                            <div class="form-group" style="display: none">
                                <label for="<%= txtMetaTitle.ClientID %>">Meta Title</label><span class="red">*</span>
                                <input id="txtMetaTitle" runat="server" name="txtMetaTitle" type="text" maxlength="250" class="form-control" />
                            </div>
                            <div class="form-group" style="display: none">
                                <label for="<%= txtMetaKeywords.ClientID %>">Meta Keywords</label><span class="red">*</span>
                                <input name="txtMetaKeywords" id="txtMetaKeywords" runat="server" type="text" class="form-control" maxlength="250" />
                            </div>
                            <div class="form-group" style="display: none">
                                <label for="<%= txtMetaDesc.ClientID %>">Meta Description</label><span class="red">*</span>
                                <input name="txtMetaDesc" id="txtMetaDesc" runat="server" type="text" class="form-control" />
                            </div>


                        </div>
                        <div class="col-lg-6">


                            <div class="form-group">
                                <label for="<%= txtAnalyticCode.ClientID %>">Analytic Code</label>
                                <textarea name="txtAnalyticCode" id="txtAnalyticCode" runat="server" style="resize: none; height: 104px;" class="form-control"></textarea>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="panel-body well-lg">
                <div class="pull-right">
                    <input type="hidden" runat="server" id="hdnContent" name="hdnContent" />
                    <button type="submit" class="btn btn-info" runat="server" id="btnSave" onclick="return ValidateForm(this);">Save Information</button>
                    <%--<button type="button" class="btn btn-default" onclick="window.location='cms-list.aspx';">Cancel</button>--%>
                </div>
            </div>

        </div>
        <div class="row" style="display: none">
            <div class="panel">
                <div class="panel-heading">
                    <div class="pull-left">
                        <h4><i class="icon-th-large"></i>General Settings</h4>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body well-lg">
                    <div class="col-lg-6">

                        <div class="form-group hide">
                            <label for="<%= txtaddressfirstContact.ClientID %>">Contact Address 1</label>
                            <textarea id="txtaddressfirstContact" runat="server" name="txtaddressfirstContact" style="resize: none;" class="form-control"></textarea>
                        </div>
                        <div class="form-group hide">
                            <label for="<%= txtaddresssecondContact.ClientID %>">Contact Address 2</label>
                            <textarea id="txtaddresssecondContact" runat="server" name="txtaddresssecondContact" style="resize: none;" class="form-control"></textarea>
                        </div>

                        <div class="form-group">
                            <label for="<%= txtaddressfirst.ClientID %>">
                                Google Map ContactUs</label>
                            <textarea id="txtaddressfirst" runat="server" rows="3" cols="50" name="txtaddressfirst" style="resize: none;" class="form-control"></textarea>
                        </div>
                    </div>

                    <div class="col-lg-6">

                        <div class="form-group hide">
                            <label>Default CMS Image</label><span class="red">*</span>
                            <input id="fupdImage" runat="server" name="fupdImage" type="file" maxlength="500" class="input-short" />
                            <b>Note:</b>Image size approx.(152px Width x 112px Height)
                        </div>
                        <div class="form-group hide">
                            <img id="imgImage" src="" alt="" runat="server" visible="false" />
                            <img src="images/delete.png" alt="Remove" id="imgFreackle" title="Remove" onclick="RemoveFreackleImage(this)" style="cursor: pointer; display: none;" />
                            <input type="hidden" runat="server" id="hdnCMSFile" name="hdnCMSFile" />
                            <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                        </div>

                        <div class="form-group">
                            <label for="<%= txtContactUs.ClientID %>">Contact Address Footer</label>
                            <textarea id="txtContactUs" runat="server" name="txtContactUs" type="text" maxlength="500" class="form-control" />
                        </div>
                        <div class="form-group hide">
                            <label for="<%= txtaddresssecond.ClientID %>">
                                Google Map Address 2</label>
                            <textarea id="txtaddresssecond" runat="server" name="txtaddresssecond" style="resize: none;" class="form-control"></textarea>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </form>
    <iframe id="ifrmForm" name="ifrmForm" style="display: none; width: 0px; height: 0px;"></iframe>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            if ($('#<%=hdnImage.ClientID%>').val() == '') {
                $('#imgFreackle').hide();
            }
            else {
                $('#imgFreackle').show();
            }

        });
        var divMsg = '<%= divMsg.ClientID %>';
        function ValidateForm(ctrl) {

            var ErrMsg = '';
            ErrMsg += DirValidateCtrl();

            var fupdImage = '<%= fupdImage.ClientID %>';
            if (document.getElementById(fupdImage).value != '') {
                if (!isImage(document.getElementById(fupdImage))) {
                    ErrMsg = ErrMsg + '<br> - ' + 'Please upload only JPG, .JPEG, .PNG, .BMP, .GIF image files.';
                    document.getElementById(fupdImage).value = '';
                }
            }
            else {
                if ($("#<%= hdnImage.ClientID %>").val() == '') {
                    ErrMsg = ErrMsg + '<br> - Upload Image';

                }
            }

            if (ErrMsg.length != 0) {
                var HeaderText = 'Please correct the following error(s):';
                DisplMsg(divMsg, HeaderText + ErrMsg, 'alert-message error');
                ScrollTop();
                return false;
            }
            else {

                $('#ctl00_CPHContent_divMsg').hide();
                return true;
            }
            return true;
        }
        function ScrollTop() {
            $('body,html').animate({
                scrollTop: 0
            }, 800);

        }
        function RemoveFreackleImage(imgFreackle) {
            if (confirm('Are you sure you want to remove advertisement image?')) {
                $(imgFreackle).hide();
                $('#<%=imgImage.ClientID %>').hide();
                $('#<%=hdnImage.ClientID %>').val('');
            }
        }
        function DirValidateCtrl(concatStr) {
            if (concatStr == undefined)
                concatStr = '<br/> - ';
            var lbl;
            var inctrl;
            var ErrMsg = '';
            var IsFirst = 0;
            $('span[class="red"]').each(function () {
                lbl = $(this).prev();
                if ($(this).prev().length > 0) {
                    inctrl = $(lbl).attr('for');
                    console.log(lbl)
                    if (inctrl.length > 0 && $(lbl).attr('for').length > 0) {
                        switch ($('#' + inctrl).attr('type')) {
                            case "textarea":
                            case "text":
                            case "file":
                            case "password":
                                if ($('#' + inctrl).val() == '' && $('#' + inctrl).is(':visible') == true) {
                                    $('#' + inctrl).addClass('valerror');
                                    ErrMsg = ErrMsg + concatStr + stripHTML($(lbl).html());
                                    if (IsFirst == 0)
                                        $('#' + inctrl).focus();
                                    IsFirst = 1;
                                } else
                                    $('#' + inctrl).removeClass('valerror');
                                $('#' + inctrl).attr('style', '');
                                break;
                            case "select":
                                if ($('#' + inctrl).val() == '')
                                    ErrMsg = ErrMsg + concatStr + stripHTML($(lbl).html());
                                else
                                    $('#' + inctrl).removeClass('valerror');
                                break;
                        }
                    }
                }
            });
            return trim(ErrMsg);
        }
    </script>


</asp:Content>

