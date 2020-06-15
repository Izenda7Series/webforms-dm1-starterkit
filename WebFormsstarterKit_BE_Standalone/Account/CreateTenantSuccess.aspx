<%@ Page Title="Create a tenant result" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="CreateTenantSuccess.aspx.cs" Async="true"
    Inherits="WebformsIntegratedBE_Standalone.Account.CreateTenantSuccess" 
    EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="CreateTenantSuccessContent">
    <h2>Success!</h2>
    <div class="alert alert-success alert-dismissable">
        <strong>Tenant has been created successfully.</strong>
    </div>
</asp:Content>
