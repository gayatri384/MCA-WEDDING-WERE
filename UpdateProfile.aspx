<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="WEDDING_WARE.User.UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Profile Container Styling */
        .profile-container {
            max-width: 650px;
            margin: auto;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.15);
        }
        /* Profile Header Styling */
        .profile-header {
            text-align: center;
            color: #007bff;
            margin-bottom: 25px;
        }

            .profile-header h2 {
                font-size: 2rem;
                font-weight: bold;
            }

        .profile-pic {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            object-fit: cover;
            border: 4px solid black;
        }
        /* Button Styling */
        .btn-change-pic, .btn-profile-actions {
            display: block;
            margin: 10px auto;
            width: 100%;
        }
        /* Aligning Change Profile Pic and Choose File on the Same Line */
        .file-upload-container {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-bottom: 20px;
        }

            .file-upload-container input {
                margin-right: 10px; /* Space between input and button */
            }
        /* Hover and Transition Effects */
        .btn-profile-actions:hover, .btn-change-pic:hover {
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
            transform: scale(1.03);
        }
        /* Information Section Styling */
        .section-title {
            font-size: 1.2rem;
            color: #6c757d;
            font-weight: 600;
            text-align: center;
            border-bottom: 2px solid #007bff;
            padding-bottom: 5px;
            margin-top: 30px;
            margin-bottom: 15px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-control {
            margin-bottom: 5px; /* Adjusting margin for better spacing */
            transition: border 0.3s ease;
        }
            /* Placeholder Styling */
            .form-control::placeholder {
                font-style: italic;
                color: #adb5bd;
            }
        /* Responsive Adjustment */
        @media (max-width: 576px) {
            .profile-container {
                padding: 20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="profile-container">
            <!-- Profile Header -->
            <div class="profile-header">
                <h2><i class="bi bi-person-circle"></i>User Profile</h2>
            </div>
            <!-- Profile Picture -->
            <div class="text-center">
                <img src="<%= ResolveUrl(UserProfileImage) %>" alt="Profile Picture" class="profile-pic mb-3" />
                
                <!-- Flex container for file upload and change picture button -->
                <div class="file-upload-container">
                    <asp:FileUpload ID="FileUploadProfilePic" runat="server" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnChangePic" runat="server" Text="Change Profile Picture" CssClass="btn btn-primary" OnClick="btnChangePic_Click" />
                </div>
            </div>

            <!-- Personal Information -->
            <div class="section-title"></div>
            <div class="form-group">
                <label for="txtUsername">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"  Placeholder="Username" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="Enter a valid email address." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtMobile">Password</label>
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"  Placeholder="Mobile Number" />
                
            </div>


            <!-- Edit, Save, and Change Password Buttons in one line -->
            <div class="d-flex justify-content-between mt-3">
                <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btn btn-warning" OnClick="btnEditProfile_Click" />
                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnSaveChanges_Click" Visible="false" />
                
            </div>
        </div>
    </div>
</asp:Content>
