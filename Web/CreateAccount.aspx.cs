using DTO.System;
using Entity.Members;
using Service.Members;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using Utilities;

namespace Web
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtRealName.Focus();
        }

        protected override void InitializeCulture()
        {
            string lang = Request["ddlPageLanguage"] == null ? "es-co" : Request["ddlPageLanguage"];
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    MembersService membersService = new MembersService(ddlPageLanguage.SelectedValue);
                    Member enMember = new Member();
                    TransactionResult result;

                    enMember.realName = txtRealName.Text;
                    enMember.displayName = txtDisplayName.Text;
                    enMember.email = txtEmail.Text;
                    enMember.password = txtPass1.Text;
                    enMember.language = ddlLanguage.SelectedValue;

                    result = membersService.createMember(enMember);
                    if (result.code == TransactionResult.transactionResultCode.Success)
                    {
                        EMail.sendAccountActivationEmail();
                    }
                    else
                    {
                        showError(result.failureReason);
                    }
                }
                catch (Exception)
                {
                    showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
                }
            }
        }

        private void showError(string message)
        {
            cvCreateAccount.IsValid = false;
            cvCreateAccount.ErrorMessage = message;
        }
    }
}