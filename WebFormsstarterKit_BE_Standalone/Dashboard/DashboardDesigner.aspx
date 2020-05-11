<%@ Page Title="New Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardDesigner.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Dashboard.DashboardDesigner" %>
<asp:Content ID="DashboardDesignerContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitNewDashboard();
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
