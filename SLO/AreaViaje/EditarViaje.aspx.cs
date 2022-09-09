using System;
using DevExpress.Web;
using SLO.Controllers;
using SLO.Models;

namespace SLO.AreaViaje
{
    public partial class EditarViaje : System.Web.UI.Page
    {
        private static string IDViaje;

        protected void Page_Load(object sender, EventArgs e)
        {
            PN_Success.Visible = false;
            PN_Error.Visible = false;

            if (Request.QueryString["ID"] != null)
            {
                Viaje viaje = ViajeController.GetByID(int.Parse(Request.QueryString["ID"].ToString()));
                
                if (!IsPostBack)
                    CargarViaje(viaje);

                IDViaje = Request.QueryString["ID"].ToString();
                LBL_IDViaje.Text = "Editar Viaje N° " + viaje.num_viaj;
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "El ID del Viaje no puede ser nulo";
                PN_ContainerForm.Visible = false;
                GV_GridResultsB.Visible = false;
            }
        }

        protected void GV_GridResultsB_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName == "Editar")
                Response.Redirect("~/AreaBL/EditarBL.aspx?ID=" + e.KeyValue.ToString(), false);
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            Viaje viaje = new Viaje();

            viaje.ID = int.Parse(IDViaje);
            viaje.cod_adua = DDL_CodAduana.Value.ToString();
            viaje.num_viaj = TB_NumViaje.Text;
            viaje.fec_sal = DateTime.Parse(DE_FecSalida.Value.ToString());
            viaje.fec_arr = DateTime.Parse(DE_FecArribo.Value.ToString());
            viaje.loc_cod = TB_LocCode.Text;
            viaje.uso = int.Parse(DDL_Uso.Value.ToString());
            viaje.total_bls = int.Parse(TB_TotBls.Text);
            viaje.total_paq = int.Parse(TB_TotPaq.Text);
            viaje.total_cont = int.Parse(TB_TotConts.Text);
            viaje.total_gm = decimal.Parse(TB_TotGM.Text);
            viaje.cod_carr = TB_CodCarr.Text;
            viaje.nom_carr = TB_NomCarr.Text;
            viaje.dir_carr = TB_DirCarr.Text;
            viaje.cod_mod_trans = int.Parse(DDL_CodModTrans.Value.ToString());
            viaje.id_trans = TB_IDTrans.Text;
            viaje.cod_nac_trans = DDL_CodNacTrans.Value.ToString();
            viaje.cod_pto_sal = TB_PtoSalida.Text;
            viaje.cod_pto_des = TB_PtoDestino.Text;
            viaje.cod_lin = TB_CodLinea.Text;
            viaje.alm_dest = TB_AlmDest.Text;
            viaje.cod_buq = TB_CodBuq.Text;
            viaje.nom_buq = TB_NomBuq.Text;

            int result = ViajeController.Edit(viaje);

            if (result == 1)
            {
                PN_Success.Visible = true;
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error al modificar el Viaje. Ver tabla de Incidentes";
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Documentacion.aspx");
        }

        private void CargarViaje(Viaje viaje)
        {
            DDL_CodAduana.Value = viaje.cod_adua;
            TB_NumViaje.Text = viaje.num_viaj;
            DE_FecArribo.Value = viaje.fec_arr;
            DE_FecSalida.Value = viaje.fec_sal;
            TB_LocCode.Text = viaje.loc_cod;
            DDL_Uso.Value = viaje.uso;

            foreach (ListEditItem item in DDL_Uso.Items)
            {
                item.Selected = viaje.uso.ToString() == item.Value.ToString();
            }

            TB_TotBls.Text = viaje.total_bls.ToString();
            TB_TotPaq.Text = viaje.total_paq.ToString();
            TB_TotConts.Text = viaje.total_cont.ToString();
            TB_TotGM.Text = viaje.total_gm.ToString();
            TB_CodCarr.Text = viaje.cod_carr;
            TB_NomCarr.Text = viaje.nom_carr;
            TB_DirCarr.Text = viaje.dir_carr;
            DDL_CodModTrans.Value = viaje.cod_mod_trans;
            TB_IDTrans.Text = viaje.id_trans;
            DDL_CodNacTrans.Value = viaje.cod_nac_trans;
            TB_PtoSalida.Text = viaje.cod_pto_sal;
            TB_PtoDestino.Text = viaje.cod_pto_des;
            TB_CodLinea.Text = viaje.cod_lin;
            TB_AlmDest.Text = viaje.alm_dest;
            TB_CodBuq.Text = viaje.cod_buq;
            TB_NomBuq.Text = viaje.nom_buq;
        }
    }
}