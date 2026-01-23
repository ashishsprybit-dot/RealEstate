<%@ Page Title="Leads" Language="C#" MasterPageFile="~/Main.master"
    AutoEventWireup="true" CodeFile="lead-list.aspx.cs" Inherits="lead_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="server">

    <header class="page-title-bar mb-3">
    <div class="d-flex justify-content-between align-items-center">
        <h1 class="page-title mb-0">Leads</h1>

        <a href="new-lead.aspx" class="btn btn-success btn-sm">
            <i class="fa fa-plus"></i> Add New
        </a>
    </div>
</header>


    <div class="page-section">
        <div class="card card-fluid">
            <div class="card-body">

                <div class="row mb-3 align-items-center">
                    <div class="col-md-4">
                        <input type="text"
                            id="searchInput"
                            class="form-control"
                            placeholder="Search by Scheme, Requirement, Customer or Contact" />
                    </div>

                    <div class="col-md-3">
                        <asp:DropDownList
                            ID="ddlStatus"
                            runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    
                </div>


                <table id="myTable" class="table table-hover table-bordered align-middle">
                    <thead class="table-light">
                        <tr>
                            <th>Scheme Name</th>
                            <th>Customer</th>
                            <th>Contact</th>
                            <th>Loan Amount</th>
                            <th>Requirement</th>
                            <th>Status</th>
                            <th>Created By</th>
<%--                            <th class="text-center">Remarks</th>
                            <th class="text-center">History</th>--%>
                            <th class="text-center">Edit</th>
                            <th class="text-center">Delete</th>
                        </tr>
                    </thead>


                    <tbody>
                     <asp:Repeater ID="rptLeads" runat="server" OnItemDataBound="rptLeads_ItemDataBound" OnItemCommand="rptLeads_ItemCommand">


                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("SchemeName") %></td>
                                    <td><%# Eval("CustomerName") %></td>
                                    <td><%# Eval("ContactNumber") %></td>
                                    <td><%# Eval("LoanAmount") %></td>
                                    <td><%# Eval("RequirementName") %></td>
                                    <%--<td>
                                        <asp:HiddenField ID="hfRequirementID" runat="server" />
                                        <asp:DropDownList ID="ddlRowRequirement" runat="server" CssClass="form-control requirement-dd">
                                        </asp:DropDownList>
                                    </td>--%>

                                    <td>
                                        <asp:DropDownList
                                         ID="ddlRowStatus"
                                         runat="server"
                                         CssClass="form-control"
                                         AutoPostBack="true"
                                         OnSelectedIndexChanged="ddlRowStatus_SelectedIndexChanged">
                                     </asp:DropDownList>
                                     
                                     <asp:HiddenField
                                         ID="hfLeadID"
                                         runat="server"
                                         Value='<%# Eval("LeadID") %>' />
                                     
                                    </td>

                                    <td><%# Eval("CreatedByName") %></td>
                                    <%--<td class="text-center">
                                        <a href="javascript:void(0);" class="icon-btn open-remarks"
                                            title="Remarks"
                                            data-id='<%# Eval("LeadID") %>'
                                            data-desc='<%# Eval("Description") %>'>
                                            <i class="fa fa-comments"></i>
                                        </a>
                                    </td>
                                    <td class="text-center">
                                        <a class="icon-btn open-history" data-id="<%# Eval("LeadID") %>">
                                            <i class="fa fa-history"></i>
                                        </a>
                                    </td>--%>
                                    <td class="text-center">
                                        <a href='new-lead.aspx?LeadID=<%# Eval("LeadID") %>' class="icon-btn">
                                            <i class="fa fa-edit"></i>
                                        </a>
                                    </td>
                                    <td class="text-center">
                                        <asp:LinkButton
                                        ID="btnDelete"
                                        runat="server"
                                        CommandName="DeleteLead"
                                        CommandArgument='<%# Eval("LeadID") %>'
                                        CssClass="icon-btn"
                                        OnClientClick="return confirm('Are you sure you want to delete this lead?');">
                                        <i class="fa fa-trash text-danger"></i>
                                    </asp:LinkButton>

                                    </td>

                                </tr>
                            </ItemTemplate>

                        </asp:Repeater>
                    </tbody>
                </table>

                <div id="remarksModal" class="modal" tabindex="-1" role="dialog" style="display: none; background: rgba(0,0,0,0.5);">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="padding-bottom: 3px;">
                                <h5 class="modal-title">Remarks</h5>
                                <button type="button" class="close" onclick="closeRemarksModal()">&times;</button>
                            </div>
                            <div class="modal-body d-flex flex-column" style="height: 400px; padding-bottom: 1.5rem;">
                                <span class="spnDescription" style="background: #f6f7f9 !important; padding: 10px; margin-bottom: 10px; display: none; border-radius: 4px;"></span>

                                <div id="remarksList" class="flex-grow-1 mb-3" style="overflow-y: auto;">
                                    <div class="text-muted text-center" id="noRemarksText">No remarks yet.</div>
                                </div>

                                <div class="d-flex mt-auto gap-2">
                                    <input type="hidden" id="hdnCurrentLeadId" class="hdnCurrentLeadId" />
                                    <input type="text" id="txtNewRemarkModal" class="form-control" placeholder="Type your message..." style="flex: 1; margin-right: 10px;" />
                                    <button class="btn btn-primary" id="btnSaveRemark" type="button" disabled>Send</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div id="historyModal" class="modal" style="display: none;">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Lead History</h5>
                    <button type="button" class="close" onclick="$('#historyModal').hide();">&times;</button>
                </div>

                <div class="modal-body" id="historyList"
                    style="max-height: 400px; overflow-y: auto;">
                </div>

            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="server">

