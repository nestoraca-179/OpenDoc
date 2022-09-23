using SLO.Controllers;
using SLO.Models;
using System;

namespace SLO.AreaContenedor
{
    public partial class AgregarContenedor : System.Web.UI.Page
    {
        protected static string IDBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id_bl"] != null)
            {
                LBL_IDContenedor.Text = "Agregar Contenedor";
                IDBL = Request.QueryString["id_bl"].ToString();
                CargarValores();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "EL ID del BL no puede ser nulo";
            }
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            Contenedor cont = new Contenedor();

            try
            {
                string prec1 = TB_Prec1.Text;
                string prec2 = TB_Prec2.Text;
                string prec3 = TB_Prec3.Text;

                cont.id_bl = int.Parse(IDBL); ;
                cont.num_cont = TB_NumCont.Text;
                cont.num_paq = int.Parse(TB_NumPaq.Text);
                cont.tip_cont = DDL_TipoCont.Value.ToString();
                cont.estado = int.Parse(DDL_Estado.Value.ToString());
                cont.eq_inter_rec1 = prec1.Length > 10 ? prec1.Substring(0, 9) : prec1;
                cont.eq_inter_rec2 = prec2.Length > 10 ? prec2.Substring(0, 9) : prec2;
                cont.eq_inter_rec3 = prec3.Length > 10 ? prec3.Substring(0, 9) : prec3;
                cont.seal_party = DDL_SealPart.Value.ToString();
                cont.peso_neto = decimal.Parse(TB_PesoNeto.Text);
                cont.peso_bruto = decimal.Parse(TB_PesoBruto.Text);
                cont.tamanio = int.Parse(TB_Tamano.Text);
                cont.temper = decimal.Parse(TB_Temp.Text);
                cont.imo = TB_IMO.Text;
                cont.num_un = TB_UNNum.Text;
                cont.ventilac = TB_Ventila.Text;
                cont.descrip_mer = TB_DescripMer.Text;
                cont.co_us_in = (Session["USER"] as Usuario).username;
                cont.fe_us_in = DateTime.Now;
                cont.co_us_mo = (Session["USER"] as Usuario).username;
                cont.fe_us_mo = DateTime.Now;

                int result = ContenedorController.Add(cont);

                if (result == 1)
                {
                    Response.Redirect(string.Format("~/AreaBL/EditarBL.aspx?ID={0}&new=1", IDBL), true);
                }
                else
                {
                    PN_Error.Visible = true;
                    LBL_Error.Text = "Ha ocurrido un error al modificar el Contenedor. Ver tabla de Incidentes";
                }
            }
            catch (Exception ex)
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
                IncidentController.CreateIncident(string.Format("ERROR PROCESANDO DATOS DEL CONTENEDOR {0}", cont.num_cont), ex);
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/AreaBL/EditarBL.aspx?ID={0}", IDBL), true);
        }

        private void CargarValores()
        {
            DDL_SealPart.Value = "CR";
            DDL_Estado.Value = 5;
            TB_PesoNeto.Text = "0";
            TB_PesoBruto.Text = "0";
            TB_Temp.Text = "0";
            TB_Tamano.Text = "0";
            TB_IMO.Text = "0.00";
            TB_UNNum.Text = "0";
        }
    }
}