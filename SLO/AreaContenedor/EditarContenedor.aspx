<%@ Page Title="Editar Contenedor" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditarContenedor.aspx.cs" Inherits="SLO.AreaContenedor.EditarContenedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form id="Form1" runat="server">
    <asp:Panel ID="PN_Success" runat="server" Width="100%" Visible="false">
        <div class="alert alert-success m-0">
            <p class="m-0">El contenedor ha sido modificado con éxito</p>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN_Error" runat="server" Width="100%" Visible="false">
        <div class="alert alert-danger m-0">
            <dx:ASPxLabel ID="LBL_Error" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN_ContainerForm" runat="server" CssClass="container-form">
        <div class="form-header">
            <div class="row">
                <div class="col d-flex justify-content-center">
                    <asp:LinkButton ID="BTN_Volver" runat="server" CssClass="btn btn-primary" OnClick="BTN_Volver_Click">
                        <i class="fas fa-arrow-left"></i> Regresar
                    </asp:LinkButton>
                </div>
                <div class="col col-6">
                    <dx:ASPxLabel ID="LBL_IDContenedor" runat="server" Width="100%" CssClass="title-screen mt-3"></dx:ASPxLabel>
                </div>
                <div class="col d-flex justify-content-center">
                    <asp:LinkButton ID="BTN_Guardar" runat="server" CssClass="btn btn-success" OnClick="BTN_Guardar_Click">
                        <i class="fas fa-floppy-disk"></i> Guardar
                    </asp:LinkButton>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Número de Paquetes</label>
                        <dx:ASPxTextBox ID="TB_NumPaq" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Tipo de Contenedor</label>
                        <dx:ASPxComboBox ID="DDL_TipoCont" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_tipo" DataSourceID="DS_TipoContenedor">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Width="40px" Caption="C&#243;digo"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_tipo" Caption="Descripci&#243;n"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_TipoContenedor" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_tipo] FROM [TipoContenedor]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Estado Vacío / Lleno</label>
                        <dx:ASPxComboBox ID="DDL_Estado" runat="server" Theme="Material" Width="100%" DataSourceID="DS_Estado" ValueField="ID" TextField="nom_estado" ValueType="System.Int32"></dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Estado" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_estado] FROM [EstadoContenedor]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Precinto 1</label>
                        <dx:ASPxTextBox ID="TB_Prec1" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Precinto 2</label>
                        <dx:ASPxTextBox ID="TB_Prec2" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Precinto 3</label>
                        <dx:ASPxTextBox ID="TB_Prec3" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Sealing Party</label>
                        <dx:ASPxComboBox ID="DDL_SealPart" runat="server" Theme="Material" Width="100%">
                            <Items>
                                <dx:ListEditItem Text="CR - CARGADOR" Value="CR"></dx:ListEditItem>
                                <dx:ListEditItem Text="CU - ADUANA" Value="CU"></dx:ListEditItem>
                                <dx:ListEditItem Text="TO - OPERADOR PORTUARIO" Value="TO"></dx:ListEditItem>
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Peso Neto</label>
                        <dx:ASPxTextBox ID="TB_PesoNeto" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Peso Bruto</label>
                        <dx:ASPxTextBox ID="TB_PesoBruto" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Tamaño</label>
                        <dx:ASPxTextBox ID="TB_Tamano" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Temperatura</label>
                        <dx:ASPxTextBox ID="TB_Temp" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>IMO</label>
                        <dx:ASPxTextBox ID="TB_IMO" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>UN Number</label>
                        <dx:ASPxTextBox ID="TB_UNNum" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Ventilación</label>
                        <dx:ASPxTextBox ID="TB_Ventila" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Descripción Mercancía</label>
                        <dx:ASPxTextBox ID="TB_DescripMer" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="form-body">
            <div class="form-grid"></div>
        </div>
    </asp:Panel>
</form>
<script>
    function onlyNumbers(_, e) {
        var event = e.htmlEvent || window.event;
        var key = event.keyCode || event.which;
        var regex = /[0-9]/;

        key = String.fromCharCode(key);
        
        if (!regex.test(key)) {
            event.returnValue = false;

            if (event.preventDefault)
                event.preventDefault();
        }
    }
</script>
</asp:Content>