using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SLO.Models;

namespace SLO.Controllers
{
    public class BLController : Repository
    {
        public static BL GetByID(int id)
        {
            return db.BL.Single(b => b.ID == id);
        }

        public static List<BL> GetAllBlsByViaje(int id_viaje)
        {
            return db.BL.Where(b => b.id_viaje == id_viaje).ToList();
        }

        public static int Add(List<DataRow> rows, int id_viaje)
        {
            int result = 0;
            BL bl = new BL();

            try
            {
                bl.id_viaje = id_viaje;
                bl.num_bl = rows[0].Field<string>(0);
                bl.num_naturaleza = 23;
                bl.tipo = "B/L";
                bl.pto_carga = rows[0].Field<string>(3);
                bl.pto_descarga = rows[0].Field<string>(4);
                bl.destino = "-";
                bl.booking = null;
                bl.condicion = "FCL";
                bl.tipo_mercancia = 0;
                bl.nom_consign = rows[0].Field<string>(9);
                bl.dir_consign = rows[0].Field<string>(10);
                bl.nom_notify = rows[0].Field<string>(12);
                bl.dir_notify = rows[0].Field<string>(13);
                bl.nom_export = rows[0].Field<string>(7);
                bl.dir_export = rows[0].Field<string>(8);
                bl.gross_mass = rows.Select(r => decimal.Parse(r.Field<string>(23))).Sum();
                bl.shipping_marks = "S/M";
                bl.num_conts = rows.Count;
                bl.volumen = 0;
                bl.descripcion = rows[0].Field<string>(15).Split('/')[1];
                bl.tipo_paq = "PT";
                bl.cant_paq = rows.Select(r => int.Parse(Regex.Match(r.Field<string>(19), @"\d+").Value)).Sum();
                bl.precinto_bl = null;
                bl.sobre_dimens = null;
                bl.observaciones = null;
                bl.gobierno = false;
                bl.fletes = 0;
                bl.mone_flet = "USD";

                BL new_bl = db.BL.Add(bl);
                db.SaveChanges();

                var containers = rows.Select(r => r.Field<string>(16)).ToList();
                foreach (string container in containers)
                {
                    DataRow row_container = rows.Single(r => r.Field<string>(16) == container);
                    result = ContenedorController.Add(row_container, new_bl.ID);

                    if (result == 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR INSERTANDO BL N° {0}", bl.num_bl), ex);
                throw ex;
            }

            return result;
        }

        public static int Edit(BL bl)
        {
            int result = 0;

            try
            {
                BL existing = GetByID(bl.ID);
                bl.id_viaje = existing.id_viaje;
                bl.num_bl = existing.num_bl;

                db.Entry(existing).CurrentValues.SetValues(bl);
                db.SaveChanges();

                result = 1;
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR MODIFICANDO VIAJE N° {0}", bl.num_bl), ex);
            }

            return result;
        }
    }
}