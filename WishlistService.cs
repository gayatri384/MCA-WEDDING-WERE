using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEDDING_WARE.User
{
    public class WishlistService
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public static void AddToWishlist(int productId, string userId)
        {
            // Define your connection string (update with your actual connection string)
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString; // Replace "cs" with your connection string name
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Wishlist (UserId, ProductId) VALUES (@UserId, @ProductId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static DataTable GetUserWishlist(string userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    W.WishlistId, 
                    P.ProductName, 
                    P.ProductImageUrl, 
                    P.Price
                FROM 
                    Wishlist W
                INNER JOIN 
                    Products P ON W.ProductId = P.ProductId
                WHERE 
                    W.UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }

        public static void RemoveFromWishlist(int wishlistId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Wishlist WHERE WishlistId = @WishlistId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@WishlistId", wishlistId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}