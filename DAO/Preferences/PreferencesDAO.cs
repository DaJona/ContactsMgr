using DTO.Preferences;
using DTO.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAO.Preferences
{
    public class PreferencesDAO
    {
        private DBWrapper dbWrapper;
        private SessionMemberInfoDTO memberInfo;

        public PreferencesDAO(SessionMemberInfoDTO sessionMemberInfo)
        {
            dbWrapper = new DBWrapper();
            memberInfo = sessionMemberInfo;
        }

        public MemberPreferencesDTO getMemberPreferences()
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT members.id, members.realName, members.displayName, members.email FROM members ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (MemberPreferencesDTO)getConvertedDatatable(dt, typeof(MemberPreferencesDTO));
        }

        public PasswordPreferencesDTO getPasswordPreferences()
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT members.id, members.password ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (PasswordPreferencesDTO)getConvertedDatatable(dt, typeof(PasswordPreferencesDTO));
        }

        public LanguagePreferencesDTO getLanguagePreferences()
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT members.id, members.language ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));                

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (LanguagePreferencesDTO)getConvertedDatatable(dt, typeof(LanguagePreferencesDTO));
        }

        public void updateMemberPreferences(MemberPreferencesDTO memberPreferences)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE members SET ";
                sqlSentence += "members.realName = @realName, ";
                sqlSentence += "members.displayName = @displayName ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@realName", memberPreferences.realName));
                sqlParameters.Add(new SqlParameter("@displayName", memberPreferences.displayName));
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updatePasswordPreferences(PasswordPreferencesDTO passwordPreferences)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE members SET ";
                sqlSentence += "members.password = @password ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@password", passwordPreferences.newPassword));
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateLanguagePreferences(LanguagePreferencesDTO languagePreferences)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE members SET ";
                sqlSentence += "members.language = @language ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@language", languagePreferences.language));
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Return data

        private object getConvertedDatatable(DataTable data, Type returnType)
        {
            if (data.Rows.Count == 0)
            {
                return null;
            }

            object convertedDatatable = null;

            if (returnType == typeof(MemberPreferencesDTO))
            {
                MemberPreferencesDTO enMemberPreferences = new MemberPreferencesDTO();

                enMemberPreferences.memberId = (int)data.Rows[0]["id"];
                enMemberPreferences.realName = (string)data.Rows[0]["realName"];
                enMemberPreferences.displayName = (string)data.Rows[0]["displayName"];
                enMemberPreferences.email = (string)data.Rows[0]["email"];

                convertedDatatable = enMemberPreferences;
            }
            else if (returnType == typeof(LanguagePreferencesDTO))
            {
                LanguagePreferencesDTO enLanguagePreferences = new LanguagePreferencesDTO();

                enLanguagePreferences.memberId = (int)data.Rows[0]["id"];
                enLanguagePreferences.language = (string)data.Rows[0]["language"];

                convertedDatatable = enLanguagePreferences;
            }

            return convertedDatatable;
        }

        #endregion        
    }
}
