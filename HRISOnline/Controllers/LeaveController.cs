using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using HRISOnline.Models;
using System.Security;

namespace HRISOnline.Controllers
{
    public class LeaveController : Controller
    {

        LeaveBAL _leaveBAL = new LeaveBAL();

        [CheckSessionOut]
        public ActionResult Index()
        {

            string empid = Session["intMstEmpPersonal"].ToString();
            int year = DateTime.Now.Year;
            var emp_personal = (EmpPersonal)Session["Personal"];

            LeaveListData data = new LeaveListData
            {
                LeaveList = _leaveBAL.GetLeaveList(empid),
                LeaveBalance = _leaveBAL.GetLeaveBalance(empid, year),
                Gender = emp_personal.Gender,
                CivilStatus = emp_personal.CivilStatus
            };                        
            
            ViewBag.MyTitle = "List of Leaves";
            return View(data);
        }

        [CheckSessionOut]
        public ActionResult Apply()
        {
            var leave = _leaveBAL.GetLeave(0);
            leave.DateFiled = DateTime.Now;
            leave.DateFrom = DateTime.Now;
            leave.DateTo = DateTime.Now;
            leave.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            leave.intOlnLeaveApplicationHead = 0;
            leave.intMstLeaveType = 0;//this is vacation leave
            leave.YearPeriod = DateTime.Now.Year;

            ViewBag.leavetypes = new SelectList(UtilitiesBAL.GetLeaveType(), "ValueMember", "DisplayMember", leave.intMstLeaveType);
            ViewBag.dtFromAMorPM = new SelectList(UtilitiesBAL.GetAMorPMType("", 0), "ValueMember", "DisplayMember");
            ViewBag.dtToAMorPM = new SelectList(UtilitiesBAL.GetAMorPMType("", 0), "ValueMember", "DisplayMember");
            ViewBag.MyTitle = "Apply for Leave";
            ViewBag.isForApproval = false;
            ViewBag.isDisapproved = false;
            return View(leave);
        }

        public ActionResult LeaveEntry()
        {
            return PartialView();
        }

        [CheckSessionOut]
        public ActionResult Edit(int id)
        {
            var leave = _leaveBAL.GetLeave(id);

            ViewBag.leavetypes = new SelectList(UtilitiesBAL.GetLeaveType(), "ValueMember", "DisplayMember", leave.intMstLeaveType);
            ViewBag.MyTitle = "Update Leave";
            ViewBag.isForApproval = false;
            ViewBag.isDisapproved = leave.isDisapproved;
            return View("Apply", leave);
        }

        [CheckSessionOut]
        public ActionResult Details(int id, bool isForApproval = false)
        {
            var leave = _leaveBAL.GetLeave(id);

            ViewBag.leavetypes = new SelectList(UtilitiesBAL.GetLeaveType(), "ValueMember", "DisplayMember", leave.intMstLeaveType);
            ViewBag.dtFromAMorPM = new SelectList(UtilitiesBAL.GetAMorPMType("", 0), "ValueMember", "DisplayMember", leave.DateFromAMorPM);
            ViewBag.dtToAMorPM = new SelectList(UtilitiesBAL.GetAMorPMType("", 0), "ValueMember", "DisplayMember", leave.DateToAMorPM);
            ViewBag.MyTitle = "Leave Details";
            ViewBag.isForApproval = isForApproval;
            ViewBag.isDisapproved = leave.isDisapproved;
            return View("Apply", leave);
        }
        
        [CheckSessionOut]
        public ActionResult forApproval()
        {
            int intMstCompany = (int)Session["emp_company"];
            string codeMstBranch = Session["emp_branchcode"].ToString();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            var leaves = _leaveBAL.GetLeaveApprovalList(intMstCompany, codeMstBranch, intMstEmpPersonal);

            ViewBag.MyTitle = "Leaves for Approval";
            return View(leaves.ToList());
        }

