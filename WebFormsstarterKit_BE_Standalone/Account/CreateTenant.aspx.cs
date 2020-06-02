using System;
using System.Web.UI;
using WebformsIntegratedBE_Standalone.IzendaBoundary;
using WebformsIntegratedBE_Standalone.Models;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class CreateTenant : Page
    {
        #region Variables
        public static bool? createdSuccessfully = null;
        public static bool serverError = false;
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // back to default status
                createdSuccessfully = null;
                serverError = false;
            }
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

                        createdSuccessfully = true;
                    }
                    else // failed at server level
                    {
                        createdSuccessfully = null;
                        serverError = true;
                    }
                }
                else
                    createdSuccessfully = false;
            }
            else
                createdSuccessfully = false;
        } 
        #endregion
    }
}
