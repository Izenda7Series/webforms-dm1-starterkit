using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
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

        public async Task<ApplicationUser> FindTenantUserAsync(string tenant, string username)
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

                return user;
            }
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
        {
        }

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

        /// <summary>
        /// Sign in using active directory
        /// </summary>
        public async Task<SignInStatus> ActiveDirectorySigninAsync(string tenant, string email, string password, bool remember)
        {
            string currentUserName = Environment.UserName; // debug - check current AD user name

            var userName = email.Split('@').FirstOrDefault();
            var userDomainName = Environment.UserDomainName;
            ContextType authenticationType = ContextType.Domain;
            UserPrincipal userPrincipal = null;
            bool isAuthenticated = false;

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userDomainName))
            {
                //CheckAllActiveDirectoryUser(); // only for debugging purpose

                using (var context = new PrincipalContext(authenticationType, Environment.UserDomainName))
                {
                    try
                    {
                        userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName);

                        if (userPrincipal != null)
                        {
                            // Validate credential with Active Directory information
                            isAuthenticated = context.ValidateCredentials(userName, password, ContextOptions.Negotiate);

                            if (isAuthenticated)
                            {
                                using (var appUserManager = UserManager as ApplicationUserManager)
                                {
                                    // retrieve tenant information after validation process
                                    var user = await appUserManager.FindTenantUserAsync(tenant, email);

                                    if (user != null)
                                    {
                                        await SignInAsync(user, remember, true);

                                        return SignInStatus.Success;
                                    }
                                    else
                                        return SignInStatus.Failure;
                                }
                            }
                            else
                                return SignInStatus.Failure;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                        return SignInStatus.Failure;
                    }

                    if (!isAuthenticated)
                        return SignInStatus.Failure;

                    if (userPrincipal.IsAccountLockedOut())
                        return SignInStatus.LockedOut;

                    if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
                        return SignInStatus.Failure;
                }
            }
            
            return SignInStatus.Failure;
        }

        /// <summary>
        /// Check list of Active Directory users
        /// </summary>
        private void CheckAllActiveDirectoryUser()
        {
            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
            {
                foreach (var result in searcher.FindAll())
                {
                    DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                    Debug.WriteLine("First Name: " + de.Properties["givenName"].Value);
                    Debug.WriteLine("Last Name : " + de.Properties["sn"].Value);
                    Debug.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                    Debug.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                }
            }
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
