using SLO.Controllers;
using System;

namespace SLO
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] != null)
                UserController.LogOut();

            Session.Clear();
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            string message = "";
            int result = UserController.LogIn(TB_Username.Text.Trim(), TB_Password.Text.Trim());

            switch (result)
            {
                case 0:
                    message = "Usuario o Clave incorrectos";
                    break;
                case 1:
                    Response.Redirect("Dashboard.aspx");
                    break;
                case 2:
                    message = "Usuario Inactivo";
                    break;
                case 3:
                    message = "Se ha producido una excepción. Ver table de Incidente";
                    break;
            }

            if (result != 1)
            {
                LBL_Error.Visible = true;
                LBL_Error.Text = message;
            }
        }
    }
}