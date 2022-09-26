using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using DocumentFormat.OpenXml.Spreadsheet;
using SLO.Controllers;
using SLO.Models;
using SpreadsheetLight;

namespace SLO
{
    public partial class Documentacion : System.Web.UI.Page
    {
        private static int IDSelected;
        private static string Folder;
        private static string FullPath;
        private static string FileName;
        private static string UserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            PN_Success.Visible = false;
            PN_Error.Visible = false;

            if (Request.QueryString["new"] != null)
            {
                PN_Success.Visible = true;
                LBL_Success.Text = "Viaje agregado con éxito";
            }

            if (Request.QueryString["xml"] != null)
            {
                string path = Environment.GetEnvironmentVariable("USERPROFILE");
                path = Path.Combine(path, "Downloads");

                PN_Success.Visible = true;
                LBL_Success.Text = string.Format("Archivo {0} generado con éxito en {1}", FileName, path);
            }
        }

        protected void BTN_UploadFilePDF_Click(object sender, EventArgs e)
        {
            PDFController pdf_controller = new PDFController();

            string error = "";
            bool valid_files = true, is_error = false;
            int succedeed = 0, failed = 0;

            if (FU_UploadFile.HasFiles)
            {
                string folder = Server.MapPath("~") + "\\Documents\\";

                // VERIFICANDO ARCHIVOS PDF
                foreach (HttpPostedFile file in FU_UploadFile.PostedFiles)
                {
                    if (pdf_controller.IsPDF(file))
                    {
                        string filename = file.FileName;
                        string path = folder + filename;

                        if (File.Exists(path))
                            File.Delete(path);

                        file.SaveAs(path);

                        if (!pdf_controller.IsValidBL(path))
                        {
                            valid_files = false;
                            error = "Uno de los archivos PDF subidos no contiene información de BL";
                            break;
                        }
                    }
                    else
                    {
                        valid_files = false;
                        error = "Todos los archivos deben ser .pdf";
                        break;
                    }
                }

                // ELIMINAR ARCHIVOS TEMPORALMENTE SUBIDOS
                // DeleteAllFiles(folder);

                // VALIDA SI TODOS LOS ARCHIVOS SON PDF Y BLS
                if (valid_files)
                {
                    try
                    {
                        ProcessResult result;
                        foreach (HttpPostedFile file in FU_UploadFile.PostedFiles)
                        {
                            string filename = file.FileName;
                            string path = folder + filename;

                            if (File.Exists(path))
                                File.Delete(path);

                            file.SaveAs(path);

                            // result = pdf_controller.ProcessPDF(path);
                            // succedeed += result.Succeded;
                            // failed += result.Failed;
                        }

                        // LBL_Result.Visible = true;
                        // LBL_Result.Text = string.Format("Archivos cargados. TOTAL ARCHIVOS SUBIDOS: {0}, BLS EXITOSOS: {1}, BLS FALLIDOS: {2}",  FU_UploadFiles.PostedFiles.Count, succedeed, failed);
                    }
                    catch (Exception ex)
                    {
                        // DeleteAllFiles(folder);
                        IncidentController.CreateIncident("ERROR LEYENDO DOCUMENTOS PDF", ex);

                        is_error = true;
                        error = "Ha ocurrido un error. Ver tabla de Incidentes";
                    }
                }
                else
                    is_error = true;
            }
            else
            {
                is_error = true;
                error = "No has subido ningún archivo PDF";
            }

            LBL_Error.Visible = is_error;
            LBL_Error.Text = error;
        }

