using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Data;
using System.Data;

namespace HRISOnline.Business
{
    public class ReportBAL
    {
        OvertimeDAL _otDAL = new OvertimeDAL();
        ReportDAL _rptDAL = new ReportDAL();

        public DataTable PayrollPayslip(int intTrnPayroll, string intMstEmpPersonal, string codeBranchCode)
        {
            string strSql = "Report_PayrollPayslip " + intTrnPayroll + ", '" + codeBranchCode + "', '" + intMstEmpPersonal + "', 0";

            return UtilitiesDAL.GetReportSource(strSql);
        }

        public DataTable PayrollPayslipOtherDeduction(int intTrnPayroll, string intMstEmpPersonal)
        {
            string strSql = "Report_PayrollPayslipOtherDeduction " + intTrnPayroll + ", '" + intMstEmpPersonal + "'";

            return UtilitiesDAL.GetReportSource(strSql);
        }

        public DataTable PayrollPayslipOtherIncome(int intTrnPayroll, string intMstEmpPersonal)
        {
            string strSql = "Report_PayrollPayslipOtherIncome " + intTrnPayroll + ", '" + intMstEmpPersonal + "'";

            return UtilitiesDAL.GetReportSource(strSql);
        }

        public DataTable GatePassReport(int intOlnGatePass)
        {
            string strSql = "[Report_OnlineUndertime] " + intOlnGatePass;

            return UtilitiesDAL.GetReportSource(strSql);
        }

        public DataTable GetOvertimeApprovalListReport(int intMstCompany, string codeMstBranch, int intMstPositionSupervisor, DateTime otDate)
        {
            return _otDAL.GetOvertimeApprovalListReport(intMstCompany, codeMstBranch, intMstPositionSupervisor, otDate);
        }

        public DataTable GetDisapproveReport(string intMstEmpPersonal, DateTime dtFrom, DateTime dtTo, string TransType)
        {
            return _rptDAL.GetDisapproveReport(intMstEmpPersonal, dtFrom, dtTo, TransType);
        }

        public DataTable GetCancelReport(string intMstEmpPersonal, DateTime dtFrom, DateTime dtTo, string TransType)
        {
            return _rptDAL.GetCancelReport(intMstEmpPersonal, dtFrom, dtTo, TransType);
        }

        public DataTable GetSubordinateDTRReport(DateTime dtFrom, DateTime dtTo, string intMstEmpPersonal)
        {
            return _rptDAL.GetSubordinateDTRReport(dtFrom, dtTo, intMstEmpPersonal);
        }

        public DataTable GetApprovedAppsReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            return _rptDAL.GetApprovedAppsReport(dtFrom, dtTo, strModule, intMstEmpPersonal);
        }

        public DataTable PayrollAwardAndBonus(int intTrnIncentive, string intMstEmpPersonal)
        {
            string strSql = "Report_AwardAndBonusPayslip " + intTrnIncentive + ", '', '" + intMstEmpPersonal + "', 0";

            return UtilitiesDAL.GetReportSource(strSql);
        }

        public DataTable GetOB(DateTime dtFrom, DateTime dtTo, string intMstEmpPersonal, string codeMstBranch, int intMstPosition)
        {
            return _rptDAL.GetOB(dtFrom, dtTo, intMstEmpPersonal, codeMstBranch, intMstPosition);
        }

        public DataTable GetHRLeave(DateTime dtFrom, DateTime dtTo, string intMstEmpPersonal, string codeMstBranch, int intMstPosition)
        {
            return _rptDAL.GetHRLeave(dtFrom, dtTo, intMstEmpPersonal, codeMstBranch, intMstPosition);
        }

        public DataTable GetApprAdjustmentReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            return _rptDAL.GetApprAdjustmentReport(dtFrom, dtTo, strModule, intMstEmpPersonal);
        }
        public DataTable GetDisApprAdjustmentReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            return _rptDAL.GetDisApprAdjustmentReport(dtFrom, dtTo, strModule, intMstEmpPersonal);
        }
        public DataTable GetOvertimeMealsReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            return _rptDAL.GetOvertimeMealsReport(dtFrom, dtTo, strModule, intMstEmpPersonal);
        }
    }
}
