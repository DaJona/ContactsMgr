<%@ Page Title="" Language="C#" MasterPageFile="~/Site/LeftSidebarSite.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Web.Site.Preferences.Account" %>
<%@ Register Src="~/Site/WUC/InfoMessage.ascx" TagName="InfoMessage" TagPrefix="wuc" %>
<%@ Register Src="~/Site/WUC/PreferencesLeftSidebar.ascx" TagName="PreferencesLeftSidebar" TagPrefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leftSidebar" runat="server">
    <wuc:PreferencesLeftSidebar ID="preferencesLeftSidebar" runat="server" ActiveItem="Member" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, InformacionCuenta %>"></asp:Literal></h1>
    <wsc:CMValidationSummary runat="server" />
    <wuc:InfoMessage ID="InfoMessage" runat="server" />

    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtEmail" Text="<%$ Resources:Resource, Email %>"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" MaxLength="50" Enabled="false"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtRealName" Text="<%$ Resources:Resource, NombreCompleto %>"></asp:Label>
        <asp:TextBox ID="txtRealName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
        <wsc:CMRequiredFieldValidator ID="reqRealName" runat="server" ControlToValidate="txtRealName" />
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtDisplayName" Text="<%$ Resources:Resource, NombreVisible %>"></asp:Label>
        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
        <wsc:CMRequiredFieldValidator ID="reqDisplayName" runat="server" ControlToValidate="txtDisplayName" />
    </div>

    <div class="btnContainer">
        <wsc:CMButton ID="cmdEditPreferences" runat="server" Text="<%$ Resources:Resource, Editar %>" CssClass="btn-lg" OnClick="cmdEditPreferences_Click" />
        <wsc:CMCustomValidator ID="cvEditPreferences" runat="server" />
    </div>
</asp:Content>