using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISOnline.Objects
{
        public class OvertimeMealsList
        {
            public int ID { get; set; }
            public string DateFiledApplied { get; set; }
            public string DateFiled { get; set; }
            public int RequestServiceID { get; set; }
            public string strRequestService { get; set; }
            public string Reason { get; set; }
            public string Status { get; set; }
            public string NextApprover { get; set; }
        }

        public class OvertimeMealsDetails
        {
            public string intMstEmpPersonal { get; set; }
            public string EmployeeName { get; set; }
            public string PositionName { get; set; }
        }

        public class OvertimeMealsForApproval
        {
            public int ID { get; set; }
            public string DateFiled { get; set; }
            public string DateFiledApplied { get; set; }
            public string AppliedBy { get; set; }
            public int RequestServiceID { get; set; }
            public string strRequestService { get; set; }
            public string PositionName { get; set; }
            public string Branch { get; set; }
            public string Reason { get; set; }
            public string ApproveDate { get; set; }
        }

        public class OvertimeMeals
        {
            public DateTime DateFiled { get; set; }
            public DateTime DateApplied { get; set; }
            public string intMstEmpPersonal { get; set; }
            public int RequestServiceID { get; set; }
            public string RequestServiceType { get; set; }
            public string Reason { get; set; }
        }
    
}
