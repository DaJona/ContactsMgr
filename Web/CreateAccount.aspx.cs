using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Members;
using DTO.System;
using Service.Members;

namespace Web
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtRealName.Focus();
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    MembersService membersService = new MembersService();
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

                    }
                    else
                    {
                        showError(HttpContext.GetGlobalResourceObject("Resource", result.failureReason).ToString());
                    }
                }
                catch (Exception ex)
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