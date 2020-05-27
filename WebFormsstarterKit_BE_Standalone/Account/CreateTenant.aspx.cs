using System;
using System.Web.UI;
using WebformsIntegratedBE_Standalone.Managers;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class CreateTenant : Page
    {
        #region Variables
        public static bool? createdSuccess = null;
        public static bool serverError = false;
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // back to default status
                createdSuccess = null;
                serverError = false;
            }
        }

        protected async void Submit(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var izendaAdminAuthToken = IzendaBoundary.IzendaTokenAuthorization.GetIzendaAdminToken();

                var isTenantExist = TenantManager.GetTenantByName(TenantName.Text);

                if (isTenantExist == null)
                {
                    var success = await TenantManager.CreateTenant(TenantName.Text, TenantID.Text, izendaAdminAuthToken);

                    if (success)
                    {
                        var newTenant = new Tenant() { Name = TenantName.Text };
                        await TenantManager.SaveTenantAsync(newTenant);

                        createdSuccess = true;
                    }
                    else // failed at server level
                    {
                        createdSuccess = null;
                        serverError = true;
                    }
                }
                else
                    createdSuccess = false;
            }
            else
                createdSuccess = false;
        } 
        #endregion
    }
}