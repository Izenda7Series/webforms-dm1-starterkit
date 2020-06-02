using System;
using System.Web;

namespace WebformsIntegratedBE_Standalone
{
    public static class IdentityHelper
    {
        #region Methods
        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!string.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
                response.Redirect(returnUrl);
            else
                response.Redirect("~/");
        } 
        #endregion
    }
}
