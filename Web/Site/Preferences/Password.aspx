<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="Web.Site.Preferences.Password" %>
<%@ Register Src="~/Site/WUC/InfoMessage.ascx" TagName="InfoMessage" TagPrefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, CambiarContrasena %>"></asp:Literal></h1>
    <wsc:CMValidationSummary runat="server" />
    <wuc:InfoMessage ID="InfoMessage" runat="server" />

    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtActualPass" Text="<%$ Resources:Resource, ContrasenaActual %>"></asp:Label>
        <asp:TextBox ID="txtActualPass" runat="server" CssClass="form-control" type="password" MaxLength="90"></asp:TextBox>
        <wsc:CMRequiredFieldValidator ID="reqActualPass" runat="server" ControlToValidate="txtActualPass" ErrorMessage="<%$ Resources:Resource, ErrorContrasenaActualReq %>" />
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtNewPass" Text="<%$ Resources:Resource, NuevaContrasena %>"></asp:Label>
        <asp:TextBox ID="txtNewPass" runat="server" CssClass="form-control" type="password" MaxLength="90"></asp:TextBox>
        <wsc:CMRequiredFieldValidator ID="reqNewPass" runat="server" ControlToValidate="txtNewPass" ErrorMessage="<%$ Resources:Resource, ErrorContrasenaNuevaReq %>" />
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtNewPass2" Text="<%$ Resources:Resource, ConfirmarContrasena %>"></asp:Label>
        <asp:TextBox ID="txtNewPass2" runat="server" CssClass="form-control" type="password" MaxLength="90"></asp:TextBox>
        <wsc:CMRequiredFieldValidator ID="reqNewPass2" runat="server" ControlToValidate="txtNewPass2" ErrorMessage="<%$ Resources:Resource, ErrorConfirmarNuevaContrasenaReq %>" />
        <wsc:CMCompareValidator ID="cmpNewPass" runat="server" ControlToValidate="txtNewPass2" ControlToCompare="txtNewPass" ErrorMessage="<%$ Resources:Resource, ErrorConfirmarContrasenaNoIgual %>" />
    </div>

    <div class="btnContainer">
        <wsc:CMButton ID="cmdUpdatePassword" runat="server" Text="<%$ Resources:Resource, CambiarContrasena %>" CssClass="btn-lg" OnClick="cmdUpdatePassword_Click" />
        <wsc:CMCustomValidator ID="cvUpdatePassword" runat="server" />
    </div>
</asp:Content>