        protected void BTN_UploadFileExcel_Click(object sender, EventArgs e)
        {
            ExcelController ec = new ExcelController();

            int result;
            bool is_error = false;
            string error = "", folder = Server.MapPath("~") + "Documents\\", user = ((Usuario)Session["USER"]).username;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (FU_UploadFile.HasFile)
            {
                HttpPostedFile file = FU_UploadFile.PostedFile;

                // VALIDA SI EL ARCHIVO ES EXCEL
                if (ec.IsExcel(file))
                {
                    try
                    {
                        string filename = file.FileName;
                        string path = folder + filename;

                        if (!File.Exists(path))
                        {
                            file.SaveAs(path);

                            var results = ec.ProcessExcel(path).AsEnumerable();
                            result = ViajeController.Add(results, filename, user);

                            if (result == 1)
                            {
                                GV_GridResultsV.DataBind();
                            }
                            else
                            {
                                is_error = true;
                                error = "Error procesando el Excel. Ver tabla de Incidentes";
                                File.Delete(path);
                            }
                        }
                        else
                        {
                            Folder = folder;
                            FullPath = path;
                            FileName = filename;
                            UserName = user;

                            Directory.CreateDirectory(folder + "Temp\\");
                            file.SaveAs(folder + "Temp\\" + filename);

                            is_error = true;
                            error = "El archivo ya está cargado";
                            ScriptManager.RegisterStartupScript(this, GetType(), "modal", "openModalWarning()", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // DeleteAllFiles(folder);
                        IncidentController.CreateIncident("ERROR LEYENDO DOCUMENTOS EXCEL", ex);

                        is_error = true;
                        error = "Ha ocurrido un error. Ver tabla de Incidentes";
                    }
                }
                else
                {
                    is_error = true;
                    error = "El archivo debe ser .xls o .xlsx";
                }
            }
            else
            {
                is_error = true;
                error = "No has subido ningún archivo Excel";
            }

            if (!is_error)
            {
                PN_Success.Visible = true;
                LBL_Success.Text = "Archivo Excel subido con éxito";
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = error;
            }
        }

        protected void GV_GridResultsV_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            IDSelected = int.Parse(e.KeyValue.ToString());

            if (e.CommandArgs.CommandName == "Editar")
            {
                Response.Redirect("~/AreaViaje/EditarViaje.aspx?ID=" + IDSelected, true);
            }
            else if (e.CommandArgs.CommandName == "Generar")
            {
                string num_viaje = (GV_GridResultsV.GetRow(e.VisibleIndex) as DataRowView).Row.ItemArray[2].ToString();
                LBL_XML.Text = string.Format("¿Desea generar el archivo XML del Viaje N° {0}?", num_viaje);

                ScriptManager.RegisterStartupScript(this, GetType(), "modal", "openModalXML()", true);
            }
            else if (e.CommandArgs.CommandName == "Eliminar")
            {
                string num_viaje = (GV_GridResultsV.GetRow(e.VisibleIndex) as DataRowView).Row.ItemArray[2].ToString();
                LBL_Delete.Text = string.Format("¿Desea eliminar el Viaje N° {0}?", num_viaje);

                ScriptManager.RegisterStartupScript(this, GetType(), "modal", "openModalDelete()", true);
            }
            else if (e.CommandArgs.CommandName == "Listar")
            {
                Viaje viaje = ViajeController.GetByID(IDSelected);

                string filename = viaje.num_viaj + ".xlsx";
                string path = Server.MapPath("~") + "Documents\\LISTADO_DESCARGA_" + filename;

                try
                {
                    new ExcelController().CreateExcel(viaje, path);

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.TransmitFile(path);
                    Response.End();
                }
                catch (Exception ex)
                {
                    IncidentController.CreateIncident(string.Format("ERROR CREANDO LISTADO DE VIAJE N° {0}", viaje.num_viaj), ex);

                    PN_Error.Visible = true;
                    LBL_Error.Text = "Error generando el listado de descarga en Excel";
                }
            }
        }

        protected void BTN_EliminarViaje_Click(object sender, EventArgs e)
        {
            string file = ViajeController.GetByID(IDSelected).file_path;
            int result = ViajeController.Delete(IDSelected);

            if (result == 1)
            {
                if (file != "SIN ARCHIVO")
                    File.Delete(Server.MapPath("~") + "Documents\\" + file);

                PN_Success.Visible = true;
                LBL_Success.Text = "Viaje eliminado con éxito";
                GV_GridResultsV.DataBind();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
            }
        }

        protected void BTN_CargarViaje_Click(object sender, EventArgs e)
        {
            ExcelController ec = new ExcelController();

            try
            {
                Viaje d_viaje = ViajeController.GetByFile(FileName);
                int result = ViajeController.Delete(d_viaje.ID);

                if (result == 1)
                {
                    File.Copy(Folder + "Temp\\" + FileName, FullPath, true);

                    var results = ec.ProcessExcel(FullPath).AsEnumerable();
                    result = ViajeController.Add(results, FileName, UserName);

                    if (result == 1)
                    {
                        GV_GridResultsV.DataBind();
                        PN_Success.Visible = true;
                        LBL_Success.Text = "Archivo Excel subido con éxito";
                    }
                    else
                    {
                        PN_Error.Visible = true;
                        LBL_Error.Text = "Error procesando el Excel. Ver tabla de Incidentes";
                    }
                }
                else
                {
                    PN_Error.Visible = true;
                    LBL_Error.Text = "Ha ocurrido un error al eliminar el Viaje. Ver tabla de Incidentes";
                }
            }
            catch (Exception ex)
            {
                IncidentController.CreateIncident("ERROR ELIMINADO EL VIAJE", ex);

                PN_Error.Visible = true;
                LBL_Error.Text = "Ha ocurrido un error. Ver tabla de Incidentes";
            }
            finally
            {
                Directory.Delete(Folder + "Temp\\", true);
            }
        }

        protected void BTN_AgregarViaje_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AreaViaje/AgregarViaje.aspx", true);
        }

        protected void BTN_GenerarXML_Click(object sender, EventArgs e)
        {
            string folder = Server.MapPath("~") + "Documents\\";
            string filename = XMLController.GenerarXMLVEPBL(folder, IDSelected);
            // string filename = XMLController.GenerarXMLVELAG(folder, IDSelected); // LA GUAIRA

            if (filename != "")
            {
                FileName = filename;
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(folder + filename);
                Response.End();
            }
            else
            {
                PN_Error.Visible = true;
                LBL_Error.Text = "Error generando el archivo XML";
            }
        }
    }
}