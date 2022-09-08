using System;
using SLO.Models;

namespace SLO
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
                LBL_User.Text = (Session["USER"] as Usuario).descrip;
            else
                Response.Redirect("Login.aspx");
        }
    }
}