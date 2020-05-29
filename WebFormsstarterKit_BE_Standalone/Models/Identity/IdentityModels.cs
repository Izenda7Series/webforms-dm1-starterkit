using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebformsIntegratedBE_Standalone.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Properties
        public virtual IDbSet<Tenant> Tenants { get; set; } 
        #endregion

        #region CTOR
        public ApplicationDbContext() 
            : base("DefaultConnection", throwIfV1Schema: false)
        { }
        #endregion

        #region Methods
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        #endregion
    }
}
