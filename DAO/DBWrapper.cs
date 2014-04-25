using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DBWrapper
    {
        const string sqlServerInstanceName = "JONIGU-DESKTOP\\SQLEXPRESS";
        const string dbName = "ContactMgr";
        const string dbUser = "ctcmgr";
        const string dbPassword = "d4j0n44dm1n";
        const bool useIntegratedSecurity = false;

        private SqlConnection oSqlConn;
        private SqlCommand oSqlCmd;
        private SqlDataAdapter oSqlDtAdptr;
        private SqlConnectionStringBuilder oConStringBuilder;

        #region Insert/Update/Delete

        /// <summary>
        /// Inserts, updates or deletes information to database.
        /// </summary>
        /// <param name="sqlQuery">The query to make the insert, update or delete.</param>
        /// <returns>The number of rows affected</returns>
        public int InsertUpdateDelete(string sqlQuery, SqlParameter[] sqlParameters)
        {
            try
            {
                oConStringBuilder = new SqlConnectionStringBuilder();
                oConStringBuilder.DataSource = sqlServerInstanceName;
                oConStringBuilder.InitialCatalog = dbName;
                oConStringBuilder.UserID = dbUser;
                oConStringBuilder.Password = dbPassword;
                oConStringBuilder.IntegratedSecurity = useIntegratedSecurity;

                using (oSqlConn = new SqlConnection(oConStringBuilder.ConnectionString))
                {
                    if (oSqlConn.State != ConnectionState.Open)
                    {
                        oSqlConn.Open();
                    }

                    using (oSqlCmd = new SqlCommand())
                    {
                        oSqlCmd.Connection = oSqlConn;
                        oSqlCmd.CommandText = sqlQuery;

                        if (sqlParameters.Length > 0)
                        {
                            oSqlCmd.Parameters.AddRange(sqlParameters);
                        }

                        return oSqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlConn.State == ConnectionState.Open)
                {
                    oSqlConn.Close();
                }
            }
        }

        /// <summary>
        /// Inserts, updates or deletes information to database into a transaction.
        /// </summary>
        /// <param name="sqlQuery">The query to make the insert, update or delete.</param>
        /// <param name="transaction">The transaction used to perform the insert, update or delete.</param>
        /// <returns>The number of rows affected</returns>
        public int InsertUpdateDelete(string sqlQuery, SqlParameter[] sqlParameters, IDbTransaction transaction)
        {
            try
            {
                oConStringBuilder = new SqlConnectionStringBuilder();
                oConStringBuilder.DataSource = sqlServerInstanceName;
                oConStringBuilder.InitialCatalog = dbName;
                oConStringBuilder.UserID = dbUser;
                oConStringBuilder.Password = dbPassword;
                oConStringBuilder.IntegratedSecurity = useIntegratedSecurity;

                using (transaction)
                {
                    using (oSqlCmd = new SqlCommand())
                    {
                        oSqlCmd.Connection = (SqlConnection)transaction.Connection;
                        oSqlCmd.Transaction = (SqlTransaction)transaction;
                        oSqlCmd.CommandText = sqlQuery;

                        if (sqlParameters.Length > 0)
                        {
                            oSqlCmd.Parameters.AddRange(sqlParameters);
                        }

                        return oSqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region Get data

        /// <summary>
        /// Gets information from database and fill a DataTable with it.
        /// </summary>
        /// <param name="sqlQuery">The query to get the data.</param>
        /// <returns>DataTable object with the data.</returns>
        public DataTable FillDataTable(string sqlQuery, SqlParameter[] sqlParameters)
        {
            try
            {
                oConStringBuilder = new SqlConnectionStringBuilder();
                oConStringBuilder.DataSource = sqlServerInstanceName;
                oConStringBuilder.InitialCatalog = dbName;
                oConStringBuilder.UserID = dbUser;
                oConStringBuilder.Password = dbPassword;
                oConStringBuilder.IntegratedSecurity = useIntegratedSecurity;

                using (oSqlConn = new SqlConnection(oConStringBuilder.ConnectionString))
                {
                    if (oSqlConn.State != ConnectionState.Open)
                    {
                        oSqlConn.Open();
                    }

                    DataTable dt = new DataTable();
                    using (oSqlCmd = new SqlCommand())
                    {
                        oSqlCmd.Connection = oSqlConn;
                        oSqlCmd.CommandText = sqlQuery;

                        if (sqlParameters.Length > 0)
                        {
                            oSqlCmd.Parameters.AddRange(sqlParameters);
                        }

                        using (oSqlDtAdptr = new SqlDataAdapter())
                        {
                            oSqlDtAdptr.SelectCommand = oSqlCmd;

                            oSqlDtAdptr.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlConn.State == ConnectionState.Open)
                {
                    oSqlConn.Close();
                }
            }
        }

        /// <summary>
        /// Gets information from database and fill a DataTable with it into a transaction.
        /// </summary>
        /// <param name="sqlQuery">The query to get the data.</param>
        /// <param name="transaction">The transaction used to perform the query.</param>
        /// <returns>DataTable object with the data.</returns>
        public DataTable FillDataTable(string sqlQuery, SqlParameter[] sqlParameters, IDbTransaction transaction)
        {
            try
            {
                oConStringBuilder = new SqlConnectionStringBuilder();
                oConStringBuilder.DataSource = sqlServerInstanceName;
                oConStringBuilder.InitialCatalog = dbName;
                oConStringBuilder.UserID = dbUser;
                oConStringBuilder.Password = dbPassword;
                oConStringBuilder.IntegratedSecurity = useIntegratedSecurity;

                using (oSqlConn = new SqlConnection(oConStringBuilder.ConnectionString))
                {
                    DataTable dt = new DataTable();
                    using (oSqlCmd = new SqlCommand())
                    {
                        oSqlCmd.Connection = (SqlConnection)transaction.Connection;
                        oSqlCmd.Transaction = (SqlTransaction)transaction;
                        oSqlCmd.CommandText = sqlQuery;

                        if (sqlParameters.Length > 0)
                        {
                            oSqlCmd.Parameters.AddRange(sqlParameters);
                        }

                        using (oSqlDtAdptr = new SqlDataAdapter())
                        {
                            oSqlDtAdptr.SelectCommand = oSqlCmd;

                            oSqlDtAdptr.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion        
    }
}
