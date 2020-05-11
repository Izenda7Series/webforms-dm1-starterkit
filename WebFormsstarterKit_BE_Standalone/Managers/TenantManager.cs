using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Managers
{
    public class TenantManager
    {
        #region Variables
        private Dictionary<string, int> _allTenants = new Dictionary<string, int>();
        #endregion

        #region Methods
        public Tenant GetTenantByName(string name)
        {
            using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
            {
                var tenant = context.Tenants.Where(x => x.Name.Equals((name == null || name == string.Empty ? "System" : name), StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

                return tenant;
            }
        }

        public Dictionary<string, int> AllTenants
        {
            get
            {
                if (_allTenants.Count > 0)
                    return _allTenants;
                using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
                {
                    _allTenants = context.Tenants.Select(x => x).ToDictionary(i => i.Name, i => i.Id);
                }
                return _allTenants;
            }
        }

        public Dictionary<string, int> NonSystemTenants
        {
            get
            {
                return AllTenants.Select(t => t).Where(x => !IsSystemTenant(x.Key)).ToDictionary(i => i.Key, i => i.Value);
            }
        }

        public static bool IsSystemTenant(string tenantName)
        {
            return string.IsNullOrEmpty(tenantName) ? true : tenantName.Equals("System", StringComparison.InvariantCultureIgnoreCase);
        }

        public Tenant GetTenantById(int id)
        {
            using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
            {
                var tenant = context.Tenants.Where(x => x.Id == id).SingleOrDefault();

                return tenant;
            }
        }

        public async Task<Tenant> SaveTenantAsync(Tenant tenant)
        {
            using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
            {
                context.Tenants.Add(tenant);
                await context.SaveChangesAsync();

                return tenant;
            }
        } 
        #endregion
    }
}