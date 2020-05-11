<%@ Page Title="Report List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Report.Reports" %>
<asp:Content ID="ReportsContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitReport();
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
