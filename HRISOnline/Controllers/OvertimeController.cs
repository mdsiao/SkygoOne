using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using System.Security;
using HRISOnline.Models;
using HRISOnline.Data;
using System.Data;


namespace HRISOnline.Controllers
{
    public class OvertimeController : Controller
    {
       

        OvertimeBAL _otBAL = new OvertimeBAL();

        [CheckSessionOut]
        public ActionResult Index()
        {
            string empId = Session["intMstEmpPersonal"].ToString();
            var otList = _otBAL.GetOvertimeList(empId);

            ViewBag.MyTitle = "List of My Overtime";
            return View(otList.ToList());            
        }

        [CheckSessionOut]
        public ActionResult Apply()
        {
            Overtime ot = new Overtime();
            ot.DateFiled = DateTime.Now;
            ot.OvertimeDate = DateTime.Now;
            ot.DateTimeFrom = DateTime.Now;
            ot.DateTimeTo = DateTime.Now;
            ot.intOlnOvertimeApplication = 0;
            ot.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            ViewBag.MyTitle = "Apply for Overtime";
            ViewBag.isForApproval = false;
            ViewBag.isFinance = false;
            ViewBag.isDisapproved = false;
            return View(ot);
        }

        [CheckSessionOut]
        public ActionResult Edit(int id)
        {
            var ot = _otBAL.GetOvertime(id);

            ViewBag.MyTitle = "Update Overtime";
            ViewBag.isForApproval = false;
            ViewBag.isFinance = false;
            ViewBag.isDisapproved = ot.isDisapproved;
            return View("Apply", ot);
        }

        [CheckSessionOut]
        public ActionResult Details(int id, bool isForApproval = false, bool isFinance = false)
        {
            var ot = _otBAL.GetOvertime(id);

            ViewBag.MyTitle = "Overtime Details";
            ViewBag.isForApproval = isForApproval;
            ViewBag.isFinance = isFinance;
            ViewBag.isDisapproved = ot.isDisapproved;
            return View("Apply", ot);
        }

        [CheckSessionOut]
        public ActionResult forApproval()
        {
            int intMstCompany = (int)Session["emp_company"];
            string codeMstBranch = Session["emp_branchcode"].ToString();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            Session["isFinanceManagerModule"] = false;

            var ot_approval = _otBAL.GetOvertimeApprovalList(intMstCompany, codeMstBranch, intMstEmpPersonal);

            ViewBag.MyTitle = "Overtime for Approval";
            return View(ot_approval);

        }

        [CheckSessionOut]
        public ActionResult WholeRegion()
        {
            //string empid = Session["intMstEmpPersonal"].ToString();
            //int compid = (int)Session["emp_company"];
            //string branchcode = Session["emp_branchcode"].ToString();
            //int position = (int)Session["emp_position"];

            //var ot = OvertimeBAL.GetHRRegionalList(empid, compid, branchcode, position);

            ViewBag.MyTitle = "Whole Region";
            //return View(ot.ToList());
            return View();
        }

