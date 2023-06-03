using Microsoft.AspNetCore.Mvc;
using na.admin.DAO;
using na.admin.Models;
using na.admin.Utility;
using Syncfusion.XlsIO;
using System.Diagnostics;
using WebApp.Models;
using Syncfusion.Drawing;
using System.Configuration;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserDAO dAO = new UserDAO();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            User oUser = dAO.getByID(1);
            return View(oUser);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GenerateExcelReport()
        {
            User user1 = new User();
            NA_Application oNA_Application = new NA_Application();
            UserRole oRole = new UserRole();
            user1.FullName = "";
            List<User> user = dAO.getList(user1);

            int i = 0;

            try
            {
                if (user.Count > 0)
                {
                    //Create an instance of ExcelEngine
                    using (ExcelEngine excelEngine = new ExcelEngine())
                    {
                        //Initialize Application
                        IApplication application = excelEngine.Excel;

                        //Set default application version as Excel 2016
                        application.DefaultVersion = ExcelVersion.Excel2016;

                        //Create a workbook with a worksheet
                        IWorkbook workbook = application.Workbooks.Create(1);

                        //Access first worksheet from workbook instance
                        IWorksheet worksheet = workbook.Worksheets[0];
                        worksheet.Name = "Hospital Divert - " + user1.FullName;

                        Color excelHeaderColor = Color.FromArgb(182, 138, 53);
                        Color excelLightRowColor = Color.FromArgb(255, 253, 242);
                        Color excelDarkRowColor = Color.FromArgb(255, 253, 242);

                        worksheet.Range["A1:O1"].Merge();
                        worksheet.Range["A1:O1"].RowHeight = 30;
                        worksheet.Range["A1:O1"].ColumnWidth = 30;
                        worksheet.Range["A1:O1"].CellStyle.Font.Bold = true;
                        worksheet.Range["A1:O1"].CellStyle.Font.Size = 15;
                        worksheet.Range["A1"].Text = "Hospital Divert Monthly Report for the Year " + user1.FullName;
                        worksheet.Range["A1:O1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                        worksheet.Range["A1:O1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                        string strHeaderRowRange = "A2:O2";

                        worksheet.Range["A2"].Text = "SL";
                        worksheet.Range["B2"].Text = "Hospital Name";

                        worksheet.Range["C2"].Text = "January";
                        worksheet.Range["D2"].Text = "February";
                        worksheet.Range["E2"].Text = "March";

                        worksheet.Range["F2"].Text = "April";
                        worksheet.Range["G2"].Text = "May";
                        worksheet.Range["H2"].Text = "June";

                        worksheet.Range["I2"].Text = "July";
                        worksheet.Range["J2"].Text = "August";
                        worksheet.Range["K2"].Text = "September";

                        worksheet.Range["L2"].Text = "October";
                        worksheet.Range["M2"].Text = "November";
                        worksheet.Range["N2"].Text = "December";

                        worksheet.Range["O2"].Text = "Total";

                        worksheet.Range["A2"].ColumnWidth = 6;
                        worksheet.Range["B2"].ColumnWidth = 45;

                        worksheet.Range["C2"].ColumnWidth = 11;
                        worksheet.Range["D2"].ColumnWidth = 11;
                        worksheet.Range["E2"].ColumnWidth = 11;

                        worksheet.Range["F2"].ColumnWidth = 11;
                        worksheet.Range["G2"].ColumnWidth = 11;
                        worksheet.Range["H2"].ColumnWidth = 11;

                        worksheet.Range["I2"].ColumnWidth = 11;
                        worksheet.Range["J2"].ColumnWidth = 11;
                        worksheet.Range["K2"].ColumnWidth = 11;

                        worksheet.Range["L2"].ColumnWidth = 11;
                        worksheet.Range["M2"].ColumnWidth = 11;
                        worksheet.Range["N2"].ColumnWidth = 11;

                        worksheet.Range["O2"].ColumnWidth = 11;

                        for (i = 2; i < user.Count + 2; i++)
                        {
                          User  oHospitalDivert = new User();
                            oHospitalDivert = user[i - 2];

                            worksheet.Range["A" + (i + 1)].Number = (i - 1);
                            worksheet.Range["B" + (i + 1)].Text = oHospitalDivert.FullName.ToString();

                            worksheet.Range["C" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["D" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["E" + (i + 1)].Value = oHospitalDivert.EmployeeNo;

                            worksheet.Range["F" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["G" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["H" + (i + 1)].Value = oHospitalDivert.EmployeeNo;

                            worksheet.Range["I" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["J" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["K" + (i + 1)].Value = oHospitalDivert.EmployeeNo;

                            worksheet.Range["L" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["M" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["N" + (i + 1)].Value = oHospitalDivert.EmployeeNo;

                            worksheet.Range["O" + (i + 1)].Formula = "=SUM(B" + (i + 1) + " :N" + (i + 1) + ")";


                            worksheet.Range["B" + (i + 2)].Text = "TOTAL:";

                            worksheet.Range["C" + (i + 2)].Formula = "=SUM(C3:C" + (i + 1) + ")";
                            worksheet.Range["D" + (i + 2)].Formula = "=SUM(D3:D" + (i + 1) + ")";
                            worksheet.Range["E" + (i + 2)].Formula = "=SUM(E3:E" + (i + 1) + ")";

                            worksheet.Range["F" + (i + 2)].Formula = "=SUM(F3:F" + (i + 1) + ")";
                            worksheet.Range["G" + (i + 2)].Formula = "=SUM(G3:G" + (i + 1) + ")";
                            worksheet.Range["H" + (i + 2)].Formula = "=SUM(H3:H" + (i + 1) + ")";

                            worksheet.Range["I" + (i + 2)].Formula = "=SUM(I3:I" + (i + 1) + ")";
                            worksheet.Range["J" + (i + 2)].Formula = "=SUM(J3:J" + (i + 1) + ")";
                            worksheet.Range["K" + (i + 2)].Formula = "=SUM(K3:K" + (i + 1) + ")";

                            worksheet.Range["L" + (i + 2)].Formula = "=SUM(L3:L" + (i + 1) + ")";
                            worksheet.Range["M" + (i + 2)].Formula = "=SUM(M3:M" + (i + 1) + ")";
                            worksheet.Range["N" + (i + 2)].Formula = "=SUM(N3:N" + (i + 1) + ")";

                            worksheet.Range["O" + (i + 2)].Formula = "=SUM(O3:O" + (i + 1) + ")";
                            worksheet.Range["O" + (i + 2)].NumberFormat = "###,###";
                            if (i > 1)
                            {
                                if (i % 2 == 0)
                                    worksheet.Range["A" + (i + 1) + ":" + "O" + (i + 1)].CellStyle.Color = excelLightRowColor;
                                else
                                    worksheet.Range["A" + (i + 1) + ":" + "O" + (i + 1)].CellStyle.Color = excelLightRowColor;
                            }
                        }

                        string strBottomTotalRange = "A" + (i + 1) + ":O" + (i + 1);
                        string strRightTotalRange = "O3:O" + (i + 1);
                        string strRange = "A2:0" + i;

                        worksheet.Range[strRange].RowHeight = 30;
                        worksheet.Range[strRange].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        worksheet.Range[strBottomTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strBottomTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strBottomTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strBottomTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        worksheet.Range[strRightTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strRightTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strRightTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range[strRightTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.Grey_25_percent;
                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.Grey_25_percent;
                        worksheet.Range[strRange].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;


                        worksheet.Range[strHeaderRowRange].CellStyle.Color = excelHeaderColor;
                        worksheet.Range[strHeaderRowRange].CellStyle.Font.Color = ExcelKnownColors.White;
                        worksheet.Range[strHeaderRowRange].CellStyle.Font.Bold = true;
                        worksheet.Range[strHeaderRowRange].CellStyle.Font.Size = 13;
                        worksheet.Range[strHeaderRowRange].RowHeight = 35;

                        worksheet.Range[strHeaderRowRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.White;
                        worksheet.Range[strHeaderRowRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.White;

                        worksheet.Range[strBottomTotalRange].CellStyle.Color = excelHeaderColor;
                        worksheet.Range[strBottomTotalRange].CellStyle.Font.Color = ExcelKnownColors.White;
                        worksheet.Range[strBottomTotalRange].CellStyle.Font.Bold = true;
                        worksheet.Range[strBottomTotalRange].CellStyle.Font.Size = 13;
                        worksheet.Range[strBottomTotalRange].RowHeight = 35;
                        worksheet.Range[strBottomTotalRange].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                        worksheet.Range[strBottomTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.White;
                        worksheet.Range[strBottomTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.White;

                        worksheet.Range[strRightTotalRange].CellStyle.Color = excelHeaderColor;
                        worksheet.Range[strRightTotalRange].CellStyle.Font.Color = ExcelKnownColors.White;
                        worksheet.Range[strRightTotalRange].CellStyle.Font.Bold = true;
                        worksheet.Range[strRightTotalRange].CellStyle.Font.Size = 13;

                        worksheet.Range[strRightTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.White;
                        worksheet.Range[strRightTotalRange].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.White;

                        // Auto-fit the columns
                        worksheet.UsedRange.AutofitColumns();

                        // Save the workbook to a memory stream
                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                        {
                            workbook.SaveAs(stream);

                            // Set the response headers
                           Response.Headers.Add("Content-Disposition", "attachment; filename=ExcelReport.xlsx");
                            Response.Headers.Add("Content-Type", "application/octet-stream");
                            Response.Headers.Add("Content-Length", stream.Length.ToString());

                            // Write the workbook data to the response stream
                            stream.Position = 0;
                            stream.CopyTo(Response.Body);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.writeToLog("ERROR", "frmRptHospitalDivert_Monthly - exportDataToExcel", "", ex.Message);
                throw ex;
            }
            return new EmptyResult();
        }//exportDataToExcel
    }
}