using System;
using Utilities;

namespace Web.Site.Preferences
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getPreferencesAccount().ToString());
        }
    }
}