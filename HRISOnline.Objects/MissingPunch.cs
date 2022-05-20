using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class MissingPunch
    {
        public int intIDMissingpunch { get; set; }
        public string intMstEmpPersonal { get; set; }
        public DateTime DateFiled { get; set; }
        public DateTime DatActualDate { get; set; }
        public int AdjustmentType { get; set; }
        public string MissedTimeIn { get; set; }
        public string MissedTimeOut { get; set; }
        public string MissedNoOfHours { get; set; }
        //public DateTime DatActualDateEnd { get; set; }
        public string ActualTimeFrom { get; set; }
        public string ActualTimeTo { get; set; }
        public string ActualNoOfHours { get; set; }
        //public string EmpName { get; set; }
        //public DateTime HolidayDate { get; set; }
        //public string HolidayType { get; set; }
        //public string NameOfHoliday { get; set; }
        public string Reason { get; set; }
        
    }

    public class MissingPunchList
    {
        public int intIDMissingpunch { get; set; }
        public string DateFiled { get; set; }
        public string DatActualDate { get; set; }
        public int AdjustmentTypeID { get; set; }
        public string AdjustmentType { get; set; }
        public string MissedPunch { get; set; }
        public string TimeinTimeOut { get; set; }
        public string NoOfHours { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string NextApprover { get; set; }
        //public List<MissingPunchList> ListPunch { get; set; }
    }

    public class MissedPunch
    {
        public int intIDMissingpunch { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string DateFiled { get; set; }
        public string DatActualDate { get; set; }
        public int AdjustmentTypeID { get; set; }
        public string AdjustmentType { get; set; }
        public string MissedTimeIn { get; set; }
        public string MissedTimeOut { get; set; }
        public string MissedNoOfHours { get; set; }
        public string Reason { get; set; }
    }
    public class MissedPunchLogs 
    {
        public int intBiometricID { get; set; }
        public string DatLogDate { get; set; }
        public string BioTimeIn { get; set; }
        public string BioTimeOut { get; set; }
        public string BioNoHours { get; set; }
        public string strStatus { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string DisApporvedBy { get; set; }
        public string DisapproveDateTime { get; set; }
        public string DisapproveReason { get; set; }
    }

    public class ViewDetailsLogs
    {
        public List<MissedPunch> ViewMissedPunch { get; set; }
        public List<MissedPunchLogs> ViewMissedPunchLogs { get; set; }

    }
}
