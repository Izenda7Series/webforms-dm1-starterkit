using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Linq;
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

    public class ApplicationUserDbContext<TUser> : IdentityDbContext<TUser> where TUser : ApplicationUser
    {
        #region Properties
        public virtual IDbSet<Tenant> Tenants { get; set; } 
        #endregion

        #region CTOR
        public ApplicationUserDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        { }
        #endregion

        #region Methods
        public static ApplicationUserDbContext<TUser> Create()
        {
            return new ApplicationUserDbContext<TUser>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var user = modelBuilder.Entity<TUser>();

            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 1 }));
            user.Property(u => u.Tenant_Id)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 2 }));
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as TUser;

                if (user != null)
                {
                    if (this.Users.Any(u => string.Equals(u.UserName, user.UserName)
                      && u.Tenant_Id == user.Tenant_Id))
                    {
                        errors.Add(new DbValidationError("User",
                          string.Format("Username {0} is already taken for tenant {1}",
                            user.UserName, user.Tenant_Id)));
                    }

                    if (this.RequireUniqueEmail
                      && this.Users.Any(u => string.Equals(u.Email, user.Email)
                      && u.Tenant_Id == user.Tenant_Id))
                    {
                        errors.Add(new DbValidationError("User",
                          string.Format("Email Address {0} is already taken for tenant {1}",
                            user.UserName, user.Tenant_Id)));
                    }
                }
                else
                {
                    var role = entityEntry.Entity as IdentityRole;

                    if (role != null && this.Roles.Any(r => string.Equals(r.Name, role.Name)))
                    {
                        errors.Add(new DbValidationError("Role",
                          string.Format("Role {0} already exists", role.Name)));
                    }
                }
                if (errors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }

            return new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
        } 
        #endregion
    }
}
