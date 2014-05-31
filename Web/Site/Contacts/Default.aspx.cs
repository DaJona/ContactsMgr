using DTO.System;
using Entity.Contacts;
using Service.Contacts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
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
                TransactionResultDTO result;
                List<Contact> listContacts;

                result = contactsService.getContacts();
                if (result.code == TransactionResultDTO.transactionResultCode.Success)
                {
                    listContacts = (List<Contact>)result.object1;
                    if (listContacts != null)
                    {
                        grdContacts.DataSource = listContacts;
                        regQuantity.Text = listContacts.Count.ToString();

                        divData.Visible = true;
                        divNoData.Visible = false;
                    }
                    else
                    {
                        grdContacts.DataSource = null;

                        divData.Visible = false;
                        divNoData.Visible = true;
                    }

                    grdContacts.DataBind();
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
                        searchContacts();
                        break;
                    case "activate":
                        contactsService.activateContact(contactId);
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

                searchContacts();
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }

        private ArrayList getSelectedContacts()
        {
            HtmlInputCheckBox chkRow;
            int contactId;
            ArrayList selectedContacts = null;

            foreach (GridViewRow row in grdContacts.Rows)
            {
                chkRow = (HtmlInputCheckBox)(row.FindControl("chkItem"));
                contactId = Convert.ToInt32(grdContacts.DataKeys[row.RowIndex]["id"]);

                if (chkRow.Checked)
                {
                    if (selectedContacts == null)
                    {
                        selectedContacts = new ArrayList();
                    }

                    selectedContacts.Add(contactId);
                }
            }

            return selectedContacts;
        }

        protected void cmdDeleteSelectedContacts_Click(object sender, EventArgs e)
        {
            try
            {
                ContactsService contactsService = new ContactsService(SessionManager.sessionMemberInfo);
                ArrayList selectedContacts = getSelectedContacts();

                contactsService.deleteContacts(selectedContacts);

                searchContacts();
            }
            catch (Exception)
            {
                showError(HttpContext.GetGlobalResourceObject("Resource", "ErrorGeneral").ToString());
            }
        }
    }
}