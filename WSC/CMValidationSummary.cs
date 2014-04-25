using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSC
{
    [ToolboxData("<{0}:CMValidationSummary runat=server />")]
    public class CMValidationSummary : ValidationSummary
    {
        public CMValidationSummary()
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            base.CssClass = ("alert alert-danger " + base.CssClass).Trim();
        }
    }
}
