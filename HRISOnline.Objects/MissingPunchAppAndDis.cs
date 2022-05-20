using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class MissingPunchAppAndDis
    {
        public int intIDMissingpunch { get; set; }
        public string DateFiled { get; set; }
        public string DatActualDate { get; set; }
        public int AdjustmentTypeID { get; set; }
        public string AdjustmentType { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
        public string Reason { get; set; }
    }
}
