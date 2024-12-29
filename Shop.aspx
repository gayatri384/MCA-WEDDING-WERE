<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="WEDDING_WARE.User.Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Our Shop</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Default.aspx">Home</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Shop</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->

    <!-- Shop Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <!-- Shop Sidebar Start -->
            <div class="col-lg-3 col-md-12 mb-5">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" Placeholder="Search by name"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="btnSearch" CssClass="btn bg-transparent text-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>

            <!-- Products Start -->
            <div class="col-lg-9 col-md-12">
                <div class="row">
                    <asp:Repeater ID="ProductRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 pb-1">
                                <div class="cat-item d-flex flex-column border mb-4" style="padding: 30px;">
                                    <p class="text-right">
                                        <%# Eval("Stock").ToString() == "0" ? "" : Eval("Price", "{0:C}") %>
                                    </p>
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
            <!-- Products End -->
        </div>
    </div>
    <!-- Shop End -->
</asp:Content>
