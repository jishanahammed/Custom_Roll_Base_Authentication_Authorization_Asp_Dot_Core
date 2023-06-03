using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using na.admin.DAO;
using na.admin.Models;
using WebApp.ConfigService;

namespace WebApp.Controllers.Web_User_Role
{
    [Authorize]
    public class Role_ModifyController : Controller
    {
        private readonly ICheckRolePrivilege checkRole;
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserRoleDAO userRole = new UserRoleDAO();
        NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        ActionPermitionDAO action=new ActionPermitionDAO();
        public Role_ModifyController(ICheckRolePrivilege checkRole, IHttpContextAccessor httpContextAccessor)
        {
            this.checkRole = checkRole;
            _httpContextAccessor = httpContextAccessor; 
        }
        [Authorize("Authorization")]
        [HttpGet]
        public IActionResult update(int id)
        {
            UserRole role = new UserRole();
            role = userRole.getByID(id);
            ViewBag.privilige = checkRole.Getmapdata(id);
            ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
            return View(role);
        }
        [HttpPost]
        public IActionResult update(UserRole role)
        {
            var res = userRole.updateRecord(role);
            if (res.isSuccess)
            {
                ModelState.Clear();              
                ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
                return RedirectToAction("update", new { id = role.RoleID });
            }
            {
                ModelState.AddModelError(string.Empty, res.Message.ToString());
                ViewBag.applicationlist = new SelectList((nA_ApplicationDAO.getList()).Select(s => new { Id = s.ApplicationName, Name = s.ApplicationName }), "Id", "Name");
                return View(role);

            }
        }
        [HttpGet]
        public IActionResult ActiveInactive(string id, int roleId)
        {
            var UId = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "UserId").Value;
            int UserId = Convert.ToInt32(UId);
            var priv = action.ROLE_PRIVILEGES_GetBy_RolePrivilige(id,roleId);
            if (priv.RolePrivilegeID==0)
            {

                var result = action.CreateRolePrivilige(id, roleId, UserId);
                return Json(result.isSuccess);

            }
            var res = action.assigenprivilige(id, roleId);
            return Json(res.isSuccess);
        }

    }
}
