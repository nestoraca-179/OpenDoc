using OpenDoc.Controllers;
using OpenDoc.Models;
using System;

namespace OpenDoc.AreaViaje
{
    public partial class AgregarViaje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_IDViaje.Text = "Agregar Viaje";
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            Viaje viaje = new Viaje();

            try
            {
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
                viaje.cod_pto_sal = DDL_PtoSalida.Value.ToString();
                viaje.cod_pto_des = DDL_PtoDestino.Value.ToString();
                viaje.cod_lin = TB_CodLinea.Text;
                viaje.alm_dest = TB_AlmDest.Text;
                viaje.cod_buq = TB_CodBuq.Text;
                viaje.nom_buq = TB_NomBuq.Text;
                viaje.file_path = "SIN ARCHIVO";
                viaje.co_us_in = (Session["USER"] as Usuario).username;
                viaje.fe_us_in = DateTime.Now;
                viaje.co_us_mo = (Session["USER"] as Usuario).username;
                viaje.fe_us_mo = DateTime.Now;

                int result = ViajeController.Add(viaje);

                if (result == 1)
                {
                    Response.Redirect("~/Documentacion.aspx?new=1");
                }
                else
                {
                    PN_Error.Visible = true;
                    LBL_Error.Text = "Ha ocurrido un error al agregar el Viaje. Ver tabla de Incidentes";
                }
            }
            catch (Exception ex)
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
                IncidentController.CreateIncident(string.Format("ERROR PROCESANDO DATOS DEL VIAJE {0}", viaje.num_viaj), ex);
            }
        }

        protected void BTN_Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Documentacion.aspx");
        }
    }
}