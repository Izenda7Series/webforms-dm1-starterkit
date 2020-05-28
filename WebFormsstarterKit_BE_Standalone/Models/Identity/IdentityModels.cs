using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace WebformsIntegratedBE_Standalone.Models
{
    public class ApplicationUserStore<TUser> : UserStore<TUser> where TUser : ApplicationUser
    {
        #region Properties
        public int TenantId { get; set; } 
        #endregion

        #region CTOR
        public ApplicationUserStore(DbContext context) : base(context)
        { }
        #endregion

        #region Methods
        public override Task CreateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            
            TenantId = user.Tenant_Id.Value;
            
            return base.CreateAsync(user);
        }

        public override Task<TUser> FindByEmailAsync(string email)
        {
            return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper() && u.Tenant_Id == TenantId);
        }

        public override Task<TUser> FindByNameAsync(string userName)
        {
            return GetUserAggregateAsync(u => u.UserName.ToUpper() == userName.ToUpper() && u.Tenant_Id == TenantId);
        } 
        #endregion
    }

    /// <summary>
    /// Client DB Tenant
    /// </summary>
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
