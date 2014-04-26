<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Site.Preferences.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, Preferencias %>"></asp:Literal></h1>

    <ul>
        <li><asp:LinkButton ID="lnkAccount" runat="server" Text="<%$ Resources:Resource, InformacionCuenta %>" OnClick="lnkAccount_Click"></asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkPassword" runat="server" Text="<%$ Resources:Resource, CambiarContrasena %>" OnClick="lnkPassword_Click"></asp:LinkButton></li>
        <li><asp:LinkButton ID="lnkLanguage" runat="server" Text="<%$ Resources:Resource, PreferenciaIdioma %>"></asp:LinkButton></li>
    </ul>
</asp:Content>