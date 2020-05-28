using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.UI;
using WebformsIntegratedBE_Standalone.IzendaBoundary;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class CreateUser : Page
    {
        #region Variables
        public static bool? createdSuccessfully = null;
        public static bool serverError = false;
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TenantList.DataSource = IzendaUtilities.GetAllTenant();
                TenantList.DataBind();

                // back to default status
                createdSuccessfully = null;
                serverError = false;
            }
        }
        
        protected async void Submit(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int? tenantId = null;
                bool isAdmin = IsAdminCheckBox.Checked;
                var selectedTenant = TenantList.SelectedValue;
                var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roleManager = Context.GetOwinContext().Get<ApplicationRoleManager>();
                
                if (!string.IsNullOrEmpty(selectedTenant))
                {
                    tenantId = IzendaUtilities.GetTenantByName(selectedTenant)?.Id;
                    isAdmin = false;

                    if (tenantId == null)
                    {
                        createdSuccessfully = null;
                        serverError = true;
                        return;
                    }
                }

                var appUser = new ApplicationUser
                {
                    UserName = UserID.Text,
                    Email = UserID.Text,
                    Tenant_Id = tenantId,
                };

                var result = await userManager.CreateAsync(appUser);

                if (result.Succeeded)
                {
                    var assignedRole = "Employee"; // set default role if required

                    if (roleManager.RoleExists(assignedRole))
                        result = await userManager.AddToRoleAsync(appUser.Id, assignedRole);
                    else
                        assignedRole = null;

                    if (result.Succeeded)
                    {
                        var izendaAdminAuthToken = IzendaTokenAuthorization.GetIzendaAdminToken();
                        appUser.Tenant = IzendaUtilities.GetTenantByName(selectedTenant);

                        var success = await IzendaUtilities.CreateIzendaUser(selectedTenant, UserID.Text, LastName.Text,
                            FirstName.Text,
                            isAdmin,
                            assignedRole, izendaAdminAuthToken);

                        if (success)
                            createdSuccessfully = true;
                        else
                        {
                            createdSuccessfully = null;
                            serverError = true;
                        }
                    }
                    else
                        createdSuccessfully = false;
                }
                else
                    createdSuccessfully = false;
            }
        }

        protected void TenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TenantList.SelectedValue))
                CheckBoxDiv.Visible = true;
            else
                CheckBoxDiv.Visible = false;
        }
        #endregion
    }
}
