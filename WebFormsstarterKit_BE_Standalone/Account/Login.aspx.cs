using Microsoft.AspNet.Identity.Owin;
using System;
using System.Configuration;
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
                bool useADlogin;
                var adLoginSetting = ConfigurationManager.AppSettings["useADlogin"]; // if you want to enable active directory login, then set this boolean value to true (Web.config). Default is false.
                bool.TryParse(adLoginSetting, out useADlogin);

                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                var result = SignInStatus.Failure;

                if (useADlogin.Equals("true") && !string.IsNullOrEmpty(Tenant.Text)) // if tenant is null, then assume that it is system level login. Go to the ValidateLogin which is used for regular login process first
                {
                    // If we allow AD authentication, then email / password field are not required because it can be retrieved from active directory information.
                    // You can remove those fields from front-end UI. 
                    // However, tenant field is required because it is used for GetToken.
                    result = await signinManager.ADSigninAsync(string.IsNullOrEmpty(Tenant.Text) ? null : Tenant.Text, Password.Text, RememberMe.Checked);
                }
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
