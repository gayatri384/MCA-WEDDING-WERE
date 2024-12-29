using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEDDING_WARE.Admin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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