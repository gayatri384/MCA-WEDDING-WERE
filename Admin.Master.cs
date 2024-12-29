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
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = GetLoggedInUserId(); // Replace with your logic to get the user's ID
                LoadUserProfile(userId);
            }
        }
        private void LoadUserProfile(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT FullName, ImagePath FROM Users WHERE UserId = @UserId", con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string fullName = reader["FullName"].ToString();
                    string imagePath = reader["ImagePath"]?.ToString() ?? "Images/No_image.png";

                    ProfileImageHiddenField.Value = "/" + imagePath;
                    ProfileNameHiddenField.Value = fullName;
                }
                else
                {
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
    }
}