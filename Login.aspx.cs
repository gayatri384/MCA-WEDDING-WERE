using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Guest
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false; // Hide error message label on page load
            if (Session["LogoutMessage"] != null)
            {
                lblMessage.Text = Session["LogoutMessage"].ToString();
                lblMessage.CssClass = "text-success"; // Optional: Add a CSS class for styling
                lblMessage.Visible = true;

                // Clear the message after displaying it
                Session["LogoutMessage"] = null;
            }
            if (Session["LoginSuccessMessage"] != null)
            {
                lblMessage.Text = Session["LoginSuccessMessage"].ToString();
                lblMessage.CssClass = "text-success"; // Optional: Add CSS class for styling
                lblMessage.Visible = true;

                // Clear the session message after displaying it
                Session["LoginSuccessMessage"] = null;
            }
            else
            {
                lblMessage.Visible = false; // Hide the label if no message exists
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Database connection string from web.config
                string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

                // Get user input
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to verify credentials
                    string query = @"
                        SELECT UserId, Role, Status 
                        FROM Users 
                        WHERE Email = @Email AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password); // Ensure secure password storage in production

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["Role"].ToString();
                                bool status = Convert.ToBoolean(reader["Status"]); // Convert BIT to boolean
                                int userId = Convert.ToInt32(reader["UserId"]); // Retrieve user's ID


                                if (status)
                                {
                                    // **Create Session Variables**
                                    Session["UserId"] = userId;
                                    Session["Email"] = email;
                                    Session["Role"] = role;

                                    // **Show the success message**
                                    lblMessage.Text = "Login successful!";
                                    lblMessage.CssClass = "text-success"; // Optional: Add CSS class for styling
                                    lblMessage.Visible = true;

                                    // **Redirect after showing the success message**
                                    if (role == "User")
                                    {
                                        // Store success message and redirect
                                        Session["LoginSuccessMessage"] = "Login successful!";
                                        string script = @"
                                            alert('Login successful!');
                                            setTimeout(function() {
                                             window.location.href = '../User/Default.aspx';
                                        }, 500);";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", script, true);
                                        
                                    }
                                    else if (role == "Admin")
                                    {
                                        // Store success message and redirect
                                        Session["LoginSuccessMessage"] = "Login successful!";
                                        string script = @"
                                            alert('Login successful!');
                                            setTimeout(function() {
                                             window.location.href = '../Admin/Dashboard.aspx';
                                        }, 500);";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", script, true);

                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Your account is inactive. Please contact support.";
                                    lblMessage.Visible = true;
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Invalid email or password.";
                                lblMessage.Visible = true;
                            }
                        }
                    }
                }
            }
        }
    }
}