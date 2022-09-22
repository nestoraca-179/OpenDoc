<%@ Page Title="Agregar Viaje" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AgregarViaje.aspx.cs" Inherits="SLO.AreaViaje.AgregarViaje" %>

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
                    <dx:ASPxButton ID="BTN_Guardar" runat="server" CssClass="btn btn-success mx-2" Text="Guardar" ValidationGroup="Viaje" OnClick="BTN_Guardar_Click" />
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
                        <label>Cód. Aduana *</label>
                        <dx:ASPxComboBox ID="DDL_CodAduana" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_adua" ValueType="System.String" DataSourceID="DS_Aduana">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_adua" Caption="Nombre"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Aduana" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_adua] FROM [Aduana] ORDER BY [ID]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Núm. Viaje *</label>
                        <dx:ASPxTextBox ID="TB_NumViaje" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Fec. Salida *</label>
                        <dx:ASPxDateEdit ID="DE_FecSalida" runat="server" Theme="Material" EditFormat="DateTime" Width="100%">
                            <TimeSectionProperties Visible="True"></TimeSectionProperties>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Fec. Arribo *</label>
                        <dx:ASPxDateEdit ID="DE_FecArribo" runat="server" Theme="Material" EditFormat="DateTime" Width="100%">
                            <TimeSectionProperties Visible="True"></TimeSectionProperties>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Location Code *</label>
                        <dx:ASPxTextBox ID="TB_LocCode" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Uso *</label>
                        <dx:ASPxComboBox ID="DDL_Uso" runat="server" Theme="Material" Width="100%" ValueType="System.Int32">
                            <Items>
                                <dx:ListEditItem Text="IMPORTACI&#211;N" Value="1"></dx:ListEditItem>
                                <dx:ListEditItem Text="EXPORTACI&#211;N" Value="2"></dx:ListEditItem>
                            </Items>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Total BLS *</label>
                        <dx:ASPxTextBox ID="TB_TotBls" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Total Paquetes *</label>
                        <dx:ASPxTextBox ID="TB_TotPaq" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Total Contenedores *</label>
                        <dx:ASPxTextBox ID="TB_TotConts" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Total Gross Mass *</label>
                        <dx:ASPxTextBox ID="TB_TotGM" runat="server" Theme="Material" Width="100%">
                            <ClientSideEvents KeyPress="function (s,e) { onlyNumbers(s, e); }" />
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Cód. Carrier *</label>
                        <dx:ASPxTextBox ID="TB_CodCarr" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Nom. Carrier *</label>
                        <dx:ASPxTextBox ID="TB_NomCarr" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Dir. Carrier *</label>
                        <dx:ASPxTextBox ID="TB_DirCarr" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cód. Mod. Transporte *</label>
                        <dx:ASPxComboBox ID="DDL_CodModTrans" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_mod_trans" ValueType="System.Int32" DataSourceID="DS_ModoTrans">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_mod_trans" Caption="Modo Transporte"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_ModoTrans" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_mod_trans] FROM [ModoTransporte]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>ID Transportador *</label>
                        <dx:ASPxTextBox ID="TB_IDTrans" runat="server" Theme="Material" Width="100%">
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cód. Nac. Transportador *</label>
                        <dx:ASPxComboBox ID="DDL_CodNacTrans" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_pais" ValueType="System.String" DataSourceID="DS_Pais">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="C&#243;digo" Width="40px"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_pais" Caption="Pa&#237;s"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Pais" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_pais] FROM [Pais] ORDER BY [ID]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cód. Pto. Salida *</label>
                        <dx:ASPxComboBox ID="DDL_PtoSalida" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_pto" ValueType="System.String" DataSourceID="DS_Puerto">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Width="40px" Caption="C&#243;digo"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_pto" Caption="Puerto"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:SqlDataSource runat="server" ID="DS_Puerto" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [nom_pto] FROM [Puerto]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cód. Pto. Destino *</label>
                        <dx:ASPxComboBox ID="DDL_PtoDestino" runat="server" Theme="Material" Width="100%" ValueField="ID" TextField="nom_pto" ValueType="System.String" DataSourceID="DS_Puerto">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Width="40px" Caption="C&#243;digo"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="nom_pto" Caption="Puerto"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings ValidationGroup="Viaje" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="controls">
                        <label>Cód. de Línea</label>
                        <dx:ASPxTextBox ID="TB_CodLinea" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Alm. Destino</label>
                        <dx:ASPxTextBox ID="TB_AlmDest" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Cód. de Buque</label>
                        <dx:ASPxTextBox ID="TB_CodBuq" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="controls">
                        <label>Nom. de Buque</label>
                        <dx:ASPxTextBox ID="TB_NomBuq" runat="server" Theme="Material" Width="100%"></dx:ASPxTextBox>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="form-body"></div>
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