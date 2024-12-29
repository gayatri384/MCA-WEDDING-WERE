using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.User
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindOrderSummary();
            }
           /* Session["Name"] = txtName.Text;
            Session["Address"] = txtAddress.Text;
            Session["City"] = txtCity.Text;
            Session["ZipCode"] = txtZipCode.Text;*/
            
        }
        private void BindOrderSummary()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            int totalQuantity = 0;
            decimal totalPrice = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Bind Order Summary
                string query = @"SELECT p.ProductName, c.Quantity, p.Price, 
                                 (c.Quantity * p.Price) AS TotalPrice 
                                 FROM Cart c 
                                 INNER JOIN Products p ON c.ProductId = p.ProductId 
                                 WHERE c.UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    OrderSummaryRepeater.DataSource = reader;
                    OrderSummaryRepeater.DataBind();
                    reader.Close();
                }

                // Calculate Total Quantity and Price
                query = @"SELECT SUM(c.Quantity) AS TotalQuantity, SUM(c.Quantity * p.Price) AS TotalPrice 
                          FROM Cart c 
                          INNER JOIN Products p ON c.ProductId = p.ProductId 
                          WHERE c.UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        totalQuantity = reader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(reader["TotalQuantity"]) : 0;
                        totalPrice = reader["TotalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPrice"]) : 0;
                    }
                }
            }
            // Find the labels in FooterTemplate
            Control footer = OrderSummaryRepeater.Controls[OrderSummaryRepeater.Controls.Count - 1].FindControl("lblTotalQuantity");
            Label lblTotalQuantity = footer as Label;

            footer = OrderSummaryRepeater.Controls[OrderSummaryRepeater.Controls.Count - 1].FindControl("lblTotalPrice");
            Label lblTotalPrice = footer as Label;

            if (lblTotalQuantity != null)
            {
                lblTotalQuantity.Text = totalQuantity.ToString();
            }

            if (lblTotalPrice != null)
            {
                lblTotalPrice.Text = totalPrice.ToString("0.00");
            }

        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string paymentMethod = rblPaymentMethod.SelectedValue;
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            decimal totalPrice = 0;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Insert into Orders and get the OrderId
                string insertOrderQuery = @"INSERT INTO Orders (UserId, Subtotal, Shipping, Total) 
                                            OUTPUT INSERTED.OrderId
                                            SELECT @UserId, SUM(c.Quantity * p.Price), 10.00, 
                                            SUM(c.Quantity * p.Price) + 10.00
                                            FROM Cart c 
                                            INNER JOIN Products p ON c.ProductId = p.ProductId 
                                            WHERE c.UserId = @UserId";

                int orderId;

                using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    orderId = (int)cmd.ExecuteScalar();
                }

                // Insert into OrderDetails
                string insertOrderDetailsQuery = @"INSERT INTO OrderDetails (OrderId, ProductId, Quantity, Price, TotalPrice)
                                                   SELECT @OrderId, c.ProductId, c.Quantity, p.Price, 
                                                   (c.Quantity * p.Price)
                                                   FROM Cart c 
                                                   INNER JOIN Products p ON c.ProductId = p.ProductId 
                                                   WHERE c.UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(insertOrderDetailsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
                // Deduct the quantities from Product stock
                string updateStockQuery = @"UPDATE Products
                                    SET Stock = Stock - c.Quantity
                                    FROM Products p
                                    INNER JOIN Cart c ON p.ProductId = c.ProductId
                                    WHERE c.UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(updateStockQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }

                // Calculate Total Price
                string totalPriceQuery = @"SELECT SUM(c.Quantity * p.Price) AS TotalPrice 
                                   FROM Cart c 
                                   INNER JOIN Products p ON c.ProductId = p.ProductId 
                                   WHERE c.UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(totalPriceQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    totalPrice = (decimal)cmd.ExecuteScalar();
                }

                // Insert into Payments
                string insertPaymentQuery = @"INSERT INTO Payments (OrderId, UserId, PaymentMethod, PaymentStatus, Amount) 
                                              VALUES (@OrderId, @UserId, @PaymentMethod, 'Pending',@Amount)";

                using (SqlCommand cmd = new SqlCommand(insertPaymentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@Amount", totalPrice); // Adding the total amount
                    cmd.ExecuteNonQuery();
                }

                // Clear the Cart
                string clearCartQuery = "DELETE FROM Cart WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(clearCartQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

          /*  string redirectUrl = $"OrderConfirmation.aspx?name={HttpUtility.UrlEncode(txtName.Text)}&address={HttpUtility.UrlEncode(txtAddress.Text)}&city={HttpUtility.UrlEncode(txtCity.Text)}&zipcode={HttpUtility.UrlEncode(txtZipCode.Text)}";
            Response.Redirect(redirectUrl); */
            Response.Redirect("OrderConfirmation.aspx");
        }
    }
}
