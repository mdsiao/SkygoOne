using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Business;
using HRISOnline.Objects;
using HRISOnline.Models;
using HRISOnline.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using LoginVerification;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Diagnostics;
using System.Data;

namespace HRISOnline.Controllers
{    
    public class EmployeeController : Controller
    {
        LoginVerify _LoginVerify = new LoginVerify();
        EmployeeBAL _empBAL = new EmployeeBAL();
        ReportBAL _rptBAL = new ReportBAL();
        EmployeeDAL _empDAL = new EmployeeDAL();

        #region Employee
        [CheckSessionOut]
        public ActionResult Index()
        {

            EmpData emp = new EmpData();
            int noOfDays;

            try
            {
                emp.EmpPersonal = _empBAL.GetEmployeePersonal(Session["intMstEmpPersonal"].ToString());
                emp.DTR = (EmpDTR)Session["DTR"];
                emp.Family = _empBAL.GetEmployeeFamily(Session["intMstEmpPersonal"].ToString());
                emp.Children = _empBAL.GetEmployeeChildren(Session["intMstEmpPersonal"].ToString());
                emp.Education = _empBAL.GetEmployeeEducation(Session["intMstEmpPersonal"].ToString());
                emp.Eligibility = _empBAL.GetEmployeeEligibility(Session["intMstEmpPersonal"].ToString());
                emp.WorkExp = _empBAL.GetEmployeeWorkExperience(Session["intMstEmpPersonal"].ToString());
                emp.PayrollDetail = _empBAL.GetPayrollDetailList(Session["intMstEmpPersonal"].ToString());
                emp.AwardsAndBonus = _empBAL.GetAwardsAndBonus(Session["intMstEmpPersonal"].ToString());
                emp.Training = _empBAL.GetEmployeeTraining(Session["intMstEmpPersonal"].ToString());
                
                
                noOfDays = Convert.ToInt32(UtilitiesBAL.GetNoOfWorkingDays(emp.DTR.intMstWorkShift));

                emp.DTR.DailyRate = (emp.DTR.MonthlyRate * 12) / noOfDays;

                ViewBag.positions = new SelectList(UtilitiesBAL.GetPositionList(), "ValueMember", "DisplayMember", emp.DTR.intMstPosition);
                ViewBag.depts = new SelectList(UtilitiesBAL.GetDepartmentList(), "ValueMember", "DisplayMember", emp.DTR.intMstDepartment);
                ViewBag.branches = new SelectList(UtilitiesBAL.GetBranchList(), "ValueMember", "DisplayMember", emp.DTR.codeMstBranch);
                ViewBag.homebranches = new SelectList(UtilitiesBAL.GetBranchList(), "ValueMember", "DisplayMember", emp.DTR.codeMstBranchHome);
                ViewBag.workshifts = new SelectList(UtilitiesBAL.GetWorkShiftList(), "ValueMember", "DisplayMember", emp.DTR.intMstWorkShift);
                ViewBag.workstatus = new SelectList(UtilitiesBAL.GetWorkStatusList(), "ValueMember", "DisplayMember", emp.DTR.intMstWorkStatus);
                ViewBag.payrolllist = new SelectList(UtilitiesBAL.GetPayroll(), "ValueMember", "DisplayMember");
                //ViewBag.contactlist = new SelectList(_empBAL.GetEmployeeList(Session["emp_branchcode"].ToString()), "ValueMember", "DisplayMember");
                ViewBag.payperiods = new SelectList(_empBAL.GetPayrollDates());
               
                
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message.ToString());
                //ViewBag.payperiods = new SelectList(_empBAL.GetPayrollDates());
            }

