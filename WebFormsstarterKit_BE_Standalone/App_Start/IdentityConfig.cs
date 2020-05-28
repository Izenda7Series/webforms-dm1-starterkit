using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        { }

        public ApplicationUser FindTenantUser(string tenant, string username, string password)
        {
            var context = ApplicationDbContext.Create();

            var user = context.Users
                .Include(x => x.Tenant)
                .Where(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault(x => x.Tenant.Name.Equals(tenant, StringComparison.InvariantCultureIgnoreCase));

            return user;
        }

        public async Task<ApplicationUser> FindTenantUserAsync(string tenant, string username, string password)
        {
            using (var context = ApplicationDbContext.Create())
            {
                ApplicationUser user = null;

                user = tenant == null
                    ? await context.Users
                        .Include(x => x.Tenant)
                        .Where(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                        .SingleOrDefaultAsync()
                    : await context.Users
                        .Include(x => x.Tenant)
                        .Where(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                        .Where(x => x.Tenant.Name.Equals(tenant, StringComparison.InvariantCultureIgnoreCase))
                        .SingleOrDefaultAsync();

                if (await CheckPasswordAsync(user, password))
                    return user;
            }

            return null;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) 
        { }

        // uncomment this if you want to customize sign in
        /*public SignInStatus CustomSignIn(string tenant, string username, string password, bool remember)
        {
            var appUserManager = this.UserManager as ApplicationUserManager;

            var user = appUserManager?.FindTenantUser(tenant, username, password);
            if (user == null)
            {
                return SignInStatus.Failure;
            }

            this.SignIn(user, remember, remember);
            return SignInStatus.Success;
        }*/

        /// <summary>
        /// Verify login 
        /// </summary>
        public async Task<SignInStatus> PasswordSigninAsync(string tenant, string username, string password, bool remember)
        {
            using (var appUserManager = UserManager as ApplicationUserManager)
            {
                if (appUserManager == null)
                    return SignInStatus.Failure;

                var user = await appUserManager.FindTenantUserAsync(tenant, username, password);

                if (user == null)
                    return SignInStatus.Failure;

                await SignInAsync(user, remember, true);
            }

            return SignInStatus.Success;
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    // Configure the application role manager used in this application. RoleManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationRoleManager : RoleManager<IdentityRole, string>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var manager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
            return manager;
        }
    }
}
