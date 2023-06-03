using System.Collections.Generic;


namespace na.admin.Models
{
    public class UserRole
    {
        private List<Privilege> m_PrivilegeList;
        public UserRole()
        {
            m_PrivilegeList = new List<Privilege>();
        }
        public NA_Application NA_Application { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Remarks { get; set; }
        public List<Privilege> PrivilegeList
        {
            get { return m_PrivilegeList; }
            set { m_PrivilegeList = value; }
        }

    }//End of Class
}//End of Namespace
