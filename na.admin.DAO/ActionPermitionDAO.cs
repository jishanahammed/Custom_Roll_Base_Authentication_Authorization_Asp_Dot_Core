using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.DAL;
using na.admin.Utility;
using System.Security.Policy;
using System.Reflection.PortableExecutable;
using System.Reflection;

namespace na.admin.DAO
{
    public class ActionPermitionDAO
    {
        public List<ActionViewModel> Permissions_RetrieveByRole(int iRoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            List<ActionViewModel> actionslist = new List<ActionViewModel>();
            ActionViewModel model=new ActionViewModel();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.Action_Permissions_RetrieveByRole";
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
                strSQL_Value = strSQL_Value + "@RoleID= " + iRoleID;
                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    model = new ActionViewModel();

                    model.ActionName = reader["ActionName"].ToString();
                    model.ControllerName = reader["ControllerName"].ToString();
                    model.url = reader["url"].ToString();
                    actionslist.Add(model); 
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "ActionPermitionDAO - Permissions_RetrieveByRole", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "ActionPermitionDAO - Permissions_RetrieveByRole", strSQL, ex.Message);
                throw ex;
            }
            return actionslist;
        }
        public List<UserMenuPermitionVM> Menu_RetrieveByRole(int iRoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            List<UserMenuPermitionVM> actionslist = new List<UserMenuPermitionVM>();
            UserMenuPermitionVM model = new UserMenuPermitionVM();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.Menu_Permissions_RetrieveByRole";
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
                strSQL_Value = strSQL_Value + "@RoleID= " + iRoleID;
                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    model = new UserMenuPermitionVM();

                    model.ActionName = reader["ActionName"].ToString();
                    model.ControllerName = reader["ControllerName"].ToString();
                    model.UiNmae = reader["UserInterfaceName"].ToString();
                    actionslist.Add(model);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "ActionPermitionDAO - Menu_RetrieveByRole", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "ActionPermitionDAO - Menu_RetrieveByRole", strSQL, ex.Message);
                throw ex;
            }
            return actionslist;
        }
        public List<RolePrivilegeViewModel> RolePrivilegeByRole(int iRoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            List<RolePrivilegeViewModel> actionslist = new List<RolePrivilegeViewModel>();
            RolePrivilegeViewModel model = new RolePrivilegeViewModel();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.Role_PriviligeListByRoleId";
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
                strSQL_Value = strSQL_Value + "@RoleID= " + iRoleID;
                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    model = new RolePrivilegeViewModel();

                    model.ActionName = reader["ActionName"].ToString();
                    model.PrivilegeName = reader["PrivilegeName"].ToString();
                    model.UserInterfaceName = reader["UserInterfaceName"].ToString();
                    model.PrivilegeID = reader["PrivilegeID"].ToString();
                    model.RolePrivilegeID = Common.stringToInt32(reader["RolePrivilegeID"].ToString());
                    model.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    actionslist.Add(model);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "ActionPermitionDAO - Menu_RetrieveByRole", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "ActionPermitionDAO - Menu_RetrieveByRole", strSQL, ex.Message);
                throw ex;
            }
            return actionslist;
        }
        public Result assigenprivilige(string id, int RoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            Result oResult = new Result();
            string strSQL = "ROLE_PRIVILEGES_Assigen";
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

                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@PrivilegeID", id);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleID= " + RoleID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@PrivilegeID= '" + id + "'," + Environment.NewLine;

                //##############################################################################################################################

                //Executes Database Operation
               command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Updated Successfully.";

                LogManager.writeToLog("INFORMATION :", "ActionPermitionDAO - assigenprivilige", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "ActionPermitionDAO - assigenprivilige", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        public Result CreateRolePrivilige(string id, int RoleID,int UserId)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            Result oResult = new Result();
            string strSQL = "ROLE_PRIVILEGES_Create";
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
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@PrivilegeID", id);
                command.Parameters.AddWithValue("@CreatedBy", UserId);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleID= " + RoleID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@PrivilegeID= '" + id + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@CreatedBy= '" + UserId + "'," + Environment.NewLine;

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();


                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Added Successfully.";
                LogManager.writeToLog("INFORMATION", "UserDAO - addRecord", strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "UserDAO - addRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }

        public RolePrivilege ROLE_PRIVILEGES_GetBy_RolePrivilige(string id, int RoleID)
        {

            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            RolePrivilege rolePrivilege = new RolePrivilege();    
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLE_PRIVILEGES_GetBy_RolePrivilige";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@PrivilegeID", id);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleID= " + RoleID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@PrivilegeID= '" + id + "'," + Environment.NewLine;

                //##############################################################################################################################

                //Executes Database Operation
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    rolePrivilege.RolePrivilegeID = Common.stringToInt32(reader["RolePrivilegeID"].ToString());                  
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "ActionPermitionDAO - ROLE_PRIVILEGES_GetBy_RolePrivilige", strSQL, "");
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR :", "ActionPermitionDAO - ROLE_PRIVILEGES_GetBy_RolePrivilige", strSQL_Value, ex.Message);
                throw ex;
            }
            return rolePrivilege;
        }
    }
}
