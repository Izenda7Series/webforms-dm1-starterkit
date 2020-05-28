using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebformsIntegratedBE_Standalone.IzendaBoundary.Models;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary
{
    public static class IzendaUtilities
    {
        #region Variables
        private static Dictionary<string, int> _allTenants = new Dictionary<string, int>();
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

        public static Dictionary<string, int> AllTenants
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

        public static Dictionary<string, int> NonSystemTenants
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

        public static Tenant GetTenantById(int id)
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
            var existingTenant = await IzendaUtilities.GetIzendaTenantByName(tenantName, authToken);
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
       
        /// <summary>
        /// Create a user
        /// For more information, please refer to https://www.izenda.com/docs/ref/api_user.html#post-user
        /// ATTN: please don't use this deprecated end point https://www.izenda.com/docs/ref/api_user.html#post-user-integration-saveuser
        /// </summary>
        public static async Task<bool> CreateIzendaUser(string tenant, string userID, string lastName, string firstName, bool isAdmin, string roleName, string authToken)
        {
            var izendaTenant = !string.IsNullOrEmpty(tenant) ? await GetIzendaTenantByName(tenant, authToken) : null;

            var izendaUser = new UserDetail
            {
                Username = userID,
                TenantId = izendaTenant != null ? (Guid?)izendaTenant.Id : null,
                LastName = lastName,
                FirstName = firstName,
                TenantDisplayId = izendaTenant != null ? izendaTenant.Name : string.Empty,
                InitPassword = false,
                SystemAdmin = isAdmin,
                Active = true
            };

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var izendaRole = await CreateRole(roleName, izendaTenant, authToken);
                izendaUser.Roles.Add(izendaRole);
            }

            bool success = await WebAPIService.Instance.PostReturnBooleanAsync("user", izendaUser, authToken);

            return success;
        }

        public static async Task<TenantDetail> GetIzendaTenantByName(string tenantName, string authToken)
        {
            var tenants = await WebAPIService.Instance.GetAsync<IList<TenantDetail>>("/tenant/allTenants", authToken);
            if (tenants != null)
                return tenants.FirstOrDefault(x => x.Name.Equals(tenantName, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }

        /// <summary>
        /// We are not supporting creating role here. TBD
        /// </summary>
        public static async Task<RoleDetail> CreateRole(string roleName, TenantDetail izendaTenant, string authToken)
        {
            var role = await GetIzendaRoleByTenantAndName(izendaTenant != null ? (Guid?)izendaTenant.Id : null, roleName, authToken);

            if (role == null)
            {
                role = new RoleDetail
                {
                    Active = true,
                    Deleted = false,
                    NotAllowSharing = false,
                    Name = roleName,
                    TenantId = izendaTenant != null ? (Guid?)izendaTenant.Id : null
                };

                var response = await WebAPIService.Instance.PostReturnValueAsync<AddRoleResponeMessage, RoleDetail>("role", role, authToken);
                role = response.Role;
            }

            return role;
        }

        private static async Task<RoleDetail> GetIzendaRoleByTenantAndName(Guid? tenantId, string roleName, string authToken)
        {
            var roles = await WebAPIService.Instance.GetAsync<IList<RoleDetail>>("/role/all/" + (tenantId.HasValue ? tenantId.ToString() : null), authToken);

            if (roles != null)
                return roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }
        #endregion
    }
}