            ViewBag.MyTitle = "Personal Profile";
            return View(emp);                  
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetDTR(string id)
        {
            DateTime dateStart;
            DateTime dateEnd;            
            string empid = Session["intMstEmpPersonal"].ToString();
            int companyid = (int)Session["emp_company"];
            string branchcode = Session["emp_branchcode"].ToString();

            string[] dates = id.Split('-');
            dateStart = Convert.ToDateTime(dates[0]);
            dateEnd = Convert.ToDateTime(dates[1]);            

            var list_bio = _empBAL.GetBiometric(companyid, branchcode, empid, dateStart, dateEnd);
            var list_logs = new List<DTRBiometricLogs>();
            //var list_logsx = _empBAL.GetBiometricLogs(companyid, branchcode, empid, dateStart, dateEnd);
            

            string serial_bio = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list_bio);
            string serial_logs = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list_logs);

            return Json(new { obj = serial_bio, objL = serial_logs }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetDTRDetail(string id) {

            DateTime dateStart;
            DateTime dateEnd;
            string empid = Session["intMstEmpPersonal"].ToString();
            int companyid = (int)Session["emp_company"];
            string branchcode = Session["emp_branchcode"].ToString();

            //string[] dates = id.Split('-');
            dateStart = Convert.ToDateTime(id);
            dateEnd = Convert.ToDateTime(id);

            //var list_bio = _empBAL.GetBiometric(companyid, branchcode, empid, dateStart, dateEnd);
            var list_logs = _empBAL.GetBiometricLogs(companyid, branchcode, empid, dateStart, dateEnd);

            //string serial_bio = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list_bio);
            string serial_logs = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list_logs);

            return Json(new { objL = serial_logs }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet, CheckSessionOut] 
        public ActionResult GetPayrollDetailList()
        {   
            string empid = Session["intMstEmpPersonal"].ToString();
            var list = _empBAL.GetPayrollDetailList(empid);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [CheckSessionOut]
        public ActionResult PayrollDetails(int id)
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            double tDeduct = 0.0;
            double tIncome = 0.0;

            var data = new PayrollData 
            { 
                PayrollEmp = _empBAL.GetPayrollDetailEmployee(id, empid),
                PayrollODeduc = _empBAL.GetPayrollOtherDeduction(id, empid),
                PayrollOInc = _empBAL.GetPayrollOtherIncome(id, empid)
            };

            foreach (var item in data.PayrollODeduc)
            {
                tDeduct += item.Amount;
            }

            foreach (var item in data.PayrollOInc)
            {
                tIncome += item.Amount;
            }

            data.PayrollEmp.TotalOtherDeductionDetail = tDeduct;
            data.PayrollEmp.TotalOtherIncomeDetail = tIncome;

            ViewBag.MyTitle = "Payroll Details";
            return View(data);
        }

        [CheckSessionOut]
        public ActionResult CancelledTrans()
        {
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            var list = _empBAL.GetCancelledTransactions(intMstEmpPersonal);

            ViewBag.MyTitle = "Cancelled Transactions";
            return View(list);
        }

        [CheckSessionOut]
        public ActionResult ApprovalNotification()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            var list = _empBAL.GetEmployeeApprovalNotification(empid);

            ViewBag.MyTitle = "Approval Notification";
            return View(list);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveEmployee(EmpSaving emp)
        {
            string message = string.Empty;
            bool success = false;

            try
            {

                emp.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
                message = _empBAL.SaveEmployee(emp, Session["Username"].ToString());
                if (message.Contains("Success")) { 
                    success = true;
                    message = "Successfully saved!";
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveElig(EmpEligibility elig)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                elig.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
                message = _empBAL.SaveEligibility(elig);
                if (message.Contains("Success")) { success = true; }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveWorkExperience(EmpWorkExperience we)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                we.intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
                message = _empBAL.SaveWorkExperience(we);
                if (message.Contains("Success")) { success = true; }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpPost, CheckSessionOut]
        public ActionResult SaveEducation(EmpEducation educ)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                educ.intMstEmpPersonal = Session["intMstEmppersonal"].ToString();
                educ.Honors = educ.Honors == null ? "" : educ.Honors;
                message = _empBAL.SaveEducation(educ);
                if (message.Contains("Success")) { success = true; }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = message });
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetEligibility()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            var list = _empBAL.GetEmployeeEligibility(empid);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetWorkExperience()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            var list = _empBAL.GetEmployeeWorkExperience(empid);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetEducation()
        {
            string empid = Session["intMstEmpPersonal"].ToString();
            var list = _empBAL.GetEmpEducationList(empid);
            string serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [CheckSessionOut, HttpPost]
        public ActionResult Delete(int id, string module)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                var empid = Session["intMstEmpPersonal"].ToString();
                message = _empBAL.DeleteItem(id, module, empid);
                if (message.Contains("Error"))
                {
                    throw new Exception(message);
                }
                else 
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
            }

            return Json(new { success = success.ToString(), msg = message });

        }

