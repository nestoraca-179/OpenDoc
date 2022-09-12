﻿using SLO.Controllers;
using SLO.Models;
using System;

namespace SLO.AreaBL
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
                bl.id_viaje = int.Parse(IDViaje);
                bl.num_bl = TB_NumBL.Text;
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