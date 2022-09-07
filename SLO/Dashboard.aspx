﻿<%@ Page Title="Dashboard" Language="C#" MasterPageFile="Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SLO.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    section.container-fluid > .container {
        max-width: 1520px;
    }
    span.m-0 {
        font-size: 14px;
    }
    .btn.btn-success {
        height: auto;
    }
    i {
        margin-right: 5px;
    }
</style>
<form id="Form1" runat="server">
    <div class="container-form">
        <asp:Panel ID="PN_Success" runat="server" Width="100%" Visible="false">
            <div class="alert alert-success m-0">
                <dx:ASPxLabel ID="LBL_Success" runat="server" CssClass="m-0"></dx:ASPxLabel>
            </div>
        </asp:Panel>
        <asp:Panel ID="PN_Error" runat="server" Width="100%" Visible="false">
            <div class="alert alert-danger m-0">
                <dx:ASPxLabel ID="LBL_Error" runat="server" CssClass="m-0"></dx:ASPxLabel>
            </div>
        </asp:Panel>
        <div class="form-header">
            <asp:FileUpload ID="FU_UploadFile" runat="server" />
            <asp:LinkButton ID="BTN_UploadFileExcel" runat="server" CssClass="btn btn-success" OnClick="BTN_UploadFileExcel_Click">
                <i class="fas fa-file-excel"></i> Subir Archivo Excel
            </asp:LinkButton>
        </div>
        <div class="form-body">
            <div class="form-grid">
                <dx:ASPxGridView ID="GV_GridResultsV" runat="server" Width="100%" Theme="Material" AutoGenerateColumns="False" DataSourceID="DS_Viaje" KeyFieldName="ID" 
                    OnRowCommand="GV_GridResultsV_RowCommand">
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="file_path" VisibleIndex="1" Caption="Archivo"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="num_viaj" VisibleIndex="2" Caption="Num. Viaje"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="fec_sal" VisibleIndex="3" Caption="Fec. Salida"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="fec_arr" VisibleIndex="4" Caption="Fec. Arribo"></dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="total_bls" VisibleIndex="5" Caption="Total BLS"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="total_paq" VisibleIndex="6" Caption="Total Paq."></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="total_cont" VisibleIndex="7" Caption="Total Cont."></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="id_trans" VisibleIndex="8" Caption="Transportador"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="cod_pto_sal" VisibleIndex="9" Caption="Pto. Salida"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="cod_pto_des" VisibleIndex="10" Caption="Pto. Descarga"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="date_uploaded" VisibleIndex="11" Caption="Fec. Subido"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="12" Caption="Editar">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="BTN_EditarViaje" runat="server" CssClass="btn btn-primary" Text="Editar" CommandName="Editar"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="13" Caption="XML">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="BTN_GenerarXML" runat="server" CssClass="btn btn-info" Text="Generar XML" CommandName="Generar"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="DS_Viaje" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [file_path], [num_viaj], [fec_sal], [fec_arr], [total_bls], [total_paq], [total_cont], [id_trans], [cod_pto_sal], [cod_pto_des], [date_uploaded] FROM [Viaje] ORDER BY [date_uploaded] DESC"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</form>
</asp:Content>