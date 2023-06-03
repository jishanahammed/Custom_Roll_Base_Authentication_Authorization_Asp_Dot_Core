using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

using na.admin.Models;
using na.admin.DAL;
using na.admin.Utility;
using System.Security.Policy;

namespace na.admin.DAO
{
    public class UserDAO
    {
        #region getUserByPagination
        public PagedModel<User> getUserByPagination(int page, int pageSize, string ApplicationName, int RoleID, string fullName, int employeeNo)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            List<User> userList = new List<User>();
            User oUser = new User();
            NA_Application oNA_Application = new NA_Application();
            UserRole oRole = new UserRole();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.USERS_RetrievePaginationList";
            string strSQL_Value = String.Empty;
            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@EmployeeNo", employeeNo);
                command.Parameters.AddWithValue("@FullName", fullName);
                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + " @ApplicationName = " + ApplicationName + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @RoleID = " + RoleID + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @EmployeeNo = " + employeeNo + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @FullName = " + fullName + Environment.NewLine;
                //##############################################################################################################################
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();
                    oNA_Application = new NA_Application();
                    oRole = new UserRole();

                    oUser.UserID = Common.stringToInt32(reader["UserID"].ToString());
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oUser.NA_Application = oNA_Application;
                    oUser.LoginName = reader["LoginName"].ToString();
                    oUser.EmployeeNo = reader["EmployeeNo"].ToString();
                    oUser.FullName = reader["FullName"].ToString();
                    oRole.RoleName = reader["RoleName"].ToString();
                    oUser.Role = oRole;
                    oUser.Remarks= reader["Remarks"].ToString();

