using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using HRISOnline.Data;
using System.Data;

namespace HRISOnline.Business
{
    public class LeaveBAL
    {

        LeaveDAL _leaveDAL = new LeaveDAL();

        public List<LeaveList> GetLeaveList(string intMstEmpPersonal)
        {
            return _leaveDAL.GetDataLeaveList(intMstEmpPersonal);
        }

        public LeaveMaster GetLeave(int intOlnLeaveApplicationHead)
        {
            return _leaveDAL.GetDataLeaveMaster(intOlnLeaveApplicationHead);
        }

        public string SaveLeave(LeaveMaster leave, bool hasWorkOnSat)
        {
            string result = string.Empty;
            //double TotalNoOfDays = 0.0;
            DataTable dt = new DataTable();
            DateTime currDate;            
            double _availableLeaves = _leaveDAL.GetDataAvailableLeave(leave.intMstEmpPersonal, leave.intMstLeaveType, leave.DateFrom.Year);

            //set the YEAR PERIOD to what year is the DATE FROM column
            leave.YearPeriod = leave.DateFrom.Year;

            try{

                if (leave.Remarks == null || leave.Remarks == string.Empty){
                    result = "Please enter your reason.";   
                }
                else if (leave.intMstLeaveType == 0) {
                    result = "Please select a leave type.";
                }
                else if (leave.NoOfDays <= 0)
                {
                    result = "<strong>DATE FROM</strong> should not be greater than <strong>DATE TO</strong>. Please re-enter the dates.";
                }
                else
                {

                    dt.Columns.Add("intOlnLeaveApplicationDetails");
                    dt.Columns.Add("intOlnLeaveApplicationHead");
                    dt.Columns.Add("DateOfLeave");
                    dt.Columns.Add("isHalfDay");
                    dt.Columns.Add("AMorPM");
                    dt.Columns.Add("NoOfDays");
                    dt.Columns.Add("Reason");
                    dt.Columns.Add("isWithPay");                   

                    //process leave details
                    currDate = leave.DateFrom;
                    while (currDate <= leave.DateTo)
                    {
                        bool isWithPay = false;
                        double _noOfDay = 1.0;
                        string _AMorPM = string.Empty;
                        bool _isHalfDay = false;

                        if (_availableLeaves >= 1)
                        {
                            isWithPay = true;

                            switch (currDate.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                case DayOfWeek.Tuesday:
                                case DayOfWeek.Wednesday:
                                case DayOfWeek.Thursday:
                                case DayOfWeek.Friday:
                                    if (currDate == leave.DateFrom && leave.DateFromAMorPM != 1)
                                    {
                                        _availableLeaves -= 0.5;
                                        goto am;
                                    }
                                    else if (currDate == leave.DateTo && leave.DateToAMorPM != 1)
                                    {
                                        _availableLeaves -= 0.5;
                                        goto pm;
                                    }
                                    else
                                    {
                                        _availableLeaves -= 1;
                                        goto end;
                                    }

                                case DayOfWeek.Saturday:
                                    if (hasWorkOnSat == true)
                                    {
                                        if (currDate == leave.DateFrom && leave.DateFromAMorPM != 1)
                                        {
                                            _availableLeaves -= 0.5;
                                            goto am;
                                        }
                                        else if (currDate == leave.DateTo && leave.DateToAMorPM != 1)
                                        {
                                            _availableLeaves -= 0.5;
                                            goto pm;
                                        }
                                        else
                                        {
                                            _availableLeaves -= 1;
                                            goto end;
                                        }
                                    }
                                    break;
                            }



                        }
                        else if (_availableLeaves == 0.5)
                        {
                            if ((currDate != leave.DateTo) || (currDate == leave.DateTo && leave.DateToAMorPM == 1))
                            {
                                _noOfDay = 0.5;
                                _AMorPM = "AM";
                                _isHalfDay = true;
                                isWithPay = true;
                                //automatic create the half day WITH PAY
                                switch (currDate.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                    case DayOfWeek.Tuesday:
                                    case DayOfWeek.Wednesday:
                                    case DayOfWeek.Thursday:
                                    case DayOfWeek.Friday:

                                        dt.Rows.Add(0, leave.intOlnLeaveApplicationHead, currDate, _isHalfDay,
                                            _AMorPM, _noOfDay, leave.Remarks, isWithPay);
                                        _availableLeaves -= 0.5;
                                        break;

                                    case DayOfWeek.Saturday:
                                        if (hasWorkOnSat == true)
                                        {
                                            dt.Rows.Add(0, leave.intOlnLeaveApplicationHead, currDate, _isHalfDay,
                                            _AMorPM, _noOfDay, leave.Remarks, isWithPay);
                                            _availableLeaves -= 0.5;
                                        }
                                        break;
                                }


                                _AMorPM = "PM";
                                isWithPay = false;

                                goto end;
                            }
                            else
                            {
                                _noOfDay = 0.5;
                                _AMorPM = leave.DateToAMorPM == 2 ? "AM" : "PM";
                                _isHalfDay = true;
                                isWithPay = true;

                                goto end;
                            }
                        }
                        else
                        {

                            isWithPay = false;
                            if (currDate < leave.DateTo)
                            {
                                goto end;
                            }
                        }

                    am:
                        //check for Start Date                        
                        if (currDate == leave.DateFrom)
                        {
                            if (leave.DateFromAMorPM != 1)
                            {
                                _noOfDay = 0.5;
                                _AMorPM = leave.DateFromAMorPM == 2 ? "AM" : "PM";
                                _isHalfDay = true;
                            }
                        }

                    pm:
                        //check for End Date
                        if (currDate == leave.DateTo)
                        {
                            if (leave.DateToAMorPM != 1)
                            {
                                _noOfDay = 0.5;
                                _AMorPM = leave.DateToAMorPM == 2 ? "AM" : "PM";
                                _isHalfDay = true;
                            }
                        }

                    end:
                        switch (currDate.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                            case DayOfWeek.Tuesday:
                            case DayOfWeek.Wednesday:
                            case DayOfWeek.Thursday:
                            case DayOfWeek.Friday:

                                dt.Rows.Add(0, leave.intOlnLeaveApplicationHead,
                                        currDate, _isHalfDay, _AMorPM, _noOfDay, leave.Remarks, isWithPay);
                                break;

                            case DayOfWeek.Saturday:
                                if (hasWorkOnSat == true)
                                {
                                    dt.Rows.Add(0, leave.intOlnLeaveApplicationHead,
                                        currDate, _isHalfDay, _AMorPM, _noOfDay, leave.Remarks, isWithPay);
                                }
                                break;
                        }

                        currDate = currDate.AddDays(1);
                    }

                    result = _leaveDAL.SaveLeave(leave, dt);

                }

            }
            catch (Exception ex) {
                result = ex.Message.ToString();
            }

            return result;
        }

