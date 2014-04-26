using System;
using Utilities;

namespace Web.Site.WUC
{
    public partial class PreferencesLeftSidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Properties

        public Enums.preferencesMenuItems ActiveItem
        {
            set
            {
                hiddenActiveItem.Value = value.ToString();
            }
        }

        #endregion

        protected void lnkAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getPreferencesAccount().ToString(), true);
        }

        protected void lnkPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getPreferencesPassword().ToString(), true);
        }
    }
}