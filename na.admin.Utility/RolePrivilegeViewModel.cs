using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace na.admin.Utility
{
    public class RolePrivilegeViewModel
    {

        public int RolePrivilegeID { set; get; }
        public string PrivilegeID { set; get; }
        public int RoleID { set; get; }
        public string ApplicationName { set; get; }
        public string RoleName { set; get; }
        public string UserInterfaceName { set; get; }
        public string PrivilegeName { set; get; }
        public string ActionName { set; get; }
        public int ActionPrecedence { set; get; }
        public string url { set; get; }
        public bool IsAssign { set; get; }
    }
}
