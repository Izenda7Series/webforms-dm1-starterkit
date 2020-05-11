using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Report
{
    public partial class ReportViewer : Page
    {
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ReportViewer));

        #region Properties
        public string ReportId { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportId = (Page.RouteData.Values["id"] + string.Empty);
            //Logger.DebugFormat("Render Report ID={0}", ReportId);
        } 
        #endregion
    }
}
