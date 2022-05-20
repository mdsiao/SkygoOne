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
    public class GatePassController : Controller
    {

        OvertimeBAL _otBAL = new OvertimeBAL();
        GatePassBAL _gpBAL = new GatePassBAL();

        [CheckSessionOut]
        public ActionResult Index(int id)
        {
            string type;
            string empid = Session["intMstEmpPersonal"].ToString();
            List<GatePassList> list = _gpBAL.GetGatePassList(empid, id);

            if (id == 2){
                type = "Undertime";
            }
            else {
                type = "Official Business";
            }

            ViewBag.MyTitle = "List of My " + type;
            ViewBag.id = id;
            ViewBag.type = type;
            return View(list.ToList());
        }

        [CheckSessionOut]
        public ActionResult Apply(int id)
        {
            string type = string.Empty;
            string branchcode = Session["emp_branchcode"].ToString();
            int workshift = (int)Session["emp_workshift"];
            int company = (int)Session["emp_company"];

            GatePass data = new GatePass() 
            { 
                intOlnGatePass = 0,
                intMstEmpPersonal = Session["intMstEmpPersonal"].ToString(),
                GatePassDate = DateTime.Now,
                DateFiled = DateTime.Now,
                intOlnGatePassType = id
            };

            if (id == 1){
                type = "Official Business";
            }
            else{
                type = "Undertime";
            }

            ViewBag.holiday = new SelectList(UtilitiesBAL.GetHoliday(data.intMstEmpPersonal, branchcode, workshift, company), "ValueMember", "DisplayMember");
            ViewBag.gatepasstype = new SelectList(UtilitiesBAL.GetGatePassType(), "ValueMember", "DisplayMember", data.intOlnGatePassType);
            ViewBag.MyTitle = "Apply for " + type;
            ViewBag.isForApproval = false;
            ViewBag.id = id;
            ViewBag.isDisapproved = false;
            return View(data);
        }

        [CheckSessionOut]
        public ActionResult Edit(int id, int xtype)
        {            
            string type;
            var gp = _gpBAL.GetGatePass(id);

            if (xtype == 1){
                type = "Official Business";
            }
            else{
                type = "Undertime";
            }
            
            ViewBag.gatepasstype = new SelectList(UtilitiesBAL.GetGatePassType(), "ValueMember", "DisplayMember", gp.intOlnGatePassType);
            ViewBag.MyTitle = "Update " + type;
            ViewBag.isForApproval = false;
            ViewBag.id = xtype;
            ViewBag.isDisapproved = gp.isDisapproved;
            return View("Apply", gp);
        }

        [CheckSessionOut]
        public ActionResult Details(int id, int xtype, bool isForApproval = false)
        {
            var gp = _gpBAL.GetGatePass(id);
            string branchcode = Session["emp_branchcode"].ToString();
            int workshift = (int)Session["emp_workshift"];
            int company = (int)Session["emp_company"];
            string type;

            if (xtype == 1){
                type = "Official Business";
            }
            else{
                type = "Undertime";
            }

            ViewBag.holiday = new SelectList(UtilitiesBAL.GetHoliday(gp.intMstEmpPersonal, branchcode, workshift, company, gp.intOlnGatePass), "ValueMember", "DisplayMember", gp.intTrnHoliday);
            ViewBag.gatepasstype = new SelectList(UtilitiesBAL.GetGatePassType(), "ValueMember", "DisplayMember", gp.intOlnGatePassType);
            ViewBag.MyTitle = type + " Details";
            ViewBag.isForApproval = isForApproval;
            ViewBag.id = xtype;
            ViewBag.isDisapproved = gp.isDisapproved;
            return View("Apply", gp);
        }

        [CheckSessionOut]
        public ActionResult forApproval(int id)
        {
            int intMstCompany = (int)Session["emp_company"];
            string codeMstBranch = Session["emp_branchcode"].ToString();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            string type;

            var list = _gpBAL.GetGatePassApprovalList(intMstCompany, codeMstBranch, intMstEmpPersonal, id);

            if (id == 1){
                type = "Official Business";
            }
            else {
                type = "Undertime";
            }

            ViewBag.MyTitle = type + " for Approval";
            ViewBag.id = id;
            return View(list);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetGatePassHours(DateTime timestart, DateTime timeend)
        {
            string strResult = string.Empty;            

            try
            {

                strResult = _otBAL.ComputeHours(timestart, timeend).ToString();
                
                return Json(new { success = "true", hours = strResult, errmsg = "", errtype = "0" }, JsonRequestBehavior.AllowGet);                
            }
            catch (Exception ex)
            {
                return Json(new { success = "false", hours = 0, errmsg = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetShifts(DateTime gpDate)
        {
            var _startTime = _otBAL.GetStartOfShift((int)Session["emp_workshift"], gpDate);
            var _endTime = _otBAL.GetEndOfShift((int)Session["emp_workshift"], gpDate);

            return Json(new { startOfTime = _startTime, endOfTime = _endTime }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveGatePass(GatePass gp)
        {
            string branchCode = Session["emp_branchcode"].ToString();
            string message = string.Empty;
            bool success = false;
            try
            {
                message = _gpBAL.SaveGatePass(gp, branchCode);
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
        public ActionResult ApproveGatePass(ICollection<GatePassApprovalList> list)
        {
            string strMsg = string.Empty;
            bool success = false;

            try
            {
                strMsg = _gpBAL.ApproveGatePass(Session["intMstEmpPersonal"].ToString(), list);
                if (strMsg.Contains("approved"))
                    success = true;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = strMsg });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult CancelGatePass(int intOlnGatePass)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = _gpBAL.CancelGatePass(intOlnGatePass);
                success = true;
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpGet, CheckSessionOut]
        public ActionResult SetDates(DateTime gpDate, string TimeIn, string TimeOut)
        {
            DateTime dDateOut, dDateIn, dDataIn, dDataOut;
            string shiftStart, shiftEnd;
            string NoOfHrs = "0";
           
            shiftStart = _otBAL.GetStartOfShift((int)Session["emp_workshift"], gpDate);
            shiftEnd = _otBAL.GetEndOfShift((int)Session["emp_workshift"], gpDate);

            dDataIn = Convert.ToDateTime(gpDate.ToShortDateString() + " " + TimeIn);
            dDataOut = Convert.ToDateTime(gpDate.ToShortDateString() + " " + TimeOut);
            dDateIn = Convert.ToDateTime(gpDate.ToShortDateString() + " " + shiftStart);
            dDateOut = Convert.ToDateTime(gpDate.ToShortDateString() + " " + shiftEnd);
            NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

            if (dDataOut > dDateOut){
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftEnd.ToString(), datanum = 1, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
            else if (dDataOut < dDateIn) {
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftStart.ToString(), datanum = 1, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
            else if (dDataIn > dDateOut){
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftEnd.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
            else if (dDataIn < dDateIn) {
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftStart.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
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
                //1 = gate pass transaction
                strMessage = UtilitiesBAL.DisapproveTransaction(3, dis, Session["intMstEmpPersonal"].ToString());
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

        [CheckSessionOut]
        public ActionResult ApprovalInfo(int id, int xtype, bool isForApproval = false)
        {
            var empid = Session["intMstEmpPersonal"].ToString();
            var gpAppInfo = _gpBAL.GetApprovalInfo(id, empid);
            string type;

            if (xtype == 1)
            {
                type = "Official Business";
            }
            else
            {
                type = "Undertime";
            }

            ViewBag.PaySched = gpAppInfo.EmpApprovalInfo.First().PaySched;
            ViewBag.gatepasstype = new SelectList(UtilitiesBAL.GetGatePassType(), "ValueMember", "DisplayMember", gpAppInfo.gp.intOlnGatePassType);
            ViewBag.MyTitle = type + " Approval Info";
            ViewBag.isForApproval = isForApproval;
            ViewBag.id = xtype;
            ViewBag.isDisapproved = gpAppInfo.gp.isDisapproved;
            return View(gpAppInfo);
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

            var obList = _gpBAL.GetGatePassRegionalList(empid, compid, branchcode, position);

            if ((Convert.ToBoolean(Session["emp_isHRRegional"]) == true) && (branchcode == "HO") && (branchmst == "HO"))
            {
                title = "OB - Home Office";
            }
            else
            {
                title = "OB - Whole Region";
            }

            ViewBag.MyTitle = title;
            return View(obList);
        }
    }
}
