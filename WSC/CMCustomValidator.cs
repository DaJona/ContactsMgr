using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSC
{
    [ToolboxData("<{0}:CMCustomValidator runat=server />")]
    public class CMCustomValidator : CustomValidator
    {
        public CMCustomValidator()
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            base.Display = ValidatorDisplay.None;
        }
    }
}
