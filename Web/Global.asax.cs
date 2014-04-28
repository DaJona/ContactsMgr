using Service.Contacts;
using System;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Utilities;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ValidationSettings.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            AssignBusinessLogicLayerHelpers();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private static void AssignBusinessLogicLayerHelpers()
        {
            var context = HttpContext.Current;
            if (context == null) return;

            var page = context.Handler as Page; if (page == null) return;

            // Assign the ResolveUrl method 
            if (Pages.ResolveUrl == null)
            {
                Pages.ResolveUrl = page.ResolveUrl;
            }

            // Assign the ServerMapPath method 
            if (SitePaths.ServerMapPath == null)
            {
                SitePaths.ServerMapPath = page.Server.MapPath;
            }
            if (ContactsService.ServerMapPath == null)
            {
                ContactsService.ServerMapPath = page.Server.MapPath;
            }
        } 
    }
}