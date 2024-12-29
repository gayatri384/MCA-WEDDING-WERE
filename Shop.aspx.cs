using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using static WEDDING_WARE.User.Cart;
using System.Drawing;

namespace WEDDING_WARE.User
{
    public partial class Shop : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        private int PageIndex
        {
            get { return (int)(ViewState["PageIndex"] ?? 0); }
            set { ViewState["PageIndex"] = value; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }
        private void LoadProducts(string searchQuery = null)
        {
            string query = @"SELECT ProductId, ProductName, Price, ProductImageUrl, Stock
                             FROM products";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += " WHERE ProductName LIKE @SearchQuery";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    ProductRepeater.DataSource = dt;
                    ProductRepeater.DataBind();

                    PagedDataSource pagedDataSource = new PagedDataSource
                    {
                        DataSource = dt.DefaultView,
                        AllowPaging = true,
                        PageSize = 9,
                        CurrentPageIndex = Math.Max(0, Math.Min(PageIndex, dt.Rows.Count - 1))
                    };

                    PreviousButton.Enabled = !pagedDataSource.IsFirstPage;
                    NextButton.Enabled = !pagedDataSource.IsLastPage;

                    PageNumberLabel.Text = $"Page {PageIndex + 1} of {pagedDataSource.PageCount}";

                    ProductRepeater.DataSource = pagedDataSource;
                    ProductRepeater.DataBind();
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadProducts(searchText);

        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Guest/Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            Button button = (Button)sender;
            string[] arguments = button.CommandArgument.Split('|');

            if (arguments.Length < 4)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid product details provided!');", true);
                return;
            }

            int productId = Convert.ToInt32(arguments[0]);
            decimal price = Convert.ToDecimal(arguments[1]);
            string productName = arguments[2];
            string productImageUrl = arguments[3];

            // Check stock level
            int stock = GetProductStock(productId);

            if (stock == 0)
            {
                // Product is out of stock
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{productName} is out of stock.');", true);
                return;
            }

            string sourceImagePath = Server.MapPath("~/" + productImageUrl);
            string cartFolderPath = Server.MapPath("~/Images/Cart/");
            if (!System.IO.Directory.Exists(cartFolderPath))
            {
                System.IO.Directory.CreateDirectory(cartFolderPath);
            }

            if (System.IO.File.Exists(sourceImagePath))
            {
                string uniqueImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(productImageUrl);
                string cartImagePath = System.IO.Path.Combine(cartFolderPath, uniqueImageName);
                System.IO.File.Copy(sourceImagePath, cartImagePath, true);

                string storedCartImageUrl = "~/Images/Cart/" + uniqueImageName;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                      INSERT INTO Cart (UserId, ProductId, ProductName, CartProductImage, Quantity, Price, AddedDate)
                      VALUES (@UserId, @ProductId, @ProductName, @CartProductImage, @Quantity, @Price, @AddedDate)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@CartProductImage", storedCartImageUrl);
                        cmd.Parameters.AddWithValue("@Quantity", 1);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@AddedDate", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{productName} has been added to your cart successfully!');", true);
                
                Response.Redirect("Cart.aspx");
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product image not found!');", true);
            }
        }
        private int GetProductStock(int productId)
        {
            int stock = 0;
            string query = "SELECT Stock FROM products WHERE ProductId = @ProductId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();
                    stock = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return stock;
        }
        private void AddProductToCart(int productId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, Price FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Create a product object to hold product details
                    var product = new
                    {
                        ProductId = reader["ProductId"],
                        ProductName = reader["ProductName"],
                        Price = reader["Price"],
                        Quantity = 1 // Default quantity
                    };

                    // Add product to the cart session
                    List<object> cart = Session["Cart"] as List<object> ?? new List<object>();
                    cart.Add(product);
                    Session["Cart"] = cart;
                }
            }
        }

        protected void PreviousButton_Click(object sender, EventArgs e)
        {
            if (PageIndex > 0)
            {
                PageIndex--;
                LoadProducts();
            }
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            PageIndex++;
            LoadProducts();
        }
        protected void AddToWishlist_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Guest/Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            Button button = (Button)sender;
            string[] arguments = button.CommandArgument.Split('|');

            if (arguments.Length < 4)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid product details provided!');", true);
                return;
            }

            int productId = Convert.ToInt32(arguments[0]);
            decimal price = Convert.ToDecimal(arguments[1]);
            string productName = arguments[2];
            string productImageUrl = arguments[3];

            string sourceImagePath = Server.MapPath("~/" + productImageUrl);
            string wishlistFolderPath = Server.MapPath("~/Images/Wishlist/");

            if (!System.IO.Directory.Exists(wishlistFolderPath))
            {
                System.IO.Directory.CreateDirectory(wishlistFolderPath);
            }

            if (System.IO.File.Exists(sourceImagePath))
            {
                string uniqueImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(productImageUrl);
                string wishlistImagePath = System.IO.Path.Combine(wishlistFolderPath, uniqueImageName);
                System.IO.File.Copy(sourceImagePath, wishlistImagePath, true);

                string storedWishlistImageUrl = "~/Images/Wishlist/" + uniqueImageName;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
              INSERT INTO Wishlist (UserId, ProductId, ProductName, WishlistProductImage, Price, AddedDate)
              VALUES (@UserId, @ProductId, @ProductName, @WishlistProductImage, @Price, @AddedDate)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@WishlistProductImage", storedWishlistImageUrl);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@AddedDate", DateTime.Now);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{productName} has been added to your wishlist!');", true);
                Response.Redirect("Wishlist.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product image not found!');", true);
            }
        }

        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}