using DAO.Members;
using DTO.System;
using Entity.Members;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using Utilities;

namespace Service.Members
{
    public class MembersService
    {
        private MembersDAO membersDao;
        private ResourceManager resourceManager;
        private CultureInfo cultureInfo;

        public MembersService(string lang)
        {
            membersDao = new MembersDAO();
            resourceManager = new ResourceManager("Resources.Resource", Assembly.Load("App_GlobalResources"));
            cultureInfo = new CultureInfo(lang, false);
        }

        #region Member entity validation

        private const int REALNAME_LENGTH = 50;
        private const int DISPLAYNAME_LENGTH = 20;
        private const int EMAIL_LENGTH = 50;
        private const int PASSWORD_LENGTH = 90;
        private const int LANG_LENGTH = 5;

        private TransactionResultDTO validateEntity(Member entityToValidate)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                string lengthError = resourceManager.GetString("ErrorLongitudSuperada", cultureInfo);
                string formatError = resourceManager.GetString("ErrorFormatoIncorrecto", cultureInfo);

                // Validate real name required
                if (entityToValidate.realName == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorNombreCompletoReq", cultureInfo);
                    return result;
                }

                // Validate real name length
                if (entityToValidate.realName.Length > REALNAME_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NombreCompleto", cultureInfo), REALNAME_LENGTH);
                    return result;
                }

                // Validate display name required
                if (entityToValidate.displayName == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorNombreVisibleReq", cultureInfo);
                    return result;
                }

                // Validate display name length
                if (entityToValidate.displayName.Length > DISPLAYNAME_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("NombreVisible", cultureInfo), DISPLAYNAME_LENGTH);
                    return result;
                }

                // Validate email required
                if (entityToValidate.email == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorEmailReq", cultureInfo);
                    return result;
                }

                // Validate email format
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

                // Validate password required
                if (entityToValidate.password == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorContrasenaReq", cultureInfo);
                    return result;
                }

                // Validate password length
                if (entityToValidate.password.Length > PASSWORD_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Contrasena", cultureInfo), PASSWORD_LENGTH);
                    return result;
                }

                // Validate lang required
                if (entityToValidate.language == string.Empty)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorIdiomaReq", cultureInfo);
                    return result;
                }

                // Validate landline number length
                if (entityToValidate.language.Length > LANG_LENGTH)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = String.Format(lengthError, resourceManager.GetString("Idioma", cultureInfo), LANG_LENGTH);
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

        public TransactionResultDTO login(string memberUser, string memberPassword)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                Member member;
                member = membersDao.getMember(memberUser, Encoding.sha512(memberUser, memberPassword));

                if (member != null)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Success;
                    result.object1 = member;
                }
                else
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorUsuarioContrasenaIncorrectos", cultureInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public TransactionResultDTO createMember(Member enMember)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                // The member email must be unique
                Member existingMember = membersDao.getMember(enMember.email);
                if (existingMember != null)
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = resourceManager.GetString("ErrorEmailYaExiste", cultureInfo);
                    return result;
                }

                result = validateEntity(enMember);
                if (result.code == TransactionResultDTO.transactionResultCode.Success)
                {
                    // Overwrite the member password with more secure one
                    enMember.password = Encoding.sha512(enMember.email, enMember.password);

                    membersDao.createMember(enMember);

                    result.code = TransactionResultDTO.transactionResultCode.Success;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
