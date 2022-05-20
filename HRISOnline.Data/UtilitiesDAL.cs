using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HRISOnline.Objects;
using System.Security.Cryptography;


namespace HRISOnline.Data
{
    public static class UtilitiesDAL
    {
        //SIAO ADDED

        public static bool canApproveOfEmployee201(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            bool result = false;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetApproverOfEmployee201";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                                result = true;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }


     

        //END OF SIAO ADDED


        public static string GetSingleData(string strQuery)
        {
            var dbMgr = new dbManager();
            string result = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strQuery;

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result = rdr[0].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return result;
        }

        public static bool isRecordExists(string strTable, string strColumn, string strCriteria) 
        {
            var dbMgr = new dbManager();
            string query = "SELECT " + strColumn + " FROM " + strTable + " WHERE " + strCriteria;
            bool result = false;

                               

            try {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = query;

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                                result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;                
            }

            return result;
        }

        public static List<ComboBoxSource> GetDataCombo(string strQuery)
        {
            var dbMgr = new dbManager();
            var source = new List<ComboBoxSource>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strQuery;

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var data = new ComboBoxSource() { 
                                    ValueMember = rdr[0],
                                    DisplayMember = rdr[1].ToString()
                                };

                                source.Add(data);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return source;
        }

        public static ItemCount GetDataItemCount(int intMstCompany, string codeMstBranch, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var itmCount = new ItemCount();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetCountOfForApprovals]";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                itmCount.OvertimeCount = Convert.ToInt32(rdr["OvertimeCount"]);
                                itmCount.LeaveCount = Convert.ToInt32(rdr["LeaveCount"]);
                                //itmCount.CoopLoanCount = Convert.ToInt32(rdr["LoanCount"]);
                                //itmCount.BDayCount = Convert.ToInt32(rdr["BDayCount"]);
                                //itmCount.GatePassCount = Convert.ToInt32(rdr["GatePassCount"]);
                                itmCount.DTRAdjCount = Convert.ToInt32(rdr["DTRAdjCount"]);
                                itmCount.OBCount = Convert.ToInt32(rdr["OBCount"]);
                                itmCount.PBCount = Convert.ToInt32(rdr["PBCount"]);
                                itmCount.FinanceOTCount = Convert.ToInt32(rdr["FinanceOTCount"]);
                                itmCount.EmpUpdateCount = Convert.ToInt32(rdr["UpdateEmployeeCount"]);
                                itmCount.MissingPunchCount = Convert.ToInt32(rdr["MissingPunchCount"]);
                                itmCount.OTMealsCount = Convert.ToInt32(rdr["OTMealsCount"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return itmCount;
        }

        public static DataTable GetReportSource(string strSQL)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL;

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }

            return dt;
        }

        public static string GetSHA1(string Input)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] ipBytes = Encoding.Default.GetBytes(Input.ToCharArray());
            byte[] opBytes = sha1.ComputeHash(ipBytes);

            StringBuilder stringBuilder = new StringBuilder(40);
            for (int i = 0; i < opBytes.Length; i++)
            {
                stringBuilder.Append(opBytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string DisapproveTransaction(int intTransType, DataTable dt, string Username, string disapproveReason)
        {
            var dbMgr = new dbManager();
            string result = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Online_DisapproveTrans", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@intTransType", intTransType));
                        cmd.Parameters.Add(new SqlParameter("@DisapprovedBy", Username));
                        cmd.Parameters.Add(new SqlParameter("@Trans", dt));
                        cmd.Parameters.Add(new SqlParameter("@Reason", disapproveReason));

                        conn.Open();
                        result = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return result;
        }

        public static MessageDates GetMessageDates()
        {
            var dbMgr = new dbManager();
            var data = new MessageDates();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblOlnMessage";

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                data.dateStart = Convert.ToDateTime(rdr[0]);
                                data.dateEnd = Convert.ToDateTime(rdr[1]);
                            }
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<EmpApprovalInfo> GetApprovalInfo(int intOlnId, string intMstEmpPersonal, string AppType)
        {
            var dbMgr = new dbManager();
            List<EmpApprovalInfo> approvers = new List<EmpApprovalInfo>();
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ApprovalInfo";
                        cmd.Parameters.Add(new SqlParameter("@intOlnApplicationId", intOlnId));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@AppType", AppType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                approvers = Utilities.Functions.DataTableToList<EmpApprovalInfo>(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return approvers;
        }
    }
}
