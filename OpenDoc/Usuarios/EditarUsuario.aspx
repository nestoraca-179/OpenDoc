<%@ Page Title="Editar Usuario" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="OpenDoc.Usuarios.EditarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    form {
        display: block;
    }
</style>
<asp:Panel ID="PN_Error" runat="server" Width="100%" CssClass="mt-2" Visible="false">
    <div class="alert alert-danger m-0">
        <dx:ASPxLabel ID="LBL_Error" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
    </div>
</asp:Panel>
<form id="Form1" runat="server" class="container">
    <div class="row my-5">
        <div class="col d-flex">
            <asp:LinkButton ID="BTN_Volver" runat="server" CssClass="btn btn-primary" OnClick="BTN_Volver_Click">
                <i class="fas fa-arrow-left"></i> Regresar
            </asp:LinkButton>
            <dx:ASPxButton ID="BTN_Guardar" runat="server" CssClass="btn btn-success mx-2" Text="Guardar" ValidationGroup="Usuario" OnClick="BTN_Guardar_Click" />
        </div>
        <div class="col">
            <dx:ASPxLabel ID="LBL_IDUsuario" runat="server" Width="100%" Font-Size="24px" CssClass="title-screen text-center"></dx:ASPxLabel>
        </div>
        <div class="col"></div>
    </div>
    <asp:Panel ID="PN_ContainerForm" runat="server" CssClass="form-header">
        <div class="row">
            <div class="col">
                <div class="controls">
                    <label>Usuario</label>
                    <dx:ASPxTextBox ID="TB_Username" runat="server" Theme="Material" Width="100%">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="col">
                <div class="controls">
                    <label>Nombre</label>
                    <dx:ASPxTextBox ID="TB_Descrip" runat="server" Theme="Material" Width="100%">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="controls">
                    <label>Clave</label>
                    <dx:ASPxTextBox ID="TB_Password" runat="server" Theme="Material" Width="100%" Password="true">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="col">
                <div class="controls">
                    <label>Email</label>
                    <dx:ASPxTextBox ID="TB_Email" runat="server" Theme="Material" Width="100%">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorText="Correo Inválido" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="controls">
                    <label>Región</label>
                    <dx:ASPxComboBox ID="DDL_Region" runat="server" Theme="Material" Width="100%">
                        <Items>
                            <dx:ListEditItem Text="VEPBL" Value="VEPBL"></dx:ListEditItem>
                            <dx:ListEditItem Text="VELAG" Value="VELAG"></dx:ListEditItem>
                            <dx:ListEditItem Text="VEMAR" Value="VEMAR"></dx:ListEditItem>
                            <dx:ListEditItem Text="VEGUT" Value="VEGUT"></dx:ListEditItem>
                            <dx:ListEditItem Text="VEEGU" Value="VEEGU"></dx:ListEditItem>
                            <dx:ListEditItem Text="VEGUB" Value="VEGUB"></dx:ListEditItem>
                        </Items>
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div class="col">
                <div class="controls">
                    <label>Tipo Usuario</label>
                    <dx:ASPxComboBox ID="DDL_TipoUsuario" runat="server" Theme="Material" Width="100%" ValueType="System.Int32">
                        <Items>
                            <dx:ListEditItem Text="ADMINISTRADOR" Value="0"></dx:ListEditItem>
                            <dx:ListEditItem Text="IMPORTACION" Value="1"></dx:ListEditItem>
                            <dx:ListEditItem Text="EXPORTACION" Value="2"></dx:ListEditItem>
                        </Items>
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="controls">
                    <dx:ASPxCheckBox ID="CK_Activo" runat="server" Theme="Material" Width="100%" Text="Activo"></dx:ASPxCheckBox>
                </div>
            </div>
            <div class="col"></div>
        </div>
    </asp:Panel>
</form>
</asp:Content>