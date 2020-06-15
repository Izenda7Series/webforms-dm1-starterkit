<%@ Page Title="Create a user result" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FailedToCreateUser.aspx.cs" Inherits="WebformsIntegratedBE_Standalone.Account.FailedToCreateUser" EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="FailedToCreateUserContent">
    <h2>Failed!</h2>
    <div class="alert alert-danger alert-dismissable">
        <strong>Can't create a new user.</strong>
    </div>
</asp:Content>
