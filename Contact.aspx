<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WEDDING_WARE.User.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Contact Us</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="">Home</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Contact</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->


    <!-- Contact Start -->
    <div class="container-fluid pt-5">
        <div class="text-center mb-4">
            <h2 class="section-title px-5"><span class="px-2">Contact For Any Queries</span></h2>
        </div>
        <div class="row px-xl-5">
            <div class="col-lg-7 mb-5">
                <div class="contact-form">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="control-group">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Your Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Please enter your name" CssClass="text-danger" />
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Your Email" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter your email" CssClass="text-danger" />
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Placeholder="Subject"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject" ErrorMessage="Please enter a subject" CssClass="text-danger" />
                        </div>
                        <div class="control-group">
                            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" Placeholder="Message"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage" ErrorMessage="Please enter your message" CssClass="text-danger" />
                        </div>
                        <div>
                            <asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary py-2 px-4" Text="Send Message" OnClick="btnSend_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-lg-5 mb-5">
                <h5 class="font-weight-semi-bold mb-3">Get In Touch</h5>
                <p>Feel free to reach out to us with any queries or concerns. Our team is here to assist you.</p>
                <div class="d-flex flex-column mb-3">
                    <h5 class="font-weight-semi-bold mb-3">W-WEDDING-WARE Main Store</h5>
                    <p class="mb-2"><i class="fa fa-map-marker-alt text-primary mr-3"></i>Rajkot-300001, Gujarat, INDIA</p>
                    <p class="mb-2"><i class="fa fa-envelope text-primary mr-3"></i>w'wedding-ware@gmail.com</p>
                    <p class="mb-2"><i class="fa fa-phone-alt text-primary mr-3"></i>+984 345 67890</p>
                </div>
                <div class="d-flex flex-column">
                    <h5 class="font-weight-semi-bold mb-3">W-WEDDING-WARE Branch</h5>
                    <p class="mb-2"><i class="fa fa-map-marker-alt text-primary mr-3"></i>123 Street, New York, USA</p>
                    <p class="mb-2"><i class="fa fa-envelope text-primary mr-3"></i>w'wedding-ware@ukgmail.com</p>
                    <p class="mb-0"><i class="fa fa-phone-alt text-primary mr-3"></i>+987 345 67890</p>
                </div>
            </div>
        </div>
    </div>
    <!-- Contact End -->

</asp:Content>
