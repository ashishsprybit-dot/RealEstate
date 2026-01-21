<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="access-denied.aspx.cs" Inherits="AdminPanel_access_denied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" Runat="Server">
   <div class="wrapper">
        <!-- .empty-state -->
        <div class="empty-state">
            <!-- .empty-state-container -->
            <div class="empty-state-container">
                <div class="state-figure">
                    <div class="display-3 text-info">
                        <i class="fa fa-lock"></i>&nbsp;401
                    </div>
                </div>
                <h3 class="state-header">Oops.. You just found an error page..</h3>
                <p class="state-description lead text-muted">We are sorry but you are not authorized to access this page.. </p>
                <%--<div class="state-action">
                    <a href="/" class="btn btn-lg btn-light"><i class="fa fa-angle-right"></i>Go Back</a>
                </div>--%>
            </div>
            <!-- /.empty-state-container -->
        </div>
        <!-- /.empty-state -->
    </div>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
</asp:Content>

