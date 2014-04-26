using DTO.Preferences;
using DTO.System;
using Service.Preferences;
using System;
using System.Web;
using System.Web.UI;
using Utilities;

namespace Web.Site.Preferences
{
    public partial class Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                clearFields();
            }
            
            txtActualPass.Focus();
        }

        private void clearFields()
        {
            txtActualPass.Text = string.Empty;
            txtNewPass.Text = string.Empty;
            txtNewPass2.Text = string.Empty;
        }

        protected void cmdUpdatePassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                updatePassword();
            }
        }

        private void updatePassword()
        {
            try
            {
                PreferencesService preferencesService = new PreferencesService((int)Session[CMSession.memberId]);
                PasswordPreferencesDTO passwordPreferences = new PasswordPreferencesDTO();
                TransactionResult result;

                passwordPreferences.memberId = (int)Session[CMSession.memberId];
                passwordPreferences.actualPassword = txtActualPass.Text;
                passwordPreferences.newPassword = txtNewPass.Text;

                result = preferencesService.updatePasswordPreferences(passwordPreferences);
                if (result.code == TransactionResult.transactionResultCode.Success)
                {
                    InfoMessage.setMessage(HttpContext.GetGlobalResourceObject("Resource", "PreferenciasActualizadas").ToString(), Enums.infoMessageType.Success);
                }
                else
                {
                    showError(HttpContext.GetGlobalResourceObject("Resource", result.failureReason).ToString());
                }

                clearFields();
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }

        private void showError(string message)
        {
            cvUpdatePassword.IsValid = false;
            cvUpdatePassword.ErrorMessage = message;
        }
    }
}