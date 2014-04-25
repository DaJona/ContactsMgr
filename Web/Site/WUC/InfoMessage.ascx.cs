using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Web.Site.WUC
{
    public partial class InfoMessage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialize();
            }
        }

        private void initialize()
        {
            titleContainer.Text = string.Empty;
            messageContainer.Text = string.Empty;
            divMessageContainer.Style["display"] = "none";
        }

        public void setMessage(string message, Enums.infoMessageType infoMessageType)
        {
            switch (infoMessageType)
            {
                case Enums.infoMessageType.Informative:
                    titleContainer.Text = "";
                    divMessageContainer.Attributes["class"] = "alert alert-info";
                    break;
                case Enums.infoMessageType.Warning:
                    titleContainer.Text = HttpContext.GetGlobalResourceObject("Resource", "AtenciónExclamacion").ToString();
                    divMessageContainer.Attributes["class"] = "alert alert-warning";
                    break;
                case Enums.infoMessageType.Success:
                    titleContainer.Text = HttpContext.GetGlobalResourceObject("Resource", "GenialExclamacion").ToString();
                    divMessageContainer.Attributes["class"] = "alert alert-success";
                    break;
                case Enums.infoMessageType.Error:
                    titleContainer.Text = HttpContext.GetGlobalResourceObject("Resource", "RayosExclamacion").ToString();
                    divMessageContainer.Attributes["class"] = "alert alert-error";
                    break;
            }

            messageContainer.Text = message;
            divMessageContainer.Style["display"] = "block";
        }
    }
}