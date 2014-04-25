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

            if (Session[CMSession.memberLang] != null)
                lang = Session[CMSession.memberLang].ToString();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }
    }
}