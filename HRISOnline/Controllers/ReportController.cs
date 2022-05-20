using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using System.Security;
using HRISOnline.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;


namespace HRISOnline.Controllers
{
    public class ReportController : Controller
    {

        string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/");

        OvertimeBAL _otBAL = new OvertimeBAL();
        ReportBAL _rptBAL = new ReportBAL();
        Employee201BAL Emp201 = new Employee201BAL();

        #region Views       
        public ActionResult Index()
        {
            string Id = Session["intMstEmpPersonal"].ToString();
            System.Data.DataSet dds = Emp201.CheckForReportRightsMeals(Id);

            if (dds.Tables[0].Rows.Count > 0)
            {
                Session["HasOTReport"] = true;
            }
            else
            {
                Session["HasOTReport"] = false;
            }


            ViewBag.MyTitle = "Reports";
            return View();
        }
        #endregion  

        #region Per Transaction
        public void ReportPerTransaction(int id, string reportName, string moduleName, int reportIndex)
        {
            //string strPath = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            ReportDocument rpt = new ReportDocument();

            try
            {
                strPath += reportName;

                switch (moduleName)
                { 
                    case "Gate Pass":
                        if (reportIndex == 1)//Gate Pass form print out
                        {
                            dt = _rptBAL.GatePassReport(id);
                        }
                        break;
                }
                
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, moduleName);
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }
        #endregion

        #region Other Reports
        //a = Overtime Date
        //b = intMstPositionSupervisor
        //c = BranchCode
        //d = intMstCompany
        public void OvertimeList(string a, int b, string c, int d)
        {
            //string strPath = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            ReportDocument rpt = new ReportDocument();

            try
            {
                strPath += "rptOvertimeApprovalList.rpt";
                dt = _rptBAL.GetOvertimeApprovalListReport(d, c, b, DateTime.Parse(a));

                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void ViewDisapproveReport(DateTime dtFrom, DateTime dtTo, string TransType)
        {
            ReportDocument rpt = new ReportDocument();                                
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            try
            {                
                if (TransType == "Leave")
                    strPath += "rptDisapproveLeave.rpt";
                else
                    strPath += "rptDisapproveOT.rpt";

                dt = _rptBAL.GetDisapproveReport(intMstEmpPersonal, dtFrom, dtTo, TransType);
                
                rpt.Load(strPath);                

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void ViewCancelReport(DateTime dtFrom, DateTime dtTo, string TransType)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            try
            {
                if (TransType == "Leave")
                    strPath += "rptCancelLeave.rpt";
                else
                    strPath += "rptCancelOT.rpt";

                dt = _rptBAL.GetCancelReport(intMstEmpPersonal, dtFrom, dtTo, TransType);
                
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void SubordinateDTRReport(DateTime dtFrom, DateTime dtTo)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            strPath += "rptSubordinateDTR.rpt";
            try
            {
                dt = _rptBAL.GetSubordinateDTRReport(dtFrom, dtTo, intMstEmpPersonal);
                
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "SubordinateDTRReport");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void ApprovedAppsReport(DateTime dtFrom, DateTime dtTo, string strModule)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            if (strModule == "Leave")
                strPath += "rptApprovedTransLeave.rpt";
            else
                strPath += "rptApprovedTrans.rpt";
                        
            try
            {
                dt = _rptBAL.GetApprovedAppsReport(dtFrom, dtTo, strModule, intMstEmpPersonal);
                
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "ApprovedApplications_" + strModule);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void DisapprovedAppsReport(DateTime dtFrom, DateTime dtTo, string strModule)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            if (strModule == "Leave")
                strPath += "rptDisapproveLeave.rpt";
            else
                strPath += "rptDisapproveOT.rpt";

            try
            {
                dt = _rptBAL.GetDisapproveReport(intMstEmpPersonal, dtFrom, dtTo, strModule);
                
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "DisapprovedApplications_" + strModule);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void CancelledAppsReport(DateTime dtFrom, DateTime dtTo, string strModule)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            if (strModule == "Leave")
                strPath += "rptCancelLeave.rpt";
            else
                strPath += "rptCancelOT.rpt";

            try
            {
                dt = _rptBAL.GetCancelReport(intMstEmpPersonal, dtFrom, dtTo, strModule);
                
                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "CancelledApplications_" + strModule);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void HROB(DateTime dtFrom, DateTime dtTo)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            string branchcode = Session["emp_branchcode"].ToString();
            int position = (int)Session["emp_position"];

            strPath += "rptHROB.rpt";

            try
            {
                dt = _rptBAL.GetOB(dtFrom, dtTo, intMstEmpPersonal, branchcode, position);

                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "HR_OB_Transactions");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void HRLeave(DateTime dtFrom, DateTime dtTo)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();
            string branchcode = Session["emp_branchcode"].ToString();
            int position = (int)Session["emp_position"];

            strPath += "rptHRLeave.rpt";

            try
            {
                dt = _rptBAL.GetHRLeave(dtFrom, dtTo, intMstEmpPersonal, branchcode, position);

                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "HR_OB_Transactions");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }
        #endregion

        public void ViewOTMealsReport(DateTime dtFrom, DateTime dtTo, string strModule)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            
             strPath += "rptOvertimeMeals.rpt";

            try
            {
                dt = _rptBAL.GetOvertimeMealsReport(dtFrom, dtTo, strModule, intMstEmpPersonal);

                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "OvertimeMeals");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void ViewApproveAdjustmentReport(DateTime dtFrom, DateTime dtTo, string strModule)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();


            strPath += "rptApproveAdjustment.rpt";

            try
            {
                dt = _rptBAL.GetApprAdjustmentReport(dtFrom, dtTo, strModule, intMstEmpPersonal);

                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                //rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "ApprovedApplications_" + strModule);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "rptApproveAdjustment");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

        public void ViewDisApproveAdjustmentReport(DateTime dtFrom, DateTime dtTo, string strModule)
        {
            ReportDocument rpt = new ReportDocument();
            DataTable dt = new DataTable();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();


            strPath += "rptDisapproveAdjustment.rpt";

            try
            {
                dt = _rptBAL.GetDisApprAdjustmentReport(dtFrom, dtTo, strModule, intMstEmpPersonal);

                rpt.Load(strPath);

                rpt.SetDataSource(dt);
                
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "rptDisapproveAdjustment");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            rpt.Close();
            rpt.Dispose();
        }

    }
}
