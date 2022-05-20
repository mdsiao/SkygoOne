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
    public class ContactDAL
    {
        //---- get all data --//
        public DataSet SelectAllData()
        {
            SqlConnection con = null;
            DataSet ds = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("SpContactDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", 0);
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
        } //---- end get all data --//

        //---checking user or admin---//
        public DataSet CheckAccess(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("SpContactDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                cmd.Parameters.AddWithValue("@Query", 5);

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
        //---end checking user or admin---//


        // --- View Details --- //    
        public DataSet ViewDetails(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("SpContactDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                cmd.Parameters.AddWithValue("@Query", 2);


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
        // --- End View Details --- // 


        //--- Edit Details ---//
        public DataSet EditDetails(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("SpContactDetails", con);
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
        //--- End Edit Details ---//


        //--- Update Details --- //
        public string UpdateData(Contacts CD)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("SpContactDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", CD.Id);
                cmd.Parameters.AddWithValue("@FullName", CD.FullName);
                cmd.Parameters.AddWithValue("@Branch", CD.Branch);
                cmd.Parameters.AddWithValue("@Department", CD.Department);
                cmd.Parameters.AddWithValue("@Position", CD.Position);
                cmd.Parameters.AddWithValue("@LocalNo", CD.LocalNo);
                cmd.Parameters.AddWithValue("@ServicePhone", CD.ServicePhone);
                cmd.Parameters.AddWithValue("@Email", CD.Email);
                cmd.Parameters.AddWithValue("@SkypeAccount", CD.SkypeEmail);
                cmd.Parameters.AddWithValue("@Query", 4);

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
        //--- ENd Update Details --- //


        public DataSet Login(string Id)
        {
            SqlConnection con = null;
            DataSet ds = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("SpContactDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                cmd.Parameters.AddWithValue("@Query", 5);

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
    }
}
