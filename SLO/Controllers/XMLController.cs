using SLO.Models;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SLO.Controllers
{
    public class XMLController
    {
        public static string GenerarXMLVEPBL(string folder, int id_viaje)
        {
            string result = "";

            Viaje viaje = ViajeController.GetByID(id_viaje);
            List<BL> bls = BLController.GetAllBlsByViaje(id_viaje);
            string file = folder + viaje.num_viaj + ".xml";

            try
            {
                // DECLARACION
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(docNode);

                // NODO AWMDS
                XmlNode mainNode = doc.CreateElement("Awmds");

                // ATRIBUTOS
                /*XmlAttribute attr1 = doc.CreateAttribute("xmlns:xs"), attr2 = doc.CreateAttribute("xmlns:fn");
                attr1.Value = "http://www.w3.org/2001/XMLSchema";
                attr2.Value = "http://www.w3.org/2005/xpath-functions";

                // AGREGANDO ATRIBUTOS
                mainNode.Attributes.Append(attr1);
                mainNode.Attributes.Append(attr2);*/

                // NODO GENERAL_SEGMENT
                XmlNode generalSegment = doc.CreateElement("General_segment");
                mainNode.AppendChild(generalSegment);

                #region NODOS HIJOS GENERAL_SEGMENT
                // NODO GENERAL_SEGMENT_ID
                XmlNode generalSegmentID = doc.CreateElement("General_segment_id");
                generalSegment.AppendChild(generalSegmentID);

                generalSegmentID.AppendChild(CreateNode(doc, "Customs_office_code", viaje.cod_adua));
                generalSegmentID.AppendChild(CreateNode(doc, "Voyage_number", viaje.num_viaj));
                generalSegmentID.AppendChild(CreateNode(doc, "Date_of_departure", viaje.fec_sal.Value.ToString("yyyy-MM-dd")));
                generalSegmentID.AppendChild(CreateNode(doc, "Date_of_arrival", viaje.fec_arr.Value.ToString("yyyy-MM-dd")));

                // NODO TOTALS_SEGMENT
                XmlNode totalsSegment = doc.CreateElement("Totals_segment");
                generalSegment.AppendChild(totalsSegment);

                totalsSegment.AppendChild(CreateNode(doc, "Total_number_of_bols", viaje.total_bls.ToString()));
                totalsSegment.AppendChild(CreateNode(doc, "Total_number_of_packages", viaje.total_paq.ToString()));
                totalsSegment.AppendChild(CreateNode(doc, "Total_number_of_containers", viaje.total_cont.ToString()));
                totalsSegment.AppendChild(CreateNode(doc, "Total_gross_mass", viaje.total_gm.ToString().Replace(",", ".")));

                // NODO TRANSPORT INFORMATION
                XmlNode transportInfo = doc.CreateElement("Transport_information");
                generalSegment.AppendChild(transportInfo);

                XmlNode carrierNode = doc.CreateElement("Carrier");
                carrierNode.AppendChild(CreateNode(doc, "Carrier_code", viaje.cod_carr));
                carrierNode.AppendChild(CreateNode(doc, "Carrier_name", viaje.nom_carr));
                carrierNode.AppendChild(CreateNode(doc, "Carrier_address", viaje.dir_carr));

                transportInfo.AppendChild(carrierNode);
                transportInfo.AppendChild(CreateNode(doc, "Mode_of_transport_code", viaje.cod_mod_trans.ToString()));
                transportInfo.AppendChild(CreateNode(doc, "Identity_of_transporter", viaje.id_trans));
                transportInfo.AppendChild(CreateNode(doc, "Nationality_of_transporter_code", viaje.cod_nac_trans));

                // NODO LOAD UNLOAD PLACE
                XmlNode loadUnload = doc.CreateElement("Load_unload_place");
                generalSegment.AppendChild(loadUnload);

                loadUnload.AppendChild(CreateNode(doc, "Place_of_departure_code", viaje.cod_pto_sal));
                loadUnload.AppendChild(CreateNode(doc, "Place_of_destination_code", viaje.cod_pto_des));
                #endregion

                #region NODOS BOL SEGMENT
                int number = 1;
                foreach (BL bl in bls)
                {
                    List<Contenedor> conts = ContenedorController.GetAllContsByBL(bl.ID);

                    XmlNode bolSegment = doc.CreateElement("Bol_segment");
                    mainNode.AppendChild(bolSegment);

                    // NODO BOL ID
                    XmlNode bolId = doc.CreateElement("Bol_id");
                    bolSegment.AppendChild(bolId);

                    bolId.AppendChild(CreateNode(doc, "Bol_reference", bl.num_bl));
                    bolId.AppendChild(CreateNode(doc, "Line_number", number.ToString()));
                    bolId.AppendChild(CreateNode(doc, "Bol_nature", bl.num_naturaleza.ToString()));
                    bolId.AppendChild(CreateNode(doc, "Bol_type_code", bl.tipo));

                    // NODO LOAD UNLOAD PLACE
                    XmlNode loadUnloadPlace = doc.CreateElement("Load_unload_place");
                    bolSegment.AppendChild(loadUnloadPlace);

                    loadUnloadPlace.AppendChild(CreateNode(doc, "Place_of_loading_code", bl.pto_carga));
                    loadUnloadPlace.AppendChild(CreateNode(doc, "Place_of_unloading_code", bl.pto_descarga));

                    // NODO TRADERS SEGMENT
                    XmlNode tradersSegment = doc.CreateElement("Traders_segment");
                    bolSegment.AppendChild(tradersSegment);

                    XmlNode exporter = doc.CreateElement("Exporter");
                    XmlNode notify = doc.CreateElement("Notify");
                    XmlNode consignee = doc.CreateElement("Consignee");

                    exporter.AppendChild(CreateNode(doc, "Exporter_name", bl.nom_export));
                    exporter.AppendChild(CreateNode(doc, "Exporter_address", bl.dir_export));
                    tradersSegment.AppendChild(exporter);

                    notify.AppendChild(CreateNode(doc, "Notify_name", bl.nom_notify));
                    notify.AppendChild(CreateNode(doc, "Notify_address", bl.dir_notify));
                    tradersSegment.AppendChild(notify);

                    consignee.AppendChild(CreateNode(doc, "Consignee_name", bl.nom_consign));
                    consignee.AppendChild(CreateNode(doc, "Consignee_address", bl.dir_consign));
                    tradersSegment.AppendChild(consignee);

                    foreach (Contenedor cont in conts)
                    {
                        // NODO CTN SEGMENT
                        XmlNode ctnSegment = doc.CreateElement("ctn_segment");
                        bolSegment.AppendChild(ctnSegment);

                        ctnSegment.AppendChild(CreateNode(doc, "Ctn_reference", cont.num_cont));
                        ctnSegment.AppendChild(CreateNode(doc, "Number_of_packages", cont.num_paq.ToString()));
                        ctnSegment.AppendChild(CreateNode(doc, "Type_of_container", cont.tip_cont));
                        ctnSegment.AppendChild(CreateNode(doc, "Empty_Full", "00" + cont.estado.ToString()));
                        ctnSegment.AppendChild(CreateNode(doc, "Marks1", cont.eq_inter_rec1));
                        ctnSegment.AppendChild(CreateNode(doc, "Marks2", cont.eq_inter_rec2));
                        ctnSegment.AppendChild(CreateNode(doc, "Marks3", cont.eq_inter_rec3));
                        ctnSegment.AppendChild(CreateNode(doc, "Sealing_Party", cont.seal_party));
                    }

                    // NODO GOODS SEGMENT
                    XmlNode goodsSegment = doc.CreateElement("Goods_segment");
                    bolSegment.AppendChild(goodsSegment);

                    goodsSegment.AppendChild(CreateNode(doc, "Number_of_packages", bl.cant_paq.ToString()));
                    goodsSegment.AppendChild(CreateNode(doc, "Package_type_code", bl.tipo_paq));
                    goodsSegment.AppendChild(CreateNode(doc, "Gross_mass", bl.gross_mass.ToString().Replace(",", ".")));
                    goodsSegment.AppendChild(CreateNode(doc, "Shipping_marks", bl.shipping_marks));
                    goodsSegment.AppendChild(CreateNode(doc, "Goods_description", bl.descripcion));

                    XmlNode sealsSegment = doc.CreateElement("Seals_segment");
                    goodsSegment.AppendChild(sealsSegment);

                    sealsSegment.AppendChild(CreateNode(doc, "Number_of_seals", "0"));
                    sealsSegment.AppendChild(CreateNode(doc, "Marks_of_seals", ""));

                    goodsSegment.AppendChild(CreateNode(doc, "Volume_in_cubic_meters", bl.volumen.ToString().Replace(",", ".")));
                    goodsSegment.AppendChild(CreateNode(doc, "Num_of_ctn_for_this_bol", bl.num_conts.ToString()));

                    // NODO VALUE SEGMENT
                    XmlNode valueSegment = doc.CreateElement("Value_segment");
                    bolSegment.AppendChild(valueSegment);

                    // NODO LOCATION
                    XmlNode location = doc.CreateElement("Location");
                    bolSegment.AppendChild(location);

                    location.AppendChild(CreateNode(doc, "Location_code", viaje.loc_cod));

                    // INCREMENTO DEL NUMERO
                    number++;
                }
                #endregion

                doc.AppendChild(mainNode);
                doc.Save(file);

                result = viaje.num_viaj + ".xml";
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR GENERANDO ARCHIVO XML {0}", viaje.num_viaj), ex);
            }

            return result;
        }

        public static string GenerarXMLVELAG(string folder, int id_viaje)
        {
            string result = "";

            Viaje viaje = ViajeController.GetByID(id_viaje);
            List<BL> bls = BLController.GetAllBlsByViaje(id_viaje);
            string file = folder + viaje.num_viaj + ".xml";

            try
            {
                // DECLARACION
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                doc.AppendChild(docNode);

                // NODO AWMDS
                XmlNode mainNode = doc.CreateElement("Awmds");

                // ATRIBUTOS
                XmlAttribute attr1 = doc.CreateAttribute("xmlns:xs"), attr2 = doc.CreateAttribute("xmlns:fn");
                attr1.Value = "http://www.w3.org/2001/XMLSchema";
                attr2.Value = "http://www.w3.org/2005/xpath-functions";

                // AGREGANDO ATRIBUTOS
                mainNode.Attributes.Append(attr1);
                mainNode.Attributes.Append(attr2);

                // NODO GENERAL_SEGMENT
                XmlNode generalSegment = doc.CreateElement("General_segment");
                mainNode.AppendChild(generalSegment);

                #region NODOS HIJOS GENERAL_SEGMENT
                // NODO GENERAL_SEGMENT_ID
                XmlNode generalSegmentID = doc.CreateElement("General_segment_id");
                generalSegment.AppendChild(generalSegmentID);

                generalSegmentID.AppendChild(CreateNode(doc, "Customs_office_code", viaje.cod_adua));
                generalSegmentID.AppendChild(CreateNode(doc, "Voyage_number", viaje.num_viaj));
                generalSegmentID.AppendChild(CreateNode(doc, "Date_of_departure", viaje.fec_sal.Value.ToString("yyyy-MM-dd")));
                generalSegmentID.AppendChild(CreateNode(doc, "Date_of_arrival", viaje.fec_arr.Value.ToString("yyyy-MM-dd")));

                // NODO TOTALS_SEGMENT
                XmlNode totalsSegment = doc.CreateElement("Totals_segment");
                generalSegment.AppendChild(totalsSegment);

                totalsSegment.AppendChild(CreateNode(doc, "Total_number_of_bols", viaje.total_bls.ToString()));
                totalsSegment.AppendChild(CreateNode(doc, "Total_number_of_packages", viaje.total_paq.ToString()));
                totalsSegment.AppendChild(CreateNode(doc, "Total_number_of_containers", viaje.total_cont.ToString()));
                totalsSegment.AppendChild(CreateNode(doc, "Total_gross_mass", viaje.total_gm.ToString().Replace(",", ".")));

                // NODO TRANSPORT INFORMATION
                XmlNode transportInfo = doc.CreateElement("Transport_information");
                generalSegment.AppendChild(transportInfo);

                XmlNode carrierNode = doc.CreateElement("Carrier");
                carrierNode.AppendChild(CreateNode(doc, "Carrier_code", viaje.cod_carr));
                carrierNode.AppendChild(CreateNode(doc, "Carrier_name", viaje.nom_carr));
                carrierNode.AppendChild(CreateNode(doc, "Carrier_address", viaje.dir_carr));

                transportInfo.AppendChild(carrierNode);
                transportInfo.AppendChild(CreateNode(doc, "Mode_of_transport_code", viaje.cod_mod_trans.ToString()));
                transportInfo.AppendChild(CreateNode(doc, "Identity_of_transporter", viaje.id_trans));
                transportInfo.AppendChild(CreateNode(doc, "Nationality_of_transporter_code", viaje.cod_nac_trans));

                // NODO LOAD UNLOAD PLACE
                XmlNode loadUnload = doc.CreateElement("Load_unload_place");
                generalSegment.AppendChild(loadUnload);

                loadUnload.AppendChild(CreateNode(doc, "Place_of_departure_code", viaje.cod_pto_sal));
                loadUnload.AppendChild(CreateNode(doc, "Place_of_destination_code", viaje.cod_pto_des));
                #endregion

                #region NODOS BOL SEGMENT
                int number = 1;
                foreach (BL bl in bls)
                {
                    List<Contenedor> conts = ContenedorController.GetAllContsByBL(bl.ID);

                    XmlNode bolSegment = doc.CreateElement("Bol_segment");
                    mainNode.AppendChild(bolSegment);

                    // NODO BOL ID
                    XmlNode bolId = doc.CreateElement("Bol_id");
                    bolSegment.AppendChild(bolId);

                    bolId.AppendChild(CreateNode(doc, "Bol_reference", bl.num_bl));
                    bolId.AppendChild(CreateNode(doc, "Line_number", number.ToString()));
                    bolId.AppendChild(CreateNode(doc, "Bol_nature", bl.num_naturaleza.ToString()));
                    bolId.AppendChild(CreateNode(doc, "Bol_type_code", bl.tipo));

                    // NODO LOAD UNLOAD PLACE
                    XmlNode loadUnloadPlace = doc.CreateElement("Load_unload_place");
                    bolSegment.AppendChild(loadUnloadPlace);

                    loadUnloadPlace.AppendChild(CreateNode(doc, "Place_of_loading_code", bl.pto_carga));
                    loadUnloadPlace.AppendChild(CreateNode(doc, "Place_of_unloading_code", bl.pto_descarga));

                    // NODO TRADERS SEGMENT
                    XmlNode tradersSegment = doc.CreateElement("Traders_segment");
                    bolSegment.AppendChild(tradersSegment);

                    XmlNode exporter = doc.CreateElement("Exporter");
                    XmlNode notify = doc.CreateElement("Notify");
                    XmlNode consignee = doc.CreateElement("Consignee");

                    exporter.AppendChild(CreateNode(doc, "Exporter_name", bl.nom_export));
                    exporter.AppendChild(CreateNode(doc, "Exporter_address", bl.dir_export));
                    tradersSegment.AppendChild(exporter);

                    notify.AppendChild(CreateNode(doc, "Notify_name", bl.nom_notify));
                    notify.AppendChild(CreateNode(doc, "Notify_address", bl.dir_notify));
                    tradersSegment.AppendChild(notify);

                    consignee.AppendChild(CreateNode(doc, "Consignee_name", bl.nom_consign));
                    consignee.AppendChild(CreateNode(doc, "Consignee_address", bl.dir_consign));
                    tradersSegment.AppendChild(consignee);

                    foreach (Contenedor cont in conts)
                    {
                        // NODO CTN SEGMENT
                        XmlNode ctnSegment = doc.CreateElement("ctn_segment");
                        bolSegment.AppendChild(ctnSegment);

                        ctnSegment.AppendChild(CreateNode(doc, "Ctn_reference", cont.num_cont));
                        ctnSegment.AppendChild(CreateNode(doc, "Number_of_packages", cont.num_paq.ToString()));
                        ctnSegment.AppendChild(CreateNode(doc, "Type_of_container", cont.tip_cont));
                        ctnSegment.AppendChild(CreateNode(doc, "Empty_Full", "00" + cont.estado.ToString()));
                        ctnSegment.AppendChild(CreateNode(doc, "Marks1", cont.eq_inter_rec1));
                        ctnSegment.AppendChild(CreateNode(doc, "Marks2", cont.eq_inter_rec2));
                        ctnSegment.AppendChild(CreateNode(doc, "Marks3", cont.eq_inter_rec3));
                        ctnSegment.AppendChild(CreateNode(doc, "Sealing_Party", cont.seal_party));
                    }

                    // NODO GOODS SEGMENT
                    XmlNode goodsSegment = doc.CreateElement("Goods_segment");
                    bolSegment.AppendChild(goodsSegment);

                    goodsSegment.AppendChild(CreateNode(doc, "Number_of_packages", bl.cant_paq.ToString()));
                    goodsSegment.AppendChild(CreateNode(doc, "Package_type_code", bl.tipo_paq));
                    goodsSegment.AppendChild(CreateNode(doc, "Gross_mass", bl.gross_mass.ToString().Replace(",", ".")));
                    goodsSegment.AppendChild(CreateNode(doc, "Shipping_marks", bl.shipping_marks));
                    goodsSegment.AppendChild(CreateNode(doc, "Goods_description", bl.descripcion));
                    goodsSegment.AppendChild(CreateNode(doc, "Volume_in_cubic_meters", bl.volumen.ToString().Replace(",", ".")));
                    goodsSegment.AppendChild(CreateNode(doc, "Num_of_ctn_for_this_bol", bl.num_conts.ToString()));

                    // NODO LOCATION
                    XmlNode location = doc.CreateElement("Location");
                    bolSegment.AppendChild(location);

                    location.AppendChild(CreateNode(doc, "Location_code", viaje.loc_cod));

                    // INCREMENTO DEL NUMERO
                    number++;
                }
                #endregion

                doc.AppendChild(mainNode);
                doc.Save(file);

                result = viaje.num_viaj + ".xml";
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident(string.Format("ERROR GENERANDO ARCHIVO XML {0}", viaje.num_viaj), ex);
            }

            return result;
        }

        private static XmlNode CreateNode(XmlDocument doc, string name, string v)
        {
            XmlNode node = doc.CreateElement(name);
            XmlText value = doc.CreateTextNode(v);
            node.AppendChild(value);

            return node;
        }
    }
}