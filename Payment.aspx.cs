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
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["Role"]?.ToString() != "Admin")
            {
                Response.Redirect("~/Guest/Login.aspx");
            }

            Session["breadCumbTitle"] = "Payment Details";
            Session["breadCumbPage"] = " Payments";

            if (!IsPostBack)
            {
                BindPaymentDetails();
            }
        }
        private void BindPaymentDetails()
        {
            // Fetch connection string from Web.config
            string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT p.PaymentId, p.OrderId, p.UserId, u.FullName, p.PaymentDate, p.PaymentMethod, p.Amount, p.PaymentStatus
                                 FROM Payments p
                                 INNER JOIN Users u ON p.UserId = u.UserId
                                 ORDER BY p.PaymentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                paymentdetails.DataSource = dt;
                paymentdetails.DataBind();
            }
        }

        protected void paymentdetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int paymentId = Convert.ToInt32(e.CommandArgument); // Get PaymentId from CommandArgument
                string connString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE Payments SET PaymentStatus = 'Completed' WHERE PaymentId = @PaymentId AND PaymentStatus = 'Pending'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Feedback to user
                            lblMessage.Text = "payment status updated successfully.";
                            lblMessage.CssClass = "text-success";
                        }
                        else
                        {
                            lblMessage.Text = "payment status update failed or already updated.";
                            lblMessage.CssClass = "text-danger";
                        }
                        if (rowsAffected > 0)
                        {
                            // Rebind the data to refresh the list
                            BindPaymentDetails();
                        }
                    }
                }
            }
        }

    }
}