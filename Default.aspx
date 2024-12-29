<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEDDING_WARE.User.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Featured Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5 pb-3 justify-content-center">
            <div class="col-lg-6 col-md-8 col-sm-12 text-center">
                <asp:Label
                    ID="lblProductCollection"
                    runat="server"
                    Text="Product Collection"
                    CssClass="d-inline-block bg-primary text-white py-3 px-4 rounded shadow-lg font-weight-bold"
                    Style="font-size: 1.5rem; text-transform: uppercase; letter-spacing: 1.5px;">
                </asp:Label>
            </div>
        </div>
    </div>
    <!-- Featured End -->


    <!-- Categories Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5 pb-3">
            <asp:Repeater ID="ProductRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-lg-4 col-md-6 pb-1">
                        <div class="cat-item d-flex flex-column border mb-4" style="padding: 30px;">
                            <p class="text-right"><%# Eval("Stock").ToString() == "0" ? "" : Eval("Price", "{0:C}") %></p>
                            <a href="javascript:void(0);" class="cat-img position-relative overflow-hidden mb-3">
                                <img width="300" height="300" src="<%# WEDDING_WARE.Utils.getImageUrl(Eval("ProductImageUrl")) %>" alt="<%# Eval("ProductName") %>">
                            </a>
                            <h5 class="font-weight-semi-bold m-0"><%# Eval("ProductName") %></h5>
                            <asp:Label ID="lblSoldOut" runat="server"
                                Text="Sold Out"
                                CssClass="text-danger font-weight-bold mt-2"
                                Visible='<%# Eval("Stock").ToString() == "0" %>'>
                            </asp:Label>
                            <asp:Button ID="AddToCartButton" runat="server" Text="Add to Cart"
                                CommandArgument='<%# Eval("ProductId")  + "|" + Eval("Price") + "|" + Eval("ProductName") 
                             + "|" + Eval("ProductImageUrl") %>'
                                OnClick="AddToCart_Click"
                                CssClass="btn btn-primary mt-2" />
                            <asp:Button ID="AddToWishlistButton" runat="server" Text="Add to Wishlist"
                                CommandArgument='<%# Eval("ProductId") + "|" + Eval("Price") + "|" + Eval("ProductName") 
                             + "|" + Eval("ProductImageUrl") %>'
                                OnClick="AddToWishlist_Click"
                                CssClass="btn btn-secondary mt-2" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Centered Pagination -->
        <div class="row justify-content-center mt-4">
            <div class="pagination text-center">
                <!-- Previous Button -->
                <asp:Button ID="PreviousButton" runat="server" Text="Previous" OnClick="PreviousButton_Click"
                    CssClass="btn btn-primary mx-2" />

                <!-- Page Number Label -->
                <asp:Label ID="PageNumberLabel" runat="server" CssClass="mx-2 font-weight-bold"></asp:Label>

                <!-- Next Button -->
                <asp:Button ID="NextButton" runat="server" Text="Next" OnClick="NextButton_Click"
                    CssClass="btn btn-primary mx-2" />
            </div>
        </div>
    </div>

    <!-- Categories End -->


    <!-- Offer Start -->
    <!--  <div class="container-fluid offer pt-5">
        <div class="row px-xl-5">
            <div class="col-md-6 pb-4">
                <div class="position-relative bg-secondary text-center text-md-right text-white mb-2 py-5 px-5">
                    <img src="img/offer-1.png" alt="">
                    <div class="position-relative" style="z-index: 1;">
                        <h5 class="text-uppercase text-primary mb-3">20% off the all order</h5>
                        <h1 class="mb-4 font-weight-semi-bold">Spring Collection</h1>
                        <a href="" class="btn btn-outline-primary py-md-2 px-md-3">Shop Now</a>
                    </div>
                </div>
            </div>
            <div class="col-md-6 pb-4">
                <div class="position-relative bg-secondary text-center text-md-left text-white mb-2 py-5 px-5">
                    <img src="img/offer-2.png" alt="">
                    <div class="position-relative" style="z-index: 1;">
                        <h5 class="text-uppercase text-primary mb-3">20% off the all order</h5>
                        <h1 class="mb-4 font-weight-semi-bold">Winter Collection</h1>
                        <a href="" class="btn btn-outline-primary py-md-2 px-md-3">Shop Now</a>
                    </div>
                </div>
            </div>
        </div>
    </div> -->
    <!-- Offer End -->





</asp:Content>
