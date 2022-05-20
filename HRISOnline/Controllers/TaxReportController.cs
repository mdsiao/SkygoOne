using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Business;
using HRISOnline.Objects;
using HRISOnline.Models;
using System.Collections.Specialized;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf.parser;
using CrystalDecisions.Shared;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

namespace HRISOnline.Controllers
{
    public class TaxReportController : Controller
    {
        TaxReportBAL _TaxReportBAL = new TaxReportBAL();

        [CheckSessionOut]
        public ActionResult Index()
        {
            ViewBag.MyTitle = "Tax Refund/Payable Report";

            var items = _TaxReportBAL.GetEmployeeReports(Session["intMstEmpPersonal"].ToString());

            return View(items);
        }

        [CheckSessionOut]
        public void ViewReport(string id)
        {
            string[] splitStr = id.Split('~');
            string tblName = splitStr[0];
            int tblID = int.Parse(splitStr[1].ToString());
            string rptName = splitStr[2];
            string storedProcedure = splitStr[3];
            string dateFiled = splitStr[4];
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();

            ReportDocument rpt = new ReportDocument();

            try
            {
                string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/" + rptName + ".rpt");

                //getting the data needed for the report
                string strSql = storedProcedure + " " + tblID + "," + intMstEmpPersonal;
                var dt = _TaxReportBAL.ViewReport(storedProcedure, tblID, intMstEmpPersonal);

                rpt.Load(strPath);

                //set the source
                rpt.SetDataSource(dt);

                //rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                //create pdf using crystal and set password
                string payslipName = tblName + "_" + dateFiled + "_" + Session["emp_name"].ToString().Replace(" ", "") + ".pdf";
                string pwd = Session["UserPasswordDecrypted"].ToString();
                string pathToFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string inputFile = System.IO.Path.Combine(pathToFile, payslipName);

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
    }
}
