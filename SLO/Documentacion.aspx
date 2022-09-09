<%@ Page Title="Documentación" Language="C#" MasterPageFile="Main.Master" AutoEventWireup="true" CodeBehind="Documentacion.aspx.cs" Inherits="SLO.Documentacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    section.container-fluid > .container {
        max-width: 1520px;
    }
    span.m-0 {
        font-size: 14px;
    }
    i {
        margin-right: 5px;
    }
</style>
<script>
    function openModal() {
        $("#modal-waiting").modal("show");
    }

    function openModalDelete() {
        setTimeout(function () { $("#modal-delete").modal("show"); }, 1);
    }

    function openModalWarning() {
        setTimeout(function () { $("#modal-warning").modal("show"); }, 1);
    }
</script>
<form id="Form1" runat="server">
    <div class="container-form">
        <asp:Panel ID="PN_Success" runat="server" Width="100%" CssClass="mb-2" Visible="false">
            <div class="alert alert-success m-0">
                <dx:ASPxLabel ID="LBL_Success" runat="server" CssClass="m-0"></dx:ASPxLabel>
            </div>
        </asp:Panel>
        <asp:Panel ID="PN_Error" runat="server" Width="100%" CssClass="mb-2" Visible="false">
            <div class="alert alert-danger m-0">
                <dx:ASPxLabel ID="LBL_Error" runat="server" CssClass="m-0"></dx:ASPxLabel>
            </div>
        </asp:Panel>
        <div class="form-header">
            <asp:FileUpload ID="FU_UploadFile" runat="server" />
            <asp:LinkButton ID="BTN_UploadFileExcel" runat="server" CssClass="btn btn-success disabled" OnClick="BTN_UploadFileExcel_Click" OnClientClick="openModal()">
            <i class="fas fa-file-excel"></i> Subir Archivo Excel
            </asp:LinkButton>
        </div>
        <hr />
        <div class="form-body">
            <h3 class="text-center mb-3">Lista de Viajes</h3>
            <div class="form-grid">
                <dx:ASPxGridView ID="GV_GridResultsV" runat="server" Width="100%" Theme="Material" AutoGenerateColumns="False" DataSourceID="DS_Viaje" KeyFieldName="ID"
                    OnRowCommand="GV_GridResultsV_RowCommand">
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0" Visible="false">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="file_path" VisibleIndex="1" Caption="Archivo"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="num_viaj" VisibleIndex="2" Caption="Num. Viaje"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="fec_sal" VisibleIndex="3" Caption="Fec. Salida"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="fec_arr" VisibleIndex="4" Caption="Fec. Arribo"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="total_bls" VisibleIndex="5" Caption="Total BLS"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="total_paq" VisibleIndex="6" Caption="Total Paq."></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="total_cont" VisibleIndex="7" Caption="Total Cont."></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="id_trans" VisibleIndex="8" Caption="Transportador" Visible="false"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="cod_pto_sal" VisibleIndex="9" Caption="Pto. Salida"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="cod_pto_des" VisibleIndex="10" Caption="Pto. Descarga"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="date_uploaded" VisibleIndex="11" Caption="Fec. Subido" Visible="false"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="12" Caption="XML">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="BTN_GenerarXML" runat="server" CssClass="btn btn-success" Text="Generar" CommandName="Generar"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="13" Caption="Editar">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="BTN_EditarViaje" runat="server" CssClass="btn btn-primary" Text="Editar" CommandName="Editar"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="14" Caption="Eliminar" >
                            <DataItemTemplate>
                                <dx:ASPxButton ID="BTN_ConfirmarEliminarViaje" runat="server" CssClass="btn btn-danger" Text="Eliminar" CommandName="Eliminar"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="DS_Viaje" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [file_path], [num_viaj], [fec_sal], [fec_arr], [total_bls], [total_paq], [total_cont], [id_trans], [cod_pto_sal], [cod_pto_des], [date_uploaded] FROM [Viaje] ORDER BY [date_uploaded] DESC"></asp:SqlDataSource>
            </div>
        </div>
    </div>
    <%-- MODAL WAITING --%>
    <div class="modal fade" id="modal-waiting" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <i class="fas fa-spinner fa-spin"></i>
                    <h2>Procesando archivo...</h2>
                </div>
            </div>
        </div>
    </div>
    <%-- MODAL DELETE --%>
    <div class="modal fade" id="modal-delete" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <i class="fas fa-warning"></i>
                    <h2>¿Desea eliminar el registro?</h2>
                </div>
                <div class="modal-footer buttons">
                    <button class="btn btn-danger" data-dismiss="modal">No</button>
                    <dx:ASPxButton ID="BTN_EliminarViaje" runat="server" Text="Sí" CssClass="btn btn-success" OnClick="BTN_EliminarViaje_Click"></dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>
    <%-- MODAL WARNING --%>
    <div class="modal fade" id="modal-warning" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <i class="fas fa-warning"></i>
                    <h4>Este archivo ya fue cargado ¿Desea cargarlo de todos modos?</h4>
                </div>
                <div class="modal-footer buttons">
                    <button class="btn btn-danger" data-dismiss="modal">No</button>
                    <dx:ASPxButton ID="BTN_CargarViaje" runat="server" Text="Sí" CssClass="btn btn-success" OnClick="BTN_CargarViaje_Click"></dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $("#menu").toggleClass("active");
            $('#menu li a p').animate({ width: 'toggle' });
        });

        $("#MainContent_FU_UploadFile").change(function () {
            $("#MainContent_BTN_UploadFileExcel").removeClass("disabled");
        });
    </script>
</form>
</asp:Content>