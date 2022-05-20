using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRISOnline.Objects;
using System.Data.SqlClient;
using System.Data;

namespace HRISOnline.Data
{
    public class TaxReportDAL
    {
        public List<EmployeeReports> GetEmployeeReports(string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            var items = new List<EmployeeReports>();
            try
            {
                using (var con = new SqlConnection(dbMgr.getSQLConnectionString()))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "Online_GetEmployeeReports";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                items.Add(new EmployeeReports
                                {
                                    tblName = rdr["tblName"].ToString(),
                                    tblID = int.Parse(rdr["tblID"].ToString()),
                                    DateFiled = rdr["DateFiled"].ToString(),
                                    ReportName = rdr["ReportName"].ToString(),
                                    Remarks = rdr["Remarks"].ToString(),
                                    strParameter = rdr["strParameter"].ToString()
                                });
                            }
                        }
                    }
                }

                return items;
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetReportSource(string storedProcedure, int tblID, string intMstEmpPersonal)
        {
            var dbMgr = new dbManager();
            DataTable dt = new DataTable();

            try
            {
                String strConnString = dbMgr.getSQLConnectionString();
                using (var con = new SqlConnection(strConnString))
                {
                    SqlCommand cmd = new SqlCommand(storedProcedure, con);
                    cmd.Parameters.Add(new SqlParameter("@tblID", tblID));
                    cmd.Parameters.Add(new SqlParameter("@intMstEmpPersonal", intMstEmpPersonal));
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    //adpt.Fill(dt, "AwardAndBonusReports");

                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }


            return dt;
        }
    }
}
