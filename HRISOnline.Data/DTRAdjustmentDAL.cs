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
    public class DTRAdjustmentDAL
    {
        public string SaveDTRAdjustment(DTRAdjustment dtrAdj)
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
                        cmd.CommandText = "Online_AddEditDTRAdjustment";
                        cmd.Parameters.Add(new SqlParameter("@intOlnDTRAdjustment", dtrAdj.intOlnDTRAdjustment));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", dtrAdj.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@DateFiled", dtrAdj.DateFiled));
                        cmd.Parameters.Add(new SqlParameter("@AdjustmentDate", dtrAdj.AdjustmentDate));
                        cmd.Parameters.Add(new SqlParameter("@TimeIn", dtrAdj.TimeIn));
                        cmd.Parameters.Add(new SqlParameter("@TimeOut", dtrAdj.TimeOut));
                        cmd.Parameters.Add(new SqlParameter("@NoOfHours", dtrAdj.NoOfHours));
                        cmd.Parameters.Add(new SqlParameter("@Reason", dtrAdj.Reason));

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

        public List<DTRAdjList> GetDataDTRAdjustmentList(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var list = new List<DTRAdjList>();
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetDTRAdjustmentList";
                        //cmd.Parameters.Add(new SqlParameter("@intOlnDTRAdjustment", 0));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        conn.Open();
                        using (var adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }
                }

                list = Utilities.Functions.DataTableToList<DTRAdjList>(dt);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }

            return list;
        }

        public DTRAdjustment GetDataDTRAdjustment(int intOlnDTRAdjustment)
        {
            var dbMgr = new dbManager();
            var data = new DTRAdjustment();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetDTRAdjustmentList";
                        cmd.Parameters.Add(new SqlParameter("@intOlnDTRAdjustment", intOlnDTRAdjustment));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                data.intOlnDTRAdjustment = Convert.ToInt32(rdr["intOlnDTRAdjustment"]);
                                data.AdjustmentDate = Convert.ToDateTime(rdr["AdjustmentDate"]);
                                data.DateFiled = Convert.ToDateTime(rdr["DateFiled"]);
                                data.intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString();
                                data.TimeIn = rdr["TimeIn"].ToString();
                                data.TimeOut = rdr["TimeOut"].ToString();
                                data.Reason = rdr["Reason"].ToString();
                                data.NoOfHours = Convert.ToDouble(rdr["NoOfHours"]);
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

        public string CancelDTRAdjustment(int intOlnDTRAdjustment)
        {
            var dbMgr = new dbManager();
            string strResult = string.Empty;

            try
            {

                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_CancelDTRAdjustment";
                        cmd.Parameters.Add(new SqlParameter("@intOlnDTRAdjustment", intOlnDTRAdjustment));

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

        public List<DTRAdjustmentApprovalList> GetDataDTRAdjustmentApprovalList(int intMstCompany, string codeMstBranch, int intMstPositionSupervisor)
        {

            var dbMgr = new dbManager();
            var list = new List<DTRAdjustmentApprovalList>();
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[Online_GetDTRAdjustmentForApproval]";
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstPositionSupervisor", intMstPositionSupervisor));

                        conn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            dt.Clear();
                            adp.Fill(dt);
                        }
                    }

                    list = Utilities.Functions.DataTableToList<DTRAdjustmentApprovalList>(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());                
            }

            return list;
        }

        public string ApproveDataDTRAdjustment(DataTable dt, int intMstPositionSupervisor, bool isHRHomeOffice = false)
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
                        cmd.CommandText = "Online_ApproveDTRAdjustment";
                        cmd.Parameters.Add(new SqlParameter("@DTRAdjustmentApproval", dt));
                        cmd.Parameters.Add(new SqlParameter("@isHRRegional", isHRHomeOffice));
                        cmd.Parameters.Add(new SqlParameter("@intMstPositionSupervisor", intMstPositionSupervisor));                        

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
    }
}
