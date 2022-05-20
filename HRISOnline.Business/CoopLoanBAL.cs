using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Data;
using HRISOnline.Objects;
using System.Data;

namespace HRISOnline.Business
{
    public static class CoopLoanBAL
    {
        public static List<CoopLoanList> GetCoopLoanList(string intMstEmpPersonal)
        {
            return CoopLoanDAL.GetDataCoopLoanList(intMstEmpPersonal);
        }

        public static List<CoopLoanList> GetCoopFullyPaidLoanList(string intMstEmpPersonal)
        {
            return CoopLoanDAL.GetDataCoopFullyPaidLoanList(intMstEmpPersonal);
        }

        public static CoopLoan GetCoopLoan(int intOlnCoopLoanApplication)
        {
            double amtPaid = 0.0;
            double amtBalance = 0.0;
            var result = CoopLoanDAL.GetDataCoopLoan(intOlnCoopLoanApplication);
            amtBalance = result.TotalAmount;

            foreach (var row in result.LoanDetails)
            {
                amtPaid += row.Amount;
                amtBalance -= row.Amount;
            }

            result.AmountPaid = amtPaid;
            result.AmountBalance = amtBalance;

            return result;
        }

        public static string SaveCoopLoan(CoopLoan coop)
        {
            string result = string.Empty;

            if (coop.AmountApplied == null || coop.AmountApplied.ToString() == "")
            {
                throw new Exception("Please enter Amount Applied.");
            }
            if (coop.AmountApplied == 0)
            {
                throw new Exception("Amount Applied should be greater than zero(0).");
            }
            if ((coop.PurposeOfLoan == "") || (coop.PurposeOfLoan == null))
            {
                throw new Exception("Please enter Purpose of Loan.");
            }

            if (result == string.Empty)
            {
                if (coop.isGovernment) 
                {
                    coop.CoMaker1 = "N/A";
                    coop.CoMaker2 = "N/A";
                }
                result = CoopLoanDAL.SaveCoopLoan(coop);
            }

            return result;
        }

        public static double GetInterestPercentage(int intOlnLoanType)
        {
            string query = "SELECT InterestPercent FROM tblOlnLoanType WHERE intOlnLoanType = " + intOlnLoanType;
            string result = UtilitiesDAL.GetSingleData(query);
            double resultValue;

            if ((result == string.Empty) || (result == ""))
            {
                resultValue = 0.0;
            }
            else
            {
                resultValue = Convert.ToDouble(result);
            }

            return resultValue;
        }

        public static string CancelCoopLoan(int intOlnCoopLoanApplication)
        {
            return CoopLoanDAL.CancelCoopLoan(intOlnCoopLoanApplication);
        }

        public static List<CoopLoanApproval> GetCoopLoanApproval(int intMstCompany, string codeMstBranch, int intMstPositionSupervisor)
        {
            return CoopLoanDAL.GetDataCoopLoanApproval(intMstCompany, codeMstBranch, intMstPositionSupervisor);
        }

        public static List<CoopLoanHRRegionalList> GetHRRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition, string option, string param)
        {
            return CoopLoanDAL.GetDataHRRegionalList(intMstEmpPersonal, intMstCompany, codeBranchCode, intMstPosition, option, param);
        }

        public static string ApproveCoopLoan(int intMstPositionSupervisor, string intMstEmpPersonal, ICollection<CoopLoanApproval> loans, bool isHR = false)
        {
            string strMessage = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("intLoanApplicationID");
            dt.Columns.Add("intMstApprovedBy");

            try
            {
                foreach (var item in loans)
                {
                    dt.Rows.Add(item.intOlnCoopLoanApplication, intMstEmpPersonal);
                }

                strMessage = CoopLoanDAL.ApproveDataCoopLoan(intMstPositionSupervisor, dt, isHR);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;

        }

        public static CoopLoanMonitoringMaster GetCoopLoanMonitoring(int intOlnCoopLoanApplication)
        {
            return CoopLoanDAL.GetCoopLoanMonitoring(intOlnCoopLoanApplication);
        }
    }
}
