using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using na.admin.DAO;
using na.admin.Models;

namespace WebApp.Controllers.Web_User
{
    [Authorize]
    public class User_ModifyController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserDAO dAO = new UserDAO();
        // ActionPermitionDAO actionpermition = new ActionPermitionDAO();
        UserRoleDAO userRole = new UserRoleDAO();
        NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        public User_ModifyController(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        [Authorize("Authorization")]
        [HttpGet]
        public IActionResult update(int userId)
        {
            User user = dAO.getByID(userId);

            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            ViewBag.rolelist = new SelectList((userRole.getByApplication(user.NA_Application.ApplicationName)).Select(s => new { Id = s.RoleID, Name = s.RoleName }), "Id", "Name");
            return View(user);
        }
        [HttpPost]
        public IActionResult update(User user)
        {
            var res=dAO.updateRecord(user);
            if (res.isSuccess)
            {
                ViewBag.message=res.Message.ToString();
                ViewBag.rolelist = new SelectList((userRole.getByApplication(user.NA_Application.ApplicationName)).Select(s => new { Id = s.RoleID, Name = s.RoleName }), "Id", "Name");
                return View(user);
            }
            ModelState.AddModelError(string.Empty, res.Message.ToString());
            ViewBag.rolelist = new SelectList((userRole.getByApplication(user.NA_Application.ApplicationName)).Select(s => new { Id = s.RoleID, Name = s.RoleName }), "Id", "Name");
            return View(user);
        }
    }
}
