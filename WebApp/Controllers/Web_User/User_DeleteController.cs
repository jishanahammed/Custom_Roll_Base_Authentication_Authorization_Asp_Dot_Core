using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using na.admin.DAO;

namespace WebApp.Controllers.Web_User
{
    [Authorize]
    public class User_DeleteController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserDAO dAO = new UserDAO();
       // ActionPermitionDAO actionpermition = new ActionPermitionDAO();
       // UserRoleDAO userRole = new UserRoleDAO();
       // NA_ApplicationDAO nA_ApplicationDAO = new NA_ApplicationDAO();
        public User_DeleteController(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize("Authorization")]
        [HttpGet]
        public IActionResult delete(int id)
        {
            var res = dAO.deleteRecord(id);
            return Json(res.isSuccess);
        }
    }
}
