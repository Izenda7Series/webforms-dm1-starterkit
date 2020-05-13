using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Report
{
    public partial class ReportViewer : Page
    {
        #region Constants
        // For demonstration purpose, we can assign id here
        private static readonly string DEMO_REPORTID = string.Empty; 
        #endregion

        #region Properties
        public string ReportId { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e) => ReportId = DEMO_REPORTID;
        #endregion
    }
}
