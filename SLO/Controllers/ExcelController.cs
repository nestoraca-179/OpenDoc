using System.Web;
using System.IO;
using System.Data;
using System.Text;
using ExcelDataReader;

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
    }
}