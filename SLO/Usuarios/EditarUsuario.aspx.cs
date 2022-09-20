using DevExpress.Web;
using SLO.Controllers;
using SLO.Models;
using System;

namespace SLO.Usuarios
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        private static string IDUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                Usuario user = UsuarioController.GetByID(int.Parse(Request.QueryString["ID"].ToString()));

                if (!IsPostBack)
                    CargarUsuario(user);

                IDUser = Request.QueryString["ID"].ToString();
                LBL_IDUsuario.Text = "Editar Usuario " + user.descrip;
            }
            else
            {
                BTN_Guardar.Visible = false;
                BTN_Volver.Visible = false;
                PN_ContainerForm.Visible = false;
                PN_Error.Visible = true;
                LBL_Error.Text = "El ID del usuario no puede ser nulo";
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Configuracion.aspx");
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();

            try
            {
                user.ID = int.Parse(IDUser);
                user.username = TB_Username.Text;
                user.password = SecurityController.Encrypt(TB_Password.Text);
                user.descrip = TB_Descrip.Text;
                user.email = TB_Email.Text;
                user.activo = CK_Activo.Checked;
                user.region = TB_Region.Text;
                user.tip_usuario = int.Parse(DDL_TipoUsuario.Value.ToString());
                user.co_us_mo = (Session["USER"] as Usuario).username;
                user.fe_us_mo = DateTime.Now;

                int result = UsuarioController.Edit(user);

                if (result == 1)
                {
                    Response.Redirect("/Configuracion.aspx?edit_user=1");
                }
                else
                {
                    PN_Error.Visible = true;
                    LBL_Error.Text = "Ha ocurrido un error al modificar el Usuario. Ver tabla de Incidentes";
                }
            }
            catch (Exception ex)
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
                IncidentController.CreateIncident(string.Format("ERROR PROCESANDO DATOS DEL USUARIO {0}", user.ID), ex);
            }
        }

        private void CargarUsuario(Usuario user)
        {
            TB_Username.Text = user.username;
            TB_Password.Text = user.password;
            TB_Descrip.Text = user.descrip;
            TB_Email.Text = user.email;
            TB_Region.Text = user.region;
            CK_Activo.Checked = user.activo;
            DDL_TipoUsuario.Value = user.tip_usuario;

            foreach (ListEditItem item in DDL_TipoUsuario.Items)
            {
                item.Selected = user.tip_usuario.ToString() == item.Value.ToString();
            }
        }
    }
}