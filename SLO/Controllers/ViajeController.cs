using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SLO.Models;

namespace SLO.Controllers
{
    public class ViajeController : Repository
    {
        public static Viaje GetByID(int id)
        {
            return db.Viaje.Single(v => v.ID == id);
        }

        public static Viaje GetByFile(string file)
        {
            return db.Viaje.Single(v => v.file_path == file);
        }

        public static int Add(EnumerableRowCollection<DataRow> table, string path, string user)
        {
            int result = 0;
            Viaje viaje = new Viaje();

            try
            {
                viaje.cod_adua = "5001";
                viaje.num_viaj = table.Select(r => r.Field<string>(1)).First();
                viaje.fec_sal = DateTime.Now;
                viaje.fec_arr = DateTime.Now;
                viaje.loc_cod = "5001079AGD";
                viaje.uso = 1;
                viaje.total_bls = table.GroupBy(r => r.Field<string>(0)).Select(g => g.First()).ToList().Count;
                viaje.total_paq = table.Select(r => int.Parse(Regex.Match(r.Field<string>(19), @"\d+").Value)).Sum();
                viaje.total_cont = table.ToList().Count;
                viaje.total_gm = table.Select(r => decimal.Parse(r.Field<string>(23))).Sum();
                viaje.cod_carr = "ATI089";
                viaje.nom_carr = "INTERSHIPPING, C.A";
                viaje.dir_carr = "AVE. SOUBLETTE, CENTRO COMERCIAL LITORAL PISO 4 OFICINA 3 LA GUAIRA.";
                viaje.cod_mod_trans = 1;
                viaje.id_trans = table.Select(r => r.Field<string>(2)).First();
                viaje.cod_nac_trans = "AC";
                viaje.cod_pto_sal = "JMKIN";
                viaje.cod_pto_des = "VELAG";
                viaje.cod_lin = null;
                viaje.alm_dest = null;
                viaje.cod_buq = null;
                viaje.nom_buq = table.Select(r => r.Field<string>(2)).First();
                viaje.file_path = path;
                viaje.date_uploaded = DateTime.Now;
                viaje.uploaded_by = user;

                Viaje new_viaje = db.Viaje.Add(viaje);
                db.SaveChanges();

                var bls = table.GroupBy(r => r.Field<string>(0)).Select(g => g.First()).Select(r => r.Field<string>(0)).ToList();
                foreach (string bl in bls)
                {
                    List<DataRow> containers = table.Where(r => r.Field<string>(0) == bl).ToList();
                    BLController.Add(containers, new_viaje.ID);
                }

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR INSERTANDO VIAJE N° {0}", viaje.num_viaj), ex);
            }

            return result;
        }

        public static int Add(Viaje viaje)
        {
            int result = 0;

            try
            {
                db.Viaje.Add(viaje);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR AGREGANDO VIAJE N° {0}", viaje.num_viaj), ex);
            }

            return result;
        }
        
        public static int Edit(Viaje viaje)
        {
            int result = 0;

            try
            {
                Viaje existing = GetByID(viaje.ID);
                viaje.date_uploaded = existing.date_uploaded;
                viaje.uploaded_by = existing.uploaded_by;
                viaje.file_path = existing.file_path;

                db.Entry(existing).CurrentValues.SetValues(viaje);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR MODIFICANDO VIAJE N° {0}", viaje.num_viaj), ex);
            }

            return result;
        }

        public static int Delete(int ID)
        {
            int result = 0;
            Viaje viaje = GetByID(ID);

            try
            {
                db.Viaje.Remove(viaje);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR ELIMINANDO VIAJE N° {0}", viaje.num_viaj), ex);
            }

            return result;
        }
    }
}