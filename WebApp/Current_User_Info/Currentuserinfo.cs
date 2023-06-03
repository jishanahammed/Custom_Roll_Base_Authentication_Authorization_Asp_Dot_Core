using na.admin.DAO;
using na.admin.Utility;

namespace WebApp.Current_User_Info
{
    public interface ICurrentuserinfo
    {
        public InfoVM GetInfo();
    }
    public class Currentuserinfo: ICurrentuserinfo
    {
 
        ActionPermitionDAO action = new ActionPermitionDAO();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Currentuserinfo(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        public InfoVM GetInfo()
        {
            InfoVM vM = new InfoVM();
            vM.LoginName = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "LoginName").Value;
            vM.FullName = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "FullName").Value;
            vM.RoleName = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "Role").Value;
            var userid = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "UserId").Value;
            var Rid = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "RoleId").Value;
            vM.RoleId = Convert.ToInt32(Rid);
            vM.actionslist = action.Menu_RetrieveByRole(vM.RoleId);
            return vM;
        }
    }
    public class InfoVM
    {
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public bool IsRoleActive { get; set; } = true;
        public List<UserMenuPermitionVM> actionslist = new List<UserMenuPermitionVM>();
    }
}
