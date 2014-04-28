using System.Globalization;
using System.Threading;
using Utilities;

namespace Web.Site
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string lang = "es-es";

            if (SessionManager.sessionMemberInfo == null)
                Response.Redirect(Pages.getLogin(), true);

            if (SessionManager.sessionMemberInfo.lang != string.Empty)
                lang = SessionManager.sessionMemberInfo.lang;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }
    }
}