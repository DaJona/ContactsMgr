﻿using DTO.System;
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
        private SessionMemberInfo memberInfo;

        public ContactsDAO(SessionMemberInfo sessionMemberInfo)
        {
            dbWrapper = new DBWrapper();
            memberInfo = sessionMemberInfo;
        }

        public List<Contact> getContacts()
        {
            DataTable dt = new DataTable();
            string sqlSentence = "";
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "SELECT * FROM contacts ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "ORDER BY contacts.firstName, contacts.lastName ";

                sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@memberId", memberInfo.id);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "SELECT * FROM contacts ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@memberId", memberInfo.id);
                sqlParameters[1] = new SqlParameter("@contactId", contactId);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "INSERT INTO contacts (memberId, firstName, lastName, email, mobileNumber, landlineNumber, isActive, picExtension, createdAt) ";
                sqlSentence += "VALUES (@memberId, @firstName, @lastName, @email, @mobileNumber, @landlineNumber, @isActive, @picExtension, @createdAt) ";

                sqlParameters = new SqlParameter[9];
                sqlParameters[0] = new SqlParameter("@memberId", memberInfo.id);
                sqlParameters[1] = new SqlParameter("@firstName", enContact.firstName);
                sqlParameters[2] = new SqlParameter("@lastName", enContact.lastName);
                sqlParameters[3] = new SqlParameter("@email", enContact.email);
                sqlParameters[4] = new SqlParameter("@mobileNumber", enContact.mobileNumber);
                sqlParameters[5] = new SqlParameter("@landlineNumber", enContact.landlineNumber);
                sqlParameters[6] = new SqlParameter("@isActive", true);
                sqlParameters[7] = new SqlParameter("@createdAt", DateTime.Now.ToUniversalTime().ToString("yyyyMMdd HH:mm:ss"));

                if (enContact.picExtension == string.Empty)
                {
                    sqlParameters[8] = new SqlParameter("@picExtension", DBNull.Value);
                }
                else
                {
                    sqlParameters[8] = new SqlParameter("@picExtension", enContact.picExtension);
                }

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.firstName = @firstName, ";
                sqlSentence += "contacts.lastName = @lastName, ";
                sqlSentence += "contacts.email = @email, ";
                sqlSentence += "contacts.mobileNumber = @mobileNumber, ";
                sqlSentence += "contacts.landlineNumber = @landlineNumber ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                sqlParameters = new SqlParameter[7];
                sqlParameters[0] = new SqlParameter("@firstName", enContact.firstName);
                sqlParameters[1] = new SqlParameter("@lastName", enContact.lastName);
                sqlParameters[2] = new SqlParameter("@email", enContact.email);
                sqlParameters[3] = new SqlParameter("@mobileNumber", enContact.mobileNumber);
                sqlParameters[4] = new SqlParameter("@landlineNumber", enContact.landlineNumber);
                sqlParameters[5] = new SqlParameter("@memberId", memberInfo.id);
                sqlParameters[6] = new SqlParameter("@contactId", enContact.id);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.picExtension = @picExtension ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@memberId", memberInfo.id);
                sqlParameters[1] = new SqlParameter("@contactId", contactId);

                if (newPicExtension == string.Empty)
                {
                    sqlParameters[2] = new SqlParameter("@picExtension", DBNull.Value);
                }
                else
                {
                    sqlParameters[2] = new SqlParameter("@picExtension", newPicExtension);
                }

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "DELETE FROM contacts WHERE contacts.id = @contactId ";

                sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@contactId", contactId);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.isActive = @isActive ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@isActive", false);
                sqlParameters[1] = new SqlParameter("@memberId", memberInfo.id);
                sqlParameters[2] = new SqlParameter("@contactId", contactId);

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
            SqlParameter[] sqlParameters;

            try
            {
                sqlSentence += "UPDATE contacts SET ";
                sqlSentence += "contacts.isActive = @isActive ";
                sqlSentence += "WHERE contacts.memberId = @memberId ";
                sqlSentence += "AND contacts.id = @contactId ";

                sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@isActive", true);
                sqlParameters[1] = new SqlParameter("@memberId", memberInfo.id);
                sqlParameters[2] = new SqlParameter("@contactId", contactId);

                dbWrapper.UpdateDelete(sqlSentence, sqlParameters);
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

            if (returnType == typeof(Contact))
            {
                Contact enContact = new Contact();

                enContact.id = (int)data.Rows[0]["id"];
                enContact.memberId = (int)data.Rows[0]["memberId"];
                enContact.firstName = (string)data.Rows[0]["firstName"];
                enContact.lastName = (string)data.Rows[0]["lastName"];
                enContact.email = data.Rows[0]["email"] is DBNull ? string.Empty : (string)data.Rows[0]["email"];
                enContact.mobileNumber = data.Rows[0]["mobileNumber"] is DBNull ? string.Empty : (string)data.Rows[0]["mobileNumber"];
                enContact.landlineNumber = data.Rows[0]["landlineNumber"] is DBNull ? string.Empty : (string)data.Rows[0]["landlineNumber"];
                enContact.isActive = (bool)data.Rows[0]["isActive"];
                enContact.picExtension = data.Rows[0]["picExtension"] is DBNull ? string.Empty : (string)data.Rows[0]["picExtension"];
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
                    enContact.email = data.Rows[index]["email"] is DBNull ? string.Empty : (string)data.Rows[index]["email"];
                    enContact.mobileNumber = data.Rows[index]["mobileNumber"] is DBNull ? string.Empty : (string)data.Rows[index]["mobileNumber"];
                    enContact.landlineNumber = data.Rows[index]["landlineNumber"] is DBNull ? string.Empty : (string)data.Rows[index]["landlineNumber"];
                    enContact.isActive = (bool)data.Rows[index]["isActive"];
                    enContact.picExtension = data.Rows[index]["picExtension"] is DBNull ? string.Empty : (string)data.Rows[index]["picExtension"];
                    enContact.createdAt = (DateTime)data.Rows[index]["createdAt"];

                    listContacts.Add(enContact);
                }

                convertedDatatable = listContacts;
            }

            return convertedDatatable;
        }
    }
}
