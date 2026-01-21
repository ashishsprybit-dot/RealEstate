<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="category-modify.aspx.cs"
    Inherits="AdminPanel_Category_Modify" ValidateRequest="false" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/AdminPanel/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <h1 class="page-title"><span id="spnHeader" runat="server">Add Category</span></h1>
    </header>
    <div class="page-section" data-select2-id="166">
        <!-- .card-deck-xl -->
        <div class="card-deck-xl">
            <!-- .card -->
            <div class="card card-fluid" data-select2-id="165">
                <!-- .card-body -->

                <form id="form_Admin" action="category-modify.aspx?id=<%=ID%>" method="post" target="ifrmForm"
                    class="nice custom formlayout need-to-validate" enctype="multipart/form-data">

                    <div class="card-body" data-select2-id="164">
                        <div id="divMsg" runat="server" class="alert alert-danger hide" style="display: none;"></div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="<%=ddlParentCategory.ClientID%>">Parent Category</label>
                                        <select id="ddlParentCategory" name="ddlParentCategory" runat="server" class="form-control ddlParentCategory" onchange="ShowDescription()"></select>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="<%=tbxCategoryName.ClientID%>">Name<abbr title="Required">*</abbr></label>
                                        <input type="text" class="form-control required" id="tbxCategoryName" runat="server" name="tbxCategoryName" maxlength="50" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-6 divSchoolsection" style="display: none">
                                    <table width="100%">
                                        <tr>
                                            <td width="30">
                                                <div class="custom-control custom-checkbox mb-1">
                                                    <input type="checkbox" class="custom-control-input" id="chkSchoolsAll" onclick="SelectAllSchools(this)">
                                                    <label class="custom-control-label" for="chkSchoolsAll">
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                <label>Schools</label>
                                            </td>
                                        </tr>
                                    </table>

                                    <div class="formRow1 form-row1 " style="height: 170px; overflow: auto; overflow-y: auto; border: 1px solid #c6c9d5; border-radius: .25rem; padding: 10px;">

                                        <asp:Repeater ID="rptSchools" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-12 col">
                                                    <div class="custom-control custom-checkbox mb-1">
                                                        <input type="checkbox" class="chkSchools custom-control-input" id="<%#Eval("ID") %>" value="<%#Eval("ID") %>">
                                                        <label class="custom-control-label" for="<%#Eval("ID") %>">
                                                            <%#Eval("Name") %>
                                                        </label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-lg-9 divIcon">
                                    <div class="form-group">
                                        <label for="exampleInputFile">Icon</label><br />
                                        <input type="file" id="fupdImage" runat="server" name="fupdImage" onchange="ValidateSingleInput(this);" />
                                        <br />
                                        <div style="font-size: 13px;"><b>Note :</b> Image size approx.(80px Width x 80px Height) (.JPG, .JPEG, .PNG, .BMP, .GIF, .SVG file only)</div>
                                    </div>

                                    <div class="form-group">
                                        <img id="imgImage" src="" alt="" runat="server" visible="false" height="70" />
                                        <img src="<%=Config.VirtualDir %>adminpanel/images/delete.png" alt="Remove" id="imgCMS" title="Remove" onclick="RemoveCMSImage(this)" style="cursor: pointer; display: none;" />
                                        <input type="hidden" runat="server" id="hdnCMSFile" name="hdnCMSFile" />
                                        <input type="hidden" id="hdnImage" runat="server" name="hdnImage" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                 <div class="col-lg-6 divLayout">
                                     <br />
                                    <div class="form-group">
                                        <label for="<%=ddlLayout.ClientID%>">Layout</label>
                                        <select id="ddlLayout" name="ddlLayout" runat="server" class="form-control ddlLayout">
                                            <option value="1">Vic Curric 1.0 (Content Descriptors)</option>
                                            <option value="2">Vic Curric 2.0 (Skills)</option>
                                        </select>
                                    </div>
                                     
                                </div>
                                 <div class="clearfix"></div>
                                <div class="col-lg-12">
                                    <div class="form-group divDescription" style="display: none">
                                        <label for="<%=tbxDescription.ClientID%>">Description</label>
                                        <textarea class="form-control" id="tbxDescription" runat="server" name="tbxDescription" style="height: 150px;"></textarea>
                                    </div>
                                </div>


                            </div>
                            <div class="col-lg-1"></div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                    </div>
                    <div class="card-body border-top">
                        <input type="hidden" id="hdnPages" name="hdnPages" class="hdnPages" runat="server" value="" />
                        <input type="hidden" id="hdnSchools" name="hdnSchools" class="hdnSchools" runat="server" value="" />
                        <input class="btn btn-primary" runat="server" id="btnSave" type="submit" value="Save Information" name="btnSave" onclick="validateForm()" />
                        <button type="button" class="btn btn-default" onclick="window.location='category-list.aspx';">Cancel</button>
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
                    <p>Please upload .JPG, .JPEG, .PNG, .BMP, .GIF, .SVG file only.</p>
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
    <script type="text/javascript">
        var divMsg = '<%= divMsg.ClientID %>';
        ShowDescription();

        $(document).ready(function () {
             var Schools = jQuery.trim('<%=strSchools%>');

            if (Schools != '') {
                var SchoolsSplit = Schools.split(',');
                for (var i = 0; i < SchoolsSplit.length; i++) {
                    $('.chkSchools').each(function () {
                        if (jQuery.trim($(this).val()) == jQuery.trim(SchoolsSplit[i])) {
                            $(this).prop('checked', true);
                        }
                    });
                }
            }

        });

        function ShowDescription() {
            if ($('.ddlParentCategory option:selected').text().indexOf('----') >= 0) {
                $('.divDescription').show();
                $('.divIcon').hide();
               

            }
            else {
                $('.divDescription').hide();

                if ($('.ddlParentCategory').val() != '0') {
                    $('.divIcon').show();
                }
                else {
                    $('.divIcon').hide();
                }
            }

            if ($('.ddlParentCategory').val() == '0') {
                $('.divSchoolsection').show();
                 $('.divLayout').show();
            }
            else {
                $('.divSchoolsection').hide();
                 $('.divLayout').hide();
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
        function SelectAllSchools(ele) {

            if ($(ele).prop('checked')) {
                $('.chkSchools').prop('checked', true);
            }
            else {
                $('.chkSchools').prop('checked', false);
            }

        }

        function validateForm() {
            var assignedSchools = '';
            if ($('.ddlParentCategory').val() == '0') {
                $('.chkSchools').each(function (index, obj) {
                    if (obj.checked)
                        assignedSchools += obj.value + ",";
                });
                if (assignedSchools.length > 0) {
                    assignedSchools = assignedSchools.substring(assignedSchools, assignedSchools.length - 1);
                }
            }
            $('.hdnSchools').val(assignedSchools);
        }
    </script>
</asp:Content>
