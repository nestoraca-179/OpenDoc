<%@ Page Title="Editar Viaje" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditarViaje.aspx.cs" Inherits="SLO.AreaViaje.EditarViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    body > section.container-fluid {
        background: #b7b7b9;
    }
</style>
<script>
    function openModalDelete() {
        setTimeout(function () { $("#modal-delete").modal("show"); }, 1);
    }
</script>
<form id="Form1" runat="server" class="container">
    <asp:Panel ID="PN_Success" runat="server" Width="100%" CssClass="mt-2" Visible="false">
        <div class="alert alert-success m-0">
            <dx:ASPxLabel ID="LBL_Success" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN_Error" runat="server" Width="100%" CssClass="mt-2" Visible="false">
        <div class="alert alert-danger m-0">
            <dx:ASPxLabel ID="LBL_Error" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN_ContainerForm" runat="server" CssClass="container-form">
        <div class="form-header">
            <div class="row mt-4">
                <div class="col d-flex">
                    <asp:LinkButton ID="BTN_Volver" runat="server" CssClass="btn btn-primary" OnClick="BTN_Volver_Click">
                        <i class="fas fa-arrow-left"></i> Regresar
                    </asp:LinkButton>
                    <asp:LinkButton ID="BTN_Guardar" runat="server" CssClass="btn btn-success mx-2" OnClick="BTN_Guardar_Click">
                        <i class="fas fa-floppy-disk"></i> Guardar
                    </asp:LinkButton>
                </div>
                <div class="col">
                    <dx:ASPxLabel ID="LBL_IDViaje" runat="server" Width="100%" CssClass="title-screen"></dx:ASPxLabel>
                </div>
                <div class="col"></div>
            </div>
            <hr />
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Código Aduana</label>
                        <dx:ASPxComboBox ID="DDL_CodAduana" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_adua" ValueType="System.String" DataSourceID="DS_Aduana">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_adua" Caption="Nombre"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Aduana" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_adua] FROM [Aduana] ORDER BY [ID]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Número Viaje</label>
                        <dx:ASPxTextBox ID="TB_NumViaje" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Fecha Salida</label>
                        <dx:ASPxDateEdit ID="DE_FecSalida" runat="server" Theme="Material" EditFormat="DateTime" Width="100%">
                            <TimeSectionProperties Visible="True"></TimeSectionProperties>
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Fecha Arribo</label>
                        <dx:ASPxDateEdit ID="DE_FecArribo" runat="server" Theme="Material" EditFormat="DateTime" Width="100%">
                            <TimeSectionProperties Visible="True"></TimeSectionProperties>
                        </dx:ASPxDateEdit>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Location Code</label>
                        <dx:ASPxTextBox ID="TB_LocCode" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Uso</label>
                        <dx:ASPxComboBox ID="DDL_Uso" runat="server" Theme="Material" Width="100%" ValueType="System.Int32">
                            <Items>
                                <dx:ListEditItem Text="IMPORTACI&#211;N" Value="1"></dx:ListEditItem>
                                <dx:ListEditItem Text="EXPORTACI&#211;N" Value="2"></dx:ListEditItem>
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Total BLS</label>
                        <dx:ASPxTextBox ID="TB_TotBls" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Total Paquetes</label>
                        <dx:ASPxTextBox ID="TB_TotPaq" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Total Contenedores</label>
                        <dx:ASPxTextBox ID="TB_TotConts" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Total Gross Mass</label>
                        <dx:ASPxTextBox ID="TB_TotGM" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Código Carrier</label>
                        <dx:ASPxTextBox ID="TB_CodCarr" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Nombre Carrier</label>
                        <dx:ASPxTextBox ID="TB_NomCarr" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Dirección Carrier</label>
                        <dx:ASPxTextBox ID="TB_DirCarr" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Código Modo Transporte</label>
                        <dx:ASPxComboBox ID="DDL_CodModTrans" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_mod_trans" ValueType="System.Int32" DataSourceID="DS_ModoTrans">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_mod_trans" Caption="Modo Transporte"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_ModoTrans" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_mod_trans] FROM [ModoTransporte]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>ID Transportador</label>
                        <dx:ASPxTextBox ID="TB_IDTrans" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Código Nacionalidad Transportador</label>
                        <dx:ASPxComboBox ID="DDL_CodNacTrans" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_pais" ValueType="System.String" DataSourceID="DS_Pais">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_pais" Caption="Pa&#237;s"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Pais" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_pais] FROM [Pais] ORDER BY [ID]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Código Puerto Salida</label>
                        <dx:ASPxTextBox ID="TB_PtoSalida" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Código Puerto Destino</label>
                        <dx:ASPxTextBox ID="TB_PtoDestino" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Código de Línea</label>
                        <dx:ASPxTextBox ID="TB_CodLinea" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Almacén Destino</label>
                        <dx:ASPxTextBox ID="TB_AlmDest" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Código de Buque</label>
                        <dx:ASPxTextBox ID="TB_CodBuq" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Nombre de Buque</label>
                        <dx:ASPxTextBox ID="TB_NomBuq" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="form-body">
            <div class="row mb-3">
                <div class="col d-flex">
                    <asp:LinkButton ID="BTN_AgregarBL" runat="server" Text="Nuevo" CssClass="btn btn-info" OnClick="BTN_AgregarBL_Click">
                        <i class="fas fa-plus"></i> Agregar Nuevo BL
                    </asp:LinkButton>
                </div>
                <div class="col">
                    <h3 class="text-center">Lista de BLS</h3>
                </div>
                <div class="col"></div>
            </div>
            <div class="form-grid">
                <dx:ASPxGridView ID="GV_GridResultsB" runat="server" Width="100%" Theme="Material" AutoGenerateColumns="False" KeyFieldName="ID" DataSourceID="DS_BL" 
                    OnRowCommand="GV_GridResultsB_RowCommand">
                    <SettingsPager PageSize="5"></SettingsPager>
                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="num_bl" VisibleIndex="1" Caption="Num. BL"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="pto_carga" VisibleIndex="2" Caption="Pto. Carga"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="pto_descarga" VisibleIndex="3" Caption="Pto. Descarga "></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="num_conts" VisibleIndex="4" Caption="Num. Cont."></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="nom_export" VisibleIndex="5" Caption="Exportador"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="nom_notify" VisibleIndex="6" Caption="Notify"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="nom_consign" VisibleIndex="7" Caption="Consignee"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="8" Caption="Descripción"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="9" Caption="Editar">
                            <DataItemTemplate>
                                <asp:LinkButton ID="BTN_EditarBL" runat="server" CssClass="btn btn-primary" CommandName="Editar">
                                    <i class="fas fa-edit"></i> Editar
                                </asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Width="60px" VisibleIndex="10" Caption="Eliminar" >
                            <DataItemTemplate>
                                <asp:LinkButton ID="BTN_ConfirmarEliminarBL" runat="server" CssClass="btn btn-danger" CommandName="Eliminar">
                                    <i class="fas fa-times"></i> Eliminar
                                </asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource runat="server" ID="DS_BL" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [num_bl], [pto_carga], [pto_descarga], [num_conts], [nom_export], [nom_notify], [nom_consign], [descripcion] FROM [BL] WHERE ([id_viaje] = @id_viaje)">
                    <SelectParameters>
                        <asp:QueryStringParameter QueryStringField="ID" Name="id_viaje" Type="Int32"></asp:QueryStringParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </asp:Panel>
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
                    <dx:ASPxButton ID="BTN_EliminarBL" runat="server" Text="Sí" CssClass="btn btn-success" OnClick="BTN_EliminarBL_Click"></dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>
</form>
<script>
    $(document).ready(function () {
        $("#menu").toggleClass("active");
        $('#menu li a p').animate({ width: 'toggle' });
    });

    function onlyNumbers(_, e) {
        var event = e.htmlEvent || window.event;
        var key = event.keyCode || event.which;
        var regex = /[0-9.,]/;

        key = String.fromCharCode(key);
        
        if (!regex.test(key)) {
            event.returnValue = false;

            if (event.preventDefault)
                event.preventDefault();
        }
    }
</script>
</asp:Content>