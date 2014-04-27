using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTO.System;
using Service.Contacts;
using Entity.Contacts;
using Utilities;

namespace Web.Site.Contacts
{
    public partial class Create : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFirstName.Focus();
        }

        private void createContact()
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                Contact newContact = new Contact();
                TransactionResult result;

                newContact.firstName = txtFirstName.Text;
                newContact.lastName = txtLastName.Text;
                newContact.email = txtEmail.Text;
                newContact.mobileNumber = txtMobileNumber.Text;
                newContact.landlineNumber = txtLandlineNumber.Text;

                result = contactsService.createContact(newContact);
                if (result.code == TransactionResult.transactionResultCode.Success)
                {
                    Response.Redirect(Pages.getContactsDefault(), true);
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

        private void showError(string message)
        {
            cvCreateContact.IsValid = false;
            cvCreateContact.ErrorMessage = message;
        }

        protected void cmdCreateContact_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                createContact();
            }
        }
    }
}