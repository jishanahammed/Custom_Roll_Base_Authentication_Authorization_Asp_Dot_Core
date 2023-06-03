using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using na.admin.Models;
using System.Security.Claims;
using WebApp.Models;
using na.admin.DAO;

namespace WebApp.Controllers
{
    public class loginController : Controller
    {
        UserDAO dAO = new UserDAO();
        UserRoleDAO userRole = new UserRoleDAO();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            User user = dAO.getByLoginName(model.UserName,model.Password,null);
            if (user == null)
            {            
                return View(model);
            }

            if (user.LoginPWD == model.Password)
            {
                var role = userRole.getByID(user.Role.RoleID);
                var claims = new List<Claim> {
                new Claim("LoginName" ,user.LoginName),
                new Claim("FullName" ,user.FullName),
                new Claim("Role" ,role.RoleName),
                new Claim("RoleId" ,role.RoleID.ToString()),
                new Claim("UserId" ,user.UserID.ToString()),
                };
                var identity = new ClaimsIdentity(
                   claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var prop = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, prop).Wait();              
                return Redirect("/Admin/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User is not verified");
                return View(model);
            }

        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/login/Index");
        }
    }
}
