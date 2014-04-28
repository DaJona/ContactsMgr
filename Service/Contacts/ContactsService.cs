using DAO.Contacts;
using DTO.System;
using Entity.Contacts;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities;

namespace Service.Contacts
{
    public class ContactsService
    {
        public static Func<string, string> ServerMapPath;
        private SessionMemberInfo memberInfo;
        private ContactsDAO contactsDAO;

        public ContactsService(SessionMemberInfo sessionMemberInfo)
        {
            contactsDAO = new ContactsDAO(sessionMemberInfo);
            memberInfo = sessionMemberInfo;
        }

        public List<Contact> getContacts()
        {
            List<Contact> listContacts;

            try
            {
                listContacts = contactsDAO.getContacts();
            }
            catch (Exception ex)
            {
                listContacts = null;
                throw ex;
            }

            return listContacts;
        }

        public Contact getContact(int contactId)
        {
            Contact existingContact;

            try
            {
                existingContact = contactsDAO.getContact(contactId);
            }
            catch (Exception)
            {
                existingContact = null;
                throw;
            }

            return existingContact;
        }

        public void deactivateContact(int contactId)
        {
            try
            {
                contactsDAO.deactivateContact(contactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void activateContact(int contactId)
        {
            try
            {
                contactsDAO.activateContact(contactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransactionResult createContact(Contact enContact)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                int createdContactId = contactsDAO.createContact(enContact);
                result.code = TransactionResult.transactionResultCode.Success;
                result.affectedId = createdContactId;
            }
            catch (Exception ex)
            {
                result.code = TransactionResult.transactionResultCode.Failed;
                result.failureReason = "ErrorGeneral";
                throw ex;
            }

            return result;
        }

        public TransactionResult editContact(Contact enContact, bool deleteContactPic = false)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                if (deleteContactPic)
                {
                    deleteContacPics(enContact.id);
                    contactsDAO.editContactPicExtension(enContact.id, string.Empty);
                }

                contactsDAO.editContact(enContact);

                // Only if the new contact info contains pic extension data, we will update it 
                // because the 'editContact' method does not update that field.
                if (enContact.picExtension != string.Empty)
                {
                    // Look for existing pics for the contact and delete them. This because the user may 
                    // be updating the contact pic with a new one with different extension.
                    deleteContacPics(enContact.id);

                    contactsDAO.editContactPicExtension(enContact.id, enContact.picExtension);
                }

                result.code = TransactionResult.transactionResultCode.Success;
            }
            catch (Exception ex)
            {
                result.code = TransactionResult.transactionResultCode.Failed;
                result.failureReason = "ErrorGeneral";
                throw ex;
            }

            return result;
        }

        public void deleteContact(int contactId)
        {
            try
            {
                contactsDAO.deleteContact(contactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void deleteContacPics(int contactId)
        {
            string[] contactPicsList = Directory.GetFiles(ServerMapPath(SitePaths.contactsPics(memberInfo.id)), Encoding.sha1(contactId.ToString()) + "*");
            foreach (string contactExistingPic in contactPicsList)
            {
                File.Delete(contactExistingPic);
            }
        }
    }
}
