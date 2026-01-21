<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="AdminPanel_Dashboard" %>

<%@ Import Namespace="Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHContent" runat="Server">
    <header class="page-title-bar">
        <%--<h1 class="page-title text-center">Hi <%=Session["FirstName"] %>,  </h1>--%>
       <%-- <div id="divAdmin" runat="server" >

            <p class="text-muted">Manage your users, category, etc..</p>

            
        </div>
       --%>
    </header>
      <div>
        <div id="notfound-state" class="empty-state" style="padding-top:10px;">
            <!-- .empty-state-container -->
            <div class="empty-state-container">
                <div class="state-figure">
                    <img class="img-fluid" src="images/solution.svg" alt="" style="max-width: 300px">
                </div>
                <h3 class="state-header">Welcome to Curriculum Level Tracker </h3>
                <p class="state-description lead text-muted">Manage your users, category, etc..</p>

            </div>
            <!-- /.empty-state-container -->
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" runat="Server">
</asp:Content>

