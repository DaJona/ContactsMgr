<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Site.Contacts.Default" %>
<%@ Register Src="~/Site/WUC/NoData.ascx" TagName="NoData" TagPrefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, MisContactos%>"></asp:Literal></h1>
    <wsc:CMValidationSummary runat="server" />

    <div class="well well-sm">
        <ul class="list-inline">
		  	<li>
		  		<asp:LinkButton ID="lnkCreate" runat="server" OnClick="lnkCreate_Click">
                    <span class="glyphicon glyphicon-plus"></span>
                    <asp:Literal runat="server" Text="<%$ Resources:Resource, Crear%>"></asp:Literal>
                </asp:LinkButton>
		  	</li>
		</ul>
    </div>

    <wsc:CMGridView ID="grdContacts" runat="server" AutoGenerateColumns="false" OnRowCommand="grdContacts_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="<%$ Resources:Resource, ClicEditar %>" 
                        CommandName="editContact"
                        CommandArgument='<%# (int)Eval("id") %>'>
                        <span class="glyphicon glyphicon-edit"></span>
                    </asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle CssClass="action" />
            </asp:TemplateField>

            <asp:BoundField HeaderText="<%$ Resources:Resource, Nombre%>" DataField="firstName" HtmlEncode="false" />
            <asp:BoundField HeaderText="<%$ Resources:Resource, Apellidos%>" DataField="lastName" HtmlEncode="false" />
            <asp:BoundField HeaderText="<%$ Resources:Resource, Email%>" DataField="email" HtmlEncode="false" />
            <asp:BoundField HeaderText="<%$ Resources:Resource, NumeroMovil%>" DataField="mobileNumber" HtmlEncode="false" />
            <asp:BoundField HeaderText="<%$ Resources:Resource, NumeroTelefono%>" DataField="landlineNumber" HtmlEncode="false" />

            <asp:TemplateField HeaderText="<%$ Resources:Resource, Estado%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkChangeStatus" runat="server" Text='<%# getStatusText((bool)Eval("isActive")) %>' ToolTip="<%$ Resources:Resource, ClicCambiar %>" 
                        CssClass='<%# ((bool)Eval("isActive")) ? "btn btn-success btn-sm" : "btn btn-warning btn-sm" %>'
                        CommandName='<%# ((bool)Eval("isActive")) ? "deactivate" : "activate" %>'
                        CommandArgument='<%# (int)Eval("id") %>'>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NextPreviousFirstLast" />
    </wsc:CMGridView>
    <wuc:NoData ID="divNoData" runat="server" />

    <wsc:CMCustomValidator ID="cvDefaultContacts" runat="server" />
</asp:Content>
