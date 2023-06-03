using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using na.admin.DAO;

namespace WebApp.Controllers.Web_User_Role
{
    [Authorize]
    public class UserRole_View_ListController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        ActionPermitionDAO actionpermition = new ActionPermitionDAO();
        UserRoleDAO userRole = new UserRoleDAO();
        NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        public UserRole_View_ListController(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        [Authorize("Authorization")]
        public async Task<IActionResult> Role_View(int page = 1, int pagesize = 10, string ApplicationName = null,  string fullName = null)
        {
            if (page < 1)
                page = 1;
            var result = userRole.getPaginationList(page, pagesize, ApplicationName, fullName);
            var Rid = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "RoleId").Value;
            int RoleId = Convert.ToInt32(Rid);
            result.action = actionpermition.Permissions_RetrieveByRole(RoleId);
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            return View(result);
        }
        [HttpGet]
        public IActionResult Getpage(int page = 1, int pagesize = 10, string ApplicationName = null, string fullName = null)
        {
            if (page < 1)
                page = 1;
            var result = userRole.getPaginationList(page, pagesize, ApplicationName, fullName);
            var Rid = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "RoleId").Value;
            int RoleId = Convert.ToInt32(Rid);
            result.action = actionpermition.Permissions_RetrieveByRole(RoleId);
            return PartialView("_userRolepaginatedpartial", result);
        }
    }
}
