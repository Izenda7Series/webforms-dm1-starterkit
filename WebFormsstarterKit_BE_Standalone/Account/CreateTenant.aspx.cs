using System;
using System.Web.UI;
using WebformsIntegratedBE_Standalone.IzendaBoundary;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class CreateTenant : Page
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void Submit(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var izendaAdminAuthToken = IzendaTokenAuthorization.GetIzendaAdminToken();

                var isTenantExist = IzendaUtilities.GetTenantByName(TenantName.Text);

                if (isTenantExist == null)
                {
                    var success = await IzendaUtilities.CreateTenant(TenantName.Text, TenantID.Text, izendaAdminAuthToken);

                    if (success)
                    {
                        var newTenant = new Tenant() { Name = TenantName.Text };
                        await IzendaUtilities.SaveTenantAsync(newTenant);
                       
                        RedirectResultPage(true);
                    }
                    else // failed at server level
                    {
                        RedirectResultPage(false);
                    }
                }
                else
                    RedirectResultPage(false);
            }
            else
                RedirectResultPage(false);
        }

        private void RedirectResultPage(bool success)
        {
            if (success)
                Response.Redirect("~/Account/CreateTenantSuccess.aspx");
            else
                Response.Redirect("~/Account/FailedToCreateTenant.aspx", false);
        }
        #endregion
    }
}
