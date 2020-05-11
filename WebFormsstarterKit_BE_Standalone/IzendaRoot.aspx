<%@ Page Title="Izenda" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="IzendaRoot.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.IzendaRoot" %>

<asp:Content ID="IzendaContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            izendaInit();
        });
    </script>

    <div class="loader" id="progressLoader"></div>
    <div class="izenda-container" id="izenda-root"></div>
</asp:Content>
