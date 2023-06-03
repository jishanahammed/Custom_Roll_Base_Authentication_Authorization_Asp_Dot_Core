using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.DAL;
using na.admin.Utility;

namespace na.admin.DAO
{
    public class NA_ApplicationDAO
    {
        #region getList
        public List<NA_Application> getList()
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<NA_Application> applicationList = new List<NA_Application>();
            NA_Application oNA_Application = new NA_Application();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.NA_APPLICATIONS_RetrieveList";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;


                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oNA_Application = new NA_Application();

                    oNA_Application.ApplicationID = Common.stringToInt32(reader["ApplicationID"].ToString());
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();

                    applicationList.Add(oNA_Application);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "NA_ApplicationDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "NA_ApplicationDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return applicationList;
        }
        #endregion

    }//End of Class
}//End of Namespace
