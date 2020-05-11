using System.Collections.Generic;

namespace WebformsIntegratedBE_Standalone.Models
{
    public class ReportPartFilter
    {
        #region Properties
        public string key { get; set; }

        public bool linkReportFilter { get; set; }

        public string value { get; set; } 
        #endregion
    }

    public class ReportPartModel
    {
        #region Properties
        public string id { get; set; }

        public List<ReportPartFilter> filters { get; set; } 
        #endregion
    }

    public class ReportPartViewModel
    {
        #region Properties
        public List<ReportPartModel> ReportParts { get; set; } 
        #endregion
    }
}
