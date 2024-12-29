<%@ Page Title="" Language="C#" MasterPageFile="~/Guest/Guest.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WEDDING_WARE.Guest.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <title>Register</title>
   <style>
        .center-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .form-box {
            width: 100%;
            max-width: 500px;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 10px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container center-container">
        <div class="form-box">
            <h2 class="text-center mb-4">Create Account</h2>
            <div class="form-group">
                <label for="txtFullName">Full Name</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter Full Name" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" ErrorMessage="Full Name is required" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter Email" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtConfirmPassword">Confirm Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm Password" ></asp:TextBox>
                <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="fuImage">Upload Image</label>
                <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvImage" runat="server" ControlToValidate="fuImage" ErrorMessage="Image is required" CssClass="text-danger" InitialValue="" />
            </div>
            <div class="form-group text-center">
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary w-100" OnClick="btnRegister_Click" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
