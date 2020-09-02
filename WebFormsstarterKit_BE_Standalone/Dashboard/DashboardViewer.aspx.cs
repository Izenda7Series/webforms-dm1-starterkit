using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Dashboard
{
    public partial class DashboardViewer : Page
    {
        #region Properties
        public string DashboardId { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e) 
        {
            DashboardId = Page.RouteData.Values["id"] as string;
        }
        #endregion
    }
}
