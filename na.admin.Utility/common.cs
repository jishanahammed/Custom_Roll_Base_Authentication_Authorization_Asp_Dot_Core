using System;
using System.Collections;
using System.IO;

using na.admin.Models;

namespace na.admin.Utility
{
    public static class Common
    {
        public static string g_CurrentUser = string.Empty;
        public static User g_oUser = new User();
        public static string g_DisplayMessageSuccess = string.Empty;
        public static string g_strStaffPhotoPath = string.Empty;

        #region handleSingleQuote
        public static string handleSingleQuote(string pValue)
        {
            string strReturnValue = string.Empty;
            try
            {
                strReturnValue = pValue.Replace("'", "''");
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - handleSingleQuote", "", ex.Message);
                throw ex;
            }
            return strReturnValue;
        }
        #endregion

        #region stringToInt32
        public static int stringToInt32(string value)
        {
            int returnValue = 0;
            try
            {
                returnValue = Convert.ToInt32(value);
            }

            catch
            {
                returnValue = 0;
            }
            return returnValue;
        }
        #endregion

        #region stringToFloat
        public static float stringToFloat(string value)
        {
            float returnValue = 0.00f;
            try
            {
                returnValue = float.Parse(value);
            }

            catch
            {
                returnValue = 0.00f;
            }
            return returnValue;
        }
        #endregion

        #region nullToEmptyString
        public static string nullToEmptyString(string value)
        {
            string returnValue = string.Empty;
            try
            {
                returnValue = value;
            }

            catch
            {
                returnValue = string.Empty;
            }
            return returnValue;
        }
        #endregion

        #region getFormattedDateTimeFromDB
        public static string getFormattedDateTimeFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime();
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "")
                {
                    strReturnValue = string.Empty;
                }
                else
                {
                    dtDateTime = Convert.ToDateTime(strDate);
                    strReturnValue = String.Format("{0:dd-MMM-yyyy HH:mm}", dtDateTime);
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getFormattedDateTimeFromDB", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getDateTimeFormattedFromDB
        public static DateTime getDateTimeFormattedFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
            try
            {
                if (strDate != "")
                    dtDateTime = Convert.ToDateTime(strDate);
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getDateTimeFormattedFromDB", strDate, ex.Message);
                throw ex;
            }

            return dtDateTime;
        }
        #endregion

