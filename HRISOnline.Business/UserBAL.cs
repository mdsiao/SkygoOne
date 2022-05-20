using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using HRISOnline.Data;

namespace HRISOnline.Business
{
    public static class UserBAL
    {
        public static sysUser AuthenticateUser(string username, string password)
        {

            var resultUser = UserDAL.AuthenticateUser(username, password);

            if (resultUser.Username == "" || resultUser.Username == null)
            {
                throw new Exception("Username and Password is not valid.");
            }
            else
            {
                if (resultUser.Status == true)
                {
                    return resultUser;
                }
                else
                {
                    throw new Exception("User is not active.");
                }
            }
            
        }

        public static sysUser AuthenticateUser(string username)
        {
            var resultUser = UserDAL.AuthenticateUser(username);

            if (resultUser.Username == "" || resultUser.Username == null)
            {
                throw new Exception("User is not found.");
            }
            else
            {
                if (resultUser.Status == true)
                {
                    return resultUser;
                }
                else
                {
                    throw new Exception("User is not active.");
                }
            }
        }

        public static string ChangePassword(ChangePassword passwords)
        {
            int limiter = 30;

            if ((passwords.ConfirmPassword == null) || (passwords.CurrentPassword == null) || (passwords.NewPassword == null))
            {
                throw new Exception("Please fill all the fields below.");
            }
            if (passwords.CorrectPassword != passwords.CurrentPassword)
            {
                throw new Exception("Current Password is not correct.");
            }
            if (passwords.NewPassword != passwords.ConfirmPassword)
            {
                throw new Exception("Confirm Password is not the same with New Password.");
            }
            if (passwords.userPassComplexity < limiter)
            {
                throw new Exception("New Password should have " + limiter + "% security level.");
            }

            passwords.NewPassword = UtilitiesBAL.GetSHA1(passwords.Username + passwords.NewPassword);
            return UserDAL.ChangePassword(passwords);

        }
    }
}
