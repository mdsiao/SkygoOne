using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class EmployeeUpdateLogs
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int intIDModuleName { get; set; }
        public string ModuleName { get; set; }
        public string DateUpdated { get; set; }
        public string strStatus { get; set; }
        public List<EmployeeUpdateLogs> UpdateLogs { get; set; }
    }
}
