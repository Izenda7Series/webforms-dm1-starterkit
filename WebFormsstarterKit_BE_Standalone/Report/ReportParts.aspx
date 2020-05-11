<%@ Page Title="Report Parts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportParts.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Report.ReportParts" %>

<asp:Content ID="ReportPartsContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInitReportPartDemo();
        });
    </script>
    
    <style>
        .report-part-chart {
            float: left;
            width: 40%;
            height:  40%;
            margin-left: 20px;
            margin-top: 20px;
            background-color: white;
        }

        .report-part-grid {
            float: left;
            width: 40%;
            height:  50%;
            margin-left: 20px;
            margin-top: 20px;
            background-color: white;
        }
    </style>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
