using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRISOnline.Objects;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRISOnline.Data
{
    public class Employee201DAL
    {
        public DataSet CheckTaskMonitoringRights(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCheckTaskMonitoringRights", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", Id);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds, "temp");
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet CheckGroupInsuranceRights(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCheckGroupInsuranceRights", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", Id);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds, "temp");
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }


        //Siao Added
        public DataSet CheckLogin(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCheckAccessEmpUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", Id);
           

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds, "temp");
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }

        }

        public DataSet EmpApprover201(string Id)
        {
            SqlConnection con = null;
            DataSet dds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("Online_GetApproverOfEmployee201", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dds = new DataSet();
                da.Fill(dds, "temp");
                return dds;
            }
            catch
            {
                return dds;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet MissingPunchApprover(string Id)
        {
            SqlConnection con = null;
            DataSet dds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spOnline_GetApproverForMissingPunch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dds = new DataSet();
                da.Fill(dds, "temp");
                return dds;
            }
            catch
            {
                return dds;
            }
            finally
            {
                con.Close();
            }
        }



        //End of Siao Added

        public DataSet getYourDetails(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                cmd.Parameters.AddWithValue("@Query", 1);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }

        }

        public string InsertMyDetails(myStoreData myStore)
        {
            string result = "";
            SqlConnection con = null;

            DataTable DataTableTranDatas = new DataTable();
            DataTableTranDatas.Columns.Add("", typeof(String));
            DataTableTranDatas.Columns.Add("ID", typeof(String));
            DataTableTranDatas.Columns.Add("Educational Attained", typeof(String));

            //leaveApplicationDetails.Columns.Add("intOlnLeaveApplicationDetails", typeof(int));
            //leaveApplicationDetails.Columns.Add("intOlnLeaveApplicationHead", typeof(int));
            //leaveApplicationDetails.Columns.Add("DateOfLeave", typeof(DateTime));
            //leaveApplicationDetails.Columns.Add("isHalfDay", typeof(bool));
            //leaveApplicationDetails.Columns.Add("AMorPM", typeof(string));
            //leaveApplicationDetails.Columns.Add("NoOfDays", typeof(decimal));
            //leaveApplicationDetails.Columns.Add("Reason", typeof(string));
            //leaveApplicationDetails.Columns.Add("isWithPay", typeof(bool));

            int row = 0;
            if (myStore.EducRecord != null)
            {
                foreach (string items in myStore.EducRecord)
                {
                    DataTableTranDatas.Rows.Add(myStore.EducRecord[row].ToString(), myStore.EducRecord[row].ToString(), myStore.EducRecord[row].ToString());
                    row++;
                }
            }

            //leaveApplicationDetails.Columns.Add("intOlnLeaveApplicationDetails", typeof(int));
            //leaveApplicationDetails.Columns.Add("intOlnLeaveApplicationHead", typeof(int));
            //leaveApplicationDetails.Columns.Add("DateOfLeave", typeof(DateTime));
            //leaveApplicationDetails.Columns.Add("isHalfDay", typeof(bool));
            //leaveApplicationDetails.Columns.Add("AMorPM", typeof(string));
            //leaveApplicationDetails.Columns.Add("NoOfDays", typeof(decimal));
            //leaveApplicationDetails.Columns.Add("Reason", typeof(string));
            //leaveApplicationDetails.Columns.Add("isWithPay", typeof(bool));

            //int dRow = 0;
            //foreach (DataGridViewRow row in this.dataGrid.Rows)
            //{
            //    leaveApplicationDetails.Rows.Add(
            //        row.Cells[6].Value == null ? 0 : int.Parse(row.Cells[6].Value.ToString()),
            //        row.Cells[7].Value == null ? 0 : int.Parse(row.Cells[7].Value.ToString()),
            //        Convert.ToDateTime(row.Cells[0].Value.ToString()),
            //        bool.Parse(row.Cells[2].FormattedValue.ToString()),
            //        row.Cells[3].Value == null ? null : row.Cells[3].Value.ToString(),
            //        decimal.Parse(row.Cells[4].Value.ToString()),
            //        row.Cells[5].Value.ToString(),
            //        bool.Parse(row.Cells[8].Value.ToString())
            //    );
            //    dRow++;
            //}





            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con);
                cmd.CommandType = CommandType.StoredProcedure;






                cmd.Parameters.AddWithValue("@EmployeeId", myStore.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", myStore.EmployeeName);
                cmd.Parameters.AddWithValue("@Email", myStore.Email);
                cmd.Parameters.AddWithValue("@BloodType", myStore.BloodType);
                cmd.Parameters.AddWithValue("@HomePhone", myStore.HomePhone);
                cmd.Parameters.AddWithValue("@MobileNum", myStore.MobileNum);
                cmd.Parameters.AddWithValue("@PermanentAdd", myStore.PermanentAdd);
                cmd.Parameters.AddWithValue("@PresentAdd", myStore.PresentAdd);
                cmd.Parameters.AddWithValue("@ProvincialAdd", myStore.ProvincialAdd);
                cmd.Parameters.AddWithValue("@PersonNotify", myStore.PersonNotify);
                cmd.Parameters.AddWithValue("@Relation", myStore.Relation);
                cmd.Parameters.AddWithValue("@ContactNum", myStore.ContactNum);

                cmd.Parameters.AddWithValue("@check1", myStore.check1);
                cmd.Parameters.AddWithValue("@detail1", myStore.detail1);
                cmd.Parameters.AddWithValue("@check2", myStore.check2);
                cmd.Parameters.AddWithValue("@detail2", myStore.detail2);
                cmd.Parameters.AddWithValue("@check3", myStore.check3);
                cmd.Parameters.AddWithValue("@detail3", myStore.detail3);
                cmd.Parameters.AddWithValue("@check4", myStore.check4);
                cmd.Parameters.AddWithValue("@detail4", myStore.detail4);
                cmd.Parameters.AddWithValue("@check5", myStore.check5);
                cmd.Parameters.AddWithValue("@detail5", myStore.detail5);
                cmd.Parameters.AddWithValue("@check6", myStore.check6);
                cmd.Parameters.AddWithValue("@detail6", myStore.detail6);
                cmd.Parameters.AddWithValue("@check7", myStore.check7);
                cmd.Parameters.AddWithValue("@detail7", myStore.detail7);
                cmd.Parameters.AddWithValue("@EducRecord", DataTableTranDatas);                             
                cmd.Parameters.AddWithValue("@Query", 2);


                con.Open();
                result = (String)cmd.ExecuteScalar();
                return result;
           
            }
            catch
            {
      
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet getMyUpdateLogs(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                cmd.Parameters.AddWithValue("@Query", 3);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }

        }

        public string CancelUpdate(string intITHead)
        {
            SqlConnection con = null;

            string result = "";


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCancelUpdateLogs", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@intITHeader", intITHead);
                //cmd.Parameters.AddWithValue("@Query", 4);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }


        public string ApproveUpdate(string Details, string EmployeeId)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spApproveUpdateEmployee201", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@DetailId", Details);              
                //cmd.Parameters.AddWithValue("@Query", 6);

                con.Open();
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //ds = new DataSet();
                //da.Fill(ds);

                result = cmd.ExecuteScalar().ToString();
                return result;

                //return ds;
            }
            catch
            {
                //return ds;
                return result = "";
            }
            finally
            {
                con.Close();
            }

        }

        public string ViewDetails(string intITheader)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spEmployeeTwoOhOne", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@intITheader", intITheader);
                cmd.Parameters.AddWithValue("@Query", 7);

                con.Open();
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                //ds = new DataSet();
                //da.Fill(ds);

                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }

        }

        public string DisApproveEmployeeUpdate(string Details, string EmployeeId, string Reason)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spDisApproveSupperior", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Reason", Reason);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@DetailId", Details);              
                //cmd.Parameters.AddWithValue("@Query", 17);

                con.Open();
                

                result = cmd.ExecuteScalar().ToString();
                return result;
         
            }
            catch
            {
            return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdatePersonalRecord(EmpPersonalProfile PersonalRecord)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spPersonalRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", PersonalRecord.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", PersonalRecord.EmployeeName);
                cmd.Parameters.AddWithValue("@Email", PersonalRecord.Email);
                cmd.Parameters.AddWithValue("@BloodType", PersonalRecord.BloodType);
                cmd.Parameters.AddWithValue("@HomeNum", PersonalRecord.HomeNum);
                cmd.Parameters.AddWithValue("@MobileNum", PersonalRecord.MobileNum);
                cmd.Parameters.AddWithValue("@PermanentAdd", PersonalRecord.PermanentAdd);
                cmd.Parameters.AddWithValue("@PresentAdd", PersonalRecord.PresentAdd);
                cmd.Parameters.AddWithValue("@ProvincialAdd", PersonalRecord.ProvincialAdd);
                cmd.Parameters.AddWithValue("@PersonToNofify", PersonalRecord.PersonToNofify);
                cmd.Parameters.AddWithValue("@Relation", PersonalRecord.Relation);
                cmd.Parameters.AddWithValue("@ContactNum", PersonalRecord.ContactNum);

                con.Open();


                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }



        public string UpdateEducRecord(string values, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spEducationalRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@values", values);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal); 

                //cmd.Parameters.AddWithValue("@intMstEmpPersonal", EducRecord.intMstEmpPersonal);
                //cmd.Parameters.AddWithValue("@EducationalAttained", EducRecord.EducationalAttained);
                //cmd.Parameters.AddWithValue("@NameOfSchool", EducRecord.NameOfSchool);
                //cmd.Parameters.AddWithValue("@Degree", EducRecord.Degree);
                //cmd.Parameters.AddWithValue("@YearAttended", EducRecord.YearAttended);
                //cmd.Parameters.AddWithValue("@Honors", EducRecord.Honors);            

                con.Open();

                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }


        public string UpdateLegalInfoRecord(EmpGeneralInformation LegalRecord)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spLegalInfoRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@intMstEmpPersonal", LegalRecord.intMstEmpPersonal);
                cmd.Parameters.AddWithValue("@check1", LegalRecord.isYes1);
                cmd.Parameters.AddWithValue("@detail1", LegalRecord.hasDetail1);
                cmd.Parameters.AddWithValue("@check2", LegalRecord.isYes2);
                cmd.Parameters.AddWithValue("@detail2", LegalRecord.hasDetail2);
                cmd.Parameters.AddWithValue("@check3", LegalRecord.isYes3);
                cmd.Parameters.AddWithValue("@detail3", LegalRecord.hasDetail3);
                cmd.Parameters.AddWithValue("@check4", LegalRecord.isYes4);
                cmd.Parameters.AddWithValue("@detail4", LegalRecord.hasDetail4);
                cmd.Parameters.AddWithValue("@check5", LegalRecord.isYes5);
                cmd.Parameters.AddWithValue("@detail5", LegalRecord.hasDetail5);
                cmd.Parameters.AddWithValue("@check6", LegalRecord.isYes6);
                cmd.Parameters.AddWithValue("@detail6", LegalRecord.hasDetail6);
                cmd.Parameters.AddWithValue("@check7", LegalRecord.isYes7);
                cmd.Parameters.AddWithValue("@detail7", LegalRecord.hasDetail7);

                con.Open();


                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdateWorkExpRecord(string values, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spWorkExperienceRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@values", values);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
                //cmd.Parameters.AddWithValue("@strDateEmployeed", WorkExpRecord.strDateEmployeed);
                //cmd.Parameters.AddWithValue("@strPosition", WorkExpRecord.strPosition);
                //cmd.Parameters.AddWithValue("@strEmployerName", WorkExpRecord.strEmployerName);
                //cmd.Parameters.AddWithValue("@strEmploymentStatus", WorkExpRecord.strEmploymentStatus);
                //cmd.Parameters.AddWithValue("@strReason", WorkExpRecord.strReason);

                con.Open();


                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string UpdateTrainingAndSemenarsRecord(string values, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spTrainingAndSemenarsRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@values", values);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);

                //cmd.Parameters.AddWithValue("@ID", TrainingRecord.ID);
                //cmd.Parameters.AddWithValue("@intMstEmpPersonal", TrainingRecord.intMstEmpPersonal);
                //cmd.Parameters.AddWithValue("@strTraining", TrainingRecord.strTraining);
                //cmd.Parameters.AddWithValue("@strSponsor", TrainingRecord.strSponsor);
                //cmd.Parameters.AddWithValue("@strDateTraining", TrainingRecord.strDateTraining);
                //cmd.Parameters.AddWithValue("@Place", TrainingRecord.Place);
               

                con.Open();


                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }


        public string DelEducRecord(string ID, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spDelEducRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
                //cmd.Parameters.AddWithValue("@Educ_attained", Educ_attained);
                //cmd.Parameters.AddWithValue("@NameOfSchool", NameOfSchool);
                //cmd.Parameters.AddWithValue("@Degree", Degree);
                //cmd.Parameters.AddWithValue("@DateOfGraduated", DateOfGraduated);
                //cmd.Parameters.AddWithValue("@Honor", Honor);

                con.Open();

                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }


        public string DelWorkExp(string ID, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spDelWorkExpRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
        

                con.Open();

                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public string DelTrainingRecord(string ID, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spTrainingSemenarRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);


                con.Open();

                result = cmd.ExecuteScalar().ToString();
                return result;

            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        //OVERTIME MEALS

        public DataSet CheckOvertimeMealsForHO(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCheckOvertimeMealsRights", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", Id);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds, "temp");
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }


        public DataSet CheckForReportRightsMeals(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spViewReportOvertimeMeals", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", Id);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds, "temp");
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }



        //END OF OVERTIME MEALS

    }
}
