using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace HRISOnline.Controllers
{
    public class TaskMonitoringController : Controller
    {
        //
        // GET: /TaskMonitoring/

        public ActionResult Index()
        {
            TaskMonitoringBAL DDLSampling = new TaskMonitoringBAL();

            DataSet ds1 = DDLSampling.BindDDLSpareparts();
            ViewBag.SparePartSampling = ds1.Tables[0];
            List<SelectListItem> items1 = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in ViewBag.SparePartSampling.Rows)
            {
                //items1.Add(new SelectListItem { Text = @dr["ItemDescription"].ToString(), Value = @dr["ItemCode"].ToString() });
                items1.Add(new SelectListItem { Text = @dr["ItemCode"].ToString(), Value = @dr["ItemDescription"].ToString() });
            }
            ViewBag.SparePartSampling = items1; 
            //ViewBag.SpartPartsDescription = ds1.Tables[0].Rows[0]["ItemDescritpion"].ToString();

            var emptySource = new List<ComboBoxSource>();

            ViewBag.emps = new SelectList(OtherDAL.GetComboData(), "ValueMember", "DisplayMember");



            SpartPartsSampling getTaskMonitoringData = new SpartPartsSampling();

            string Id = Session["intMstEmpPersonal"].ToString();

            //------CASH COUNT COLLECTION --------//
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spGetTaskMonitoringData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);
                    cmd.Parameters.AddWithValue("@TaskNo", 1);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<CashCountCollection> CashCountCollections = new List<CashCountCollection>();


                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        CashCountCollection CashCountCollection = new CashCountCollection();

                        CashCountCollection.strDate = dset.Tables[0].Rows[i]["strDDATE"].ToString();
                        CashCountCollection.datCollectionDateSKYGO = dset.Tables[0].Rows[i]["datCollectionDateSkygo"].ToString();
                        CashCountCollection.intCashCountCollectionSKYGO = dset.Tables[0].Rows[i]["intCashCountCollectionSKYGO"].ToString();
                        CashCountCollection.intCashCountAccountabilitySKYGO = dset.Tables[0].Rows[i]["intCashCountAccountabilitySKYGO"].ToString();
                        CashCountCollection.TimeCollectionDateSKYGO = dset.Tables[0].Rows[i]["collectinTimeSKYGO"].ToString();

                        CashCountCollection.datCollectionDateSNDC = dset.Tables[0].Rows[i]["datCollectionDateSNDC"].ToString();
                        CashCountCollection.intCashCountCollectionSNDC = dset.Tables[0].Rows[i]["intCashCountCollectionSNDC"].ToString();
                        CashCountCollection.intCashCountAccountabilitySNDC = dset.Tables[0].Rows[i]["intCashCountAccountabilitySNDC"].ToString();
                        CashCountCollection.TimeCollectionDateSNDC = dset.Tables[0].Rows[i]["collectinTimeSNDC"].ToString();

                        CashCountCollections.Add(CashCountCollection);
                    }
                    getTaskMonitoringData.TaskCashCountCollection = CashCountCollections;
                }
                con.Close();
            } //------ END CASH COUNT COLLECTION --------//





            return View(getTaskMonitoringData);
        }

        public ActionResult SaveCashCountCollection(CashCountCollection ccCollection)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

       
            string result = taskmonitor.SaveCashCountCollection(ccCollection);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveCashCountPettyCash(CashCountPettyCash ccPettyCash)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

            string result = taskmonitor.SaveCashCountPettyCash(ccPettyCash);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveReviewVsDeposits(ReviewVsDeposits ReviewDeposits)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

            string result = taskmonitor.SaveReviewVsDeposits(ReviewDeposits);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveValidateDepositVsAbstactDeposit(ValDepositVsAbstDeposit vdVSad)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

            string result = taskmonitor.SaveValidateDepositVsAbstactDeposit(vdVSad);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SavePartsCountandRecon(PartsCountandRecon partscountrecon)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

            string result = taskmonitor.SavePartsCountandRecon(partscountrecon);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveMCUnitCount(MCUnitCount MCCount)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

            string result = taskmonitor.SaveMCUnitCount(MCCount);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveSpartPartsSampling(SpartPartsSampling partssamp)
        {
            TaskMonitoringBAL taskmonitor = new TaskMonitoringBAL();

            string result = taskmonitor.SaveSpartPartsSampling(partssamp);

            if (result != "Insert")
            { 
                return Json(new { success = false, responseText = "Failed to saved. Please check on the report if you already submitted!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully submitted!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetData()
        {
            var data = OtherDAL.GetComboData();

            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
