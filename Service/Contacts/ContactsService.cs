using DAO.Contacts;
using DTO.Contacts;
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
        private SessionMemberInfoDTO memberInfo;
        private ContactsDAO contactsDAO;
        private ResourceManager resourceManager;
        private CultureInfo cultureInfo;

        public ContactsService(SessionMemberInfoDTO sessionMemberInfo)
        {
            contactsDAO = new ContactsDAO(sessionMemberInfo);
            memberInfo = sessionMemberInfo;
            resourceManager = new ResourceManager("Resources.Resource", Assembly.Load("App_GlobalResources"));
            cultureInfo = new CultureInfo(memberInfo.lang, false);
        }

        #region Entities validation

        private const int FIRSTNAME_LENGTH = 20;
        private const int LASTNAME_LENGTH = 20;
        private const int EMAIL_LENGTH = 50;
        private const int MOBILENUMBER_LENGTH = 20;
        private const int LANDLINENUMBER_LENGTH = 20;
        private const int COMMENTS_LENGTH = 200;

        #region Contact entity validation

        private TransactionResultDTO validateContactEntity(Contact entityToValidate)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                string lengthError = resourceManager.GetString("ErrorLongitudSuperada", cultureInfo);
                string formatError = resourceManager.GetString("ErrorFormatoIncorrecto", cultureInfo);

                // Validate first name required
                if (entityToValidate.firstName == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorNombreReq", cultureInfo);
                    return result;
                }

                // Validate first name length
                if (entityToValidate.firstName.Length > FIRSTNAME_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Nombre", cultureInfo), FIRSTNAME_LENGTH);
                    return result;
                }

                // Validate last name required
                if (entityToValidate.lastName == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorApellidosReq", cultureInfo);
                    return result;
                }

                // Validate last name length
                if (entityToValidate.lastName.Length > LASTNAME_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Apellidos", cultureInfo), LASTNAME_LENGTH);
                    return result;
                }

                // Validate genre required
                if (entityToValidate.genre == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorGenreReq", cultureInfo);
                    return result;
                }

                // Validate genre format
                if (entityToValidate.genre != "F" && entityToValidate.genre != "M")
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(formatError, resourceManager.GetString("Genero", cultureInfo));
                    return result;
                }

                // Validate email format
                if (entityToValidate.email != string.Empty)
                {
                    if (!Validations.isValidEmailFormat(entityToValidate.email))
                    {
                        result.code = TransactionResultDTO.transactionResultCode.Failed;
                        result.failureReason = String.Format(formatError, resourceManager.GetString("Email", cultureInfo));
                        return result;
                    }
                }

                // Validate email length
                if (entityToValidate.email.Length > EMAIL_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Email", cultureInfo), EMAIL_LENGTH);
                    return result;
                }

                // Validate mobile number length
                if (entityToValidate.mobileNumber.Length > MOBILENUMBER_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NumeroMovil", cultureInfo), MOBILENUMBER_LENGTH);
                    return result;
                }

                // Validate landline number length
                if (entityToValidate.landlineNumber.Length > LANDLINENUMBER_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NumeroTelefono", cultureInfo), LANDLINENUMBER_LENGTH);
                    return result;
                }

                // Validate comments length
                if (entityToValidate.comments.Length > COMMENTS_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Comentarios", cultureInfo), COMMENTS_LENGTH);
                    return result;
                }

                result.code = TransactionResultDTO.transactionResultCode.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion

        #region Search contact entity validation

        private TransactionResultDTO validateSearchContactEntity(SearchContactDTO entityToValidate)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                string lengthError = resourceManager.GetString("ErrorLongitudSuperada", cultureInfo);
                string formatError = resourceManager.GetString("ErrorFormatoIncorrecto", cultureInfo);
                string incompleteDatesRangeError = resourceManager.GetString("ErrorRangoFechasIncompleto", cultureInfo);
                string incorrectDatesRangeError = resourceManager.GetString("ErrorRangoFechasIncorrecto", cultureInfo);

                // Validate first name length
                if (entityToValidate.firstName.Length > FIRSTNAME_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Nombre", cultureInfo), FIRSTNAME_LENGTH);
                    return result;
                }

                // Validate last name length
                if (entityToValidate.lastName.Length > LASTNAME_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Apellidos", cultureInfo), LASTNAME_LENGTH);
                    return result;
                }

                // Validate genre format
                if (entityToValidate.genre != string.Empty)
                    if (entityToValidate.genre != "F" && entityToValidate.genre != "M")
                    {
                        result.code = TransactionResultDTO.transactionResultCode.Failed;
                        result.failureReason = String.Format(formatError, resourceManager.GetString("Genero", cultureInfo));
                        return result;
                    }

                // Validate email format
                if (entityToValidate.email != string.Empty)
                    if (!Validations.isValidEmailFormat(entityToValidate.email))
                    {
                        result.code = TransactionResultDTO.transactionResultCode.Failed;
                        result.failureReason = String.Format(formatError, resourceManager.GetString("Email", cultureInfo));
                        return result;
                    }

                // Validate email length
                if (entityToValidate.email.Length > EMAIL_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Email", cultureInfo), EMAIL_LENGTH);
                    return result;
                }

                // Validate mobile number length
                if (entityToValidate.mobileNumber.Length > MOBILENUMBER_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NumeroMovil", cultureInfo), MOBILENUMBER_LENGTH);
                    return result;
                }

                // Validate landline number length
                if (entityToValidate.landlineNumber.Length > LANDLINENUMBER_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NumeroTelefono", cultureInfo), LANDLINENUMBER_LENGTH);
                    return result;
                }

                // Validate comments length
                if (entityToValidate.comments.Length > COMMENTS_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Comentarios", cultureInfo), COMMENTS_LENGTH);
                    return result;
                }

                // Validate dates range to be complete
                if ((entityToValidate.createdSince != null && entityToValidate.createdUntil == null) || (entityToValidate.createdSince == null && entityToValidate.createdUntil != null))
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(incompleteDatesRangeError, resourceManager.GetString("FechaCreacion", cultureInfo));
                    return result;
                }

                // Validate since date to be less or equal than until date
                if (entityToValidate.createdSince != null && entityToValidate.createdUntil != null)
                    if (entityToValidate.createdSince > entityToValidate.createdUntil)
                    {
                        result.code = TransactionResultDTO.transactionResultCode.Failed;
                        result.failureReason = String.Format(incorrectDatesRangeError, resourceManager.GetString("FechaCreacion", cultureInfo));
                        return result;
                    }

                result.code = TransactionResultDTO.transactionResultCode.Success;
            }
            catch (Exception ex)
            {                
                throw ex;
            }

            return result;            
        }

        #endregion

        #endregion

        public TransactionResultDTO getContacts(SearchContactDTO searchOptions = null)
        {
            TransactionResultDTO result = new TransactionResultDTO();
            List<Contact> listContacts;

            try
            {
                if (searchOptions != null)
                {
                    result = validateSearchContactEntity(searchOptions);
                    if (result.code == TransactionResultDTO.transactionResultCode.Success)
                    {
                        listContacts = contactsDAO.getContacts(searchOptions);
                        result.code = TransactionResultDTO.transactionResultCode.Success;
                        result.object1 = listContacts;
                    }
                }
                else
                {
                    listContacts = contactsDAO.getContacts();
                    result.code = TransactionResultDTO.transactionResultCode.Success;
                    result.object1 = listContacts;
                }
            }
            catch (Exception ex)
            {
                listContacts = null;
                throw ex;
            }

            return result;
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

        public TransactionResultDTO createContact(Contact enContact)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                result = validateContactEntity(enContact);
                if (result.code == TransactionResultDTO.transactionResultCode.Success)
                {
                    int createdContactId = contactsDAO.createContact(enContact);
                    result.code = TransactionResultDTO.transactionResultCode.Success;
                    result.affectedId = createdContactId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public TransactionResultDTO editContact(Contact enContact, bool deleteContactPic = false)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                result = validateContactEntity(enContact);
                if (result.code == TransactionResultDTO.transactionResultCode.Success)
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

                    result.code = TransactionResultDTO.transactionResultCode.Success;
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
