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
    public class GroupInsuranceDAL
    {


        public string UpdateGeneralInfoRecord(GroupEmployeeDetails GeneralInfo)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spInsertGeneralGroupInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", GeneralInfo.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", GeneralInfo.EmployeeName);
                cmd.Parameters.AddWithValue("@NickName", GeneralInfo.NickName);
                cmd.Parameters.AddWithValue("@Gender", GeneralInfo.Gender);
                cmd.Parameters.AddWithValue("@Title", GeneralInfo.Title);
                cmd.Parameters.AddWithValue("@Status", GeneralInfo.Status);
                cmd.Parameters.AddWithValue("@Birthdate", GeneralInfo.Birthdate);
                cmd.Parameters.AddWithValue("@BirthPlace", GeneralInfo.BirthPlace);
                cmd.Parameters.AddWithValue("@Age", GeneralInfo.Age);
                cmd.Parameters.AddWithValue("@Nationality", GeneralInfo.Nationality);
                cmd.Parameters.AddWithValue("@ResidenceAddress", GeneralInfo.ResidenceAddress);
                cmd.Parameters.AddWithValue("@City", GeneralInfo.City);
                cmd.Parameters.AddWithValue("@Province", GeneralInfo.Province);
                cmd.Parameters.AddWithValue("@Country", GeneralInfo.Country);
                cmd.Parameters.AddWithValue("@Zipcode", GeneralInfo.ZipCode);
                cmd.Parameters.AddWithValue("@HomeNo", GeneralInfo.HomeNo);
                cmd.Parameters.AddWithValue("@CellNo", GeneralInfo.CellNo);
                cmd.Parameters.AddWithValue("@Occupation", GeneralInfo.Occupation);
                cmd.Parameters.AddWithValue("@TIN", GeneralInfo.TIN);
                cmd.Parameters.AddWithValue("@SSS", GeneralInfo.SSS);

                cmd.Parameters.AddWithValue("@NameOfEmployer", GeneralInfo.NameOfEmployer);
                cmd.Parameters.AddWithValue("@BResidence", GeneralInfo.BResidence);
                cmd.Parameters.AddWithValue("@BCity", GeneralInfo.BCity);
                cmd.Parameters.AddWithValue("@BProvince", GeneralInfo.BProvince);
                cmd.Parameters.AddWithValue("@BCountry", GeneralInfo.BCountry);
                cmd.Parameters.AddWithValue("@BZipcode", GeneralInfo.BZipcode);
                cmd.Parameters.AddWithValue("@BPhoneNo", GeneralInfo.BPhoneNo);
                cmd.Parameters.AddWithValue("@BEmail", GeneralInfo.BEmail);
                cmd.Parameters.AddWithValue("@dateEmployed", GeneralInfo.dateEmployed);
                cmd.Parameters.AddWithValue("@EffectiveDate", GeneralInfo.EffectiveDate);

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


        public string UpdatePrimaryBenificiary(string primary, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spInsertPrimaryBenificiary", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@primary", primary);
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

        public string UpdateSecondaryBenificiary(string secondary, string intMstEmpPersonal)
        {

            string result = "";
            SqlConnection con = null;


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spInsertSecondaryBenificiary", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@secondary", secondary);
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

        public string UpdateDependents(string depends, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spInsertDependents", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@depends", depends);
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

        public string CancelTransaction(string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCancelGroupInsurance", con);
                cmd.CommandType = CommandType.StoredProcedure;

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


    }
}
