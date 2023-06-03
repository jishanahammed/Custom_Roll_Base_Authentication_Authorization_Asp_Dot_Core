using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.DAL;
using na.admin.Utility;

namespace na.admin.DAO
{
    public class UserRoleDAO
    {
        #region getPaginationList
        public PagedModel<UserRole> getPaginationList(int page, int pageSize, string ApplicationName, string RoleName)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<UserRole> roleList = new List<UserRole>();
            UserRole oRole = new UserRole();
            NA_Application oNA_Application;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLES_RetrievePaginationList";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@ApplicationName",ApplicationName);
                command.Parameters.AddWithValue("@RoleName", RoleName);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + " @ApplicationName = " + ApplicationName + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @RoleName = " + RoleName + Environment.NewLine;

                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oRole = new UserRole();
                    oNA_Application = new NA_Application();

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oRole.NA_Application = oNA_Application;
                    oRole.RoleName = reader["RoleName"].ToString();
                    oRole.Remarks = reader["Remarks"].ToString();
                    roleList.Add(oRole);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "RoleDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "RoleDAO - ROLES_RetrievePaginationList", strSQL_Value, ex.Message);
                throw ex;
            }
            int resCount = roleList.Count();
            var pagers = new PagedList(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var pagedList = roleList.Skip(recSkip).Take(pagers.PageSize).ToList();
            int FirstSerialNumber = ((page * pageSize) - pageSize) + 1;
            PagedModel<UserRole> pagedModel = new PagedModel<UserRole>()
            {
                Models = pagedList,
                FirstSerialNumber = FirstSerialNumber,
                PagedList = pagers,
                TotalEntity = resCount,
            };
            return pagedModel;

        }
        #endregion
        #region getList
        public List<UserRole> getList()
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<UserRole> roleList = new List<UserRole>();
            UserRole oRole = new UserRole();
            NA_Application oNA_Application;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLES_RetrieveList";
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
                    oRole = new UserRole();
                    oNA_Application = new NA_Application();

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oRole.NA_Application = oNA_Application;
                    oRole.RoleName = reader["RoleName"].ToString();

                    roleList.Add(oRole);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "RoleDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "RoleDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return roleList;
        }
        #endregion

        #region getList
        public List<UserRole> getList(UserRole oRole)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<UserRole> roleList = new List<UserRole>();
            NA_Application oNA_Application;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLES_RetrieveList";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                if (oRole.NA_Application.ApplicationName != "")
                    command.Parameters.AddWithValue("@ApplicationName", oRole.NA_Application.ApplicationName);

                if (oRole.RoleName != "")
                    command.Parameters.AddWithValue("@RoleName", oRole.RoleName);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;


                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oRole = new UserRole();
                    oNA_Application = new NA_Application();

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oRole.NA_Application = oNA_Application;
                    oRole.RoleName = reader["RoleName"].ToString();

                    roleList.Add(oRole);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "RoleDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "RoleDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return roleList;
        }
        #endregion

        #region getByID
        public UserRole getByID(int iRoleID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            NA_Application oNA_Application;
            UserRole oRole = new UserRole();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLES_RetrieveByID";
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
                    oRole = new UserRole();
                    oNA_Application = new NA_Application();
                    
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oRole.RoleName = reader["RoleName"].ToString();
                    oRole.NA_Application = oNA_Application;

                    oRole.Remarks = reader["Remarks"].ToString();
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "UserRoleDAO - getByID", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserRoleDAO - getByID", strSQL, ex.Message);
                throw ex;
            }
            return oRole;
        }
        #endregion
        #region getByID
        public List<UserRole> getByApplication(string app)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            List<UserRole> roleList = new List<UserRole>();
            UserRole oRole = new UserRole();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.ROLES_RetrieveByApplication";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@ApplicationName", app);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@ApplicationName= " + app;
                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oRole = new UserRole();

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oRole.RoleName = reader["RoleName"].ToString();
                    oRole.Remarks = reader["Remarks"].ToString();
                    roleList.Add(oRole);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "UserRoleDAO - getByApplication", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserRoleDAO - getByApplication", strSQL, ex.Message);
                throw ex;
            }
            return roleList;
        }
        #endregion
        #region addRecord
        public Result addRecord(UserRole oRole)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.ROLES_Create";
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

                command.Parameters.AddWithValue("@RoleName", oRole.RoleName);
                command.Parameters.AddWithValue("@ApplicationName", oRole.NA_Application.ApplicationName);
                if (oRole.Remarks != string.Empty)
                    command.Parameters.AddWithValue("@Remarks", oRole.Remarks);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleName= '" + oRole.RoleName + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@ApplicationName= '" + oRole.NA_Application.ApplicationName + "'";
                if (oRole.Remarks != string.Empty)
                    strSQL_Value = strSQL_Value + Environment.NewLine + ", " +"@Remarks= '" + oRole.Remarks + "'" ;

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
        #endregion

        #region updateRecord
        public Result updateRecord(UserRole oRole)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.ROLES_Update";
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

                command.Parameters.AddWithValue("@RoleID", oRole.RoleID);

                if (oRole.RoleName != string.Empty)
                    command.Parameters.AddWithValue("@RoleName", oRole.RoleName);

                if (oRole.Remarks != string.Empty)
                    command.Parameters.AddWithValue("@Remarks", oRole.Remarks);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleID= " + oRole.RoleID + "," + Environment.NewLine;

                if (oRole.RoleName != string.Empty)
                    strSQL_Value = strSQL_Value + "," + Environment.NewLine + "@RoleName= '" + oRole.RoleName + "'";

                if (oRole.Remarks != string.Empty)
                    strSQL_Value = strSQL_Value + "," + Environment.NewLine + "@Remarks= '" + oRole.Remarks + "'";

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Updated Successfully.";

                LogManager.writeToLog("INFORMATION :", "UserRoleDAO - updateRecord", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "UserRoleDAO - updateRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        #endregion
        public Result deleteRecord(int roleId)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.ROLES_Soft-Delete";
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
                DateTime date = DateTime.UtcNow;

                command.Parameters.AddWithValue("@RoleID", roleId);
                command.Parameters.AddWithValue("@IsDelete", date);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@RoleID= " + roleId + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@IsDelete= '" + date + "'," + Environment.NewLine;

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Delete Successfully.";

                LogManager.writeToLog("INFORMATION :", "UserRoleDAO - deleteRecord", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "UserRoleDAO - deleteRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
    }//End of Class
}//End of Namespace
