<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Web.Site.Contacts.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, CrearContacto%>"></asp:Literal></h1>
    <wsc:CMValidationSummary runat="server" />

    <div class="row">
        <div class="col-sm-3 text-center">
            <div class="formContactPic">
                <asp:Image ID="imgContactPic" runat="server" ImageUrl="~/img/contactsPics/no-pic.jpg" />
            </div>
            <asp:FileUpload ID="uplContactPic" runat="server" title="<%$ Resources:Resource, CambiarFotoContacto %>" data-filename-placement="inside" />
        </div>
        <div class="col-sm-9">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFirstName" Text="<%$ Resources:Resource, Nombre%>"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="<%$ Resources:Resource, ErrorNombreReq%>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtLastName" Text="<%$ Resources:Resource, Apellidos%>"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                <wsc:CMRequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="<%$ Resources:Resource, ErrorApellidosReq%>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlGenre" Text="<%$ Resources:Resource, Genero%>"></asp:Label>
                <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-control">
                    <asp:ListItem Value="0" Text="--" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="F" Text="<%$ Resources:Resource, Femenino%>"></asp:ListItem>
                    <asp:ListItem Value="M" Text="<%$ Resources:Resource, Masculino%>"></asp:ListItem>
                </asp:DropDownList>
                <wsc:CMRequiredFieldValidator ID="reqGenre" runat="server" ControlToValidate="ddlGenre" InitialValue="0" ErrorMessage="<%$ Resources:Resource, ErrorGenreReq%>" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="<%$ Resources:Resource, Email%>"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email" MaxLength="50"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtMobileNumber" Text="<%$ Resources:Resource, NumeroMovil%>"></asp:Label>
                <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtLandlineNumber" Text="<%$ Resources:Resource, NumeroTelefono%>"></asp:Label>
                <asp:TextBox ID="txtLandlineNumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="btnContainer">
        <wsc:CMButton ID="cmdCreateContact" runat="server" Text="<%$ Resources:Resource, Crear%>" CssClass="btn-lg" OnClick="cmdCreateContact_Click" />
        <asp:Button ID="cmdCancel" runat="server" Text="<%$ Resources:Resource, Cancelar %>" CssClass="btn btn-default btn-lg" CausesValidation="false" OnClick="cmdCancel_Click" />
        <wsc:CMCustomValidator ID="cvCreateContact" runat="server" />
    </div>
</asp:Content>
