using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformsIntegratedBE_Standalone.Managers;

namespace WebformsIntegratedBE_Standalone.Account
{
    public partial class CreateUser : Page
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TenantList.DataSource = TenantManager.GetAllTenant();
                TenantList.DataBind();
            }
        }
        
        protected async void Submit(object sender, EventArgs e)
        {
            var test = TenantList.SelectedValue;
        }

        protected void TenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TenantList.SelectedValue))
                CheckBoxDiv.Visible = true;
            else
                CheckBoxDiv.Visible = false;
        }
        #endregion
    }
}
