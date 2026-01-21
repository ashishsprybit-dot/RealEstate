<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListPagePagging.ascx.cs" Inherits="admin_includes_ListPagePagging" %>
<%--<style>
    .tableWhite tbody tr:nth-child(2n) {background:none repeat scroll 0 0 #FFF;    }
</style>--%>
<%--<link href="assets/footable/footable-0.1.css" rel="stylesheet" />
 <script src="assets/js/footable/footable.js"></script>

    <script type="text/javascript">
        jQuery(function ($) {
            $('.footable').footable();

        });
    </script>--%>
<%--<div class="myrs-pagination clearfix">--%>
<div class="row myrs-pagination ">
    <div class="col-xs-6">
        <asp:Repeater runat="server" ID="rptPaging">
            <ItemTemplate>
                <a href="javascript:;" name="<%# ((System.Data.DataRowView)Container.DataItem)["PageNo"] %>" class="<%# ((System.Data.DataRowView)Container.DataItem)["class"] %>"><%# ((System.Data.DataRowView)Container.DataItem)["PageName"] %></a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="col-xs-6">
        <div class="dataTables_paginate paging_bootstrap">
            <div class="rangeTotal">
                <span class="myrs-pagination-total"><strong id="divTotRec"><%= TotalRecord%> records</strong></span>
                <span class="myrs-pagination-maxvalue" style="display: inline;">
                    <a class="myrs-pagination-maxvalue-toggler" href="javascript:;"><strong id="StrRecPerPage">25</strong> Per Page</a>
                    <div class="myrs-pagination-maxvalue-list" style="display: none; padding-right: 5px; font-size: 14px;">
                        <ul>
                            <li><a href="javascript:;"><strong>10</strong> Per Page</a></li>
                            <li><a href="javascript:;"><strong>25</strong> Per Page</a></li>
                            <li><a href="javascript:;"><strong>50</strong> Per Page</a></li>
                            <li><a href="javascript:;"><strong>100</strong> Per Page</a></li>
                            <li><a href="javascript:;"><strong>200</strong> Per Page</a></li>
                        </ul>
                    </div>
                </span>
            </div>
        </div>
    </div>
</div>






