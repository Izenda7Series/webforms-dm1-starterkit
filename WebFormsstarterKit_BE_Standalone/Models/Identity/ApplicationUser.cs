using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebformsIntegratedBE_Standalone.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        #region Properties
        /// <summary>
        /// Property Tenant_Id can be nullable - SystemAdmin case
        /// </summary>
        public int? Tenant_Id { get; set; }
        [ForeignKey("Tenant_Id")]

        public Tenant Tenant { get; set; }
        #endregion

        #region Methods
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            if (Tenant != null)
            {
                userIdentity.AddClaims(new[] {
                    new Claim("tenantName",Tenant.Name),
                    new Claim("tenantId",Tenant.Id.ToString()),
                });
            }

            var role = (await manager.GetRolesAsync(this.Id)).FirstOrDefault();
            userIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, role ?? string.Empty));

            // Add custom user claims here
            return userIdentity;
        }
        #endregion
    }
}
