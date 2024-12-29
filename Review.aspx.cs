using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Admin
{
    public partial class Review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Admin")
            {
                // Debugging log
                System.Diagnostics.Debug.WriteLine($"UserId: {Session["UserId"]}, Role: {Session["Role"]}");
                Response.Redirect("~/Guest/Login.aspx");
            }

            Session["breadCumbTitle"] = "WishList Product";
            Session["breadCumbPage"] = " Whishlist";
            if (!IsPostBack)
            {
                BindReviewList();
            }
        }
        private void BindReviewList()
        {
            // Connection string from Web.config
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT 
                        R.ReviewId,
                        U.FullName AS UserName,
                        P.ProductName,
                        R.Rating,
                        R.ReviewText,
                        R.CreatedAt
                    FROM Reviews R
                    INNER JOIN Users U ON R.UserId = U.UserId
                    INNER JOIN Products P ON R.ProductId = P.ProductId
                    ORDER BY R.CreatedAt DESC";


                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvWishlist.DataSource = dt;
                gvWishlist.DataBind();

            }
        }
    }
}