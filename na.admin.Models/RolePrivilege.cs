
namespace na.admin.Models
{
    public class RolePrivilege
    {
        public RolePrivilege()
        {

        }
        public int RolePrivilegeID { get; set; }
        public UserRole Role { get; set; }
        public Privilege Privilege { get; set; }
        public string CreatedBy { get; set; }
        public string CreationDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public int LastUpdatedDate { get; set; }
        public string IsDeleted { get; set; }

    }//End of Class
}//End of Namespace
