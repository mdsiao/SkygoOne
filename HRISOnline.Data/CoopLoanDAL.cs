using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data;
using System.Data.SqlClient;

namespace HRISOnline.Data
{
    public static class CoopLoanDAL
    {
        public static CoopLoan GetDataCoopLoan(int intOlnCoopLoanApplication)
        {
            var dbMgr = new dbManager();
            var coop = new CoopLoan();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblOlnCoopLoanApplication WHERE intOlnCoopLoanApplication = @intOlnCoopLoanApplication";
                        cmd.Parameters.Add(new SqlParameter("@intOlnCoopLoanApplication", intOlnCoopLoanApplication));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                coop.intOlnCoopLoanApplication = Convert.ToInt32(rdr["intOlnCoopLoanApplication"]);
                                coop.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                coop.DateFiled = Convert.ToDateTime(rdr["DateFiled"]);
                                coop.TypeOfMembership = Convert.ToInt32(rdr["TypeOfMembership"]);
                                coop.PurposeOfLoan = rdr["PurposeOfLoan"].ToString();
                                coop.intOlnLoanType = Convert.ToInt32(rdr["intOlnLoanType"]);
                                coop.NoOfMonths = Convert.ToDouble(rdr["NoOfMonths"]);
                                coop.AmountApplied = Convert.ToDouble(rdr["AmountApplied"]);
                                coop.InterestAmount = Convert.ToDouble(rdr["InterestAmount"]);
                                coop.TotalAmount = Convert.ToDouble(rdr["TotalAmount"]);
                                coop.AmountToBePaid = Convert.ToDouble(rdr["AmountToBePaid"]);
                                coop.CoMaker1 = rdr["CoMaker1"].ToString();
                                coop.CoMaker2 = rdr["CoMaker2"].ToString();
                                coop.ImmediateSuperior = rdr["ImmediateSuperior"].ToString();
                                coop.AreaSupervisor = rdr["AreaSupervisor"].ToString();
                                coop.HR = rdr["HR"].ToString();
                                coop.isCancelled = Convert.ToBoolean(rdr["isCancelled"]);
                                coop.isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]);
                                coop.DisapprovedBy = rdr["DisapproveBy"].ToString();
                                coop.DisapprovedDateTime = Convert.ToDateTime(rdr["DisapproveDateTime"]);
                                coop.isGovernment = Convert.ToBoolean(rdr["isGovernment"]);
                            }
                            
                        }

                        coop.LoanDetails = GetDataCoopLoanDetails(coop.intOlnCoopLoanApplication);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return coop;
        }

        public static List<CoopLoanDetails> GetDataCoopLoanDetails(int intOlnCoopLoanApplication)
        {
            var dbMgr = new dbManager();
            var list = new List<CoopLoanDetails>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetLoanDetail";
                        cmd.Parameters.Add(new SqlParameter("@intOlnCoopLoanApplication", intOlnCoopLoanApplication));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var det = new CoopLoanDetails 
                                {
                                    intTrnPayroll = Convert.ToInt32(rdr["intTrnPayroll"]),
                                    BatchNumber = rdr["BatchNumber"].ToString(),
                                    Amount = Convert.ToDouble(rdr["Amount"])
                                };

                                list.Add(det);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public static List<CoopLoanList> GetDataCoopLoanList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var coop = new List<CoopLoanList>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetCoopLoanApplications";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                var c = new CoopLoanList() 
                                {
                                    intOlnCoopLoanApplication = Convert.ToInt32(rdr["intOlnCoopLoanApplication"]),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]),
                                    PurposeOfLoan = rdr["PurposeOfLoan"].ToString(),
                                    LoanType = rdr["LoanType"].ToString(),
                                    NoOfMonths = Convert.ToDouble(rdr["NoOfMonths"]),
                                    TotalAmount = Convert.ToDouble(rdr["TotalAmount"]),
                                    isFullyPaid = Convert.ToBoolean(rdr["isFullyPaid"]),
                                    TotalDeduction = Convert.ToDouble(rdr["TotalDeduction"]),
                                    Balance = Convert.ToDouble(rdr["Balance"])
                                };

                                coop.Add(c);
                            }                            
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return coop;
        }

        public static List<CoopLoanList> GetDataCoopFullyPaidLoanList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var coop = new List<CoopLoanList>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetCoopLoanApplicationsFullyPaid";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                var c = new CoopLoanList()
                                {
                                    intOlnCoopLoanApplication = Convert.ToInt32(rdr["intOlnCoopLoanApplication"]),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]),
                                    PurposeOfLoan = rdr["PurposeOfLoan"].ToString(),
                                    LoanType = rdr["LoanType"].ToString(),
                                    NoOfMonths = Convert.ToDouble(rdr["NoOfMonths"]),
                                    TotalAmount = Convert.ToDouble(rdr["TotalAmount"]),
                                    isFullyPaid = Convert.ToBoolean(rdr["isFullyPaid"]),
                                    TotalDeduction = Convert.ToDouble(rdr["TotalDeduction"]),
                                    Balance = Convert.ToDouble(rdr["Balance"])
                                };

                                coop.Add(c);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return coop;
        }

        public static string SaveCoopLoan(CoopLoan coop)
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
                        cmd.CommandText = "Online_AddEditCoopLoanApplication";
                        cmd.Parameters.Add(new SqlParameter("@intOlnCoopLoanApplication", coop.intOlnCoopLoanApplication));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", coop.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFiled", coop.DateFiled));
                        cmd.Parameters.Add(new SqlParameter("@TypeOfMembership", coop.TypeOfMembership));
                        cmd.Parameters.Add(new SqlParameter("@PurposeOfLoan", coop.PurposeOfLoan));
                        cmd.Parameters.Add(new SqlParameter("@intOlnLoanType", coop.intOlnLoanType));
                        cmd.Parameters.Add(new SqlParameter("@NoOfMonths", coop.NoOfMonths));
                        cmd.Parameters.Add(new SqlParameter("@AmountApplied", coop.AmountApplied));
                        cmd.Parameters.Add(new SqlParameter("@InterestAmount", coop.InterestAmount));
                        cmd.Parameters.Add(new SqlParameter("@TotalAmount", coop.TotalAmount));
                        cmd.Parameters.Add(new SqlParameter("@AmountToBePaid", coop.AmountToBePaid));
                        cmd.Parameters.Add(new SqlParameter("@CoMaker1", coop.CoMaker1));
                        cmd.Parameters.Add(new SqlParameter("@CoMaker2", coop.CoMaker2));
                        cmd.Parameters.Add(new SqlParameter("@isGovernment", coop.isGovernment));

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

        public static string CancelCoopLoan(int intOlnCoopLoanApplication)
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
                        cmd.CommandText = "[Online_CancelCoopLoan]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnCoopLoanApplication", intOlnCoopLoanApplication));

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

        public static List<CoopLoanApproval> GetDataCoopLoanApproval(int intMstCompany, string codeMstBranch, int intMstPositionSupervisor)
        {
            var dbMgr = new dbManager();
            var list = new List<CoopLoanApproval>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetCoopLoanForApproval";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstPositionSupervisor", intMstPositionSupervisor));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var loan = new CoopLoanApproval 
                                {
                                    intOlnCoopLoanApplication = Convert.ToInt32(rdr["intOlnCoopLoanApplication"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    EmpName = rdr["EmpName"].ToString(),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]),
                                    LoanType = rdr["LoanType"].ToString()
                                };

                                list.Add(loan);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public static List<CoopLoanHRRegionalList> GetDataHRRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition, string option, string param)
        {
            var dbMgr = new dbManager();
            var list = new List<CoopLoanHRRegionalList>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_HRRegionalList]";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeBranchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));
                        cmd.Parameters.Add(new SqlParameter("@type", "Coop Loan"));
                        cmd.Parameters.Add(new SqlParameter("@option", option));
                        cmd.Parameters.Add(new SqlParameter("@parameter", param));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var loan = new CoopLoanHRRegionalList 
                                {
                                    intOlnCoopLoanApplication = Convert.ToInt32(rdr["intOlnCoopLoanApplication"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]).ToShortDateString(),
                                    EmpName = rdr["EmpName"].ToString(),
                                    ImmediateSuperior = rdr["ImmediateSuperior"].ToString(),
                                    AreaSupervisor = rdr["AreaSupervisor"].ToString(),
                                    HR = rdr["HR"].ToString(),
                                    isCancelled = Convert.ToBoolean(rdr["isCancelled"]),
                                    isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]),
                                    ApprovalLevel = Convert.ToInt32(rdr["ApprovalLevel"]),                                    
                                };

                                list.Add(loan);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public static string ApproveDataCoopLoan(int intMstPositionSupervisor, DataTable dt, bool isHR = false)
        {
            var dbMgr = new dbManager();
            string strMessage = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ApproveCoopLoans";
                        cmd.Parameters.Add(new SqlParameter("@intMstPositionSupervisor", intMstPositionSupervisor));
                        cmd.Parameters.Add(new SqlParameter("@loanForApproval", dt));
                        cmd.Parameters.Add(new SqlParameter("@isHR", isHR));

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

        public static CoopLoanMonitoringMaster GetCoopLoanMonitoring(int intOlnCoopLoanApplication)
        {
            var dbMgr = new dbManager();
            var data = new CoopLoanMonitoringMaster();

            try 
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("[Online_GetLoanDetail]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@intOlnCoopLoanApplication", intOlnCoopLoanApplication));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read()) 
                            {
                                data.DateFiled = Convert.ToDateTime(rdr["DateFiled"]);
                                data.LoanType = rdr["LoanType"].ToString();
                                data.NoOfMonths = Convert.ToDouble(rdr["NoOfMonths"]);
                                data.TotalAmount = Convert.ToDouble(rdr["TotalAmount"]);
                                data.TotalDeduction = Convert.ToDouble(rdr["TotalDeduction"]);
                                data.RemainingBalance = Convert.ToDouble(rdr["Balance"]);
                                data.Remarks = rdr["Remarks"].ToString();
                                data.AmountPerMonth = Convert.ToDouble(rdr["AmountPerMonth"]);
                                data.AmountToBePaid = Convert.ToDouble(rdr["AmountToBePaid"]);
                            }

                            data.LoanPayments = GetCoopLoanPayments(intOlnCoopLoanApplication);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                return null;
            }

            return data;
        }

        public static List<CoopLoanMonitoring> GetCoopLoanPayments(int intOlnCoopLoanApplication)
        {
            var dbMgr = new dbManager();
            var list = new List<CoopLoanMonitoring>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetLoanDeductions]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnCoopLoanApplication", intOlnCoopLoanApplication));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var det = new CoopLoanMonitoring
                                {
                                    intTrnPayroll = Convert.ToInt32(rdr["intTrnPayroll"]),
                                    BatchNumber = rdr["BatchNumber"].ToString(),
                                    intTrnPayrollOtherDeduction = Convert.ToInt32(rdr["intTrnPayrollOtherDeduction"]),
                                    intTrnPayrollOtherDeductionDetail = Convert.ToInt32(rdr["intTrnPayrollOtherDeductionDetail"]),
                                    Amount = Convert.ToDouble(rdr["Amount"]),
                                    Remarks = rdr["Remarks"].ToString(),
                                    PayrollDate = rdr["PayrollDate"].ToString()
                                };

                                if (det.BatchNumber.Contains("Sup")){
                                    det.BatchNumber = det.BatchNumber.Substring(0, det.BatchNumber.Length - 3);
                                }

                                list.Add(det);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }
    }
}
