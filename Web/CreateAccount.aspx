<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="Web.CreateAccount" Culture="auto" UICulture="auto" %>
<%@ Register Src="~/Site/WUC/LanguagesList.ascx" TagName="LanguagesList" TagPrefix="wuc" %>

<!DOCTYPE html>

<html>
    <head runat="server">
        <title><asp:Literal runat="server" Text="<%$ Resources:Resource, CrearCuenta %>"></asp:Literal></title>

        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

        <link href="css/bootstrap/bootstrap.css" rel="stylesheet" type="text/css" media="all">
        <link href="css/site.css" rel="stylesheet" type="text/css" media="all">
    </head>
    <body>
        <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, CrearCuenta %>"></asp:Literal></h1>

        <form id="createAccountForm" runat="server">
            <wsc:CMValidationSummary runat="server" />

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtRealName" Text="<%$ Resources:Resource, NombreCompleto %>" CssClass="requiredField"></asp:Label>
                <asp:TextBox ID="txtRealName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqRealName" runat="server" ControlToValidate="txtRealName" ErrorMessage="<%$ Resources:Resource, ErrorNombreCompletoReq %>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtDisplayName" Text="<%$ Resources:Resource, NombreVisible %>" CssClass="requiredField"></asp:Label>
                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqDisplayName" runat="server" ControlToValidate="txtDisplayName" ErrorMessage="<%$ Resources:Resource, ErrorNombreVisibleReq %>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="<%$ Resources:Resource, Email %>" CssClass="requiredField"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" MaxLength="30"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="<%$ Resources:Resource, ErrorEmailReq %>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPass1" Text="<%$ Resources:Resource, Contrasena %>" CssClass="requiredField"></asp:Label>
                <asp:TextBox ID="txtPass1" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqPass1" runat="server" ControlToValidate="txtPass1" ErrorMessage="<%$ Resources:Resource, ErrorContrasenaReq %>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPass2" Text="<%$ Resources:Resource, ConfirmarContrasena %>" CssClass="requiredField"></asp:Label>
                <asp:TextBox ID="txtPass2" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqPass2" runat="server" ControlToValidate="txtPass2" ErrorMessage="<%$ Resources:Resource, ErrorConfirmarContrasenaReq %>" />
                <wsc:CMCompareValidator ID="cmpPass2" runat="server" ControlToValidate="txtPass2" ControlToCompare="txtPass1" ErrorMessage="<%$ Resources:Resource, ErrorConfirmarContrasenaNoIgual %>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlLanguage" Text="<%$ Resources:Resource, Idioma %>"></asp:Label>
                <wuc:LanguagesList ID="ddlLanguage" runat="server" />
            </div>

            <div class="btnContainer">
                <wsc:CMButton ID="btnCreateAccount" runat="server" Text="<%$ Resources:Resource, CrearCuenta %>" CssClass="btn-lg" OnClick="btnCreateAccount_Click" />
                <wsc:CMCustomValidator ID="cvCreateAccount" runat="server" />
            </div>
        </form>

        <script type="text/javascript" lang="javascript" src="<%= ResolveUrl("~/js/jquery-1.7.2.js")%>"></script>
        <script type="text/javascript" lang="javascript" src="<%= ResolveUrl("~/js/bootstrap.js")%>"></script>
    </body>
</html>
