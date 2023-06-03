namespace na.admin.Models
{
    public class Privilege
    {
        public Privilege()
        {
        }
        public NA_Application NA_Application { get; set; }
        public string UserInterfaceName { get; set; }
        public string PrivilegeID { get; set; }        
        public string PrivilegeName { get; set; }
        public string ActionName { get; set; }
        public int ActionPrecedence { get; set; }
        public int Precedence { get; set; }
        public RolePrivilege RolePrivilege { get; set; }

    }//End of Class
}//End of Namespace