        #region getISOFormattedDateTimeFromDB
        public static string getISOFormattedDateTimeFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime();
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "")
                {
                    strReturnValue = string.Empty;
                }
                else
                {
                    dtDateTime = Convert.ToDateTime(strDate);
                    strReturnValue = String.Format("{0:yyyy-MM-dd HH:mm}", dtDateTime);
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getISOFormattedDateTimeFromDB", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getFormattedDateFromDB
        public static string getISOFormattedDateFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime();
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "")
                {
                    strReturnValue = string.Empty;
                }
                else
                {
                    dtDateTime = Convert.ToDateTime(strDate);
                    strReturnValue = String.Format("{0:yyyy-MM-dd}", dtDateTime);
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getFormattedDateFromDB", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getFormattedDateFromDB
        public static string getFormattedDateFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime();
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "")
                {
                    strReturnValue = string.Empty;
                }
                else
                {
                    dtDateTime = Convert.ToDateTime(strDate);
                    strReturnValue = String.Format("{0:dd-MMM-yyyy}", dtDateTime);
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getFormattedDateFromDB", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getSQLFormattedDateFromDB
        public static string getSQLFormattedDateFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime();
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "")
                {
                    strReturnValue = string.Empty;
                }
                else
                {
                    dtDateTime = Convert.ToDateTime(strDate);
                    strReturnValue = String.Format("{0:yyyy-MM-dd}", dtDateTime);
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getFormattedDateFromDB", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getFormattedTimeFromDB
        public static string getFormattedTimeFromDB(string strDate)
        {
            DateTime dtDateTime = new DateTime();
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "")
                {
                    strReturnValue = string.Empty;
                }
                else
                {
                    dtDateTime = Convert.ToDateTime(strDate);
                    strReturnValue = String.Format("{0:HH:mm}", dtDateTime);
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getFormattedTimeFromDB", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getFormattedDateFromExcel
        public static string getFormattedDateFromExcel(string strDate)
        {
            string strYear = string.Empty;
            string strMonth = string.Empty;
            string strDay = string.Empty;
            string strReturnValue = string.Empty;
            try
            {
                if (strDate == "" || strDate == "NULL")
                {
                    strReturnValue = "1900-01-01";
                }
                else
                {
                    strDay = strDate.Substring(0, 2);
                    strMonth = strDate.Substring(3, 2);
                    strYear = strDate.Substring(6);
                    strReturnValue = strYear + "-" + strMonth + "-" + strDay;
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Common - getFormattedDateFromExcel", strDate, ex.Message);
                throw ex;
            }

            return strReturnValue;
        }
        #endregion

        #region getFormattedDateTime
        public static DateTime getFormattedDateTime(string strDateTime)
        {
            DateTime dtDateTime = new DateTime();
            try
            {

                dtDateTime = Convert.ToDateTime(strDateTime);

                LogManager.writeToLog("INFORMATION", "Utility - getFormattedDate", strDateTime, "");
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Utility - getFormattedDate", strDateTime, ex.Message);
                throw ex;
            }

            return dtDateTime;
        }
        #endregion

        #region getFormattedDateForMSSQL
        public static string getFormattedDateForMSSQL(DateTime dtDate)
        {

            string strdateFormat = string.Empty;
            try
            {
                strdateFormat = String.Format("{0:yyyy-MM-dd}", dtDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdateFormat;
        }
        #endregion

        #region getFormattedDateTimeForMSSQL
        public static string getFormattedDateTimeForMSSQL(DateTime dtDate)
        {

            string strdateFormat = string.Empty;
            try
            {
                strdateFormat = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dtDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdateFormat;
        }
        #endregion

        #region getFormattedDateForMSSQLWithTimeStamp
        public static string getFormattedDateForMSSQLWithTimeStamp(DateTime dtDate)
        {

            string strdateFormat = string.Empty;
            try
            {
                strdateFormat = String.Format("{0:yyyy-MM-dd_HHmmss}", dtDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdateFormat;
        }
        #endregion

        #region getFormattedDateForIncidentNo
        public static string getFormattedDateForIncidentNo(DateTime dtDate)
        {

            string strdateFormat = string.Empty;
            try
            {
                strdateFormat = String.Format("{0:yyyyMMdd}", dtDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdateFormat;
        }
        #endregion

        #region readImageFile Method
        public static byte[] readImageFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            try
            {
                //Use FileInfo object to get file size.
                FileInfo fInfo = new FileInfo(sPath);
                long numBytes = fInfo.Length;

                //Open FileStream to read file
                FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

                //Use BinaryReader to read file stream into byte array.
                BinaryReader br = new BinaryReader(fStream);

                //When you use BinaryReader, you need to supply number of bytes to read from file.
                //In this case we want to read entire file. So supplying total number of bytes.
                data = br.ReadBytes((int)numBytes);
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Utility - readImageFile", "", ex.Message);
                throw ex;
            }

            return data;
        }
        #endregion

        #region createFileExportLocation
        public static string createFileExportLocation(string pFileLocation, string pIncidentNo)
        {
            string strExportLocation = string.Empty;
            string strYear = string.Empty;
            string strMonth = string.Empty;
            string strDay = string.Empty;

            try
            {
                if (pIncidentNo != "")
                {
                    strYear = pIncidentNo.Substring(0, 4);
                    strMonth = pIncidentNo.Substring(4, 2);
                    strDay = pIncidentNo.Substring(6, 2);
                }

                strExportLocation = strYear + @"\" + strYear + @"-" + strMonth + @"\" + strYear + "-" + strMonth + "-" + strDay + @"\" + pIncidentNo;

                LogManager.createDirectory(pFileLocation + strExportLocation);

                strExportLocation = strExportLocation + @"\" + pIncidentNo;

            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Utility - createFileExportLocation", "", ex.Message);
                throw ex;
            }
            return strExportLocation;
        }
        #endregion

        #region getMVAPatientTypeList
        public static ArrayList getMVAPatientTypeList()
        {
            ArrayList alMVAPatientTypeList = new ArrayList();
            try
            {
                alMVAPatientTypeList.Add("Driver");
                alMVAPatientTypeList.Add("Front-Seat Passenger");
                alMVAPatientTypeList.Add("Back-Seat Passenger");
                alMVAPatientTypeList.Add("Pedestrian");
                alMVAPatientTypeList.Add("Motor Cyclist");

                alMVAPatientTypeList.Add("Cyclist");
                alMVAPatientTypeList.Add("Not Observed");
                alMVAPatientTypeList.Add("Other");
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Utility - getMVAPatientTypeList", "", ex.Message);
                throw ex;
            }
            return alMVAPatientTypeList;
        }
        #endregion

        #region relocatePDFFile
        public static string relocatePDFFile(string pSourceFilePath, string pIncidentNo)
        {
            string strReturnValue = string.Empty;
            string strFileUploadLocation = @"N:\Insurance\Claims\MVA\SupportingDocuments\";
            try
            {
                pSourceFilePath = pSourceFilePath + pIncidentNo + ".pdf";
                string strExportFileLocation = Common.createFileExportLocation(strFileUploadLocation, pIncidentNo);
                string strNewFilePath = strFileUploadLocation + strExportFileLocation + "_Supporting_Document" + ".pdf";

                FileInfo fi = new FileInfo(pSourceFilePath);
                LogManager.createDirectory(strFileUploadLocation);

                //Uploads the file to Server
                fi.CopyTo(strNewFilePath, true);
                strReturnValue = strNewFilePath;
            }
            catch (Exception ex)
            {
                strReturnValue = string.Empty;
                LogManager.writeToLog("ERROR", "Utility-Common - relocatePDFFile", "", ex.Message);
            }
            return strReturnValue;
        }
        #endregion

        #region deletePDFFile
        public static void deletePDFFile(string pFileToDelete, string pIncidentNo)
        {

            try
            {
                pFileToDelete = pFileToDelete + pIncidentNo + ".pdf";

                if (File.Exists(pFileToDelete))
                    File.Delete(pFileToDelete);
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "Utility-Common - deletePDFFile", "", ex.Message);
            }
        }
        #endregion

        #region getFormattedDateForMSSQL_MonthDate
        public static string getFormattedDateForMSSQL_MonthDate(DateTime dtDate, string pFirstOrLast)
        {

            string strdateFormat = string.Empty;
            string strYear = string.Empty;
            string strMonth = string.Empty;
            string strDay = string.Empty;

            try
            {
                strYear = dtDate.Year.ToString("D4");
                strMonth = dtDate.Month.ToString("D2");
                strDay = DateTime.DaysInMonth(dtDate.Year, dtDate.Month).ToString();

                if (pFirstOrLast == "First")
                    strdateFormat = strYear + "-" + strMonth + "-01";
                else
                    strdateFormat = strYear + "-" + strMonth + "-" + strDay;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdateFormat;
        }
        #endregion

        #region getFormattedDateForMSSQL_Year
        public static string getFormattedDateForMSSQL_Year(DateTime dtDate, string pFirstOrLast)
        {

            string strdateFormat = string.Empty;
            string strYear = string.Empty;

            try
            {
                strYear = dtDate.Year.ToString("D4");

                if (pFirstOrLast == "First")
                    strdateFormat = strYear + "-01-01";
                else
                    strdateFormat = strYear + "-12-31";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdateFormat;
        }
        #endregion

    }//End of Class
}//End of Namespace