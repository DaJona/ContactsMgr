using DTO.System;
using Entity.Contacts;
using Service.Contacts;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Utilities;

namespace Web.Site.Contacts
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                searchContacts();
            }
        }

        private void searchContacts()
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                List<Contact> listContacts;

                listContacts = contactsService.getContacts();
                if (listContacts != null)
                {
                    grdContacts.DataSource = listContacts;
                    divNoData.Visible = false;
                }
                else
                {
                    grdContacts.DataSource = null;
                    divNoData.Visible = true;
                }

                grdContacts.DataBind();
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }

        private void showError(string message)
        {
            cvDefaultContacts.IsValid = false;
            cvDefaultContacts.ErrorMessage = message;
        }

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.getContactsCreate(), true);
        }

        protected string getStatusText(bool isActive)
        {
            if (isActive)
            {
                return HttpContext.GetGlobalResourceObject("Resource", "Activo").ToString();
            }
            else
            {
                return HttpContext.GetGlobalResourceObject("Resource", "Inactivo").ToString();
            }
        }

        protected void grdContacts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                int contactId = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "editContact":
                        Response.Redirect(Pages.getContactsEdit(contactId), true);
                        break;
                    case "deleteContact":
                        hdnContactIdToDelete.Value = contactId.ToString();
                        mpeDeleteContact.Show();
                        break;
                    case "deactivate":
                        contactsService.deactivateContact(contactId);
                        //Response.Redirect(Pages.getContactsDefault(), true);
                        searchContacts();
                        break;
                    case "activate":
                        contactsService.activateContact(contactId);
                        //Response.Redirect(Pages.getContactsDefault(), true);
                        searchContacts();
                        break;
                }
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }            
        }

        protected void cmdDeleteContact_Click(object sender, EventArgs e)
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                contactsService.deleteContact(Convert.ToInt32(hdnContactIdToDelete.Value));

                //Response.Redirect(Pages.getContactsDefault(), true);
                searchContacts();
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }
    }
}