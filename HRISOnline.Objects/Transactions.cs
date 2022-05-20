using System;
using System.Collections.Generic;

namespace HRISOnline.Objects
{
    
    public class Overtime 
        {
            public int intOlnOvertimeApplication { get; set; }
            public string intMstEmpPersonal { get; set; }
            public DateTime DateFiled { get; set; }
            public DateTime OvertimeDate { get; set; }
            public string PurposeOfWork { get; set; }
            public string TimeStarted { get; set; }
            public string TimeEnded { get; set; }
            public double NoOfHours { get; set; }
            public string Remarks { get; set; }
            public string ImmediateSuperior { get; set; }
            public string AreaSupervisor { get; set; }
            public string HR { get; set; }
            public string HRLiason { get; set; }
            public bool isCancelled { get; set; }
            public bool isDisapproved { get; set; }
            public string DisapprovedBy { get; set; }
            public DateTime DisapprovedDateTime { get; set; }
            public DateTime DateTimeFrom { get; set; }
            public DateTime DateTimeTo { get; set; }
            public string DisapprovedReason { get; set; }
            public string DisapprovedByName { get; set; }
            public double NoOfHoursDisplay { get; set; }
        }

    public class LeaveMaster
   {
       public int intOlnLeaveApplicationHead { get; set; }
       public string intMstEmpPersonal { get; set; }
       public int YearPeriod { get; set; }
       public DateTime DateFiled { get; set; }
       public int intMstLeaveType { get; set; }
       public string HRLiason { get; set; }
       public string ImmediateSuperior { get; set; }
       public string AreaSupervisor { get; set; }
       public string HR { get; set; }
       public string Remarks { get; set; }
       public bool isCancelled { get; set; }
       public bool isDisapproved { get; set; }
       public string DisapprovedBy { get; set; }
       public DateTime DisapprovedDateTime { get; set; }
       public DateTime DateFrom { get; set; }
       public int DateFromAMorPM { get; set; }
       public DateTime DateTo { get; set; }
       public int DateToAMorPM { get; set; }
       public double NoOfDays { get; set; }
       public string DisapproveByName { get; set; }      
       public string DisapproveReason { get; set; }

       public virtual ICollection<LeaveDetails> LeaveDetails { get; set; }
   }
   
    public class LeaveDetails
   {
       public int intOlnLeaveApplicationDetails { get; set; }
       public int intOlnLeaveApplicationHead { get; set; }
       public DateTime DateOfLeave { get; set; }
       public bool isHalfDay { get; set; }
       public string AMorPM { get; set; }
       public double NoOfDays { get; set; }
       public string Reason { get; set; }
       public bool isWithPay { get; set; }
   }

    public class CoopLoan
   {
       public int intOlnCoopLoanApplication { get; set; }
       public string intMstEmpPersonal { get; set; }
       public DateTime DateFiled { get; set; }
       public int TypeOfMembership { get; set; }
       public string PurposeOfLoan { get; set; }
       public int intOlnLoanType { get; set; }
       public double NoOfMonths { get; set; }
       public double AmountApplied { get; set; }
       public double InterestAmount { get; set; }
       public double TotalAmount { get; set; }
       public double AmountToBePaid { get; set; }
       public string CoMaker1 { get; set; }
       public string CoMaker2 { get; set; }
       public string ImmediateSuperior { get; set; }
       public string AreaSupervisor { get; set; }
       public string HR { get; set; }       
       public bool isCancelled { get; set; }
       public bool isDisapproved { get; set; }
       public string DisapprovedBy { get; set; }
       public DateTime DisapprovedDateTime { get; set; }
       public double AmountPaid { get; set; }
       public double AmountBalance { get; set; }
       public virtual ICollection<CoopLoanDetails> LoanDetails { get; set; }
       public bool isGovernment { get; set; }
   }

    public class CoopLoanDetails
   {
       public int intTrnPayroll { get; set; }
       public string BatchNumber { get; set; }
       public double Amount { get; set; }       
   }

    public class GatePass
    {
        public int intOlnGatePass { get; set; }
        public string intMstEmpPersonal { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime GatePassDate { get; set; }
        public int intOlnGatePassType { get; set; }
        public string TimeOut { get; set; }
        public string TimeIn { get; set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
        public string DisapprovedBy { get; set; }
        public DateTime DisapprovedDateTime { get; set; }
        public double NoOfHours { get; set; }
        public string DisapproveByName { get; set; }
        public string DisapproveReason { get; set; }
        public bool isOBHoliday { get; set; }
        public int? intTrnHoliday { get; set; }
    }

    public class DTRAdjustment
    {
        public int intOlnDTRAdjustment { get; set; }
        public string intMstEmpPersonal { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public double NoOfHours { get; set; }
        public string Reason { get; set; }
        public string HeadApproval { get; set; }
        public DateTime HeadDateTime { get; set; }
        public string RegionalApproval { get; set; }
        public DateTime RegionalDateTime { get; set; }
        public bool isCancelled { get; set; }
        public bool isDisapproved { get; set; }
        public string DisapprovedBy { get; set; }
        public DateTime DisapprovedDateTime { get; set; }
    }
}
