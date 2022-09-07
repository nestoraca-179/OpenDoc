using System;
using SLO.Controllers;
using SLO.Models;

namespace SLO.AreaBL
{
    public partial class EditarBL : System.Web.UI.Page
    {
        private static string IDBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                BL bl = BLController.GetByID(int.Parse(Request.QueryString["ID"].ToString()));
                
                if (!IsPostBack)
                    CargarBL(bl);

                IDBL = Request.QueryString["ID"].ToString();
                LBL_IDBL.Text = "Editar BL N° " + bl.num_bl.ToString();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "El ID del BL no puede ser nulo";
                PN_ContainerForm.Visible = false;
                GV_GridResultsC.Visible = false;
            }
        }

        protected void GV_GridResultsC_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "Editar")
                Response.Redirect("~/AreaContenedor/EditarContenedor.aspx?ID=" + e.KeyValue.ToString(), false);
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            BL bl = new BL();

            bl.ID = int.Parse(IDBL);
            bl.num_naturaleza = int.Parse(TB_Naturaleza.Text);
            bl.tipo = DDL_Tipo.Value.ToString();
            bl.pto_carga = TB_PtoCarga.Text;
            bl.pto_descarga = TB_PtoDescarga.Text;
            bl.destino = TB_Destino.Text;
            bl.booking = TB_Booking.Text;
            bl.condicion = TB_Cond.Text;
            bl.tipo_mercancia = int.Parse(DDL_TipMercancia.Value.ToString());
            bl.nom_consign = TB_NomConsignee.Text;
            bl.dir_consign = TB_DirConsignee.Text;
            bl.nom_notify = TB_NomNotify.Text;
            bl.dir_notify = TB_DirNotify.Text;
            bl.nom_export = TB_NomExport.Text;
            bl.dir_export = TB_DirExport.Text;
            bl.gross_mass = decimal.Parse(TB_GrossMass.Text);
            bl.shipping_marks = TB_ShipMarks.Text;
            bl.num_conts = int.Parse(TB_CantCont.Text);
            bl.volumen = decimal.Parse(TB_CantCont.Text);
            bl.descripcion = TB_Descrip.Text;
            bl.tipo_paq = DDL_TipPaq.Value.ToString();
            bl.cant_paq = int.Parse(TB_CantPaq.Text);
            bl.precinto_bl = TB_PrecBL.Text;
            bl.observaciones = TB_Observa.Text;
            bl.gobierno = CK_Gobierno.Checked;
            bl.sobre_dimens = TB_SobreDim.Text;
            bl.fletes = int.Parse(TB_Fletes.Text);
            bl.mone_flet = TB_MonFletes.Text;

            int result = BLController.Edit(bl);

            if (result == 1)
            {
                PN_Success.Visible = true;
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error al modificar el BL. Ver tabla de Incidente";
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            BL bl = BLController.GetByID(int.Parse(IDBL));
            Response.Redirect("~/AreaViaje/EditarViaje.aspx?ID=" + bl.id_viaje.ToString());
        }

        private void CargarBL(BL bl)
        {
            TB_Naturaleza.Text = bl.num_naturaleza.ToString();
            DDL_Tipo.Value = bl.tipo;
            TB_PtoCarga.Text = bl.pto_carga;
            TB_PtoDescarga.Text = bl.pto_descarga;
            TB_Destino.Text = bl.destino;
            TB_Booking.Text = bl.booking;
            TB_Cond.Text = bl.condicion;
            DDL_TipMercancia.Value = bl.tipo_mercancia;
            TB_NomConsignee.Text = bl.nom_consign;
            TB_DirConsignee.Text = bl.dir_consign;
            TB_NomNotify.Text = bl.nom_notify;
            TB_DirNotify.Text = bl.dir_notify;
            TB_NomExport.Text = bl.nom_export;
            TB_DirExport.Text = bl.dir_export;
            TB_GrossMass.Text = bl.gross_mass.ToString();
            TB_ShipMarks.Text = bl.shipping_marks;
            TB_CantCont.Text = bl.num_conts.ToString();
            TB_Volumen.Text = bl.volumen.ToString();
            TB_Descrip.Text = bl.descripcion;
            DDL_TipPaq.Value = bl.tipo_paq;
            TB_CantPaq.Text = bl.cant_paq.ToString();
            TB_PrecBL.Text = bl.precinto_bl;
            TB_Observa.Text = bl.observaciones;
            CK_Gobierno.Checked = bl.gobierno.Value;
            TB_SobreDim.Text = bl.sobre_dimens;
            TB_Fletes.Text = bl.fletes.ToString();
            TB_MonFletes.Text = bl.mone_flet.ToString();
        }
    }
}