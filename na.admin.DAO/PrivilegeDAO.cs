using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.DAL;
using na.admin.Utility;

namespace na.admin.DAO
{
    public class PrivilegeDAO
    {
        #region getList
        public List<Privilege> getList(string strApplicationName, int iRoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<Privilege> privilegeList = new List<Privilege>();
            NA_Application oNA_Application = new NA_Application();
            Privilege oPrivilege = new Privilege();
            RolePrivilege oRolePrivilege = new RolePrivilege();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.PRIVILEGES_RetrieveListByApplication";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@ApplicationName", strApplicationName);
                command.Parameters.AddWithValue("@RoleID", iRoleID);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @ApplicationName = '" + strApplicationName + "'";
                strSQL_Value = strSQL_Value + "," + " @RoleID = " + iRoleID;

                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oPrivilege = new Privilege();
                    oNA_Application = new NA_Application();
                    oRolePrivilege = new RolePrivilege();

                    oPrivilege.PrivilegeID = reader["PrivilegeID"].ToString();
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oPrivilege.NA_Application = oNA_Application;
                    oPrivilege.UserInterfaceName = reader["UserInterfaceName"].ToString();
                    oPrivilege.ActionName = reader["ActionName"].ToString();

                    oRolePrivilege.RolePrivilegeID = Common.stringToInt32(reader["RolePrivilegeID"].ToString());                    

                    if (reader["IsDeleted"].ToString().Contains("1900"))
                        oRolePrivilege.IsDeleted = "0";
                    else
                        oRolePrivilege.IsDeleted = "1";

                    oPrivilege.RolePrivilege = oRolePrivilege;

                    privilegeList.Add(oPrivilege);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "PrivilegeDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "PrivilegeDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return privilegeList;
        }
        #endregion

        #region getAllList
        public List<Privilege> getAllPrivilegeList()
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Privilege oPrivilege = new Privilege();
            List<Privilege> privilegeList = new List<Privilege>();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.PRIVILEGES_RetrieveAllList";
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
                    oPrivilege = new Privilege();

                    oPrivilege.PrivilegeID = reader["PrivilegeID"].ToString();
                    oPrivilege.UserInterfaceName = reader["UserInterfaceName"].ToString();
                    oPrivilege.ActionName = reader["ActionName"].ToString();
                    oPrivilege.PrivilegeName = reader["PrivilegeName"].ToString();

                    privilegeList.Add(oPrivilege);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "PrivilegeDAO - PRIVILEGES_RetrieveList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "PrivilegeDAO - PRIVILEGES_RetrieveList", strSQL_Value, ex.Message);
                throw ex;
            }
            return privilegeList;
        }
        #endregion


        #region getList
        public List<Privilege> getList(int iRoleID)
        {           
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Privilege oPrivilege = new Privilege();
            List<Privilege> privilegeList = new List<Privilege>();            
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.PRIVILEGES_RetrieveListByRole";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@RoleID", iRoleID);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @RoleID = '" + iRoleID + "'";

                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oPrivilege = new Privilege();

                    oPrivilege.PrivilegeID = reader["PrivilegeID"].ToString();
                    oPrivilege.UserInterfaceName = reader["UserInterfaceName"].ToString();
                    oPrivilege.ActionName = reader["ActionName"].ToString();

                    privilegeList.Add(oPrivilege);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "PrivilegeDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "PrivilegeDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return privilegeList;
        }
        #endregion

    }//End of Class
}//End of Namespace