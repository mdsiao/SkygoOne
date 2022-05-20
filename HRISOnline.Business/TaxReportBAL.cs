using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRISOnline.Objects;
using HRISOnline.Data;
using System.Data;

namespace HRISOnline.Business
{
    public class TaxReportBAL
    {
        TaxReportDAL _TaxReportDAL = new TaxReportDAL();

        public List<EmployeeReports> GetEmployeeReports(string intMstEmpPersonal)
        {
            return _TaxReportDAL.GetEmployeeReports(intMstEmpPersonal);
        }

        public DataTable ViewReport(string storedProcedure, int tblID, string intMstEmpPersonal)
        {
            return _TaxReportDAL.GetReportSource(storedProcedure, tblID, intMstEmpPersonal);
        }
    }
}
