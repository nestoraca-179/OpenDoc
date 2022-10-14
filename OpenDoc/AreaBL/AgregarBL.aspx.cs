using OpenDoc.Controllers;
using OpenDoc.Models;
using System;

namespace OpenDoc.AreaBL
{
    public partial class AgregarBL : System.Web.UI.Page
    {
        protected static string IDViaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id_viaje"] != null)
            {
                LBL_IDBL.Text = "Agregar BL";
                IDViaje = Request.QueryString["id_viaje"].ToString();
                CargarValores();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "EL ID del Viaje no puede ser nulo";
            }
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            BL bl = new BL();

            try
            {
                string nom_consign = TB_NomConsignee.Text;
                string dir_consign = TB_DirConsignee.Text;
                string nom_notify = TB_NomNotify.Text;
                string dir_notify = TB_DirNotify.Text;
                string nom_export = TB_NomExport.Text;
                string dir_export = TB_DirExport.Text;

                bl.id_viaje = int.Parse(IDViaje);
                bl.num_bl = TB_NumBL.Text;
                bl.num_naturaleza = int.Parse(TB_Naturaleza.Text);
                bl.tipo = DDL_Tipo.Value.ToString();
                bl.pto_carga = DDL_PtoCarga.Value.ToString();
                bl.pto_descarga = DDL_PtoDescarga.Value.ToString();
                bl.destino = TB_Destino.Text;
                bl.booking = TB_Booking.Text;
                bl.condicion = TB_Cond.Text;
                bl.tipo_mercancia = int.Parse(DDL_TipMercancia.Value.ToString());
                bl.nom_consign = nom_consign.Length > 35 ? nom_consign.Substring(0, 34) : nom_consign;
                bl.dir_consign = dir_consign;
                bl.nom_notify = nom_notify.Length > 35 ? nom_notify.Substring(0, 34) : nom_notify;
                bl.dir_notify = dir_notify;
                bl.nom_export = nom_export.Length > 35 ? nom_export.Substring(0, 34) : nom_export;
                bl.dir_export = dir_export.Length > 70 ? dir_export.Substring(0, 69) : dir_export;
                bl.gross_mass = decimal.Parse(TB_GrossMass.Text);
                bl.shipping_marks = TB_ShipMarks.Text;
                bl.num_conts = int.Parse(TB_CantCont.Text);
                bl.volumen = 0;
                bl.descripcion = TB_Descrip.Text;
                bl.tipo_paq = DDL_TipPaq.Value.ToString();
                bl.cant_paq = int.Parse(TB_CantPaq.Text);
                bl.precinto_bl = TB_PrecBL.Text;
                bl.observaciones = TB_Observa.Text;
                bl.gobierno = CK_Gobierno.Checked;
                bl.sobre_dimens = TB_SobreDim.Text;
                bl.fletes = int.Parse(TB_Fletes.Text);
                bl.mone_flet = TB_MonFletes.Text;
                bl.co_us_in = (Session["USER"] as Usuario).username;
                bl.fe_us_in = DateTime.Now;
                bl.co_us_mo = (Session["USER"] as Usuario).username;
                bl.fe_us_mo = DateTime.Now;

                int result = BLController.Add(bl);

                if (result == 1)
                {
                    Response.Redirect(string.Format("~/AreaViaje/EditarViaje.aspx?ID={0}&new=1", IDViaje), true);
                }
                else
                {
                    PN_Error.Visible = true;
                    LBL_Error.Text = "Ha ocurrido un error al modificar el BL. Ver tabla de Incidentes";
                }
            }
            catch (Exception ex)
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
                IncidentController.CreateIncident(string.Format("ERROR PROCESANDO DATOS DEL BL {0}", bl.num_bl), ex);
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AreaViaje/EditarViaje.aspx?ID=" + IDViaje, true);
        }

        private void CargarValores()
        {
            DDL_Tipo.Value = "B/L";
            TB_Cond.Text = "FCL";
            DDL_TipMercancia.Value = 0;
            TB_ShipMarks.Text = "S/M";
            TB_Volumen.Text = "0";
            DDL_TipPaq.Value = "PT";
            TB_Fletes.Text = "0";
            TB_MonFletes.Text = "USD";
        }
    }
}