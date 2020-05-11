using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebformsIntegratedBE_Standalone.Startup))]
namespace WebformsIntegratedBE_Standalone
{
    public partial class Startup
    {
        #region Methods
        public void Configuration(IAppBuilder app) => ConfigureAuth(app);
        #endregion
    }
}
