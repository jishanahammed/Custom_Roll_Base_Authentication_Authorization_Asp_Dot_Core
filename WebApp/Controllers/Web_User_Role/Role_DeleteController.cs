using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using na.admin.DAO;

namespace WebApp.Controllers.Web_User_Role
{
    [Authorize]
    public class Role_DeleteController : Controller
    {
        UserRoleDAO userRole = new UserRoleDAO();
        [Authorize("Authorization")]
        [HttpGet]
        public IActionResult delete(int id)
        {
            var res = userRole.deleteRecord(id);
            return Json(res.isSuccess);
        }
    }
}
