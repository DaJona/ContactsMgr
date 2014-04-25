using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSC
{
    [ToolboxData("<{0}:CMCompareValidator runat=server />")]
    public class CMCompareValidator : CompareValidator
    {
        public CMCompareValidator()
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            base.Text = "*";
            base.Display = ValidatorDisplay.Dynamic;
            base.ForeColor = Color.Red;
            base.Font.Size = FontUnit.Parse("16");
        }
    }
}
