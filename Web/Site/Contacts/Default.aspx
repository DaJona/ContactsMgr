﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Site.Contacts.Default" %>
<%@ Register Src="~/Site/WUC/NoData.ascx" TagName="NoData" TagPrefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site" runat="server">
    <h1><asp:Literal runat="server" Text="<%$ Resources:Resource, MisContactos%>"></asp:Literal></h1>
    <wsc:CMValidationSummary runat="server" />

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="divData" runat="server" class="panel panel-default">
                <div class="panel-heading">
                    <ul class="list-inline">
		  	            <li>
		  		            <asp:LinkButton ID="lnkCreate" runat="server" OnClick="lnkCreate_Click">
                                <span class="glyphicon glyphicon-plus"></span>
                                <asp:Literal runat="server" Text="<%$ Resources:Resource, Crear%>"></asp:Literal>
                            </asp:LinkButton>
		  	            </li>
		            </ul>
                </div>
                <div class="panel-body">
                    <wsc:CMGridView ID="grdContacts" runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnRowCommand="grdContacts_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="action" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="<%$ Resources:Resource, ClicEditar %>" 
                                        CommandName="editContact"
                                        CommandArgument='<%# (int)Eval("id") %>'>
                                        <span class="glyphicon glyphicon-edit"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="<%$ Resources:Resource, ClicEliminar %>" 
                                        CommandName="deleteContact"
                                        CommandArgument='<%# (int)Eval("id") %>'>
                                        <span class="glyphicon glyphicon-remove"></span>
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
                                <ItemStyle CssClass="action" Width="100px" />
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
                </div>
                <div class="panel-footer">
                    <asp:Literal runat="server" Text="<%$ Resources:Resource, RegistrosEncontrados %>"></asp:Literal>: <asp:Literal ID="regQuantity" runat="server"></asp:Literal>
                </div>
            </div>
            <wuc:NoData ID="divNoData" runat="server" />

            <wsc:CMCustomValidator ID="cvDefaultContacts" runat="server" />

            <!-- Modals -->
            <asp:HiddenField ID="hdnContactIdToDelete" runat="server" />
            <asp:Panel ID="deleteContactModal" runat="server" class="modal-dialog modal-sm" style="display: none;">
                <div class="modal-content">
                    <div class="modal-header">
                        <a href="#" class="close" onclick="return hideDeleteContactModal()">&times;</a>
                        <h3 class="modal-title"><asp:Literal runat="server" Text="<%$ Resources:Resource, EliminarContacto %>"></asp:Literal></h3>
                    </div>
                    <div class="modal-body">
                        <p><asp:Literal runat="server" Text="<%$ Resources:Resource, ConfirmarEliminarContacto %>"></asp:Literal></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="cmdDeleteContact" runat="server" CssClass="btn btn-danger" Text="<%$ Resources:Resource, EliminarContacto %>" OnClick="cmdDeleteContact_Click" />
                    </div>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeDeleteContact" runat="server"
                TargetControlID="hdnContactIdToDelete"
                PopupControlID="deleteContactModal"
                BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResizeAndScroll">
            </ajaxToolkit:ModalPopupExtender>
            <!-- Modals -->
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function hideDeleteContactModal() {
            $find("<%= this.mpeDeleteContact.ClientID%>").hide();
            return false;
        }
    </script>
</asp:Content>
