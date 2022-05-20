using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HRISOnline.Objects;
using HRISOnline.Business;
using HRISOnline.Models;

namespace HRISOnline.Controllers
{
    public class EmployeeTwoOhOneController : Controller
    {
        //myEmployee201Profile
        // GET: /EmployeeTwoOhOne/
        //public ActionResult Index(Employee201 emp)
        
        public ActionResult Index()
        {
            
            EmpDisplayDetails EmpDisplayDetails = new EmpDisplayDetails();

            string Id = Session["intMstEmpPersonal"].ToString();


            //UpdateLogs

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset6 = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spDisplayUpdateLogs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                  
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset6);

                    List<EmpUpdateLogs> EmpUpdateLogs = new List<EmpUpdateLogs>();

                    for (int i = 0; i < dset6.Tables[0].Rows.Count; i++)
                    {
                        EmpUpdateLogs EmpUpdateLog = new EmpUpdateLogs();

                        EmpUpdateLog.alreadyUpdatePersonal = Convert.ToInt32(dset6.Tables[0].Rows[i]["personal"].ToString());
                        EmpUpdateLog.alreadyUpdateLegal = Convert.ToInt32(dset6.Tables[0].Rows[i]["legal"].ToString());
                        EmpUpdateLog.alreadyUpdateEducational = Convert.ToInt32(dset6.Tables[0].Rows[i]["Educ"].ToString());
                        EmpUpdateLog.alreadyUpdateWork = Convert.ToInt32(dset6.Tables[0].Rows[i]["Work"].ToString());
                        EmpUpdateLog.alreadyUpdateTraining = Convert.ToInt32(dset6.Tables[0].Rows[i]["Training"].ToString());

                        EmpUpdateLogs.Add(EmpUpdateLog);
                    }
                    EmpDisplayDetails.EmpUpdateLogings = EmpUpdateLogs;
                }
                con.Close();
            }


            //Personal Profile
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spDisplayPersonalProfile", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                    //cmd.Parameters.AddWithValue("@Query", 18);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<EmpPersonalProfile> EmpPersonalProfiles = new List<EmpPersonalProfile>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        EmpPersonalProfile EmpPersonalProfile = new EmpPersonalProfile();

                        EmpPersonalProfile.EmployeeId = dset.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        EmpPersonalProfile.EmployeeName = dset.Tables[0].Rows[i]["EmployeeName"].ToString();
                        EmpPersonalProfile.Email = dset.Tables[0].Rows[i]["EmailAddress"].ToString();
                        EmpPersonalProfile.BloodType = dset.Tables[0].Rows[i]["BloodType"].ToString();
                        EmpPersonalProfile.HomeNum = dset.Tables[0].Rows[i]["HomeNumber"].ToString();
                        EmpPersonalProfile.MobileNum = dset.Tables[0].Rows[i]["Cellphone"].ToString();
                        EmpPersonalProfile.PermanentAdd = dset.Tables[0].Rows[i]["HomeAddress"].ToString();
                        EmpPersonalProfile.PresentAdd = dset.Tables[0].Rows[i]["CurrentAddress"].ToString();
                        EmpPersonalProfile.ProvincialAdd = dset.Tables[0].Rows[i]["ProvincialAddress"].ToString();
                        EmpPersonalProfile.PersonToNofify = dset.Tables[0].Rows[i]["PersonToNotify"].ToString();
                        EmpPersonalProfile.Relation = dset.Tables[0].Rows[i]["Relation"].ToString();
                        EmpPersonalProfile.ContactNum = dset.Tables[0].Rows[i]["ContactNum"].ToString();
                        EmpPersonalProfile.alreadyupdated = Convert.ToInt32(dset.Tables[0].Rows[i]["alreadyupdated"].ToString());

                        EmpPersonalProfiles.Add(EmpPersonalProfile);
                    }
                    EmpDisplayDetails.EmpProfile = EmpPersonalProfiles;
                }
                con.Close();
            }

            //General Information
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset1 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spDisplayLegalInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                    //cmd.Parameters.AddWithValue("@Query", 19);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset1);

                    List<EmpGeneralInformation> EmpGeneralInformations = new List<EmpGeneralInformation>();

                    for (int i = 0; i < dset1.Tables[0].Rows.Count; i++)
                    {
                        EmpGeneralInformation EmpGeneralInformation = new EmpGeneralInformation();

                                         
                        EmpGeneralInformation.intMstEmpPersonal = dset1.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();                       
                        EmpGeneralInformation.isYes1 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ1"].ToString());
                        EmpGeneralInformation.hasDetail1 = dset1.Tables[0].Rows[i]["DetailQ1"].ToString();
                        EmpGeneralInformation.isYes2 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ2"].ToString());
                        EmpGeneralInformation.hasDetail2 = dset1.Tables[0].Rows[i]["DetailQ2"].ToString();
                        EmpGeneralInformation.isYes3 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ3"].ToString());
                        EmpGeneralInformation.hasDetail3 = dset1.Tables[0].Rows[i]["DetailQ3"].ToString();
                        EmpGeneralInformation.isYes4 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ4"].ToString());
                        EmpGeneralInformation.hasDetail4 = dset1.Tables[0].Rows[i]["DetailQ4"].ToString();
                        EmpGeneralInformation.isYes5 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ5"].ToString());
                        EmpGeneralInformation.hasDetail5 = dset1.Tables[0].Rows[i]["DetailQ5"].ToString();
                        EmpGeneralInformation.isYes6 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ6"].ToString());
                        EmpGeneralInformation.hasDetail6 = dset1.Tables[0].Rows[i]["DetailQ6"].ToString();
                        EmpGeneralInformation.isYes7 = Convert.ToBoolean(dset1.Tables[0].Rows[i]["isYesQ7"].ToString());
                        EmpGeneralInformation.hasDetail7 = dset1.Tables[0].Rows[i]["DetailQ7"].ToString();
                        EmpGeneralInformation.alreadyupdated = Convert.ToInt32(dset1.Tables[0].Rows[i]["alreadyupdated"].ToString());

                        EmpGeneralInformations.Add(EmpGeneralInformation);
                    }
                    EmpDisplayDetails.EmpGenInfo = EmpGeneralInformations;
                }
                con.Close();
            }

            //Eduacational Attained
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset2 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spDisplayEducAttainment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                    //cmd.Parameters.AddWithValue("@Query", 20);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset2);

                    List<EmpEducationAttainment> EmpEducationAttainments = new List<EmpEducationAttainment>();

                    for (int i = 0; i < dset2.Tables[0].Rows.Count; i++)
                    {
                        EmpEducationAttainment EmpEducationAttainment = new EmpEducationAttainment();

                        EmpEducationAttainment.ID = Convert.ToInt32(dset2.Tables[0].Rows[i]["intMstEmpEducation"].ToString());
                        EmpEducationAttainment.intMstEmpPersonal = dset2.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        EmpEducationAttainment.EducationalAttained = dset2.Tables[0].Rows[i]["EducationalAttained"].ToString();
                        EmpEducationAttainment.NameOfSchool = dset2.Tables[0].Rows[i]["NameOfSchool"].ToString();
                        EmpEducationAttainment.Degree = dset2.Tables[0].Rows[i]["Degree"].ToString();
                        EmpEducationAttainment.DateFrom = dset2.Tables[0].Rows[i]["DateFrom"].ToString();
                        EmpEducationAttainment.DateTo = dset2.Tables[0].Rows[i]["DateTo"].ToString();
                        EmpEducationAttainment.Honors = dset2.Tables[0].Rows[i]["Honors"].ToString();
                        EmpEducationAttainment.alreadyupdated = Convert.ToInt32(dset2.Tables[0].Rows[i]["alreadyupdated"].ToString());

                        EmpEducationAttainments.Add(EmpEducationAttainment);
                    }
                    EmpDisplayDetails.EmpEducational = EmpEducationAttainments;
                }
                con.Close();
            }

            //Work Experience
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset3 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spDisplayWorkExerience", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                    //cmd.Parameters.AddWithValue("@Query", 21);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset3);


                    List<EmpWorkExperiences> EmpWorkExperiences = new List<EmpWorkExperiences>();

                    for (int i = 0; i < dset3.Tables[0].Rows.Count; i++)
                    {
                        EmpWorkExperiences EmpWorkExperience = new EmpWorkExperiences();

                        EmpWorkExperience.ID = Convert.ToInt32(dset3.Tables[0].Rows[i]["intMstEmpWorkExperience"].ToString());
                        EmpWorkExperience.intMstEmpPersonal = dset3.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        EmpWorkExperience.strDateEmployeed = dset3.Tables[0].Rows[i]["strDateEmloyed"].ToString();
                        EmpWorkExperience.strPosition = dset3.Tables[0].Rows[i]["strPosition"].ToString();
                        EmpWorkExperience.strEmployerName = dset3.Tables[0].Rows[i]["strEmployerName"].ToString();
                        //EmpWorkExperience.strEmploymentStatus = dset3.Tables[0].Rows[i]["strEmploymentStatus"].ToString();
                        EmpWorkExperience.EmployerContact = dset3.Tables[0].Rows[i]["EmployerContact"].ToString();
                        EmpWorkExperience.strReason = dset3.Tables[0].Rows[i]["strReason"].ToString();
                        EmpWorkExperience.alreadyupdated = Convert.ToInt32(dset3.Tables[0].Rows[i]["alreadyupdated"].ToString());

                        EmpWorkExperiences.Add(EmpWorkExperience);
                    }
                    EmpDisplayDetails.EmpWorkExp = EmpWorkExperiences;
                }
                con.Close();
            }

            //Training
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                 DataSet dset4 = new DataSet();
                 using (SqlCommand cmd = new SqlCommand("spDisplayTrainingAndSemenars", con))
                 {
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@EmployeeId", Id);
                     //cmd.Parameters.AddWithValue("@Query", 22);
                     con.Open();
                     SqlDataAdapter da = new SqlDataAdapter(cmd);
                     da.Fill(dset4);

                     List<EmpTrainingAndSemenars> EmpTrainingAndSemenars = new List<EmpTrainingAndSemenars>();
                     for (int i = 0; i < dset4.Tables[0].Rows.Count; i++)
                     {
                         EmpTrainingAndSemenars EmpTrainingAndSemenar = new EmpTrainingAndSemenars();

                         EmpTrainingAndSemenar.ID = Convert.ToInt32(dset4.Tables[0].Rows[i]["intMstEmpTraining"].ToString());
                         EmpTrainingAndSemenar.intMstEmpPersonal = dset4.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                         EmpTrainingAndSemenar.strTraining = dset4.Tables[0].Rows[i]["strTraining"].ToString();
                         EmpTrainingAndSemenar.strSponsor = dset4.Tables[0].Rows[i]["strSponsor"].ToString();
                         EmpTrainingAndSemenar.strDateTraining = dset4.Tables[0].Rows[i]["strDateTraining"].ToString();
                         EmpTrainingAndSemenar.Place = dset4.Tables[0].Rows[i]["Place"].ToString();
                         EmpTrainingAndSemenar.alreadyupdated = Convert.ToInt32(dset4.Tables[0].Rows[i]["alreadyupdated"].ToString());

                         EmpTrainingAndSemenars.Add(EmpTrainingAndSemenar);
                     }
                     EmpDisplayDetails.EmpTraining = EmpTrainingAndSemenars;
                 }
                 con.Close();
            }


            return View(EmpDisplayDetails);
        }
        
        //public ActionResult UpdateInformation(string EmployeeId, string EmployeeRecord, string LegalRecord, string EducValues, string WorkValues, string TrainingValues)
        //public ActionResult UpdateInformation(string[] EmpRecord, string[] LegalRecord, string[] EducRecord, string[] WorkRecord, string[] TrainRecord)    
        public ActionResult UpdateInformation(myStoreData myStore)
        
         {
             string Id = Session["intMstEmpPersonal"].ToString();

            Employee201BAL Emp201 = new Employee201BAL();
            string Result = Emp201.SaveMyDetails(myStore);

            if (Result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to Update!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Saved!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult myEmployee201Profile()
        {
            EmployeeUpdateLogs empUp = new EmployeeUpdateLogs();
            DataSet ds = new DataSet();

            string Id = Session["intMstEmpPersonal"].ToString();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {

                //spGetEmployeeUpdatedLogs
                using (SqlCommand cmd = new SqlCommand("spMyPersonalUpdateLogs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                    //cmd.Parameters.AddWithValue("@Query", 3);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<EmployeeUpdateLogs> EmpTwoOne = new List<EmployeeUpdateLogs>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeUpdateLogs emp = new EmployeeUpdateLogs();

                        emp.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        emp.EmployeeID = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                        emp.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                        emp.intIDModuleName = Convert.ToInt32(ds.Tables[0].Rows[i]["intIDModuleName"].ToString());
                        emp.ModuleName = ds.Tables[0].Rows[i]["ModuleName"].ToString();
                        emp.DateUpdated = ds.Tables[0].Rows[i]["DateInserted"].ToString();
                        emp.strStatus = ds.Tables[0].Rows[i]["Statuss"].ToString();

                        EmpTwoOne.Add(emp);
                    }
                    empUp.UpdateLogs = EmpTwoOne;
                }
                con.Close();
            }

            ViewBag.MyTitle = "Employee 201 Update Logs";
            return View(empUp);
        }

        public ActionResult CancelUpdate(string intITHead)
        {
            Employee201BAL Emp201 = new Employee201BAL();

            string result = Emp201.CancelUpdate(intITHead);


            if (result != "Cancel")
            {
                return Json(new { success = false, responseText = "Failed to Cancel!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Cancelled!" }, JsonRequestBehavior.AllowGet);
            }

            //return View();
        }

      // VIEW DETAILS 

        public ActionResult ViewDetails(string idt, string intModule)
        {
            EmpViewDetails EmpViewDetails = new EmpViewDetails();

     
            string Id = Session["intMstEmpPersonal"].ToString();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

               using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@intIThead", idt);
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                  
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<EmployeeProf> EmployeeProfs = new List<EmployeeProf>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeProf EmployeeProf = new EmployeeProf();

                        
                        EmployeeProf.EmployeeId = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                        EmployeeProf.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                        EmployeeProf.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        EmployeeProf.BloodType = ds.Tables[0].Rows[i]["BloodType"].ToString();
                        EmployeeProf.HomeNum = ds.Tables[0].Rows[i]["HomeNumber"].ToString();
                        EmployeeProf.MobileNum = ds.Tables[0].Rows[i]["MobileNumber"].ToString();
                        EmployeeProf.PermanentAdd = ds.Tables[0].Rows[i]["PermanentAddress"].ToString();
                        EmployeeProf.PresentAdd = ds.Tables[0].Rows[i]["PresentAddress"].ToString();
                        EmployeeProf.ProvincialAdd = ds.Tables[0].Rows[i]["ProvincialAddress"].ToString();
                        EmployeeProf.PersonToNofify = ds.Tables[0].Rows[i]["PersonNotify"].ToString();
                        EmployeeProf.Relation = ds.Tables[0].Rows[i]["Relation"].ToString();
                        EmployeeProf.ContactNum = ds.Tables[0].Rows[i]["ContactNumber"].ToString();
                        EmployeeProf.DateInserted = ds.Tables[0].Rows[i]["DateInserted"].ToString();

                       
                        EmployeeProfs.Add(EmployeeProf);
                    }
                    EmpViewDetails.EmployeeProf = EmployeeProfs;
                }
                con.Close();
            }


            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            //{
            //    DataSet ds = new DataSet();

            //    using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("@intIThead", idt);
            //        cmd.Parameters.AddWithValue("@EmployeeId", Id);
            //        cmd.Parameters.AddWithValue("@Query", 7);
            //        con.Open();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(ds);

            //        List<EmployeeProf> EmployeeProfs = new List<EmployeeProf>();

            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            EmployeeProf EmployeeProf = new EmployeeProf();

            //            EmployeeProf.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
            //            EmployeeProf.EmployeeId = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
            //            EmployeeProf.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
            //            EmployeeProf.Email = ds.Tables[0].Rows[i]["Email"].ToString();
            //            EmployeeProf.BloodType = ds.Tables[0].Rows[i]["BloodType"].ToString();
            //            EmployeeProf.HomeNum = ds.Tables[0].Rows[i]["HomeNumber"].ToString();
            //            EmployeeProf.MobileNum = ds.Tables[0].Rows[i]["MobileNumber"].ToString();
            //            EmployeeProf.PermanentAdd = ds.Tables[0].Rows[i]["PermanentAddress"].ToString();
            //            EmployeeProf.PresentAdd = ds.Tables[0].Rows[i]["PresentAddress"].ToString();
            //            EmployeeProf.ProvincialAdd = ds.Tables[0].Rows[i]["ProvincialAddress"].ToString();
            //            EmployeeProf.PersonToNofify = ds.Tables[0].Rows[i]["PersonNotify"].ToString();
            //            EmployeeProf.Relation = ds.Tables[0].Rows[i]["Relation"].ToString();
            //            EmployeeProf.ContactNum = ds.Tables[0].Rows[i]["ContactNumber"].ToString();
            //            EmployeeProf.DateInserted = ds.Tables[0].Rows[i]["DateInserted"].ToString();

                       
            //            EmployeeProfs.Add(EmployeeProf);
            //        }
            //        EmpViewDetails.EmployeeProf = EmployeeProfs;
            //    }
            //    con.Close();
            //}

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            //{
            //    DataSet ds4 = new DataSet();

            //    using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@intIThead", idt);
            //        cmd.Parameters.AddWithValue("@EmployeeId", Id);
            //        cmd.Parameters.AddWithValue("@Query", 11);
            //        con.Open();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(ds4);

            //        List<GeneralInfo> GeneralInfo = new List<GeneralInfo>();

            //        for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            //        {
            //            GeneralInfo genInfos = new GeneralInfo();

            //            genInfos.ID = Convert.ToInt32(ds4.Tables[0].Rows[i]["ID"].ToString());
            //            genInfos.intITHeader = Convert.ToInt32(ds4.Tables[0].Rows[i]["intITHeader"].ToString());
            //            genInfos.intMstEmpPersonal = ds4.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
            //            genInfos.isYes1 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ1"].ToString());
            //            genInfos.hasDetail1 = ds4.Tables[0].Rows[i]["DetailQ1"].ToString();
            //            genInfos.isYes2 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ2"].ToString());
            //            genInfos.hasDetail2 = ds4.Tables[0].Rows[i]["DetailQ2"].ToString();
            //            genInfos.isYes3 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ3"].ToString());
            //            genInfos.hasDetail3 = ds4.Tables[0].Rows[i]["DetailQ3"].ToString();
            //            genInfos.isYes4 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ4"].ToString());
            //            genInfos.hasDetail4 = ds4.Tables[0].Rows[i]["DetailQ4"].ToString();
            //            genInfos.isYes5 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ5"].ToString());
            //            genInfos.hasDetail5 = ds4.Tables[0].Rows[i]["DetailQ5"].ToString();
            //            genInfos.isYes6 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ6"].ToString());
            //            genInfos.hasDetail6 = ds4.Tables[0].Rows[i]["DetailQ6"].ToString();
            //            genInfos.isYes7 = Convert.ToInt32(ds4.Tables[0].Rows[i]["isYesQ7"].ToString());
            //            genInfos.hasDetail7 = ds4.Tables[0].Rows[i]["DetailQ7"].ToString();

            //            GeneralInfo.Add(genInfos);
            //        }
            //        EmpViewDetails.GeneralInformation = GeneralInfo;
            //    }
            //    con.Close();
            //}
           

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            //{
            //    DataSet ds1 = new DataSet();
            //    using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@intIThead", idt);
            //        cmd.Parameters.AddWithValue("@EmployeeId", Id);
            //        cmd.Parameters.AddWithValue("@Query", 8);
            //        con.Open();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(ds1);

            //        List<EducationalAttainment> EducationalAttainments = new List<EducationalAttainment>();

            //        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //        {
            //            EducationalAttainment EducationalAttainment = new EducationalAttainment();

            //            EducationalAttainment.ID = Convert.ToInt32(ds1.Tables[0].Rows[i]["ID"].ToString());
            //            EducationalAttainment.intITHeader = ds1.Tables[0].Rows[i]["intITHeader"].ToString();
            //            EducationalAttainment.intMstEmpPersonal = ds1.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
            //            EducationalAttainment.EducationalAttained = ds1.Tables[0].Rows[i]["EducationalAttained"].ToString();
            //            EducationalAttainment.NameOfSchool = ds1.Tables[0].Rows[i]["NameOfSchool"].ToString();
            //            EducationalAttainment.Degree = ds1.Tables[0].Rows[i]["Degree"].ToString();
            //            EducationalAttainment.YearAttended = ds1.Tables[0].Rows[i]["YearAttended"].ToString();
            //            EducationalAttainment.Honors = ds1.Tables[0].Rows[i]["Honors"].ToString();

            //            EducationalAttainments.Add(EducationalAttainment);
            //        }
            //        EmpViewDetails.Educ_attained = EducationalAttainments;
            //    }
            //    con.Close();
            //}

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            //{
            //    DataSet ds2 = new DataSet();
            //    using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@intIThead", idt);
            //        cmd.Parameters.AddWithValue("@EmployeeId", Id);
            //        cmd.Parameters.AddWithValue("@Query", 9);
            //        con.Open();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(ds2);

            //        List<WorkExperience> WorkExperiences = new List<WorkExperience>();

            //        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            //        {
            //            WorkExperience WorkExperience = new WorkExperience();

            //            WorkExperience.ID = Convert.ToInt32(ds2.Tables[0].Rows[i]["ID"].ToString());
            //            WorkExperience.intITHeader = Convert.ToInt32(ds2.Tables[0].Rows[i]["intITHeader"].ToString());
            //            WorkExperience.intMstEmpPersonal = ds2.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
            //            WorkExperience.strDateEmployeed = ds2.Tables[0].Rows[i]["strDateEmloyed"].ToString();
            //            WorkExperience.strPosition = ds2.Tables[0].Rows[i]["strPosition"].ToString();
            //            WorkExperience.strEmployerName = ds2.Tables[0].Rows[i]["strEmployerName"].ToString();
            //            WorkExperience.strEmploymentStatus = ds2.Tables[0].Rows[i]["strEmploymentStatus"].ToString();
            //            WorkExperience.strReason = ds2.Tables[0].Rows[i]["strReason"].ToString();

            //            WorkExperiences.Add(WorkExperience);
            //        }
            //        EmpViewDetails.Work_Exp = WorkExperiences;
            //    }
            //    con.Close();
            //}

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            //{
            //    DataSet ds3 = new DataSet();
            //    using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@intIThead", idt);
            //        cmd.Parameters.AddWithValue("@EmployeeId", Id);
            //        cmd.Parameters.AddWithValue("@Query", 10);
            //        con.Open();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(ds3);

            //        List<TrainingAndSeminars> TrainingAndSeminars = new List<TrainingAndSeminars>();

            //        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            //        {
            //            TrainingAndSeminars TrainingAndSeminar = new TrainingAndSeminars();

            //            TrainingAndSeminar.ID = Convert.ToInt32(ds3.Tables[0].Rows[i]["ID"].ToString());
            //            TrainingAndSeminar.intITHeader = Convert.ToInt32(ds3.Tables[0].Rows[i]["intITHeader"].ToString());
            //            TrainingAndSeminar.intMstEmpPersonal = ds3.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
            //            TrainingAndSeminar.strTraining = ds3.Tables[0].Rows[i]["strTraining"].ToString();
            //            TrainingAndSeminar.strSponsor = ds3.Tables[0].Rows[i]["strSponsor"].ToString();
            //            TrainingAndSeminar.strDateTraining = ds3.Tables[0].Rows[i]["strDateTraining"].ToString();
            //            TrainingAndSeminar.Place = ds3.Tables[0].Rows[i]["Place"].ToString();

            //            TrainingAndSeminars.Add(TrainingAndSeminar);
            //        }
            //        EmpViewDetails.Train_Sem = TrainingAndSeminars;
            //    }
            //    con.Close();
            //}
            

            //Employee201BAL Emp201 = new Employee201BAL();
            //string  ds = Emp201.ViewDetails(intITheader);

            ViewBag.MyTitle = "View Details";

            return View(EmpViewDetails);

        }
        //END OF VIEW DETAILS 


        public ActionResult EmployeeTwoOhOneforApproval()
        {
           // DisplayForApproval forApproval = new DisplayForApproval();
            DisplayListOfApprovals ListOfApprovals = new DisplayListOfApprovals();

            DataSet ds = new DataSet();
            string Id = Session["intMstEmpPersonal"].ToString();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayForApproval", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<DisplayForApproval> forApprovals = new List<DisplayForApproval>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DisplayForApproval forApprove = new DisplayForApproval();

                        forApprove.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        forApprove.DateFiled = ds.Tables[0].Rows[i]["DateInserted"].ToString();
                        forApprove.EmployeeId = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                        forApprove.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                        forApprove.intIDModuleName = Convert.ToInt32(ds.Tables[0].Rows[i]["intIDModuleName"].ToString());
                        forApprove.strModuleName = ds.Tables[0].Rows[i]["ModuleName"].ToString();
                        forApprove.intMstPosition = Convert.ToInt32(ds.Tables[0].Rows[i]["intMstPosition"].ToString());
                        forApprove.PositionName = ds.Tables[0].Rows[i]["PositionName"].ToString();
                        forApprove.intMstBranch = Convert.ToInt32(ds.Tables[0].Rows[i]["intMstBranch"].ToString());
                        forApprove.BranchName = ds.Tables[0].Rows[i]["BranchName"].ToString();
                        forApprove.intMstDepartment = Convert.ToInt32(ds.Tables[0].Rows[i]["intMstDepartment"].ToString());
                        forApprove.DepartmentName = ds.Tables[0].Rows[i]["DepartmentName"].ToString();

                        forApprovals.Add(forApprove);
                    }
                    ListOfApprovals.DisplayForApproval = forApprovals;
                }
                con.Close();
            }

            DataSet dds = new DataSet();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayApprovedAndDisApproved", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", Id);                 
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dds);

                    List<DisplayApprovedAndDisApproved> DisplayApprovedAndDisApproveds = new List<DisplayApprovedAndDisApproved>();

                    for (int i = 0; i < dds.Tables[0].Rows.Count; i++)
                    {
                        DisplayApprovedAndDisApproved DisplayApprovedAndDisApproved = new DisplayApprovedAndDisApproved();

                        DisplayApprovedAndDisApproved.ID = Convert.ToInt32(dds.Tables[0].Rows[i]["ID"].ToString());
                        DisplayApprovedAndDisApproved.DateFiled = dds.Tables[0].Rows[i]["DateFiled"].ToString();
                        DisplayApprovedAndDisApproved.EmployeeID = dds.Tables[0].Rows[i]["EmployeeID"].ToString();
                        DisplayApprovedAndDisApproved.EmployeeName = dds.Tables[0].Rows[i]["EmployeeName"].ToString();
                        DisplayApprovedAndDisApproved.ModuleName = dds.Tables[0].Rows[i]["ModuleName"].ToString();
                        DisplayApprovedAndDisApproved.PosistionName = dds.Tables[0].Rows[i]["PositionName"].ToString();
                        DisplayApprovedAndDisApproved.BranchName = dds.Tables[0].Rows[i]["BranchName"].ToString();
                        DisplayApprovedAndDisApproved.DepartmentName = dds.Tables[0].Rows[i]["DepartmentName"].ToString();
                        DisplayApprovedAndDisApproved.strStatus = dds.Tables[0].Rows[i]["strStatus"].ToString();
                        DisplayApprovedAndDisApproved.DateApprovedAndDisApproved = dds.Tables[0].Rows[i]["DateApproveAndDisApprove"].ToString();

                        DisplayApprovedAndDisApproveds.Add(DisplayApprovedAndDisApproved);
                    }
                    ListOfApprovals.ApprovedAndDisApproved = DisplayApprovedAndDisApproveds;
                }
                con.Close();
            }

            ViewBag.MyTitle = "Employee 201 for Approval";
            return View(ListOfApprovals);
        }

        public ActionResult ApproveUpdate(string Details)
        {
            //string strMessage = string.Empty;
            //bool success = false;
            //bool isHRs = (bool)Session["emp_isHRRegional"];
            string Id = Session["intMstEmpPersonal"].ToString();


            Employee201BAL Emp201 = new Employee201BAL();

            string Result = Emp201.ApproveUpdate(Details, Id);

            if (Result != "Update")
            {
                return Json(new { success = false, responseText = "Failed to Approved!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Approved!" }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { success = success.ToString(), msg = strMessage });
        }

        public ActionResult DisApproveEmployeeUpdate(string Details, string Reason)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string Id = Session["intMstEmpPersonal"].ToString();

            string Result = Emp201.DisApproveEmployeeUpdate(Details, Id, Reason);

            if (Result != "disapprove")
            {
                return Json(new { success = false, responseText = "Failed to Approved!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Disapproved!" }, JsonRequestBehavior.AllowGet);
            }
        }
        //not used
        public ActionResult ViewDetailsApproval(string intID, string EmployeeID)
        {
            EmpViewDetailsForApproval ViewDetailApproval = new EmpViewDetailsForApproval();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@intID", intID);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    cmd.Parameters.AddWithValue("@Query", 12);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<EmployeeProfs> EmployeeProfiles = new List<EmployeeProfs>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        EmployeeProfs EmployeeProfile = new EmployeeProfs();

                        EmployeeProfile.ID = Convert.ToInt32(DatSet.Tables[0].Rows[i]["ID"].ToString());
                        EmployeeProfile.EmployeeId = DatSet.Tables[0].Rows[i]["EmployeeID"].ToString();
                        EmployeeProfile.EmployeeName = DatSet.Tables[0].Rows[i]["EmployeeName"].ToString();
                        EmployeeProfile.Email = DatSet.Tables[0].Rows[i]["Email"].ToString();
                        EmployeeProfile.BloodType = DatSet.Tables[0].Rows[i]["BloodType"].ToString();
                        EmployeeProfile.HomeNum = DatSet.Tables[0].Rows[i]["HomeNumber"].ToString();
                        EmployeeProfile.MobileNum = DatSet.Tables[0].Rows[i]["MobileNumber"].ToString();
                        EmployeeProfile.PermanentAdd = DatSet.Tables[0].Rows[i]["PermanentAddress"].ToString();
                        EmployeeProfile.PresentAdd = DatSet.Tables[0].Rows[i]["PresentAddress"].ToString();
                        EmployeeProfile.ProvincialAdd = DatSet.Tables[0].Rows[i]["ProvincialAddress"].ToString();
                        EmployeeProfile.PersonToNofify = DatSet.Tables[0].Rows[i]["PersonNotify"].ToString();
                        EmployeeProfile.Relation = DatSet.Tables[0].Rows[i]["Relation"].ToString();
                        EmployeeProfile.ContactNum = DatSet.Tables[0].Rows[i]["ContactNumber"].ToString();
                        EmployeeProfile.DateInserted = DatSet.Tables[0].Rows[i]["DateInserted"].ToString();


                        EmployeeProfiles.Add(EmployeeProfile);
                    }
                    ViewDetailApproval.EmployeeProf = EmployeeProfiles;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet1 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intID", intID);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    cmd.Parameters.AddWithValue("@Query", 13);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet1);

                    List<GeneralInfos> GeneralInformations = new List<GeneralInfos>();

                    for (int i = 0; i < DatSet1.Tables[0].Rows.Count; i++)
                    {
                        GeneralInfos GeneralInformation = new GeneralInfos();

                        GeneralInformation.ID = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["ID"].ToString());                      
                        GeneralInformation.intMstEmpPersonal = DatSet1.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        GeneralInformation.isYes1 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ1"].ToString());
                        GeneralInformation.hasDetail1 = DatSet1.Tables[0].Rows[i]["DetailQ1"].ToString();
                        GeneralInformation.isYes2 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ2"].ToString());
                        GeneralInformation.hasDetail2 = DatSet1.Tables[0].Rows[i]["DetailQ2"].ToString();
                        GeneralInformation.isYes3 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ3"].ToString());
                        GeneralInformation.hasDetail3 = DatSet1.Tables[0].Rows[i]["DetailQ3"].ToString();
                        GeneralInformation.isYes4 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ4"].ToString());
                        GeneralInformation.hasDetail4 = DatSet1.Tables[0].Rows[i]["DetailQ4"].ToString();
                        GeneralInformation.isYes5 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ5"].ToString());
                        GeneralInformation.hasDetail5 = DatSet1.Tables[0].Rows[i]["DetailQ5"].ToString();
                        GeneralInformation.isYes6 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ6"].ToString());
                        GeneralInformation.hasDetail6 = DatSet1.Tables[0].Rows[i]["DetailQ6"].ToString();
                        GeneralInformation.isYes7 = Convert.ToInt32(DatSet1.Tables[0].Rows[i]["isYesQ7"].ToString());
                        GeneralInformation.hasDetail7 = DatSet1.Tables[0].Rows[i]["DetailQ7"].ToString();

                        GeneralInformations.Add(GeneralInformation);
                    }
                    ViewDetailApproval.GeneralInformation = GeneralInformations;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet2 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intID", intID);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    cmd.Parameters.AddWithValue("@Query", 14);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet2);

                    List<EducationalAttainments> EducationalAttainments = new List<EducationalAttainments>();

                    for (int i = 0; i < DatSet2.Tables[0].Rows.Count; i++)
                    {
                        EducationalAttainments EducationalAttainment = new EducationalAttainments();

                        EducationalAttainment.ID = Convert.ToInt32(DatSet2.Tables[0].Rows[i]["ID"].ToString());
                        EducationalAttainment.intMstEmpPersonal = DatSet2.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        EducationalAttainment.EducationalAttained = DatSet2.Tables[0].Rows[i]["EducationalAttained"].ToString();
                        EducationalAttainment.NameOfSchool = DatSet2.Tables[0].Rows[i]["NameOfSchool"].ToString();
                        EducationalAttainment.Degree = DatSet2.Tables[0].Rows[i]["Degree"].ToString();
                        EducationalAttainment.DateFrom = DatSet2.Tables[0].Rows[i]["DateFrom"].ToString();
                        EducationalAttainment.DateTo = DatSet2.Tables[0].Rows[i]["DateTo"].ToString();
                        EducationalAttainment.Honors = DatSet2.Tables[0].Rows[i]["Honors"].ToString();

                        EducationalAttainments.Add(EducationalAttainment);
                    }
                    ViewDetailApproval.Educ_attained = EducationalAttainments;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet3 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intID", intID);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    cmd.Parameters.AddWithValue("@Query", 15);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet3);

                    List<WorkExperiences> WorkExperiences = new List<WorkExperiences>();

                    for (int i = 0; i < DatSet3.Tables[0].Rows.Count; i++)
                    {
                        WorkExperiences WorkExperience = new WorkExperiences();

                        WorkExperience.ID = Convert.ToInt32(DatSet3.Tables[0].Rows[i]["ID"].ToString());                       
                        WorkExperience.intMstEmpPersonal = DatSet3.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        WorkExperience.strDateEmployeed = DatSet3.Tables[0].Rows[i]["strDateEmloyed"].ToString();
                        WorkExperience.strPosition = DatSet3.Tables[0].Rows[i]["strPosition"].ToString();
                        WorkExperience.strEmployerName = DatSet3.Tables[0].Rows[i]["strEmployerName"].ToString();                        
                        WorkExperience.strEmployerContactNo = DatSet3.Tables[0].Rows[i]["strEmploymentStatus"].ToString();
                        WorkExperience.strReason = DatSet3.Tables[0].Rows[i]["strReason"].ToString();
                        

                        WorkExperiences.Add(WorkExperience);
                    }

                    ViewDetailApproval.Work_Exp = WorkExperiences;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet4 = new DataSet();
                using (SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intID", intID);
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeeID);
                    cmd.Parameters.AddWithValue("@Query", 16);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet4);

                    List<TrainingAndSeminar> TrainingAndSeminars = new List<TrainingAndSeminar>();

                    for (int i = 0; i < DatSet4.Tables[0].Rows.Count; i++)
                    {
                        TrainingAndSeminar TrainingAndSeminar = new TrainingAndSeminar();

                        TrainingAndSeminar.ID = Convert.ToInt32(DatSet4.Tables[0].Rows[i]["ID"].ToString());                      
                        TrainingAndSeminar.intMstEmpPersonal = DatSet4.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        TrainingAndSeminar.strTraining = DatSet4.Tables[0].Rows[i]["strTraining"].ToString();
                        TrainingAndSeminar.strSponsor = DatSet4.Tables[0].Rows[i]["strSponsor"].ToString();
                        TrainingAndSeminar.strDateTraining = DatSet4.Tables[0].Rows[i]["strDateTraining"].ToString();
                        TrainingAndSeminar.Place = DatSet4.Tables[0].Rows[i]["Place"].ToString();

                        TrainingAndSeminars.Add(TrainingAndSeminar);
                    }
                    ViewDetailApproval.Train_Sem = TrainingAndSeminars;
                }
                con.Close();
            }


            ViewBag.MyTitle = "View Details";
            return View(ViewDetailApproval);
        }

        
        
        public JsonResult UpdateEducRecord(string values, string intMstEmpPersonal)
        {
            Employee201BAL Emp201 = new Employee201BAL();

            string ID  = Session["intMstEmpPersonal"].ToString();

            string result = Emp201.UpdateEducRecord(values, intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to Update your Educational Experience!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your educational experience!" }, JsonRequestBehavior.AllowGet);
            }
  
        }

        public ActionResult DelEducRecord(string ID, string intMstEmpPersonal)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string result = Emp201.DelEducRecord(ID, intMstEmpPersonal);


            if (result != "Deleted")
            {
                return Json(new { success = false, responseText = "Failed to deleted your educational experience!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully deleted your educational experience!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdatePersonalRecord(EmpPersonalProfile PersonalRecord)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string result = Emp201.UpdatePersonalRecord(PersonalRecord);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your personal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your personal information!" }, JsonRequestBehavior.AllowGet);
            }

        }
        
        public ActionResult UpdateLegalInfoRecord(EmpGeneralInformation LegalRecord)
        {
            Employee201BAL Emp201 = new Employee201BAL();

            string result = Emp201.UpdateLegalInfoRecord(LegalRecord);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your legal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your legal information!" }, JsonRequestBehavior.AllowGet);
            }


         
        }
        
        
        public ActionResult UpdateWorkExpRecord(string values, string intMstEmpPersonal)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string result = Emp201.UpdateWorkExpRecord(values, intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your work experience!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your work experience!" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelWorkExp(string ID, string intMstEmpPersonal)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string result = Emp201.DelWorkExp(ID, intMstEmpPersonal);

            if (result != "Deleted")
            {
                return Json(new { success = false, responseText = "Failed to Delete your Work Experience!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Update your Work Experience!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateTrainingAndSemenarsRecord(string values, string intMstEmpPersonal)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string result = Emp201.UpdateTrainingAndSemenarsRecord(values,intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your training and seminars!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your training and seminars!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelTrainingRecord(string ID, string intMstEmpPersonal)
        {
            Employee201BAL Emp201 = new Employee201BAL();
            string result = Emp201.DelTrainingRecord(ID, intMstEmpPersonal);

            if (result != "Deleted")
            {
                return Json(new { success = false, responseText = "Failed to Delete your Legal Information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Successfully Deleted your Legal Information!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewDetailLogs(string idt, string intModule)
        {
            EmpViewDetails EmpViewDetails = new EmpViewDetails();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailPersonalInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<EmployeeProf> EmployeeProfs = new List<EmployeeProf>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeProf EmployeeProf = new EmployeeProf();


                        EmployeeProf.EmployeeId = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                        EmployeeProf.EmployeeName = ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                        EmployeeProf.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        EmployeeProf.BloodType = ds.Tables[0].Rows[i]["BloodType"].ToString();
                        EmployeeProf.HomeNum = ds.Tables[0].Rows[i]["HomeNumber"].ToString();
                        EmployeeProf.MobileNum = ds.Tables[0].Rows[i]["MobileNumber"].ToString();
                        EmployeeProf.PermanentAdd = ds.Tables[0].Rows[i]["PermanentAddress"].ToString();
                        EmployeeProf.PresentAdd = ds.Tables[0].Rows[i]["PresentAddress"].ToString();
                        EmployeeProf.ProvincialAdd = ds.Tables[0].Rows[i]["ProvincialAddress"].ToString();
                        EmployeeProf.PersonToNofify = ds.Tables[0].Rows[i]["PersonNotify"].ToString();
                        EmployeeProf.Relation = ds.Tables[0].Rows[i]["Relation"].ToString();
                        EmployeeProf.ContactNum = ds.Tables[0].Rows[i]["ContactNumber"].ToString();

                        EmployeeProf.FirstLevelSuperior = ds.Tables[0].Rows[i]["FirstLevelSuperior"].ToString();
                        EmployeeProf.strFirstSuperior = ds.Tables[0].Rows[i]["strFirstSuperior"].ToString();
                        EmployeeProf.isApprovedFirst = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedFirst"].ToString());
                            //ds.Tables[0].Rows[i]["isApprovedFirst"].ToString();
                        EmployeeProf.SecondLevelSuperior = ds.Tables[0].Rows[i]["SecondLevelSuperior"].ToString();
                        EmployeeProf.strSecondSuperior = ds.Tables[0].Rows[i]["strSecondSuperior"].ToString();
                        EmployeeProf.isApprovedSecond = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedSecond"].ToString());
                            //ds.Tables[0].Rows[i]["isApprovedSecond"].ToString();                          
                        EmployeeProf.DisApproveReason = ds.Tables[0].Rows[i]["DisApproveReason"].ToString();


                        EmployeeProfs.Add(EmployeeProf);
                    }
                    EmpViewDetails.EmployeeProf = EmployeeProfs;
                }
                con.Close();
            }

            ViewBag.MyTitle = "View Details";

            return View(EmpViewDetails);
        }

        public ActionResult ViewDetailLogsLegal(string idt, string intModule)
        {
            EmpViewDetails EmpViewDetails = new EmpViewDetails();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailLegalInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<GeneralInfo> GeneralInfos = new List<GeneralInfo>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GeneralInfo GeneralInfo = new GeneralInfo();

                        GeneralInfo.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        GeneralInfo.intMstEmpPersonal = ds.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        GeneralInfo.isYes1 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ1"].ToString());
                        GeneralInfo.hasDetail1 = ds.Tables[0].Rows[i]["DetailQ1"].ToString();
                        GeneralInfo.isYes2 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ2"].ToString());
                        GeneralInfo.hasDetail2 = ds.Tables[0].Rows[i]["DetailQ2"].ToString();
                        GeneralInfo.isYes3 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ3"].ToString());
                        GeneralInfo.hasDetail3 = ds.Tables[0].Rows[i]["DetailQ3"].ToString();
                        GeneralInfo.isYes4 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ4"].ToString());
                        GeneralInfo.hasDetail4 = ds.Tables[0].Rows[i]["DetailQ4"].ToString();
                        GeneralInfo.isYes5 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ5"].ToString());
                        GeneralInfo.hasDetail5 = ds.Tables[0].Rows[i]["DetailQ5"].ToString();
                        GeneralInfo.isYes6 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ6"].ToString());
                        GeneralInfo.hasDetail6 = ds.Tables[0].Rows[i]["DetailQ6"].ToString();
                        GeneralInfo.isYes7 = Convert.ToInt32(ds.Tables[0].Rows[i]["isYesQ7"].ToString());
                        GeneralInfo.hasDetail7 = ds.Tables[0].Rows[i]["DetailQ7"].ToString();

                        GeneralInfo.FirstLevelSuperior = ds.Tables[0].Rows[i]["FirstLevelSuperior"].ToString();
                        GeneralInfo.strFirstSuperior = ds.Tables[0].Rows[i]["strFirstSuperior"].ToString();
                        GeneralInfo.isApprovedFirst = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedFirst"].ToString());
                        GeneralInfo.SecondLevelSuperior = ds.Tables[0].Rows[i]["SecondLevelSuperior"].ToString();
                        GeneralInfo.strSecondSuperior = ds.Tables[0].Rows[i]["strSecondSuperior"].ToString();
                        GeneralInfo.isApprovedSecond = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedSecond"].ToString());
                        GeneralInfo.DisApproveReason = ds.Tables[0].Rows[i]["DisApproveReason"].ToString();

                        GeneralInfos.Add(GeneralInfo);
                    }
                    EmpViewDetails.GeneralInformation = GeneralInfos;
                }
                con.Close();
            }

            return View(EmpViewDetails);
        }

        public ActionResult ViewDetailEducAttained(string idt, string intModule)
        {
            EmpViewDetails EmpViewDetails = new EmpViewDetails();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailEducAttained", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<EducationalAttainment> EducationalAttainments = new List<EducationalAttainment>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EducationalAttainment EducationalAttainment = new EducationalAttainment();

                        EducationalAttainment.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        EducationalAttainment.intMstEmpPersonal = ds.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        EducationalAttainment.EducationalAttained = ds.Tables[0].Rows[i]["EducationalAttained"].ToString();
                        EducationalAttainment.NameOfSchool = ds.Tables[0].Rows[i]["NameOfSchool"].ToString();
                        EducationalAttainment.Degree = ds.Tables[0].Rows[i]["Degree"].ToString();
                        EducationalAttainment.DateFrom = ds.Tables[0].Rows[i]["DateFrom"].ToString();
                        EducationalAttainment.DateTo = ds.Tables[0].Rows[i]["DateTo"].ToString();
                        EducationalAttainment.Honors = ds.Tables[0].Rows[i]["Honors"].ToString();

                       
                        
                        EducationalAttainments.Add(EducationalAttainment);
                    }
                    EmpViewDetails.Educ_attained = EducationalAttainments;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spEducationalReason", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<EducationalReason> EducationalReasons = new List<EducationalReason>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EducationalReason EducationalReason = new EducationalReason();

                        EducationalReason.FirstLevelSuperior = ds.Tables[0].Rows[i]["FirstLevelSuperior"].ToString();
                        EducationalReason.strFirstSuperior = ds.Tables[0].Rows[i]["strFirstSuperior"].ToString();
                        EducationalReason.isApprovedFirst = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedFirst"].ToString());
                        EducationalReason.SecondLevelSuperior = ds.Tables[0].Rows[i]["SecondLevelSuperior"].ToString();
                        EducationalReason.strSecondSuperior = ds.Tables[0].Rows[i]["strSecondSuperior"].ToString();
                        EducationalReason.isApprovedSecond = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedSecond"].ToString());
                        EducationalReason.DisApproveReason = ds.Tables[0].Rows[i]["DisApproveReason"].ToString();

                        EducationalReasons.Add(EducationalReason);
                    }
                    EmpViewDetails.EducReason = EducationalReasons;
                }
                con.Close();
            }

            return View(EmpViewDetails);
        }

        public ActionResult ViewDetailWorkExp(string idt, string intModule)
        {
            EmpViewDetails EmpViewDetails = new EmpViewDetails();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailWorkExp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<WorkExperience> WorkExperiences = new List<WorkExperience>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        WorkExperience WorkExperience = new WorkExperience();

                        WorkExperience.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        WorkExperience.intMstEmpPersonal = ds.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        WorkExperience.strDateEmployeed = ds.Tables[0].Rows[i]["strDateEmloyed"].ToString();
                        WorkExperience.strPosition = ds.Tables[0].Rows[i]["strPosition"].ToString();
                        WorkExperience.strEmployerName = ds.Tables[0].Rows[i]["strEmployerName"].ToString();
                        WorkExperience.strEmployerContactNum = ds.Tables[0].Rows[i]["EmployerContact"].ToString();
                        WorkExperience.strReason = ds.Tables[0].Rows[i]["strReason"].ToString();                     

                        WorkExperiences.Add(WorkExperience);
                    }
                    EmpViewDetails.Work_Exp = WorkExperiences;
                }

                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewWorkReason", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<WorkReason> WorkReasons = new List<WorkReason>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        WorkReason WorkReason = new WorkReason();

                        WorkReason.FirstLevelSuperior = ds.Tables[0].Rows[i]["FirstLevelSuperior"].ToString();
                        WorkReason.strFirstSuperior = ds.Tables[0].Rows[i]["strFirstSuperior"].ToString();
                        WorkReason.isApprovedFirst = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedFirst"].ToString());
                        WorkReason.SecondLevelSuperior = ds.Tables[0].Rows[i]["SecondLevelSuperior"].ToString();
                        WorkReason.strSecondSuperior = ds.Tables[0].Rows[i]["strSecondSuperior"].ToString();
                        WorkReason.isApprovedSecond = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedSecond"].ToString());
                        WorkReason.DisApproveReason = ds.Tables[0].Rows[i]["DisApproveReason"].ToString();

                        WorkReasons.Add(WorkReason);
                    }
                    EmpViewDetails.WorkReason = WorkReasons;
                }

                con.Close();
            }

            return View(EmpViewDetails);
        }

        public ActionResult ViewDetailTrainingExp(string idt, string intModule)
        {
            EmpViewDetails EmpViewDetails = new EmpViewDetails();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailTrainingExp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<TrainingAndSeminars> TrainingAndSeminars = new List<TrainingAndSeminars>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TrainingAndSeminars TrainingAndSeminar = new TrainingAndSeminars();

                        TrainingAndSeminar.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        TrainingAndSeminar.intMstEmpPersonal = ds.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        TrainingAndSeminar.strTraining = ds.Tables[0].Rows[i]["strTraining"].ToString();
                        TrainingAndSeminar.strSponsor = ds.Tables[0].Rows[i]["strSponsor"].ToString();
                        TrainingAndSeminar.strDateTraining = ds.Tables[0].Rows[i]["strDateTraining"].ToString();
                        TrainingAndSeminar.Place = ds.Tables[0].Rows[i]["Place"].ToString();

                        TrainingAndSeminars.Add(TrainingAndSeminar);
                    }
                    EmpViewDetails.Train_Sem = TrainingAndSeminars;
                }
                con.Close();
            }


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet ds = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewTrainingReason", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", idt);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    List<TrainingAndSeminarsReason> TrainingAndSeminarsReasons = new List<TrainingAndSeminarsReason>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TrainingAndSeminarsReason TrainingAndSeminarsReason = new TrainingAndSeminarsReason();

                        TrainingAndSeminarsReason.FirstLevelSuperior = ds.Tables[0].Rows[i]["FirstLevelSuperior"].ToString();
                        TrainingAndSeminarsReason.strFirstSuperior = ds.Tables[0].Rows[i]["strFirstSuperior"].ToString();
                        TrainingAndSeminarsReason.isApprovedFirst = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedFirst"].ToString());
                            //ds.Tables[0].Rows[i]["isApprovedFirst"].ToString();
                        TrainingAndSeminarsReason.SecondLevelSuperior = ds.Tables[0].Rows[i]["SecondLevelSuperior"].ToString();
                        TrainingAndSeminarsReason.strSecondSuperior = ds.Tables[0].Rows[i]["strSecondSuperior"].ToString();
                        TrainingAndSeminarsReason.isApprovedSecond = Convert.ToInt32(ds.Tables[0].Rows[i]["isApprovedSecond"].ToString());
                            //ds.Tables[0].Rows[i]["isApprovedSecond"].ToString(); 
                        TrainingAndSeminarsReason.DisApproveReason = ds.Tables[0].Rows[i]["DisApproveReason"].ToString();
                        TrainingAndSeminarsReasons.Add(TrainingAndSeminarsReason);
                    }
                    EmpViewDetails.TrainingReason = TrainingAndSeminarsReasons;
                }
                con.Close();
            }

            return View(EmpViewDetails);
        }

        public ActionResult ViewDetailApprovalPersonal(string intID) 
        {
            EmpViewDetailsForApproval ViewDetailApproval = new EmpViewDetailsForApproval();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                 using (SqlCommand cmd = new SqlCommand("spViewDetailPersonalInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", intID);
                          
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<EmployeeProfs> EmployeeProfiles = new List<EmployeeProfs>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        EmployeeProfs EmployeeProfile = new EmployeeProfs();

                        EmployeeProfile.EmployeeId = DatSet.Tables[0].Rows[i]["EmployeeID"].ToString();
                        EmployeeProfile.EmployeeName = DatSet.Tables[0].Rows[i]["EmployeeName"].ToString();
                        EmployeeProfile.Email = DatSet.Tables[0].Rows[i]["Email"].ToString();
                        EmployeeProfile.BloodType = DatSet.Tables[0].Rows[i]["BloodType"].ToString();
                        EmployeeProfile.HomeNum = DatSet.Tables[0].Rows[i]["HomeNumber"].ToString();
                        EmployeeProfile.MobileNum = DatSet.Tables[0].Rows[i]["MobileNumber"].ToString();
                        EmployeeProfile.PermanentAdd = DatSet.Tables[0].Rows[i]["PermanentAddress"].ToString();
                        EmployeeProfile.PresentAdd = DatSet.Tables[0].Rows[i]["PresentAddress"].ToString();
                        EmployeeProfile.ProvincialAdd = DatSet.Tables[0].Rows[i]["ProvincialAddress"].ToString();
                        EmployeeProfile.PersonToNofify = DatSet.Tables[0].Rows[i]["PersonNotify"].ToString();
                        EmployeeProfile.Relation = DatSet.Tables[0].Rows[i]["Relation"].ToString();
                        EmployeeProfile.ContactNum = DatSet.Tables[0].Rows[i]["ContactNumber"].ToString();

                        EmployeeProfile.Current_EmployeeID = DatSet.Tables[0].Rows[i]["CurrentID"].ToString();
                        EmployeeProfile.Current_EmployeeName = DatSet.Tables[0].Rows[i]["Current_Name"].ToString();
                        EmployeeProfile.Current_Email = DatSet.Tables[0].Rows[i]["Current_Email"].ToString();
                        EmployeeProfile.Current_BloodType = DatSet.Tables[0].Rows[i]["Current_BloodType"].ToString();
                        EmployeeProfile.Current_HomeNum = DatSet.Tables[0].Rows[i]["Current_HomeNum"].ToString();
                        EmployeeProfile.Current_MobileNum = DatSet.Tables[0].Rows[i]["Curent_Cellphone"].ToString();
                        EmployeeProfile.Current_PermanentAdd = DatSet.Tables[0].Rows[i]["Current_HomeAdd"].ToString();
                        EmployeeProfile.Current_PresentAdd = DatSet.Tables[0].Rows[i]["Current_CurAddress"].ToString();
                        EmployeeProfile.Current_ProvincialAdd = DatSet.Tables[0].Rows[i]["Current_ProvAddress"].ToString();
                        EmployeeProfile.Current_PersonToNotify = DatSet.Tables[0].Rows[i]["Current_PersonToNotify"].ToString();
                        EmployeeProfile.Current_Relation = DatSet.Tables[0].Rows[i]["Current_Relation"].ToString();
                        EmployeeProfile.Current_ContactNum = DatSet.Tables[0].Rows[i]["Current_ContactNum"].ToString();

                        EmployeeProfiles.Add(EmployeeProfile);
                    }
                    ViewDetailApproval.EmployeeProf = EmployeeProfiles;
                }
                con.Close();
            }

            return View(ViewDetailApproval);
        }

        public ActionResult ViewDetailApprovalLegal(string intID)
        {
            EmpViewDetailsForApproval ViewDetailApproval = new EmpViewDetailsForApproval();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailLegalInfo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<GeneralInfos> GeneralInfos = new List<GeneralInfos>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        GeneralInfos GeneralInfo = new GeneralInfos();

                        GeneralInfo.intMstEmpPersonal = DatSet.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        GeneralInfo.isYes1 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ1"].ToString());
                        GeneralInfo.hasDetail1 = DatSet.Tables[0].Rows[i]["DetailQ1"].ToString();
                        GeneralInfo.isYes2 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ2"].ToString());
                        GeneralInfo.hasDetail2 = DatSet.Tables[0].Rows[i]["DetailQ2"].ToString();
                        GeneralInfo.isYes3 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ3"].ToString());
                        GeneralInfo.hasDetail3 = DatSet.Tables[0].Rows[i]["DetailQ3"].ToString();
                        GeneralInfo.isYes4 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ4"].ToString());
                        GeneralInfo.hasDetail4 = DatSet.Tables[0].Rows[i]["DetailQ4"].ToString();
                        GeneralInfo.isYes5 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ5"].ToString());
                        GeneralInfo.hasDetail5 = DatSet.Tables[0].Rows[i]["DetailQ5"].ToString();
                        GeneralInfo.isYes6 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ6"].ToString());
                        GeneralInfo.hasDetail6 = DatSet.Tables[0].Rows[i]["DetailQ6"].ToString();
                        GeneralInfo.isYes7 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["isYesQ7"].ToString());
                        GeneralInfo.hasDetail7 = DatSet.Tables[0].Rows[i]["DetailQ7"].ToString();

                        GeneralInfo.current_intMstEmpPersonal = DatSet.Tables[0].Rows[i]["Current_EmpPersonal"].ToString();
                        GeneralInfo.current_isYes1 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ1"].ToString());
                        GeneralInfo.current_hasDetail1 = DatSet.Tables[0].Rows[i]["Current_DetailQ1"].ToString();
                        GeneralInfo.current_isYes2 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ2"].ToString());
                        GeneralInfo.current_hasDetail2 = DatSet.Tables[0].Rows[i]["Current_DetailQ2"].ToString();
                        GeneralInfo.current_isYes3 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ3"].ToString());
                        GeneralInfo.current_hasDetail3 = DatSet.Tables[0].Rows[i]["Current_DetailQ3"].ToString();
                        GeneralInfo.current_isYes4 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ4"].ToString());
                        GeneralInfo.current_hasDetail4 = DatSet.Tables[0].Rows[i]["Current_DetailQ4"].ToString();
                        GeneralInfo.current_isYes5 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ5"].ToString());
                        GeneralInfo.current_hasDetail5 = DatSet.Tables[0].Rows[i]["Current_DetailQ5"].ToString();
                        GeneralInfo.current_isYes6 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ6"].ToString());
                        GeneralInfo.current_hasDetail6 = DatSet.Tables[0].Rows[i]["Current_DetailQ6"].ToString();
                        GeneralInfo.current_isYes7 = Convert.ToInt32(DatSet.Tables[0].Rows[i]["Current_isYesQ7"].ToString());
                        GeneralInfo.current_hasDetail7 = DatSet.Tables[0].Rows[i]["Current_DetailQ7"].ToString();


                        GeneralInfos.Add(GeneralInfo);
                    }
                    ViewDetailApproval.GeneralInformation = GeneralInfos;
                }
                con.Close();
            }
            return View(ViewDetailApproval);
        }

        public ActionResult ViewDetailApprovalEducational(string intID)
        {
            EmpViewDetailsForApproval ViewDetailApproval = new EmpViewDetailsForApproval();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailEducAttained", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<EducationalAttainments> EducationalAttainments = new List<EducationalAttainments>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        EducationalAttainments EducationalAttainment = new EducationalAttainments();

                        EducationalAttainment.intMstEmpPersonal = DatSet.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        EducationalAttainment.EducationalAttained = DatSet.Tables[0].Rows[i]["EducationalAttained"].ToString();
                        EducationalAttainment.NameOfSchool = DatSet.Tables[0].Rows[i]["NameOfSchool"].ToString();
                        EducationalAttainment.Degree = DatSet.Tables[0].Rows[i]["Degree"].ToString();
                        EducationalAttainment.DateFrom = DatSet.Tables[0].Rows[i]["DateFrom"].ToString();
                        EducationalAttainment.DateTo = DatSet.Tables[0].Rows[i]["DateTo"].ToString();
                        EducationalAttainment.Honors = DatSet.Tables[0].Rows[i]["Honors"].ToString();
                        
                        EducationalAttainments.Add(EducationalAttainment);
                    }
                    ViewDetailApproval.Educ_attained = EducationalAttainments;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailEducAttainedCurrent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<EducationalAttainmentsCurrent> EducationalAttainmentsCurrents = new List<EducationalAttainmentsCurrent>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        EducationalAttainmentsCurrent EducationalAttainmentsCurrent = new EducationalAttainmentsCurrent();

                        EducationalAttainmentsCurrent.current_intMstEmpPersonal = DatSet.Tables[0].Rows[i]["current_EmpPersonal"].ToString();
                        EducationalAttainmentsCurrent.current_EducationalAttained = DatSet.Tables[0].Rows[i]["current_Level"].ToString();
                        EducationalAttainmentsCurrent.current_NameOfSchool = DatSet.Tables[0].Rows[i]["current_School"].ToString();
                        EducationalAttainmentsCurrent.current_Degree = DatSet.Tables[0].Rows[i]["current_Degree"].ToString();
                        EducationalAttainmentsCurrent.current_DateFrom = DatSet.Tables[0].Rows[i]["current_DateFrom"].ToString();
                        EducationalAttainmentsCurrent.current_DateTo = DatSet.Tables[0].Rows[i]["current_DateTo"].ToString();
                        EducationalAttainmentsCurrent.current_Honors = DatSet.Tables[0].Rows[i]["current_Honors"].ToString();

                        EducationalAttainmentsCurrents.Add(EducationalAttainmentsCurrent);
                    }
                    ViewDetailApproval.Current_EducAttained = EducationalAttainmentsCurrents;
                }
                con.Close();
            }



            return View(ViewDetailApproval);
        }

        public ActionResult ViewDetailApprovalWorkExp(string intID)
        {
            EmpViewDetailsForApproval ViewDetailApproval = new EmpViewDetailsForApproval();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailWorkExp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<WorkExperiences> WorkExperiences = new List<WorkExperiences>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        WorkExperiences WorkExperience = new WorkExperiences();

                        WorkExperience.intMstEmpPersonal = DatSet.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        WorkExperience.strDateEmployeed = DatSet.Tables[0].Rows[i]["strDateEmloyed"].ToString();
                        WorkExperience.strPosition = DatSet.Tables[0].Rows[i]["strPosition"].ToString();
                        WorkExperience.strEmployerName = DatSet.Tables[0].Rows[i]["strEmployerName"].ToString();
                        WorkExperience.strEmployerContactNo = DatSet.Tables[0].Rows[i]["EmployerContact"].ToString();
                        WorkExperience.strReason = DatSet.Tables[0].Rows[i]["strReason"].ToString();

                        WorkExperiences.Add(WorkExperience);
                    }
                    ViewDetailApproval.Work_Exp = WorkExperiences;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailWorkExpCurrent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<WorkExperiencesCurrent> WorkExperiencesCurrents = new List<WorkExperiencesCurrent>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        WorkExperiencesCurrent WorkExperiencesCurrent = new WorkExperiencesCurrent();

                        WorkExperiencesCurrent.current_intMstEmpPersonal = DatSet.Tables[0].Rows[i]["current_intMstEmpPersonal"].ToString();
                        WorkExperiencesCurrent.current_strDateEmployeed = DatSet.Tables[0].Rows[i]["current_DateStart"].ToString();
                        WorkExperiencesCurrent.current_strPosition = DatSet.Tables[0].Rows[i]["current_Position"].ToString();
                        WorkExperiencesCurrent.current_strEmployerName = DatSet.Tables[0].Rows[i]["current_EmployerName"].ToString();
                        WorkExperiencesCurrent.current_strEmployerContactNo = DatSet.Tables[0].Rows[i]["current_EmployerNo"].ToString();
                        WorkExperiencesCurrent.current_strReason = DatSet.Tables[0].Rows[i]["current_Reason"].ToString();

                        WorkExperiencesCurrents.Add(WorkExperiencesCurrent);
                    }
                    ViewDetailApproval.Current_Work = WorkExperiencesCurrents;
                }
                con.Close();
            }


            return View(ViewDetailApproval);
        }

        public ActionResult ViewDetailApprovalTraining(string intID)
        {
            EmpViewDetailsForApproval ViewDetailApproval = new EmpViewDetailsForApproval();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailTrainingExp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<TrainingAndSeminar> TrainingAndSeminars = new List<TrainingAndSeminar>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        TrainingAndSeminar TrainingAndSeminar = new TrainingAndSeminar();

                        TrainingAndSeminar.intMstEmpPersonal = DatSet.Tables[0].Rows[i]["intMstEmpPersonal"].ToString();
                        TrainingAndSeminar.strTraining = DatSet.Tables[0].Rows[i]["strTraining"].ToString();
                        TrainingAndSeminar.strSponsor = DatSet.Tables[0].Rows[i]["strSponsor"].ToString();
                        TrainingAndSeminar.strDateTraining = DatSet.Tables[0].Rows[i]["strDateTraining"].ToString();
                        TrainingAndSeminar.Place = DatSet.Tables[0].Rows[i]["Place"].ToString();

                        TrainingAndSeminars.Add(TrainingAndSeminar);
                    }
                    ViewDetailApproval.Train_Sem = TrainingAndSeminars;
                }
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet DatSet = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spViewDetailTrainingExpCurrent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", intID);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(DatSet);

                    List<TrainingAndSeminarCurrent> TrainingAndSeminarCurrents = new List<TrainingAndSeminarCurrent>();

                    for (int i = 0; i < DatSet.Tables[0].Rows.Count; i++)
                    {
                        TrainingAndSeminarCurrent TrainingAndSeminarCurrent = new TrainingAndSeminarCurrent();

                        TrainingAndSeminarCurrent.current_intMstEmpPersonal = DatSet.Tables[0].Rows[i]["current_Personal"].ToString();
                        TrainingAndSeminarCurrent.current_strTraining = DatSet.Tables[0].Rows[i]["current_TrainingSeminar"].ToString();
                        TrainingAndSeminarCurrent.current_strSponsor = DatSet.Tables[0].Rows[i]["current_Sponsor"].ToString();
                        TrainingAndSeminarCurrent.current_strDateTraining = DatSet.Tables[0].Rows[i]["current_DateStart"].ToString();
                        TrainingAndSeminarCurrent.current_Place = DatSet.Tables[0].Rows[i]["current_Place"].ToString();

                        TrainingAndSeminarCurrents.Add(TrainingAndSeminarCurrent);
                    }
                    ViewDetailApproval.Current_Training = TrainingAndSeminarCurrents;
                }
                con.Close();
            }

            return View(ViewDetailApproval);
        }


    }
}
