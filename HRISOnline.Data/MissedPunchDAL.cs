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
    public class MissedPunchDAL
    {
        public string InsertMissedPunch(MissingPunch punch)
        {
            string result = "";
            SqlConnection con = null;


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spInsertMissingPunch", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@intIDMissingpunch", 0);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", punch.intMstEmpPersonal);
                cmd.Parameters.AddWithValue("@DateFiled", punch.DateFiled);
                cmd.Parameters.AddWithValue("@DatActualDate", punch.DatActualDate);
                cmd.Parameters.AddWithValue("@AdjustmentType", punch.AdjustmentType);
                cmd.Parameters.AddWithValue("@MissedTimeIn", punch.MissedTimeIn);
                cmd.Parameters.AddWithValue("@MissedTimeOut", punch.MissedTimeOut);
                cmd.Parameters.AddWithValue("@MissedNoOfHours", punch.MissedNoOfHours);
                cmd.Parameters.AddWithValue("@ActualTimeFrom", punch.ActualTimeFrom);
                cmd.Parameters.AddWithValue("@ActualTimeTo", punch.ActualTimeTo);
                cmd.Parameters.AddWithValue("@ActualNoOfHours", punch.ActualNoOfHours);
                cmd.Parameters.AddWithValue("@Reason", punch.Reason);


                con.Open();

                //result = cmd.ExecuteScalar().ToString();
                result = (string)cmd.ExecuteScalar();
                //return result;
            }
            catch (Exception ex)
            {
                //return result = "";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public List<MissingPunchList> ListOfMissingPunch(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<MissingPunchList>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spDisListMissingPunch";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<MissingPunchList>(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public string CancelPunch(string ID)
        {
            SqlConnection con = null;

            string result = "";

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spCancelMissingPunch", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@intITHeader", ID);
            
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

        public List<MissingPunchAppAndDis> MissingPunchApproval(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<MissingPunchAppAndDis>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spMissingPunchApprovalList";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<MissingPunchAppAndDis>(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        
        }

        public string ApproveMissingPunch(string Details, string EmployeeId)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spMissingPunchApproved", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@DetailId", Details);


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

        public string DisapproveMissingPunch(string Details, string Reason, string Id)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spMissingPunchDisapproved", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                cmd.Parameters.AddWithValue("@DetailId", Details);
                cmd.Parameters.AddWithValue("@Reason", Reason);

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

        public DataSet AdjustmentTypeDDL(string intMstEmpPersonal)
        {
            SqlConnection con = null;

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand("spAdjustmentTypeDDL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet BranchDeptTypeDDL(string intMstEmpPersonal)
        {
            SqlConnection con = null;

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand("spBranchDeptTypeDDL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet BindEmployeeGroupDDL(string BranchID, string DepartmentID, string PositionID)
        {
            SqlConnection con = null;

            SqlCommand cmd = new SqlCommand("spGetEmployeeDDL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@PositionID", PositionID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //public List<MissingPunchList> ViewDetals(int ID)
        //{
        //    var dbMgr = new dbManager();
        //    var list = new List<MissingPunchList>();
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "";
        //                cmd.Parameters.Add(new SqlParameter("@ID", ID));

        //                conn.Open();
        //                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
        //                {
        //                    dt.Clear();
        //                    adp.Fill(dt);
        //                }
        //            }
        //        }

        //        list = Utilities.Functions.DataTableToList<MissingPunchList>(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }

        //    return list;
        //}

    }
}
