<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardViewer.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Dashboard.DashboardViewer" %>
<asp:Content ID="DashboardViewerContent" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
        $(document).ready(function () {
            izendaInitDashboardViewer('<%=DashboardId%>');
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
