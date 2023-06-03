using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.DAL;
using na.admin.Utility;

namespace na.admin.DAO
{
    public class RolePrivilegeDAO
    {

        #region addRecord
        public Result addRecord(RolePrivilege oRolePrivilege)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.ROLE_PRIVILEGES_Create";
            string strSQL_Value = String.Empty;
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction();

            try
            {
                //Prepares Master Data 
                command.CommandText = strSQL;
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@RoleID", oRolePrivilege.Role.RoleID);
                command.Parameters.AddWithValue("@PrivilegeID", oRolePrivilege.Privilege.PrivilegeID);
                command.Parameters.AddWithValue("@CreatedBy", oRolePrivilege.CreatedBy);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleID= " + oRolePrivilege.Role.RoleID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@PrivilegeID= '" + oRolePrivilege.Privilege.PrivilegeID + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@CreatedBy= '" + oRolePrivilege.CreatedBy + "'";

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();


                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Added Successfully.";
                LogManager.writeToLog("INFORMATION", "RolePrivilegeDAO - addRecord", strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "RolePrivilegeDAO - addRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        #endregion

        #region getList
        public List<RolePrivilege> getList(int iRoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<RolePrivilege> rolePrivilegeList = new List<RolePrivilege>();
            RolePrivilege oRolePrivilege = new RolePrivilege();
            UserRole oRole = new UserRole();
            Privilege oPrivilege = new Privilege();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLE_PRIVILEGES_RetrieveListByRole";
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
                strSQL_Value = strSQL_Value + " @RoleID = " + iRoleID;

                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oRolePrivilege = new RolePrivilege();
                    oPrivilege = new Privilege();
                    oRole = new UserRole();

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oRolePrivilege.Role = oRole;

                    oRolePrivilege.RolePrivilegeID = Common.stringToInt32(reader["RolePrivilegeID"].ToString());
                    
                    oPrivilege.PrivilegeID = reader["PrivilegeID"].ToString();
                    oPrivilege.UserInterfaceName = reader["UserInterfaceName"].ToString();
                    oPrivilege.ActionName = reader["ActionName"].ToString();
                    oRolePrivilege.Privilege = oPrivilege;

                    rolePrivilegeList.Add(oRolePrivilege);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "RolePrivilegeDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "PrivilegeDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return rolePrivilegeList;
        }
        #endregion

        #region updateRecord_Disable
        public Result updateRecord_Disable(RolePrivilege oRolePrivilege)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.ROLE_PRIVILEGES_Disable";
            string strSQL_Value = String.Empty;
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction();
            try
            {
                //Prepares Master Data 
                command.CommandText = strSQL;
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@RolePrivilegeID", oRolePrivilege.RolePrivilegeID);
                command.Parameters.AddWithValue("@LastUpdatedBy", oRolePrivilege.LastUpdatedBy);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RolePrivilegeID= " + oRolePrivilege.RolePrivilegeID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "," + Environment.NewLine + "@LastUpdatedBy= " + oRolePrivilege.LastUpdatedBy;

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Updated Successfully.";

                LogManager.writeToLog("INFORMATION :", "RolePrivilegeDAO - updateRecord", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "RolePrivilegeDAO - updateRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        #endregion

        #region updateRecord_Enable
        public Result updateRecord_Enable(RolePrivilege oRolePrivilege)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.ROLE_PRIVILEGES_Enable";
            string strSQL_Value = String.Empty;
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction();
            try
            {
                //Prepares Master Data 
                command.CommandText = strSQL;
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@RolePrivilegeID", oRolePrivilege.RolePrivilegeID);
                command.Parameters.AddWithValue("@LastUpdatedBy", oRolePrivilege.LastUpdatedBy);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RolePrivilegeID= " + oRolePrivilege.RolePrivilegeID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "," + Environment.NewLine + "@LastUpdatedBy= " + oRolePrivilege.LastUpdatedBy;

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Updated Successfully.";

                LogManager.writeToLog("INFORMATION :", "RolePrivilegeDAO - updateRecord", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "RolePrivilegeDAO - updateRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        #endregion

    }//End of Class
}//End of Namespace
