using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Objects;
using HRISOnline.Business;
using HRISOnline.Data;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace HRISOnline.Controllers
{
    public class MissingPunchController : Controller
    {
        //
        // GET: /MissingPunch/

        public ActionResult Index()
        {

            MissedPunchDAL AdjustDAL = new MissedPunchDAL();
            string intMstEmpPersonal = Session["intMstEmpPersonal"].ToString();


            MissingPunch miss = new MissingPunch()
            {
                intIDMissingpunch = 0,
                DateFiled = DateTime.Now,
                DatActualDate = DateTime.Now,
                intMstEmpPersonal = Session["intMstEmpPersonal"].ToString()

            };

            //Adjustment Type dropdown
            DataSet ds = AdjustDAL.AdjustmentTypeDDL(intMstEmpPersonal);
            ViewBag.AdjustmentType = ds.Tables[0];
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in ViewBag.AdjustmentType.Rows)
            {
                items.Add(new SelectListItem { Text = @dr["AdjustmentType"].ToString(), Value = @dr["ID"].ToString() });
            }
            ViewBag.AdjustmentType = items;


            ////Branch/Department dropdown
            //DataSet ds1 = AdjustDAL.BranchDeptTypeDDL(intMstEmpPersonal);
            //ViewBag.EmpName = ds1.Tables[0];
            //List<SelectListItem> item1 = new List<SelectListItem>();
            //foreach (System.Data.DataRow dr in ViewBag.EmpName.Rows)
            //{
            //    item1.Add(new SelectListItem { Text = @dr["EmployeeName"].ToString(), Value = @dr["intMstEmpPersonal"].ToString() });
            //}
            //ViewBag.EmpName = item1;



            ViewBag.MyTitle = "Apply for Adjustment on DTR";
            return View(miss);
        }

        public ActionResult InsertMissedPunch(MissingPunch punch)
        {
            MissedPunchBAL MissedPun = new MissedPunchBAL();
            string result = string.Empty;
            bool success = false;
            //string result = MissedPun.InsertMissedPunch(punch);

            //if (result != "Success")
            //{
            //    return Json(new { success = false, responseText = "Failed to saved! Please check your entry!" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { success = true, responseText = "Successfully Saved!" }, JsonRequestBehavior.AllowGet);
            //}

            try
            {
                result = MissedPun.InsertMissedPunch(punch);
                success = true;
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
                success = false;
            }


            return Json(new { success = success.ToString(), msg = result });
        }

        public ActionResult ListOfMissingPunch()
        {
            MissedPunchBAL MissedPun = new MissedPunchBAL();

            string empid = Session["intMstEmpPersonal"].ToString();
            List<MissingPunchList> list = MissedPun.ListOfMissingPunch(empid);

            ViewBag.MyTitle = "List of my Filed Missing Punch";
            return View(list.ToList());
        }

        public ActionResult CancelPunch(string intITHead)
        {
            MissedPunchBAL MissedPun = new MissedPunchBAL();

            string result = MissedPun.CancelPunch(intITHead);

            if (result != "Cancel")
            {
                return Json(new { success = false, responseText = "Failed to Cancel!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Cancelled!" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult MissingPunchApproval()
        {
            MissedPunchBAL MissedPun = new MissedPunchBAL();

            string empid = Session["intMstEmpPersonal"].ToString();
            List<MissingPunchAppAndDis> list = MissedPun.MissingPunchApproval(empid);


            ViewBag.MyTitle = "Missing Punches for Approval/Disapproval";
  
            return View(list.ToList());
        }

        public ActionResult ApproveMissingPunch(string Details)
        {
            MissedPunchBAL MissedPun = new MissedPunchBAL();

            string Id = Session["intMstEmpPersonal"].ToString();


            string Result = MissedPun.ApproveMissingPunch(Details, Id);

            if (Result != "Approve")
            {
                return Json(new { success = false, responseText = "Failed to Approved!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Approved!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DisapproveMissingPunch(string Details, string Reason)
        {
            MissedPunchBAL MissedPun = new MissedPunchBAL();

            string Id = Session["intMstEmpPersonal"].ToString();

            string Result = MissedPun.DisapproveMissingPunch(Details, Reason, Id);

            if (Result != "disapprove")
            {
                return Json(new { success = false, responseText = "Failed to Approved!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Disapproved!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SetDates(DateTime gpDate, string TimeIn, string TimeOut)
        {
            OvertimeBAL _otBAL = new OvertimeBAL();

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

            //if (dDataOut != dDateOut)
            //{
            //    NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

            //    return Json(new { NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //else if (dDataOut != dDateIn)
            //{
            //    NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

            //    return Json(new { shift = shiftStart.ToString(), datanum = 1, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //else 
            //if (dDataIn != dDateOut)
            //{
            //    NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

            //    return Json(new { shift = shiftEnd.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}
            //else if (dDataIn != dDateIn)
            //{
            //    NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

            //    return Json(new { shift = shiftStart.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            //}


            return Json(new { shiftIn = shiftStart.ToString(), shiftOut = shiftEnd.ToString(), NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
        }

 
        public ActionResult GetShifts(DateTime gpDate)
        {
            OvertimeBAL _otBAL = new OvertimeBAL();

            var _startTime = _otBAL.GetStartOfShift((int)Session["emp_workshift"], gpDate);
            var _endTime = _otBAL.GetEndOfShift((int)Session["emp_workshift"], gpDate);

            return Json(new { startOfTime = _startTime, endOfTime = _endTime }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetDatesMissed(DateTime gpDate, string TimeIn, string TimeOut)
        {

            OvertimeBAL _otBAL = new OvertimeBAL();

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

            if (dDataOut > dDateOut)
            {
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
            else if (dDataOut < dDateIn)
            {
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftStart.ToString(), datanum = 1, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
            else if (dDataIn > dDateOut)
            {
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftEnd.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }
            else if (dDataIn < dDateIn)
            {
                NoOfHrs = _otBAL.ComputeHours(dDataOut, dDataIn).ToString();

                return Json(new { shift = shiftStart.ToString(), datanum = 2, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { shift = "", datanum = 0, NoOfHrs = NoOfHrs }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Details(int ID)
        //{
        //    MissedPunchBAL MissedPun = new MissedPunchBAL();

        //    //List<MissingPunchList> list = MissedPun.ViewDetals(ID);

        //    ViewBag.MyTitle = "View Details";

        //    return View();
        //}

        public ActionResult ViewMissedDetails(int ID)
        {
            ViewDetailsLogs DetailLogs = new ViewDetailsLogs();


            //this is for the missed puch
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewAdjustmentsLogs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<MissedPunch> MissedPunchs = new List<MissedPunch>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        MissedPunch MissedPunch = new MissedPunch();

                        MissedPunch.intIDMissingpunch = Convert.ToInt32(dset.Tables[0].Rows[i]["intIDMissingpunch"].ToString());
                        MissedPunch.intMstEmpPersonal = dset.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        MissedPunch.DateFiled = dset.Tables[0].Rows[i]["DateFiled"].ToString();
                        MissedPunch.DatActualDate = dset.Tables[0].Rows[i]["DatActualDate"].ToString();
                        MissedPunch.AdjustmentTypeID = Convert.ToInt32(dset.Tables[0].Rows[i]["AdjustmentTypeID"].ToString());
                        MissedPunch.AdjustmentType = dset.Tables[0].Rows[i]["AdjustmentType"].ToString();
                        MissedPunch.MissedTimeIn = dset.Tables[0].Rows[i]["TimeIn"].ToString();
                        MissedPunch.MissedTimeOut = dset.Tables[0].Rows[i]["TimeOut"].ToString();
                        MissedPunch.MissedNoOfHours = dset.Tables[0].Rows[i]["NoOfHours"].ToString();
                        MissedPunch.Reason = dset.Tables[0].Rows[i]["Reason"].ToString();
                        MissedPunchs.Add(MissedPunch);
                    }
                    DetailLogs.ViewMissedPunch = MissedPunchs;
                }
                con.Close();
            }


            //this is for biometric logs
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset1 = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewBiometricLogs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset1);

                    List<MissedPunchLogs> MissedPunchLogs = new List<MissedPunchLogs>();

                    for (int i = 0; i < dset1.Tables[0].Rows.Count; i++)
                    {
                        MissedPunchLogs MissedPunchLog = new MissedPunchLogs();

                        MissedPunchLog.DatLogDate = dset1.Tables[0].Rows[i]["LogDate"].ToString();
                        MissedPunchLog.BioTimeIn = dset1.Tables[0].Rows[i]["LogTimeIn"].ToString();
                        MissedPunchLog.BioTimeOut = dset1.Tables[0].Rows[i]["LogTimeOut"].ToString();
                        MissedPunchLog.BioNoHours = dset1.Tables[0].Rows[i]["NoOfHours"].ToString();
                        //MissedPunchLog.intMstEmpPersonal = dset1.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        //MissedPunchLog.DisApporvedBy = dset1.Tables[0].Rows[i]["DisapprovedBy"].ToString();
                        MissedPunchLog.strStatus = dset1.Tables[0].Rows[i]["strStatus"].ToString();
                        MissedPunchLog.DisapproveDateTime = dset1.Tables[0].Rows[i]["DisapprovedDateTime"].ToString();
                        MissedPunchLog.DisapproveReason = dset1.Tables[0].Rows[i]["DisapproveReason"].ToString();

                        MissedPunchLogs.Add(MissedPunchLog);
                    }
                    DetailLogs.ViewMissedPunchLogs = MissedPunchLogs;
                }
                con.Close();
            }

            ViewBag.MyTitle = "View Missing Punch Details";
            return View(DetailLogs);
        }







    }
}
