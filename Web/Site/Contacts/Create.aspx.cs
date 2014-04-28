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

                // Variables to manage the contact pic file
                string fileExtension = "";
                int fileSizeInBytes = 0;

                if (uplContactPic.HasFile)
                {
                    fileExtension = System.IO.Path.GetExtension(uplContactPic.FileName);
                    fileSizeInBytes = uplContactPic.PostedFile.ContentLength;

                    if (!FilesValidations.isValidContactPic(fileExtension, fileSizeInBytes))
                    {
                        showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorFotoContactoInvalida").ToString());
                        return;
                    }
                }

                newContact.firstName = txtFirstName.Text;
                newContact.lastName = txtLastName.Text;
                newContact.email = txtEmail.Text;
                newContact.mobileNumber = txtMobileNumber.Text;
                newContact.landlineNumber = txtLandlineNumber.Text;
                newContact.picExtension = fileExtension;
                
                result = contactsService.createContact(newContact);
                if (result.code == TransactionResult.transactionResultCode.Success)
                {
                    int createdContactId = result.affectedId;

                    try
                    {
                        if (uplContactPic.HasFile)
                        {
                            uplContactPic.SaveAs(Server.MapPath(SitePaths.contactsPics(SessionManager.sessionMemberInfo.id) + Encoding.sha1(createdContactId.ToString()) + fileExtension));
                        }

                        Response.Redirect(Pages.getContactsDefault(), false);
                    }
                    catch (Exception)
                    {
                        contactsService.deleteContact(createdContactId);
                        showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
                    }
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