using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using WebformsIntegratedBE_Standalone.IzendaBoundary;
using WebformsIntegratedBE_Standalone.Models;
using WebGrease.Css.Extensions;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class CreateUser : Page
    {
        #region Methods
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TenantList.DataSource = IzendaUtilities.GetAllTenants();
                TenantList.DataBind();

                await PopulateRoleList();
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
                var izendaAdminAuthToken = IzendaTokenAuthorization.GetIzendaAdminToken();

                if (!string.IsNullOrEmpty(selectedTenant))
                {
                    tenantId = IzendaUtilities.GetTenantByName(selectedTenant)?.Id;
                    isAdmin = false;

                    if (tenantId == null)
                        return;
                }

                var appUser = new ApplicationUser
                {
                    UserName = UserID.Text,
                    Email = UserID.Text,
                    Tenant_Id = tenantId,
                };

                var result = await userManager.CreateAsync(appUser); // attempt to create a new user in client DB

                if (result.Succeeded) // if successful, then start creating a user at Izenda DB
                {
                    var assignedRole = !string.IsNullOrEmpty(RoleList.SelectedValue) ? RoleList.SelectedValue : "Employee"; // set default role if required. As an example, Employee is set by default

                    if (!roleManager.RoleExists(assignedRole)) // check assigned role exist in client DB. if not, assigned role is null
                    {
                        try
                        {
                            await roleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(assignedRole));
                            result = await userManager.AddToRoleAsync(appUser.Id, assignedRole);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }

                    if (result.Succeeded)
                    {
                        appUser.Tenant = IzendaUtilities.GetTenantByName(selectedTenant); // set client DB application user's tenant

                        // Create a new user at Izenda DB
                        var success = await IzendaUtilities.CreateIzendaUser(
                            selectedTenant, 
                            UserID.Text, 
                            LastName.Text,
                            FirstName.Text,
                            isAdmin,
                            assignedRole, 
                            izendaAdminAuthToken);

                        if (success)
                            RedirectResultPage(true);
                        else
                            RedirectResultPage(false);
                    }
                    else
                        RedirectResultPage(false);
                }
                else
                    RedirectResultPage(false);
            }
        }

        protected async void TenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TenantList.SelectedValue))
            {
                CheckBoxDiv.Visible = true;
            }
            else
            {
                CheckBoxDiv.Visible = false;
            }

            await PopulateRoleList();
        }

        private async Task PopulateRoleList()
        {
            RoleList.Items.Clear();

            var selectList = new List<string>();
            var selectedTenant = TenantList.SelectedValue;
            var adminToken = IzendaTokenAuthorization.GetIzendaAdminToken();
            var izendaTenant = await IzendaUtilities.GetIzendaTenantByName(selectedTenant, adminToken);
            var roleDetailsByTenant = await IzendaUtilities.GetAllIzendaRoleByTenant(izendaTenant?.Id ?? null, adminToken);
            roleDetailsByTenant.ForEach(r => selectList.Add(r.Name));

            RoleList.DataSource = selectList;
            RoleList.DataBind();
            RoleList.SelectedValue = selectList.FirstOrDefault();
        }

        private void RedirectResultPage(bool success)
        {
            if (success)
                Response.Redirect("~/Account/CreateUserSuccess.aspx");
            else
                Response.Redirect("~/Account/FailedToCreateUser.aspx", false);
        }
        #endregion
    }
}
