using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace WebformsIntegratedBE_Standalone
{
    public static class RouteConfig
    {
        #region Methods
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Ignore("api/{*pathInfo}");

            routes.MapPageRoute(
                routeName: "ReportPart",
                routeUrl: "viewer/reportpart/{id}",
                physicalFile: "~/Report/ReportPart.aspx",
                checkPhysicalUrlAccess: false
            );

            routes.MapPageRoute(
                routeName: "ReportViewer",
                routeUrl: "report/view/",
                physicalFile: "~/report/ReportViewer.aspx",
                checkPhysicalUrlAccess: false
            );

            routes.MapPageRoute(
                routeName: "DashboardViewer",
                routeUrl: "dashboard/view/",
                physicalFile: "~/Dashboard/DashboardViewer.aspx",
                checkPhysicalUrlAccess: false
            );

            var settings = new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            };
            routes.EnableFriendlyUrls(settings);
        } 
        #endregion
    }
}
