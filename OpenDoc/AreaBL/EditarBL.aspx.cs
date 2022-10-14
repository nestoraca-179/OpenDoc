using System;
using System.Data;
using System.Web.UI;
using OpenDoc.Controllers;
using OpenDoc.Models;

namespace OpenDoc.AreaBL
{
    public partial class EditarBL : System.Web.UI.Page
    {
        private static int IDSelected;
        private static string IDBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            PN_Success.Visible = false;
            PN_Error.Visible = false;

            if (Request.QueryString["ID"] != null)
            {
                BL bl = BLController.GetByID(int.Parse(Request.QueryString["ID"].ToString()));
                
                if (!IsPostBack)
                    CargarBL(bl);

                IDBL = Request.QueryString["ID"].ToString();
                LBL_IDBL.Text = "Editar BL N° " + bl.num_bl.ToString();

                if (Request.QueryString["new"] != null)
                {
                    PN_Success.Visible = true;
                    LBL_Success.Text = "El Contenedor ha sido agregado con éxito";
                }
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
            IDSelected = int.Parse(e.KeyValue.ToString());

            if (e.CommandArgs.CommandName == "Editar")
            {
                Response.Redirect("~/AreaContenedor/EditarContenedor.aspx?ID=" + IDSelected, false);
            }
            else if (e.CommandArgs.CommandName == "Eliminar")
            {
                string num_cont = (GV_GridResultsC.GetRow(e.VisibleIndex) as DataRowView).Row.ItemArray[1].ToString();
                LBL_Delete.Text = string.Format("¿Desea eliminar el Contenedor N° {0}?", num_cont);

                ScriptManager.RegisterStartupScript(this, GetType(), "modal", "openModalDelete()", true);
            }
        }

        protected void BTN_EliminarContenedor_Click(object sender, EventArgs e)
        {
            int result = ContenedorController.Delete(IDSelected);

            if (result == 1)
            {
                PN_Success.Visible = true;
                LBL_Success.Text = "Contenedor eliminado con éxito";
                GV_GridResultsC.DataBind();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
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

                bl.ID = int.Parse(IDBL);
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
                bl.co_us_mo = (Session["USER"] as Usuario).username;
                bl.fe_us_mo = DateTime.Now;

                int result = BLController.Edit(bl);

                if (result == 1)
                {
                    PN_Success.Visible = true;
                    LBL_Success.Text = "El BL ha sido modificado con éxito";
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
            BL bl = BLController.GetByID(int.Parse(IDBL));
            Response.Redirect("~/AreaViaje/EditarViaje.aspx?ID=" + bl.id_viaje.ToString());
        }

        protected void BTN_AgregarContenedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AreaContenedor/AgregarContenedor.aspx?id_bl=" + IDBL);
        }

        private void CargarBL(BL bl)
        {
            TB_Naturaleza.Text = bl.num_naturaleza.ToString();
            DDL_Tipo.Value = bl.tipo;
            DDL_PtoCarga.Value = bl.pto_carga;
            DDL_PtoDescarga.Value = bl.pto_descarga;
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