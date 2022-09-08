<%@ Page Title="Dashboard" Language="C#" MasterPageFile="Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SLO.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
.col-10.container {
    position: relative;
}
img {
    margin: 0 auto;
    display: block;
    position: absolute;
    top: 50%;
    left: 0;
    right: 0;
    -ms-transform: translateY(-50%);
    transform: translateY(-50%);
}
</style>
<asp:Image ImageUrl="Images/logo.png" runat="server" />
</asp:Content>