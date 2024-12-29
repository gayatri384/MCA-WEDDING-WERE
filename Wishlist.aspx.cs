using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.User
{
    public partial class Wishlist : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Guest/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadWishlist();
            }
        }

        private void LoadWishlist()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT WishlistId, ProductName, Price, WishlistProductImage 
                    FROM Wishlist 
                    WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        WishlistRepeater.DataSource = dt;
                        WishlistRepeater.DataBind();
                    }
                    else
                    {
                        // Handle case when the wishlist is empty (Optional)
                    }
                }
            }
        }

        // Remove item from wishlist (Handle POST request)
        [System.Web.Services.WebMethod]
        public static string RemoveFromWishlist(int wishlistId, int userId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Wishlist WHERE WishlistId = @WishlistId AND UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return "Item removed from wishlist.";
                    }
                    else
                    {
                        return "Failed to remove item from wishlist.";
                    }
                }
            }
        }
        //  move to cart throught whishlist
        protected void btnMoveToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int wishlistId = Convert.ToInt32(btn.CommandArgument);

            // Retrieve the UserId from the session
            int userId = Convert.ToInt32(Session["UserId"]);

            // Call the method to move the product from wishlist to cart
            MoveProductToCart(wishlistId, userId);
            Response.Redirect("Cart.aspx");
        }

        private void MoveProductToCart(int wishlistId, int userId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT ProductId, ProductName, Price, WishlistProductImage 
                FROM Wishlist 
                WHERE WishlistId = @WishlistId AND UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();

                    // Open SqlDataReader and read data
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int productId = reader.GetInt32(0);
                        string productName = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string productImage = reader.GetString(3);

                        // Close the reader before running another query
                        reader.Close();

                        // Add the product to the cart
                        string insertQuery = @"
                    INSERT INTO Cart (UserId, ProductId, ProductName, CartProductImage, Quantity, Price, AddedDate)
                    VALUES (@UserId, @ProductId, @ProductName, @CartProductImage, @Quantity, @Price, @AddedDate)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@ProductId", productId);
                            insertCmd.Parameters.AddWithValue("@ProductName", productName);
                            insertCmd.Parameters.AddWithValue("@CartProductImage", productImage);
                            insertCmd.Parameters.AddWithValue("@Quantity", 1);
                            insertCmd.Parameters.AddWithValue("@Price", price);
                            insertCmd.Parameters.AddWithValue("@AddedDate", DateTime.Now);

                            insertCmd.ExecuteNonQuery();
                        }

                        // Remove from wishlist
                        string deleteQuery = "DELETE FROM Wishlist WHERE WishlistId = @WishlistId AND UserId = @UserId";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                            deleteCmd.Parameters.AddWithValue("@UserId", userId);

                            deleteCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        protected void AddToWishlist_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            // Ensure the user is logged in
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Guest/Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]); // Get logged-in user's ID
            Button button = (Button)sender;
            int productId = Convert.ToInt32(button.CommandArgument); // Get ProductId from CommandArgument

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                INSERT INTO Wishlist (UserId, ProductId, AddedDate)
                VALUES (@UserId, @ProductId, @AddedDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@AddedDate", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Optional: Notify the user
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product added to wishlist successfully!');", true);
        }

        
    }
}