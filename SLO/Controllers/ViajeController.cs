using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SLO.Models;
using System.Reflection;
using System.Data.Entity;

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

            Viaje viaje = new Viaje(), new_viaje = new Viaje();

            using (DbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    viaje.cod_adua = "5004";
                    // viaje.cod_adua = "5001"; LA GUAIRA
                    viaje.num_viaj = table.Select(r => r.Field<string>(1)).First();
                    viaje.fec_sal = DateTime.Now;
                    viaje.fec_arr = DateTime.Now;
                    viaje.loc_cod = "5004016DT";
                    // viaje.loc_cod = "5001079AGD"; LA GUAIRA
                    viaje.uso = 1;
                    viaje.total_bls = table.GroupBy(r => r.Field<string>(0)).Select(g => g.First()).ToList().Count;
                    viaje.total_paq = table.Select(r => int.Parse(Regex.Match(r.Field<string>(19), @"\d+").Value)).Sum();
                    viaje.total_cont = table.ToList().Count;
                    viaje.total_gm = table.Select(r => decimal.Parse(r.Field<string>(21).Replace(".", ","))).Sum();
                    viaje.cod_carr = "ATI089";
                    viaje.nom_carr = "INTERSHIPPING, C.A";
                    viaje.dir_carr = "AVE. SOUBLETTE, CENTRO COMERCIAL LITORAL PISO 4 OFICINA 3 LA GUAIRA.";
                    viaje.cod_mod_trans = 1;
                    viaje.id_trans = table.Select(r => r.Field<string>(2)).First();
                    viaje.cod_nac_trans = ""; // "AC";
                    viaje.cod_pto_sal = ""; //  "JMKIN";
                    viaje.cod_pto_des = ""; //  "VELAG";
                    viaje.cod_lin = null;
                    viaje.alm_dest = null;
                    viaje.cod_buq = null;
                    viaje.nom_buq = table.Select(r => r.Field<string>(2)).First();
                    viaje.file_path = path;
                    viaje.co_us_in = user;
                    viaje.fe_us_in = DateTime.Now;
                    viaje.co_us_mo = user;
                    viaje.fe_us_mo = DateTime.Now;

                    new_viaje = db.Viaje.Add(viaje);
                    db.SaveChanges();

                    var bls = table.GroupBy(r => r.Field<string>(0)).Select(g => g.First()).Select(r => r.Field<string>(0)).ToList();
                    foreach (string bl in bls)
                    {
                        List<DataRow> containers = table.Where(r => r.Field<string>(0) == bl).ToList();
                        BLController.Add(containers, new_viaje.ID, user);
                    }

                    LogController.CreateLog(user, "VIAJE", new_viaje.ID, "I", null);
                    tran.Commit();
                    result = 1;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    IncidentController.CreateIncident(string.Format("ERROR INSERTANDO VIAJE N° {0}", viaje.num_viaj), ex);
                }
            }

            return result;
        }

        public static int Add(Viaje viaje)
        {
            int result = 0;

            try
            {
                Viaje v = db.Viaje.Add(viaje);
                db.SaveChanges();

                LogController.CreateLog(v.co_us_in, "VIAJE", v.ID, "I", null);
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
                viaje.co_us_in = existing.co_us_in;
                viaje.fe_us_in = existing.fe_us_in;
                viaje.file_path = existing.file_path;

                string campos = GetChanges(existing, viaje);

                db.Entry(existing).CurrentValues.SetValues(viaje);
                db.SaveChanges();

                LogController.CreateLog(viaje.co_us_mo, "VIAJE", viaje.ID, "M", campos);
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
                Viaje v = db.Viaje.Remove(viaje);
                db.SaveChanges();

                LogController.CreateLog(v.co_us_in, "VIAJE", v.ID, "E", null);
                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR ELIMINANDO VIAJE N° {0}", viaje.num_viaj), ex);
            }

            return result;
        }

        private static string GetChanges(Viaje old_v, Viaje new_v)
        {
            string campos = "";
            Type type = new Viaje().GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.Name != "fe_us_in" && prop.Name != "fe_us_mo")
                {
                    string valor1 = prop.GetValue(old_v) == null ? "" : prop.GetValue(old_v).ToString();
                    string valor2 = prop.GetValue(new_v) == null ? "" : prop.GetValue(new_v).ToString();

                    if (valor1 != valor2)
                    {
                        campos += string.Format("{0}: {1} -> {2}; ", prop.Name, valor1, valor2);
                    }
                }
            }

            return campos;
        }
    }
}