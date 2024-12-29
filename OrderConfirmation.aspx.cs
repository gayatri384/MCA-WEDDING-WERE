using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WEDDING_WARE.User
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        private int SelectedProductId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductDropdown();
            }
        }
        private void BindProductDropdown()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT p.ProductId, p.ProductName
                         FROM OrderDetails od
                         INNER JOIN Products p ON od.ProductId = p.ProductId
                         WHERE od.OrderId = @OrderId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", GetOrderIdForConfirmation(userId));
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlProduct.DataSource = reader;
                    ddlProduct.DataTextField = "ProductName";
                    ddlProduct.DataValueField = "ProductId";
                    ddlProduct.DataBind();
                    reader.Close();
                }
            }
        }

        private int GetOrderIdForConfirmation(int userId)
        {
            // Get the most recent OrderId for the user (this assumes the user only places one order at a time)
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            int orderId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT TOP 1 OrderId FROM Orders WHERE UserId = @UserId ORDER BY OrderDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    orderId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return orderId;
        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            int productId = Convert.ToInt32(ddlProduct.SelectedValue);
            int rating = Convert.ToInt32(rblRating.SelectedValue);
            string reviewText = txtReviewText.Text;
            int orderId = GetOrderIdForConfirmation(userId);

            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertReviewQuery = @"INSERT INTO Reviews (OrderId, UserId, ProductId, Rating, ReviewText)
                                     VALUES (@OrderId, @UserId, @ProductId, @Rating, @ReviewText)";

                using (SqlCommand cmd = new SqlCommand(insertReviewQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@ReviewText", reviewText);
                    cmd.ExecuteNonQuery();
                }
            }

            
            lblReviewSent.Visible = true;
        }
        

    }
}