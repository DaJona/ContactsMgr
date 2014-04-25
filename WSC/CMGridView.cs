using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSC
{
    [ToolboxData("<{0}:CMGridView runat=server />")]
    public class CMGridView : GridView
    {
        public CMGridView()
        {
            base.GridLines = System.Web.UI.WebControls.GridLines.None;
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            base.CssClass = ("table table-hover table-responsive " + base.CssClass).Trim();

            if (base.Rows.Count > 0)
            {
                // This replaces <td> with <th> and adds the scope attribute
                base.UseAccessibleHeader = true;

                // This will add the <thead> and <tbody> elements
                base.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        /*protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            base.InitializePager(row, columnSpan, pagedDataSource);

            TableCell goToCell = new TableCell();
            goToCell.Style.Add(HtmlTextWriterStyle.Width, "100%");

            Table pagerTable = (Table)row.Cells[0].Controls[0];
            pagerTable.Rows[0].Cells.Clear();
            pagerTable.Rows[0].Cells.Add(goToCell);

            Button btnGo = new Button();
            btnGo.Text = "Go";
            btnGo.CommandName = "Page1";
            btnGo.CommandArgument = "2";
            btnGo.ID = "ctl_PageIndex";
            btnGo.Height = new Unit("16px");
            btnGo.Font.Size = new FontUnit("10px");
            btnGo.CssClass = this.PagerStyle.CssClass;

            goToCell.Controls.Add(btnGo);
        }*/
    }
}
