using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;

namespace HRISOnline.Data
{
    public class ReportDAL
    {
        public DataTable GetDisapproveReport(string intMstEmpPersonal, DateTime dtFrom, DateTime dtTo, string TransType)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();            

            try
            {
                
                 using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                 {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Report_OnlineDisapproved";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));                        
                        cmd.Parameters.Add(new SqlParameter("@TransType", TransType));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;
                
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public DataTable GetCancelReport(string intMstEmpPersonal, DateTime dtFrom, DateTime dtTo, string TransType)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();

            try
            {               

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Report_OnlineCancelled";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@TransType", TransType));                            

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetSubordinateDTRReport(DateTime dtFrom, DateTime dtTo, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[Report_OnlineSubordinateDTR]";
             
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateStart", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateEnd", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", 2));
                        cmd.Parameters.Add(new SqlParameter("@MstBranch_BranchCode", "HO"));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetApprovedAppsReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[Report_OnlineApprovedApplications]";

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@TransType", strModule));                        

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetOB(DateTime dtFrom, DateTime dtTo, string intMstEmpPersonal, string codeMstBranch, int intMstPosition)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[Report_OnlineOB]";

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));                        

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetHRLeave(DateTime dtFrom, DateTime dtTo, string intMstEmpPersonal, string codeMstBranch, int intMstPosition)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[Report_OnlineHRLeave]";

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@dtFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@dtTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetApprAdjustmentReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[spGetApproveAdjustmentType]";

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@TransType", strModule));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetDisApprAdjustmentReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[spGetDisApproveAdjustment]";

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@TransType", strModule));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetOvertimeMealsReport(DateTime dtFrom, DateTime dtTo, string strModule, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();
            string queryName = string.Empty;

            try
            {
                queryName = "[spGetOvertimeMealsReport]";

                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = queryName;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFrom", dtFrom));
                        cmd.Parameters.Add(new SqlParameter("@DateTo", dtTo));
                        cmd.Parameters.Add(new SqlParameter("@TransType", strModule));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
