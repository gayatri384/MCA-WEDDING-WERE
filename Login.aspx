<%@ Page Title="" Language="C#" MasterPageFile="~/Guest/Guest.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEDDING_WARE.Guest.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="container d-flex justify-content-center align-items-center" style="min-height: 80vh;">
        <div class="card p-4" style="width: 400px;">
            <h2 class="text-center mb-4">Login</h2>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter Email" required></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password" required></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" CssClass="text-danger" />
            </div>
            <div class="form-group text-center mt-3">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false" CssClass="d-block mt-2"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
