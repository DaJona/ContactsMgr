using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSC
{
    [ToolboxData("<{0}:CMRequiredFieldValidator runat=server />")]
    public class CMRequiredFieldValidator : RequiredFieldValidator
    {
        public CMRequiredFieldValidator()
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
