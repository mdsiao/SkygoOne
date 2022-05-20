using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class DisplayListOfApprovals
    {
        public List<DisplayForApproval> DisplayForApproval { get; set; }
        public IEnumerable<DisplayApprovedAndDisApproved> ApprovedAndDisApproved { get; set; }
    }

    public class DisplayForApproval
    {
        public int ID { get; set; }
        public string DateFiled { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int intIDModuleName { get; set; }
        public string strModuleName { get; set; }
        public int intMstPosition { get; set; }
        public string PositionName { get; set; }
        public int intMstBranch { get; set; }
        public string BranchName { get; set; }
        public int intMstDepartment { get; set; }
        public string DepartmentName { get; set; }
        public List<DisplayForApproval> DisplayListOfApprovals { get; set; }
    }

    public class DisplayApprovedAndDisApproved
    {
        public int ID { get; set; }
        public string DateFiled { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string ModuleName { get; set; }
        public string PosistionName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string strStatus { get; set; }
        public string DateApprovedAndDisApproved { get; set; }
    }
}

