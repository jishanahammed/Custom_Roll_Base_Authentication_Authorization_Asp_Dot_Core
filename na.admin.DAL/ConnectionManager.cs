using System;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.Utility;

namespace na.admin.DAL
{
    public static class ConnectionManager
    {
        public static SqlConnection Conn = new SqlConnection();

        public static string g_strSupportingDocumentPath = string.Empty;
        public static SqlConnection getSqlConnection()
        {
            try
            {
                if (Conn.State.ToString().Equals("Closed"))
                {
                    string strHostName = string.Empty;
                    string strDatabase = string.Empty;
                    string strUserName = string.Empty;
                    string strPassword = string.Empty;

                    strHostName = System.Configuration.ConfigurationManager.AppSettings["HostName"];
                    strDatabase = System.Configuration.ConfigurationManager.AppSettings["Database"];
                    strUserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                    strPassword = System.Configuration.ConfigurationManager.AppSettings["Password"];

                    string connectionString = null;

                    connectionString = "SERVER=" + strHostName + ";UID=" + strUserName + ";PWD=" + strPassword + ";DATABASE=" + strDatabase;
                    Conn.ConnectionString = connectionString;

                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR-DBConection", "ConnectionManager", "", ex.Message);
                throw ex;
            }

            return Conn;
        }

        public static SqlConnection getSqlConnection(DatabaseParams pParams)
        {
            try
            {
                if (Conn.State.ToString().Equals("Closed"))
                {
                    string connectionString = null;

                    connectionString = "SERVER=" + pParams.DatabaseServer + ";UID=" + pParams.DatabaseUser + ";PWD=" + pParams.DatabaseUserPwd + ";DATABASE=" + pParams.DatabaseName;
                    Conn.ConnectionString = connectionString;

                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR-DBConection", "ConnectionManager", "", ex.Message);
                throw ex;
            }

            return Conn;
        }


        public static SqlConnection getSqlConnection_WinAuth()
        {
            try
            {
                if (Conn.State.ToString().Equals("Closed"))
                {
                    string connectionString = null;

                    //connectionString = "SERVER=" + pParams.DatabaseServer + ";Integrated Security=true;" + "DATABASE=" + pParams.DatabaseName;
                    connectionString = "Server=ERP-5\\SQLEXPRESS;Database=NA_MIS;Trusted_Connection=True;MultipleActiveResultSets=true";
                    Conn.ConnectionString = connectionString;

                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR-DBConection", "ConnectionManager", "", ex.Message);
                throw ex;
            }

            return Conn;
        }


        public static SqlConnection getSqlConnection_eCBD()
        {
            try
            {
                if (Conn.State.ToString().Equals("Closed"))
                {
                    string strHostName = string.Empty;
                    string strDatabase = string.Empty;
                    string strUserName = string.Empty;
                    string strPassword = string.Empty;

                    strHostName = System.Configuration.ConfigurationManager.AppSettings["HostName_eCBD"];
                    strDatabase = System.Configuration.ConfigurationManager.AppSettings["Database_eCBD"];
                    strUserName = System.Configuration.ConfigurationManager.AppSettings["UserName_eCBD"];
                    strPassword = System.Configuration.ConfigurationManager.AppSettings["Password_eCBD"];

                    string connectionString = null;

                    connectionString = "SERVER=" + strHostName + ";UID=" + strUserName + ";PWD=" + strPassword + ";DATABASE=" + strDatabase;
                    Conn.ConnectionString = connectionString;

                    Conn.Open();
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR-DBConection", "ConnectionManager", "", ex.Message);
                throw ex;
            }

            return Conn;
        }

    }//End of Class
}//End of Namespace