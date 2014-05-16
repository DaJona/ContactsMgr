using DAO.Members;
using DAO.Preferences;
using DTO.Preferences;
using DTO.System;
using Entity.Members;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Utilities;

namespace Service.Preferences
{
    public class PreferencesService
    {
        private SessionMemberInfoDTO memberInfo;
        private PreferencesDAO preferencesDAO;
        private ResourceManager resourceManager;
        private CultureInfo cultureInfo;

        public PreferencesService(SessionMemberInfoDTO sessionMemberInfo)
        {
            preferencesDAO = new PreferencesDAO(sessionMemberInfo);
            memberInfo = sessionMemberInfo;
            resourceManager = new ResourceManager("Resources.Resource", Assembly.Load("App_GlobalResources"));
            cultureInfo = new CultureInfo(memberInfo.lang, false);
        }

        public MemberPreferencesDTO getMemberPreferences()
        {
            MemberPreferencesDTO memberPreferences;

            try
            {
                memberPreferences = preferencesDAO.getMemberPreferences();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return memberPreferences;
        }

        public PasswordPreferencesDTO getPasswordPreferences()
        {
            PasswordPreferencesDTO passwordPreferences;

            try
            {
                passwordPreferences = preferencesDAO.getPasswordPreferences();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return passwordPreferences;
        }

        public LanguagePreferencesDTO getLanguagePreferences()
        {
            LanguagePreferencesDTO languagePreferences;

            try
            {
                languagePreferences = preferencesDAO.getLanguagePreferences();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return languagePreferences;
        }

        public void updateMemberPreferences(MemberPreferencesDTO memberPreferences)
        {
            try
            {
                preferencesDAO.updateMemberPreferences(memberPreferences);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransactionResultDTO updatePasswordPreferences(PasswordPreferencesDTO passwordPreferences)
        {
            TransactionResultDTO result = new TransactionResultDTO();

            try
            {
                MembersDAO membersDAO = new MembersDAO();
                Member enMember = membersDAO.getMember(memberInfo.id);
                string savedPassword = enMember.password;
                string newPassword = Encoding.sha512(enMember.email, passwordPreferences.actualPassword);

                // If the saved password is the same than the 'actualPassword' sent, continue update
                if (savedPassword == newPassword)
                {
                    PreferencesDAO preferencesDAO = new PreferencesDAO(memberInfo);

                    // Overwrite the member new password with more secure one
                    passwordPreferences.newPassword = Encoding.sha512(enMember.email, passwordPreferences.newPassword);

                    preferencesDAO.updatePasswordPreferences(passwordPreferences);

                    result.code = TransactionResultDTO.transactionResultCode.Success;
                }
                else
                {
                    result.code = TransactionResultDTO.transactionResultCode.Failed;
                    result.failureReason = "ErrorContrasenaActualIncorrecta";
                    return result;
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
