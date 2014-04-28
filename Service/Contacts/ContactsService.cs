using DAO.Contacts;
using DTO.System;
using Entity.Contacts;
using System;
using System.Collections.Generic;

namespace Service.Contacts
{
    public class ContactsService
    {
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

        public TransactionResult editContact(Contact enContact)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                contactsDAO.editContact(enContact);
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
    }
}
