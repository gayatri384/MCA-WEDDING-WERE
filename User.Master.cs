using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.User
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                         
            if (Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                // load user control
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserContro.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Guest/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                int userId = GetLoggedInUserId(); // Replace with your method to get the logged-in user's ID
                UpdateWishlistAndCartCounts(userId);
                LoadUserProfile(userId);
                LoadCategories();
            }
        }
        private void LoadCategories()
        {
            Utils utils = new Utils();
            DataTable dtCategories = utils.GetCategories(); // Get active categories

            if (dtCategories != null && dtCategories.Rows.Count > 0)
            {
                CategoriesDropDown.DataSource = dtCategories;
                CategoriesDropDown.DataTextField = "CategoryName"; // Column for display
                CategoriesDropDown.DataValueField = "CategoryId";  // Column for value
                CategoriesDropDown.DataBind();
            }
            else
            {
                // Handle the case where no categories are retrieved
                CategoriesDropDown.Items.Clear();
            }

            CategoriesDropDown.Items.Insert(0, new ListItem("Categories", "0")); // Default item
        }
        private void LoadUserProfile(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query to get the profile image and name
                SqlCommand cmd = new SqlCommand("SELECT FullName, ImagePath FROM Users WHERE UserId = @UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string fullName = reader["FullName"].ToString();
                    // Assuming 'imagePath' is fetched from the database
                    string imagePath = reader["ImagePath"].ToString();

                    // Set profile image and name
                    ProfileImageHiddenField.Value =$"/" + imagePath;
                    ProfileNameHiddenField.Value = fullName;
                } 
                else
                {
                    // Defaults if no user record is found
                    ProfileImageHiddenField.Value = "Images/No_image.png";
                    ProfileNameHiddenField.Value = "Guest";
                }
            }
        }
        private int GetLoggedInUserId()
        {
            // Assuming the user ID is stored in Session or Cookies after login
            if (Session["UserId"] != null)
            {
                return Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                Response.Redirect("Login.aspx");
                return 0; // This won't be executed but ensures method returns an int
            }
        }

        private void UpdateWishlistAndCartCounts(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query for wishlist count
                SqlCommand wishlistCmd = new SqlCommand("SELECT COUNT(*) FROM Wishlist WHERE UserId = @UserId", con);
                wishlistCmd.Parameters.AddWithValue("@UserId", userId);
                int wishlistCount = (int)wishlistCmd.ExecuteScalar();

                // Query for cart count
                SqlCommand cartCmd = new SqlCommand("SELECT COUNT(*) FROM Cart WHERE UserId = @UserId", con);
                cartCmd.Parameters.AddWithValue("@UserId", userId);
                int cartCount = (int)cartCmd.ExecuteScalar();

                // Set the counts in labels or hidden fields to be accessed in front-end
                WishlistCountHiddenField.Value = wishlistCount.ToString();
                CartCountHiddenField.Value = cartCount.ToString();
            }
        }
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            // Clear session variables
            Session.Clear();
            Session.Abandon();

            // Optional: Clear authentication cookies (if used)
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);

            // Set a logout success message
            Session["LogoutMessage"] = "You have successfully logged out.";

            // Redirect to the login page
            Response.Redirect("~/Guest/Login.aspx");
        }
    }
}