<script>
    let currentLeadId = 0;

    $(document).ready(function () {
        $("#searchInput").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#myTable tbody tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
            });
        });

        $(document).on('click', '.open-remarks', function () {
            currentLeadId = $(this).data('id');
            const desc = $(this).data('desc');

            if (desc && desc.trim() !== "") {
                $('.spnDescription').html('<b>Description: </b>' + desc).show();
            } else {
                $('.spnDescription').hide();
            }

            $('#hdnCurrentLeadId').val(currentLeadId);
            loadRemarks(currentLeadId); 
        });

        $(document).on('click', '.open-history', function () {
            let leadId = $(this).data('id');
            loadHistory(leadId);
        });

        $('#btnSaveRemark').click(function () {
            saveRemark();
        });

        $('#txtNewRemarkModal').on('keypress', function (e) {
            if (e.which === 13 && !$('#btnSaveRemark').is(':disabled')) {
                e.preventDefault();
                saveRemark();
            }
        }).on('input', function () {
            const value = $(this).val().trim();
            $('#btnSaveRemark').prop('disabled', value === '');
        });
    });


    function loadRemarks(leadId) {
        $.ajax({
            type: "POST",
            url: "lead-list.aspx/GetRemarks",
            data: JSON.stringify({ leadId: leadId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                const remarks = response.d || [];
                const container = $('#remarksList');
                container.html('');

                if (remarks.length === 0) {
                    container.html('<div class="text-muted text-center" id="noRemarksText">No remarks yet.</div>');
                } else {
                    remarks.forEach(function (item) {
                        container.append(`
                            <div class="mb-3" style="font-size: 0.9rem; border-bottom: 1px solid #eee; padding-bottom: 5px;">
                                <div>${item.RemarkText}</div>
                                <div><small class="text-muted">${item.UserID} • ${item.RemarkDate}</small></div>
                            </div>`);
                    });
                }
                $('#remarksModal').show(); 
                container.scrollTop(container[0].scrollHeight);
            },
            error: function () {
                alert("Error loading remarks.");
            }
        });
    }

    function saveRemark() {
        const remark = $('#txtNewRemarkModal').val();
        if (!remark.trim()) return;

        $.ajax({
            type: "POST",
            url: "lead-list.aspx/SaveRemark",
            data: JSON.stringify({ leadId: currentLeadId, remarkText: remark }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                $('#txtNewRemarkModal').val('');
                $('#btnSaveRemark').prop('disabled', true);
                loadRemarks(currentLeadId);
            }
        });
    }

    function loadHistory(leadId) {
        $.ajax({
            type: "POST",
            url: "lead-list.aspx/GetLeadHistory",
            data: JSON.stringify({ leadId: leadId }),
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                $('#historyList').html('');
                if (res.d && res.d.length > 0) {
                    res.d.forEach(x => {
                        $('#historyList').append(
                            `<div class="mb-2">
                                <b>${x.FieldName}</b><br/>
                                ${x.OldValue} → ${x.NewValue}
                             </div><hr/>`
                        );
                    });
                } else {
                    $('#historyList').html('<div class="text-center">No history found.</div>');
                }
                $('#historyModal').show();
            }
        });
    }

    function closeRemarksModal() {
        $('#remarksModal').hide();
        $('#remarksList').html('');
        $('#txtNewRemarkModal').val('');
    }

        $(document).ready(function () {
        if (typeof (theForm) !== "undefined") {
            theForm.action = "lead-list.aspx";
        }
    });


    //$(document).on('change', '.status-dd', function () {
    //    let $ddl = $(this);
    //    let leadId = $ddl.data('lead-id');
    //    let statusId = $ddl.val();

    //    if (leadId && statusId && statusId !== "0") {
    //        updateField("UpdateLeadStatus", {
    //            leadId: parseInt(leadId),
    //            statusId: parseInt(statusId)
    //        });
    //    }
    //});


    //$(document).on('change', '.requirement-dd', function () {
    //    let $ddl = $(this);
    //    let leadId = $ddl.attr('data-leadid'); 
    //    let requirementId = $ddl.val();

    //    console.log("LeadID:", leadId, "ReqID:", requirementId); 

    //    if (leadId && requirementId && requirementId !== "0") {
    //        updateField("UpdateLeadRequirement", {
    //            leadId: parseInt(leadId),
    //            requirementId: parseInt(requirementId)
    //        });
    //    }
    //});

    //function updateField(method, data) {
    //    $.ajax({
    //        type: "POST",
    //        url: "/lead-list.aspx/" + method,
    //        data: JSON.stringify(data),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function () {
    //            toastr.success("Requirement updated successfully");

                
    //            if (method === "UpdateLeadStatus") {
    //                setTimeout(function () { location.reload(); }, 500);
    //            }
    //        },
    //        error: function (xhr) {
    //            console.error(xhr.responseText);
    //            toastr.error("Update failed.");
    //        }
    //    });
    //}
    //    $(document).ready(function () {
    //    if (typeof (theForm) !== 'undefined') {
    //        theForm.action = "lead-list.aspx";
    //    }
    //    });

    //$(document).on('click', '.delete-lead', function () {
    //    let leadId = $(this).data('id');
    //    let customerName = $(this).data('name');

    //    if (confirm("Are you sure you want to delete lead for: " + customerName + "?")) {
    //        $.ajax({
    //            type: "POST",
    //            url: "lead-list.aspx/DeleteLead",
    //            data: JSON.stringify({ leadId: leadId }),
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (res) {
    //                if (res.d === true) {
    //                    toastr.success("Lead deleted successfully");
    //                    setTimeout(() => location.reload(), 800);
    //                } else {
    //                    toastr.warning("Session expired or lead not found");
    //                }
    //            },
    //            error: function () {
    //                toastr.error("Unexpected server error");
    //            }

    //        });
    //    }
    //});

</script>

</asp:Content>
