﻿using System;
using System.Collections.Generic;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary.Models
{
    public class QuerySourceCategoryModel
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public Guid ConnectionId { get; set; }

        public List<QuerySourceModel> QuerySources { get; set; }

        public List<QuerySourceCategoryModel> Childs { get; set; }

        public ConnectionModel Connection { get; set; }

        public int PhysicalChange { get; set; }

        public bool Existed { get; set; }

        public bool Deleted { get; set; }

        public bool Checked { get; set; } 
        #endregion
    }
}