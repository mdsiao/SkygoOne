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
    public class CoopLoanController : Controller
    {
        
        [CheckSessionOut]
        public ActionResult Index()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            CoopLoanMain coopMain = new CoopLoanMain() 
            { 
                CoopUnpaidList = CoopLoanBAL.GetCoopLoanList(empid),
                CoopFullyPaidList = CoopLoanBAL.GetCoopFullyPaidLoanList(empid)
            };
            //var coop = CoopLoanBAL.GetCoopLoanList(empid);

            ViewBag.MyTitle = "List of My Loans";
            return View(coopMain);
        }

        [CheckSessionOut]
        public ActionResult Apply(bool isGov = false)
        {
            var coop = new CoopLoan();
            coop.DateFiled = DateTime.Now;
            coop.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            coop.intOlnCoopLoanApplication = 0;

            ViewBag.loantypes = new SelectList((isGov == false) ? UtilitiesBAL.GetLoanType() : UtilitiesBAL.GetLoanType(0), "ValueMember", "DisplayMember");
            ViewBag.membershiptypes = new SelectList((isGov == false) ? UtilitiesBAL.GetMembershipType() : UtilitiesBAL.GetMembershipType(0), "ValueMember", "DisplayMember");
            //ViewBag.comaker1 = new SelectList(EmployeeBAL.GetEmployeeComboList(), "ValueMember", "DisplayMember");
            //ViewBag.comaker2 = new SelectList(EmployeeBAL.GetEmployeeComboList(), "ValueMember", "DisplayMember");

            ViewBag.isGovLoan = isGov;
            ViewBag.isForApproval = false;
            if (isGov)
            {
                ViewBag.MyTitle = "Apply Government Loan";
                ViewBag.MyTypeOfMembership = "Government Loan Type";
            }
            else
            {
                ViewBag.MyTitle = "Apply Coop Loan";
                ViewBag.MyTypeOfMembership = "Type Of Membership";
            }
            
            
            return View(coop);
        }        

        [CheckSessionOut]
        public ActionResult Edit(int id)
        {
            var coop = CoopLoanBAL.GetCoopLoan(id);

            //ViewBag.loantypes = new SelectList(UtilitiesBAL.GetLoanType(), "ValueMember", "DisplayMember", coop.intOlnLoanType);
            //ViewBag.membershiptypes = new SelectList(UtilitiesBAL.GetMembershipType(), "ValueMember", "DisplayMember", coop.TypeOfMembership);
            ViewBag.loantypes = new SelectList((coop.isGovernment == false) ? UtilitiesBAL.GetLoanType() : UtilitiesBAL.GetLoanType(0), "ValueMember", "DisplayMember");
            ViewBag.membershiptypes = new SelectList((coop.isGovernment == false) ? UtilitiesBAL.GetMembershipType() : UtilitiesBAL.GetMembershipType(0), "ValueMember", "DisplayMember");
            //ViewBag.cmkr1 = new SelectList(EmployeeBAL.GetEmployeeComboList(), "ValueMember", "DisplayMember", coop.CoMaker1);
            //ViewBag.cmkr2 = new SelectList(EmployeeBAL.GetEmployeeComboList(), "ValueMember", "DisplayMember", coop.CoMaker2);
            if (coop.isGovernment){
                ViewBag.MyTitle = "Update Government Loan";
                ViewBag.MyTypeOfMembership = "Government Loan Type";
            }
            else {
                ViewBag.MyTitle = "Update Coop Loan";
                ViewBag.MyTypeOfMembership = "Type Of Membership";
            }
            ViewBag.isForApproval = false;
            ViewBag.isGovLoan = coop.isGovernment;
            return View("Apply", coop);
        }

        [CheckSessionOut]
        public ActionResult Details(int id, bool isForApproval = false)
        {
            var coop = CoopLoanBAL.GetCoopLoan(id);

            //ViewBag.loantypes = new SelectList(UtilitiesBAL.GetLoanType(), "ValueMember", "DisplayMember", coop.intOlnLoanType);
            //ViewBag.membershiptypes = new SelectList(UtilitiesBAL.GetMembershipType(), "ValueMember", "DisplayMember", coop.TypeOfMembership);
            ViewBag.loantypes = new SelectList((coop.isGovernment == false) ? UtilitiesBAL.GetLoanType() : UtilitiesBAL.GetLoanType(0), "ValueMember", "DisplayMember");
            ViewBag.membershiptypes = new SelectList((coop.isGovernment == false) ? UtilitiesBAL.GetMembershipType() : UtilitiesBAL.GetMembershipType(0), "ValueMember", "DisplayMember");
            //ViewBag.comaker1 = new SelectList(EmployeeBAL.GetEmployeeComboList(), "ValueMember", "DisplayMember", coop.CoMaker1);
            //ViewBag.comaker2 = new SelectList(EmployeeBAL.GetEmployeeComboList(), "ValueMember", "DisplayMember", coop.CoMaker2);
            if (coop.isGovernment){
                ViewBag.MyTitle = "Government Loan Details";
                ViewBag.MyTypeOfMembership = "Government Loan Type";
            }
            else {
                ViewBag.MyTitle = "Coop Loan Details";
                ViewBag.MyTypeOfMembership = "Type Of Membership";
            }            
            ViewBag.isForApproval = isForApproval;
            ViewBag.isGovLoan = coop.isGovernment;
            return View("Apply", coop);
        }

        [CheckSessionOut]
        public ActionResult WholeRegion()
        {
            //string empid = Session["intMstEmpPersonal"].ToString();
            //int compid = (int)Session["emp_company"];
            //string branchcode = Session["emp_branchcode"].ToString();
            //int position = (int)Session["emp_position"];

            //var leaves = CoopLoanBAL.GetHRRegionalList(empid, compid, branchcode, position);

            ViewBag.MyTitle = "Whole Region";
            return View();
        }

        [CheckSessionOut]
        public ActionResult forApproval()
        {
            int intMstCompany = (int)Session["emp_company"];
            string codeMstBranch = Session["emp_branchcode"].ToString();
            int intMstPositionSupervisor = (int)Session["emp_position"];

            var loans = CoopLoanBAL.GetCoopLoanApproval(intMstCompany, codeMstBranch, intMstPositionSupervisor);

            ViewBag.MyTitle = "For Approval";
            return View(loans.ToList());
        }

        [CheckSessionOut]
        public ActionResult ViewDetails(int id)
        {
            var coop = CoopLoanBAL.GetCoopLoanMonitoring(id);

            ViewBag.MyTitle = "Loan Payments";
            return View(coop);
        }

        [HttpGet]
        public ActionResult GetWholeRegion(string option, string param)
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            int compid = (int)Session["emp_company"];
            string branchcode = Session["emp_branchcode"].ToString();
            int position = (int)Session["emp_position"];

            var leaves = CoopLoanBAL.GetHRRegionalList(empid, compid, branchcode, position, option, param);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(leaves);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ComputeTotalAmount(double NoOfMonths, double AmountApplied, int intOlnLoanType)
        {
            double totalAmount, amountToBePaid, interestAmount, interestPercent;

            interestPercent = CoopLoanBAL.GetInterestPercentage(intOlnLoanType);

            interestAmount = AmountApplied * (interestPercent / Convert.ToDouble(100));
            totalAmount = AmountApplied + interestAmount;
            amountToBePaid = totalAmount / NoOfMonths;

            return Json(new { interestAmount = interestAmount.ToString(), 
                            totalAmount = totalAmount.ToString(), 
                            amountToBePaid = amountToBePaid.ToString() }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetInterestPercentage(int intOlnLoanType)
        {
            double value = CoopLoanBAL.GetInterestPercentage(intOlnLoanType);

            return Json(new { interestPercentage = value.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveCoopLoan(CoopLoan coop)
        {

            string message = string.Empty;
            bool success = false;

            try
            {
                message = CoopLoanBAL.SaveCoopLoan(coop);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = message });
        
        }

        [HttpPost, CheckSessionOut]
        public ActionResult CancelCoopLoan(int intOlnCoopLoanApplication)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                message = CoopLoanBAL.CancelCoopLoan(intOlnCoopLoanApplication);
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
        public ActionResult ApproveLoan(ICollection<CoopLoanApproval> details)
        {
            string strMessage = string.Empty;
            bool success = false;
            bool isHR = (bool)Session["emp_isHRRegional"];

            try
            {
                strMessage = CoopLoanBAL.ApproveCoopLoan((int)Session["emp_position"], Session["intMstEmpPersonal"].ToString(), details, isHR);
                success = true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = strMessage });
        }
    }
}