        [HttpGet, CheckSessionOut]
        public ActionResult CheckPassword(string id)
        {
            string msg = string.Empty;
            bool success = false;
            string getSHAvalue = UtilitiesBAL.GetSHA1(Session["Username"].ToString() + id);            

            if (Session["UserPassword"].ToString() == getSHAvalue){
                success = true;
                Session["UserPasswordDecrypted"] = id;
            }
            else {
                msg = "Invalid password! Please try again.";
            }

            return Json(new { success = success.ToString(), msg = msg }, JsonRequestBehavior.AllowGet);

        }
        
        #endregion

        #region Toolbox related
        [CheckSessionOut]
        public ActionResult Memo()
        {
            var memos = new List<MemorandumToolbox>();
            var msgdates = UtilitiesBAL.GetMessageDates();
            bool showMessage = false;
            DateTime currDate = DateTime.Now;

            memos = _empBAL.GetMemoToolbox(Session["Username"].ToString(), "Memorandum");

            if ((currDate >= msgdates.dateStart) && (currDate <= msgdates.dateEnd))
                showMessage = true;

            ViewBag.MyTitle = "Department Order";
            ViewBag.ShowMessage = showMessage;
            return View(memos);
        }

        [CheckSessionOut]
        public ActionResult Notification()
        {
            var notif = new List<NotificationToolbox>();

            notif = _empBAL.GetNotificationToolbox(Session["Username"].ToString(), "Notification");

            ////Department Group
            
            //DataSet ds = _empDAL.BindDepartmentGroupDDL();
            //ViewBag.DepartmentGroup = ds.Tables[0];
            //List<SelectListItem> items4 = new List<SelectListItem>();
            //foreach (System.Data.DataRow dr in ViewBag.DepartmentGroup.Rows)
            //{
            //    items4.Add(new SelectListItem { Text = @dr["DepartmentName"].ToString(), Value = @dr["intMstDepartment"].ToString() });
            //}
            //ViewBag.DepartmentGroup = items4;




            ViewBag.MyTitle = "Announcements";
            return View(notif);
        }

        [CheckSessionOut]
        public ActionResult EmployeeNotice()
        {
            var notice = new List<EmpNoticeToolbox>();

            notice = _empBAL.GetEmpNoticeToolbox(Session["Username"].ToString(), "Notice");

            ViewBag.MyTitle = "Employee Notice";
            return View(notice);
        }


        [CheckSessionOut]
        public ActionResult AuditReport()
        {
            var audit = new List<EmpAuditToolbox>();

            audit = _empBAL.GetAuditReportToolbox(Session["Username"].ToString(), "Audit");
            ViewBag.MyTitle = "Audit Report";
            return View(audit);
        }
        //FOR DEVIATION 
        public ActionResult GetAuditReportWithoutDeviationToolbox()
        {
            var audit = new List<EmpAuditToolbox>();

            audit = _empBAL.GetAuditReportWithoutDeviationToolbox(Session["Username"].ToString(), "Audit");
            ViewBag.MyTitle = "Audit Report Without Deviation";
            return View(audit);
        }

        public ActionResult GetAuditReportWithMinorDeviationToolbox()
        {
            var audit = new List<EmpAuditToolbox>();

            audit = _empBAL.GetAuditReportWithMinorDeviationToolbox(Session["Username"].ToString(), "Audit");
            ViewBag.MyTitle = "Audit Report With Minor Deviation";
            return View(audit);
        }

