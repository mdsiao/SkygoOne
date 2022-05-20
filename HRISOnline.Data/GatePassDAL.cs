using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using HRISOnline.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace HRISOnline.Data
{
    public class GatePassDAL
    {
        public List<GatePassList> GetDataGatePassList(string intMstEmpPersonal, int id)
        {
            var dbMgr = new dbManager();
            var list = new List<GatePassList>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetGatePassList";
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePass", 0));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePassType", id));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {                            
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<GatePassList>(dt);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public GatePass GetDataGatePass(int intOlnGatePass)
        {
            var dbMgr = new dbManager();
            var data = new GatePass();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetGatePassList]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePass", intOlnGatePass));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", ""));
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePassType", 0));                        

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                data.intOlnGatePass = Convert.ToInt32(rdr["intOlnGatePass"]);
                                data.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                data.GatePassDate = Convert.ToDateTime(rdr["GatePassDate"]);
                                data.DateFiled = Convert.ToDateTime(rdr["DateFiled"]);
                                data.intOlnGatePassType = Convert.ToInt32(rdr["intOlnGatePassType"]);
                                data.TimeOut = rdr["TimeOut"].ToString();
                                data.TimeIn = rdr["TimeIn"].ToString();
                                data.Remarks = rdr["Remarks"].ToString();                                
                                data.NoOfHours = Convert.ToDouble(rdr["NoOfHours"]);
                                data.DisapproveByName = rdr["DisapproveByName"].ToString();
                                data.DisapproveReason = rdr["DisapproveReason"].ToString();
                                data.DisapprovedDateTime = Convert.ToDateTime(rdr["DisapprovedDateTime"]);
                                data.isDisapproved = Convert.ToBoolean(rdr["isDisapproved"]);
                                data.ApprovedBy = rdr["ApprovedBy"].ToString();
                                data.DisapprovedBy = rdr["DisapprovedBy"].ToString();
                                data.isOBHoliday = Convert.ToBoolean(rdr["isOBHoliday"]);
                                data.intTrnHoliday = rdr["intTrnHoliday"].Equals(null) ? 0 : Convert.ToInt32(rdr["intTrnHoliday"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return data;
        }

        public string SaveDataGatePass(GatePass gp, string branchCode)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_AddEditGatePass";
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePass", gp.intOlnGatePass));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", gp.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFiled", gp.DateFiled));
                        cmd.Parameters.Add(new SqlParameter("@GatePassDate", gp.GatePassDate));
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePassType", gp.intOlnGatePassType));
                        cmd.Parameters.Add(new SqlParameter("@TimeOut", gp.TimeOut));
                        cmd.Parameters.Add(new SqlParameter("@TimeIn", gp.TimeIn));
                        cmd.Parameters.Add(new SqlParameter("@Remarks", gp.Remarks));
                        cmd.Parameters.Add(new SqlParameter("@NoOfHours", gp.NoOfHours));
                        cmd.Parameters.Add(new SqlParameter("@mstBranch_BranchCode", branchCode));
                        cmd.Parameters.Add(new SqlParameter("@isOBHoliday", gp.isOBHoliday));
                        cmd.Parameters.Add(new SqlParameter("@intTrnHoliday", gp.intTrnHoliday));

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

        public List<GatePassApprovalList> GetDataGatePassApprovalList(int intMstCompany, string codeMstBranch, string intMstEmpPersonal, int intOlnGatePassType)
        {
            var dbMgr = new dbManager();
            var list = new List<GatePassApprovalList>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetGatePassForApproval";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePassType", intOlnGatePassType));                        

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }

                    list = Utilities.Functions.DataTableToList<GatePassApprovalList>(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public string ApproveDataGatePass(DataTable dt)
        {
            var dbMgr = new dbManager();
            string _result = string.Empty;

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_ApproveGatePass";
                        cmd.Parameters.Add(new SqlParameter("@GatePassApproval", dt));                        
                        //cmd.Parameters.Add(new SqlParameter("@mstBranchCode", mstBranchCode));                        

                        conn.Open();
                        _result = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());                
            }

            return _result;
        }

        public string CancelGatePass(int intOlnGatePass)
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
                        cmd.CommandText = "[Online_CancelGatePass]";
                        cmd.Parameters.Add(new SqlParameter("@intOlnGatePass", intOlnGatePass));

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

        public List<GatePassRegionalList> GetGatePassRegionalList(string intMstEmpPersonal, int intMstCompany, string codeBranchCode, int intMstPosition)
        {
            var dbMgr = new dbManager();
            var list = new List<GatePassRegionalList>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_HRRegionalList_OB";
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeBranchCode));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));                        

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {                            
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<GatePassRegionalList>(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

    }
}
