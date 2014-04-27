using DAO.Members;
using DAO.Preferences;
using DTO.Preferences;
using DTO.System;
using Entity.Members;
using System;
using Utilities;

namespace Service.Preferences
{
    public class PreferencesService
    {
        private SessionMemberInfo memberInfo;
        private PreferencesDAO preferencesDAO;

        public PreferencesService(SessionMemberInfo sessionMemberInfo)
        {
            preferencesDAO = new PreferencesDAO(sessionMemberInfo);
            memberInfo = sessionMemberInfo;
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
                memberPreferences = null;
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
                passwordPreferences = null;
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
                languagePreferences = null;
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

        public TransactionResult updatePasswordPreferences(PasswordPreferencesDTO passwordPreferences)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                MembersDAO membersDAO = new MembersDAO();
                Member enMember = membersDAO.getMember(memberInfo.id);
                string savedPassword = enMember.password;
                string newPassword = Encoding.getHashedPassword(enMember.email, passwordPreferences.actualPassword);

                // If the saved password is the same than the 'actualPassword' sent, continue update
                if (savedPassword == newPassword)
                {
                    PreferencesDAO preferencesDAO = new PreferencesDAO(memberInfo);

                    // Overwrite the member new password with more secure one
                    passwordPreferences.newPassword = Encoding.getHashedPassword(enMember.email, passwordPreferences.newPassword);

                    preferencesDAO.updatePasswordPreferences(passwordPreferences);

                    result.code = TransactionResult.transactionResultCode.Success;
                }
                else
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
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
