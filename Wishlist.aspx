<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="WEDDING_WARE.User.Wishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>My Wishlist</title>
    
<script>
    function removeFromWishlist(wishlistId) {
        const userId = <%= Session["UserId"] %>;

        $.ajax({
            type: "POST",
            url: "Wishlist.aspx/RemoveFromWishlist",
            data: JSON.stringify({ wishlistId: wishlistId, userId: userId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert(response.d);
                reloadWishlist();
            }
        });
    }

    function reloadWishlist() {
        location.reload();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <!-- Page Header Start -->
<div class="container-fluid bg-secondary mb-5">
    <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
        <h1 class="font-weight-semi-bold text-uppercase mb-3">My Whishlist</h1>
        <div class="d-inline-flex">
            <p class="m-0"><a href="Default.aspx">Home</a></p>
            <p class="m-0 px-2">-</p>
            <p class="m-0">My Whishlist</p>
        </div>
    </div>
</div>
<!-- Page Header End -->
    <div class="container mt-5">
      
        
        <asp:Repeater ID="WishlistRepeater" runat="server">
            <HeaderTemplate>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Product Name</th>
                            <th>Price</th>
                            <th>Remove</th>
                            <th>Add To Cart</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <img src='<%# ResolveUrl(Eval("WishlistProductImage").ToString()) %>' alt="Product Image" width="100" height="100" />
                    </td>
                    <td><%# Eval("ProductName") %></td>
                    <td>$<%# Eval("Price", "{0:F2}") %></td>
                    <td>
                        <button class="btn btn-danger" 
                                onclick="removeFromWishlist('<%# Eval("WishlistId") %>')">Remove</button>
                    </td>
                     <td>
                       <asp:Button ID="btnMoveToCart" runat="server" 
                            Text="Move to Cart" 
                            CssClass="btn btn-success" 
                            CommandArgument='<%# Eval("WishlistId") %>' 
                            OnClick="btnMoveToCart_Click" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </div>
</asp:Content>

