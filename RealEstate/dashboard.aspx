<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<%@ Import Namespace="Utility" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">

        <div class="d-flex justify-content-between">
            <h1 class="page-title">Dashboard</h1>
           <%-- <div class="btn-toolbar">
                <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" Text="Add New" OnClientClick="window.location.href='new-lead.aspx'; return false;" />
            </div>--%>
        </div>
    </header>

    <div id="divMsg" runat="server" visible="false" class="alert" style="display: none;"></div>

<div class="row">
    <div class="col-12">
        <div id="divStatusCards" runat="server" class="status-card-wrapper"></div>
    </div>
</div>



    <asp:HiddenField ID="hfSelectedStatus" runat="server" />

    <div class="page-section">
        <div class="card card-fluid">
                <div class="row">
            </div>
    </div>

    <div id="remarksModal" class="modal" tabindex="-1" role="dialog" style="display: none;">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">

                <div class="modal-header" style="padding-bottom: 3px;">
                    <h5 class="modal-title">Remarks</h5>

                    <button type="button" class="close" onclick="closeRemarksModal()">&times;</button>
                </div>

                <div class="modal-body d-flex flex-column" style="height: 400px; padding-bottom: 1.5rem;">
                    <span class="spnDescription" style="background: #f6f7f9 !important; padding: 10px 10px 10px 10px; margin-bottom: 10px;"></span>

                    <div id="remarksList" class="flex-grow-1 mb-3" style="overflow-y: auto;">
                        <div class="text-muted text-center" id="noRemarksText">No remarks yet.</div>
                    </div>

                    <div class="d-flex mt-auto gap-2">
                        <input type="hidden" id="hdnCurrentLeadId" name="hdnCurrentLeadId" class="hdnCurrentLeadId" />
                        <input type="text" id="newRemark" class="form-control" placeholder="Type your message..." style="margin-right: 10px" />
                        <button class="btn btn-primary" id="btnSaveRemark" type="button" disabled>Send</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="historyModal" class="modal" tabindex="-1" style="display: none;">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Lead History</h5>
                    <div class="clearfix"></div>
                    <hr />
                    <button type="button" class="close" onclick="$('#historyModal').hide();">&times;</button>
                </div>
                <div class="modal-body" id="historyList" style="max-height: 400px; overflow-y: auto;">
                </div>
                <div class="text-center text-muted" id="noHistoryText" style="display: none">
                    No history found.<br />
                    <br />
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
    <%-- <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>--%>
    <script>
        var currentLeadId = 0;

        $(document).ready(function () {
            $("#searchInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#myTable tbody tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });

            $('.open-remarks').click(function () {
                const leadId = $(this).data('id');
                loadRemarks(leadId);
                if ($(this).data('desc') != '') {
                    $('.spnDescription').html('<b>Description: </b>' + $(this).data('desc'));
                    $('.spnDescription').show();
                }
                else {
                    $('.spnDescription').hide();
                }
            });

            $('#btnSaveRemark').click(saveRemark);

            $(document).on('change', '.ddl-status', function () {
                var leadId = $(this).closest("tr").find("input[id*='hfLeadId']").val();
                var newStatus = $(this).val();

                if (newStatus === "") return;

                $.ajax({
                    type: "POST",
                    url: "dash-board.aspx/UpdateStatus",
                    data: JSON.stringify({ leadId: leadId, status: newStatus }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error: function () {
                        alert("Error while updating status.");
                    }
                });
            });
        });

        $(document).ready(function () {
            $('#newRemark').on('input', function () {
                const value = $(this).val().trim();
                $('#btnSaveRemark').prop('disabled', value === '');
            });
        });

        function closeRemarksModal() {
            $('#remarksModal').hide();
            $('#remarksList').html('');
            $('#newRemark').val('');
        }

        function loadRemarks(leadId) {
            currentLeadId = leadId;
            $('.hdnCurrentLeadId').val(currentLeadId);
            $.ajax({
                type: "POST",
                url: "dash-board.aspx/GetRemarks",
                data: JSON.stringify({ leadId: leadId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const remarks = response.d || [];
                    const container = $('#remarksList');
                    container.html('');
                    remarks.forEach(function (item) {
                        $('#remarksList').append(`
                        <div class="mb-3" style="font-size: 0.9rem;">
                            <div>${item.RemarkText}</div>
                            <div><small class="text-muted">${item.UserID} • ${item.RemarkDate}</small></div>
                        </div>
                    `);
                    });

                    $('#remarksModal').show();
                },
                error: function () {
                    alert("Error loading remarks.");
                }
            });
        }

        function saveRemark() {
            const remark = $('#newRemark').val();
            if (!remark.trim()) return;

            currentUserId = $('.hdnCurrentLeadId').val();

            $.ajax({
                type: "POST",
                url: "dash-board.aspx/SaveRemark",
                data: JSON.stringify({ leadId: currentLeadId, remarkText: remark, userId: currentUserId }),
                contentType: "application/json;",
                dataType: "json",
                success: function () {
                    $('#newRemark').val('');
                    loadRemarks(currentLeadId);
                },
                error: function () {
                    alert("Error saving remark.");
                }
            });
        }

        $('.open-history').click(function () {
            var leadId = $(this).data('id');

            $.ajax({
                type: "POST",
                url: "dash-board.aspx/GetLeadHistory",
                data: JSON.stringify({ leadId: leadId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var history = response.d;
                    var container = $('#historyList');
                    container.html('');

                    if (history.length === 0) {
                        $('#noHistoryText').show();
                    } else {
                        $('#noHistoryText').hide();

                        history.forEach(function (item) {
                            var changeText = "";
                            if (item.OldValue !== item.NewValue) {
                                changeText = `<div>Changed from "${item.OldValue}" to "${item.NewValue}"</div>`;
                            }
                            else {
                                changeText = `<div>${item.NewValue}</div>`;
                            }

                            container.append(`
                        <div class="mb-3">
                            <div style="color: #007bff; font-weight: bold;">${item.FieldName} Updated</div>
                            <div><small>By: ${item.UpdatedBy}</small></div>
                            <div><small>Date: ${item.UpdatedDate}</small></div>
                             ${changeText}
                            <hr />
                        </div>
                    `);
                        });
                    }

                    $('#historyModal').show();
                },
                error: function () {
                    alert("Failed to load history.");
                }
            });
        });

        window.onload = function () {
            var msgDiv = document.getElementById('<%= divMsg.ClientID %>');
            if (msgDiv && msgDiv.innerHTML.trim() !== "") {
                msgDiv.style.display = "block";

                setTimeout(function () {
                    msgDiv.style.display = "none";

                    if (window.history.replaceState) {
                        const cleanUrl = window.location.protocol + "//" + window.location.host + window.location.pathname;
                        window.history.replaceState({}, document.title, cleanUrl);
                    }
                }, 2000);
            }
        };

        document.addEventListener('keydown', function (event) {
            if (event.key === "Escape") {
                let modal = document.getElementById('remarksModal');
                if (modal.style.display === 'block') {
                    closeRemarksModal();
                }

                let hmodal = document.getElementById('historyModal');
                if (hmodal.style.display === 'block') {
                    $('#historyModal').hide();
                }

            }
        });

        function filterByStatus(card) {
            var status = $(card).attr('data-status');
            $('#<%= hfSelectedStatus.ClientID %>').val(status);
           __doPostBack('ShowLeadsByStatus', status);
           $('.status-card').removeClass('active');
           $(card).addClass('active');
        }

        $(document).ready(function () {
            var selectedStatus = $('#<%= hfSelectedStatus.ClientID %>').val();
            $('.status-card').each(function () {
                if ($(this).attr('data-status') === selectedStatus) {
                    $(this).addClass('active');
                } else {
                    $(this).removeClass('active');
                }
            });
        });

        function filterByStatus(card) {
            var status = $(card).attr('data-status');
            if (status) {
                window.location.href = 'lead-list.aspx?status=' + encodeURIComponent(status);
            }
        }

        function fetchNotifications() {
            $.ajax({
                type: "POST",
                url: "dash-board.aspx/GetNotifications",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var messages = response.d;
                    if (messages.length > 0) {
                        showNotificationsSequentially(messages);
                    }
                }
            });
        }

        function showNotificationsSequentially(messages) {
            let index = 0;

            function showNext() {
                if (index >= messages.length) return;

                showToast(messages[index], function () {
                    index++;
                    setTimeout(showNext, 1000);
                });
            }

            showNext();
        }

        function showToast(message, onHide) {
            var toast = $('<div class="toast-popup"><span class="toast-icon">&#128276;</span> <span class="toast-message">' + message + '</span></div>');
            $("body").append(toast);

            toast.css({
                position: "fixed",
                bottom: "100px",
                right: "30px",
                background: "#006ec2",
                color: "#fff",
                padding: "20px 30px",
                borderRadius: "12px",
                boxShadow: "0 8px 20px rgba(0,0,0,0.2)",
                zIndex: 9999,
                cursor: "pointer",
                fontSize: "16px",
                maxWidth: "400px",
                lineHeight: "1.5",
                fontFamily: "Segoe UI, Arial, sans-serif",
                fontWeight: "500",
                letterSpacing: "0.3px",
                textAlign: "left",
                display: "flex",
                alignItems: "center"
            });

            toast.find('.toast-icon').css({
                marginRight: "12px",
                fontSize: "20px"
            });

            toast.click(function () {
                window.location.href = "lead-list.aspx";
            });

            setTimeout(function () {
                toast.fadeOut(700, function () {
                    $(this).remove();
                    if (typeof onHide === "function") {
                        onHide();
                    }
                });
            }, 7000);
        }

        setInterval(fetchNotifications, 3000);

    </script>
</asp:Content>

