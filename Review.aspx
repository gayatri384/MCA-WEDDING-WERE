<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="WEDDING_WARE.Admin.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="col-sm-12">
        <div class="card">
            <div class="card-body">
                <h4 class="card-title">Reviews

                </h4>
                <hr />
                <div class="table-responsive">
                    <asp:Repeater ID="gvWishlist" runat="server">
                        <HeaderTemplate>
                            <table class="table data-table-export table-hover nowrap">
                                <thead>
                                    <tr>
                                        <th class="table-plus">ReviewId</th>
                                                <th>UserName</th>
                                                <th>ProductName</th>
                                                <th>Rating</th>
                                                <th>Review</th>
                                                <th>Date</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="table-plus"><%# Eval("ReviewId") %></td>
                                        <td><%# Eval("UserName") %></td>
                                        <td><%# Eval("ProductName") %></td>
                                        <td><%# Eval("Rating") %></td>
                                        <td><%# Eval("ReviewText") %></td>
                                        <td><%# Eval("CreatedAt", "{0:yyyy-MM-dd HH:mm:ss}") %></td>
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
