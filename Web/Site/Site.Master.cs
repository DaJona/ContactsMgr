using System;
using System.Web;
using System.Web.UI;
using Utilities;

namespace Web.Site
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.hasActiveSession)
                Response.Redirect(Pages.getLogin() + "?RedirectTo=" + HttpUtility.HtmlEncode(Request.Url.ToString()), true);

            lblDisplayName.Text = SessionManager.sessionMemberInfo.name;
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(Pages.getLogin(), true);
        }

        protected void lnkContacts_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getContactsDefault(), true);
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getHomeDefault(), true);
        }

        protected void lnkPreferences_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getPreferencesDefault(), true);
        }
    }
}