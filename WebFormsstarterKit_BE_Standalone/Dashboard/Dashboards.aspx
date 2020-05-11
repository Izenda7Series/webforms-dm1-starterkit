<%@ Page Title="Dashboards" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboards.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Dashboard.Dashboards" %>
<asp:Content ID="DashboardsContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitDashboard();
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
