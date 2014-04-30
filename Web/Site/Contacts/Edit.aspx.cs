using DTO.System;
using Entity.Contacts;
using Service.Contacts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Web.Site.Contacts
{
    public partial class Edit : BasePage
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

                // If the contact has an extension, means that has a pic
                if (existingContact.picExtension != string.Empty)
                {
                    imgContactPic.ImageUrl = SitePaths.contactsPics(SessionManager.sessionMemberInfo.id) + Encoding.sha1(existingContact.id.ToString()) + existingContact.picExtension;
                    chkDeletePic.Visible = true;
                }

                // If contact is not active, the user can not edit it
                if (!existingContact.isActive)
                {
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;
                    txtEmail.Enabled = false;
                    txtMobileNumber.Enabled = false;
                    txtLandlineNumber.Enabled = false;
                    cmdEditContact.Visible = false;
                    uplContactPic.Visible = false;
                    chkDeletePic.Visible = false;
                }
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

                editedContact.id = Convert.ToInt32(Request.QueryString[Parameters.id]);
                editedContact.firstName = txtFirstName.Text;
                editedContact.lastName = txtLastName.Text;
                editedContact.email = txtEmail.Text;
                editedContact.mobileNumber = txtMobileNumber.Text;
                editedContact.landlineNumber = txtLandlineNumber.Text;
                editedContact.picExtension = fileExtension;

                result = contactsService.editContact(editedContact, chkDeletePic.Checked);
                if (result.code == TransactionResult.transactionResultCode.Success)
                {
                    try
                    {
                        if (uplContactPic.HasFile)
                        {
                            uplContactPic.SaveAs(Server.MapPath(SitePaths.contactsPics(SessionManager.sessionMemberInfo.id) + Encoding.sha1(editedContact.id.ToString()) + fileExtension));
                        }

                        Response.Redirect(Pages.getContactsDefault(), false);
                    }
                    catch (Exception)
                    {
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

        protected void chkDeletePic_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                uplContactPic.Dispose();
                uplContactPic.Enabled = false;
            }
            else
            {
                uplContactPic.Enabled = true;
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getContactsDefault(), true);
        }
    }
}