                    userList.Add(oUser);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "UserDAO - getUserByPagination", strSQL_Value, "");
            }
            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserDAO - getUserByPagination", strSQL_Value, ex.Message);
                throw ex;
            }
            int resCount = userList.Count();
            var pagers = new PagedList(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var pagedList = userList.Skip(recSkip).Take(pagers.PageSize).ToList();
            int FirstSerialNumber = ((page * pageSize) - pageSize) + 1;
            PagedModel<User> pagedModel = new PagedModel<User>()
            {
                Models = pagedList,
                FirstSerialNumber = FirstSerialNumber,
                PagedList = pagers,
                TotalEntity = resCount,
                //action = privilegemap.Rolepermition()
            };
            return pagedModel;
        }
        #endregion
        #region userExcleExport
        public List<User> userExcleExport(string ApplicationName, int RoleID, string fullName, int employeeNo)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            List<User> userList = new List<User>();
            User oUser = new User();
            NA_Application oNA_Application = new NA_Application();
            UserRole oRole = new UserRole();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.USERS_RetrievePaginationList";
            string strSQL_Value = String.Empty;
            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;
                command.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@EmployeeNo", employeeNo);
                command.Parameters.AddWithValue("@FullName", fullName);
                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + " @ApplicationName = " + ApplicationName + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @RoleID = " + RoleID + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @EmployeeNo = " + employeeNo + Environment.NewLine;
                strSQL_Value = strSQL_Value + " @FullName = " + fullName + Environment.NewLine;
                //##############################################################################################################################
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();
                    oNA_Application = new NA_Application();
                    oRole = new UserRole();

                    oUser.UserID = Common.stringToInt32(reader["UserID"].ToString());
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oUser.NA_Application = oNA_Application;
                    oUser.LoginName = reader["LoginName"].ToString();
                    oUser.EmployeeNo = reader["EmployeeNo"].ToString();
                    oUser.FullName = reader["FullName"].ToString();
                    oRole.RoleName = reader["RoleName"].ToString();
                    oUser.Role = oRole;
                    oUser.Remarks = reader["Remarks"].ToString();

                    userList.Add(oUser);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "UserDAO - userExcleExport", strSQL_Value, "");
            }
            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserDAO - userExcleExport", strSQL_Value, ex.Message);
                throw ex;
            }

            return userList;
        }
        #endregion



        #region getList
        public List<User> getList(User oUser)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            List<User> userList = new List<User>();
            NA_Application oNA_Application = new NA_Application();
            UserRole oRole = new UserRole();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.USERS_RetrieveList";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                if (oUser.NA_Application.ApplicationName != "")
                    command.Parameters.AddWithValue("@ApplicationName", oUser.NA_Application.ApplicationName);

                if (oUser.FullName != "")
                    command.Parameters.AddWithValue("@FullName", oUser.FullName);

                if (oUser.Role.RoleID != 0)
                    command.Parameters.AddWithValue("@RoleID", oUser.Role.RoleID.ToString());

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                if (oUser.NA_Application.ApplicationName != "")
                    strSQL_Value = strSQL_Value + " @ApplicationName = " + oUser.NA_Application.ApplicationName + Environment.NewLine;

                if (oUser.FullName != "")
                    strSQL_Value = strSQL_Value + " @FullName = " + oUser.FullName + Environment.NewLine;

                if (oUser.EmployeeNo != "")
                    strSQL_Value = strSQL_Value + " @EmployeeNo = " + oUser.EmployeeNo + Environment.NewLine;


                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();
                    oNA_Application = new NA_Application();
                    oRole = new UserRole();

                    oUser.UserID = Common.stringToInt32(reader["UserID"].ToString());
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oUser.NA_Application = oNA_Application;
                    //oUser.LoginName = reader["LoginName"].ToString();
                    oUser.EmployeeNo = reader["EmployeeNo"].ToString();
                    oUser.FullName = reader["FullName"].ToString();
                    oRole.RoleName = reader["RoleName"].ToString();
                    oUser.Role = oRole;

                    userList.Add(oUser);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "UserDAO - getList", strSQL_Value, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserDAO - getList", strSQL_Value, ex.Message);
                throw ex;
            }
            return userList;
        }
        #endregion

        #region getInfoByID
        public User getInfoByID(int pEmployeeNo)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            User oUser = new User();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.USERS_RetrieveByEmployeeNo";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@EmployeeNo", pEmployeeNo);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@EmployeeNo= " + pEmployeeNo;
                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();

                    oUser.EmployeeNo = reader["EmployeeNo"].ToString();
                    oUser.FullName = reader["FullName"].ToString();
                    oUser.LoginName = reader["LoginName"].ToString();
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "UserDAO - getInfoByID", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserDAO - getInfoByID", strSQL, ex.Message);
                throw ex;
            }
            return oUser;
        }
        #endregion

        #region getByID
        public User getByID(int iUserID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            User oUser = new User();
            NA_Application oNA_Application;
            UserRole oRole;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            string strSQL = "DBO.USERS_RetrieveByID";
            string strSQL_Value = String.Empty;

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@UserID", iUserID);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@UserID= " + iUserID;
                //##############################################################################################################################

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();
                    oNA_Application = new NA_Application();
                    oRole = new UserRole();

                    oUser.UserID = Common.stringToInt32(reader["UserID"].ToString());
                    oUser.EmployeeNo = reader["EmployeeNo"].ToString();
                    oUser.FullName = reader["FullName"].ToString();
                    oUser.LoginName = reader["LoginName"].ToString();

                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oUser.NA_Application = oNA_Application;

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oRole.RoleName = reader["RoleName"].ToString();
                    oUser.Role = oRole;

                    oUser.Remarks = reader["Remarks"].ToString();
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "UserDAO - getByID", strSQL, "");
            }

            catch (Exception ex)
            {
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("ERROR", "UserDAO - getByID", strSQL, ex.Message);
                throw ex;
            }
            return oUser;
        }
        #endregion

        #region addRecord
        public Result addRecord(User oUser)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.USERS_Create";
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

                command.Parameters.AddWithValue("@LoginName", oUser.LoginName);
                command.Parameters.AddWithValue("@FullName", oUser.FullName);
                command.Parameters.AddWithValue("@EmployeeNo", oUser.EmployeeNo);
                command.Parameters.AddWithValue("@ApplicationName", oUser.NA_Application.ApplicationName);
                command.Parameters.AddWithValue("@RoleID", oUser.Role.RoleID);
                command.Parameters.AddWithValue("@Remarks", oUser.Remarks);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@LoginName= '" + oUser.LoginName + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@FullName= '" + oUser.FullName + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@EmployeeNo= " + oUser.EmployeeNo + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@ApplicationName= '" + oUser.NA_Application.ApplicationName + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@RoleID= " + oUser.Role.RoleID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@Remarks= '" + oUser.Remarks + "'" + Environment.NewLine;

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
        public Result updateRecord(User oUser)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.USERS_Update";
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

                command.Parameters.AddWithValue("@UserID", oUser.UserID);
                command.Parameters.AddWithValue("@ApplicationName", oUser.NA_Application.ApplicationName);
                command.Parameters.AddWithValue("@RoleID", oUser.Role.RoleID);
                if (oUser.Remarks != string.Empty)
                    command.Parameters.AddWithValue("@Remarks", oUser.Remarks);

                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@UserID= " + oUser.UserID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@ApplicationName= '" + oUser.NA_Application.ApplicationName + "'," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@RoleID= " + oUser.Role.RoleID;
                if (oUser.Remarks != string.Empty)
                    strSQL_Value = strSQL_Value + "," + Environment.NewLine + "@Remarks= '" + oUser.Remarks + "'";

                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Updated Successfully.";

                LogManager.writeToLog("INFORMATION :", "UserDAO - updateRecord", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "UserDAO - updateRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        #endregion
        #region deletrRecord
        public Result deleteRecord(int iUserID)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            Result oResult = new Result();
            string strSQL = "DBO.USERS_Soft_Delete";
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

                command.Parameters.AddWithValue("@UserID", iUserID);
                command.Parameters.AddWithValue("@IsDelete", date);


                //##############################################################################################################################
                // SQL Statement for Log
                //##############################################################################################################################

                strSQL_Value = "EXECUTE " + strSQL + Environment.NewLine;

                strSQL_Value = strSQL_Value + "@UserID= " + iUserID + "," + Environment.NewLine;
                strSQL_Value = strSQL_Value + "@IsDelete= '" + date + "'," + Environment.NewLine;
       
                //##############################################################################################################################

                //Executes Database Operation
                command.ExecuteNonQuery();

                transaction.Commit();
                oResult.isSuccess = true;
                oResult.Message = "Information Delete Successfully.";

                LogManager.writeToLog("INFORMATION :", "UserDAO - deleteRecord", strSQL + " " + strSQL_Value, "");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                oResult.isSuccess = false;
                oResult.Message = ex.Message;
                LogManager.writeToLog("ERROR :", "UserDAO - deleteRecord", strSQL_Value, ex.Message);
                throw ex;
            }
            return oResult;
        }
        #endregion
        #region getByLoginName
        public User getByLoginName(string pLoginName, string pLoginPWD, string pApplicationName)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection_WinAuth();
            User oUser = new User();
            NA_Application oNA_Application;
            UserRole oRole;
            SqlCommand command = new SqlCommand();
            string strSQL = "DBO.USERS_RetrieveByLoginName";

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@LoginName", pLoginName);

                if (pLoginPWD != "")
                    command.Parameters.AddWithValue("@LoginPWD", pLoginPWD);

                command.Parameters.AddWithValue("@ApplicationName", pApplicationName);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();
                    oNA_Application = new NA_Application();
                    oRole = new UserRole();

                    oUser.UserID = Common.stringToInt32(reader["UserID"].ToString());
                    oUser.EmployeeNo = reader["EmployeeNo"].ToString();
                    oUser.FullName = reader["FullName"].ToString();
                    oUser.LoginName = reader["LoginName"].ToString();
                    oUser.LoginPWD = reader["LoginPWD"].ToString();
                    oNA_Application.ApplicationName = reader["ApplicationName"].ToString();
                    oNA_Application.ApplicationFullName = reader["ApplicationFullName"].ToString();
                    oUser.NA_Application = oNA_Application;

                    oRole.RoleID = Common.stringToInt32(reader["RoleID"].ToString());
                    oRole.RoleName = reader["RoleName"].ToString();
                    oUser.Role = oRole;

                    oUser.Remarks = reader["Remarks"].ToString();
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                LogManager.writeToLog("INFORMATION", "UserDAO - getByLoginName", strSQL, "");
            }

            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "UserDAO - getByLoginName", strSQL, ex.Message);
                throw ex;
            }
            return oUser;
        }
        #endregion

        #region verifyAppUser
        public User verifyAppUser(string strLoginName, string strApplicationName)
        {
            SqlConnection conn = ConnectionManager.getSqlConnection();
            User oUser = new User();
            SqlCommand command = new SqlCommand();
            string strSQL = "DBO.USER_verifyAppUser";

            try
            {
                command.CommandText = strSQL;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                command.Parameters.AddWithValue("@LoginName", strLoginName);
                command.Parameters.AddWithValue("@ApplicationName", strApplicationName);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    oUser = new User();

                    oUser.LoginName = reader["LoginName"].ToString();
                    oUser.IsDomainUser = Common.stringToInt32(reader["IsDomainUser"].ToString());

                }
                reader.Close();
                reader.Dispose();
                command.Dispose();

                LogManager.writeToLog("INFORMATION", "UserDAO - verifyLogin", strSQL, "");
            }

            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "UserDAO - verifyLogin", strSQL, ex.Message);
                command.Dispose();
                throw ex;
            }
            return oUser;
        }
        #endregion

    }//End of Class
}//End of Namespace