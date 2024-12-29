using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEDDING_WARE.User
{
    public class CartHandler
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public void AddToCart(int userId, int productId, int quantity, decimal price)
        {
            // Step 1: Add to Session
            List<CartItem> cart = (List<CartItem>)HttpContext.Current.Session["Cart"] ?? new List<CartItem>();

            // Check if product already exists in the session cart
            var existingItem = cart.Find(item => item.ProductId == productId);

            if (existingItem != null)
            {
                // Update quantity if product exists
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new product to the session cart
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = "Product Name", // Replace with actual product name fetched from the database
                    ProductImageUrl = "Images/Product.jpg", // Replace with actual product image URL
                    Price = price,
                    Quantity = quantity
                });
            }

            // Save updated cart to session
            HttpContext.Current.Session["Cart"] = cart;

            // Step 2: Add to Database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if product already exists in the database cart
                string checkQuery = "SELECT Quantity FROM Cart WHERE UserId = @UserId AND ProductId = @ProductId";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@UserId", userId);
                    checkCmd.Parameters.AddWithValue("@ProductId", productId);

                    object result = checkCmd.ExecuteScalar();

                    if (result != null) // Product exists, update quantity
                    {
                        int existingQuantity = Convert.ToInt32(result);
                        string updateQuery = "UPDATE Cart SET Quantity = @Quantity WHERE UserId = @UserId AND ProductId = @ProductId";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@Quantity", existingQuantity + quantity);
                            updateCmd.Parameters.AddWithValue("@UserId", userId);
                            updateCmd.Parameters.AddWithValue("@ProductId", productId);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else // Product does not exist, insert new row
                    {
                        string insertQuery = "INSERT INTO Cart (UserId, ProductId, Quantity, Price, CreatedAt) VALUES (@UserId, @ProductId, @Quantity, @Price, @CreatedAt)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@ProductId", productId);
                            insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                            insertCmd.Parameters.AddWithValue("@Price", price);
                            insertCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public List<CartItem> GetCartItems(int userId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT c.ProductId, p.ProductName, p.ImageUrl, c.Price, c.Quantity FROM Cart c INNER JOIN Products p ON c.ProductId = p.Id WHERE c.UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartItems.Add(new CartItem
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                ProductImageUrl = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Quantity = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return cartItems;
        }
    }
}