using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;

namespace HRISOnline.Data
{
    public static class EvaluationDAL
    {
        public static List<EvalDet> GetDataEvaluationDetail(int intOlnEvaluation = 0)
        {
            var dbMgr = new dbManager();
            var list = new List<EvalDet>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetEvaluationDetail";
                        cmd.Parameters.Add(new SqlParameter("@intOlnEvaluation", intOlnEvaluation));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new EvalDet 
                                {
                                    intOlnEvaluation = Convert.ToInt32(rdr["intOlnEvaluation"]),
                                    intOlnEvaluationDetail = Convert.ToInt32(rdr["intOlnEvaluationDetail"]),
                                    intMstPerformance = Convert.ToInt32(rdr["intMstPerformance"]),
                                    PerformanceName = rdr["PerformanceName"].ToString(),
                                    Description = rdr["Description"].ToString(),
                                    Comments = rdr["PerformanceName"].ToString(),
                                    Score = Convert.ToInt32(rdr["Score"])
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

        public static List<PerformanceDetail> GetDataEvaluationDetail()
        {
            var dbMgr = new dbManager();
            var list = new List<PerformanceDetail>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM tblMstPerformanceDetail ORDER BY intMstPerformance,OrderNo";

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new PerformanceDetail
                                {
                                    intMstPerformance = Convert.ToInt32(rdr["intMstPerformance"]),
                                    intMstPerformanceDetail = Convert.ToInt32(rdr["intMstPerformanceDetail"]),
                                    OrderNo = Convert.ToInt32(rdr["OrderNo"]),
                                    PerformanceDescription = rdr["PerformanceDescription"].ToString()
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

        public static List<EvalEmployee> GetDataEvaluationEmployees(string codeMstBranch, int intMstCompany, int intMstPosition)
        {
            var dbMgr = new dbManager();
            var list = new List<EvalEmployee>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetEvaluationEmployee";
                        cmd.Parameters.Add(new SqlParameter("@codeMstBranch", codeMstBranch));
                        cmd.Parameters.Add(new SqlParameter("@intMstCompany", intMstCompany));
                        cmd.Parameters.Add(new SqlParameter("@intMstPosition", intMstPosition));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new EvalEmployee 
                                {
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    EmpName = rdr["EmpName"].ToString()
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

        public static string SaveEvaluation(Evaluation evaluation, DataTable dt)
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
                        cmd.CommandText = "Online_AddEditEvaluation";
                        cmd.Parameters.Add(new SqlParameter("@intOlnEvaluation", evaluation.intOlnEvaluation));
                        cmd.Parameters.Add(new SqlParameter("@EvaluationDate", evaluation.EvaluationDate.ToShortDateString()));
                        cmd.Parameters.Add(new SqlParameter("@EvaluateBy", evaluation.EvaluateBy));
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", evaluation.intMstEmpPersonal));
                        cmd.Parameters.Add(new SqlParameter("@PeriodCoveredFrom", evaluation.PeriodCoveredFrom));
                        cmd.Parameters.Add(new SqlParameter("@PeriodCoveredTo", evaluation.PeriodCoveredTo));
                        cmd.Parameters.Add(new SqlParameter("@EffectivityDate", evaluation.EffectivityDate.ToShortDateString()));
                        cmd.Parameters.Add(new SqlParameter("@FinalScore", evaluation.FinalScore));
                        cmd.Parameters.Add(new SqlParameter("@isWithMeritIncrease", evaluation.isWithMeritIncrease));
                        cmd.Parameters.Add(new SqlParameter("@isWithoutMeritIncrease", evaluation.isWithoutMeritIncrease));
                        cmd.Parameters.Add(new SqlParameter("@isTransfer", evaluation.isTransfer));
                        cmd.Parameters.Add(new SqlParameter("@TransferOption", evaluation.TransferOption));
                        cmd.Parameters.Add(new SqlParameter("@TransferTo", evaluation.TransferTo));
                        cmd.Parameters.Add(new SqlParameter("@Comments", evaluation.Comments));
                        cmd.Parameters.Add(new SqlParameter("@EvaluationDetail", dt));

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

        public static List<EvalList> GetDataEvaluationList(string EvaluateBy)
        {
            var dbMgr = new dbManager();
            var list = new List<EvalList>();

            try
            {
                using (var conn = new SqlConnection(dbMgr.getSQLConnectionString())) 
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Online_GetEvaluationList";
                        cmd.Parameters.Add(new SqlParameter("@EvaluateBy", EvaluateBy));

                        conn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var item = new EvalList 
                                {
                                    intOlnEvaluation = Convert.ToInt32(rdr["intOlnEvaluation"]),
                                    intMstEmpPersonal = rdr["intMstEmpPersonal"].ToString(),
                                    EffectivityDate = Convert.ToDateTime(rdr["EffectivityDate"]),
                                    EmpName = rdr["EmpName"].ToString(),
                                    PeriodCoveredFrom = rdr["PeriodCoveredFrom"].ToString(),
                                    PeriodCoveredTo = rdr["PeriodCoveredTo"].ToString()
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
    }
}
