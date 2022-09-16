using System;
using SLO.Models;

namespace SLO
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                Usuario user = Session["USER"] as Usuario;

                LBL_User.Text = user.descrip;
                item_conf.Visible = user.tip_usuario == 0;
            }
            else
                Response.Redirect("Login.aspx");
        }
    }
}