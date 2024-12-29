<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="WEDDING_WARE.User.OrderConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Order Confirmation</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Default.aspx"></a></p>
                <p class="m-0 px-2"></p>
                <p class="m-0"></p>
            </div>
        </div>
    </div>

     <!-- Success Message Box -->
    <div class="container mt-5">
        <!-- Success Message in TextBox -->
        <div class="form-group">
            <label for="txtSuccessMessage">Order Successful</label>
            <asp:TextBox ID="txtSuccessMessage" runat="server" CssClass="form-control" TextMode="SingleLine" ReadOnly="true" Text="Your order has been successfully placed. Thank you for shopping with us!" />
        </div>
    </div>
    
    <div class="container mt-5">

        <!-- Display Order Confirmation details -->

        <h3>Leave a Review</h3>

        <div class="form-group">
            <label for="ddlProduct">Select Product</label>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <br />
        <div class="form-group">
            <label for="rblRating">Rating</label>
            <asp:RadioButtonList ID="rblRating" runat="server" CssClass="form-control">
                <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="form-group">
            <label for="txtReviewText">Your Review</label>
            <asp:TextBox ID="txtReviewText" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Write your review..."></asp:TextBox>
        </div>
        <div class="form-group" align="center">
            <asp:Button ID="btnSubmitReview" runat="server" Text="Submit Review" CssClass="btn btn-primary" OnClick="btnSubmitReview_Click" />
        </div>

        <!-- Review Sent Message -->
        <div class="form-group" align="center">
            <asp:Label ID="lblReviewSent" runat="server" CssClass="alert alert-success" Style="display:none;" Text="Your review has been sent successfully!"></asp:Label>
        </div>

    </div>
</asp:Content>
