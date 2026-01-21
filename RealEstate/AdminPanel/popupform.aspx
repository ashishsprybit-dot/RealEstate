<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupform.aspx.cs" Inherits="controlpanel_popupform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <style>
        .modal-dialog td {
            padding: 0px 8px !important;
        }
    </style>
</head>
<body>
    <div class="modal-dialog" id="divContactUs" runat="server" visible="false">
        <div class="modal-content">
            <div class="modal-header" style="padding-bottom: 0">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&#215;</button>
                <h4 class="modal-title" id="h4head" runat="server" style="text-align: left">Contact Us enquiry</h4>
                <hr />
            </div>
            <div class="modal-body table-responsive table-no-border">
                <asp:Repeater ID="rptContactUs" runat="server" Visible="false">
                    <ItemTemplate>
                        <table class="table table-hover table-content popup">
                              <tr >
                                <td valign="top">
                                    <b>Business Name</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top"><strong><%#Convert.ToString(Eval("BusinessName")).Trim() != String.Empty ? Convert.ToString(Eval("BusinessName")).Trim() : " - "%></strong></td>
                            </tr>
                            <tr >
                                <td valign="top">
                                    <b>Name</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top"><strong><%#Convert.ToString(Eval("Name")).Trim() != String.Empty ? Convert.ToString(Eval("Name")).Trim() : " - "%></strong></td>
                            </tr>
                            <tr>
                                <td valign="top" width="127">
                                    <b>Email Address</b>
                                </td>
                                <td valign="top" width="1%">
                                    <label>:</label>
                                </td>
                                <td class="text" valign="top"><%#Convert.ToString(Eval("EmailAddress")).Trim() != String.Empty ? Convert.ToString(Eval("EmailAddress")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" width="100">
                                    <b>Phone</b>
                                </td>
                                <td valign="top" width="1%">
                                    <label>:</label>
                                </td>
                                <td class="text" valign="top"><%#Convert.ToString(Eval("Phone")).Trim() != String.Empty ? Convert.ToString(Eval("Phone")).Trim() : " - "%></td>
                            </tr>
                            <tr >
                                <td valign="top">
                                    <b>Subject</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top"><%#Convert.ToString(Eval("Subject")).Trim() != String.Empty ? Convert.ToString(Eval("Subject")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <b>Comments</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top"><%#Convert.ToString(Eval("Comments")).Trim() != String.Empty ? Convert.ToString(Eval("Comments")).Trim() : " - "%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <b>Sent Date</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top"><%#Convert.ToString(Eval("CreatedOn")).Trim() != String.Empty ? Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedOn"]).ToString("dd/MM/yyyy") : " - "%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <div class="modal-dialog" id="divPartners" runat="server" visible="false">
        <div class="modal-content">
            <div class="modal-header" style="padding-bottom: 0">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&#215;</button>
                <h4 class="modal-title" id="Heading" runat="server" style="text-align: left">Vendor</h4>
                <hr />
            </div>
            <div class="modal-body table-responsive table-no-border">
                <asp:Repeater ID="rptPartners" runat="server" Visible="false">
                    <ItemTemplate>
                        <table class="table table-hover table-content popup" style="background-color: #fff">
                            <tr>
                                <td valign="top" align="left">
                                    <b>Business Name</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><strong><%#Convert.ToString(Eval("Name")).Trim() != String.Empty ? Convert.ToString(Eval("Name")).Trim() : " - "%></strong></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>ABN</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top" align="left"><%#Convert.ToString(Eval("ABN")).Trim() != String.Empty ? Convert.ToString(Eval("ABN")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" width="130" align="left">
                                    <b>Full Name</b>
                                </td>
                                <td valign="top" width="1%">
                                    <label>:</label>
                                </td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("FullName")).Trim() != String.Empty ? Convert.ToString(Eval("FullName")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Email Address</b>
                                </td>
                                <td valign="top" width="1%">
                                    <label>:</label>
                                </td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("EmailAddress")).Trim() != String.Empty ? Convert.ToString(Eval("EmailAddress")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Business Phone</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top" align="left"><%#Convert.ToString(Eval("BusinessPhone")).Trim() != String.Empty ? Convert.ToString(Eval("BusinessPhone")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Street Address</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("StreetAddress")).Trim() != String.Empty ? Convert.ToString(Eval("StreetAddress")).Trim() : " - "%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Location</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("Location")).Trim() != String.Empty ? Convert.ToString(Eval("Location")).Trim() : " - "%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Category</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%# Category != String.Empty ? Convert.ToString(Category).Trim() : " - "%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Food Items</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%# FoodItems != String.Empty ? Convert.ToString(FoodItems).Trim() : " - "%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Registered Date</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top" align="left"><%#Convert.ToString(Eval("CreatedOn")).Trim() != String.Empty ? Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedOn"]).ToString("dd/MM/yyyy") : " - "%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <div class="modal-dialog" id="divUser" runat="server" visible="false">
        <div class="modal-content">
            <div class="modal-header" style="padding-bottom: 0">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&#215;</button>
                <h4 class="modal-title" id="h1USer" runat="server" style="text-align: left">User</h4>
                <hr />
            </div>
            <div class="modal-body table-responsive table-no-border">
                <asp:Repeater ID="rptUsers" runat="server" Visible="false">
                    <ItemTemplate>
                        <table class="table table-hover table-content popup" style="background-color: #fff">
                            <tr>
                                <td valign="top" align="left" width="150">
                                    <b>Name</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><strong><%#Convert.ToString(Eval("Name")).Trim() != String.Empty ? Convert.ToString(Eval("Name")).Trim() : " - "%></strong></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Email Address</b>
                                </td>
                                <td valign="top" width="1%">
                                    <label>:</label>
                                </td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("Email")).Trim() != String.Empty ? Convert.ToString(Eval("Email")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Mobile</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top" align="left"><%#Convert.ToString(Eval("Mobile")).Trim() != String.Empty ? Convert.ToString(Eval("Mobile")).Trim() : " - "%></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Facebook Signup?</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top" align="left"><%#Convert.ToString(Eval("FacebookID")).Trim() != String.Empty ? "YES" : "NO"%></td>
                            </tr>
                              <tr>
                                <td valign="top" align="left">
                                    <b>Birth Date</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("BirthDate")).Trim() != String.Empty ? Convert.ToDateTime(Eval("BirthDate")).ToString("dd/MMM/yyyy") : " - "%>
                                </td>
                            </tr>
                              <tr>
                                <td valign="top" align="left">
                                    <b>Gender</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("Gender")).Trim() != String.Empty ? Convert.ToString(Eval("Gender")): " - "%>
                                </td>
                            </tr>
                          <%--  <tr>
                                <td valign="top" align="left">
                                    <b>Street Address</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("StreetAddress")).Trim() != String.Empty ? Convert.ToString(Eval("StreetAddress")).Trim() : " - "%>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Location</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td class="text" valign="top" align="left"><%#Convert.ToString(Eval("Location")).Trim() != String.Empty ? Convert.ToString(Eval("Location")).Trim() : " - "%>
                                </td>
                            </tr>--%>
                            <tr>
                                <td valign="top" align="left">
                                    <b>Registered Date</b>
                                </td>
                                <td valign="top">
                                    <label>:</label></td>
                                <td valign="top" align="left"><%#Convert.ToString(Eval("CreatedDate")).Trim() != String.Empty ? Convert.ToDateTime(((System.Data.DataRowView)Container.DataItem)["CreatedDate"]).ToString("dd/MMM/yyyy") : " - "%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
</body>
</html>

