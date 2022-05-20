using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;

namespace HRISOnline.Data
{
    public class LeaveDAL
    {

        public  LeaveMaster GetDataLeaveMaster(int intOlnLeaveApplicationHead)
        {
            var dbMgr = new dbManager();
            var leave = new LeaveMaster();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetLeaveApplications]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnLeaveApplicationHead", intOlnLeaveApplicationHead));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", ""));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                leave.intOlnLeaveApplicationHead = Convert.ToInt32(rdr["intOlnLeaveApplicationHead"]);
                                leave.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                leave.DateFiled = Convert.ToDateTime(rdr["DateFiled"]);
                                leave.YearPeriod = Convert.ToInt32(rdr["YearPeriod"]);
                                leave.intMstLeaveType = Convert.ToInt32(rdr["intMstLeaveType"]);
                                leave.HRLiason = rdr["HRLiason"].ToString();
                                leave.ImmediateSuperior = rdr["ImmediateSuperior"].ToString();
                                leave.AreaSupervisor = rdr["AreaSupervisor"].ToString();
                                leave.HR = rdr["HR"].ToString();
                                leave.Remarks = rdr["Remarks"].ToString();
                                leave.isCancelled = Convert.ToBoolean(rdr["isCancelled"]);
                                leave.isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]);
                                leave.DisapprovedBy = rdr["DisapproveBy"].ToString();
                                leave.DateFrom = Convert.ToDateTime(rdr["DateFrom"]);
                                leave.DateTo = Convert.ToDateTime(rdr["DateTo"]);
                                leave.NoOfDays = Convert.ToDouble(rdr["NoOfDays"]);
                                leave.DateFromAMorPM = Convert.ToInt32(rdr["DateFromAMorPM"]);
                                leave.DateToAMorPM = Convert.ToInt32(rdr["DateToAMorPM"]);                                
                                leave.DisapprovedDateTime = Convert.ToDateTime(rdr["DisapproveDateTime"]);
                                leave.DisapproveByName = rdr["DisapproveByName"].ToString();
                                leave.DisapproveReason = rdr["DisapproveReason"].ToString();
                            }
                        }

                        leave.LeaveDetails = GetDataLeaveDetails(intOlnLeaveApplicationHead);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return leave;
        }

        public  List<LeaveDetails> GetDataLeaveDetails(int intOlnLeaveApplicationHead)
        {
            var dbMgr = new dbManager();
            var leave_det = new List<LeaveDetails>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblOlnLeaveApplicationDetails WHERE intOlnLeaveApplicationHead = @intOlnLeaveApplicationHead";
                        cmd.Parameters.Add(new SqlParameter("@intOlnLeaveApplicationHead", intOlnLeaveApplicationHead));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var det = new LeaveDetails 
                                {
                                    intOlnLeaveApplicationDetails = Convert.ToInt32(rdr["intOlnLeaveApplicationDetails"]),
                                    intOlnLeaveApplicationHead = Convert.ToInt32(rdr["intOlnLeaveApplicationHead"]),
                                    DateOfLeave = Convert.ToDateTime(rdr["DateOfLeave"]),
                                    AMorPM = rdr["AMorPM"].ToString(),
                                    isHalfDay = Convert.ToBoolean(rdr["isHalfDay"]),
                                    NoOfDays = Convert.ToDouble(rdr["NoOfDays"]),
                                    Reason = rdr["Reason"].ToString(),
                                    isWithPay = Convert.ToBoolean(rdr["isWithPay"]),
                                };

                                leave_det.Add(det);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return leave_det;
        }

        public  List<LeaveList> GetDataLeaveList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var leave_list = new List<LeaveList>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetLeaveApplications]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnLeaveApplicationHead", 0));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var leave = new LeaveList
                                {
                                    intOlnLeaveApplicationHead = Convert.ToInt32(rdr["intOlnLeaveApplicationHead"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]),
                                    Remarks = rdr["Remarks"].ToString(),
                                    //ImmediateSuperior = rdr["ImmediateSuperior"].ToString(),
                                    //AreaSupervisor = rdr["AreaSupervisor"].ToString(),
                                    //HR = rdr["HR"].ToString(),
                                    isCancelled = Convert.ToBoolean(rdr["isCancelled"]),
                                    isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]),
                                    //DisapproveBy = rdr["DisapproveBy"].ToString(),                                    
                                    LeaveType = rdr["LeaveType"].ToString(),
                                    isProcessed = Convert.ToBoolean(rdr["isProcessed"]),
                                    ReferenceId = Convert.ToInt32(DBNull.Value.Equals(rdr["ReferenceId"]) ? "0" : rdr["ReferenceId"]),
                                    Status = rdr["Status"].ToString(),
                                    StatusRemarks = rdr["StatusRemarks"].ToString(),
                                    NoOfDays = Convert.ToDouble(rdr["NoOfDays"]),
                                    LeaveDates = rdr["LeaveDates"].ToString(),
                                    NextApprover = rdr["NextApprover"].ToString()                                    
                                };

                                leave_list.Add(leave);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return leave_list;
        }

        public  string SaveLeave(LeaveMaster leave, DataTable leave_details)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_AddEditLeaveApplication";
                        cmd.Parameters.Add(new SqlParameter("@intOlnLeaveApplicationHead",leave.intOlnLeaveApplicationHead));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", leave.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@YearPeriod", leave.YearPeriod));
                        cmd.Parameters.Add(new SqlParameter("@DateFiled", leave.DateFiled));
                        cmd.Parameters.Add(new SqlParameter("@intMstLeaveType", leave.intMstLeaveType));
                        cmd.Parameters.Add(new SqlParameter("@Remarks", leave.Remarks));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", leave.DateFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateFromAMorPM", leave.DateFromAMorPM));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", leave.DateTo));
                        cmd.Parameters.Add(new SqlParameter("@DateToAMorPM", leave.DateToAMorPM));
                        cmd.Parameters.Add(new SqlParameter("@NoOfDays", leave.NoOfDays));                                                
                        cmd.Parameters.Add(new SqlParameter("@LeaveDetails", leave_details));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strResult;
        }

        public  string CancelLeave(int intOlnLeaveApplicationHead)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_CancelLeave";
                        cmd.Parameters.Add(new SqlParameter("@intOlnLeaveApplicationHead", intOlnLeaveApplicationHead));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strResult;
        }

        public string CancelLeaveByBatch(DataTable dt)
        {
            var dbMgr = new dbManager();
            string strMessage = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_CancelLeaveByBatch";
                        cmd.Parameters.Add(new SqlParameter("@leaves", dt));

                        conn.Open();
                        strMessage = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }

        public  List<LeaveApproval> GetDataLeaveApproval(int intMstCompany, string codeMstBranch, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var leaves = new List<LeaveApproval>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetLeaveForApproval";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        conn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var lv = new LeaveApproval 
                                {
                                    intOlnLeaveApplicationHead = Convert.ToInt32(rdr["intOlnLeaveApplicationHead"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    EmpName = rdr["EmpName"].ToString(),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]),
                                    LeaveType = rdr["LeaveType"].ToString(),
                                    DateFromAndTo = rdr["DateFromTo"].ToString(),
                                    NoOfDays = Convert.ToDouble(rdr["NoOfDays"]),
                                    DaysWithPay = Convert.ToDouble(rdr["DaysWithPay"])
                                };

                                leaves.Add(lv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return leaves;
        }

        public string ApproveDataLeave(string intMstEmpPersonal, DataTable dt, bool isHR = false)
        {
            var dbMgr = new dbManager();
            string strMessage = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ApproveLeave";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@leaveForApproval", dt));                        

                        conn.Open();
                        strMessage = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }        

        public  List<LeaveHRRegionalList> GetDataHRRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition)
        {
            var dbMgr = new dbManager();
            var leaves = new List<LeaveHRRegionalList>();

            try
            {

                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_HRRegionalList";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeBranchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));
                        cmd.Parameters.Add(new SqlParameter("@type", "Leave"));                        

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var myLV = new LeaveHRRegionalList
                                {
                                    intOlnLeaveApplicationHead = Convert.ToInt32(rdr["intOlnLeaveApplicationHead"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]).ToShortDateString(),
                                    EmpName = rdr["EmpName"].ToString(),
                                    LeaveDates = rdr["LeaveDates"].ToString(),
                                    BranchName = rdr["BranchName"].ToString(),
                                    NoOfDays = Convert.ToDouble(rdr["NoOfDays"])
                                };

                                leaves.Add(myLV);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return leaves;
        }

        public  List<LeaveBalance> GetDataLeaveBalance(string intMstEmpPersonal, int intYear)
        {
            var dbMgr = new dbManager();
            var leave = new List<LeaveBalance>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetLeaveBalance";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@YearPeriod", intYear));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var lv = new LeaveBalance 
                                { 
                                    VL = Convert.ToDouble(rdr["VL"]),
                                    SL = Convert.ToDouble(rdr["SL"]),
                                    PL = Convert.ToDouble(rdr["PL"]),
                                    ML = Convert.ToDouble(rdr["ML"])
                                };

                                leave.Add(lv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return leave;
        }

        public  double GetDataAvailableLeave(string intMstEmpPersonal, int intMstLeaveType, int intYear)
        {
            var dbMgr = new dbManager();
            double result = 0.0;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetAvailableLeave";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intMstLeaveType", intMstLeaveType));
                        cmd.Parameters.Add(new SqlParameter("@intYear", intYear));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader()) 
                        {
                            while (rdr.Read())
                            {
                                result = Convert.ToDouble(rdr[0]);
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
       
    }
}
