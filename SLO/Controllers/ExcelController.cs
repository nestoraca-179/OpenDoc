using System.Web;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using ExcelDataReader;
using System.Configuration;
using System.Data.SqlClient;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;
using SLO.Models;

namespace SLO.Controllers
{
    public class ExcelController
    {
        public bool IsExcel(HttpPostedFile file)
        {
            string ext = Path.GetExtension(file.FileName).ToLower();
            
            return ext == ".xls" || ext == ".xlsx";
        }

        public DataTable ProcessExcel(string path)
        {
            DataTable dt = new DataTable("Results");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool firstRow = true;
                    while (reader.Read()) // RECORRIENDO FILAS
                    {
                        if (firstRow)
                        {
                            for (int column = 0; column < reader.FieldCount - 1; column++)
                            {
                                dt.Columns.Add(reader.GetValue(column) != null ? reader.GetValue(column).ToString() : "");
                            }

                            firstRow = false;
                        }
                        else
                        {
                            dt.Rows.Add();

                            bool delete_row = true;
                            for (int column = 0; column < reader.FieldCount - 1; column++)
                            {
                                string value = reader.GetValue(column) != null ? reader.GetValue(column).ToString() : "";

                                if (delete_row && value != "")
                                    delete_row = false;

                                dt.Rows[dt.Rows.Count - 1][column] = value;
                            }

                            if (delete_row)
                                dt.Rows[dt.Rows.Count - 1].Delete();
                        }
                    }

                    dt.Columns.RemoveAt(0);
                }
            }

