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
    public class GatePassBAL
    {

        OvertimeBAL _otBAL = new OvertimeBAL();
        GatePassDAL _gpDAL = new GatePassDAL();

        public List<GatePassList> GetGatePassList(string intMstEmpPersonal, int id)
        {
            return _gpDAL.GetDataGatePassList(intMstEmpPersonal, id);
        }

        public GatePass GetGatePass(int intOlnGatePass)
        {
            return _gpDAL.GetDataGatePass(intOlnGatePass);
        }

        public string SaveGatePass(GatePass gp, string branchCode)
        {
            double _noOfHrs = 0.00;
            
            _noOfHrs = _otBAL.ComputeHours(Convert.ToDateTime(gp.TimeOut), Convert.ToDateTime(gp.TimeIn));
            gp.NoOfHours = _noOfHrs;

            if (gp.NoOfHours <= 0){
                throw new Exception("Duration should be greater than zero(0).");                
            }
            if ((gp.TimeIn == null) || (gp.TimeIn == "")) {
                throw new Exception("Please enter TIME IN.");
            }
            if ((gp.TimeOut == null) || (gp.TimeOut == "")) {
                throw new Exception("Please enter TIME OUT.");
            }
            if (((gp.TimeIn == "") && (gp.TimeOut == "")) &&
                (!gp.TimeIn.Contains("am") || !gp.TimeIn.Contains("AM") || !gp.TimeIn.Contains("pm") || !gp.TimeIn.Contains("PM")) &&
                (!gp.TimeOut.Contains("am") || !gp.TimeOut.Contains("AM") || !gp.TimeOut.Contains("pm") || !gp.TimeOut.Contains("PM")))
            {
                throw new Exception("Please enter a valid time.");
            }
            
            //if (gp.NoOfHours == 0)            
            return _gpDAL.SaveDataGatePass(gp, branchCode);
        }

        public List<GatePassApprovalList> GetGatePassApprovalList(int intMstCompany, string codeMstBranch, string intMstEmpPersonal, int intOlnGatePassType)
        {
            return _gpDAL.GetDataGatePassApprovalList(intMstCompany, codeMstBranch, intMstEmpPersonal, intOlnGatePassType);
        }

        public string ApproveGatePass(string ApprovedBy, ICollection<GatePassApprovalList> gpApproval)
        {
            string strMessage = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("intOlnGatePass");
            dt.Columns.Add("ApprovedBy");

            try
            {
                foreach (var item in gpApproval)
                {
                    dt.Rows.Add(item.intOlnGatePass, ApprovedBy);
                }

                strMessage = _gpDAL.ApproveDataGatePass(dt);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }

        public string CancelGatePass(int intOlnGatePass)
        {
            return _gpDAL.CancelGatePass(intOlnGatePass);
        }

        public GatePassApprovalInfo GetApprovalInfo(int intOlnId, string intMstEmpPersonal)
        {
            GatePassApprovalInfo gpApp = new GatePassApprovalInfo()
            {
                gp = GetGatePass(intOlnId),
                EmpApprovalInfo = UtilitiesDAL.GetApprovalInfo(intOlnId, intMstEmpPersonal, "GatePass")
            };            

            return gpApp;
        }

        public List<GatePassRegionalList> GetGatePassRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition)
        {
            return _gpDAL.GetGatePassRegionalList(intMstEmpPersonal, intMstCompany, codeBranchCode, intMstPosition);
        }
    }
}
