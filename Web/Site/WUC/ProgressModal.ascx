<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressModal.ascx.cs" Inherits="Web.Site.WUC.ProgressModal" %>

<style type="text/css">
    .progressBackground {
        position: fixed;
        z-index: 98;
        top: 0px;
        left: 0px;
        right: 0px;
        bottom: 0px;
        background-color: #aaa;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }
    .progressContent {
        z-index: 99;
        margin: 250px auto;
        width: 128px;
        height: 128px;
    }
</style>

<asp:UpdateProgress runat="server" DisplayAfter="100">
    <ProgressTemplate>
        <div class="progressBackground" />
        <div class="progressContent">
            <!--<img src="../../img/loading.gif" alt="" />-->
            <h2><asp:Literal runat="server" Text="<%$ Resources:Resource, Procesando %>"></asp:Literal></h2>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
