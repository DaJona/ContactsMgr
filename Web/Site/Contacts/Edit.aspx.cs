using DTO.System;
using Entity.Contacts;
using Service.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Web.Site.Contacts
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString[Parameters.id] != null)
                {
                    loadContact(Convert.ToInt32(Request.QueryString[Parameters.id]));
                }
                else
                {
                    Response.Redirect(Pages.getContactsDefault(), true);
                }
            }            
        }

        private void loadContact(int conctactId)
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                Contact existingContact = contactsService.getContact(conctactId);

                txtFirstName.Text = existingContact.firstName;
                txtLastName.Text = existingContact.lastName;
                txtEmail.Text = existingContact.email;
                txtMobileNumber.Text = existingContact.mobileNumber;
                txtLandlineNumber.Text = existingContact.landlineNumber;
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }

        private void editContact()
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                Contact editedContact = new Contact();
                TransactionResult result;

                editedContact.id = Convert.ToInt32(Request.QueryString[Parameters.id]);
                editedContact.firstName = txtFirstName.Text;
                editedContact.lastName = txtLastName.Text;
                editedContact.email = txtEmail.Text;
                editedContact.mobileNumber = txtMobileNumber.Text;
                editedContact.landlineNumber = txtLandlineNumber.Text;

                result = contactsService.editContact(editedContact);
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
            cvEditContact.IsValid = false;
            cvEditContact.ErrorMessage = message;
        }

        protected void cmdEditContact_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                editContact();
            }
        }
    }
}