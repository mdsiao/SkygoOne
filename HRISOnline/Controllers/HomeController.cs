using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Business;
using HRISOnline.Objects;
using HRISOnline.Models;
using LoginVerification;

namespace HRISOnline.Controllers
{    
    public class HomeController : Controller
    {
        LoginVerify _LoginVerify = new LoginVerify();
        EmployeeBAL _empBAL = new EmployeeBAL();

        public ActionResult Index()
        {
            var isLoggedIn = (this.HttpContext.Session["isLoggedIn"] == null) ? false : this.HttpContext.Session["isLoggedIn"];

            if (Convert.ToBoolean(isLoggedIn) == false)
            {
                return RedirectToAction("Login", "User");
            }
            return View("Index", "Employee");

            //string sysLinkONEAccess = _LoginVerify.GetSysLinkONEAccess();
            //return Redirect(sysLinkONEAccess);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet, CheckSessionOut]        
        public ActionResult GetItemCount()
        {
            if (Session["intMstEmpPersonal"] != null)
            {
                int intMstCompany = (int)Session["emp_company"];
                string codeMstBranch = Session["emp_branchcode"].ToString();
                string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
                int intMstPositionSupervisor = (int)Session["emp_position"];

                var cancelledList = _empBAL.GetCancelledTransactions(intMstEmpPersonal);
                var item = new ItemCount();
                item = UtilitiesBAL.GetItemCount(intMstCompany, codeMstBranch, intMstEmpPersonal);

                return Json(new
                {
                       otCount = item.OvertimeCount,
                    leaveCount = item.LeaveCount,
                    coopCount = item.CoopLoanCount,
                    bdayCount = item.BDayCount,
                    gpCount = item.GatePassCount,
                    adjCount = item.DTRAdjCount,
                    obCount = item.OBCount,
                    pbCount = item.PBCount,
                    fmCount = item.FinanceOTCount,
                    cancelledCount = cancelledList.Count,
                    EmpCount = item.EmpUpdateCount,
                    MPCount = item.MissingPunchCount,
                    OTMealsCount = item.OTMealsCount
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                otCount = "0", 
                leaveCount = "0", 
                coopCount = "0",
                bdayCount = "0",
                gpCount = "0",
                adjCount = "0",
                EmpCount= "0",
                MPCount = "0",
                OTMealsCount = "0",
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
