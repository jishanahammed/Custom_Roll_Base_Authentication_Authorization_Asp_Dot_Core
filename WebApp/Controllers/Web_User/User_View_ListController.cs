using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using na.admin.DAO;
using Syncfusion.XlsIO;
using System.Diagnostics;
using WebApp.Models;
using Syncfusion.Drawing;
using na.admin.Models;
using na.admin.Utility;

namespace WebApp.Controllers.Web_User
{
    [Authorize]
    public class User_View_ListController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserDAO dAO = new UserDAO();
        ActionPermitionDAO actionpermition = new ActionPermitionDAO();
        UserRoleDAO userRole=new UserRoleDAO();
        NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        public User_View_ListController(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        [Authorize("Authorization")]
        public async Task<IActionResult> User_View(int page = 1, int pagesize = 10, string ApplicationName = null, int RoleID = 0, string fullName = null, int employeeNo = 0)
        {
            if (page < 1)
                page = 1;
             var result = dAO.getUserByPagination(page, pagesize, ApplicationName, RoleID, fullName, employeeNo);
            var Rid = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "RoleId").Value;
            int RoleId = Convert.ToInt32(Rid);
            result.action =actionpermition.Permissions_RetrieveByRole(RoleId);
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            ViewBag.rolelist = userRole.getList().Select(s => new { Id = s.RoleID, Name = s.RoleName, ApplicationName = s.NA_Application.ApplicationName });

            return View(result);
        }
        [HttpGet]
        public IActionResult Getpage(int page = 1, int pagesize = 10, string ApplicationName = null, int RoleID = 0, string fullName = null, int employeeNo = 0)
        {
            if (page < 1)
                page = 1;
            var result = dAO.getUserByPagination(page, pagesize, ApplicationName, RoleID, fullName, employeeNo);
            var Rid = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "RoleId").Value;
            int RoleId = Convert.ToInt32(Rid);
            result.action = actionpermition.Permissions_RetrieveByRole(RoleId);
            return PartialView("_userpaginatedpartial",result);
        }

       public IActionResult ExcelExprote(string ApplicationName = null, int RoleID = 0, string fullName = null, int employeeNo = 0)
        {
            List<User> user = dAO.userExcleExport(ApplicationName, RoleID, fullName, employeeNo);
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
                        worksheet.Name = "User List";

                        Color excelHeaderColor = Color.FromArgb(182, 138, 53);
                        Color excelLightRowColor = Color.FromArgb(255, 253, 242);
                        Color excelDarkRowColor = Color.FromArgb(255, 253, 242);

                        worksheet.Range["A1:G1"].Merge();
                        worksheet.Range["A1:G1"].RowHeight = 30;
                        worksheet.Range["A1:G1"].ColumnWidth = 30;
                        worksheet.Range["A1:G1"].CellStyle.Font.Bold = true;
                        worksheet.Range["A1:G1"].CellStyle.Font.Size = 15;
                        worksheet.Range["A1"].Text = "All User Expor";
                        worksheet.Range["A1:G1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                        worksheet.Range["A1:G1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                        string strHeaderRowRange = "A2:G2";

                        worksheet.Range["A2"].Text = "SL";
                        worksheet.Range["B2"].Text = "Login Name";

                        worksheet.Range["C2"].Text = "Full Name";
                        worksheet.Range["D2"].Text = "Role Name";
                        worksheet.Range["E2"].Text = "Application Name";

                        worksheet.Range["F2"].Text = "Employee No";
                        worksheet.Range["G2"].Text = "Remarks";
                     


                        worksheet.Range["A2"].ColumnWidth = 11;
                        worksheet.Range["B2"].ColumnWidth = 20;

                        worksheet.Range["C2"].ColumnWidth = 30;
                        worksheet.Range["D2"].ColumnWidth = 25;
                        worksheet.Range["E2"].ColumnWidth = 35;

                        worksheet.Range["F2"].ColumnWidth = 11;
                        worksheet.Range["G2"].ColumnWidth = 30;
                       

                        for (i = 2; i < user.Count + 2; i++)
                        {
                            User oHospitalDivert = new User();
                            oHospitalDivert = user[i - 2];

                            worksheet.Range["A" + (i + 1)].Number = (i - 1);
                            worksheet.Range["B" + (i + 1)].Text = oHospitalDivert.LoginName.ToString();

                            worksheet.Range["C" + (i + 1)].Value = oHospitalDivert.FullName.ToString();
                            worksheet.Range["D" + (i + 1)].Value = oHospitalDivert.Role.RoleName.ToString();
                            worksheet.Range["E" + (i + 1)].Value = oHospitalDivert.NA_Application.ApplicationName;

                            worksheet.Range["F" + (i + 1)].Value = oHospitalDivert.EmployeeNo;
                            worksheet.Range["G" + (i + 1)].Value = oHospitalDivert.Remarks;
                            worksheet.Range[$"A{i + 1}:G{i + 1}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                            worksheet.Range[$"A{i + 1}:G{i + 1}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                            worksheet.Range[$"A{i + 1}:G{i + 1}"].ColumnWidth = 30; ;

                        }
                        worksheet.Range[strHeaderRowRange].CellStyle.Color = excelHeaderColor;
                        worksheet.Range[strHeaderRowRange].CellStyle.Font.Color = ExcelKnownColors.White;
                        worksheet.Range[strHeaderRowRange].CellStyle.Font.Bold = true;
                        worksheet.Range[strHeaderRowRange].CellStyle.Font.Size = 13;
                        worksheet.Range[strHeaderRowRange].RowHeight = 35;

                        worksheet.Range[strHeaderRowRange].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.White;
                        worksheet.Range[strHeaderRowRange].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.White;

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
