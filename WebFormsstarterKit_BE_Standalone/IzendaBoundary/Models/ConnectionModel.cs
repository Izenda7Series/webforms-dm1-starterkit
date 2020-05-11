﻿using System;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary.Models
{
    public class ConnectionModel
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ServerTypeId { get; set; }

        public string ServerTypeName { get; set; }

        public string ConnectionString { get; set; }

        public bool Visible { get; set; }

        public bool Deleted { get; set; }

        public Guid? RelateToConnectionId { get; set; }

        public Guid? TenantId { get; set; }

        public DBSourceModel DBSource { get; set; }

        public int PhysicalChange { get; set; }

        public bool Checked { get; set; }

        public string DatabaseName { get; set; } 
        #endregion
    }
}