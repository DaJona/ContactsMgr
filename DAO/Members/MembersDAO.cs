using DTO.System;
using Entity.Members;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAO.Members
{
    public class MembersDAO
    {
        private DBWrapper dbWrapper;

        public MembersDAO()
        {
            dbWrapper = new DBWrapper();
        }

        public Member getMember(int memberId)
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT * FROM members ";
                sqlSentence += "WHERE members.id = @memberId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberId));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (Member)getConvertedDatatable(dt, typeof(Member));
        }

        public Member getMember(string memberEmail)
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT * FROM members ";
                sqlSentence += "WHERE members.email = @email ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@email", memberEmail));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (Member)getConvertedDatatable(dt, typeof(Member));
        }

        public Member getMember(string memberUser, string memberPassword)
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT * FROM members ";
                sqlSentence += "WHERE members.email = @email ";
                sqlSentence += "AND members.password = @password ";
                sqlSentence += "AND members.isActive = @isActive ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@email", memberUser));
                sqlParameters.Add(new SqlParameter("@password", memberPassword));
                sqlParameters.Add(new SqlParameter("@isActive", true));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (Member)getConvertedDatatable(dt, typeof(Member));
        }

        public void createMember(Member enMember)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "INSERT INTO members (realName, displayName, email, password, language, isActive, createdAt) ";
                sqlSentence += "VALUES (@realName, @displayName, @email, @password, @language, @isActive, @createdAt) ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@realName", enMember.realName));
                sqlParameters.Add(new SqlParameter("@displayName", enMember.displayName));
                sqlParameters.Add(new SqlParameter("@email", enMember.email));
                sqlParameters.Add(new SqlParameter("@password", enMember.password));
                sqlParameters.Add(new SqlParameter("@language", enMember.language));
                sqlParameters.Add(new SqlParameter("@isActive", true));
                sqlParameters.Add(new SqlParameter("@createdAt", DateTime.Now.ToUniversalTime().ToString("yyyyMMdd HH:mm:ss")));

                dbWrapper.Insert(sqlSentence, sqlParameters);
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

            object convertedDatatable;

            if (returnType == typeof(Member))
            {
                Member enMember = new Member();

                enMember.id = (int)data.Rows[0]["id"];
                enMember.realName = (string)data.Rows[0]["realName"];
                enMember.displayName = (string)data.Rows[0]["displayName"];
                enMember.email = (string)data.Rows[0]["email"];
                enMember.password = (string)data.Rows[0]["password"];
                enMember.language = (string)data.Rows[0]["language"];
                enMember.isActive = (bool)data.Rows[0]["isActive"];
                enMember.createdAt = (DateTime)data.Rows[0]["createdAt"];

                convertedDatatable = enMember;
            }
            else
            {
                List<Member> listMembers = new List<Member>();
                Member enMember;

                for (int index = 0; index < data.Rows.Count; index++)
                {
                    enMember = new Member();

                    enMember.id = (int)data.Rows[index]["id"];
                    enMember.realName = (string)data.Rows[index]["realName"];
                    enMember.displayName = (string)data.Rows[index]["displayName"];
                    enMember.email = (string)data.Rows[index]["email"];
                    enMember.password = (string)data.Rows[index]["password"];
                    enMember.language = (string)data.Rows[index]["language"];
                    enMember.isActive = (bool)data.Rows[index]["isActive"];
                    enMember.createdAt = (DateTime)data.Rows[index]["createdAt"];

                    listMembers.Add(enMember);
                }

                convertedDatatable = listMembers;
            }

            return convertedDatatable;
        }

        #endregion
    }
}
