using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WEDDING_WARE.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Admin")
            {
                // Debugging log
                System.Diagnostics.Debug.WriteLine($"UserId: {Session["UserId"]}, Role: {Session["Role"]}");
                Response.Redirect("~/Guest/Login.aspx");
            }

            Session["breadCumbTitle"] = "Manage Users";
            Session["breadCumbPage"] = " Users";
            lblMsg.Visible = false;
            getUsers();
        }

        void getUsers()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rUser.DataSource = dt;
            rUser.DataBind();
        }
        void clear()
        {
            txtUserName.Text = string.Empty;
            cbIsActive.Enabled = false;
            hfUserId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
        }
        protected void btnAddOrUpdate_Click1(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtention = string.Empty;
            bool isValidToExecute = false;
            int userId = Convert.ToInt32(hfUserId.Value);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("@Action", userId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@FullName", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", cbIsActive.Checked);
            if (fuUserImage.HasFile)
            {
                if (Utils.isValidExtension(fuUserImage.FileName))
                {
                    string newImageaName = Utils.getUniqueId();
                    fileExtention = Path.GetExtension(fuUserImage.FileName).Trim();
                    imagePath = "Images/User/" + newImageaName.ToString() + fileExtention;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + newImageaName.ToString() + fileExtention);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "Please select jpg ,jpeg or png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }

            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "User  " + actionName + "  successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getUsers();
                    clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }
        protected void btnClear_Click1(object sender, EventArgs e)
        {
            clear();
        }
        protected void rUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("User_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETUSERBYID");
                cmd.Parameters.AddWithValue("@UserId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtUserName.Text = dt.Rows[0]["FullName"].ToString();
                    cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["Status"]);
                    imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImagePath"].ToString())
                        ? "../Images/No_image.png"
                        : "../" + dt.Rows[0]["ImagePath"].ToString();
                    imagePreview.Height = 200;
                    imagePreview.Width = 200;
                    hfUserId.Value = dt.Rows[0]["UserId"].ToString();
                    btnAddOrUpdate.Text = "Update";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User not found.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("User_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@UserId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "User  deleted  successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getUsers();

                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}