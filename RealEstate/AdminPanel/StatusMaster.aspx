<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="StatusMaster.aspx.cs" Inherits="AdminPanel_StatusMaster" %>

<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">

    <header class="page-title-bar">
        <div class="d-flex justify-content-between">
            <h1 class="page-title">Status Master</h1>
            <div class="btn-toolbar">
                <button type="button" style="margin-right: 20px;" class="bmd-modalButton btn btn-warning btnChangeOrder" data-toggle="modal" data-bmdsrc="<%=Utility.Config.WebSiteUrl %>status-order.aspx" data-bmdwidth="1000" data-bmdheight="900" data-target="#SequenceModal" data-bmdvideofullscreen="true">Change Status Sequence</button>

                <button type="button" class="btn btn-primary" data-toggle="modal" onclick="$('#StatusID').val('0');$('#tbxStatus').val('');" data-target="#myModal">Add Status </button>
            </div>
        </div>
    </header>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add Status</h4>
                </div>
                <div class="modal-body">
                    <div class="alert alert-danger hide" id="divMsg" runat="server"></div>
                    <input type="hidden" id="StatusID" name="StatusID" value="0" />
                    <input type="text" id="tbxStatus" class="form-control required" autofocus name="tbxStatus" placeholder="Enter Status" autocomplete="off" />
                </div>
                <div class="modal-footer">
                    <input class="btn btn-primary" runat="server" id="btnSave" type="button" value="Save" name="btnSave" onclick="Save()" />
                    <button type="button" class="btn btn-default" onclick="Close()" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="alert alert-danger hide" id="divDelete" runat="server"></div>

    <div class="page-section">
        <div class="card card-fluid">
            <div class="card-body">
                <table id="myTable" class="table">
                    <thead>
                        <tr>
                            <th>Status Type</th>
                            <th width="70" class="text-center">Edit</th>
                            <th width="70" class="text-center">Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptStatus" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Status") %></td>
                                    <td class="text-center">
                                        <a class="btn btn-sm btn-icon btn-secondary" href="javascript:void(0)" onclick="ShowModal(<%# Eval("StatusID") %>, '<%# Eval("Status") %>')"><i class="fa fa-pencil-alt"></i></a>
                                    </td>
                                    <td class="text-center">
                                        <a class="btn btn-sm btn-icon btn-secondary" href="javascript:void(0)" onclick="Delete(<%# Eval("StatusID") %>)"><i class="fa fa-trash-alt"></i></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="modal fade" id="SequenceModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content bmd-modalContent" style="min-height: 550px;">

                <div class="modal-body">

                    <div class="close-button">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="embed-responsive embed-responsive-16by9" style="min-height: 510px;">
                        <iframe class="embed-responsive-item" frameborder="0"></iframe>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var divMsg = '<%= divMsg.ClientID %>';
        var divDelete = '<%= divDelete.ClientID %>';

        function Save() {
            if ($('#tbxStatus').val() != '') {
                $.ajax({
                    url: 'StatusMaster.aspx',
                    type: 'POST',
                    data: { 'Save': 'y', 'Status': $('#tbxStatus').val(), 'StatusID': $('#StatusID').val() },
                    success: function (resp) {
                        if (resp == "success") {
                            $('#tbxStatus').val('');
                            $('#StatusID').val('0');
                            DisplMsg(divMsg, 'Status saved successfully.', 'alert-message success');
                            window.setTimeout('Reload()', 1000);
                        } else {
                            DisplMsg(divMsg, 'Status already exists.', 'alert-message error');
                        }
                    }
                });
            } else {
                alert("Please enter Status.");
            }
        }

        function Reload() {
            window.location.href = 'StatusMaster.aspx';
        }

        function ShowModal(id, name) {
            $('#StatusID').val(id);
            $('#tbxStatus').val(name);

            var myModal = new bootstrap.Modal(document.getElementById('myModal'));
            myModal.show();

        }

        function Close() {
            $('#tbxStatus').val('');
            $('#StatusID').val('0');
        }

        function Delete(ID) {
            if (confirm('Are you sure you want to delete this Status?')) {
                $.ajax({
                    url: 'StatusMaster.aspx',
                    type: 'POST',
                    data: { 'Delete': 'y', 'ID': ID },
                    success: function (resp) {
                        if (resp == "success") {
                            DisplMsg(divDelete, 'Status deleted successfully.', 'alert-message success');
                            window.setTimeout('Reload()', 1000);
                        }
                    }
                });
            }
        }
    </script>
    <script>
        (function ($) {

            $.fn.bmdIframe = function (options) {
                var self = this;
                var settings = $.extend({
                    classBtn: '.bmd-modalButton',
                    defaultW: 1000,
                    defaultH: 700
                }, options);
                console.log(settings)
                $(settings.classBtn).on('click', function (e) {
                    var allowFullscreen = $(this).attr('data-bmdVideoFullscreen') || false;

                    var dataVideo = {
                        'src': $(this).attr('data-bmdSrc'),
                        'height': $(this).attr('data-bmdHeight') || settings.defaultH,
                        'width': $(this).attr('data-bmdWidth') || settings.defaultW
                    };

                    if (allowFullscreen) dataVideo.allowfullscreen = "";

                    // stampiamo i nostri dati nell'iframe
                    $(self).find("iframe").attr(dataVideo);
                });

                // se si chiude la modale resettiamo i dati dell'iframe per impedire ad un video di continuare a riprodursi anche quando la modale è chiusa
                this.on('hidden.bs.modal', function () {
                    $(this).find('iframe').html("").attr("src", "");
                });

                return this;
            };

        })(jQuery);

        $(document).ready(function () {
            jQuery("#SequenceModal").bmdIframe();
        });
    </script>
</asp:Content>

