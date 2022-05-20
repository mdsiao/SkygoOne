using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using System.Security;
using HRISOnline.Models;

namespace HRISOnline.Controllers
{
    public class DTRAdjustmentController : Controller
    {
        OvertimeBAL _otBAL = new OvertimeBAL();
        DTRAdjustmentBAL _dtrBAL = new DTRAdjustmentBAL();

        [CheckSessionOut]
        public ActionResult Index()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            List<DTRAdjList> list = _dtrBAL.GetDTRAdjustmentList(empid);

            ViewBag.MyTitle = "List of My DTR Adjustments";
            return View(list.ToList());
        }

        [CheckSessionOut]
        public ActionResult Apply()
        {
            DTRAdjustment data = new DTRAdjustment();
            data.intOlnDTRAdjustment = 0;
            data.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            data.AdjustmentDate = DateTime.Now;
            data.DateFiled = DateTime.Now;

            ViewBag.MyTitle = "Apply for DTR Adjustment";
            ViewBag.isForApproval = false;
            return View(data);
        }

        [CheckSessionOut]
        public ActionResult Edit(int id)
        {
            var data = _dtrBAL.GetDTRAdjustment(id);

            ViewBag.MyTitle = "Update DTR Adjustment";
            ViewBag.isForApproval = false;
            return View("Apply", data);
        }

        [CheckSessionOut]
        public ActionResult Details(int id, bool isForApproval = false)
        {
            var data = _dtrBAL.GetDTRAdjustment(id);

            ViewBag.MyTitle = "DTR Adjustment Details";
            ViewBag.isForApproval = isForApproval;
            return View("Apply", data);
        }

        [CheckSessionOut, HttpPost]
        public ActionResult SaveDTRAdjustment(DTRAdjustment adj)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = _dtrBAL.SaveDTRAdjustment(adj);
                success = true;
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpGet]
        public ActionResult GetShifts(DateTime adjDate)
        {
            var _startTime = _otBAL.GetStartOfShift((int)Session["emp_workshift"], adjDate);
            var _endTime = _otBAL.GetEndOfShift((int)Session["emp_workshift"], adjDate);

            return Json(new { startOfTime = _startTime, endOfTime = _endTime }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetNoOfHours(DateTime timestart, DateTime timeend)
        {
            string strResult = string.Empty;
            DateTime _startOfShift = _otBAL.GetActualShift((int)Session["emp_workshift"], Convert.ToDateTime(timestart.ToShortDateString()), "start");

            try
            {

                strResult = _otBAL.ComputeHours(timestart, timeend).ToString();
                if (timestart < _startOfShift)
                {
                    return Json(new { success = "false", hours = "0", errmsg = "Time Started should be greater than employee End Of Shift.", errtype = "1" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Convert.ToDouble(strResult) < 0)
                        return Json(new { success = "false", hours = "0", errmsg = "Time ended should be greater than Time Started.", errtype = "2" }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { success = "true", hours = strResult, errmsg = "", errtype = "0" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = "false", hours = 0, errmsg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost, CheckSessionOut]
        public ActionResult CancelDTRAdjustment(int intOlnDTRAdjustment)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = _dtrBAL.CancelDTRAdjustment(intOlnDTRAdjustment);
                success = true;
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [CheckSessionOut]
        public ActionResult forApproval()
        {
            int intMstCompany = (int)Session["emp_company"];
            string codeMstBranch = Session["emp_branchcode"].ToString();
            int intMstPositionSupervisor = (int)Session["emp_position"];

            DTRAdjustmentApprovalListMain dtrApp = new DTRAdjustmentApprovalListMain();
            dtrApp.DTRAdjAppList = _dtrBAL.GetDTRAdjustmentApprovalList(intMstCompany, codeMstBranch, intMstPositionSupervisor);
            dtrApp.PayrollMonth = DateTime.Now.Month;
            dtrApp.PayrollYear = DateTime.Now.Year;

            ViewBag.paymonth = new SelectList(UtilitiesBAL.GetComboBoxMonth(), "ValueMember", "DisplayMember", dtrApp.PayrollMonth);
            ViewBag.payyear = new SelectList(UtilitiesBAL.GetComboBoxYear(), "ValueMember", "DisplayMember", dtrApp.PayrollYear);
            ViewBag.payperiod = new SelectList(new[] 
                                { 
                                    new{Id = 1, Name = "1st Period"},
                                    new{Id = 2, Name = "2nd Period"},
                                }, "Id","Name");

            ViewBag.MyTitle = "DTR Adjustment for Approval";
            return View(dtrApp);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult ApproveDTRAdjustment(DTRAdjustmentApprovalListMain dtr)
        {
            string strMsg = string.Empty;
            bool success = false;

            try
            {

                strMsg = _dtrBAL.ApproveDTRAdjustment(Session["intMstEmpPersonal"].ToString(), (bool)Session["emp_isHRHomeOffice"], dtr, (int)Session["emp_position"]);
                if (strMsg.Contains("approved"))
                    success = true;

            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = strMsg });
        }

        [HttpGet, CheckSessionOut]
        public ActionResult SetDates(DateTime adjDate, string TimeIn, string TimeOut)
        {
            DateTime dDateOut, dDateIn, dDataIn, dDataOut;
            string shiftStart, shiftEnd;
            string NoOfHrs = "0";

            shiftStart = _otBAL.GetStartOfShift((int)Session["emp_workshift"], adjDate);
            shiftEnd = _otBAL.GetEndOfShift((int)Session["emp_workshift"], adjDate);

            dDataIn = Convert.ToDateTime(adjDate.ToShortDateString() + " " + TimeIn);
            dDataOut = Convert.ToDateTime(adjDate.ToShortDateString() + " " + TimeOut);
            dDateIn = Convert.ToDateTime(adjDate.ToShortDateString() + " " + shiftStart);
            dDateOut = Convert.ToDateTime(adjDate.ToShortDateString() + " " + shiftEnd);
            //NoOfHrs = _otBAL.GetComputeNoOfHours(dDataIn, dDataOut).ToString();

            //if (dDataOut > dDateOut)
            //{
            //    NoOfHrs = _otBAL.GetComputeNoOfHours(dDataIn, dDataOut).ToString();

            //    return Json(new { shift = shiftEnd.ToString(), datanum = 1, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //else if (dDataOut < dDateIn)
            //{
            //    NoOfHrs = _otBAL.GetComputeNoOfHours(dDataIn, dDataOut).ToString();

            //    return Json(new { shift = shiftStart.ToString(), datanum = 1, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //else if (dDataIn > dDateOut)
            //{
            //    NoOfHrs = _otBAL.GetComputeNoOfHours(dDataIn, dDataOut).ToString();

            //    return Json(new { shift = shiftEnd.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //else if (dDataIn < dDateIn)
            //{
            //    NoOfHrs = _otBAL.GetComputeNoOfHours(dDataIn, dDataOut).ToString();

            //    return Json(new { shift = shiftStart.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //}            

            return Json(new { shift = "", datanum = 0, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost, CheckSessionOut]
        public ActionResult Disapprove(ICollection<DisapproveTrans> dis)
        {
            string strMessage = string.Empty;
            bool success = false;

            try
            {
                //2 = LEAVE transaction
                strMessage = UtilitiesBAL.DisapproveTransaction(4, dis, Session["intMstEmpPersonal"].ToString());
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
    }
}
