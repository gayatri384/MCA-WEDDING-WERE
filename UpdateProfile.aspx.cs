using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.User
{
    public partial class UpdateProfile : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        // Property to manage UserProfileImage path in ViewState
        protected string UserProfileImage
        {
            get
            {
                string imagePath = ViewState["ImagePath"]?.ToString();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // Ensure the path starts with "/Images/User/"
                    // return imagePath.Replace("/Images/User/", "/Images/User/");
                    // return ResolveUrl($"{imagePath}");
                    //return ResolveUrl(imagePath);  
                    return ResolveUrl("~/Images/User/" + Path.GetFileName(imagePath));

                }
                return ResolveUrl("/Images/User/No_image.png");
            }
            set => ViewState["ImagePath"] = value;
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {

            //EnableFields(true);
            btnSaveChanges.Visible = true;
            btnEditProfile.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    LoadUserProfile();
                }
                else
                {
                    RedirectToLogin("You need to log in to view your profile.");
                }
            }
        }
        private void LoadUserProfile()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FullName, Email, Password, ImagePath FROM Users WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtUsername.Text = reader["FullName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtMobile.Text = reader["Password"].ToString();

                    string imagePath = reader["ImagePath"]?.ToString();
                    UserProfileImage = !string.IsNullOrEmpty(imagePath)
                        ? ResolveUrl(imagePath)
                        : ResolveUrl("~/Images/User/No_image.png");
                }
                else
                {
                    ShowMessage("User profile not found.");
                }
            }
        }


        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET FullName = @FullName, Email = @Email, Password = @Password  WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtMobile.Text);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }

            // Update session and reset fields
            Session["UserName"] = txtUsername.Text;
            LoadUserProfile();
            //    EnableFields(false);
            btnSaveChanges.Visible = false;
            btnEditProfile.Visible = true;
        }



        protected void btnChangePic_Click(object sender, EventArgs e)
        {
            if (FileUploadProfilePic.HasFile)
            {
                // Define the folder path where the images are stored
                string folderPath = "Images/User/";

                // Save the uploaded image and get the relative path
                string newFilePath = SaveFile(FileUploadProfilePic, folderPath);

                if (!string.IsNullOrEmpty(newFilePath))
                {
                    // Delete the old image if it exists (excluding the default "no image" file)
                    string oldImagePath = Server.MapPath(UserProfileImage);
                    if (File.Exists(oldImagePath) && !UserProfileImage.Contains("No_image.png"))
                    {
                        File.Delete(oldImagePath);
                    }

                    // Update the user's image path in the database
                    UpdateUserImage(newFilePath);

                    // Update the UserProfileImage to reflect the new image
                    UserProfileImage = newFilePath;

                    // Reload the profile to reflect the changes
                    LoadUserProfile();
                }
                else
                {
                    ShowMessage("File upload failed. Please try again.");
                }
            }
        }


        private void RedirectToLogin(string message)
        {
            ShowMessage(message);
            Response.Redirect("~/User/Login.aspx");
        }

        private void ShowMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        private string SaveFile(FileUpload fileUpload, string folderPath)
        {
            try
            {
                // Generate a unique name for the uploaded file using GUID
                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileUpload.FileName)}";

                // Get the physical path of the directory
                string physicalFolderPath = Server.MapPath("../"+ folderPath);

                // Ensure the directory exists
                if (!Directory.Exists(physicalFolderPath))
                {
                    Directory.CreateDirectory(physicalFolderPath);
                }

                // Save the file to the server with the unique name
                string fullPath = Path.Combine(physicalFolderPath, uniqueFileName);
                fileUpload.SaveAs(fullPath);

                // Return the relative path of the image
                return folderPath.TrimEnd('/') + "/" + uniqueFileName;
            }
            catch
            {
                return null;
            }
        }

        private void UpdateUserImage(string imagePath)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET ImagePath = @ImagePath WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }
    }
}