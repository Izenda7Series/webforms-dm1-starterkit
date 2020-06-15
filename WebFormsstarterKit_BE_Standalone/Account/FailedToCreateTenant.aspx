<%@ Page Title="Create a tenant result" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FailedToCreateTenant.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Account.FailedToCreateTenant" EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="FailedToCreateTenantContent">
    <h2>Failed!</h2>
    <div class="alert alert-danger alert-dismissable">
        <strong>Can't create a new tenant.</strong>
    </div>
</asp:Content>
