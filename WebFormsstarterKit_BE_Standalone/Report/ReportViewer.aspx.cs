using System;
using System.Web.UI;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebformsIntegratedBE_Standalone.Report
{
    public partial class ReportViewer : Page
    {
        #region Properties
        public string ReportId { get; set; }

        public IHtmlString OverridingFilterQueries { get; set; }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e) 
        {
            var queryString = Request.QueryString;
            dynamic filters = new System.Dynamic.ExpandoObject();
            foreach (string key in queryString.AllKeys)
            {
                ((IDictionary<string, object>)filters).Add(key, queryString[key]);
            }

            var queries = JsonConvert.SerializeObject(filters);
            var decoded = System.Net.WebUtility.HtmlDecode(queries);

            OverridingFilterQueries = HTMLRaw(decoded);
            ReportId = Page.RouteData.Values["id"] as string;
        }

        public static IHtmlString HTMLRaw(string source)
        {
            return new HtmlString(source);
        }
        #endregion
    }
}
