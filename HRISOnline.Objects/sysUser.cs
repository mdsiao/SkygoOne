using System;

namespace HRISOnline.Objects
{
    public class sysUser
    {
        public int intOlnUsers { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public bool Status { get; set; }
    }

    public class ChangePassword
    {
        public string Username { get; set; }
        public string CorrectPassword { get; set; }
        public string CurrentPassword { get; set; }        
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int userPassComplexity { get; set; }
    }
}
