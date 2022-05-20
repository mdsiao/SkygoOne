using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;

namespace HRISOnline.Data
{
    public static class UserDAL
    {

        public static sysUser AuthenticateUser(string username, string password)
        {
            var dbMgr = new dbManager();
            var user = new sysUser();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblOlnUsers";
                        cmd.CommandTimeout = 180;

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (username == rdr["Username"].ToString() && password == rdr["UserPassword"].ToString() && Convert.ToBoolean(rdr["Status"]) == true)
                                {                                    
                                    user.intOlnUsers = Convert.ToInt32(rdr["intOlnUsers"]);
                                    user.intMstEmpPersonal = rdr["EmpIdCode"].ToString();
                                    user.Username = rdr["Username"].ToString();
                                    user.UserPassword = rdr["UserPassword"].ToString();
                                    user.Status = Convert.ToBoolean(rdr["Status"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return user;
        }

        public static sysUser AuthenticateUser(string username)
        {
            var dbMgr = new dbManager();
            var user = new sysUser();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetUser";
                        cmd.Parameters.Add(new SqlParameter("@Username", username));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                user.intOlnUsers = Convert.ToInt32(rdr["intOlnUsers"]);
                                user.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                user.Username = rdr["userName"].ToString();
                                user.UserPassword = rdr["userPass"].ToString();
                                user.Status = Convert.ToBoolean(rdr["Status"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return user;
        }

        public static string ChangePassword(ChangePassword passwords)
        {
            var dbMgr = new dbManager();
            string result = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ChangePassword";
                        cmd.Parameters.Add(new SqlParameter("@Username", passwords.Username));
                        cmd.Parameters.Add(new SqlParameter("@NewPassword", passwords.NewPassword));

                        conn.Open();
                        result = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
