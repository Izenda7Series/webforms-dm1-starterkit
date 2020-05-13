using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Dashboard
{
    public partial class DashboardViewer : Page
    {
        #region Contants
        // For demonstration purpose, we can assign id here
        private static readonly string DEMO_DASHBOARDID = string.Empty; 
        #endregion

        #region Properties
        public string DashboardId { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e) => DashboardId = DEMO_DASHBOARDID;
        #endregion
    }
}
