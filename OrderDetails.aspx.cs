using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Data;
using System.Web.UI;
using WEDDING_WARE.User;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Admin
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Admin")
            {
                // Debugging log
                System.Diagnostics.Debug.WriteLine($"UserId: {Session["UserId"]}, Role: {Session["Role"]}");
                Response.Redirect("~/Guest/Login.aspx");
            }

            Session["breadCumbTitle"] = "Order Details";
            Session["breadCumbPage"] = " Order";

            if (!IsPostBack)
            {
                BindWishlist();
            }
        }
        protected void orderdetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE Orders SET Status = 'Completed' WHERE OrderId = @OrderId AND Status = 'Pending'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Feedback to user
                            lblMessage.Text = "Order status updated successfully.";
                            lblMessage.CssClass = "text-success";
                        }
                        else
                        {
                            lblMessage.Text = "Order status update failed or already updated.";
                            lblMessage.CssClass = "text-danger";
                        }
                    }
                }

                // Rebind data to refresh
                BindWishlist();
            }
        }

        private void BindWishlist()
        {
            // Connection string from Web.config
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT o.OrderId, o.UserId, u.FullName, o.Subtotal, o.Shipping, o.Total, o.OrderDate AS OrderDate, o.Status
                                 FROM Orders o
                                 INNER JOIN Users u ON o.UserId = u.UserId
                                 ORDER BY o.OrderDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                orderdetails.DataSource = dt;
                orderdetails.DataBind();

            }
        }
        

    }
}