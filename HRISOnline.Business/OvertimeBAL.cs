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

    public class OvertimeBAL
    {

        OvertimeDAL _otDAL = new OvertimeDAL();

        public List<OvertimeList> GetOvertimeList(string intMstEmpPersonal)
        {
            return _otDAL.GetDataOvertimeList(intMstEmpPersonal);
        }

        public Overtime GetOvertime(int intOlnOvertimeApplication)
        {
            return _otDAL.GetDataOvertime(intOlnOvertimeApplication);
        }

        public string SaveOvertime(Overtime ot, int sessWorkShift)
        {
            string result = string.Empty;
            double _noOfHrs = 0.00;
            
            ot.NoOfHours = GetComputeNoOfHours(Convert.ToDateTime(ot.TimeStarted), Convert.ToDateTime(ot.TimeEnded), ot.OvertimeDate, sessWorkShift);
            ot.NoOfHoursDisplay = GetComputeNoOfHours(Convert.ToDateTime(ot.TimeStarted), Convert.ToDateTime(ot.TimeEnded), ot.OvertimeDate, sessWorkShift, true);

            if ((ot.PurposeOfWork == "") || (ot.PurposeOfWork == null))
            {
                throw new Exception("Please enter purpose of work.");
            }
            if (ot.TimeStarted == ot.TimeEnded)
            {
                throw new Exception("<strong>Start Time</strong> and <strong>End Time</strong> should not be the same.");
            }
            if (ot.NoOfHours < 0)
            {
                throw new Exception("<strong>Start Time</strong> should not be greater than <strong>End Time</strong>. Please re-enter time.");
            }

            if (result == string.Empty)
            {
                result = _otDAL.SaveOvertime(ot);
            }

            return result;
        }

        public string CancelOvertime(int intOlnOvertimeApplication)
        {
            return _otDAL.CancelOvertime(intOlnOvertimeApplication);
        }

        public double GetComputeNoOfHours(DateTime TimeStarted, DateTime TimeEnded, DateTime otDate, int sessWorkShift, bool isNoOfHoursDispaly = false)
        {
            DateTime dStartData, dEndData, dEndShift, dStartShift, dNoonData, dLunchInData, dLunchOutData;
            string shiftStart, shiftEnd, shiftLunchOut, shiftLunchIn;
            double OTHours;
            int _dayType = 0;

            _dayType = GetDayType(sessWorkShift, otDate);
            shiftStart = GetStartOfShift(sessWorkShift, otDate);
            shiftEnd = GetEndOfShift(sessWorkShift, otDate);
            shiftLunchOut = GetLunchShift(sessWorkShift, otDate, "LunchTimeOut");
            shiftLunchIn = GetLunchShift(sessWorkShift, otDate, "LunchTimeIn");

            dStartData = Convert.ToDateTime(otDate.ToShortDateString() + " " + TimeStarted.ToShortTimeString());
            dEndData = Convert.ToDateTime(otDate.ToShortDateString() + " " + TimeEnded.ToShortTimeString());
            dEndShift = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftEnd);
            dStartShift = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftStart);
            dLunchInData = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftLunchIn);
            dLunchOutData = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftLunchOut);
            dNoonData = Convert.ToDateTime(otDate.ToShortDateString() + " " + "12:00 PM");
            if (isNoOfHoursDispaly == false)
                OTHours = Functions.ComputeNoOfHoursOT(TimeStarted, TimeEnded);
            else
                OTHours = ComputeHours(TimeStarted, TimeEnded);

            if (_dayType != 2)
            {
                if ((dStartData > dStartShift) && (dStartData <= dNoonData))
                {
                    if (isNoOfHoursDispaly == false)
                        OTHours = Functions.ComputeNoOfHoursOT(dStartShift, dEndData);
                    else
                        OTHours = ComputeHours(dStartShift, dEndData);

                    return OTHours;
                }
                else if ((dStartData < dEndShift) && (dStartData > dNoonData))
                {
                    if (isNoOfHoursDispaly == false)
                        OTHours = Functions.ComputeNoOfHoursOT(dEndShift, dEndData);
                    else
                        OTHours = ComputeHours(dEndShift, dEndData);

                    return OTHours;
                }
                else if ((dEndData > dStartShift) && (dEndData <= dNoonData))
                {
                    if (isNoOfHoursDispaly == false)
                        OTHours = Functions.ComputeNoOfHoursOT(dStartData, dStartShift);
                    else
                        OTHours = ComputeHours(dStartData, dStartShift);

                    return OTHours;
                }
                else if ((dEndData < dEndShift) && (dEndData > dNoonData))
                {
                    if (isNoOfHoursDispaly == false)
                        OTHours = Functions.ComputeNoOfHoursOT(dStartData, dEndShift);
                    else
                        OTHours = ComputeHours(dStartData, dEndShift);

                    return OTHours;
                }
            }
            else
            {
                double RDHours = 0;
                string[] splitStr;

                if (((dStartData < dLunchOutData) && (dEndData < dLunchOutData)) || ((dStartData > dLunchInData) && (dEndData > dLunchInData))
                    || (dStartData == dLunchInData))
                {
                    if (isNoOfHoursDispaly == false)
                        RDHours = Functions.ComputeNoOfHoursOT(dStartData, dEndData);
                    else
                        RDHours = ComputeHours(dStartData, dEndData);
                }
                else
                {
                    if (isNoOfHoursDispaly == false)
                        RDHours = Functions.ComputeNoOfHoursOT(dStartData, dLunchOutData);
                    else
                        RDHours = ComputeHours(dStartData, dLunchOutData);

                    if (dEndData > dLunchInData)
                    {
                        if (isNoOfHoursDispaly == false)
                            RDHours += Functions.ComputeNoOfHoursOT(dLunchInData, dEndData);
                        else
                            RDHours += ComputeHours(dLunchInData, dEndData);
                    }

                    if (RDHours.ToString().Contains("."))
                    {
                        splitStr = RDHours.ToString().Split('.');

                        switch (splitStr[1].ToString())
                        {
                            case "6":
                                RDHours = Convert.ToInt32(splitStr[0]) + 1;
                                break;
                        }
                    }
                }

                OTHours = RDHours;
            }

            return OTHours;
        }

        public double ComputeHours(DateTime TimeStarted, DateTime TimeEnded)
        {
            return Functions.ComputeNoOfHours(TimeStarted, TimeEnded);
        }

        public double ComputeHoursOrig(DateTime TimeStarted, DateTime TimeEnded)
        {
            return Functions.ComputeNoOfHoursOT(TimeStarted, TimeEnded);
        }

        public List<OvertimeApproval> GetOvertimeApprovalList(int intMstCompany, string codeMstBranch, string intMstEmpPersonal)
        {
            return _otDAL.GetDataOvertimeApprovalList(intMstCompany, codeMstBranch, intMstEmpPersonal);
        }

        public string ApproveOvertime(string intMstEmpPersonal, ICollection<OvertimeApproval> otApproval, bool isFinanceManager = false, bool isFinanceApprovalModule = false)
        {

            string strMessage = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("intOlnOvertimeApplication");
            dt.Columns.Add("intMstApprovedBy");

            try
            {
                foreach (var item in otApproval)
                {
                    dt.Rows.Add(item.intOlnOvertimeApplication, intMstEmpPersonal);
                }

                strMessage = _otDAL.ApproveDataOvertime(intMstEmpPersonal, dt, isFinanceManager, isFinanceApprovalModule);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }

        public List<OvertimeHRRegionalList> GetHRRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition, string option, string param)
        {
            return _otDAL.GetDataHRRegionalList(intMstEmpPersonal, intMstCompany, codeBranchCode, intMstPosition, option, param);
        }

        public string GetEndOfShift(int intMstWorkShift, DateTime otDate)
        {
            var _dayName = otDate.DayOfWeek.ToString();
            var strSQL = string.Empty;
            var result = string.Empty;

            strSQL = "SELECT intMstDayType FROM tblMstWorkShiftDetails WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            if (UtilitiesDAL.GetSingleData(strSQL) == "1")
            {
                strSQL = "SELECT FORMAT(CONVERT(datetime, CONVERT(varchar, GETDATE(), 101) + ' ' + CONVERT(varchar, [TimeOut], 108)), 'hh:mm tt') AS OTTIme ";
                strSQL += "FROM tblMstWorkShiftDetails ";
                strSQL += "WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";   
            }
            else
            {
                strSQL = "SELECT FORMAT(CONVERT(datetime, CONVERT(varchar, GETDATE(), 101) + ' ' + CONVERT(varchar, [TimeIn], 108)), 'hh:mm tt') AS OTTIme ";
                strSQL += "FROM tblMstWorkShiftDetails ";
                strSQL += "WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            }

            return UtilitiesDAL.GetSingleData(strSQL);
        }

        public string GetStartOfShift(int intMstWorkShift, DateTime otDate)
        {
            var _dayName = otDate.DayOfWeek.ToString();
            var strSQL = string.Empty;
            var result = string.Empty;

            strSQL = "SELECT intMstDayType FROM tblMstWorkShiftDetails WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            if (UtilitiesDAL.GetSingleData(strSQL) == "1")
            {
                strSQL = "SELECT FORMAT(CONVERT(datetime, CONVERT(varchar, GETDATE(), 101) + ' ' + CONVERT(varchar, [TimeIn], 108)), 'hh:mm tt') AS OTTIme ";
                strSQL += "FROM tblMstWorkShiftDetails ";
                strSQL += "WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            }
            else
            {
                strSQL = "SELECT FORMAT(CONVERT(datetime, CONVERT(varchar, GETDATE(), 101) + ' ' + CONVERT(varchar, [TimeOut], 108)), 'hh:mm tt') AS OTTIme ";
                strSQL += "FROM tblMstWorkShiftDetails ";
                strSQL += "WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            }

            return UtilitiesDAL.GetSingleData(strSQL);
        }

        public string GetLunchShift(int intMstWorkShift, DateTime otDate, string fieldName)
        {
            var _dayName = otDate.DayOfWeek.ToString();
            var strSQL = string.Empty;
            var result = string.Empty;

            //strSQL = "SELECT intMstDayType FROM tblMstWorkShiftDetails WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            strSQL = "SELECT FORMAT(CONVERT(datetime,  '" + otDate.ToShortDateString() + "' + ' ' + CONVERT(varchar, " + fieldName + ", 108)), 'hh:mm tt') AS OTTIme ";
            strSQL += "FROM tblMstWorkShiftDetails ";
            strSQL += "WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";

            return UtilitiesDAL.GetSingleData(strSQL);
        }

        public DateTime GetActualShift(int intMstWorkShift, DateTime otDate, string typeOfShift = "end")
        {
            var _dayName = otDate.DayOfWeek.ToString();
            var strSQL = string.Empty;
            DateTime _result = otDate;
            string _res = string.Empty;
            string _type = string.Empty;

            if (typeOfShift == "end")
                _type = "[TimeOut]";
            else
                _type = "TimeIn";

            strSQL = "SELECT " + _type + " FROM tblMstWorkShiftDetails WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            _res = _result.ToShortDateString() + " " + UtilitiesDAL.GetSingleData(strSQL);
            _result = Convert.ToDateTime(_res);

            return _result;
        }

        public int GetDayType(int intMstWorkShift, DateTime otDate)
        {
            var _dayName = otDate.DayOfWeek.ToString();
            var strSQL = string.Empty;
            var result = string.Empty;

            strSQL = "SELECT intMstDayType FROM tblMstWorkShiftDetails WHERE (intMstWorkShift = " + intMstWorkShift + ") AND (DayName = '" + _dayName + "')";
            return Convert.ToInt32(UtilitiesDAL.GetSingleData(strSQL));//if 1 this means regular day, if 2 this means rest day
        }

        public List<OvertimeFinanceApproval> GetOvertimeFinanceManager(int intMstCompany, string codeBranchCode, string empid)
        {
            return _otDAL.GetOvertimeFinanceManager(intMstCompany, codeBranchCode, empid);
        }

        public OvertimeApprovalInfo GetApprovalInfo(int intOlnId, string intMstEmpPersonal)
        {
            OvertimeApprovalInfo otAppInfo = new OvertimeApprovalInfo()
            {
                ot = GetOvertime(intOlnId),
                EmpApprovalInfo = UtilitiesDAL.GetApprovalInfo(intOlnId, intMstEmpPersonal, "Overtime")
            };

            return otAppInfo;
        }
        public string getPosition(string intMstEmpPersonal)
        {
            return _otDAL.getPosition(intMstEmpPersonal);
        }

        public string InsertOvertimeMeals(OvertimeMeals meals, string intMstEmpPersonal)
        {
            return _otDAL.InsertOvertimeMeals(meals, intMstEmpPersonal);
        }

        public List<OvertimeMealsList> ListOfOvertimeMeals(string intMstEmpPersonal)
        {
            return _otDAL.ListOfOvertimeMeals(intMstEmpPersonal);
        }

        public List<OvertimeMealsDetails> ViewOvertimeMealsDetails(int intIDHead)
        {
            return _otDAL.ViewOvertimeMealsDetails(intIDHead);
        }
        public string CancelPunch(string ID)
        {
            return _otDAL.CancelPunch(ID);
        }
        public List<OvertimeMealsForApproval> OvertimeMealsForApproval(string intMstEmpPersonal)
        {
            return _otDAL.OvertimeMealsForApproval(intMstEmpPersonal);
        }

        public string ApprovedOvertimeMeals(string Details, string EmployeeId)
        {
            return _otDAL.ApprovedOvertimeMeals(Details, EmployeeId);
        }
        public string DisapproveOvertimeMeals(string Details, string Reason, string Id)
        {

            return _otDAL.DisapproveOvertimeMeals(Details, Reason, Id);
        }
    }
}
