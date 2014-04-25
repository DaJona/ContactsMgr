<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" Culture="auto" UICulture="auto" %>

<!DOCTYPE html>

<html>
    <head runat="server">
        <title><asp:Literal runat="server" Text="<%$ Resources:Resource, IniciarSesion%>" /></title>

        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

        <link href="css/bootstrap/bootstrap.css" rel="stylesheet" type="text/css" media="all">
        <link href="css/site.css" rel="stylesheet" type="text/css" media="all">
    </head>
    <body>
        <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, IniciarSesion %>"></asp:Literal></h1>

        <form id="loginForm" runat="server" role="form">
            <wsc:CMValidationSummary runat="server" />

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="<%$ Resources:Resource, Usuario %>"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="<%$ Resources:Resource, ErrorUsuarioReq %>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPass" Text="<%$ Resources:Resource, Contrasena %>"></asp:Label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqPass" runat="server" ControlToValidate="txtPass" ErrorMessage="<%$ Resources:Resource, ErrorContrasenaReq %>" />
            </div>
            
            <div class="btnContainer">
                <wsc:CMButton ID="btnLogin" runat="server" Text="<%$ Resources:Resource, IniciarSesion %>" CssClass="btn-lg" OnClick="btnLogin_Click" />
                <wsc:CMCustomValidator  ID="cvLogin" runat="server" />
            </div>
        </form>

        <script type="text/javascript" lang="javascript" src="<%= ResolveUrl("~/js/jquery-1.7.2.js")%>"></script>
        <script type="text/javascript" lang="javascript" src="<%= ResolveUrl("~/js/bootstrap.js")%>"></script>
    </body>
</html>