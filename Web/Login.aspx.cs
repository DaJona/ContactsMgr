using System;
using System.Web.UI;
using Entity.Members;
using Service.Members;
using DTO.System;
using System.Web;
using Utilities;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CMSession.memberId] != null)
            {
                if (Request.QueryString[Parameters.redirectTo] != null)
                    Response.Redirect(Request.QueryString[Parameters.redirectTo].ToString(), true);

                Response.Redirect(Pages.getHomeDefault(), true);
            }

            txtEmail.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    MembersService membersService = new MembersService();
                    TransactionResult result;
                    
                    result = membersService.login(txtEmail.Text, txtPass.Text);
                    if (result.code == TransactionResult.transactionResultCode.Success)
                    {
                        Member logedMember = (Member)result.object1;

                        Session[CMSession.memberId] = logedMember.id;
                        Session[CMSession.memberName] = logedMember.displayName;
                        Session[CMSession.memberLang] = logedMember.language;

                        if (Request.QueryString[Parameters.redirectTo] != null)
                            Response.Redirect(Request.QueryString[Parameters.redirectTo].ToString(), true);

                        Response.Redirect(Pages.getHomeDefault(), true);
                    }
                    else
                    {
                        showError(HttpContext.GetGlobalResourceObject("Resource", result.failureReason).ToString());
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
    }
}