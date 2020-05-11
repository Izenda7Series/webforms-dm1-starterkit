using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone.Report
{
    public partial class ReportPart : Page
    {
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ReportPart));

        #region Properties
        public string ReportId { get; set; }

        public string Token { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportId = (Page.RouteData.Values["id"] + string.Empty);
            Token = Request.QueryString["token"];

            //Logger.DebugFormat("Render report part id={0}, token={1}", ReportId, Token);
        } 
        #endregion
    }
}
