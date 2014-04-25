using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSC
{
    [ToolboxData("<{0}:CMButton runat=server />")]
    public class CMButton : Button
    {
        public CMButton()
        {
            
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            base.CssClass = ("btn btn-primary " + base.CssClass).Trim();
        }
    }
}
