<%@ Page Title="" Language="C#" MasterPageFile="~/Site/LeftSidebarSite.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Site.Preferences.Default" %>
<%@ Register Src="~/Site/WUC/PreferencesLeftSidebar.ascx" TagName="PreferencesLeftSidebar" TagPrefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leftSidebar" runat="server">
    <wuc:PreferencesLeftSidebar ID="preferencesLeftSidebar" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, Preferencias %>"></asp:Literal></h1>
</asp:Content>