            return dt;
        }
    
        public void CreateExcel(Viaje viaje, string path, int type, string title)
        {
            using (SLDocument doc = new SLDocument())
            {
                DataTable dt = new DataTable();

                if (type == 1) // DESCARGA
                    dt = CreateDatatableList(viaje.ID);
                else if (type == 2) // NEPTUNO
                    dt = CreateDatatableNeptuno(viaje.ID);

                SLStyle font_bold = new SLStyle() { Font = new SLFont() { Bold = true } };
                SLPageSettings ps = new SLPageSettings();
                ps.PrintGridLines = false;
                doc.SetPageSettings(ps);

                doc.MergeWorksheetCells("B2", "C2");
                doc.MergeWorksheetCells("B3", "C3");

                doc.SetCellValue("B2", "INTERSHIPPING C.A.");
                doc.SetCellValue("B3", "RIF J-00116905-9");

                doc.SetCellStyle("B2", font_bold);
                doc.SetCellStyle("B3", font_bold);

                doc.MergeWorksheetCells("K2", "L2");
                doc.MergeWorksheetCells("K3", "L3");

                doc.SetCellValue("K2", "BUQUE: " + viaje.nom_buq);
                doc.SetCellValue("K3", "VIAJE: " + viaje.num_viaj);

                doc.SetCellStyle("K2", font_bold);
                doc.SetCellStyle("K3", font_bold);

                SLStyle s_title = doc.CreateStyle();
                s_title.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                s_title.Alignment.Vertical = VerticalAlignmentValues.Center;
                s_title.Font.FontSize = 18;
                s_title.Font.Bold = true;

                doc.MergeWorksheetCells("E2", "I3");
                doc.SetCellStyle("E2", s_title);
                doc.SetCellValue("E2", title);

                // ESTILOS ENCABEZADO
                SLStyle s_header = doc.CreateStyle();
                s_header.Border.TopBorder.BorderStyle = BorderStyleValues.Thick;
                s_header.Border.TopBorder.Color = System.Drawing.Color.Black;
                s_header.Border.BottomBorder.BorderStyle = BorderStyleValues.Thick;
                s_header.Border.BottomBorder.Color = System.Drawing.Color.Black;
                s_header.Border.RightBorder.BorderStyle = BorderStyleValues.Thick;
                s_header.Border.RightBorder.Color = System.Drawing.Color.Black;
                s_header.Border.LeftBorder.BorderStyle = BorderStyleValues.Thick;
                s_header.Border.LeftBorder.Color = System.Drawing.Color.Black;
                s_header.Alignment.Vertical = VerticalAlignmentValues.Center;
                s_header.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                s_header.Fill.SetPattern(PatternValues.Solid, System.Drawing.ColorTranslator.FromHtml("#C5D9F1"), System.Drawing.ColorTranslator.FromHtml("#000"));

                doc.SetRowHeight(5, 20);
                for (int i = 2; i <= dt.Columns.Count + 1; i++)
                {
                    doc.SetColumnWidth(i, 20);
                    doc.SetCellStyle(5, i, s_header);
                }

                // ESTILO GRILLA
                SLStyle s_grid = doc.CreateStyle();
                s_grid.Border.LeftBorder.BorderStyle = BorderStyleValues.Thick;
                s_grid.Border.LeftBorder.Color = System.Drawing.Color.Black;
                s_grid.Border.RightBorder.BorderStyle = BorderStyleValues.Thick;
                s_grid.Border.RightBorder.Color = System.Drawing.Color.Black;
                s_grid.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
                s_grid.Border.BottomBorder.Color = System.Drawing.Color.Black;
                s_grid.Alignment.Vertical = VerticalAlignmentValues.Center;
                s_grid.Alignment.Horizontal = HorizontalAlignmentValues.Center;

                for (int i = 6; i <= dt.Rows.Count + 5; i++)
                {
                    s_grid.Font.FontSize = 10;
                    for (int j = 2; j <= dt.Columns.Count + 1; j++)
                    {
                        if (j == dt.Columns.Count)
                        {
                            s_grid.Font.FontSize = 8;
                            s_grid.SetWrapText(true);
                        }

                        doc.SetCellStyle(i, j, s_grid);
                    }
                }

                string name_field = type == 1 ? "Peso" : "Peso Bruto (KGS)";

                int count20 = dt.AsEnumerable().Where(r => r.Field<int>("Tamano") == 20).ToList().Count;
                int count40 = dt.AsEnumerable().Where(r => r.Field<int>("Tamano") == 40).ToList().Count;
                int count_t = count20 + count40;
                decimal weight20 = dt.AsEnumerable().Where(r => r.Field<int>("Tamano") == 20).Select(r => r.Field<decimal>(name_field)).Sum();
                decimal weight40 = dt.AsEnumerable().Where(r => r.Field<int>("Tamano") == 40).Select(r => r.Field<decimal>(name_field)).Sum();
                decimal weigth_t = weight20 + weight40;

                doc.MergeWorksheetCells(dt.Rows.Count + 8, 2, dt.Rows.Count + 8, 5);
                doc.MergeWorksheetCells(dt.Rows.Count + 9, 2, dt.Rows.Count + 9, 5);
                doc.MergeWorksheetCells(dt.Rows.Count + 10, 2, dt.Rows.Count + 10, 5);
                doc.MergeWorksheetCells(dt.Rows.Count + 11, 2, dt.Rows.Count + 11, 5);

                doc.SetCellStyle(dt.Rows.Count + 8, 2, font_bold);
                doc.SetCellValue(dt.Rows.Count + 8, 2, "RECAPITULACION DE LA CARGA");
                doc.SetCellValue(dt.Rows.Count + 9, 2, string.Format("SON {0} X 20 FULL CON UN PESO TOTAL DE = {1:n} KGS", count20, weight20));
                doc.SetCellValue(dt.Rows.Count + 10, 2, string.Format("SON {0} X 40 FULL CON UN PESO TOTAL DE = {1:n} KGS", count40, weight40));
                doc.SetCellValue(dt.Rows.Count + 11, 2, string.Format("TOTAL {0} CONTENEDORES FULL CON UN PESO TOTAL DE = {1:n} KGS", count_t, weigth_t));

                doc.ImportDataTable(5, 2, dt, true);

                if (type == 1)
                {
                    doc.AutoFitColumn(dt.Columns.Count - 1);
                    doc.AutoFitColumn(dt.Columns.Count - 2);
                }
                else if (type == 2)
                {
                    doc.AutoFitColumn(4);
                    doc.AutoFitColumn(5);
                    doc.AutoFitColumn(11);
                    doc.AutoFitColumn(12);
                }

                doc.SaveAs(path);
            }
        }
        
        private DataTable CreateDatatableList(int ID)
        {
            DataTable dt = new DataTable();

            string connect = ConfigurationManager.ConnectionStrings["SLOConnectionString"].ConnectionString;
            string query = @"select
	            ROW_NUMBER() OVER(ORDER BY C.ID) AS Item, 
	            'CMA CGM' as Linea, 
	            C.num_cont as Contenedor, 
	            C.eq_inter_rec1 as 'Precinto 1', 
	            C.eq_inter_rec2 as 'Precinto 2', 
	            C.eq_inter_rec3 as 'Precinto 3', 
	            C.tamanio as Tamano, 
	            SUBSTRING(C.tip_cont_orig, 3, 2) as Tipo, 
	            B.condicion as Condicion, 
	            C.peso_bruto as Peso, 
	            C.num_paq as Bultos,
	            case B.num_naturaleza
		            when 22 then 'EXPORTACION'
		            when 23 then 'IMPORTACION'
	            end as Categoria,
	            B.pto_carga as POL,
	            B.pto_descarga as POD,
	            case C.temper
			        when 0 then ''
			        else CAST(C.temper as varchar(10)) + '°C'
		        end as Temperatura,
	            C.imo as IMO,
	            C.num_un as UN,
	            B.num_bl as BL,
	            B.nom_consign as Consignatario,
	            TM.nom_mercancia as 'Tipo Mercancia',
	            B.descripcion as Contenido,
                '' as Observaciones
            from Contenedor C
            inner join BL B on B.ID = C.id_bl
            inner join TipoMercancia TM on TM.ID = B.tipo_mercancia
            inner join Viaje V on V.ID = B.id_viaje
            where V.ID = " + ID;

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(comm))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private DataTable CreateDatatableNeptuno(int ID)
        {
            DataTable dt = new DataTable();

            string connect = ConfigurationManager.ConnectionStrings["SLOConnectionString"].ConnectionString;
            string query = @"select
	            ROW_NUMBER() OVER(ORDER BY C.ID) AS Item, 
	            B.num_bl as BL,
	            B.nom_export as Embarcador,
	            B.nom_consign as Consignatario,
	            V.cod_pto_sal as Origen,
	            B.pto_carga as POL,
	            B.pto_descarga as POD,
	            C.tamanio as Tamano, 
	            SUBSTRING(C.tip_cont_orig, 3, 2) as Tipo, 
	            C.num_cont as 'Siglas de Contenedor', 
	            C.eq_inter_rec1 + ' - ' + C.eq_inter_rec2 + ' - ' + C.eq_inter_rec3 as 'Precinto de Confrontacion',
	            B.descripcion as Mercancia,
	            B.tipo_paq as Empaque,
	            C.num_paq as Cantidad,
	            EC.nom_estado as 'Lleno / Vacio',
	            B.condicion as 'Condicion Venta', 
	            C.peso_bruto as 'Peso Bruto (KGS)',
	            C.peso_neto as 'Peso Neto (KGS)',
	            0 as M3,
	            0 as TMIN,
	            0 as TMAX,
	            'N' as HAZ,
	            C.imo as IMO,
	            C.num_un as UN,
	            case B.num_naturaleza
		            when 22 then 'EXPORTACION (22)'
		            when 23 then 'IMPORTACION (23)'
	            end as 'Uso SIDUNEA',
                '' as Observaciones
            from Contenedor C
            inner join EstadoContenedor EC on EC.ID = C.estado
            inner join BL B on B.ID = C.id_bl
            inner join TipoMercancia TM on TM.ID = B.tipo_mercancia
            inner join Viaje V on V.ID = B.id_viaje
            where V.ID = " + ID;

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(comm))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}