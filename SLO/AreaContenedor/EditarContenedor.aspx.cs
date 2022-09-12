using System;
using DevExpress.Web;
using SLO.Controllers;
using SLO.Models;

namespace SLO.AreaContenedor
{
    public partial class EditarContenedor : System.Web.UI.Page
    {
        private static string IDContenedor;

        protected void Page_Load(object sender, EventArgs e)
        {
            PN_Success.Visible = false;
            PN_Error.Visible = false;

            if (Request.QueryString["ID"] != null)
            {
                Contenedor cont = ContenedorController.GetByID(int.Parse(Request.QueryString["ID"].ToString()));
                
                if (!IsPostBack)
                    CargarContenedor(cont);

                IDContenedor = Request.QueryString["ID"].ToString();
                LBL_IDContenedor.Text = "Editar Contenedor N° " + cont.num_cont;
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "El ID del Contenedor no puede ser nulo";
                PN_ContainerForm.Visible = false;
            }
        }

        protected void BTN_Guardar_Click(object sender, EventArgs e)
        {
            Contenedor cont = new Contenedor();

            try
            {
                cont.ID = int.Parse(IDContenedor);
                cont.num_paq = int.Parse(TB_NumPaq.Text);
                cont.tip_cont = DDL_TipoCont.Value.ToString();
                cont.estado = int.Parse(DDL_Estado.Value.ToString());
                cont.eq_inter_rec1 = TB_Prec1.Text;
                cont.eq_inter_rec2 = TB_Prec2.Text;
                cont.eq_inter_rec3 = TB_Prec3.Text;
                cont.seal_party = DDL_SealPart.Value.ToString();
                cont.peso_neto = decimal.Parse(TB_PesoNeto.Text);
                cont.peso_bruto = decimal.Parse(TB_PesoBruto.Text);
                cont.tamanio = int.Parse(TB_Tamano.Text);
                cont.temper = decimal.Parse(TB_Temp.Text);
                cont.imo = TB_IMO.Text;
                cont.num_un = TB_UNNum.Text;
                cont.ventilac = TB_Ventila.Text;
                cont.descrip_mer = TB_DescripMer.Text;

                int result = ContenedorController.Edit(cont);

                if (result == 1)
                {
                    PN_Success.Visible = true;
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
            Contenedor cont = ContenedorController.GetByID(int.Parse(IDContenedor));
            Response.Redirect("~/AreaBL/EditarBL.aspx?ID=" + cont.id_bl.ToString());
        }

        private void CargarContenedor(Contenedor cont)
        {
            TB_NumPaq.Text = cont.num_paq.ToString();
            DDL_TipoCont.Value = cont.tip_cont;
            DDL_Estado.Value = cont.estado;
            TB_Prec1.Text = cont.eq_inter_rec1;
            TB_Prec2.Text = cont.eq_inter_rec2;
            TB_Prec3.Text = cont.eq_inter_rec3;
            DDL_SealPart.Value = cont.seal_party;

            foreach (ListEditItem item in DDL_SealPart.Items)
            {
                item.Selected = cont.seal_party == item.Value.ToString();
            }

            TB_PesoNeto.Text = cont.peso_neto.ToString();
            TB_PesoBruto.Text = cont.peso_bruto.ToString();
            TB_Tamano.Text = cont.tamanio.ToString();
            TB_Temp.Text = cont.temper.ToString();
            TB_IMO.Text = cont.imo;
            TB_UNNum.Text = cont.num_un;
            TB_Ventila.Text = cont.ventilac;
            TB_DescripMer.Text = cont.descrip_mer;
        }
    }
}