using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace na.admin.Utility
{
    public class UserViewModel
    {
        public int UserID { set; get; }
        public string LoginName { set; get; }
        public string LoginPWD { set; get; }
        public int EmployeeNo { set; get; }
        public string FullName { set; get; }
        public string Remarks { set; get; }
        public string ApplicationName { set; get; }
        public int RoleID { set; get; }
        public DateTime CreationDate { set; get; }
        public int CreatedBy { set; get; }
        public DateTime LastUpdatedDate { set; get; }
        public int LastUpdatedBy { set; get; }
        public DateTime IsDeleted { set; get; }
        public string sms { set; get; }
        
    }
}