        public string CancelLeave(int intOlnLeaveApplicationHead)
        {
            return _leaveDAL.CancelLeave(intOlnLeaveApplicationHead);
        }

        public string CancelLeave(ICollection<LeaveApproval> leaves, string intMstEmpPersonal)
        {
            string strMessage = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("intOlnLeaveApplicationHead");
            dt.Columns.Add("intMstApprovedBy");

            try
            {
                foreach (var item in leaves){
                    dt.Rows.Add(item.intOlnLeaveApplicationHead, intMstEmpPersonal);
                }

                strMessage = _leaveDAL.CancelLeaveByBatch(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }

        public List<LeaveApproval> GetLeaveApprovalList(int intMstCompany, string codeMstBranch, string intMstEmpPersonal)
        {
            return _leaveDAL.GetDataLeaveApproval(intMstCompany, codeMstBranch, intMstEmpPersonal);
        }

        public string ApproveLeave(string intMstEmpPersonal, ICollection<LeaveApproval> leaves, bool isHR = false)
        {
            string strMessage = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("intOlnLeaveApplicationHead");
            dt.Columns.Add("intMstApprovedBy");

            try
            {
                foreach (var item in leaves)
                {
                    dt.Rows.Add(item.intOlnLeaveApplicationHead, intMstEmpPersonal);
                }

                strMessage = _leaveDAL.ApproveDataLeave(intMstEmpPersonal, dt, isHR);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }

        public List<LeaveHRRegionalList> GetHRRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition)
        {
            return _leaveDAL.GetDataHRRegionalList(intMstEmpPersonal, intMstCompany, codeBranchCode, intMstPosition);
        }

        public List<LeaveBalance> GetLeaveBalance(string intMstEmpPersonal, int intYear)
        {
            return _leaveDAL.GetDataLeaveBalance(intMstEmpPersonal, intYear);
        }

        public double GetAvailableLeave(string intMstEmpPersonal, int intMstLeaveType, int intYear)
        {
            return _leaveDAL.GetDataAvailableLeave(intMstEmpPersonal, intMstLeaveType, intYear);
        }

        public double GetNoOfDays(DateTime dtFrom, DateTime dtTo, bool hasWorkOnSat)
        {
            return Utilities.Functions.ComputeNoOfDays(dtFrom, dtTo, hasWorkOnSat);
        }

        public LeaveApprovalInfo GetApprovalInfo(int intOlnId, string intMstEmpPersonal)
        {
            LeaveApprovalInfo lvApp = new LeaveApprovalInfo()
            {
                lv = GetLeave(intOlnId),
                EmpApprovalInfo = UtilitiesDAL.GetApprovalInfo(intOlnId, intMstEmpPersonal, "Leave")
            };

            return lvApp;
        }
    }
}
