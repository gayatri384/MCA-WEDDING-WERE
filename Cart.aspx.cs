using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEDDING_WARE.Admin;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services;

namespace WEDDING_WARE.User
{
    public partial class Cart : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        // The CartItem class represents an individual item in the cart.
        public class CartItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductImageUrl { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Guest/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCart();
            }
        }
        private void LoadCart()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT ProductId, ProductName, CartProductImage, Quantity, Price, (Quantity * Price) AS TotalPrice
                FROM Cart
                WHERE UserId = @UserId";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable cartTable = new DataTable();
                        adapter.Fill(cartTable);

                        CartRepeater.DataSource = cartTable;
                        CartRepeater.DataBind();

                        // Update Cart Summary
                        UpdateCartSummary(cartTable);
                    }
                }
            }
        }

        private void UpdateCartSummary(DataTable cartTable)
        {
            decimal subtotal = 0;

            foreach (DataRow row in cartTable.Rows)
            {
                subtotal += Convert.ToDecimal(row["TotalPrice"]);
            }

            decimal shipping = 10.00m; // Flat shipping rate
            decimal total = subtotal + shipping;

            // Update the hidden fields
            SubtotalHiddenField.Value = subtotal.ToString("F2");
            TotalHiddenField.Value = total.ToString("F2");

            // Update the frontend using JavaScript
            ClientScript.RegisterStartupScript(GetType(), "UpdateCartSummary", $@"
                document.getElementById('subtotal').innerText = '${subtotal:F2}';
                document.getElementById('total').innerText = '${total:F2}';
            ", true);
        }

        [System.Web.Services.WebMethod]
        public static string UpdateQuantity(int productId, int quantity, int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            string message = "Quantity updated successfully.";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Cart 
                    SET Quantity = @Quantity
                    WHERE ProductId = @ProductId AND UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        message = "Failed to update quantity.";
                    }
                }
            }

            return message;
        }

        [System.Web.Services.WebMethod]
        public static string RemoveFromCart(int productId, int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            string message = "Item removed successfully.";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cart WHERE ProductId = @ProductId AND UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        message = "Failed to remove item.";
                    }
                }
            }

            return message;
        }
        private void BindCartData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT  ProductImageUrl,ProductId,ProductName, Price, Quantity FROM Cart WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CartRepeater.DataSource = dt;
                CartRepeater.DataBind();
            }
        }
        protected void RemoveFromCart_Click(object sender, EventArgs e)
        {
            var button = (System.Web.UI.WebControls.Button)sender;
            string productId = button.CommandArgument;

            // Remove product from cart
            List<dynamic> cart = (List<dynamic>)Session["Cart"] ?? new List<dynamic>();
            var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                Session["Cart"] = cart;
            }

            // Rebind cart data
            BindCartData();
        }
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Redirect to checkout page
            Response.Redirect("checkout.aspx");
        }
        protected void ProceedToCheckout_Click(object sender, EventArgs e)
        {
            // Redirect to checkout page
            Response.Redirect("checkout.aspx");
        }
    }
}