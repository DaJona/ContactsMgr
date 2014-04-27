using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Site.WUC
{
    public partial class LanguagesList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SelectedValue
        {
            get { return ddlLanguages.SelectedValue; }
            set { ddlLanguages.SelectedValue = value; }
        }

        public string CssClass
        {
            get { return ddlLanguages.CssClass; }
            set { ddlLanguages.CssClass = value; }
        }
    }
}