        [CheckSessionOut]
        public ActionResult FinanceApproval()
        {
            int intMstCompany = (int)Session["emp_company"];
            string codeMstBranch = Session["emp_branchcode"].ToString();
            Session["isFinanceManagerModule"] = true;
            string empid = Session["intMstEmpPersonal"].ToString();

            var ot_approval = _otBAL.GetOvertimeFinanceManager(intMstCompany, codeMstBranch, empid);

            ViewBag.MyTitle = "Finance for Approval";
            return View(ot_approval);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetWholeRegion(string option, string param)
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            int compid = (int)Session["emp_company"];
            string branchcode = Session["emp_branchcode"].ToString();
            int position = (int)Session["emp_position"];

            var ot = _otBAL.GetHRRegionalList(empid, compid, branchcode, position, option, param);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ot);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetOvertimeHours(DateTime timestart, DateTime timeend)
        {
            string strResult = string.Empty;
            DateTime _endOfShift = _otBAL.GetActualShift((int)Session["emp_workshift"], Convert.ToDateTime(timestart.ToShortDateString()));

            try
            {

                strResult = _otBAL.ComputeHours(timestart, timeend).ToString();
                                
                if (Convert.ToDouble(strResult) < 0)
                    return Json(new { success = "false", hours = "0", errmsg = "Time ended should be greater than Time Started.", errtype = "2" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = "true", hours = strResult, errmsg = "", errtype = "0" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = "false", hours = 0, errmsg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            
        }        

        [HttpPost, CheckSessionOut]
        public ActionResult SaveOvertime(Overtime ot)
        {

            string message = string.Empty;
            bool success = false;

            try
            {
                message = _otBAL.SaveOvertime(ot, (int)Session["emp_workshift"]);
                success = true;
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult CancelOvertime(int intOlnOvertimeApplication)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = _otBAL.CancelOvertime(intOlnOvertimeApplication);
                success = true;
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult ApproveOvertime(ICollection<OvertimeApproval> details)
        {
            string strMessage = string.Empty;
            bool success = false;
            bool isFinanceManager = (bool)Session["emp_isFinanceManager"];
            bool isFinanceManagerModule = Convert.ToBoolean(Session["isFinanceManagerModule"]);

            try
            {
                strMessage = _otBAL.ApproveOvertime(Session["intMstEmpPersonal"].ToString(), details, isFinanceManager, isFinanceManagerModule);
                success = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
            }

            Session["isFinanceManagerModule"] = null;
            return Json(new { success = success.ToString(), msg = strMessage });
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetEndOfShift(DateTime otDate)
        {
            var _endTime = _otBAL.GetEndOfShift((int)Session["emp_workshift"], otDate);

            return Json(new { endOfTime = _endTime.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetStartOfShift(DateTime otDate)
        {
            var _startTime = _otBAL.GetStartOfShift((int)Session["emp_workshift"], otDate);

            return Json(new { startOfTime = _startTime.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult SetDates(DateTime otDate, string startTime, string endTime)
        {
            DateTime dStartData, dEndData, dEndShift, dStartShift, dNoonData, dLunchInData, dLunchOutData;
            string shiftStart, shiftEnd, shiftLunchOut, shiftLunchIn;
            string OTHours;
            string OTHoursOrig;
            int _dayType = 0;

            _dayType = _otBAL.GetDayType((int)Session["emp_workshift"], otDate);
            shiftStart = _otBAL.GetStartOfShift((int)Session["emp_workshift"], otDate);
            shiftEnd = _otBAL.GetEndOfShift((int)Session["emp_workshift"], otDate);
            shiftLunchOut = _otBAL.GetLunchShift((int)Session["emp_workshift"], otDate, "LunchTimeOut");
            shiftLunchIn = _otBAL.GetLunchShift((int)Session["emp_workshift"], otDate, "LunchTimeIn");

            dStartData = Convert.ToDateTime(otDate.ToShortDateString() + " " + startTime);
            dEndData = Convert.ToDateTime(otDate.ToShortDateString() + " " + endTime);
            dEndShift = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftEnd);
            dStartShift = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftStart);
            dLunchInData = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftLunchIn);
            dLunchOutData = Convert.ToDateTime(otDate.ToShortDateString() + " " + shiftLunchOut);
            dNoonData = Convert.ToDateTime(otDate.ToShortDateString() + " " + "12:00 PM");
            
            OTHours = _otBAL.ComputeHours(dStartData, dEndData).ToString();
            OTHoursOrig = _otBAL.ComputeHoursOrig(dStartData, dEndData).ToString();

            if (_dayType != 2)
            {
                if ((dStartData > dStartShift) && (dStartData <= dNoonData))
                {
                    OTHours = _otBAL.ComputeHours(dStartShift, dEndData).ToString();
                    OTHoursOrig = _otBAL.ComputeHoursOrig(dStartShift, dEndData).ToString();

                    return Json(new { shiftStart = shiftStart.ToString(), datanum = 1, OTHours = OTHours.ToString(), OTHoursOrig = OTHoursOrig.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else if ((dStartData < dEndShift) && (dStartData > dNoonData))
                {
                    OTHours = _otBAL.ComputeHours(dEndShift, dEndData).ToString();
                    OTHoursOrig = _otBAL.ComputeHoursOrig(dEndShift, dEndData).ToString();

                    return Json(new { shiftStart = shiftEnd.ToString(), datanum = 1, OTHours = OTHours.ToString(), OTHoursOrig = OTHoursOrig.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else if ((dEndData > dStartShift) && (dEndData <= dNoonData))
                {
                    OTHours = _otBAL.ComputeHours(dStartData, dStartShift).ToString();
                    OTHoursOrig = _otBAL.ComputeHoursOrig(dStartData, dStartShift).ToString();

                    return Json(new { shiftEnd = shiftStart.ToString(), datanum = 2, OTHours = OTHours.ToString(), OTHoursOrig = OTHoursOrig.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else if ((dEndData < dEndShift) && (dEndData > dNoonData))
                {
                    OTHours = _otBAL.ComputeHours(dStartData, dEndShift).ToString();
                    OTHoursOrig = _otBAL.ComputeHoursOrig(dStartData, dEndShift).ToString();

                    return Json(new { shiftEnd = shiftEnd.ToString(), datanum = 2, OTHours = OTHours.ToString(), OTHoursOrig = OTHoursOrig.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                double RDHours = 0;
                double RDHoursOrig = 0;
                string[] splitStr;

                if (((dStartData < dLunchOutData) && (dEndData < dLunchOutData)) || ((dStartData > dLunchInData) && (dEndData > dLunchInData))
                    || (dStartData == dLunchInData)){
                    RDHours = _otBAL.ComputeHours(dStartData, dEndData);
                    RDHoursOrig = _otBAL.ComputeHoursOrig(dStartData, dEndData);
                }
                else{
                    RDHours = _otBAL.ComputeHours(dStartData, dLunchOutData);
                    RDHoursOrig = _otBAL.ComputeHoursOrig(dStartData, dLunchOutData);

                    if (dEndData > dLunchInData) {
                        RDHours += _otBAL.ComputeHours(dLunchInData, dEndData);
                        RDHoursOrig += _otBAL.ComputeHoursOrig(dLunchInData, dEndData);
                    }

                    if (RDHours.ToString().Contains(".")) {
                        splitStr = RDHours.ToString().Split('.');

                        switch (splitStr[1].ToString())
                        {
                            case "6":
                                RDHours = Convert.ToInt32(splitStr[0]) + 1;
                                break;
                        }
                    }                    
                }                
                
                OTHours = RDHours.ToString();
                OTHoursOrig = RDHoursOrig.ToString();
            }


            return Json(new { shiftStart = "", shiftEnd = "", datanum = 0, OTHours = OTHours.ToString(), OTHoursOrig = OTHoursOrig.ToString() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost, CheckSessionOut]
        public ActionResult Disapprove(ICollection<DisapproveTrans> dis)
        {
            string strMessage = string.Empty;
            bool success = false;

            try
            {
                //1 = overtime transaction
                strMessage = UtilitiesBAL.DisapproveTransaction(1, dis, Session["intMstEmpPersonal"].ToString());
                if (strMessage.Contains("disapproved")) {
                    success = true;
                }                
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = strMessage });

        }

        [CheckSessionOut]
        public ActionResult ApprovalInfo(int id, bool isForApproval = false, bool isFinance = false)
        {
            var empid = Session["intMstEmpPersonal"].ToString();            
            var otAppInfo = _otBAL.GetApprovalInfo(id, empid);

            ViewBag.PaySched = otAppInfo.EmpApprovalInfo.First().PaySched;
            ViewBag.MyTitle = "Overtime Approval Info";
            ViewBag.isForApproval = isForApproval;
            ViewBag.isDisapproved = otAppInfo.ot.isDisapproved;
            ViewBag.isFinance = isFinance;
            return View(otAppInfo);
        }

        public ActionResult ApplyForMeals()
        {
            ViewBag.MyTitle = "Apply for Overtime Meals";
            var intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            OvertimeDAL EmpNameDAL = new OvertimeDAL();


            DataSet ds = EmpNameDAL.BindEmployeeDDL(intMstEmpPersonal);
            ViewBag.EmpName = ds.Tables[0];
            List<SelectListItem> item1 = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in ViewBag.EmpName.Rows)
            {
                item1.Add(new SelectListItem { Text = @dr["EmployeeName"].ToString(), Value = @dr["intMstEmpPersonal"].ToString() });
            }
            ViewBag.EmpName = item1;

            return View();
        }

        public ActionResult ApplyOvertimeMeals()
        {
            ViewBag.MyTitle = "Apply for Overtime Meals";
            var intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            OvertimeDAL OTRequestDAL = new OvertimeDAL();

            DataSet ds = OTRequestDAL.BindOvertimeRequestServiceDDL(intMstEmpPersonal);
            ViewBag.OTRequest = ds.Tables[0];
            List<SelectListItem> item1 = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in ViewBag.OTRequest.Rows)
            {
                item1.Add(new SelectListItem { Text = @dr["RequestService"].ToString(), Value = @dr["ID"].ToString() });
            }
            ViewBag.OTRequest = item1;

            OvertimeMeals OTMeals = new OvertimeMeals()
            {
                DateFiled = DateTime.Now,
                DateApplied = DateTime.Now,
                intMstEmpPersonal  = Session["intMstEmpPersonal"].ToString()
            };

            return View(OTMeals);
        }


        public ActionResult GetPosition(string intMstEmpPersonal)
        {
            string strPosition;

            strPosition = _otBAL.getPosition(intMstEmpPersonal);

            return Json(new { responseText = strPosition }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult InsertOvertimeMeals(string DateFiled, string DateApplied,  int RequestServiceID, string RequestServiceType, string Reason)
        public ActionResult InsertOvertimeMeals(OvertimeMeals meals)    
        {
            var intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            //string result = _otBAL.InsertOvertimeMeals(DateFiled, intMstEmpPersonal, DateApplied,RequestServiceID, RequestServiceType, Reason);

            //if (result != "Success")
            //{
            //    return Json(new { success = false, responseText = "Failed to apply for Overtime Meals!" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { success = true, responseText = "You have successfully applied for Overtime Meals!", msg = result }, JsonRequestBehavior.AllowGet);
            //}

            string message = string.Empty;
            bool success = false;

            try
            {

                message = _otBAL.InsertOvertimeMeals(meals, intMstEmpPersonal);
                 success = true;
                
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        public ActionResult ListOfOvertimeMeals()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            List<OvertimeMealsList> list = _otBAL.ListOfOvertimeMeals(empid);

            ViewBag.MyTitle = "List of Applied Overtime Meals";


            return View(list.ToList());
        }

        public ActionResult ViewOvertimeMealsDetails(int intIDHead)
        {
            string empid = Session["intMstEmpPersonal"].ToString();

            List<OvertimeMealsDetails> list = _otBAL.ViewOvertimeMealsDetails(intIDHead);

            ViewBag.MyTitle = "List who Applied Overtime Meals";

            return View(list.ToList());
        }

        public ActionResult CancelPunch(string intITHead)
        {
           string result = _otBAL.CancelPunch(intITHead);

            if (result != "Cancel")
            {
                return Json(new { success = false, responseText = "Failed to Cancel!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Cancelled!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OvertimeMealsForApproval()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            List<OvertimeMealsForApproval> list = _otBAL.OvertimeMealsForApproval(empid);


            ViewBag.MyTitle = "List of Overtime Meals Approval";
            return View(list.ToList());
        }

        public ActionResult ViewOvertimeMealsDetailsForApproval(int intIDHead)
        {
            string empid = Session["intMstEmpPersonal"].ToString();

            List<OvertimeMealsDetails> list = _otBAL.ViewOvertimeMealsDetails(intIDHead);

            ViewBag.MyTitle = "List who Applied Overtime Meals";

            return View(list.ToList());
        }

        public ActionResult ApprovedOvertimeMeals(string Details)
        {
    
            string Id = Session["intMstEmpPersonal"].ToString();


            string Result = _otBAL.ApprovedOvertimeMeals(Details, Id);

            if (Result != "Success")
            {
                return Json(new { success = false, responseText = "Failed to Approved!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Approved!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DisapproveOvertimeMeals(string Details, string Reason)
        {
         

            string Id = Session["intMstEmpPersonal"].ToString();

            string Result = _otBAL.DisapproveOvertimeMeals(Details, Reason, Id);

            if (Result != "disapprove")
            {
                return Json(new { success = false, responseText = "Failed to Disapproved Overtime Meals!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Disapproved Overtime Meals!" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
