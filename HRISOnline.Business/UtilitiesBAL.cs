using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Data;
using HRISOnline.Objects;

namespace HRISOnline.Business
{
    public static class UtilitiesBAL
    {
        //SIAO ADDED
        public static bool canApproveOfEmployee201(string intMstEmpPersonal)
        {
            return UtilitiesDAL.canApproveOfEmployee201(intMstEmpPersonal);           
        }
        //END OF SIAO ADDED

        public static string GetNoOfWorkingDays(int workshift)
        {
            string query = "SELECT NoOfDays FROM tblMstWorkShift WHERE intMstWorkShift = " + workshift;
            return UtilitiesDAL.GetSingleData(query);
        }

        public static List<ComboBoxSource> GetPositionList()
        { 
            string query = "SELECT intMstPosition, PositionName FROM tblMstPosition ORDER BY PositionName";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetDepartmentList()
        {
            string query = "SELECT intMstDepartment, DepartmentName FROM tblMstDepartment ORDER BY DepartmentName";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetBranchList()
        {
            string query = "SELECT BranchCode, BranchName FROM tblMstBranch ORDER BY BranchName";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetWorkShiftList()
        {
            string query = "SELECT intMstWorkShift, WorkShiftName FROM tblMstWorkShift ORDER BY WorkShiftName";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetWorkStatusList()
        {
            string query = "SELECT intMstWorkStatus, WorkStatus FROM tblMstWorkStatus ORDER BY WorkStatus";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetLeaveType()
        {
            string query = "SELECT 0 AS intMstLeaveType, '' AS LeaveType ";
            query += "UNION SELECT intMstLeaveType, LeaveType FROM tblMstLeaveType WHERE intMstLeaveType IN (1,2) ORDER BY LeaveType";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetLoanType(int isCoop = 1)
        {
            string query = "SELECT intOlnLoanType, LoanType FROM tblOlnLoanType WHERE isCoopLoan = " + isCoop + " ORDER BY LoanType";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetMembershipType(int isCoop = 1)
        {
            string query = "SELECT intOlnMembershipType, MembershipType FROM tblOlnMembershipType WHERE isCoopLoan = " + isCoop + " ORDER BY MembershipType";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetAMorPMType(string dType, double noOfDays) {
            string query = "SELECT * FROM tblOlnAMorPMDescription";

            if (noOfDays > 1) {
                if (dType == "from"){

                    query += " WHERE intOlnAMorPM <> 2";
                }
                else {

                    query += " WHERE intOlnAMorPM <> 3";
                }
            }

            query += " ORDER BY intOlnAMorPM";

            return UtilitiesDAL.GetDataCombo(query);
        }

        public static ItemCount GetItemCount(int intMstCompany, string codeMstBranch, string intMstEmpPersonal)
        {
            return UtilitiesDAL.GetDataItemCount(intMstCompany, codeMstBranch, intMstEmpPersonal);
        }

        public static bool isHRRegional(int intMstPosition)
        {
            //string query = "SELECT COUNT(intMstPosition) AS res FROM tblOlnHRRegional WHERE intMstPosition = " + intMstPosition;
            //return Convert.ToBoolean(Convert.ToInt32(UtilitiesDAL.GetSingleData(query)));
            return UtilitiesDAL.isRecordExists("tblOlnHRRegional", "intMstPosition", "intOlnHRRegional = 1 AND intMstPosition = " + intMstPosition);
        }

        public static bool isFinanceManager(int intMstPosition)
        {
            //string query = "SELECT COUNT(intOlnFinancePosition) AS res FROM tblOlnFinancePosition WHERE intOlnFinancePosition = " + intMstPosition;
            //return Convert.ToBoolean(Convert.ToInt32(UtilitiesDAL.GetSingleData(query)));
            return UtilitiesDAL.isRecordExists("tblOlnFinancePosition", "intOlnFinancePosition", "intOlnFinancePosition = " + intMstPosition);
        }

        public static bool isHRHomeOffice(int intMstPosition)
        {
            return UtilitiesDAL.isRecordExists("tblOlnHRRegional", "intMstPosition", "intOlnHRRegional = 2 AND intMstPosition = " + intMstPosition);
        }

        public static bool isAllowedOvertime(int intMstPosition)
        {
            return UtilitiesDAL.isRecordExists("tblMstPosition", "intMstPosition", "(intMstPosition = " + intMstPosition + ") AND (isAllowedOvertime = 1)");
        }

        public static bool canApproveTransactions(string intMstEmpPersonal)
        {
            return UtilitiesDAL.isRecordExists("tblMstEmpDTR", "intMstPosition", "intMstEmpPersonalSupervisor = '" + intMstEmpPersonal + "'");
        }

        public static bool canApplyForOBHoliday(int positionid)
        {
            return UtilitiesDAL.isRecordExists("tblMstPosition", "intMstPosition", "intMstPosition = " + positionid + " AND isAllowedFileOBHoliday = 1");
        }

        public static List<ComboBoxSource> GetPayroll()
        {
            string query = "SELECT intTrnPayroll, Remarks FROM tblTrnPayroll WHERE isLocked = 1 ORDER BY intTrnPayroll";
            return UtilitiesDAL.GetDataCombo(query);
        }        

        public static List<ComboBoxSource> GetGatePassType()
        {
            string query = "SELECT intOlnGatePassType, GatePassTypeName FROM tblOlnGatePassType ORDER BY intOlnGatePassType";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetComboBoxMonth()
        {
            string query = "";
            int intMonth = DateTime.Now.Month;            
            query = "SELECT intMonthNo, MonthName FROM tblMonth ";
            if (DateTime.Now.Day >= 20){
                query += "WHERE (intMonthNo > " + intMonth + ") ORDER BY intMonthNo";
            }
            else {
                query += "WHERE (intMonthNo >= " + intMonth + ") ORDER BY intMonthNo";
            }
            
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static List<ComboBoxSource> GetComboBoxYear()
        {
            string query = "SELECT intYear, intYear AS YearNo FROM tblYear WHERE (intYear >= " + DateTime.Now.Year + ") ORDER BY intYear";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public static string GetDocumentPath()
        {
            string query = "SELECT * FROM tblOlnDocPath";
            return UtilitiesDAL.GetSingleData(query);
        }

        public static string GetSHA1(string Input)
        {
            return UtilitiesDAL.GetSHA1(Input);
        }

        public static string DisapproveTransaction(int intTransType, ICollection<DisapproveTrans> dTrans, string Username)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string result = string.Empty;
            string reason = string.Empty;

            dt.Columns.Add("intOlnTransNo");

            try
            {
                foreach (var item in dTrans){
                    dt.Rows.Add(item.intOlnTransNo);
                    reason = item.Reason;
                }

                result = UtilitiesDAL.DisapproveTransaction(intTransType, dt, Username, reason);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return result;
        }

        public static MessageDates GetMessageDates()
        {
            return UtilitiesDAL.GetMessageDates();
        }

        public static List<ComboBoxSource> GetHoliday(string empid, string branchcode, int workshift, int company, int gatepass = 0)
        {
            string query = "Online_GetHoliday '" + empid + "', '" + branchcode + "', " + workshift + "," + company + "," + gatepass;

            return UtilitiesDAL.GetDataCombo(query);

        }
    }
}
