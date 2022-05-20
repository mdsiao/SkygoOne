using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using HRISOnline.Data;

namespace HRISOnline.Business
{
    public class EmployeeBAL
    {

        EmployeeDAL _empDAL = new EmployeeDAL();

        #region Employee
        public EmpPersonal GetEmployeePersonal(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeePersonal(intMstEmpPersonal);                                    
        }

        public EmpDTR GetEmployeeDTR(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeDTR(intMstEmpPersonal);
        }

        public EmpFamily GetEmployeeFamily(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeFamily(intMstEmpPersonal);
        }

        public List<EmpChildren> GetEmployeeChildren(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeChildren(intMstEmpPersonal);
        }

        public List<EmpEducation> GetEmployeeEducation(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeEducation(intMstEmpPersonal);
        }

        public List<EmpEligibility> GetEmployeeEligibility(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeEligibility(intMstEmpPersonal);
        }

        public List<EmpSemenarsTraining> GetEmployeeTraining(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeTraining(intMstEmpPersonal);
        }
        public List<EmpWorkExperience> GetEmployeeWorkExperience(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeWorkExperience(intMstEmpPersonal);
        }

        public int GetEmployeeApprovalLevel(int intMstPosition)
        {
            string query = "SELECT ApprovalLevel FROM tblMstPosition WHERE (intMstPosition = " + intMstPosition + ")";
            string result = UtilitiesDAL.GetSingleData(query);

            if (result == "")
            {
                result = "-1";
            }
            return Convert.ToInt32(result);
        }

        public List<ComboBoxSource> GetEmployeeComboList()
        {
            string query = "SELECT intMstEmpPersonal, Lastname + ', ' + Firstname AS EmpName FROM tblMstEmpPersonal ORDER BY Lastname";
            return UtilitiesDAL.GetDataCombo(query);
        }

        public List<EmpDTRData> GetEmployeeDTRData(int intMstCompany, string branchCode, string intMstEmpPersonal, DateTime DateStart, DateTime DateEnd)
        {
            return _empDAL.GetEmployeeDTRData(intMstCompany, branchCode, intMstEmpPersonal, DateStart, DateEnd);
        }

        public List<EmpEducation> GetEmpEducationList(string intMstEmpPersonal)
        {
            //var emp_educ = EmployeeDAL.GetEmployeeEducation(intMstEmpPersonal);
            //System.Data.DataTable dt = Utilities.Functions.ConvertToDatatable<EmpEducation>(emp_educ);

            return _empDAL.GetEmployeeEducation(intMstEmpPersonal); ;

        }

        public DateTime GetPayrollDates(int intTrnPayroll, string whatDate)
        {
            DateTime _result;
            string query = "SELECT " + whatDate + " FROM tblTrnPayroll WHERE intTrnPayroll = " + intTrnPayroll;

            _result = Convert.ToDateTime(UtilitiesDAL.GetSingleData(query));
            return _result;
        }

        public string SaveEmployee(EmpSaving emp, string username)
        {
            return _empDAL.SaveEmployee(emp, username);
        }

        public string SaveEligibility(EmpEligibility elig)
        {
            return _empDAL.SaveEligibility(elig);
        }

        public string SaveWorkExperience(EmpWorkExperience we)
        {
            return _empDAL.SaveWorkExperience(we);
        }

        public string SaveEducation(EmpEducation educ)
        {
            return _empDAL.SaveEducation(educ);
        }

        public List<string> GetPayrollDates()
        {
            //string result = UtilitiesDAL.GetSingleData("SELECT TOP 1 DateStart FROM tblTrnPayroll WHERE ReferenceId IS NULL ORDER BY PayrollDate ASC");
            string result = "9/28/2015";
            if (result == ""){
                result = DateTime.Now.Month.ToString() + "/1/" + DateTime.Now.Year.ToString();
            }
            DateTime payrollStart = Convert.ToDateTime(result);

            return Utilities.Functions.GetPayrollPeriods(payrollStart);
        }
      

        public string DeleteItem(int id, string module, string username)
        {
            return _empDAL.DeleteItem(id, module, username);
        }

        public   List<NotificationTrans> GetCancelledTransactions(string intMstEmpPersonal)
        {
            return _empDAL.GetCancelledTransactions(intMstEmpPersonal);
        }

        public bool hasWorkOnSaturday(int intMstWorkShift)
        {
            return UtilitiesDAL.isRecordExists("tblMstWorkShiftDetails", "intMstWorkShift", "intMstWorkShift = " + intMstWorkShift + " AND DayName = 'Saturday' AND intMstDayType = 1");
        }

        public List<EmpApprovalNotification> GetEmployeeApprovalNotification(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeApprovalNotification(intMstEmpPersonal);
        }

        public List<ComboBoxSource> GetEmployeeList(string branchCode)
        {
            string query = "Online_GetEmployee 0,'Contacts', '" + branchCode + "'";

            return UtilitiesDAL.GetDataCombo(query);
        }

        public ApproversName GetEmployeeApproverName(string intMstEmpPersonal)
        {
            return _empDAL.GetEmployeeApproverName(intMstEmpPersonal);
        }
        #endregion        

