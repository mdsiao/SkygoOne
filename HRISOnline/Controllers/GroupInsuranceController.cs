using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISOnline.Models;
using HRISOnline.Objects;
using HRISOnline.Business;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRISOnline.Controllers
{
    public class GroupInsuranceController : Controller
    {
        //
        // GET: /GroupInsurance/

        public ActionResult Index()
        {
            GroupInsuranceDetails GroupInsuranceDetail = new GroupInsuranceDetails();

            string Id = Session["intMstEmpPersonal"].ToString();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spGroupInsuranceDetail1", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<GroupEmployeeDetails> GroupEmployeeDetails = new List<GroupEmployeeDetails>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        GroupEmployeeDetails GroupEmployeeDetail = new GroupEmployeeDetails();

                        GroupEmployeeDetail.EmployeeID = dset.Tables[0].Rows[i]["EmployeeID"].ToString();
                        GroupEmployeeDetail.EmployeeName = dset.Tables[0].Rows[i]["strEmployeeName"].ToString();
                        GroupEmployeeDetail.NickName = dset.Tables[0].Rows[i]["strExtensionName"].ToString();
                        GroupEmployeeDetail.Gender = dset.Tables[0].Rows[i]["strGender"].ToString();
                        GroupEmployeeDetail.Title = dset.Tables[0].Rows[i]["strTitle"].ToString();
                        GroupEmployeeDetail.Status = dset.Tables[0].Rows[i]["strStatus"].ToString();
                        GroupEmployeeDetail.Birthdate =  dset.Tables[0].Rows[i]["strBirthDate"].ToString();
                        GroupEmployeeDetail.Age = dset.Tables[0].Rows[i]["strAge"].ToString();
                        GroupEmployeeDetail.BirthPlace = dset.Tables[0].Rows[i]["strBirthPlace"].ToString();
                        GroupEmployeeDetail.Nationality = dset.Tables[0].Rows[i]["strNationality"].ToString();
                        GroupEmployeeDetail.TIN = dset.Tables[0].Rows[i]["strTIN"].ToString();
                        GroupEmployeeDetail.SSS = dset.Tables[0].Rows[i]["strSSS"].ToString();
                        GroupEmployeeDetail.ResidenceAddress = dset.Tables[0].Rows[i]["strResidenceAdd"].ToString();
                        GroupEmployeeDetail.City = dset.Tables[0].Rows[i]["strCity"].ToString();
                        GroupEmployeeDetail.Province = dset.Tables[0].Rows[i]["strPronvince"].ToString();
                        GroupEmployeeDetail.Country = dset.Tables[0].Rows[i]["strCountry"].ToString();
                        GroupEmployeeDetail.ZipCode = dset.Tables[0].Rows[i]["strZipCode"].ToString();
                        GroupEmployeeDetail.Occupation = dset.Tables[0].Rows[i]["strOccupation"].ToString();
                        GroupEmployeeDetail.NameOfEmployer = dset.Tables[0].Rows[i]["strNameOfEmployer"].ToString();
                        GroupEmployeeDetail.dateEmployed = dset.Tables[0].Rows[i]["strDateEmployed"].ToString();
                        GroupEmployeeDetail.BResidence = dset.Tables[0].Rows[i]["strBusinessAddress"].ToString();
                        GroupEmployeeDetail.BCity = dset.Tables[0].Rows[i]["strBusinessCity"].ToString();
                        GroupEmployeeDetail.BProvince = dset.Tables[0].Rows[i]["strBusinessProvince"].ToString();
                        GroupEmployeeDetail.BCountry = dset.Tables[0].Rows[i]["strBusinessCountry"].ToString();
                        GroupEmployeeDetail.BZipcode = dset.Tables[0].Rows[i]["strBusinessZipcode"].ToString();
                        GroupEmployeeDetail.BPhoneNo = dset.Tables[0].Rows[i]["strBusinessNo"].ToString();
                        GroupEmployeeDetail.HomeNo = dset.Tables[0].Rows[i]["strHomeNo"].ToString();
                        GroupEmployeeDetail.BEmail = dset.Tables[0].Rows[i]["strEmail"].ToString();
                        GroupEmployeeDetail.EffectiveDate = dset.Tables[0].Rows[i]["datEffectiveDate"].ToString();
                        GroupEmployeeDetail.intTranStatus = Convert.ToInt32(dset.Tables[0].Rows[i]["intTranStatus"].ToString());
                        GroupEmployeeDetail.CellNo = dset.Tables[0].Rows[i]["strCellNo"].ToString();
                        GroupEmployeeDetail.hasCancel = Convert.ToInt32(dset.Tables[0].Rows[i]["hasCancel"].ToString());
                        GroupEmployeeDetail.isProcess = Convert.ToInt32(dset.Tables[0].Rows[i]["IsProcess"].ToString());

                        GroupEmployeeDetails.Add(GroupEmployeeDetail);
                    }
                    GroupInsuranceDetail.EmpProfile = GroupEmployeeDetails;
                }
                con.Close();
            }

            // PRIMARY
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spGetPrimaryBenificiary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<GroupPrimaryDetials> GroupPrimaryDetials = new List<GroupPrimaryDetials>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        GroupPrimaryDetials GroupPrimaryDetial = new GroupPrimaryDetials();

                        GroupPrimaryDetial.ID = Convert.ToInt32(dset.Tables[0].Rows[i]["ID"].ToString());
                        GroupPrimaryDetial.PrimaryFirstName = dset.Tables[0].Rows[i]["PrimaryFirstName"].ToString();
                        GroupPrimaryDetial.PrimaryLastName = dset.Tables[0].Rows[i]["PrimaryLastName"].ToString();
                        GroupPrimaryDetial.PrimaryGender = dset.Tables[0].Rows[i]["PrimaryGender"].ToString();
                        GroupPrimaryDetial.DateOfBirth = dset.Tables[0].Rows[i]["DateOfBirth"].ToString();
                        GroupPrimaryDetial.Relationship = dset.Tables[0].Rows[i]["Relation"].ToString();
                        GroupPrimaryDetial.intStatus = Convert.ToInt32(dset.Tables[0].Rows[i]["intStatus"].ToString());

                        GroupPrimaryDetials.Add(GroupPrimaryDetial);
                    }
                    GroupInsuranceDetail.EmpPrimary = GroupPrimaryDetials;
                }
                con.Close();
            }

            //Secondary
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spGetSecondaryBenificiary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<GroupSecondaryDetails> GroupSecondaryDetails = new List<GroupSecondaryDetails>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        GroupSecondaryDetails GroupSecondaryDetail = new GroupSecondaryDetails();

                        GroupSecondaryDetail.ID = Convert.ToInt32(dset.Tables[0].Rows[i]["ID"].ToString());
                        GroupSecondaryDetail.SecondaryFirstName = dset.Tables[0].Rows[i]["SecondaryFirstName"].ToString();
                        GroupSecondaryDetail.SecondaryLastName = dset.Tables[0].Rows[i]["SecondaryLastName"].ToString();
                        GroupSecondaryDetail.SecondaryGender = dset.Tables[0].Rows[i]["SecondaryGender"].ToString();
                        GroupSecondaryDetail.DateOfBirth = dset.Tables[0].Rows[i]["DateOfBirth"].ToString();
                        GroupSecondaryDetail.Relationship = dset.Tables[0].Rows[i]["Relation"].ToString();
                        GroupSecondaryDetail.intStatus = Convert.ToInt32(dset.Tables[0].Rows[i]["intStatus"].ToString());

                        GroupSecondaryDetails.Add(GroupSecondaryDetail);
                    }
                    GroupInsuranceDetail.EmpSecondary = GroupSecondaryDetails;
                }
                con.Close();
            }


            //Dependents
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spGetDependents", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<GroupDependents> GroupDependents = new List<GroupDependents>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        GroupDependents GroupDependent = new GroupDependents();

                        GroupDependent.ID = Convert.ToInt32(dset.Tables[0].Rows[i]["ID"].ToString());
                        GroupDependent.DependentsFirstName = dset.Tables[0].Rows[i]["DependentsFirstName"].ToString();
                        GroupDependent.DependentsLastName = dset.Tables[0].Rows[i]["DependentsLastName"].ToString();
                        GroupDependent.DependentsGender = dset.Tables[0].Rows[i]["DependentsGender"].ToString();
                        GroupDependent.DateOfBirth = dset.Tables[0].Rows[i]["DateOfBirth"].ToString();
                        GroupDependent.Age = dset.Tables[0].Rows[i]["Age"].ToString();
                        GroupDependent.Relationship = dset.Tables[0].Rows[i]["Relation"].ToString();
                        GroupDependent.intStatus = Convert.ToInt32(dset.Tables[0].Rows[i]["intStatus"].ToString());

                        GroupDependents.Add(GroupDependent);
                    }
                    GroupInsuranceDetail.EmpDependents = GroupDependents;
                }
                con.Close();
            }

            //GroupInsuranceStatus
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString()))
            {
                DataSet dset = new DataSet();

                using (SqlCommand cmd = new SqlCommand("spGetGroupInsuranceStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dset);

                    List<GroupInsuranceStatus> GroupInsuranceStatus = new List<GroupInsuranceStatus>();

                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        GroupInsuranceStatus GroupInsuranceStatu = new GroupInsuranceStatus();

                        GroupInsuranceStatu.ID = Convert.ToInt32(dset.Tables[0].Rows[i]["ID"].ToString());
                        GroupInsuranceStatu.intStatus = Convert.ToInt32(dset.Tables[0].Rows[i]["intStatus"].ToString());
                        GroupInsuranceStatu.isProcess = Convert.ToInt32(dset.Tables[0].Rows[i]["IsProcess"].ToString());

                        GroupInsuranceStatus.Add(GroupInsuranceStatu);
                    }
                    GroupInsuranceDetail.EmpGroupInsuranceStatus = GroupInsuranceStatus;
                }
                con.Close();
            }

            return View(GroupInsuranceDetail);
        }

        public ActionResult UpdateGeneralRecord(GroupEmployeeDetails GeneralInfo)
        {
            GroupInsuranceBAL GroupInsurance = new GroupInsuranceBAL();

            //string result = GroupInsurance.UpdateGeneralInfoRecord(GeneralInfo);

            string result = GroupInsurance.UpdateGeneralInfoRecord(GeneralInfo);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your personal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your personal information!" }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult UpdatePrimaryBenificiary(string primary, string intMstEmpPersonal)
        {
            GroupInsuranceBAL GroupInsurance = new GroupInsuranceBAL();

            string result = GroupInsurance.UpdatePrimaryBenificiary(primary, intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your personal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your personal information!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateSecondaryBenificiary(string secondary, string intMstEmpPersonal)
        {
            GroupInsuranceBAL GroupInsurance = new GroupInsuranceBAL();

            string result = GroupInsurance.UpdateSecondaryBenificiary(secondary, intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your personal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your personal information!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateDependents(string depends, string intMstEmpPersonal)
        {
            GroupInsuranceBAL GroupInsurance = new GroupInsuranceBAL();

            string result = GroupInsurance.UpdateDependents(depends, intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your personal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your personal information!" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CancelTransaction(string intMstEmpPersonal)
        {
            GroupInsuranceBAL GroupInsurance = new GroupInsuranceBAL();

            string result = GroupInsurance.CancelTransaction(intMstEmpPersonal);

            if (result != "Insert")
            {
                return Json(new { success = false, responseText = "Failed to update your personal information!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "You have successfully updated your personal information!" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GenerateInsurance()
        {

            return View();
        }


    }
}
