<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="WEDDING_WARE.Admin.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Order List</h4>
                    <hr />
                    <asp:Label ID="lblMessage" runat="server" CssClass="my-2"></asp:Label>
                    <div class="table-responsive">
                        <asp:Repeater ID="orderdetails" runat="server" OnItemCommand="orderdetails_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">OrderId</th>
                                            <th>UserId</th>
                                            <th>FullName</th>
                                            <th>Subtotal</th>
                                            <th>Shipping</th>
                                            <th>Total</th>
                                            <th>OrderDate</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("OrderId") %></td>
                                    <td>
                                        <%# Eval("UserId") %>
                                    </td>
                                    <td>
                                        <%# Eval("FullName") %>
                                    </td>
                                    <td>
                                        <%# Eval("Subtotal") %>
                                    </td>
                                    <td>
                                        <%# Eval("Shipping") %>
                                    </td>
                                    <td>
                                        <%# Eval("Total") %>
                                    </td>
                                    <td>
                                        <%# Eval("OrderDate") %>
                                    </td>
                                    <td>
                                        <%# Eval("Status") %>
                                    </td>
                                    <td>
                                        <asp:Button
                                            ID="btnUpdateStatus"
                                            runat="server"
                                            CommandName="UpdateStatus"
                                            CommandArgument='<%# Eval("OrderId") %>'
                                            Text="Complete"
                                            CssClass="btn btn-success btn-sm"
                                            Visible='<%# Eval("Status").ToString().Trim().Equals("Pending", StringComparison.OrdinalIgnoreCase) %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                    </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
