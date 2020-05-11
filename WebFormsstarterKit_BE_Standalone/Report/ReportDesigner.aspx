<%@ Page Title="New Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportDesigner.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Report.ReportDesigner" %>

<asp:Content ID="ReportDesignerContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitReportDesigner();
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
