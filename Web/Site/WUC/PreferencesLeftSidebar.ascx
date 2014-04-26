<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreferencesLeftSidebar.ascx.cs" Inherits="Web.Site.WUC.PreferencesLeftSidebar" %>

<h3><asp:Literal runat="server" Text="<%$ Resources:Resource, Menu %>"></asp:Literal></h3>
<ul class="nav nav-pills nav-stacked">
    <li id="memberPreferencesItem"><asp:LinkButton ID="lnkAccount" runat="server" Text="<%$ Resources:Resource, InformacionCuenta %>" OnClick="lnkAccount_Click"></asp:LinkButton></li>
    <li id="passwordPreferencesItem"><asp:LinkButton ID="lnkPassword" runat="server" Text="<%$ Resources:Resource, CambiarContrasena %>" OnClick="lnkPassword_Click"></asp:LinkButton></li>
    <li id="languagePreferencesItem"><asp:LinkButton ID="lnkLanguage" runat="server" Text="<%$ Resources:Resource, PreferenciaIdioma %>"></asp:LinkButton></li>
</ul>

<input id="hiddenActiveItem" runat="server" type="hidden" />

<script type="text/javascript">
    $(document).ready(function () {
        setActiveItem();
    });

    function setActiveItem() {
        // Get the saved value from the hidden input
        var activeItem = $("#<%= hiddenActiveItem.ClientID%>").val();

        // Reset all items
        $("#memberPreferencesItem").removeClass("active");
        $("#passwordPreferencesItem").removeClass("active");
        $("#languagePreferencesItem").removeClass("active");

        // Set the class to specific item
        switch (activeItem.toLowerCase()) {
            case "member":
                $("#memberPreferencesItem").addClass("active");
                break;
            case "password":
                $("#passwordPreferencesItem").addClass("active");
                break;
            case "language":
                $("#languagePreferencesItem").addClass("active");
                break;
            default:
        }
    }
</script>