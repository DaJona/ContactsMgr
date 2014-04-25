using DAO.Preferences;
using DTO.Preferences;
using System;

namespace Service.Preferences
{
    public class PreferencesService
    {
        private int memberId;
        private PreferencesDAO preferencesDAO;

        public PreferencesService(int sessionMemberId)
        {
            memberId = sessionMemberId;
            preferencesDAO = new PreferencesDAO(memberId);
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
    }
}
