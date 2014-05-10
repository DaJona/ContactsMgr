using DAO.Contacts;
using DTO.System;
using Entity.Contacts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Utilities;

namespace Service.Contacts
{
    public class ContactsService
    {
        public static Func<string, string> ServerMapPath;
        private SessionMemberInfo memberInfo;
        private ContactsDAO contactsDAO;
        private ResourceManager resourceManager;
        private CultureInfo cultureInfo;

        public ContactsService(SessionMemberInfo sessionMemberInfo)
        {
            contactsDAO = new ContactsDAO(sessionMemberInfo);
            memberInfo = sessionMemberInfo;
            resourceManager = new ResourceManager("Resources.Resource", Assembly.Load("App_GlobalResources"));
            cultureInfo = new CultureInfo(memberInfo.lang, false);
        }

        #region Contact entity validation

        private const int FIRSTNAME_LENGTH = 20;
        private const int LASTNAME_LENGTH = 20;
        private const int EMAIL_LENGTH = 50;
        private const int MOBILENUMBER_LENGTH = 20;
        private const int LANDLINENUMBER_LENGTH = 20;
        private const int COMMENTS_LENGTH = 200;

        private TransactionResult validateEntity(Contact entityToValidate)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                string lengthError = resourceManager.GetString("ErrorLongitudSuperada", cultureInfo);
                string formatError = resourceManager.GetString("ErrorFormatoIncorrecto", cultureInfo);

                // Validate first name required
                if (entityToValidate.firstName == string.Empty)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorNombreReq", cultureInfo);
                    return result;
                }

                // Validate first name length
                if (entityToValidate.firstName.Length > FIRSTNAME_LENGTH)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Nombre", cultureInfo), FIRSTNAME_LENGTH);
                    return result;
                }

                // Validate last name required
                if (entityToValidate.lastName == string.Empty)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorApellidosReq", cultureInfo);
                    return result;
                }

                // Validate last name length
                if (entityToValidate.lastName.Length > LASTNAME_LENGTH)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Apellidos", cultureInfo), LASTNAME_LENGTH);
                    return result;
                }

                // Validate genre required
                if (entityToValidate.genre == string.Empty)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorGenreReq", cultureInfo);
                    return result;
                }

                // Validate genre format
                if (entityToValidate.genre != "F" && entityToValidate.genre != "M")
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(formatError, resourceManager.GetString("Genero", cultureInfo));
                    return result;
                }

                // Validate email format
                if (entityToValidate.email != string.Empty)
                {
                    string emailPattern = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$;";
                    if (!Regex.IsMatch(entityToValidate.email.Trim(), emailPattern))
                    {
                        result.code = TransactionResult.transactionResultCode.Failed;
                        result.failureReason = String.Format(formatError, resourceManager.GetString("Email", cultureInfo));
                        return result;
                    }
                }                

                // Validate email length
                if (entityToValidate.email.Length > EMAIL_LENGTH)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Email", cultureInfo), EMAIL_LENGTH);
                    return result;
                }

                // Validate mobile number length
                if (entityToValidate.mobileNumber.Length > MOBILENUMBER_LENGTH)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NumeroMovil", cultureInfo), MOBILENUMBER_LENGTH);
                    return result;
                }

                // Validate landline number length
                if (entityToValidate.landlineNumber.Length > LANDLINENUMBER_LENGTH)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NumeroTelefono", cultureInfo), LANDLINENUMBER_LENGTH);
                    return result;
                }

                // Validate comments length
                if (entityToValidate.comments.Length > COMMENTS_LENGTH)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Comentarios", cultureInfo), COMMENTS_LENGTH);
                    return result;
                }

                result.code = TransactionResult.transactionResultCode.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion

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
                result = validateEntity(enContact);
                if (result.code == TransactionResult.transactionResultCode.Success)
                {
                    int createdContactId = contactsDAO.createContact(enContact);
                    result.code = TransactionResult.transactionResultCode.Success;
                    result.affectedId = createdContactId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public TransactionResult editContact(Contact enContact, bool deleteContactPic = false)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                result = validateEntity(enContact);
                if (result.code == TransactionResult.transactionResultCode.Success)
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public void deleteContact(int contactId)
        {
            try
            {
                deleteContacPics(contactId);
                contactsDAO.deleteContact(contactId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void deleteContacts(ArrayList contactsIdToDelete)
        {
            try
            {
                foreach (int contactId in contactsIdToDelete)
                {
                    deleteContact(contactId);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void deleteContacPics(int contactId)
        {
            string[] contactPicsList = Directory.GetFiles(ServerMapPath(SitePaths.contactsPics(memberInfo.id)), Encoding.sha1(contactId.ToString()) + "*");
            foreach (string contactExistingPic in contactPicsList)
            {
                File.Delete(contactExistingPic);
            }
        }
    }
}
