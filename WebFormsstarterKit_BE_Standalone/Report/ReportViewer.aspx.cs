using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Report
{
    public partial class ReportViewer : Page
    {
        #region Properties
        public string ReportId { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e) 
        {
            ReportId = (Page.RouteData.Values["id"] + string.Empty);
        }
        #endregion
    }
}
