<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebformsIntegratedBE_Standalone._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Izenda WebForms Starterkit</h1>
        <p class="lead">This starter kit will help your development team quickly and easily embed Izenda into your application.</p>
    </div>

    <div>
        <div class="col-md-4">
            <h2>User Integration</h2>
            <p>Easily integrate your users, roles, and tenants with Izenda. </p>
            <p><a class="btn btn-primary" href="https://www.izenda.com/docs/dev/api_backend_integration.html" role="button">Learn more »</a></p>
        </div>
        <div class="col-md-4">
            <h2>Hidden Filters</h2>
            <p>Our flexible API allows you to apply row-level security to your data sources. Filter data by user, role, tenant, or your own custom logic. </p>
            <p><a class="btn btn-primary" href="https://www.izenda.com/docs/dev/code_modifying_user_access_token.html" role="button">Learn more »</a></p>
        </div>
        <div class="col-md-4">
            <h2>Security</h2>
            <p>Review best practices to help ensure a safe and secure deployment of Izenda.</p>
            <p><a class="btn btn-primary" href="https://www.izenda.com/docs/ref/models/_security.html" role="button">Learn more »</a></p>
        </div>
    </div>
</asp:Content>
