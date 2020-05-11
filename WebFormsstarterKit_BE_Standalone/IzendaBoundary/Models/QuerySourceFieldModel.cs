using System;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary.Models
{
    public class QuerySourceFieldModel
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string DataType { get; set; }

        public string IzendaDataType { get; set; }

        public bool AllowDistinct { get; set; }

        public bool Visible { get; set; }

        public bool Filterable { get; set; } 
        #endregion
    }
}