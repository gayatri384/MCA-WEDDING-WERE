using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Admin")
            {
                Response.Redirect("~/Guest/Login.aspx");
            }
            Session["breadCumbTitle"] = "Dashboard";
            Session["breadCumbPage"] = " Dashboard";

            if (!IsPostBack)
            {
                LoadCounts();
            }
        }
        private void LoadCounts()
        {
            int usersCount = GetCountFromDatabase("SELECT COUNT(*) FROM Users");
            int productsCount = GetCountFromDatabase("SELECT COUNT(*) FROM Products");
            int categoriesCount = GetCountFromDatabase("SELECT COUNT(*) FROM Category");
            int wishlistCount = GetCountFromDatabase("SELECT COUNT(*) FROM Wishlist");
            int orderCount = GetCountFromDatabase("SELECT COUNT(OrderId) FROM Orders");
            int reviewCount = GetCountFromDatabase("SELECT COUNT(*) FROM Reviews");

            lblUsersCount.Text = usersCount.ToString();
            lblProductsCount.Text = productsCount.ToString();
            lblCategoriesCount.Text = categoriesCount.ToString();
            lblWhishlistCount.Text = wishlistCount.ToString();
            lblorderCount.Text = orderCount.ToString();
            lblreviewCount.Text = reviewCount.ToString();
        }

        private int GetCountFromDatabase(string query)
        {
            int count = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }

            return count;
        }

    }
}