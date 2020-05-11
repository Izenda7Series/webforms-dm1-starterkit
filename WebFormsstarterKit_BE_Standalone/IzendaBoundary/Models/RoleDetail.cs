using System;
using System.Collections.Generic;
using WebformsIntegratedBE_Standalone.IzendaBoundary.Models.Permissions;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary.Models
{
    public class RoleDetail
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? TenantId { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public bool NotAllowSharing { get; set; }

        public string TenantUniqueName { get; set; }

        public Permission Permission { get; set; }

        public List<QuerySourceModel> VisibleQuerySources { get; set; }

        public List<UserDetail> Users { get; set; }
        #endregion

        #region CTOR
        public RoleDetail()
        {
            Users = new List<UserDetail>();
        } 
        #endregion
    }
}