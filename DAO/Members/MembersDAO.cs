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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "SELECT * FROM members ";
                sqlSentence += "WHERE members.id = @memberId ";

                sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@memberId", memberId);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "SELECT * FROM members ";
                sqlSentence += "WHERE members.email = @email ";

                sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@email", memberEmail);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "SELECT * FROM members ";
                sqlSentence += "WHERE members.email = @email ";
                sqlSentence += "AND members.password = @password ";
                sqlSentence += "AND members.isActive = @isActive ";

                sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@email", memberUser);
                sqlParameters[1] = new SqlParameter("@password", memberPassword);
                sqlParameters[2] = new SqlParameter("@isActive", true);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "INSERT INTO members (realName, displayName, email, password, language, isActive) ";
                sqlSentence += "VALUES (@realName, @displayName, @email, @password, @language, @isActive) ";

                sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@realName", enMember.realName);
                sqlParameters[1] = new SqlParameter("@displayName", enMember.displayName);
                sqlParameters[2] = new SqlParameter("@email", enMember.email);
                sqlParameters[3] = new SqlParameter("@password", enMember.password);
                sqlParameters[4] = new SqlParameter("@language", enMember.language);
                sqlParameters[5] = new SqlParameter("@isActive", true);

                dbWrapper.InsertUpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                    listMembers.Add(enMember);
                }

                convertedDatatable = listMembers;
            }

            return convertedDatatable;
        }
    }
}
