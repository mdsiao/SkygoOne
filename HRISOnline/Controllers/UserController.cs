using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using System.Security;
using HRISOnline.Models;
using LoginVerification;

namespace HRISOnline.Controllers
{
    public class UserController : Controller
    {
        LoginVerify _LoginVerify = new LoginVerify();
        EmployeeBAL _empBAL = new EmployeeBAL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [CheckSessionOut]
        public ActionResult ChangePassword()
        {
            ViewBag.MyTitle = "Change Password";
            return View();
            //Session["intMstEmpPersonal"] = null;
            //Session["Username"] = null;
            //Session["UserPassword"] = null;
            //Session["Status"] = null;
            //Session["intOlnUsers"] = null;
            //Session["isLoggedIn"] = null;

            //Session["Personal"] = null;
            //Session["DTR"] = null;
            //Session["emp_name"] = null;
            //Session["emp_company"] = null;
            //Session["emp_branchcode"] = null;
            //Session["emp_approvallevel"] = null;
            //Session["emp_position"] = null;
            //Session["emp_isHRRegional"] = null;
            //Session["emp_workshift"] = null;
            //Session.RemoveAll();

            //return Redirect("http://localhost:41473/Account/ChangePassword?isFromONE=true");

        }

        [HttpPost]
        public ActionResult Login(sysUser myUser)
            {
                //========== COMMENT THIS WHEN YOU'RE GOING TO DEPLOY TO LIVE ==========//
                string strMsg = string.Empty;
                EmpPersonal emp = new EmpPersonal();
                EmpDTR dtr = new EmpDTR();
                ApproversName apr = new ApproversName();
                List<ObjUserDetails> usrdet = new List<ObjUserDetails>();

                try
                {
                    var user = UserBAL.AuthenticateUser(myUser.Username);
                    if (user.Username != "")
                    {
                        Session["intMstEmpPersonal"] = user.intMstEmpPersonal;
                        Session["Username"] = user.Username;
                        //Session["UserPassword"] = user.UserPassword;                                     
                        Session["UserPassword"] = _LoginVerify.GetUserPass(user.Username);
                        Session["Status"] = user.Status;
                        Session["intOlnUsers"] = user.intOlnUsers;
                        Session["isLoggedIn"] = true;

                        emp = _empBAL.GetEmployeePersonal(Session["intMstEmpPersonal"].ToString());
                        dtr = _empBAL.GetEmployeeDTR(Session["intMstEmpPersonal"].ToString());
                        usrdet = _empBAL.GetUserDetails(user.Username);
                        apr = _empBAL.GetEmployeeApproverName(Session["intMstEmpPersonal"].ToString());

                        ////objects into session
                        Session["Personal"] = emp;
                        Session["DTR"] = dtr;

                        Session["emp_firstApprover"] = apr.FirstApprover == null ? "" : apr.FirstApprover;
                        Session["emp_secondApprover"] = apr.SecondApprover == null ? "" : apr.SecondApprover;

                        Session["emp_name"] = emp.FirstName + " " + (emp.MiddleName.Length == 0 ? "" : emp.MiddleName.Substring(0, 1)) + ". " + emp.LastName;
                        Session["emp_company"] = emp.intMstCompany;
                        Session["emp_branchcode"] = dtr.codeMstBranchHome;
                        Session["emp_mstbranch"] = dtr.codeMstBranch;
                        Session["emp_approvallevel"] = _empBAL.GetEmployeeApprovalLevel(dtr.intMstPosition);
                        Session["emp_position"] = dtr.intMstPosition;
                        Session["emp_workshift"] = dtr.intMstWorkShift;
                        Session["one_userdet"] = usrdet;
                        Session["emp_hasWorkOnSat"] = _empBAL.hasWorkOnSaturday(dtr.intMstWorkShift);

                        Session["emp_isAllowedOvertime"] = UtilitiesBAL.isAllowedOvertime(dtr.intMstPosition);
                        Session["emp_isHRRegional"] = UtilitiesBAL.isHRRegional(dtr.intMstPosition);
                        Session["emp_isHRHomeOffice"] = UtilitiesBAL.isHRHomeOffice(dtr.intMstPosition);
                        Session["emp_isFinanceManager"] = UtilitiesBAL.isFinanceManager(dtr.intMstPosition);
                        Session["emp_canApproveTransactions"] = UtilitiesBAL.canApproveTransactions(dtr.intMstEmpPersonal);
                        Session["emp_canApplyOBHoliday"] = UtilitiesBAL.canApplyForOBHoliday(dtr.intMstPosition);

                        Employee201BAL Emp201 = new Employee201BAL();
                        System.Data.DataSet dds = Emp201.CheckLogin(user.intMstEmpPersonal);

                        if (dds.Tables[0].Rows.Count > 0)
                        {
                            Session["HasEmpUpdate"] = true;
                        }
                        else
                        {
                            Session["HasEmpUpdate"] = false;
                        }

                        //GroupInsuranceRights
                        System.Data.DataSet dds1 = Emp201.CheckGroupInsuranceRights(user.intMstEmpPersonal);

                        if (dds1.Tables[0].Rows.Count > 0)
                        {
                            Session["HasGroupInsurance"] = true;
                        }
                        else
                        {
                            Session["HasGroupInsurance"] = false;
                        }

                        //End GroupInsurance

                        ////TASK MONITORING
                        //System.Data.DataSet dds2 = Emp201.CheckTaskMonitoringRights(user.intMstEmpPersonal);

                        //if (dds2.Tables[0].Rows.Count > 0)
                        //{
                        //    Session["HasTaskMonitoring"] = true;
                        //}
                        //else
                        //{
                        //    Session["HasTaskMonitoring"] = false;
                        //}
                        ////END OF TASK MONITORING

                        //Missing Punches
                        System.Data.DataSet dds3 = Emp201.MissingPunchApprover(user.intMstEmpPersonal);
                        if (dds3.Tables[0].Rows.Count > 0)
                        {
                            Session["HasMissingPunchApprover"] = true;
                        }
                        else
                        {
                            Session["HasMissingPunchApprover"] = false;
                        }

                        //End of Missing Punches

                        ////OvertimeMeals
                        //System.Data.DataSet dds4 = Emp201.CheckOvertimeMealsForHO(user.intMstEmpPersonal);
                        //if (dds4.Tables[0].Rows.Count > 0)
                        //{
                        //    Session["HasOvertimeMealsHO"] = true;
                        //}
                        //else
                        //{
                        //    Session["HasOvertimeMealsHO"] = false;
                        //}
                        ////End of Overtime Meals

                        //End of Overtime Meals



                        Employee201BAL EmpApprove201 = new Employee201BAL();
                        System.Data.DataSet ddds = EmpApprove201.EmpApprover201(user.intMstEmpPersonal);

                        if (ddds.Tables[0].Rows.Count > 0)
                        {
                            Session["emp_canApproveEmployee201"] = true;
                        }
                        else
                        {
                            Session["emp_canApproveEmployee201"] = false;
                        }



                        ContactsBAL CB = new ContactsBAL();
                        System.Data.DataSet ds = CB.Login(user.intMstEmpPersonal);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["IsAdmin"] = true;
                        }
                        else
                        {
                            Session["IsAdmin"] = false;
                        }


                        Session["UserPasswordDecrypted"] = "12345";


                        return RedirectToAction("Notification", "Employee");
                    }
                    else
                    {
                        return View(myUser);
                    }
                }
                catch (Exception ex)
                {
                    strMsg = ex.Message.ToString();
                }

