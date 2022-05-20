//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace HRISOnline.Objects
{

    #region Employee Data
    public class EmpData
    {
        public EmpPersonal EmpPersonal { get; set; }
        public EmpDTR DTR { get; set; }
        public EmpFamily Family { get; set; }
        public virtual ICollection<EmpChildren> Children { get; set; }
        public virtual ICollection<EmpEducation> Education { get; set; }
        public virtual ICollection<EmpEligibility> Eligibility { get; set; }
        public virtual ICollection<EmpWorkExperience> WorkExp { get; set; }
        public virtual ICollection<EmpSemenarsTraining> Training { get; set; }
        public virtual ICollection<PayrollDetailList> PayrollDetail { get; set; }
        public virtual ICollection<AwardsAndBonus> AwardsAndBonus { get; set; }

        public ApproversName SuperiorsName { get; set; }
        public int intTrnPayroll { get; set; }
        public string PayrollPeriods { get; set; }
    }

    public class EmpSaving
    {
        //Personal
        public string intMstEmpPersonal { get; set; }
        public int intMstCompany { get; set; }
        public string EmpIdCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtensionName { get; set; }
        public DateTime BirthDay { get; set; }
        public string BirthPlace { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Citizenship { get; set; }
        public string Religion { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodType { get; set; }
        public string HomeAddress { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string CurrentAddress { get; set; }
        public string CurrentZipCode { get; set; }
        public string CurrentTelephone { get; set; }
        public string EmailAddress { get; set; }
        public string Cellphone { get; set; }
        public string HDMFNo { get; set; }
        public string EYEEId { get; set; }
        public string PHICNo { get; set; }
        public string SSSNo { get; set; }
        public string TIN { get; set; }
        public int intMstTaxCode { get; set; }
        public string BankAccountNo { get; set; }
        public string EmployeePic { get; set; }
        public string PersonToNotify { get; set; }
        public string Relation { get; set; }
        public string ContactNum { get; set; }

        //DTR
        public int intMstEmpDTR { get; set; }        
        public string codeMstBranch { get; set; }
        public int intMstDepartment { get; set; }
        public int intMstPosition { get; set; }
        public int intMstWorkShift { get; set; }
        public string codeMstBranchHome { get; set; }
        public DateTime DateHired { get; set; }
        public string DateSeparated { get; set; }
        public DateTime DateProbationary { get; set; }
        public int intMstWorkStatus { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsSupervisoryRate { get; set; }
        public string intMstEmpPersonalSupervisor { get; set; }
        public double MonthlyRate { get; set; }
        public double DailyRate { get; set; }
        public double DiscAllowance { get; set; }
        public double Cola { get; set; }
        public bool LateDeduction { get; set; }
        public bool DTRPunches { get; set; }
        public bool Biometrics { get; set; }
        public double HDMFEmployee { get; set; }
        public double HDMFEmployer { get; set; }
        public bool CoopMember { get; set; }
        public bool IncludeSSS { get; set; }
        public bool IncludePHIC { get; set; }
        public bool IncludeHDMF { get; set; }
        public bool IncludeWithTax { get; set; }
        public bool IncludeHealthInsurance { get; set; }
        public double CoopDeposit { get; set; }
        public double CoopSavings { get; set; }
        public string ReasonForSeparation { get; set; }
        public int intBioRegistration { get; set; }

        //Family
        public int intMstEmpFamily { get; set; }        
        public string SpouseFirstname { get; set; }
        public string SpouseLastname { get; set; }
        public string SpouseMiddlename { get; set; }
        public string SpouseTelephone { get; set; }
        public string SpouseAddress { get; set; }
        public string FatherFirstname { get; set; }
        public string FatherLastname { get; set; }
        public string FatherMiddlename { get; set; }
        public string MotherFirstname { get; set; }
        public string MotherLastname { get; set; }
        public string MotherMiddlename { get; set; }
    }

    public class EmpApprovalInfo
    {
        public string intMstEmpPersonalApprover { get; set; }
        public string ApproverName { get; set; }
        public string ApprovedDateTime { get; set; }
        public string AppStatus { get; set; }
        public string Timing { get; set; }
        public string PaySched { get; set; }
    }    
    #endregion

    #region Others
    public class ComboBoxSource
    {
        public object ValueMember { get; set; }
        public string DisplayMember { get; set; }
    }

    public class ItemCount
    {
        public int OvertimeCount { get; set; }
        public int LeaveCount { get; set; }
        public int CoopLoanCount { get; set; }
        public int BDayCount { get; set; }
        public int GatePassCount { get; set; }
        public int DTRAdjCount { get; set; }
        public int OBCount { get; set; }
        public int PBCount { get; set; }
        public int FinanceOTCount { get; set; }
        public int CancelledTrans { get; set; }
        public int EmpUpdateCount { get; set; }
        public int MissingPunchCount { get; set; }
        public int OTMealsCount { get; set; }
    }

    public class DisapproveTrans
    {
        public int intOlnTransNo { get; set; }
        public string Reason { get; set; }
    }

    public class NotificationTrans
    {
        public int intId { get; set; }
        public DateTime dDate { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string dDesc { get; set; }
        public string dType { get; set; }
    }

    public class EmpApprovalNotification
    {
        public string dType { get; set; }
        public string DateTimeCovered { get; set; }
        public string Duration { get; set; }
        public string Remarks { get; set; }
        public DateTime DateFiled { get; set; }
        public string Status { get; set; }
    }

    public class MessageDates
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
    }    
    #endregion

    #region Leaves
    public class LeaveListData
    {
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public virtual ICollection<LeaveList> LeaveList { get; set; }
        public virtual ICollection<LeaveBalance> LeaveBalance { get; set; }
    }

    public class LeaveList
    {
        public int intOlnLeaveApplicationHead { get; set; }
        public string intMstEmpPersonal { get; set; }
        public DateTime DateFiled { get; set; }
        public string LeaveType { get; set; }        
        public string Remarks { get; set; }
        //public string ImmediateSuperior { get; set; }
        //public string AreaSupervisor { get; set; }
        //public string HR { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
        //public string DisapproveBy { get; set; }
        //public DateTime DisapproveDateTime { get; set; }
        public bool isProcessed { get; set; }
        public int ReferenceId { get; set; }
        public string Status { get; set; }
        public string StatusRemarks { get; set; }
        public double NoOfDays { get; set; }
        public string LeaveDates { get; set; }
        public string NextApprover { get; set; }                
    }

    public class LeaveBalance
    {
        public double VL { get; set; }
        public double SL { get; set; }
        public double ML { get; set; }
        public double PL { get; set; }
    }

    public class LeaveApproval
    {
        public int intOlnLeaveApplicationHead { get; set; }
        public DateTime DateFiled { get; set; }
        public string LeaveType { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
        public string DateFromAndTo { get; set; }
        public double NoOfDays { get; set; }
        public double DaysWithPay { get; set; }
    }

    public class LeaveHRRegionalList
    {
        public int intOlnLeaveApplicationHead { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
        public string DateFiled { get; set; }
        public string LeaveDates { get; set; }
        public double NoOfDays { get; set; }
        public string BranchName { get; set; }        
    }

    public class LeaveApprovalInfo
    {
        public LeaveMaster lv { get; set; }
        public virtual ICollection<EmpApprovalInfo> EmpApprovalInfo { get; set; }
    }
    #endregion

    #region Coop Loan
    public class CoopLoanList
    {
        public int intOlnCoopLoanApplication { get; set; }
        public DateTime DateFiled { get; set; }
        public string PurposeOfLoan { get; set; }
        public string LoanType { get; set; }        
        public double NoOfMonths { get; set; }
        public double TotalAmount { get; set; }
        public bool isFullyPaid { get; set; }        
        public double TotalDeduction { get; set; }
        public double Balance { get; set; }        
    }        

    public class CoopLoanApproval
    {
        public int intOlnCoopLoanApplication { get; set; }
        public DateTime DateFiled { get; set; }
        public string LoanType { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
    }

    public class CoopLoanHRRegionalList
    {
        public int intOlnCoopLoanApplication { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
        public string DateFiled { get; set; }
        public string ImmediateSuperior { get; set; }
        public string AreaSupervisor { get; set; }
        public string HR { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
        public int ApprovalLevel { get; set; }
    }

    public class CoopLoanMonitoringMaster
    {
        public DateTime DateFiled { get; set; }
        public string LoanType { get; set; }
        public double NoOfMonths { get; set; }
        public double TotalAmount { get; set; }
        public double TotalDeduction { get; set; }
        public double RemainingBalance { get; set; }
        public string Remarks { get; set; }
        public double AmountPerMonth { get; set; }
        public double AmountToBePaid { get; set; }
        public virtual ICollection<CoopLoanMonitoring> LoanPayments { get; set; }
    }

    public class CoopLoanMonitoring
    {
        public int intTrnPayroll { get; set; }
        public string PayrollDate { get; set; }
        public string BatchNumber { get; set; }
        public int intTrnPayrollOtherDeductionDetail { get; set; }
        public int intTrnPayrollOtherDeduction { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }        
    }

    public class CoopLoanMain
    {
        public virtual ICollection<CoopLoanList> CoopUnpaidList { get; set; }
        public virtual ICollection<CoopLoanList> CoopFullyPaidList { get; set; }
    }
    #endregion

    #region Overtime
    public class OvertimeList
    {
        public int intOlnOvertimeApplication { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime OvertimeDate { get; set; }
        public string NoOfHours { get; set; }
        public string PurposeOfWork { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
        public bool isProcessed { get; set; }
        public string NextApprover { get; set; }
    }

    public class OvertimeApproval
    {
        public int intOlnOvertimeApplication { get; set; }
        public DateTime OvertimeDate { get; set; }
        public string OvertimeTime { get; set; }
        public string NoOfHours { get; set; }
        public string EmpName { get; set; }
        public string DepartmentName { get; set; }
    }

    public class OvertimeFinanceApproval
    {
        public int intOlnOvertimeApplication { get; set; }
        public DateTime OvertimeDate { get; set; }        
        public string OvertimeTime { get; set; }
        public string NoOfHours { get; set; }
        public string EmpName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateFiled { get; set; }
        public string PurposeOfWork { get; set; }
        public string ApproverName { get; set; }
    }

    public class OvertimeHRRegionalList
    {
        public int intOlnOvertimeApplication { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
        public string OvertimeDate { get; set; }
        public string ImmediateSuperior { get; set; }
        public string AreaSupervisor { get; set; }
        public string HR { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
        public int ApprovalLevel { get; set; }
        public bool isInBiometric { get; set; }
    }

    public class OvertimeApprovalInfo
    {
        public Overtime ot { get; set; }
        public virtual ICollection<EmpApprovalInfo> EmpApprovalInfo { get; set; }
    }
    #endregion

    #region Evaluation
    public class EvaluationData
    {
        public virtual ICollection<EvalDet> Performance { get; set; }
        public virtual ICollection<PerformanceDetail> PerformanceDetail { get; set; }        
        public EmpPersonal EmpPersonal { get; set; }
        public EmpDTR EmpDTR { get; set; }
        public Evaluation Eval { get; set; }
    }

    public class EvalDet
    {
        public int intOlnEvaluationDetail { get; set; }
        public int intOlnEvaluation { get; set; }
        public int intMstPerformance { get; set; }
        public string PerformanceName { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
    }

    public class EvalEmployee
    {
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
    }

    public class EvalList
    {
        public int intOlnEvaluation { get; set; }
        public DateTime EffectivityDate { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmpName { get; set; }
        public string PeriodCoveredFrom { get; set; }
        public string PeriodCoveredTo { get; set; }
    }
    #endregion

    #region Payroll Employee Detail
    public class PayrollData
    {
        public PayrollDetailEmployee PayrollEmp { get; set; }
        public List<PayrollDetailEmployeeOtherDeduction> PayrollODeduc { get; set; }
        public List<PayrollDetailEmployeeOtherIncome> PayrollOInc { get; set; }
    }

    public class PayrollDetailList
    {
        public int intTrnPayroll { get; set; }
        public string PayrollDate { get; set; }
        public string Remarks { get; set; }
        public string intMstEmpPersonal { get; set; }
    }

    public class PayrollDetailEmployee
    {
        public int intTrnPayroll { get; set; }
        public string BatchNumber { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EmployeeName { get; set; }
        public double DailyRate { get; set; }
        public double MonthlyRate { get; set; }
        public double NoOfWorkDays { get; set; }
        public double NoOfRHoliday { get; set; }
        public double NoOfSHoliday { get; set; }
        public double NoOfHolidays { get; set; }
        public double NoOfSLVL { get; set; }
        public double BasicAmount { get; set; }
        public double BasicCola { get; set; }
        public double Month13thPay { get; set; }
        public double OvertimePay { get; set; }
        public double RHolidayPay { get; set; }
        public double SHolidayPay { get; set; }
        public double MonetizePay { get; set; }
        public double OtherIncome { get; set; }
        public double AbsenceAmount { get; set; }
        public double TardyAmount { get; set; }
        public double UTAmount { get; set; }
        public double GrossPay { get; set; }
        public double PHICEmployee { get; set; }
        public double HDMFEmployee { get; set; }
        public double SSSEmployee { get; set; }
        public double WithholdingTax { get; set; }
        public double OtherDeduction { get; set; }
        public double NetPay { get; set; }
        public double VLSLAmount { get; set; }
        public double NoOfLeaveMonetize { get; set; }
        public double DaysAbsent { get; set; }
        public double BasicAllowance { get; set; }
        public double DiscAllowance { get; set; }
        public double Cola { get; set; }
        
        public virtual ICollection<PayrollDetailEmployeeOtherDeduction> OtherDeductionDetail { get; set; }
        public virtual ICollection<PayrollDetailEmployeeOtherIncome> OtherIncomeDetail { get; set; }
        public double TotalOtherDeductionDetail { get; set; }
        public double TotalOtherIncomeDetail { get; set; }
    }

    public class PayrollDetailEmployeeOtherDeduction
    {
        public int intTrnPayrollOtherDeductionDetail { get; set; }
        public string DeductionName { get; set; }
        public double Amount { get; set; }
    }

    public class PayrollDetailEmployeeOtherIncome
    {
        public int intTrnPayrollOtherIncomeDetail { get; set; }
        public string IncomeName { get; set; }
        public double Amount { get; set; }
    }
    #endregion  

    #region Gate Pass
    public class GatePassList
    {
        public int intOlnGatePass { get; set; }
        public DateTime DateFiled { get; set; }        
        public DateTime GatePassDate { get; set; }
        public string Remarks { get; set; }
        public string GatePassTime { get; set; }
        public bool isCancelled { get; set; }        
        public bool isDisapproved { get; set; }
        public string NoOfHours { get; set; }
        public string Status { get; set; }
        public string NextApprover { get; set; }
        public bool isOBHoliday { get; set; }
        public string HolidayName { get; set; }
    }

    public class GatePassApprovalList
    {
        public int intOlnGatePass { get; set; }
        public string EmpName { get; set; }
        public DateTime GatePassDate { get; set; }
        public DateTime DateFiled { get; set; }
        public string GatePassType { get; set; }
        public string NoOfHours { get; set; }
        public string Remarks { get; set; }
        public bool isOBHoliday { get; set; }
        public string HolidayName { get; set; }
    }

    public class GatePassApprovalInfo
    {
        public GatePass gp { get; set; }
        public virtual ICollection<EmpApprovalInfo> EmpApprovalInfo { get; set; }
    }

    public class GatePassRegionalList
    {
        public int intOlnGatePass { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime GatePassDate { get; set; }
        public string EmpName { get; set; }
        public string BranchName { get; set; }
        public string Remarks { get; set; }
        public string GatePassTime { get; set; }
    }
    #endregion

    #region DTR Adjustment
    public class DTRAdjList
    {
        public int intOlnDTRAdjustment { get; set; }
        public string intMstEmpPersonal { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public DateTime DateFiled { get; set; }
        public string HeadApproval { get; set; }
        public string Reason { get; set; }
        public string DTRAdjTime { get; set; }        
        public double NoOfHours { get; set; }
        public string Status { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
    }

    public class DTRAdjustmentApprovalListMain
    {
        public ICollection<DTRAdjustmentApprovalList> DTRAdjAppList { get; set; }
        public int PayrollMonth { get; set; }
        public int PayrollPeriod { get; set; }
        public int PayrollYear { get; set; }
    }

    public class DTRAdjustmentApprovalList
    {
        public int intOlnDTRAdjustment { get; set; }
        public string EmpName { get; set; }
        public DateTime AdjustmentDate { get; set; }        
        public string AdjustmentTime { get; set; }
    }
    #endregion


   
    public static class OtherDAL
    {
        public static List<ComboBoxSource> GetComboData()
        {
          
            SqlConnection con = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());

      
            var source = new List<ComboBoxSource>();

            try
            {

                SqlCommand cmd = new SqlCommand("spBindDDLSampling", con);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var data = new ComboBoxSource()
                        {
                            ValueMember = rdr[1],
                            DisplayMember = rdr[0].ToString()
                        };

                        source.Add(data);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return source;
        }
    }
}