        #region Payroll Employee Detail
        public List<PayrollDetailList> GetPayrollDetailList(string intMstEmpPersonal)
        {
            return _empDAL.GetDataPayrollDetailList(intMstEmpPersonal);
        }

        public PayrollDetailEmployee GetPayrollDetailEmployee(int intTrnPayroll, string intMstEmpPersonal)
        {
            return _empDAL.GetDataPayrollDetailEmployee(intTrnPayroll, intMstEmpPersonal);
        }

        public List<PayrollDetailEmployeeOtherDeduction> GetPayrollOtherDeduction(int intTrnPayroll, string intMstEmpPersonal)
        {
            return _empDAL.GetDataPayrollDetailEmployeeOtherDeduction(intTrnPayroll, intMstEmpPersonal);
        }

        public List<PayrollDetailEmployeeOtherIncome> GetPayrollOtherIncome(int intTrnPayroll, string intMstEmpPersonal)
        {
            return _empDAL.GetDataPayrollDetailEmployeeOtherIncome(intTrnPayroll, intMstEmpPersonal);
        }

        public string GetPayrollDatePaySlip(int intTrnPayrollId)
        {
            string query = "SELECT CONVERT(varchar, PayrollDate, 101) AS PayrollDate FROM tblTrnPayroll WHERE intTrnPayroll = " + intTrnPayrollId;

            return UtilitiesDAL.GetSingleData(query);
        }

        public List<AwardsAndBonus> GetAwardsAndBonus(string intMstEmpPersonal)
        {
            return _empDAL.GetDataAwardsAndBonusList(intMstEmpPersonal);
        }
        #endregion

        #region Toolbox
        public List<MemorandumToolbox> GetMemoToolbox(string Username, string whatType)
        {
            return _empDAL.GetDataMemoToolbox(Username, whatType);
        }

        public List<NotificationToolbox> GetNotificationToolbox(string Username, string whatType)
        {
            return _empDAL.GetNotificationToolbox(Username, whatType);
        }

        public List<EmpNoticeToolbox> GetEmpNoticeToolbox(string Username, string whatType)
        {
            return _empDAL.GetEmployeeNoticeToolbox(Username, whatType);
        }

        public List<EmpAuditToolbox> GetAuditReportToolbox(string Username, string whatType)
        {
            return _empDAL.GetAuditReportToolbox(Username, whatType);
        }

        //SIAO ADDED FOR DEVIATION
        public List<EmpAuditToolbox> GetAuditReportWithoutDeviationToolbox(string Username, string whatType)
        {
            return _empDAL.GetAuditReportWithoutDeviationToolbox(Username, whatType);
        }
        public List<EmpAuditToolbox> GetAuditReportWithMinorDeviationToolbox(string Username, string whatType)
        {
            return _empDAL.GetAuditReportWithMinorDeviationToolbox(Username, whatType);
        }
        public List<EmpAuditToolbox> GetAuditReportWithMajorDeviationToolbox(string Username, string whatType)
        {
            return _empDAL.GetAuditReportWithMajorDeviationToolbox(Username, whatType);
        }

        //END SIAO ADDED
        public List<PolicyToolbox> GetPolicyToolbox(string Username, string whatType)
        {
            return _empDAL.GetPolicyToolbox(Username, whatType);
        }

        public string UpdateMemoToOpen(string fileName, string Username, string whatType)
        {
            return _empDAL.UpdateDataMemoToOpen(fileName, Username, whatType);
        }

        public string AcknowledgeDocument(string fileName, string Username, string whatType)
        {
            return _empDAL.AcknowledgeDocument(fileName, Username, whatType);
        }

        public AnnouncementCount GetAnnouncementCount(string username, string intMstEmpPersonal)
        {
            return _empDAL.GetAnnouncementCount(username, intMstEmpPersonal);
        }
        #endregion

        #region ONE Access
        public List<ObjUserDetails> GetUserDetails(string userName)
        {
            return _empDAL.GetUserDetails(userName);
        }
        #endregion

        #region Biometrics
        public List<DTRBiometricLogs> GetBiometricLogs(int intMstCompany, string branchCode, string intMstEmpPersonal, DateTime DateStart, DateTime DateEnd)
        {
            return _empDAL.GetBiometricLogs(intMstCompany, branchCode, intMstEmpPersonal, DateStart, DateEnd);
        }

        public List<DTRBiometrics> GetBiometric(int intMstCompany, string branchCode, string intMstEmpPersonal, DateTime DateStart, DateTime DateEnd)
        {
            return _empDAL.GetBiometric(intMstCompany, branchCode, intMstEmpPersonal, DateStart, DateEnd);
        }

        public System.Data.DataTable GetDTRReport(DateTime dateStart, DateTime dateEnd, int intMstCompany, string branchCode, string intMstEmpPersonal)
        {
            return _empDAL.GetDTReport(dateStart, dateEnd, intMstCompany, branchCode, intMstEmpPersonal);
        }
        #endregion

        public string DocumentReaction(int ReactionType, string ID, string FName, string Username)
        {
            return _empDAL.DocumentReaction(ReactionType, ID, FName, Username);
        }

        public RecentReaction GetRecentReaction(string Filename, string username)
        {
            return _empDAL.GetRecentReaction(Filename, username);
        }

    }
}