                ViewBag.ErrorMsg = strMsg;
                return View(myUser);
                //END HERE


                ////UNCOMMENT THIS WHEN YOU ARE GOING TO DEPLOY TO LIVE
                //string sysLinkONEAccess = _LoginVerify.GetSysLinkONEAccess();
                //return Redirect(sysLinkONEAccess);
                ////END HERE
        }

        public ActionResult LogOut()
        {
            Session["intMstEmpPersonal"] = null;
            Session["Username"] = null;
            Session["UserPassword"] = null;
            Session["Status"] = null;
            Session["intOlnUsers"] = null;
            Session["isLoggedIn"] = null;

            Session["Personal"] = null;
            Session["DTR"] = null;
            Session["emp_name"] = null;
            Session["emp_company"] = null;
            Session["emp_branchcode"] = null;
            Session["emp_approvallevel"] = null;
            Session["emp_position"] = null;
            Session["emp_isHRRegional"] = null;
            Session["emp_workshift"] = null;
            Session.RemoveAll();

            //return Redirect("Login");
            string sysLinkONEAccess = _LoginVerify.GetSysLinkONEAccess();
            return Redirect(sysLinkONEAccess);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult ChangePassword(ChangePassword passwords)
        {
            string message = string.Empty;
            bool success = false;

            try
            {


                passwords.CorrectPassword = Session["UserPassword"].ToString();
                passwords.Username = Session["Username"].ToString();
                passwords.CurrentPassword = UtilitiesBAL.GetSHA1(passwords.Username + passwords.CurrentPassword);                

                message = UserBAL.ChangePassword(passwords);
                if (message.Contains("successfully")) { success = true; }
            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
                success = false;
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpGet]
        public ActionResult getTime()
        {
            var datetime = DateTime.Now;

            return Json(new { datetime = datetime.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogInUser(string stringA, string stringB, string stringC)
        {

            EmpPersonal emp = new EmpPersonal();
            EmpDTR dtr = new EmpDTR();
            ApproversName apr = new ApproversName();
            List<ObjUserDetails> usrdet = new List<ObjUserDetails>();

            if (_LoginVerify.VerifyUsername(stringA, stringB))
            {
                // TODO HERE
                // CREATE SESSION OR SOMETHING CHURVANESS                

                try
                {
                    var user = UserBAL.AuthenticateUser(stringA);
                    if (user.Username != "")
                    {
                        Session["intMstEmpPersonal"] = user.intMstEmpPersonal;
                        Session["Username"] = user.Username;
                        //Session["UserPassword"] = user.UserPassword;                                     
                        Session["UserPassword"] = _LoginVerify.GetUserPass(user.Username);
                        Session["Status"] = user.Status;
                        Session["intOlnUsers"] = user.intOlnUsers;
                        Session["isLoggedIn"] = true;

                        emp = _empBAL.GetEmployeePersonal(Session["intMstEmpPersonal"].ToString());
                        dtr = _empBAL.GetEmployeeDTR(Session["intMstEmpPersonal"].ToString());
                        usrdet = _empBAL.GetUserDetails(user.Username);
                        apr = _empBAL.GetEmployeeApproverName(Session["intMstEmpPersonal"].ToString());

                        ////objects into session
                        Session["Personal"] = emp;
                        Session["DTR"] = dtr;

                        Session["emp_firstApprover"] = apr.FirstApprover == null ? "" : apr.FirstApprover;
                        Session["emp_secondApprover"] = apr.SecondApprover == null ? "" : apr.SecondApprover;

                        Session["emp_name"] = emp.FirstName + " " + (emp.MiddleName.Length == 0 ? "" : emp.MiddleName.Substring(0, 1)) + ". " + emp.LastName;
                        Session["emp_company"] = emp.intMstCompany;
                        Session["emp_branchcode"] = dtr.codeMstBranchHome;
                        Session["emp_mstbranch"] = dtr.codeMstBranch;
                        Session["emp_approvallevel"] = _empBAL.GetEmployeeApprovalLevel(dtr.intMstPosition);
                        Session["emp_position"] = dtr.intMstPosition;
                        Session["emp_workshift"] = dtr.intMstWorkShift;
                        Session["one_userdet"] = usrdet;
                        Session["emp_hasWorkOnSat"] = _empBAL.hasWorkOnSaturday(dtr.intMstWorkShift);

                        Session["emp_isAllowedOvertime"] = UtilitiesBAL.isAllowedOvertime(dtr.intMstPosition);
                        Session["emp_isHRRegional"] = UtilitiesBAL.isHRRegional(dtr.intMstPosition);
                        Session["emp_isHRHomeOffice"] = UtilitiesBAL.isHRHomeOffice(dtr.intMstPosition);
                        Session["emp_isFinanceManager"] = UtilitiesBAL.isFinanceManager(dtr.intMstPosition);
                        Session["emp_canApproveTransactions"] = UtilitiesBAL.canApproveTransactions(dtr.intMstEmpPersonal);
                        Session["emp_canApplyOBHoliday"] = UtilitiesBAL.canApplyForOBHoliday(dtr.intMstPosition);

                        Session["UserPasswordDecrypted"] = _LoginVerify.Decrypt(stringC);


                        Employee201BAL Emp201 = new Employee201BAL();
                        System.Data.DataSet dds = Emp201.CheckLogin(user.intMstEmpPersonal);

                        if (dds.Tables[0].Rows.Count > 0)
                        {
                            Session["HasEmpUpdate"] = true;
                        }
                        else
                        {
                            Session["HasEmpUpdate"] = false;
                        }

                        //GroupInsuranceRights
                        System.Data.DataSet dds1 = Emp201.CheckGroupInsuranceRights(user.intMstEmpPersonal);

                        if (dds1.Tables[0].Rows.Count > 0)
                        {
                            Session["HasGroupInsurance"] = true;
                        }
                        else
                        {
                            Session["HasGroupInsurance"] = false;
                        }

                        //End GroupInsurance

                        //TASK MONITORING
                        System.Data.DataSet dds2 = Emp201.CheckTaskMonitoringRights(user.intMstEmpPersonal);

                        if (dds2.Tables[0].Rows.Count > 0)
                        {
                            Session["HasTaskMonitoring"] = true;
                        }
                        else
                        {
                            Session["HasTaskMonitoring"] = false;
                        }
                        //END OF TASK MONITORING


                        Employee201BAL EmpApprove201 = new Employee201BAL();
                        System.Data.DataSet ddds = EmpApprove201.EmpApprover201(user.intMstEmpPersonal);

                        if (ddds.Tables[0].Rows.Count > 0)
                        {
                            Session["emp_canApproveEmployee201"] = true;
                        }
                        else
                        {
                            Session["emp_canApproveEmployee201"] = false;
                        }

                        ContactsBAL CB = new ContactsBAL();
                        System.Data.DataSet ds = CB.Login(user.intMstEmpPersonal);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Session["IsAdmin"] = true;
                        }
                        else
                        {
                            Session["IsAdmin"] = false;
                        }




                        return RedirectToAction("Notification", "Employee");
                    }
                    else
                    {
                        string sysLinkONEAccess = _LoginVerify.GetSysLinkONEAccess();
                        return Redirect(sysLinkONEAccess);
                    }
                }
                catch (Exception)
                {
                    string sysLinkONEAccess = _LoginVerify.GetSysLinkONEAccess();

                    // REDIREC TO LINK
                    return Redirect(sysLinkONEAccess);
                }

                //// GET SYSTEMS ALLOWED TO VIEW
                //var userDetails = _BAL.GetUserDetails(stringA);
                //return View(userDetails);

            }
            else
            {
                // IF REACH HERE, USERNAME OR ONE TIME PASSWORD NOT VERIFIED
                // BACK TO ONE ACCESS LOGIN PAGE

                // GET ONE ACCESS LINK
                string sysLinkONEAccess = _LoginVerify.GetSysLinkONEAccess();

                // REDIREC TO LINK
                return Redirect(sysLinkONEAccess);
            }
        }        
    }
}
