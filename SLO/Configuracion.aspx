<%@ Page Title="Configuracion | OpenDoc" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SLO.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    form {
        display: block;
    }
    .form-grid {
        position: static;
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
    <asp:Panel ID="PN_ContainerForm" runat="server">
        <div class="row my-3">
            <div class="col d-flex">
                <asp:LinkButton ID="BTN_AgregarUsuario" runat="server" Text="Nuevo" CssClass="btn btn-info" OnClick="BTN_AgregarUsuario_Click">
                    <i class="fas fa-user-plus"></i> Agregar Nuevo Usuario
                </asp:LinkButton>
            </div>
            <div class="col">
                <h2 class="text-center">Usuarios</h2>
            </div>
            <div class="col"></div>
        </div>
        <div class="form-grid">
            <dx:ASPxGridView ID="GV_Usuarios" runat="server" Width="100%" Theme="Material" AutoGenerateColumns="False" DataSourceID="DS_Usuario" KeyFieldName="ID" 
                OnRowCommand="GV_Usuarios_RowCommand">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="username" Caption="Usuario" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="descrip" Caption="Nombre" VisibleIndex="2"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="email" Caption="Email" VisibleIndex="3"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="region" Caption="Región" VisibleIndex="4"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="activo" Caption="Activo" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                    <dx:GridViewDataColumn Width="60px" VisibleIndex="6" Caption="Editar">
                        <DataItemTemplate>
                            <asp:LinkButton ID="BTN_EditarUsuario" runat="server" CssClass="btn btn-primary" CommandName="Editar">
                                <i class="fas fa-edit"></i> Editar
                            </asp:LinkButton>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Width="60px" VisibleIndex="7" Caption="Eliminar" >
                        <DataItemTemplate>
                            <asp:LinkButton ID="BTN_ConfirmarEliminarUsuario" runat="server" CssClass="btn btn-danger" CommandName="Eliminar">
                                <i class="fas fa-times"></i> Eliminar
                            </asp:LinkButton>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
    </asp:Panel>
    <asp:SqlDataSource runat="server" ID="DS_Usuario" ConnectionString='<%$ ConnectionStrings:SLOConnectionString %>' SelectCommand="SELECT [ID], [username], [descrip], [email], [region], [activo] FROM [Usuario]"></asp:SqlDataSource>
    <%-- MODAL DELETE --%>
    <div class="modal fade" id="modal-delete" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <i class="fas fa-warning"></i>
                    <dx:ASPxLabel ID="LBL_Delete" runat="server" Font-Size="25px" Width="100%"></dx:ASPxLabel>
                </div>
                <div class="modal-footer buttons">
                    <button class="btn btn-danger" data-dismiss="modal">No</button>
                    <dx:ASPxButton ID="BTN_EliminarUsuario" runat="server" Text="Sí" CssClass="btn btn-success" OnClick="BTN_EliminarUsuario_Click"></dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>
</form>
</asp:Content>