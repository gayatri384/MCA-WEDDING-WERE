<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="WEDDING_WARE.Admin.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Payment List</h4>
                    <hr />
                    <asp:Label ID="lblMessage" runat="server" CssClass="my-2"></asp:Label>
                    <div class="table-responsive">
                        <asp:Repeater ID="paymentdetails" runat="server" OnItemCommand="paymentdetails_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">PaymentId</th>
                                            <th>OrderId</th>
                                            <th>UserId</th>
                                            <th>FullName</th>
                                            <th>PaymentDate</th>
                                            <th>PaymentMethod</th>
                                            <th>Amount</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("PaymentId") %></td>
                                    <td><%# Eval("OrderId") %></td>
                                    <td><%# Eval("UserId") %></td>
                                    <td><%# Eval("FullName") %></td>
                                    <td><%# Eval("PaymentDate") %></td>
                                    <td><%# Eval("PaymentMethod") %></td>
                                    <td><%# Eval("Amount") %></td>
                                    <td><%# Eval("PaymentStatus") %></td>
                                    <td>
                                        <asp:Button
                                            ID="btnUpdateStatus"
                                            runat="server"
                                            CommandName="UpdateStatus"
                                            CommandArgument='<%# Eval("PaymentId") %>'
                                            Text="Complete"
                                            CssClass="btn btn-success btn-sm"
                                            Visible='<%# Eval("PaymentStatus").ToString() == "Pending" %>' />
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
