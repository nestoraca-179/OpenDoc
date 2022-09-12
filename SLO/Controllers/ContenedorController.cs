using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SLO.Models;

namespace SLO.Controllers
{
    public class ContenedorController : Repository
    {
        public static Contenedor GetByID(int id)
        {
            return db.Contenedor.Single(c => c.ID == id);
        }

        public static List<Contenedor> GetAllContsByBL(int id_bl)
        {
            return db.Contenedor.Where(c => c.id_bl == id_bl).ToList();
        }

        public static void Add(DataRow row, int id_bl)
        {
            Contenedor cont = new Contenedor();

            try
            {
                cont.id_bl = id_bl;
                cont.num_cont = row.Field<string>(16);
                cont.num_paq = int.Parse(Regex.Match(row.Field<string>(19), @"\d+").Value);
                cont.tip_cont = GetContType(row.Field<string>(17));
                cont.estado = 5;
                cont.eq_inter_rec1 = row.Field<string>(20);
                cont.eq_inter_rec2 = row.Field<string>(24);
                cont.eq_inter_rec3 = row.Field<string>(25);
                cont.seal_party = "CR";
                cont.peso_neto = decimal.Parse(row.Field<string>(21));
                cont.peso_bruto = decimal.Parse(row.Field<string>(23));
                cont.tamanio = int.Parse(Regex.Match(row.Field<string>(17), @"\d+").Value);
                cont.temper = string.IsNullOrEmpty(row.Field<string>(27).Trim()) ? 0 : decimal.Parse(row.Field<string>(27).Trim());
                cont.imo = string.IsNullOrEmpty(row.Field<string>(26).Replace(" ", "")) ? "" : row.Field<string>(26).Split('/')[0].Split('-')[1].Trim();
                cont.num_un = row.Field<string>(26).Split('/')[0].Split('-')[0].Trim();
                cont.ventilac = null;
                cont.descrip_mer = null;

                Contenedor new_cont = db.Contenedor.Add(cont);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR INSERTANDO CONTENEDOR N° {0}", cont.num_cont), ex);
                throw ex;
            }
        }

        public static int Add(Contenedor cont)
        {
            int result = 0;

            try
            {
                db.Contenedor.Add(cont);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR AGREGANDO CONTENEDOR N° {0}", cont.num_cont), ex);
            }

            return result;
        }

        public static int Edit(Contenedor cont)
        {
            int result = 0;

            try
            {
                Contenedor existing = GetByID(cont.ID);
                cont.id_bl = existing.id_bl;
                cont.num_cont = existing.num_cont;

                db.Entry(existing).CurrentValues.SetValues(cont);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR MODIFICANDO VIAJE N° {0}", cont.num_cont), ex);
            }

            return result;
        }

        public static int Delete(int ID)
        {
            int result = 0;
            Contenedor cont = GetByID(ID);

            try
            {
                db.Contenedor.Remove(cont);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR ELIMINANDO CONTENEDOR N° {0}", cont.num_cont), ex);
            }

            return result;
        }

        private static string GetContType(string type)
        {
            string new_type = "";

            switch (type)
            {
                case "20DC":
                case "20ST":
                    new_type = "2000";
                    break;
                case "20RF":
                    new_type = "2032";
                    break;
                case "20OT":
                    new_type = "2050";
                    break;
                case "20FR":
                case "20FF":
                case "22G1":
                    new_type = "2260";
                    break;
                case "20TK":
                    new_type = "2270";
                    break;
                case "40DC":
                case "40ST":
                    new_type = "4000";
                    break;
                case "40OT":
                    new_type = "4050";
                    break;
                case "40FR":
                    new_type = "4060";
                    break;
                case "40HC":
                case "45G1":
                    new_type = "4400";
                    break;
                case "40RH":
                case "40RF":
                    new_type = "4432";
                    break;
            }

            return new_type;
        }
    }
}