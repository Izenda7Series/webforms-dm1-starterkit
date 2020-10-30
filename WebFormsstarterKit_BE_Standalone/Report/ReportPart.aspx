<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPart.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Report.ReportPart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Report Viewer</title>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <%: Scripts.Render("~/bundles/modernizr") %>
    
    <%--Apply css--%>
    <link href="~/Scripts/izenda/izenda-ui.css" rel="stylesheet"/>

</head>
<body>
    <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>
    <%: Scripts.Render("~/bundles/izenda") %>

    <script type="text/javascript">
        $(document).ready(function () {
            DoIzendaConfig();
            $('[data-toggle="tooltip"]').tooltip();
            $('[data-toggle="dropdown"]').dropdown();
            $("#reportPartLoader").hide();
            $("#reportPartLoaderText").hide();

            izendaInitReportPartExportViewer('<%=ReportId%>', '<%=Token%>');
            // $('#izenda-root').children().first().removeClass('izenda');
            //var classList = $('#izenda-root').children().first().attr('class').split(/\s+/);
            //$.each(classList, function (index, item) {
            //    console.log(item);
            //});
        });
    </script>

    <style>
    #izenda-root > .izenda {
        background-color: transparent !important;
    }

    body {
        padding: 0 !important;
    }

    .container-fluid {
        padding: 0 !important;
        line-height: 0 !important;
    }
</style>

    <div class="izenda-container" id="izenda-root" style="margin-top: 0px;"></div>
</body>
</html>
