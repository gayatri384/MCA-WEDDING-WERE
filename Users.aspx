<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="WEDDING_WARE.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        /* For disappring alert message*/
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        }
    </script>
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imagePreview.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Users</h4>
                    <hr />

                    <div class="form-body">
                        <label>User Name</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"
                                        placeholder="Enter User Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server"
                                        ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        ControlToValidate="txtUserName" ErrorMessage="Users Name is required">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>

                        <label>User Image</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="fuUserImage" runat="server" CssClass="from-Control"
                                        onchange="ImagePreview(this)" />
                                    <asp:HiddenField ID="hfUserId" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>

                        <label>Email</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                        placeholder="Enter User Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                        ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        ControlToValidate="txtEmail" ErrorMessage="Email is required">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <label>Password</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"
                                        placeholder="Enter User Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                        ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        ControlToValidate="txtPassword" ErrorMessage="Users Name is required">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                   <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive"/> 
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="from-action pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddOrUpdate_Click1" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" OnClick="btnClear_Click1" />
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagePreview" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Users List</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rUser" runat="server" OnItemCommand="rUser_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Name</th>
                                            <th>Image</th>
                                            <th>Email</th>
                                            <th>IsActive</th>
                                            <th>CreatedDate</th>
                                            <th class="datatable-nosort">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"><%# Eval("FullName") %></td>
                                    <td>
                                        <img width="40" src="<%# WEDDING_WARE.Utils.getImageUrl( Eval("ImagePath")) %>" alt="Image" />
                                    </td>
                                    <td>
                                        <%# Eval("Email") %>
                            

                                    </td>
                                    <td>
                                        <asp:Label ID="lblIaActive" runat="server"
                                            Text='<%# (Eval("Status").ToString().ToLower() == "active") ? "In-Active" : "Active" %>'
                                            CssClass='<%# (Eval("Status").ToString().ToLower() == "active") ? "badge badge-danger" : "badge badge-success" %>'>

                                        </asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("CreatedAt") %>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("UserId") %>' CommandName="edit" CausesValidation="false">
                                    <i class="fas fa-edit">

                                    </i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" Text="Edit" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("UserId") %>' CommandName="delete" CausesValidation="false">
                                    <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                        </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
