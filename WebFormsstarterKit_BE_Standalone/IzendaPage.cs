﻿using System;
using System.Web.UI;

namespace WebformsIntegratedBE_Standalone
{
    public class IzendaPage : Page
    {
        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            // Let handle redirect from izneda.integrate.js if not, please use following lines
            //if (Page.User != null && Page.User.Identity != null && !Page.User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("~/Account/Login");
            //}
            base.OnLoad(e);
        } 
        #endregion
    }
}
