using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using na.admin.DAO;
using na.admin.Models;

namespace WebApp.Controllers.Web_User_Role
{
    [Authorize]
    public class Role_AddController : Controller
    {
        UserRoleDAO userRole = new UserRoleDAO();
        NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        [Authorize("Authorization")]
        [HttpGet]
        public IActionResult AddRecord()
        {
            UserRole role=new UserRole();
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            return View(role);
        }

        [HttpPost]
        public IActionResult AddRecord(UserRole role)
        {
            var res=userRole.addRecord(role);
            if (res.isSuccess)
            {
                ModelState.Clear();
                ViewBag.message =res.Message.ToString();
                ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
                return View(role);
            }
            ModelState.AddModelError(string.Empty, res.Message.ToString());
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            return View(role);
        }

     }
}
