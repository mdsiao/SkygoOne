using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace HRISOnline.Data
{
    public class OvertimeDAL
    {
        public Overtime GetDataOvertime(int intOlnOvertimeApplication)
        {
            var dbMgr = new dbManager();
            var ot = new Overtime();
            string query = string.Empty;

            try
            {                

                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetOvertimeList";
                        cmd.Parameters.Add(new SqlParameter("@intOlnOvertimeApplication", intOlnOvertimeApplication));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", ""));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                ot.intOlnOvertimeApplication = Convert.ToInt32(rdr["intOlnOvertimeApplication"]);
                                ot.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                ot.DateFiled = Convert.ToDateTime(rdr["DateFiled"]);
                                ot.OvertimeDate = Convert.ToDateTime(rdr["OvertimeDate"]);
                                ot.PurposeOfWork = rdr["PurposeOfWork"].ToString();
                                ot.TimeStarted = rdr["TimeStarted"].ToString();
                                ot.TimeEnded = rdr["TimeEnded"].ToString();
                                ot.NoOfHours = Convert.ToDouble(rdr["NoOfHoursDisplay"]);
                                ot.Remarks = rdr["Remarks"].ToString();
                                ot.ImmediateSuperior = rdr["ImmediateSuperior"].ToString();
                                ot.AreaSupervisor = rdr["AreaSupervisor"].ToString();
                                ot.HR = rdr["HR"].ToString();
                                ot.HRLiason = rdr["HRLiason"].ToString();
                                ot.isCancelled = Convert.ToBoolean(rdr["isCancelled"]);
                                ot.isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]);
                                ot.DisapprovedBy = rdr["DisapproveBy"].ToString();
                                ot.DisapprovedDateTime = Convert.ToDateTime(rdr["DisapproveDateTime"]);
                                ot.DisapprovedReason = rdr["DisapproveReason"].ToString();
                                ot.DisapprovedByName = rdr["DisapprovedByName"].ToString();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return ot;
        }

        public List<OvertimeList> GetDataOvertimeList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var ot = new List<OvertimeList>();            

            try
            {                

                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetOvertimeList";
                        cmd.Parameters.Add(new SqlParameter("@intOlnOvertimeApplication", 0));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var myOt = new OvertimeList 
                                { 
                                    intOlnOvertimeApplication = Convert.ToInt32(rdr["intOlnOvertimeApplication"]),                                    
                                    DateFiled = Convert.ToDateTime(rdr["DateFiled"]),
                                    OvertimeDate = Convert.ToDateTime(rdr["OvertimeDate"]),
                                    PurposeOfWork = rdr["PurposeOfWork"].ToString(),
                                    NoOfHours = Utilities.Functions.ConvertHoursToWords(Convert.ToDouble(rdr["NoOfHours"])),
                                    isCancelled = Convert.ToBoolean(rdr["isCancelled"]),
                                    isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]),                                    
                                    isProcessed = Convert.ToBoolean(rdr["isProcessed"]),
                                    Status = rdr["Status"].ToString(),
                                    Remarks = rdr["Remarks"].ToString(),
                                    NextApprover = rdr["NextApprover"].ToString()
                                };

                                ot.Add(myOt);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return ot;
        }

        public string SaveOvertime(Overtime ot)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_AddEditOvertimeApplication]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnOvertimeApplication", ot.intOlnOvertimeApplication));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", ot.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFiled", ot.DateFiled));
                        cmd.Parameters.Add(new SqlParameter("@OvertimeDate", ot.OvertimeDate));
                        cmd.Parameters.Add(new SqlParameter("@PurposeOfWork", ot.PurposeOfWork));
                        cmd.Parameters.Add(new SqlParameter("@TimeStarted", ot.TimeStarted));
                        cmd.Parameters.Add(new SqlParameter("@TimeEnded", ot.TimeEnded));
                        cmd.Parameters.Add(new SqlParameter("@NoOfHours", ot.NoOfHours));
                        cmd.Parameters.Add(new SqlParameter("@Remarks", ot.Remarks));
                        cmd.Parameters.Add(new SqlParameter("@NoOfHoursDisplay", ot.NoOfHoursDisplay));
                        //cmd.Parameters.Add(new SqlParameter("@DateTimeFrom", ot.DateTimeFrom));
                        //cmd.Parameters.Add(new SqlParameter("@DateTimeTo", ot.DateTimeTo));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strResult;
        }

        public string CancelOvertime(int intOlnOvertimeApplication)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_CancelOvertime]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnOvertimeApplication", intOlnOvertimeApplication));

                        conn.Open();
                        strResult = (string)cmd.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strResult;
        }

        public List<OvertimeApproval> GetDataOvertimeApprovalList(int intMstCompany, string codeMstBranch, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<OvertimeApproval>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetOvertimeForApproval]";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new OvertimeApproval 
                                {
                                    intOlnOvertimeApplication = Convert.ToInt32(rdr["intOlnOvertimeApplication"]),
                                    OvertimeDate = Convert.ToDateTime(rdr["OvertimeDate"]),
                                    OvertimeTime = rdr["OvertimeTime"].ToString(),
                                    EmpName = rdr["EmpName"].ToString(),
                                    NoOfHours = Utilities.Functions.ConvertHoursToWords(Convert.ToDouble(rdr["NoOfHours"]))
                                };

                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public DataTable GetOvertimeApprovalListReport(int intMstCompany, string codeMstBranch, int intMstPositionSupervisor, DateTime otDate)
        {
            var dbMgr = new dbManager();            
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetOvertimeForApprovalByDate]";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstPositionSupervisor", intMstPositionSupervisor));
                        cmd.Parameters.Add(new SqlParameter("@otDate", otDate));

                        conn.Open();
                        using (var adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;
                
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        public string ApproveDataOvertime(string intMstEmpPersonal, DataTable dt, bool isFinanceManager = false, bool isFinanceApprovalModule = false)
        {
            var dbMgr = new dbManager();
            string strMessage = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ApproveOvertime";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@overtimeForApproval", dt));
                        cmd.Parameters.Add(new SqlParameter("@isFinanceManager", isFinanceManager));
                        cmd.Parameters.Add(new SqlParameter("@isFinanceApprovalModule", isFinanceApprovalModule));                        

                        conn.Open();
                        strMessage = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return strMessage;
        }

        public List<OvertimeHRRegionalList> GetDataHRRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition, string option, string param)
        {
            var dbMgr = new dbManager();
            var ot = new List<OvertimeHRRegionalList>();            

            try
            {                

                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_HRRegionalList";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeBranchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));
                        cmd.Parameters.Add(new SqlParameter("@type", "Overtime"));
                        cmd.Parameters.Add(new SqlParameter("@option", option));
                        cmd.Parameters.Add(new SqlParameter("@parameter", param));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var myOt = new OvertimeHRRegionalList
                                {
                                    intOlnOvertimeApplication = Convert.ToInt32(rdr["intOlnOvertimeApplication"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),                                    
                                    OvertimeDate = Convert.ToDateTime(rdr["OvertimeDate"]).ToShortDateString(),
                                    EmpName = rdr["EmpName"].ToString(),                                                                        
                                    ImmediateSuperior = rdr["ImmediateSuperior"].ToString(),
                                    AreaSupervisor = rdr["AreaSupervisor"].ToString(),
                                    HR = rdr["HR"].ToString(),
                                    isCancelled = Convert.ToBoolean(rdr["isCancelled"]),
                                    isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]),
                                    ApprovalLevel = Convert.ToInt32(rdr["ApprovalLevel"]),
                                    isInBiometric = Convert.ToBoolean(rdr["isInBiometric"])
                                };

                                ot.Add(myOt);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return ot;
        }

        public List<OvertimeFinanceApproval> GetOvertimeFinanceManager(int intMstCompany, string codeBranchCode, string empid)
        {
            var dbMgr = new dbManager();
            var list = new List<OvertimeFinanceApproval>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetOvertimeFinanceManager";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeBranchCode", codeBranchCode));
                        cmd.Parameters.Add(new SqlParameter("@empid", empid));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<OvertimeFinanceApproval>(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());                
            }

            return list;
        }
        public DataSet BindEmployeeDDL(string intMstEmpPersonal)
        {
            SqlConnection con = null;

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand("spEmpNameDDL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet BindOvertimeRequestServiceDDL(string intMstEmpPersonal)
        {
            SqlConnection con = null;

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand("spOvertimeRequestServiceDLL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public string getPosition(string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spGetPositionName", con);
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

        public string InsertOvertimeMeals(OvertimeMeals meals, string intMstEmpPersonal)
        {
            string result = "";
            SqlConnection con = null;


            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spInsertOvertimeMealsApplicationBranch", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DateFiled", meals.DateFiled);
                cmd.Parameters.AddWithValue("@DateApplied", meals.DateApplied);
                cmd.Parameters.AddWithValue("@intMstEmpPersonal", intMstEmpPersonal);
                cmd.Parameters.AddWithValue("@RequestServiceID", meals.RequestServiceID);
                cmd.Parameters.AddWithValue("@RequestServiceType", meals.RequestServiceType);
                cmd.Parameters.AddWithValue("@Reason", meals.Reason);

                con.Open();

                //result = cmd.ExecuteScalar().ToString();
                //result = (string)cmd.ExecuteScalar().ToString();
                result = (string)cmd.ExecuteScalar();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return result;


        }


        public List<OvertimeMealsList> ListOfOvertimeMeals(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<OvertimeMealsList>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spListOfOvertimeMealsApplied";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<OvertimeMealsList>(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public List<OvertimeMealsDetails> ViewOvertimeMealsDetails(int intIDHead)
        {
            var dbMgr = new dbManager();
            var list = new List<OvertimeMealsDetails>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spGetOvertimeMealDetails";
                        cmd.Parameters.Add(new SqlParameter("@intIDHead", intIDHead));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<OvertimeMealsDetails>(dt);
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
                SqlCommand cmd = new SqlCommand("spCancelOvertimeMeals", con);
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

        public List<OvertimeMealsForApproval> OvertimeMealsForApproval(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<OvertimeMealsForApproval>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spGetOvertimeMealsForApproval";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<OvertimeMealsForApproval>(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;

        }

        public string ApprovedOvertimeMeals(string Details, string EmployeeId)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spApproveOvertimeMeals", con);
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

        public string DisapproveOvertimeMeals(string Details, string Reason, string Id)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spDisapproveOvertimeMeals", con);
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


    }
}
