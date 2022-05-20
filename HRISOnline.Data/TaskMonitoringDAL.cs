using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HRISOnline.Objects;

namespace HRISOnline.Data
{
    public class TaskMonitoringDAL
    {
        

        public DataSet BindDDLSpareparts()
        {
            SqlConnection con = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());

            SqlCommand cmd = new SqlCommand("spBindDDLSampling", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public string SaveCashCountCollection(CashCountCollection ccCollection)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSaveCashCountCollection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", ccCollection.EmployeeID);
                cmd.Parameters.AddWithValue("@intCashCountCollectionSKYGO", ccCollection.intCashCountCollectionSKYGO);
                cmd.Parameters.AddWithValue("@intCashCountAccountabilitySKYGO", ccCollection.intCashCountAccountabilitySKYGO);
                cmd.Parameters.AddWithValue("@datCollectionDateSKYGO", ccCollection.datCollectionDateSKYGO);
                cmd.Parameters.AddWithValue("@intCashCountCollectionSNDC", ccCollection.intCashCountCollectionSNDC);
                cmd.Parameters.AddWithValue("@intCashCountAccountabilitySNDC", ccCollection.intCashCountAccountabilitySNDC);
                cmd.Parameters.AddWithValue("@datCollectionDateSNDC", ccCollection.datCollectionDateSNDC);

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

        public string SaveCashCountPettyCash(CashCountPettyCash ccPettyCash)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSaveCashCountPettyCash  ", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", ccPettyCash.EmployeeID);
                cmd.Parameters.AddWithValue("@intCashCountPettyCashSKYGO", ccPettyCash.intCashCountPettyCashSKYGO);
                cmd.Parameters.AddWithValue("@intCashCountExpensesSKYGO", ccPettyCash.intCashCountExpensesSKYGO);
                cmd.Parameters.AddWithValue("@intCashCountPettyCashSNDC", ccPettyCash.intCashCountPettyCashSNDC);
                cmd.Parameters.AddWithValue("@intCashCountExpensesSNDC", ccPettyCash.intCashCountExpensesSNDC);

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

        public string SaveReviewVsDeposits(ReviewVsDeposits ReviewDeposits)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSaveReviewVsDeposits", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", ReviewDeposits.EmployeeID);
                cmd.Parameters.AddWithValue("@intReviewVsDepositsCashCountSKYGO", ReviewDeposits.intReviewVsDepositsCashCountSKYGO);
                cmd.Parameters.AddWithValue("@intReviewVsDepositsAccountabilitySKYGO", ReviewDeposits.intReviewVsDepositsAccountabilitySKYGO);
                cmd.Parameters.AddWithValue("@datDateReceiptSKYGO", ReviewDeposits.datDateReceiptSKYGO);
                cmd.Parameters.AddWithValue("@intReviewVsDepositsCashCountSNDC", ReviewDeposits.intReviewVsDepositsCashCountSNDC);
                cmd.Parameters.AddWithValue("@intReviewVsDepositsAccountabilitySNDC", ReviewDeposits.intReviewVsDepositsAccountabilitySNDC);
                cmd.Parameters.AddWithValue("@datDateReceiptSNDC", ReviewDeposits.datDateReceiptSNDC);

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

        public string SaveValidateDepositVsAbstactDeposit(ValDepositVsAbstDeposit vdVSad)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSaveValidateDepositVsAbstactDeposit", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", vdVSad.EmployeeID);
                cmd.Parameters.AddWithValue("@datValidationDateSKYGO", vdVSad.datValidationDateSKYGO);
                cmd.Parameters.AddWithValue("@intCashValidatedSKYGO", vdVSad.intCashValidatedSKYGO);
                cmd.Parameters.AddWithValue("@datValidationDateSNDC", vdVSad.datValidationDateSNDC);
                cmd.Parameters.AddWithValue("@intCashValidatedSNDC", vdVSad.intCashValidatedSNDC);

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

        public string SavePartsCountandRecon(PartsCountandRecon partscountrecon)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSavePartsCountandRecon", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", partscountrecon.EmployeeID);
                cmd.Parameters.AddWithValue("@intTotalLineItem", partscountrecon.totalLineItem);
                cmd.Parameters.AddWithValue("@intTotalActualQty", partscountrecon.totalActualQty);

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

        public string SaveMCUnitCount(MCUnitCount MCCount)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSaveMCUnitCount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", MCCount.EmployeeID);
                cmd.Parameters.AddWithValue("@intMCUnitCount", MCCount.intMCUnitCount);


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

        public string SaveSpartPartsSampling(SpartPartsSampling partssamp)
        {
            string result = "";
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand("spSaveSpartPartsSampling", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", partssamp.EmployeeID);
                cmd.Parameters.AddWithValue("@datTransationDate", partssamp.datTransationDate);
                cmd.Parameters.AddWithValue("@strSparePartSampling", partssamp.strSparePartSampling);
                cmd.Parameters.AddWithValue("@strpartsDesc", partssamp.strpartsDesc);
                cmd.Parameters.AddWithValue("@intSpartPartsSampling", partssamp.intSpartPartsSampling);


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
