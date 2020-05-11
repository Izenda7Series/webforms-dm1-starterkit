using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebformsIntegratedBE_Standalone.IzendaBoundary.Models;
using WebformsIntegratedBE_Standalone.IzendaBoundary.Models.Permissions;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary
{
    public class IzendaUtility
    {
        #region Methods
        public static async Task<IList<ConnectionModel>> GetConnections(Guid? tenantId, string authToken)
        {
            string action = tenantId == null ? "connection" : "connection/" + tenantId.ToString();

            var connections = await WebAPIService.Instance.GetAsync<IList<ConnectionModel>>(action, authToken);

            return connections;
        }

        public static async Task<ConnectionDetail> GetConnectionDetail(Guid connectionId, string authToken)
        {
            var action = "connection/detail/" + connectionId.ToString();

            var connection = await WebAPIService.Instance.GetAsync<ConnectionDetail>(action, authToken);

            return connection;
        }

        public static async Task UpdateConnectionDetail(ConnectionModel payload, string authToken)
        {
            var action = "connection";

            await WebAPIService.Instance.PostAsync(action, payload, authToken);
        }

        public static async Task SaveRole(RoleDetail payload, string authToken)
        {
            var action = "role/intergration/saveRole";
            //string action = "role";
            await WebAPIService.Instance.PostAsync(action, payload, authToken);
        }

        public static async Task<IList<TenantsModel>> GetTenants(string authToken)
        {
            var action = "tenant/allTenants";

            var tenants = await WebAPIService.Instance.GetAsync<IList<TenantsModel>>(action, authToken);

            return tenants;
        }

        public static async Task<IList<RoleDetail>> GetRoles(Guid? tenantId, string authToken)
        {
            var action = tenantId == null ? "role/all" : "role/all/" + tenantId.ToString();

            var roles = await WebAPIService.Instance.GetAsync<IList<RoleDetail>>(action, authToken);

            return roles;
        }

        public static async Task<IList<UserDetail>> GetUsers(Guid? tenantId, string authToken)
        {
            var action = tenantId == null ? "user/all" : "user/all/" + tenantId.ToString();

            var users = await WebAPIService.Instance.GetAsync<IList<UserDetail>>(action, authToken);

            return users;
        }

        public static async Task<RoleDetail> GetRole(Guid roleId, string authToken)
        {
            var action = "role/" + roleId.ToString();

            var role = await WebAPIService.Instance.GetAsync<RoleDetail>(action, authToken);

            return role;
        }

        public static async Task<Permission> GetPermission(string authToken)
        {
            var action = "authorization/permission";

            var permission = await WebAPIService.Instance.GetAsync<Permission>(action, authToken);

            return permission;
        }

        public static async Task<ConnectionModel> ReloadRemoteSchema(object payload, string authToken)
        {
            var action = "connection/reloadRemoteSchema";

            return await WebAPIService.Instance.PostReturnValueAsync<ConnectionModel, object>(action, payload, authToken);
        }

        public static async Task CreateTenant(string tenantName, string authToken)
        {
            var existingTenant = await GetIzendaTenantByName(tenantName, authToken);

            if (existingTenant != null)
                return;

            var tenantDetail = new TenantDetail
            {
                Active = true,
                Disable = false,
                Name = tenantName.Equals("System") ? null : tenantName,
                TenantId = tenantName.Equals("System") ? null : tenantName
            };

            await WebAPIService.Instance.PostAsync("tenant", tenantDetail, authToken);
        }

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

        /// <summary>
        /// Adds the user to the Izenda database
        /// See the link below for more details:
        /// https://www.izenda.com/docs/ref/api_user.html?highlight=user%20integration#post-user-integration-saveuser
        /// </summary>
        public static async Task<bool> CreateIzendaUser(WebformsIntegratedBE_Standalone.Models.ApplicationUser appUser, string roleName, string authToken)
        {
            var izendaTenant = appUser.Tenant != null ? await GetIzendaTenantByName(appUser.Tenant.Name, authToken) : null;

            var izendaUser = new UserDetail
            {
                FirstName = izendaTenant != null ? izendaTenant.Name : "Admin",
                LastName = appUser.UserName.Split('@')[0],
                Username = appUser.UserName,
                TenantDisplayId = izendaTenant != null ? izendaTenant.Name : string.Empty,
                TenantId = izendaTenant != null ? (Guid?)izendaTenant.Id : null,
                InitPassword = true,
                SystemAdmin = izendaTenant == null,
                Active = true
            };

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var izendaRole = await CreateRole(roleName, izendaTenant, authToken);
                izendaUser.Roles.Add(izendaRole);
            }

            bool success = await WebAPIService.Instance.PostReturnValueAsync<bool, UserDetail>("user/integration/saveUser", izendaUser, authToken);

            return success;
        }

        [Obsolete("This method is deprecated, please use CreateIzendaUser instead")]
        public static async Task<UserDetail> CreateUser(WebformsIntegratedBE_Standalone.Models.ApplicationUser hostingUser, string roleName, string authToken)
        {
            var izendaTenant = hostingUser.Tenant != null ? await GetIzendaTenantByName(hostingUser.Tenant.Name, authToken) : null;

            var izendaUser = new UserDetail
            {
                FirstName = izendaTenant != null ? izendaTenant.Name : string.Empty,
                LastName = hostingUser.UserName.Split('@')[0],
                Username = hostingUser.UserName,
                TenantDisplayId = izendaTenant != null ? izendaTenant.Name : string.Empty,
                TenantId = izendaTenant != null ? (Guid?)izendaTenant.Id : null
            };

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var izendaRole = await CreateRole(roleName, izendaTenant, authToken);
                izendaUser.Roles.Add(izendaRole);
            }

            izendaUser = await WebAPIService.Instance.PostReturnValueAsync<UserDetail, UserDetail>("user", izendaUser, authToken);

            return izendaUser;
        }

        private static async Task<RoleDetail> GetIzendaRoleByTenantAndName(Guid? tenantId, string roleName, string authToken)
        {
            var roles = await WebAPIService.Instance.GetAsync<IList<RoleDetail>>("/role/all/" + (tenantId.HasValue ? tenantId.ToString() : null), authToken);

            if (roles != null)
                return roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

            return null;
        }

        private static async Task<TenantDetail> GetIzendaTenantByName(string tenantName, string authToken)
        {
            if (tenantName.Equals("System"))
                return null;

            var tenants = await WebAPIService.Instance.GetAsync<IList<TenantDetail>>("/tenant/allTenants", authToken);

            if (tenants != null)
                return tenants.FirstOrDefault(x => x.Name.Equals(tenantName, StringComparison.InvariantCultureIgnoreCase));

            return null;
        } 
        #endregion
    }
}
