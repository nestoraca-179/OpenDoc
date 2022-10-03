<%@ Page Title="Agregar BL" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AgregarBL.aspx.cs" Inherits="SLO.AreaBL.AgregarBL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    body > section.container-fluid {
        background: #b7b7b9;
    }
</style>
<form id="Form1" runat="server" class="container">
    <asp:Panel ID="PN_Error" runat="server" Width="100%" Visible="false" CssClass="mt-2">
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
                    <dx:ASPxButton ID="BTN_Guardar" runat="server" CssClass="btn btn-success mx-2" Text="Guardar" ValidationGroup="BL" OnClick="BTN_Guardar_Click" />
                </div>
                <div class="col">
                    <dx:ASPxLabel ID="LBL_IDBL" runat="server" Width="100%" CssClass="title-screen"></dx:ASPxLabel>
                </div>
                <div class="col d-flex justify-content-center"></div>
            </div>
            <hr />
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>NÚMERO BL *</label>
                        <dx:ASPxTextBox ID="TB_NumBL" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Naturaleza *</label>
                        <dx:ASPxTextBox ID="TB_Naturaleza" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Tipo *</label>
                        <dx:ASPxComboBox ID="DDL_Tipo" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_tip_bl" DataSourceID="DS_TipoBL">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_tip_bl" Caption="Descripci&#243;n"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_TipoBL" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_tip_bl] FROM [TipoBL]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Puerto Carga *</label>
                        <dx:ASPxComboBox ID="DDL_PtoCarga" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_pto" ValueType="System.String" DataSourceID="DS_Puerto">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Width="40px" Caption="C&#243;digo"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_pto" Caption="Puerto"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Puerto" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_pto] FROM [Puerto]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Puerto Descarga *</label>
                        <dx:ASPxComboBox ID="DDL_PtoDescarga" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_pto" ValueType="System.String" DataSourceID="DS_Puerto">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Width="40px" Caption="C&#243;digo"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_pto" Caption="Puerto"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Destino</label>
                        <dx:ASPxTextBox ID="TB_Destino" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Booking</label>
                        <dx:ASPxTextBox ID="TB_Booking" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Condición *</label>
                        <dx:ASPxTextBox ID="TB_Cond" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Tipo Mercancía</label>
                        <dx:ASPxComboBox ID="DDL_TipMercancia" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_mercancia" ValueType="System.Int32" DataSourceID="DS_TipoMercancia">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_mercancia" Caption="Descripci&#243;n"></dx:ListBoxColumn>
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_TipoMercancia" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_mercancia] FROM [TipoMercancia]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Nombre Consignee *</label>
                        <dx:ASPxTextBox ID="TB_NomConsignee" runat="server" Theme="Material" Width="100%" MaxLength="35">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col col-9">
                    <div class="controls">
                        <label>Dirección Consignee *</label>
                        <dx:ASPxTextBox ID="TB_DirConsignee" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Nombre Notify *</label>
                        <dx:ASPxTextBox ID="TB_NomNotify" runat="server" Theme="Material" Width="100%" MaxLength="35">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col col-9">
                    <div class="controls">
                        <label>Dirección Notify *</label>
                        <dx:ASPxTextBox ID="TB_DirNotify" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Nombre Exportador *</label>
                        <dx:ASPxTextBox ID="TB_NomExport" runat="server" Theme="Material" Width="100%" MaxLength="35">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col col-9">
                    <div class="controls">
                        <label>Dirección Exportador *</label>
                        <dx:ASPxTextBox ID="TB_DirExport" runat="server" Theme="Material" Width="100%" MaxLength="70">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Gross Mass *</label>
                        <dx:ASPxTextBox ID="TB_GrossMass" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Shipping Marks *</label>
                        <dx:ASPxTextBox ID="TB_ShipMarks" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cantidad de contenedores *</label>
                        <dx:ASPxTextBox ID="TB_CantCont" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Volumen *</label>
                        <dx:ASPxTextBox ID="TB_Volumen" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col col-6">
                    <div class="controls">
                        <label>Descripción *</label>
                        <dx:ASPxTextBox ID="TB_Descrip" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Tipo de Paquetes *</label>
                        <dx:ASPxComboBox ID="DDL_TipPaq" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_tip_paq" DataSourceID="DS_TipoPaquete">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Width="40px" Caption="C&#243;digo"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_tip_paq" Caption="Descripci&#243;n"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_TipoPaquete" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_tip_paq] FROM [TipoPaquete]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cantidad de Paquetes *</label>
                        <dx:ASPxTextBox ID="TB_CantPaq" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="BL" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Precinto por BL</label>
                        <dx:ASPxTextBox ID="TB_PrecBL" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col col-9">
                    <div class="controls">
                        <label>Observaciones</label>
                        <dx:ASPxTextBox ID="TB_Observa" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label></label>
                        <dx:ASPxCheckBox ID="CK_Gobierno" runat="server" Theme="Material" Width="100%" Text="Gobierno"></dx:ASPxCheckBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Sobre Dimensiones</label>
                        <dx:ASPxTextBox ID="TB_SobreDim" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Fletes</label>
                        <dx:ASPxTextBox ID="TB_Fletes" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Moneda Fletes</label>
                        <dx:ASPxTextBox ID="TB_MonFletes" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <hr />
    </asp:Panel>
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