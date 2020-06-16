using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class Login : Page
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            // Enable this once you have account confirmation enabled for password reset functionality
            // ForgotPasswordHyperLink.NavigateUrl = "Forgot";
        }

        protected async void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                bool activeDirectoryLogin = true; // active directory login 
                SignInStatus result = SignInStatus.Failure;
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                if (activeDirectoryLogin && !string.IsNullOrEmpty(Tenant.Text)) // exclude system level case
                    result = await signinManager.ActiveDirectorySigninAsync(string.IsNullOrEmpty(Tenant.Text) ? null : Tenant.Text, Email.Text, Password.Text, RememberMe.Checked);
                else
                    result = await signinManager.PasswordSigninAsync(string.IsNullOrEmpty(Tenant.Text) ? null : Tenant.Text, Email.Text, Password.Text, RememberMe.Checked);

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;

                    case SignInStatus.LockedOut:
                        // Optional enum - you can set up Lockout
                        // Response.Redirect("/Account/Lockout");
                        break;

                    case SignInStatus.RequiresVerification:
                        Response.Redirect($"/Account/TwoFactorAuthenticationSignIn?ReturnUrl={Request.QueryString["ReturnUrl"]}&RememberMe={RememberMe.Checked}", true);
                        break;

                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        } 
        #endregion
    }
}
