<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WEDDING_WARE.User.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Cart</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">My Cart</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="">Home</a></p>
                <p class="m-0 px-2"></p>
                <p class="m-0">Cart</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->
    <div class="container">

        <br />
        <asp:Repeater ID="CartRepeater" runat="server">
            <HeaderTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Name</th>
                            <th>Price</th>
                            <th>Quantity</th>
                            <th>Total</th>
                            <th>Remove</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td>
                        <img src='<%# ResolveUrl(Eval("CartProductImage").ToString()) %>' alt="Product Image" width="100" height="100" />
                    </td>
                    <td><%# Eval("ProductName") %></td>
                    <td>₹<%# Eval("Price", "{0:F2}") %></td>
                    <td>
                        <input type="number" value='<%# Eval("Quantity") %>' min="1"
                            onchange="updateQuantity('<%# Eval("ProductId") %>', this.value)"
                            class="quantity-box" />
                    </td>
                    <td>$<%# Eval("TotalPrice", "{0:F2}") %></td>
                    <td>
                        <button class="btn btn-danger"
                            onclick="removeFromCart('<%# Eval("ProductId") %>')">
                            Remove</button>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </tbody>
                    </table>
               
            </FooterTemplate>
        </asp:Repeater>

        <br />

        <!-- Cart Summary -->
        <div class="cart-summary" align="center">
            <h3>Cart Summary</h3>
            <p>Subtotal: <span id="subtotal">0.00</span></p>
            <p>Shipping: <span>₹10.00</span></p>
            <p>Total: <span id="total">0.00</span></p>
            <asp:HiddenField ID="SubtotalHiddenField" runat="server" />
            <asp:HiddenField ID="TotalHiddenField" runat="server" />
        </div>
        <br />
        <!-- final chechout -->
        <div class="checkout" align="center">
            <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn-checkout" OnClick="btnCheckout_Click" Height="50px"
                Width="200px" ForeColor="Black" BackColor="#FF9999" BorderStyle="None" />
        </div>
    </div>

    <script>


        function updateQuantity(productId, quantity) {
            const userId = <%= Session["UserId"] %>;

            $.ajax({
                type: "POST",
                url: "Cart.aspx/UpdateQuantity",
                data: JSON.stringify({ productId: productId, quantity: quantity, userId: userId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                    reloadCart();
                }
            });
        }

        function removeFromCart(productId) {
            const userId = <%= Session["UserId"] %>;

            $.ajax({
                type: "POST",
                url: "Cart.aspx/RemoveFromCart",
                data: JSON.stringify({ productId: productId, userId: userId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                    reloadCart();
                }
            });
        }

        function reloadCart() {
            location.reload();
        }
</script>
</asp:Content>