        [CheckSessionOut]
        public ActionResult WholeRegion()
        {
            string title = "";
            string empid = Session["intMstEmpPersonal"].ToString();
            int compid = (int)Session["emp_company"];
            string branchcode = Session["emp_branchcode"].ToString();
            int position = (int)Session["emp_position"];
            string branchmst = Session["emp_mstbranch"].ToString();

            var leaves = _leaveBAL.GetHRRegionalList(empid, compid, branchcode, position);

            if ((Convert.ToBoolean(Session["emp_isHRRegional"]) == true) && (branchcode == "HO") && (branchmst == "HO")){
                title = "Leave - Home Office";
            }
            else {
                title = "Leave - Whole Region";
            }

            ViewBag.MyTitle = title;
            return View(leaves);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveLeave(LeaveMaster leave)
        {
            string message = string.Empty;
            bool success = false;

            message = _leaveBAL.SaveLeave(leave, Convert.ToBoolean(Session["emp_hasWorkOnSat"]));
            if (message.Contains("saved")) { success = true; }
            
            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult CancelLeave(int intOlnLeaveApplicationHead)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = _leaveBAL.CancelLeave(intOlnLeaveApplicationHead);
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
        public ActionResult CancelLeaveByBatch(ICollection<LeaveApproval> details)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = _leaveBAL.CancelLeave(details, Session["intMstEmpPersonal"].ToString());
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
        public ActionResult ApproveLeave(ICollection<LeaveApproval> details)
        {
            string strMessage = string.Empty;
            bool success = false;
            bool isHR = (bool)Session["emp_isHRRegional"];

            try
            {
                strMessage = _leaveBAL.ApproveLeave(Session["intMstEmpPersonal"].ToString(), details, isHR);
                success = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = strMessage });
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetAvailableLeave(int leaveType, DateTime lvDate)
        {
            double dblLeaveCount = _leaveBAL.GetAvailableLeave(Session["intMstEmpPersonal"].ToString(), leaveType, lvDate.Year);

            return Json(new { avLeave = dblLeaveCount.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult Disapprove(ICollection<DisapproveTrans> dis)
        {
            string strMessage = string.Empty;
            bool success = false;

            try
            {
                //2 = LEAVE transaction
                strMessage = UtilitiesBAL.DisapproveTransaction(2, dis, Session["intMstEmpPersonal"].ToString());
                if (strMessage.Contains("disapproved"))
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = strMessage });

        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetNoOfDays(DateTime dtFrom, DateTime dtTo)
        {            
            double result = _leaveBAL.GetNoOfDays(dtFrom, dtTo, Convert.ToBoolean(Session["emp_hasWorkOnSat"]));
            
            return Json(new { days = result.ToString() }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet, CheckSessionOut]
        public ActionResult GetAMorPMType(string dType, double noOfDays) {
            var list = UtilitiesBAL.GetAMorPMType(dType, noOfDays);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [CheckSessionOut]
        public ActionResult ApprovalInfo(int id, bool isForApproval = false)
        {
            var empid = Session["intMstEmpPersonal"].ToString();
            var lvAppInfo = _leaveBAL.GetApprovalInfo(id, empid);

            ViewBag.PaySched = lvAppInfo.EmpApprovalInfo.First().PaySched;
            ViewBag.leavetypes = new SelectList(UtilitiesBAL.GetLeaveType(), "ValueMember", "DisplayMember", lvAppInfo.lv.intMstLeaveType);
            ViewBag.dtFromAMorPM = new SelectList(UtilitiesBAL.GetAMorPMType("", 0), "ValueMember", "DisplayMember", lvAppInfo.lv.DateFromAMorPM);
            ViewBag.dtToAMorPM = new SelectList(UtilitiesBAL.GetAMorPMType("", 0), "ValueMember", "DisplayMember", lvAppInfo.lv.DateToAMorPM);
            ViewBag.MyTitle = "Leave Approval Info";
            ViewBag.isForApproval = isForApproval;
            ViewBag.isDisapproved = lvAppInfo.lv.isDisapproved;
            return View(lvAppInfo);
        }
    }
}
