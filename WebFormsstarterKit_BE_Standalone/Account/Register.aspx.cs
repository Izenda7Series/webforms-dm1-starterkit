using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class Register : Page
    {
        #region variables
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        #endregion

        #region Properties
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Context.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Context.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// event handler for click to register
        /// </summary>
        protected async void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var tenant = new Tenant() { Name = Tenant.Text };
            var tenantManager = new Managers.TenantManager();
            var exstingTenant = tenantManager.GetTenantByName(tenant.Name);

            tenant = exstingTenant ?? await tenantManager.SaveTenantAsync(tenant);

            var user = new ApplicationUser()
            {
                UserName = Email.Text,
                Email = Email.Text,
                Tenant_Id = tenant.Id
            };

            IdentityResult result = await manager.CreateAsync(user, Password.Text);
            string assignedRole = "Employee";
            string assignedRoleName = "";

            if (RoleManager.RoleExists(assignedRole))
            {
                result = await UserManager.AddToRoleAsync(user.Id, assignedRole);
                assignedRoleName = (RoleManager.FindByNameAsync(assignedRole)).Result.Name;
            }
            else
            {
                assignedRole = null;
            }
            if (result.Succeeded)
            {
                user.Tenant = tenant;

                ////izenda
                var izendaAdminAuthToken = IzendaBoundary.IzendaTokenAuthorization.GetIzendaAdminToken();

                if (tenant != null)
                    await IzendaBoundary.IzendaUtility.CreateTenant(tenant.Name, izendaAdminAuthToken);

                //CreateUser is Deprecated in favor of CreateIzendaUser
                //await IzendaBoundary.IzendaUtilities.CreateUser(user, assignedRole, izendaAdminAuthToken);
                await IzendaBoundary.IzendaUtility.CreateIzendaUser(user, assignedRoleName, izendaAdminAuthToken);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
                ErrorMessage.Text = result.Errors.FirstOrDefault();
        } 
        #endregion
    }
}
