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
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Manage Products";
            Session["breadCumbPage"] = "Products";
            lblMsg.Visible = false;
            if (!IsPostBack)
            {
                getProducts();
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            Utils utils = new Utils();
            DataTable dtCategories = utils.GetCategories(); // Get active categories from Utils

            ddlCategory.DataSource = dtCategories;
            ddlCategory.DataTextField = "CategoryName"; // Column name to display
            ddlCategory.DataValueField = "CategoryId";  // Column name for value
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("Select Category", "0")); // Default item
        }
        void getProducts()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rProduct.DataSource = dt;
            rProduct.DataBind();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();

        }

        void clear()
        {
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtStock.Text = string.Empty;
            cbIsActive.Checked = false;
            hfProductId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
        }

        protected void rProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtStock.Text = dt.Rows[0]["Stock"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ProductImageUrl"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["ProductImageUrl"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
                hfProductId.Value = dt.Rows[0]["ProductId"].ToString();
                btnAddOrUpdate.Text = "Update";
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void btnAddOrUpdate_Click1(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int productId = Convert.ToInt32(hfProductId.Value);
            string categoryName = ddlCategory.SelectedItem.Text;  // Get selected Category Name
            int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);  // Get selected Category Id

            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Product_Crud", con);
            cmd.Parameters.AddWithValue("@Action", productId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
            cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(txtStock.Text));
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);

            // Pass CategoryId and CategoryName from the dropdown
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);  // Pass CategoryId
            cmd.Parameters.AddWithValue("@CategoryName", categoryName);  // Pass CategoryName

            if (fuProductImage.HasFile)
            {
                if (Utils.isValidExtension(fuProductImage.FileName))
                {
                    string newImageName = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuProductImage.FileName).Trim();
                    imagePath = "Images/Products/" + newImageName + fileExtension;
                    fuProductImage.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + newImageName + fileExtension);
                    cmd.Parameters.AddWithValue("@ProductImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select jpg, jpeg, or png image";
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
                    actionName = productId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product " + actionName + " successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();
                    clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
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