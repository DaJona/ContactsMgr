using DTO.Contacts;
using DTO.System;
using Entity.Contacts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAO.Contacts
{
    public class ContactsDAO
    {
        private DBWrapper dbWrapper;
        private SessionMemberInfoDTO memberInfo;

        public ContactsDAO(SessionMemberInfoDTO sessionMemberInfo)
        {
            dbWrapper = new DBWrapper();
            memberInfo = sessionMemberInfo;
        }

        public List<Contact> getContacts(SearchContactDTO searchOptions = null)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT * FROM contacts ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";

                if (searchOptions != null)
                {
                    if (searchOptions.firstName != string.Empty)
                        sqlSentence += "AND contacts.firstName LIKE '%" + searchOptions.firstName + "%' ";

                    if (searchOptions.lastName != string.Empty)
                        sqlSentence += "AND contacts.lastName LIKE '%" + searchOptions.lastName + "%' ";

                    if (searchOptions.genre != string.Empty)
                        if (searchOptions.genre.ToUpper() == "F" || searchOptions.genre.ToUpper() == "M")
                        {
                            sqlSentence += "AND contacts.genre = @genre ";
                            sqlParameters.Add(new SqlParameter("@genre", searchOptions.genre));
                        }

                    if (searchOptions.email != string.Empty)
                        sqlSentence += "AND contacts.email LIKE '%" + searchOptions.email + "%' ";

                    if (searchOptions.mobileNumber != string.Empty)
                        sqlSentence += "AND contacts.mobileNumber LIKE '%" + searchOptions.mobileNumber + "%' ";

                    if (searchOptions.landlineNumber != string.Empty)
                        sqlSentence += "AND contacts.landlineNumber LIKE '%" + searchOptions.landlineNumber + "%' ";

                    if (searchOptions.onlyActive && !searchOptions.onlyInactive)
                    {
                        sqlSentence += "AND contacts.isActive = @active ";
                        sqlParameters.Add(new SqlParameter("@active", true));
                    }

                    if (searchOptions.onlyInactive && !searchOptions.onlyActive)
                    {
                        sqlSentence += "AND contacts.isActive = @active ";
                        sqlParameters.Add(new SqlParameter("@active", false));
                    }

                    if (searchOptions.onlyWithPicture && !searchOptions.onlyWithoutPicture)
                        sqlSentence += "AND contacts.picExtension IS NOT NULL ";

                    if (searchOptions.onlyWithoutPicture && !searchOptions.onlyWithPicture)
                        sqlSentence += "AND contacts.picExtension IS NULL ";

                    if (searchOptions.comments != string.Empty)
                        sqlSentence += "AND contacts.comments LIKE '%" + searchOptions.comments + "%' ";

                    if (searchOptions.createdSince != null && searchOptions.createdUntil != null)
                        if (searchOptions.createdSince <= searchOptions.createdUntil)
                        {
                            sqlSentence += "AND (contacts.createdAt BETWEEN @dateSince AND @dateUntil) ";
                            SqlParameter dateSince = new SqlParameter("@dateSince", searchOptions.createdSince);
                            SqlParameter dateUntil = new SqlParameter("@dateUntil", searchOptions.createdUntil);

                            dateSince.DbType = DbType.DateTime;
                            dateUntil.DbType = DbType.DateTime;

                            sqlParameters.Add(dateSince);
                            sqlParameters.Add(dateUntil);
                        }
                }                

                sqlSentence += "ORDER BY contacts.firstName, contacts.lastName ";

                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (List<Contact>)getConvertedDatatable(dt, typeof(List<Contact>));
        }

        public Contact getContact(int contactId)
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";

            try
            {
                sqlSentence += "SELECT * FROM contacts ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));
                sqlParameters.Add(new SqlParameter("@contactId", contactId));

                dt = dbWrapper.FillDataTable(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (Contact)getConvertedDatatable(dt, typeof(Contact));
        }

        public int createContact(Contact enContact)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "INSERT INTO contacts (memberId, firstName, lastName, genre, email, mobileNumber, landlineNumber, isActive, picExtension, comments, createdAt) ";
                sqlSentence += "VALUES (@memberId, @firstName, @lastName, @genre, @email, @mobileNumber, @landlineNumber, @isActive, @picExtension, @comments, @createdAt) ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));
                sqlParameters.Add(new SqlParameter("@firstName", enContact.firstName));
                sqlParameters.Add(new SqlParameter("@lastName", enContact.lastName));
                sqlParameters.Add(new SqlParameter("@genre", enContact.genre.ToUpper()));
                sqlParameters.Add(new SqlParameter("@email", enContact.email));
                sqlParameters.Add(new SqlParameter("@mobileNumber", enContact.mobileNumber));
                sqlParameters.Add(new SqlParameter("@landlineNumber", enContact.landlineNumber));
                sqlParameters.Add(new SqlParameter("@isActive", true));
                sqlParameters.Add(new SqlParameter("@createdAt", DateTime.Now.ToUniversalTime().ToString("yyyyMMdd HH:mm:ss")));

                if (enContact.picExtension == string.Empty)
                    sqlParameters.Add(new SqlParameter("@picExtension", DBNull.Value));
                else
                    sqlParameters.Add(new SqlParameter("@picExtension", enContact.picExtension));

                if (enContact.comments == string.Empty)
                    sqlParameters.Add(new SqlParameter("@comments", DBNull.Value));
                else
                    sqlParameters.Add(new SqlParameter("@comments", enContact.comments));

                return dbWrapper.Insert(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void editContact(Contact enContact)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.firstName = @firstName, ";
                sqlSentence += "contacts.lastName = @lastName, ";
                sqlSentence += "contacts.genre = @genre, ";
                sqlSentence += "contacts.email = @email, ";
                sqlSentence += "contacts.mobileNumber = @mobileNumber, ";
                sqlSentence += "contacts.landlineNumber = @landlineNumber, ";
                sqlSentence += "contacts.comments = @comments ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@firstName", enContact.firstName));
                sqlParameters.Add(new SqlParameter("@lastName", enContact.lastName));
                sqlParameters.Add(new SqlParameter("@genre", enContact.genre.ToUpper()));
                sqlParameters.Add(new SqlParameter("@email", enContact.email));
                sqlParameters.Add(new SqlParameter("@mobileNumber", enContact.mobileNumber));
                sqlParameters.Add(new SqlParameter("@landlineNumber", enContact.landlineNumber));
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));
                sqlParameters.Add(new SqlParameter("@contactId", enContact.id));

                if (enContact.comments == string.Empty)
                    sqlParameters.Add(new SqlParameter("@comments", DBNull.Value));
                else
                    sqlParameters.Add(new SqlParameter("@comments", enContact.comments));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void editContactPicExtension(int contactId, string newPicExtension)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.picExtension = @picExtension ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));
                sqlParameters.Add(new SqlParameter("@contactId", contactId));

                if (newPicExtension == string.Empty)
                    sqlParameters.Add(new SqlParameter("@picExtension", DBNull.Value));
                else
                    sqlParameters.Add(new SqlParameter("@picExtension", newPicExtension));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void deleteContact(int contactId)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "DELETE FROM contacts WHERE contacts.id = @contactId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@contactId", contactId));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void deactivateContact(int contactId)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.isActive = @isActive ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@isActive", false));
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));
                sqlParameters.Add(new SqlParameter("@contactId", contactId));

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void activateContact(int contactId)
        {
            string sqlSentence = "";

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.isActive = @isActive ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@isActive", true));
                sqlParameters.Add(new SqlParameter("@memberId", memberInfo.id));
                sqlParameters.Add(new SqlParameter("@contactId", contactId));

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

            object convertedDatatable;

            if (returnType == typeof(Contact))
            {
                Contact enContact = new Contact();

                enContact.id = (int)data.Rows[0]["id"];
                enContact.memberId = (int)data.Rows[0]["memberId"];
                enContact.firstName = (string)data.Rows[0]["firstName"];
                enContact.lastName = (string)data.Rows[0]["lastName"];
                enContact.genre = (string)data.Rows[0]["genre"];
                enContact.email = data.Rows[0]["email"] is DBNull ? string.Empty : (string)data.Rows[0]["email"];
                enContact.mobileNumber = data.Rows[0]["mobileNumber"] is DBNull ? string.Empty : (string)data.Rows[0]["mobileNumber"];
                enContact.landlineNumber = data.Rows[0]["landlineNumber"] is DBNull ? string.Empty : (string)data.Rows[0]["landlineNumber"];
                enContact.isActive = (bool)data.Rows[0]["isActive"];
                enContact.picExtension = data.Rows[0]["picExtension"] is DBNull ? string.Empty : (string)data.Rows[0]["picExtension"];
                enContact.comments = data.Rows[0]["comments"] is DBNull ? string.Empty : (string)data.Rows[0]["comments"];
                enContact.createdAt = (DateTime)data.Rows[0]["createdAt"];

                convertedDatatable = enContact;
            }
            else
            {
                List<Contact> listContacts = new List<Contact>();
                Contact enContact;

                for (int index = 0; index < data.Rows.Count; index++)
                {
                    enContact = new Contact();

                    enContact.id = (int)data.Rows[index]["id"];
                    enContact.memberId = (int)data.Rows[index]["memberId"];
                    enContact.firstName = (string)data.Rows[index]["firstName"];
                    enContact.lastName = (string)data.Rows[index]["lastName"];
                    enContact.genre = (string)data.Rows[index]["genre"];
                    enContact.email = data.Rows[index]["email"] is DBNull ? string.Empty : (string)data.Rows[index]["email"];
                    enContact.mobileNumber = data.Rows[index]["mobileNumber"] is DBNull ? string.Empty : (string)data.Rows[index]["mobileNumber"];
                    enContact.landlineNumber = data.Rows[index]["landlineNumber"] is DBNull ? string.Empty : (string)data.Rows[index]["landlineNumber"];
                    enContact.isActive = (bool)data.Rows[index]["isActive"];
                    enContact.picExtension = data.Rows[index]["picExtension"] is DBNull ? string.Empty : (string)data.Rows[index]["picExtension"];
                    enContact.comments = data.Rows[index]["comments"] is DBNull ? string.Empty : (string)data.Rows[index]["comments"];
                    enContact.createdAt = (DateTime)data.Rows[index]["createdAt"];

                    listContacts.Add(enContact);
                }

                convertedDatatable = listContacts;
            }

            return convertedDatatable;
        }

        #endregion        
    }
}
