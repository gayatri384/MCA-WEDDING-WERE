using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Guest
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Database connection string
                string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

                // Variables to capture input data
                string fullName = txtFullName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                string imagePath = "";

                // Handle image upload
                if (fuImage.HasFile)
                {
                    string fileName = Path.GetFileName(fuImage.PostedFile.FileName);
                    string uniqueFileName = Guid.NewGuid() + "_" + fileName;
                    string savePath = Server.MapPath("~/Images/User/") + uniqueFileName;
                 //   string savePath = Server.MapPath("D:/MCA/MCA-1/MAJOR_PROJECT/WEDDING-WARE") + uniqueFileName;

                    fuImage.SaveAs(savePath);
                    imagePath = "Images/User/" + uniqueFileName; // Save relative path
                    //imagePath = "D:/MCA/MCA-1/MAJOR_PROJECT/WEDDING-WARE" + uniqueFileName; // Save relative path
                }

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "INSERT INTO Users (FullName, Email, Password, ImagePath, CreatedAt) " +
                                       "VALUES (@FullName, @Email, @Password, @ImagePath, @CreatedAt)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@FullName", fullName);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Password", password); // Store securely in production
                            cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                // Show a JavaScript alert and redirect to login.aspx
                                string script = "alert('Registration successful! Redirecting to login page.');" +
                                                "window.location='login.aspx';";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", script, true);
                            }
                            else
                            {
                                lblMessage.Text = "Registration failed. Please try again.";
                                lblMessage.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.Visible = true;
                }
            }
        }
        private void ClearForm()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}