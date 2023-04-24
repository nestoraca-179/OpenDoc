<%@ Page Title="Registros | OpenDoc" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Registros.aspx.cs" Inherits="OpenDoc.Registros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    form {
        display: block;
    }
    .form-grid {
        position: static;
    }
</style>
<form id="Form1" runat="server" class="container">
    <asp:Panel ID="PN_ContainerForm" runat="server">
        <div class="row my-3">
            <div class="col"></div>
            <div class="col">
                <h2 class="text-center">Incidentes</h2>
            </div>
            <div class="col"></div>
        </div>
        <div class="form-grid">
            <dx:ASPxGridView ID="GV_Incidentes" runat="server" Theme="Material" Width="100%" AutoGenerateColumns="False" DataSourceID="DS_Incidente" EnableTheming="True" 
                KeyFieldName="ID" SettingsBehavior-AllowDragDrop="false">
                <SettingsPager PageSize="10" Position="TopAndBottom">
                    <PageSizeItemSettings Visible="True"></PageSizeItemSettings>
                </SettingsPager>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Titulo" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="2"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="Fecha" VisibleIndex="3"></dx:GridViewDataDateColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:SqlDataSource runat="server" ID="DS_Incidente" ConnectionString='<%$ ConnectionStrings:OpenDocConnectionString %>' SelectCommand="SELECT * FROM [Incidente] ORDER BY [Fecha] DESC"></asp:SqlDataSource>
        </div>
    </asp:Panel>
</form>
</asp:Content>