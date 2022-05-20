using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using HRISOnline.Data;
using HRISOnline.Utilities;
using System.Data;

namespace HRISOnline.Business
{
    public class DTRAdjustmentBAL
    {
        OvertimeBAL _otBAL = new OvertimeBAL();
        DTRAdjustmentDAL _dtrAdj = new DTRAdjustmentDAL();

        public string SaveDTRAdjustment(DTRAdjustment dtrAdj)
        {
            double NoOfHrs = 0.00;

            NoOfHrs = _otBAL.ComputeHours(Convert.ToDateTime(dtrAdj.TimeIn), Convert.ToDateTime(dtrAdj.TimeOut));
            dtrAdj.NoOfHours = NoOfHrs;

            if (dtrAdj.NoOfHours <= 0)
            {
                throw new Exception("No. of Hours should be greater than zero(0).");
            }
            if (dtrAdj.TimeIn == null || dtrAdj.TimeIn == "") 
            {
                throw new Exception("Please enter TIME IN.");
            }
            if (dtrAdj.TimeOut == null || dtrAdj.TimeOut == "")
            {
                throw new Exception("Please enter TIME OUT.");
            }
            if (((dtrAdj.TimeIn == "") && (dtrAdj.TimeOut == "")) &&
               (!dtrAdj.TimeIn.Contains("am") || !dtrAdj.TimeIn.Contains("AM") || !dtrAdj.TimeIn.Contains("pm") || !dtrAdj.TimeIn.Contains("PM")) &&
               (!dtrAdj.TimeOut.Contains("am") || !dtrAdj.TimeOut.Contains("AM") || !dtrAdj.TimeOut.Contains("pm") || !dtrAdj.TimeOut.Contains("PM")))
            {
                throw new Exception("Please enter a valid time.");
            }

            return _dtrAdj.SaveDTRAdjustment(dtrAdj);
        }

        public List<DTRAdjList> GetDTRAdjustmentList(string intMstEmpPersonal)
        {
            return _dtrAdj.GetDataDTRAdjustmentList(intMstEmpPersonal);
        }

        public DTRAdjustment GetDTRAdjustment(int intOlnDTRAdjustment)
        {
            return _dtrAdj.GetDataDTRAdjustment(intOlnDTRAdjustment);
        }

        public string CancelDTRAdjustment(int intOlnDTRAdjustment)
        {
            return _dtrAdj.CancelDTRAdjustment(intOlnDTRAdjustment);
        }

        public List<DTRAdjustmentApprovalList> GetDTRAdjustmentApprovalList(int intMstCompany, string codeMstBranch, int intMstPositionSupervisor)
        {
            return _dtrAdj.GetDataDTRAdjustmentApprovalList(intMstCompany, codeMstBranch, intMstPositionSupervisor);
        }

        public string ApproveDTRAdjustment(string ApprovedBy, bool isHRHomeOffice, DTRAdjustmentApprovalListMain adjApproval, int intMstPositionSupervisor)
        {
            string _msg = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("intOlnDTRAdjustment");
            dt.Columns.Add("ApprovedBy");
            dt.Columns.Add("PayrollMonth");
            dt.Columns.Add("PayrollPeriod");
            dt.Columns.Add("PayrollYear");

            try
            {
                foreach (var item in adjApproval.DTRAdjAppList)
                {
                    dt.Rows.Add(item.intOlnDTRAdjustment, ApprovedBy, adjApproval.PayrollMonth, adjApproval.PayrollPeriod, adjApproval.PayrollYear);
                }

                _msg = _dtrAdj.ApproveDataDTRAdjustment(dt, intMstPositionSupervisor, isHRHomeOffice);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());                
            }

            return _msg;
        }
    }
}
