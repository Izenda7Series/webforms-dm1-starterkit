<%@ Page Title="Report Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Report.ReportViewer" %>
<asp:Content ID="ReportViewerContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitReportViewer('<%=ReportId%>');
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
