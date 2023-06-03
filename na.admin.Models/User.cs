namespace na.admin.Models
{
    public class User
    {
        private string m_LoginName;
        private string m_LoginPWD;
        public User()
        {
            m_LoginName = string.Empty;
            m_LoginPWD = string.Empty;
        }

        public int UserID { get; set; }
        public string LoginName
        {
            get { return m_LoginName; }
            set { m_LoginName = value; }
        }
        public string LoginPWD
        {
            get { return m_LoginPWD; }
            set { m_LoginPWD = value; }
        }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public NA_Application NA_Application { get; set; }
        public UserRole Role { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public int LastUpdatedBy { get; set; }
        public int AccessLevel { get; set; }
        public int IsDomainUser { get; set; }
        public string IsDeleted { get; set; }

    }//End of Class
}//End of Namespace