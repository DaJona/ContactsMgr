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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="divData" runat="server" class="panel panel-default">
                <div class="panel-heading">
                    <strong><asp:Literal runat="server" Text="<%$ Resources:Resource, OpcionesMasivas %>"></asp:Literal>: </strong>
                    <div id="divMasiveOptions" style="display: none;">
                        <ul class="list-inline">
		  	                <li>
		  		                <asp:LinkButton ID="lnkDeleteMasive" runat="server" OnClick="lnkDeleteMasive_Click">
                                    <span class="glyphicon glyphicon-remove"></span>
                                    <asp:Literal runat="server" Text="<%$ Resources:Resource, EliminarSeleccionados%>"></asp:Literal>
                                </asp:LinkButton>
		  	                </li>
		                </ul>
                    </div>
                    <div id="divMasiveOptionsExplanation" style="display: inline-block; font-style: italic; font-size: 90%;">
                        <asp:Literal runat="server" Text="<%$ Resources:Resource, SeleccioneOpcionesMasivas %>"></asp:Literal>
                    </div>
                </div>
                <div class="panel-body">
                    <wsc:CMGridView ID="grdContacts" runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnRowCommand="grdContacts_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input type="checkbox" id="chkHeader" runat="server" class="chkHeader" onchange="checkHeader(this)">
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" id="chkItem" runat="server" class="chkItem" onchange="showHideMasiveOptions()">
                                    
                                </ItemTemplate>
                                <HeaderStyle CssClass="action" />
                            </asp:TemplateField>

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

            <asp:Panel ID="deleteSelectedContactsModal" runat="server" class="modal-dialog modal-sm" style="display: none;">
                <div class="modal-content">
                    <div class="modal-header">
                        <a href="#" class="close" onclick="return hideDeleteSelectedContactsModal()">&times;</a>
                        <h3 class="modal-title"><asp:Literal runat="server" Text="<%$ Resources:Resource, EliminarContactosSeleccionados %>"></asp:Literal></h3>
                    </div>
                    <div class="modal-body">
                        <p><asp:Literal runat="server" Text="<%$ Resources:Resource, ConfirmarEliminarContactosSeleccionados %>"></asp:Literal></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="cmdDeleteSelectedContacts" runat="server" CssClass="btn btn-danger" Text="<%$ Resources:Resource, EliminarContactosSeleccionados %>" OnClick="cmdDeleteSelectedContacts_Click" />
                    </div>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeDeleteSelectedContacts" runat="server"
                TargetControlID="lnkDeleteMasive"
                PopupControlID="deleteSelectedContactsModal"
                BackgroundCssClass="modalBackground"
                RepositionMode="RepositionOnWindowResizeAndScroll">
            </ajaxToolkit:ModalPopupExtender>
            <!-- Modals -->
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function checkHeader(chk) {
            if ($(chk).is(':checked')) {
                $(".chkItem").attr('checked', true);
            }
            else {
                $(".chkItem").attr('checked', false);
            }

            showHideMasiveOptions();
        }

        function showHideMasiveOptions() {
            var result;
            
            $(".chkItem").each(function () {
                if ($(this).is(':checked')) {
                    result = true;
                    return false;
                }
                else {
                    result = false;
                }
            });

            if (result) {
                $("#divMasiveOptionsExplanation").hide();
                $("#divMasiveOptions").css("display", "inline-block");
            }
            else {
                $("#divMasiveOptionsExplanation").css("display", "inline-block");
                $("#divMasiveOptions").hide();
            }
        }

        function hideDeleteContactModal() {
            $find("<%= this.mpeDeleteContact.ClientID%>").hide();
            return false;
        }

        function hideDeleteSelectedContactsModal() {
            $find("<%= this.mpeDeleteSelectedContacts.ClientID%>").hide();
            return false;
        }
    </script>
</asp:Content>
