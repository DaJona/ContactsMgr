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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.hasActiveSession)
            {
                if (Request.QueryString[Parameters.redirectTo] != null)
                    Response.Redirect(Request.QueryString[Parameters.redirectTo].ToString(), true);

                Response.Redirect(Pages.getHomeDefault(), true);
            }

            txtEmail.Focus();
        }

        protected override void InitializeCulture()
        {
            string lang = Request["ddlPageLanguage"] == null ? "es-co" : Request["ddlPageLanguage"];
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }

        #region Properties

        protected string timeZone
        {
            set
            {
                timeZoneOffset.Value = value;
            }
            get
            {
                return timeZoneOffset.Value;
            }
        }

        #endregion

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    MembersService membersService = new MembersService(ddlPageLanguage.SelectedValue);
                    TransactionResult result;
                    
                    result = membersService.login(txtEmail.Text, txtPass.Text);
                    if (result.code == TransactionResult.transactionResultCode.Success)
                    {
                        Member logedMember = (Member)result.object1;
                        SessionManager.create(logedMember.id, logedMember.displayName, logedMember.language, Convert.ToInt32(timeZoneOffset.Value));

                        if (Request.QueryString[Parameters.redirectTo] != null)
                            Response.Redirect(Request.QueryString[Parameters.redirectTo].ToString(), true);

                        Response.Redirect(Pages.getHomeDefault(), true);
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
            cvLogin.IsValid = false;
            cvLogin.ErrorMessage = message;
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}