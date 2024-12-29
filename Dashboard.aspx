<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WEDDING_WARE.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- *************************************************************** -->
    <!-- Start First Cards -->
    <!-- *************************************************************** -->
    <div class="row">
        <!-- Users Panel -->
        <asp:Panel ID="panelUsers" runat="server" CssClass="col-md-4 mb-4">
            <div class="card border-right" style="height: 150px;">
                <div class="card-body">
                    <div class="d-flex d-lg-flex d-md-block align-items-center">
                        <div>
                            <h2 class="text-dark mb-1 font-weight-medium">
                                <asp:Label ID="lblUsersCount" runat="server" Text="0"></asp:Label>
                            </h2>
                            <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Users</h6>
                        </div>
                        <div class="ml-auto mt-md-3 mt-lg-0">
                            <span class="opacity-7 text-muted"><i data-feather="user"></i></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Products Panel -->
        <asp:Panel ID="panelProducts" runat="server" CssClass="col-md-4 mb-4">
            <div class="card border-right" style="height: 150px;">
                <div class="card-body">
                    <div class="d-flex d-lg-flex d-md-block align-items-center">
                        <div>
                            <h2 class="text-dark mb-1 font-weight-medium">
                                <asp:Label ID="lblProductsCount" runat="server" Text="0"></asp:Label>
                            </h2>
                            <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Products</h6>
                        </div>
                        <div class="ml-auto mt-md-3 mt-lg-0">
                            <span class="opacity-7 text-muted"><i data-feather="box"></i></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Categories Panel -->
        <asp:Panel ID="panelCategories" runat="server" CssClass="col-md-4 mb-4">
            <div class="card border-right" style="height: 150px;">
                <div class="card-body">
                    <div class="d-flex d-lg-flex d-md-block align-items-center">
                        <div>
                            <h2 class="text-dark mb-1 font-weight-medium">
                                <asp:Label ID="lblCategoriesCount" runat="server" Text="0"></asp:Label>
                            </h2>
                            <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Categories</h6>
                        </div>
                        <div class="ml-auto mt-md-3 mt-lg-0">
                            <span class="opacity-7 text-muted"><i data-feather="tag"></i></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <!-- Wishlist Panel -->
        <asp:Panel ID="panelWishlist" runat="server" CssClass="col-md-4 mb-4">
            <div class="card border-right" style="height: 150px;">
                <div class="card-body">
                    <div class="d-flex d-lg-flex d-md-block align-items-center">
                        <div>
                            <h2 class="text-dark mb-1 font-weight-medium">
                                <asp:Label ID="lblWhishlistCount" runat="server" Text="0"></asp:Label>
                            </h2>
                            <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Wishlist</h6>
                        </div>
                        <div class="ml-auto mt-md-3 mt-lg-0">
                            <span class="opacity-7 text-muted"><i data-feather="tag"></i></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Wishlist Panel -->
        <asp:Panel ID="panelOrder" runat="server" CssClass="col-md-4 mb-4">
            <div class="card border-right" style="height: 150px;">
                <div class="card-body">
                    <div class="d-flex d-lg-flex d-md-block align-items-center">
                        <div>
                            <h2 class="text-dark mb-1 font-weight-medium">
                                <asp:Label ID="lblorderCount" runat="server" Text="0"></asp:Label>
                            </h2>
                            <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Order</h6>
                        </div>
                        <div class="ml-auto mt-md-3 mt-lg-0">
                            <span class="opacity-7 text-muted"><i data-feather="tag"></i></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Review Panel -->
        <asp:Panel ID="panel1" runat="server" CssClass="col-md-4 mb-4">
            <div class="card border-right" style="height: 150px;">
                <div class="card-body">
                    <div class="d-flex d-lg-flex d-md-block align-items-center">
                        <div>
                            <h2 class="text-dark mb-1 font-weight-medium">
                                <asp:Label ID="lblreviewCount" runat="server" Text="0"></asp:Label>
                            </h2>
                            <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Review</h6>
                        </div>
                        <div class="ml-auto mt-md-3 mt-lg-0">
                            <span class="opacity-7 text-muted"><i data-feather="tag"></i></span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <!-- *************************************************************** -->
    <!-- End First Cards -->
    <!-- *************************************************************** -->
    <!-- *************************************************************** -->
    <!-- Start Sales Charts Section -->
    <!-- *************************************************************** -->
    <div class="row">
        <div class="col-lg-4 col-md-12">
        </div>
        <div class="col-lg-4 col-md-12">
        </div>
        <div class="col-lg-4 col-md-12">
        </div>
    </div>
    <!-- *************************************************************** -->
    <!-- End Sales Charts Section -->
    <!-- *************************************************************** -->
    <!-- *************************************************************** -->
    <!-- Start Location and Earnings Charts Section -->
    <!-- *************************************************************** -->
    <!-- *************************************************************** -->
    <!-- End Top Leader Table -->
    <!-- *************************************************************** -->


</asp:Content>
