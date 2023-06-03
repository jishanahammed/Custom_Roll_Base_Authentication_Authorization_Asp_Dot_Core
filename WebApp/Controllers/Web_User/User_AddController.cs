using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using na.admin.DAO;
using na.admin.Models;

namespace WebApp.Controllers.Web_User
{
    [Authorize]
    public class User_AddController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserDAO dAO = new UserDAO();
        // ActionPermitionDAO actionpermition = new ActionPermitionDAO();
        UserRoleDAO userRole = new UserRoleDAO();
        NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        public User_AddController(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        [Authorize("Authorization")]
        public IActionResult AddRecord()
        {
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            ViewBag.rolelist = userRole.getList().Select(s => new { Id = s.RoleID, Name = s.RoleName, ApplicationName = s.NA_Application.ApplicationName });
            return View();
        }
        [HttpPost]
        public IActionResult AddRecord(User user)
        {
            var res = dAO.addRecord(user);
            if (res.isSuccess)
            {
                ModelState.Clear();
                ViewBag.message = res.Message.ToString();
                ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
                ViewBag.rolelist = userRole.getList().Select(s => new { Id = s.RoleID, Name = s.RoleName, ApplicationName = s.NA_Application.ApplicationName });
                return View();
            }
            ModelState.AddModelError(string.Empty, res.ToString());
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            ViewBag.rolelist = userRole.getList().Select(s => new { Id = s.RoleID, Name = s.RoleName, ApplicationName = s.NA_Application.ApplicationName });
            return View(user);
        }
    
    }
}
