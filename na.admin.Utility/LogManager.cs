using System;
using System.IO;

namespace na.admin.Utility
{
    public static class LogManager
    {
        #region writeToLog
        public static void writeToLog(string strLogType, string strSource, string strSQL, string strMessage)
        {
            try
            {
                string strPath = @"c:\Logs\NAAdmin-App-Log\" + DateTime.Now.ToString("yyyy-MM") + @"\";
                LogManager.createDirectory(strPath);

                string strFileName = DateTime.Now.ToString("yyyy-MM-dd");
                strPath = @strPath + strFileName + ".log";

                if (!File.Exists(strPath))
                {
                    using (StreamWriter sw = File.CreateText(strPath))
                    {
                        sw.WriteLine("Log Type: " + strLogType);
                        sw.WriteLine("");
                        sw.WriteLine("Execution Time: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff"));
                        sw.WriteLine("");
                        sw.WriteLine("Source: " + strSource);
                        sw.WriteLine("");
                        if (strSQL != "")
                        {
                            sw.WriteLine("SQL Query: " + strSQL);
                            sw.WriteLine("");
                        }

                        if (strMessage != "")
                        {
                            sw.WriteLine("Message: " + strMessage);
                        }
                        sw.WriteLine("---------------------------------------------------------------------------------------");
                    }
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("Log Type: " + strLogType);
                    sw.WriteLine("");
                    sw.WriteLine("Execution Time: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff"));
                    sw.WriteLine("");
                    sw.WriteLine("Source: " + strSource);
                    sw.WriteLine("");
                    if (strSQL != "")
                    {
                        sw.WriteLine("SQL Query: " + strSQL);
                        sw.WriteLine("");
                    }

                    if (strMessage != "")
                    {
                        sw.WriteLine("Message: " + strMessage);
                    }
                    sw.WriteLine("---------------------------------------------------------------------------------------");
                }
                using (StreamReader sr = File.OpenText(strPath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region createDirectory
        public static void createDirectory(string strPath)
        {
            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }//End of Class
}//End of Namespace