using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Guest
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateCartSummary();
                LoadCategories(); // Load categories only on the initial page load
            }
            if (Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                // load user control
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }
            else
            {

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
        private void UpdateCartSummary()
        {
            // Get the cart from the session
            List<dynamic> cart = (List<dynamic>)Session["Cart"] ?? new List<dynamic>();
            CartCountLabel.Visible= false;
            // Display the number of items in the cart
          //  CartCountLabel.Text = cart.Count.ToString(); // Assuming CartCountLabel is a Label control
        }
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            // Redirect to the Login page
            Response.Redirect("Login.aspx");
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            // Redirect to the Registration page
            Response.Redirect("Register.aspx");
        }
        protected void txtComboBox_TextChanged(object sender, EventArgs e)
        {

        }

    }
}