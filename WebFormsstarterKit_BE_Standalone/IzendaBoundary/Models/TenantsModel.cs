using System;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary.Models
{
    public class TenantsModel
    {
        #region Properties
        public Guid Id { get; set; }

        public string TenantID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; } 
        #endregion
    }
}