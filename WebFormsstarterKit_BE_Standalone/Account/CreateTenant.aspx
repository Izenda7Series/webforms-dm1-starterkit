<%@ Page Title="Create Tenant" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateTenant.aspx.cs" Async="true" Inherits="WebformsIntegratedBE_Standalone.Account.CreateTenant" EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="CreateTenantContent">
    <h2><%: Title %></h2>

    <% if (createdSuccessfully != null && !(bool)createdSuccessfully)
        { %>
    <div class="alert alert-danger alert-dismissable">
        <strong>Can't create a new tenant. The tenant name or id already exists.</strong>
    </div>
    <% } %>

    <% if (createdSuccessfully == null && serverError)
        { %>
    <div class="alert alert-danger alert-dismissable">
        <strong>Server does not allow your request.</strong>
    </div>
    <% } %>

    <% if (createdSuccessfully != null && (bool)createdSuccessfully)
        { %>
    <div class="alert alert-success alert-dismissable">
        <strong>Tenant has been created successfully.</strong>
    </div>
    <% } %>

    <div class="row">
        <div class="col-md-8">
            <section id="CreateTenantForm">
                <div class="form-horizontal">
                    <h4>Create a new tenant</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TenantID" CssClass="col-md-2 control-label">Tenant ID</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TenantID" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="TenantID" CssClass="text-danger" ErrorMessage="The tenant ID field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TenantName" CssClass="col-md-2 control-label">Tenant Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TenantName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="TenantName" CssClass="text-danger" ErrorMessage="The tenant name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="Submit" Text="Submit" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <div class="col-md-4">
            <section id="tenantIntruction">
                <div>
                    <h4>Further Instruction</h4>
                    <hr />
                    <p>
                        If you want to add "Description" or enable a Module(s) to a newly created tenant, please go to Settings &#8594 Tenant Setup and update it.
                    </p>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
