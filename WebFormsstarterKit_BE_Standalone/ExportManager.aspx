<%@ Page Title="Export Manager" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportManager.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.ExportManager" %>

<asp:Content ID="SettingsContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitRenderExportManagerPage();
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
