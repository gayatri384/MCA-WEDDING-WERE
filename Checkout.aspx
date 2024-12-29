<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WEDDING_WARE.User.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Checkout</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Default.aspx">Home</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Checkout</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->

    <div class="container">
        <h1 align="center">Checkout</h1>
        <br />

        <!-- Billing Address Section -->
        <div class="billing-section">
            <h3>Billing Address</h3>
            <br />
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Full Name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfname" runat="server" ControlToValidate="txtName" Text="Fame is requred"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="Address"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfAddress" runat="server" ControlToValidate="txtAddress" Text="Address is requred"></asp:RequiredFieldValidator>

            <br />
            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Placeholder="City"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfCity" runat="server" ControlToValidate="txtCity" Text="City is requred"></asp:RequiredFieldValidator>

            <br />
            <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" Placeholder="Zip Code"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfZipCode" runat="server" ControlToValidate="txtZipCode" Text="Zip code is requred"></asp:RequiredFieldValidator>

            <br />
        </div>
        <br />

        <!-- Order Summary Section -->
        <div class="order-summary">
            <h3>Order Summary</h3>
            <br />
            <asp:Repeater ID="OrderSummaryRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Quantity</th>
                                <th>Price</th>
                                <th>Total</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ProductName") %></td>
                        <td><%# Eval("Quantity") %></td>
                        <td>₹<%# Eval("Price", "{0:F2}") %></td>
                        <td>₹<%# Eval("TotalPrice", "{0:F2}") %></td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
                    </table>
                    <div class="text-right">
                        <label>Total Quantity: </label>
                        <asp:Label ID="lblTotalQuantity" runat="server" Text="0"></asp:Label>
                        <br />
                        <label>Total Price: </label>
                        <asp:Label ID="lblTotalPrice" runat="server" Text="0.00"></asp:Label>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <br />

        <!-- Payment Section -->
        <div class="payment-section">
            <h3>Payment Method</h3>
            <br />
            <asp:RadioButtonList ID="rblPaymentMethod" runat="server" CssClass="form-check">
                <asp:ListItem Text="Cash on Delivery (COD)" Value="COD"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblPaymentMethod" 
                Text="requred">
            </asp:RequiredFieldValidator>
            <br />

            <!-- Card Details Section -->
            <div id="DivCardDetails" runat="server" visible="false">
                <h4>Card Details</h4>
                <br />
                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" Placeholder="Card Number"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control" Placeholder="MM/YY"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" Placeholder="CVV"></asp:TextBox>
                <br />
            </div>
        </div>

        <br />

        <!-- Place Order Button -->
        <div class="payment-section" align="center">
            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-primary" OnClick="btnPlaceOrder_Click" />
        </div>
    </div>
</asp:Content>
