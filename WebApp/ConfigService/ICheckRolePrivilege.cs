using na.admin.DAO;
using na.admin.Utility;

namespace WebApp.ConfigService
{
    public interface ICheckRolePrivilege
    {
       public  List<RolePrivilegeViewModel> Getmapdata(int roleid);
    }

    public class CheckRolePrivilegeService : ICheckRolePrivilege
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        ActionPermitionDAO  action=new ActionPermitionDAO();
        PrivilegeDAO privilege =new PrivilegeDAO();
        public CheckRolePrivilegeService(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        public List<RolePrivilegeViewModel> Getmapdata(int roleid)
        {
            List<RolePrivilegeViewModel> models = new List<RolePrivilegeViewModel>();   
            var datalist = action.RolePrivilegeByRole(roleid);
            var priv= privilege.getAllPrivilegeList();
            foreach (var item in priv)
            {
                RolePrivilegeViewModel model=new RolePrivilegeViewModel();
                model.PrivilegeID = item.PrivilegeID;
                model.UserInterfaceName = item.UserInterfaceName;
                model.ActionName = item.ActionName;
                model.PrivilegeName = item.PrivilegeName;
                var check = datalist.FirstOrDefault(n => n.PrivilegeID == item.PrivilegeID && n.RoleID == roleid);
                if (check!=null)
                {
                    model.IsAssign= true;
                }
                else
                {
                    model.IsAssign = false;
                }

                models.Add(model);  
            }
          return models;    
        }
    }
}
