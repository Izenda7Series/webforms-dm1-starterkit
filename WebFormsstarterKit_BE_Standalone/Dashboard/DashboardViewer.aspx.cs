using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Dashboard
{
    public partial class DashboardViewer : Page
    {
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DashboardViewer));

        #region Properties
        public string DashboardId { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            // Update this part to make Dashboard viewer functional
            // DashboardId = (Page.RouteData.Values["id"] + string.Empty);
            // Logger.DebugFormat("Render Dashboard ID={0}", DashboardId);
        } 
        #endregion
    }
}
