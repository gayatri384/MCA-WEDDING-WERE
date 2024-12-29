<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WEDDING_WARE.User.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
              <!-- Page Header Start -->
<div class="container-fluid bg-secondary mb-5">
    <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
        <h1 class="font-weight-semi-bold text-uppercase mb-3">About Us</h1>
        <div class="d-inline-flex">
            <p class="m-0"><a href="Default.aspx">About</a></p>
            <p class="m-0 px-2">-</p>
            <p class="m-0">About</p>
        </div>
    </div>
</div>
     <!-- About Section Start -->
<div class="container-fluid py-5">
    <div class="row px-xl-5">
        <div class="col-lg-5 mb-5">
            <asp:Image ID="imgAbout" runat="server" CssClass="img-fluid rounded" ImageUrl="img/WWW.PNG" AlternateText="About Us" />
        </div>
        <div class="col-lg-7 mb-5">
            
            <asp:Literal ID="litDescription" runat="server" Text="<p class='mb-4'>At<B> W' Wedding-were</B> we are committed to making your special day unforgettable. Our collection showcases timeless elegance and modern sophistication in bridal gowns, bridesmaid dresses, and accessories. Every piece is crafted to perfection, ensuring you feel confident and radiant on your wedding day.</p>" />
            
            <asp:Label ID="lblValues" runat="server" Text="Our Values" CssClass="font-weight-semi-bold" />
            <ul class="list-unstyled mb-4">
                <li><i class="fa fa-check text-primary mr-3"></i><asp:Label ID="lblValue1" runat="server" Text="Unmatched Quality" /></li>
                <li><i class="fa fa-check text-primary mr-3"></i><asp:Label ID="lblValue2" runat="server" Text="Tailored Elegance" /></li>
                <li><i class="fa fa-check text-primary mr-3"></i><asp:Label ID="lblValue3" runat="server" Text="Exceptional Customer Service" /></li>
            </ul>
            
            <asp:Label ID="lblMission" runat="server" Text="Our Mission" CssClass="font-weight-semi-bold" />
            <asp:Literal ID="litMission" runat="server" Text="<p class='mb-0'>To deliver exquisite wedding wear that embodies the essence of your dream wedding while providing an unparalleled shopping experience.</p>" />
        </div>
    </div>
</div>
<!-- About Section End -->
</asp:Content>