        public ActionResult GetAuditReportWithMajorDeviationToolbox()
        {
            var audit = new List<EmpAuditToolbox>();

            audit = _empBAL.GetAuditReportWithMajorDeviationToolbox(Session["Username"].ToString(), "Audit");
            ViewBag.MyTitle = "Audit Report With Major Deviation";
            return View(audit);
        }


        //END FOR DEVIATION
        [CheckSessionOut]
        public ActionResult Policy()
        {
            var poli = new List<PolicyToolbox>();

            poli = _empBAL.GetPolicyToolbox(Session["Username"].ToString(), "Policy");

            ViewBag.MyTitle = "Memo Circular";
            return View(poli);
        }

        public FileStreamResult GetPDF(string id, bool isOpen, string whatType, string folderName)
        {
            string _result = string.Empty;
            string docPath = UtilitiesBAL.GetDocumentPath();

            try
            {

                if (isOpen == false)
                {
                    _result = _empBAL.UpdateMemoToOpen(id, Session["Username"].ToString(), whatType);
                }
                else
                {
                    _result = "Opened";
                }

                if (_result.Contains("Opened"))
                {

                    FileStream fs = new FileStream(docPath + folderName + "\\" + id, FileMode.Open, FileAccess.Read);
                    return File(fs, "application/pdf");
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        [HttpGet, CheckSessionOut]
        public ActionResult GetAnnCount()
        {

            string username = "";
            if (Session["Username"] != null)
            {
                username = Session["Username"].ToString();
            }

            var list = _empBAL.GetAnnouncementCount(username, Session["intMstEmpPersonal"].ToString());
            var serial = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

            return Json(new { obj = serial }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, CheckSessionOut]
        public ActionResult AcknowledgeDocument(string fileName, string whatType)
        {
            string message = string.Empty;
            string _userName = string.Empty;
            bool success = false;

            try
            {
                _userName = Session["Username"].ToString();
                message = _empBAL.AcknowledgeDocument(fileName, _userName, whatType);
                if (message.ToLower().Contains("success"))
                    success = true;

                return Json(new { success = success.ToString(), msg = message });
                
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message.ToString();
                return Json(new { textStatus = success.ToString(), errorThrown = message });
            }

            
        }
        #endregion
        
        #region PaySlip Report
        public void GetPaySlipParam(int id)
        {
            Session["PaySlip_intTrnPayroll"] = id;
        }

        //[CheckSessionOut]
        //public void ViewPaySlip(int id)
        //{

        //    string empid = Session["intMstEmpPersonal"].ToString();
        //    string branchCode = Session["emp_branchcode"].ToString();
        //    //int intTrnPayroll = (int)Session["PaySlip_intTrnPayroll"];
        //    int intTrnPayroll = id;
        //    string payrollDate = _empBAL.GetPayrollDatePaySlip(intTrnPayroll);

        //    try
        //    {
        //        string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/rptPaySlip.rpt");

        //        var dt = _rptBAL.PayrollPayslip(intTrnPayroll, empid, branchCode);
        //        var dtODeduc = _rptBAL.PayrollPayslipOtherDeduction(intTrnPayroll, empid);
        //        var dtOInc = _rptBAL.PayrollPayslipOtherIncome(intTrnPayroll, empid);


        //        ReportDocument rpt = new ReportDocument();
        //        rpt.Load(strPath);

        //        rpt.SetDataSource(dt);
        //        rpt.Subreports["rptPaySlip_OtherIncome"].SetDataSource(dtOInc);
        //        rpt.Subreports["rptPaySlip_OtherDeduction"].SetDataSource(dtODeduc);

        //        //rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                                
        //        payrollDate = payrollDate.Replace("/", "-");

        //        //create pdf using crystal and set password
        //        string payslipName = payrollDate + "_Payslip_" + Session["emp_name"].ToString().Replace(" ", "") + ".pdf";
        //        string pwd = Session["UserPasswordDecrypted"].ToString();
        //        string pathToFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        //        string inputFile = Path.Combine(pathToFile, payslipName);

        //        var ms = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
        //        var doc = new Document(PageSize.LETTER);
        //        ms.Position = 0;
                
        //        //using (var fs = new FileStream(inputFile, FileMode.Open, FileAccess.Write, FileShare.None))
        //        //{
        //        //    PdfReader reader = new PdfReader(ms);
        //        //    PdfEncryptor.Encrypt(reader, fs, true, pwd, pwd, PdfWriter.ALLOW_SCREENREADERS);
        //        //    //Process.Start(inputFile);                    
        //        //}
                
        //        MemoryStream memoryStream = new MemoryStream();
        //        ms.CopyTo(memoryStream);

        //        using (PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream))
        //        {
        //            //doc.Open();                    
        //            writer.SetEncryption(PdfWriter.STRENGTH40BITS, pwd, pwd, PdfWriter.AllowCopy);
        //            writer.Close();
        //            ms.Close();
        //            //doc.Close();
        //            Response.ContentType = "pdf/application";
        //            Response.AddHeader("content-disposition", "attachment;filename=" + payslipName);
        //            Response.OutputStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
        //        }

        //        Session["UserPasswordDecrypted"] = null;
        //        //return File(inputFile, "application/pdf");                    

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //        //return null;
        //    }
        //}

        [CheckSessionOut]
        public void ViewPaySlip(string id)
        {
            string[] splitStr = id.Split('~');
            int intTrnPayroll = Convert.ToInt32(splitStr[0]);
            string empid = splitStr[1];
            //string branchCode = Session["emp_branchcode"].ToString();                        
            string branchCode = "ALL";//replace this to ALL to view the payslip even if the employee is transfered
            string payrollDate = _empBAL.GetPayrollDatePaySlip(intTrnPayroll);
            ReportDocument rpt = new ReportDocument();

            try
            {
                string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/rptPaySlip.rpt");

                //getting the data needed for the report
                var dt = _rptBAL.PayrollPayslip(intTrnPayroll, empid, branchCode);
                var dtODeduc = _rptBAL.PayrollPayslipOtherDeduction(intTrnPayroll, empid);
                var dtOInc = _rptBAL.PayrollPayslipOtherIncome(intTrnPayroll, empid);
                
                rpt.Load(strPath);

                //set the source
                rpt.SetDataSource(dt);
                rpt.Subreports["rptPaySlip_OtherIncome"].SetDataSource(dtOInc);
                rpt.Subreports["rptPaySlip_OtherDeduction"].SetDataSource(dtODeduc);

                //rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                payrollDate = payrollDate.Replace("/", "-");

                //create pdf using crystal and set password
                string payslipName = payrollDate + "_Payslip_" + Session["emp_name"].ToString().Replace(" ", "") + ".pdf";
                string pwd = Session["UserPasswordDecrypted"].ToString();
                string pathToFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string inputFile = Path.Combine(pathToFile, payslipName);

                //this will export the convert to Stream(resides on the memory)
                var ms = rpt.ExportToStream(ExportFormatType.PortableDocFormat);

                Document pdfDoc = new Document(PageSize.LETTER);//determine the paper size of the document                
                using (MemoryStream memoryStream = new MemoryStream())//this will handle the exported rpt
                {
                    PdfWriter.GetInstance(pdfDoc, memoryStream);
                    //pass the Stream(ms) to MemoryStream
                    ms.CopyTo(memoryStream);                    
                    byte[] bytes = memoryStream.ToArray();//convert it to byte
                    memoryStream.Close();

                    using (MemoryStream input = new MemoryStream(bytes))//this is the rpt file(exported to stream)
                    {
                        using (MemoryStream output = new MemoryStream())//this will be the result with the password in it
                        {
                            PdfReader reader = new PdfReader(input);//create reader
                            PdfEncryptor.Encrypt(reader, output, true, pwd, pwd, PdfWriter.ALLOW_SCREENREADERS);//set the password
                            //pop up for the SAVE AS of the pdf file
                            bytes = output.ToArray();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + payslipName);
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(bytes);
                            Response.End();
                        }
                    }
                }

                //Session["UserPasswordDecrypted"] = null;                
                //return File(inputFile, "application/pdf");                    

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                //return null;
            }

            rpt.Close();
            rpt.Dispose();
        }

        [CheckSessionOut]
        public void ViewAwardAndBonus(string id)
        {
            string[] splitStr = id.Split('~');
            int intTrnPayroll = Convert.ToInt32(splitStr[0]);
            string empid = splitStr[1];
            string incentiveDate = splitStr[2];
            string bonusType = splitStr[3].Replace(" ", "_");
            //string branchCode = Session["emp_branchcode"].ToString();                        
            //string branchCode = "ALL";//replace this to ALL to view the payslip even if the employee is transfered
            //string payrollDate = _empBAL.GetPayrollDatePaySlip(intTrnPayroll);
            ReportDocument rpt = new ReportDocument();

            try
            {
                string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/AwardAndBonus_Payslip.rpt");

                //getting the data needed for the report
                var dt = _rptBAL.PayrollAwardAndBonus(intTrnPayroll, empid);
                //var dtODeduc = _rptBAL.PayrollPayslipOtherDeduction(intTrnPayroll, empid);
                //var dtOInc = _rptBAL.PayrollPayslipOtherIncome(intTrnPayroll, empid);

                rpt.Load(strPath);

                //set the source
                rpt.SetDataSource(dt);
                //rpt.Subreports["rptPaySlip_OtherIncome"].SetDataSource(dtOInc);
                //rpt.Subreports["rptPaySlip_OtherDeduction"].SetDataSource(dtODeduc);

                //rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                //payrollDate = payrollDate.Replace("/", "-");

                //create pdf using crystal and set password
                string payslipName = incentiveDate + "_" + bonusType + "_Payslip_" + Session["emp_name"].ToString().Replace(" ", "") + ".pdf";
                string pwd = Session["UserPasswordDecrypted"].ToString();
                string pathToFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string inputFile = Path.Combine(pathToFile, payslipName);

                //this will export the convert to Stream(resides on the memory)
                var ms = rpt.ExportToStream(ExportFormatType.PortableDocFormat);

                Document pdfDoc = new Document(PageSize.LETTER);//determine the paper size of the document                
                using (MemoryStream memoryStream = new MemoryStream())//this will handle the exported rpt
                {
                    PdfWriter.GetInstance(pdfDoc, memoryStream);
                    //pass the Stream(ms) to MemoryStream
                    ms.CopyTo(memoryStream);
                    byte[] bytes = memoryStream.ToArray();//convert it to byte
                    memoryStream.Close();

                    using (MemoryStream input = new MemoryStream(bytes))//this is the rpt file(exported to stream)
                    {
                        using (MemoryStream output = new MemoryStream())//this will be the result with the password in it
                        {
                            PdfReader reader = new PdfReader(input);//create reader
                            PdfEncryptor.Encrypt(reader, output, true, pwd, pwd, PdfWriter.ALLOW_SCREENREADERS);//set the password
                            //pop up for the SAVE AS of the pdf file
                            bytes = output.ToArray();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + payslipName);
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(bytes);
                            Response.End();
                        }
                    }
                }

                //Session["UserPasswordDecrypted"] = null;                
                //return File(inputFile, "application/pdf");                    

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                //return null;
            }

            rpt.Close();
            rpt.Dispose();
        }
        #endregion

        #region Other Report
        [CheckSessionOut]
        public void GetDTRReport(string id)
        {
            DateTime dateStart;
            DateTime dateEnd;
            string empid = Session["intMstEmpPersonal"].ToString();
            int companyid = (int)Session["emp_company"];
            string branchcode = Session["emp_branchcode"].ToString();

            string[] dates = id.Split('-');
            dateStart = Convert.ToDateTime(dates[0]);
            dateEnd = Convert.ToDateTime(dates[1]);

            try
            {
                string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/rptDTR.rpt");

                var dt = _empBAL.GetDTRReport(dateStart, dateEnd, companyid, branchcode, empid);

                ReportDocument rpt = new ReportDocument();
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "DTRReport");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }           
           
        }
        #endregion

        #region ONE Access
        [CheckSessionOut]
        public ActionResult OpenNewSystem(int intMstSysMasterfile, int ComNo = 0)
        {
            // LOAD USER DETAILS TABLE
            var userDetails = _empBAL.GetUserDetails(this.HttpContext.Session["Username"].ToString());

            // GET THE SYSTEM LINK AND USER RIGHT WANTED TO VIEW
            var theSystem = userDetails.Find(item => item.intMstSysMasterfile == intMstSysMasterfile);

            // GET THE ONE TIME USER PASSWORD
            string oneTimeUserPass = _LoginVerify.OneTimeUserPass(_LoginVerify.GetUserPass(this.HttpContext.Session["Username"].ToString()));

            // REDIRECT TO WEBSITE
            return Redirect(theSystem.sysLink + "stringA=" + theSystem.userName + "&stringB=" + oneTimeUserPass + "&right=" + theSystem.intMstSysRights + "&intMstSysMasterFile=" + intMstSysMasterfile + "&ComNo=" + ComNo + "&isShortCut=false");
        }

        [CheckSessionOut]
        public ActionResult OpenToolboxCommunication(int intMstSysMasterfile, int ComNo)
        {
            // LOAD USER DETAILS TABLE
            var userDetails = _empBAL.GetUserDetails(this.HttpContext.Session["Username"].ToString());

            // GET THE SYSTEM LINK AND USER RIGHT WANTED TO VIEW
            var theSystem = userDetails.Find(item => item.intMstSysMasterfile == intMstSysMasterfile);


            if (theSystem != null)
            {
                // GET THE ONE TIME USER PASSWORD
                string oneTimeUserPass = _LoginVerify.OneTimeUserPass(_LoginVerify.GetUserPass(this.HttpContext.Session["Username"].ToString()));

                // REDIRECT TO WEBSITE
                return Redirect(theSystem.sysLink + "stringA=" + theSystem.userName + "&stringB=" + oneTimeUserPass + "&right=" + theSystem.intMstSysRights + "&intMstSysMasterFile=" + intMstSysMasterfile + "&ComNo=" + ComNo + "&isShortCut=true");
            }
            else 
            {
                return RedirectToAction("ErrorNoRightsToolbox");
            }
            
        }

        [CheckSessionOut]
        public ActionResult ErrorNoRightsToolbox()
        {
            return View();
        }
        #endregion

        public ActionResult DocumentReaction(int ReactionType, string ID, string FName)
        {
            string message = string.Empty;
            string _userName = string.Empty;
            string success = "";

            try
            {
                _userName = Session["Username"].ToString();
                message = _empBAL.DocumentReaction(ReactionType, ID, FName, _userName);
                if (message.ToLower().Contains("successlike"))
                {
                    success = "successlike"; 
                }
                else if (message.ToLower().Contains("successheart"))
                {
                    success = "successheart";
                }
                else if (message.ToLower().Contains("successsad"))
                {
                    success = "successsad";
                }
                else if (message.ToLower().Contains("successdislike"))
                {
                    success = "successdislike";
                }
                    

                return Json(new { success = success.ToString(), msg = message });

            }
            catch (Exception ex)
            {
                success = "";
                message = ex.Message.ToString();
                return Json(new { textStatus = success.ToString(), errorThrown = message });
            }
        }

        public ActionResult GetRecentReaction(string Filename)
        {
            //string message = string.Empty;
            string _userName = string.Empty;
            //string success = "";

            
                _userName = Session["Username"].ToString();
               var  list = _empBAL.GetRecentReaction(Filename, _userName);
               //var _dataRec = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);

              

               //return Json(new { obj = _dataRec }, JsonRequestBehavior.AllowGet);

               return Json(new { intReaction = list.intReaction.ToString(), strReaction = list.strReaction.ToString(), IsAllowed = list.IsAllowed.ToString() }, JsonRequestBehavior.AllowGet);
        }
    }
}