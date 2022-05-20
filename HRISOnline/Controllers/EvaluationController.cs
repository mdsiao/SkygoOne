using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Business;
using HRISOnline.Objects;
using HRISOnline.Models;

namespace HRISOnline.Controllers
{
    public class EvaluationController : Controller
    {

        [CheckSessionOut]
        public ActionResult Evaluate(string id)
        {
            EvaluationData eval = EvaluationBAL.GetEvaluation(id);
            eval.Eval.intMstEmpPersonal = eval.EmpPersonal.intMstEmpPersonal;
            eval.Eval.EvaluationDate = DateTime.Now;            

            string[] arr = new string[15] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o" };

            ViewBag.positions = new SelectList(UtilitiesBAL.GetPositionList(), "ValueMember", "DisplayMember", eval.EmpDTR.intMstPosition);
            ViewBag.depts = new SelectList(UtilitiesBAL.GetDepartmentList(), "ValueMember", "DisplayMember", eval.EmpDTR.intMstDepartment);
            ViewBag.branches = new SelectList(UtilitiesBAL.GetBranchList(), "ValueMember", "DisplayMember", eval.EmpDTR.codeMstBranch);

            ViewBag.MyTitle = "Evaluation Form";
            ViewBag.arr = arr;
            return View(eval);
        }

        [CheckSessionOut]
        public ActionResult Employees()
        {
            string branchCode = Session["emp_branchcode"].ToString();
            int compId = (int)Session["emp_company"];
            int positionId = (int)Session["emp_position"];            

            List<EvalEmployee> emps = EvaluationBAL.GetEvaluationEmployees(branchCode, compId, positionId);            
            ViewBag.MyTitle = "Employees";
            return View(emps.ToList());
        }

        [CheckSessionOut]
        public ActionResult List()
        {
            string evalBy = Session["intMstEmpPersonal"].ToString();
            var evalList = EvaluationBAL.GetEvaluationList(evalBy);

            ViewBag.MyTitle = "List of Evaluations";
            return View(evalList.ToList());
        }

        [HttpGet]
        public ActionResult GetTransferData(string strValue)
        {
            var cboData = EvaluationBAL.GetTransferData(strValue);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cboData);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveEvaluation(Evaluation eval)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                eval.EvaluateBy = Session["intMstEmpPersonal"].ToString();
                message = EvaluationBAL.SaveEvaluation(eval);
                if (message.Contains("successfully")) { success = true; }
            }
            catch (Exception ex)
            {

                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }


    }
}
