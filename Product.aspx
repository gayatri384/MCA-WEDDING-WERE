<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WEDDING_WARE.Admin.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function (){
                document.getElementById("<%=lblMsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        }

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
                    <h4 class="card-title">Product</h4>
                    <hr/> 

                    <div class="form-body">
                        <label>Product Name</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter Product Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="txtProductName" 
                                ErrorMessage="Product Name is required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" />
                        </div>

                        <label>Category</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCategory" runat="server"
                                ControlToValidate="ddlCategory" InitialValue="0"
                                ErrorMessage="Please select a category" ForeColor="Red" />
                        </div>

                        <label>Price</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Price"></asp:TextBox>
                        </div>

                        <label>Stock</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="Enter Stock Quantity"></asp:TextBox>
                        </div>

                        <label>Product Image</label>
                        <div class="form-group">
                            <asp:FileUpload ID="fuProductImage" runat="server" CssClass="form-control" onchange="ImagePreview(this)" />
                            <asp:HiddenField ID="hfProductId" runat="server" Value="0" />
                        </div>

                        <div class="form-group">
                            <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive"/>
                        </div>
                    </div>

                    <div class="form-action pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddOrUpdate_Click1" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" OnClick="btnClear_Click" />
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
                    <h4 class="card-title">Product List</h4>
                    <hr/>
                    <div class="table-responsive">
                        <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rProduct_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Category</th>
                                            <th>Price</th>
                                            <th>Stock</th>
                                            <th>Image</th>
                                            <th>IsActive</th>
                                            <th>CreatedDate</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("ProductName") %></td>
                                    <td><%# Eval("CategoryName") %></td>
                                    <td><%# Eval("Price", "{0:C}") %></td>
                                    <td><%# Eval("Stock") %></td>
                                    <td>
                                        <img width="40" src="<%# WEDDING_WARE.Utils.getImageUrl(Eval("ProductImageUrl")) %>" alt="Image" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIsActive" runat="server" 
                                            Text='<%# (bool)Eval("IsActive") ? "Active" : "In-Active" %>'
                                            CssClass='<%# (bool)Eval("IsActive") ? "badge badge-success" : "badge badge-danger" %>'>
                                        </asp:Label>
                                    </td>
                                    <td><%# Eval("CreatedAt") %></td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("ProductId") %>' CommandName="edit" CausesValidation="false">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" Text="Delete" runat="server" CssClass="badge badge-danger"
                                            CommandArgument='<%# Eval("ProductId") %>' CommandName="delete" CausesValidation="false">
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
    </div>
</asp:Content>
