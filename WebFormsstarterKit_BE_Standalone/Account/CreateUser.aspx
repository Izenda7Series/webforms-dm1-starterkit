<%@ Page Title="Create User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Async="true" Inherits="WebformsIntegratedBE_Standalone.Account.CreateUser" EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="CreateUserContent">
    <h2><%: Title %></h2>

    <% if (createdSuccessfully != null && !(bool)createdSuccessfully)
        { %>
    <div class="alert alert-danger alert-dismissable">
        <strong>Can't create a new user. The user name or id already exists.</strong>
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
        <strong>User has been created successfully.</strong>
    </div>
    <% } %>

    <div class="row">
        <div class="col-md-8">
            <section id="CreateTenantForm">
                <div class="form-horizontal">
                    <h4>Create a new user</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="TenantList" CssClass="col-md-2 control-label">Tenant*</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="TenantList" CssClass="form-control" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="TenantList_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="Select Tenant"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" runat="server" id="CheckBoxDiv">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="IsAdminCheckBox" />
                                <asp:Label runat="server" AssociatedControlID="IsAdminCheckBox">Is Admin</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="UserID" CssClass="col-md-2 control-label">Tenant ID</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="UserID" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="UserID" CssClass="text-danger" ErrorMessage="The user ID field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">Tenant Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="FirstName" CssClass="text-danger" ErrorMessage="The first name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Tenant Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="LastName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="LastName" CssClass="text-danger" ErrorMessage="The last name field is required." />
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
            <section id="userInstruction">
                <div>
                    <h4>Further Instruction</h4>
                    <hr />
                    <p>
                        If you want to add "Email", or update "TimeZone", "Timestamp Offset", "Language", and "Date Format" for a newly created user, please go to Settings &#8594 User Setup and update it.
                    </p>
                    <p>
                        *If you don't select tenant, it will create a user under system level.
                    </p>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
