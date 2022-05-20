using System;

namespace HRISOnline.Objects
{
    #region Employee
    public class EmpPersonal
    {
        public string intMstEmpPersonal { get; set; }
        public int intMstCompany { get; set; }
        public string Company { get; set; }
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
        public string TaxCode { get; set; }
        public string BankAccountNo { get; set; }
        public string EmployeePic { get; set; }
        public string intMstEmpPersonalOriginal { get; set; }
        public string PersonToNotify { get; set; }
        public string Relation { get; set; }
        public string ContactNum { get; set; }
        
    }

    public class EmpDTR
    {
        public int intMstEmpDTR { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string codeMstBranch { get; set; }
        public int intMstDepartment { get; set; }
        public int intMstPosition { get; set; }
        public int intMstWorkShift { get; set; }
        public string codeMstBranchHome { get; set; }
        public DateTime DateHired { get; set; }
        public DateTime DateSeparated { get; set; }
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
    }

    public class EmpChildren
    {
        public int intMstEmpChildren { get; set; }        
        public string ChildName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class EmpEducation
    {
        public int intMstEmpEducation { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EduLevel { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        //public string DateGraduated { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Honors { get; set; }
    }

    
    public class EmpEligibility
    {
        public int intMstEmpEligibility { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string Eligibility { get; set; }
        public string DateTaken { get; set; }
        public string Place { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseDate { get; set; }
        public string ExpiryDate { get; set; }

       
    }

    public class EmpFamily
    {
        public int intMstEmpFamily { get; set; }
        public string intMstEmpPersonal { get; set; }
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

    public class EmpWorkExperience
    {
        public int intMstWorkExperience { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Position { get; set; }
        public string OfficeCompany { get; set; }
        public string MonthlySalary { get; set; }
        //public string EmploymentStatus { get; set; }
        public string ContactNumber { get; set; }
        public string Reason { get; set; }
        public bool isGovernment { get; set; }
        public string urlString { get; set; }

      
    }

    public class EmpSemenarsTraining
    {
        public int intMstEmpTraining { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string TrainingTitle { get; set; }
        public string DateStart { get; set; }
        public string strSponsor { get; set; }
        public string Location { get; set; }
    }

    public class EmpDTRData
    {
        public string BiometricDate { get; set; }
        public string BiometricDayName { get; set; }
        public string WorkShiftName { get; set; }
        public string DayType { get; set; }
        public string TimeIn1 { get; set; }
        public string TimeOut1 { get; set; }
        public string TimeIn2 { get; set; }
        public string TimeOut2 { get; set; }        
        public double RegularHrs { get; set; }
        public string LeaveType { get; set; }
        public double OTHrs { get; set; }
        public double DaysAbsent { get; set; }
        public double MinutesLate { get; set; }
        public double MinutesUT { get; set; }
        public bool isOfficialBusiness { get; set; }
    }    

    public class DocPath
    {
        public string strDocPath { get; set; }
    }

    public class ApproversName
    {
        public string FirstApprover { get; set; }
        public string SecondApprover { get; set; }
    }

    public class AwardsAndBonus
    {
        public int intTrnIncentive { get; set; }
        public string intMstEmpPersonal { get; set; }        
        public string BatchNumber { get; set; }
        public DateTime IncentiveDate { get; set; }
        public string Remarks { get; set; }
        public string AwardAndBonusDesc { get; set; }
    }
    #endregion

    #region Toolbox
    public class MemorandumToolbox
    {
        public int intTrnMemo { get; set; }
        public string MemoNo { get; set; }
        public string Subject { get; set; }
        public DateTime MemoDate { get; set; }
        public string MemoDescription { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public bool isOpened { get; set; }
        public DateTime DateOpened { get; set; }
        public bool isAcknowledged { get; set; }
        public string DateAcknowledged { get; set; }
        public int intReaction { get; set; }
        public string strReaction { get; set; }
        public int IsAllowed { get; set; }
    }

    public class NotificationToolbox
    {
        public int intTrnNotification { get; set; }
        public string DocNo { get; set; }
        public string Subject { get; set; }
        public string NotiDescription { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public bool isOpened { get; set; }
        public DateTime DateOpened { get; set; }
        public bool isAcknowledged { get; set; }
        public string DateAcknowledged { get; set; }
        public int intReaction { get; set; }
        public string strReaction { get; set; }
        public int IsAllowed { get; set; }
    }

    public class EmpNoticeToolbox
    {
        public int intTrnEmpNotice { get; set; }
        public DateTime EmpNoticeDate { get; set; }
        public string NoticeTypeName { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public bool isOpened { get; set; }
        public DateTime DateOpened { get; set; }
        public bool isAcknowledged { get; set; }
        public string DateAcknowledged { get; set; }
        public int intReaction { get; set; }
        public string strReaction { get; set; }
        public int IsAllowed { get; set; }
    }

    public class PolicyToolbox
    {
        public int intTrnPolicy { get; set; }
        public string DocNo { get; set; }
        public DateTime DateEffective { get; set; }
        public string PolicyTitle { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyTypeName { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public bool isOpened { get; set; }
        public DateTime DateOpened { get; set; }
        public bool isAcknowledged { get; set; }
        public string DateAcknowledged { get; set; }
        public int intReaction { get; set; }
        public string strReaction { get; set; }
        public int IsAllowed { get; set; }
    }

    public class RecentReaction
    {
        public int intTrnID { get; set; }
        public int intReaction { get; set; }
        public string strReaction { get; set; }
        public int IsAllowed { get; set; }
    }

    public class AnnouncementCount
    {
        public int MemoCount { get; set; }
        public int NotificationCount { get; set; }
        public int EmpNoticeCount { get; set; }
        public int EmpAuditCount { get; set; }
        public int PolicyCount { get; set; }
        public int TotalCount { get; set; }
        public int EmpApprovalNotification { get; set; }

        public int OTPendingCount { get; set; }
        public int LeavePendingCount { get; set; }
        public int UTPendingCount { get; set; }
        public int OBPendingCount { get; set; }
        public int MPPendingCount { get; set; }
        public int OTMealsCount { get; set; }
        public int TotalPendingCount { get; set; }
        public int WithOutDeviation { get; set; }
        public int MinorDeviation { get; set; }
        public int MajorDeviation { get; set; }
        public int PrevAuditReport { get; set; }
    }
    #endregion

    #region "ONE Access"
        public class ObjUserDetails
        {
            public string userName { get; set; }
            public int intMstSysMasterfile { get; set; }
            public string sysName { get; set; }
            public string sysLink { get; set; }
            public int intMstSysRights { get; set; }
        }
    #endregion

    #region Biometrics
    public class DTRBiometricLogs
    {
        public string dDate { get; set; }
        public string LogTimeIn { get; set; }
        public string LogTimeOut { get; set; }
    }

    public class DTRBiometrics
    {
        public int intBiometric { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string DayDate { get; set; }
        public string dDayName { get; set; }
        public string DayType { get; set; }
        public string EmpDayStat { get; set; }
        public double WorkHour { get; set; }
        public double Tardy { get; set; }
        public double Undertime { get; set; }
        public double Overtime { get; set; }
        public string urlDetails { get; set; }
        public string WorkHourStr { get; set; }
    }   
    #endregion
}
