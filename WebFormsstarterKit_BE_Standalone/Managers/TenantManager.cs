using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebformsIntegratedBE_Standalone.IzendaBoundary.Models;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Managers
{
    public class TenantManager
    {
        #region Variables
        private Dictionary<string, int> _allTenants = new Dictionary<string, int>();
        #endregion

        #region Methods
        public static Tenant GetTenantByName(string name)
        {
            using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
            {
                var tenant = context.Tenants.Where(x => x.Name.Equals((name == null || name == string.Empty ? "System" : name), StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

                return tenant;
            }
        }

        public static IEnumerable<string> GetAllTenant()
        {
            using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
            {
                var tenantList = new List<string>();

                foreach (var tenant in context.Tenants)
                {
                    // excludes System tenant
                    if (!tenant.Name.Equals("System", StringComparison.InvariantCultureIgnoreCase))
                        tenantList.Add(tenant.Name);
                }

                return tenantList;
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

        public static async Task<Tenant> SaveTenantAsync(Tenant tenant)
        {
            using (var context = ApplicationUserDbContext<ApplicationUser>.Create())
            {
                context.Tenants.Add(tenant);
                await context.SaveChangesAsync();

                return tenant;
            }
        }

        /// <summary>
        /// Create a tenant
        /// For more information, please refer to https://www.izenda.com/docs/ref/api_tenant.html#tenant-apis
        /// </summary>
        public static async Task<bool> CreateTenant(string tenantName, string tenantId, string authToken)
        {
            var existingTenant = await GetIzendaTenantByName(tenantName, authToken);
            if (existingTenant != null)
                return false;

            var tenantDetail = new TenantDetail
            {
                Active = true,
                Disable = false,
                Name = tenantName,
                TenantId = tenantId
            };

            // For more information, please refer to https://www.izenda.com/docs/ref/api_tenant.html#post-tenant
            return await IzendaBoundary.WebAPIService.Instance.PostReturnBooleanAsync("tenant", tenantDetail, authToken);
        }

        private static async Task<TenantDetail> GetIzendaTenantByName(string tenantName, string authToken)
        {
            var tenants = await IzendaBoundary.WebAPIService.Instance.GetAsync<IList<TenantDetail>>("/tenant/allTenants", authToken);
            if (tenants != null)
                return tenants.FirstOrDefault(x => x.Name.Equals(tenantName, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }
        #endregion
    }
}