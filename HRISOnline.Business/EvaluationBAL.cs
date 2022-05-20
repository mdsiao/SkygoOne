using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRISOnline.Objects;
using HRISOnline.Data;
using System.Data;

namespace HRISOnline.Business
{
    public static class EvaluationBAL
    {        

        public static EvaluationData GetEvaluation(string intMstEmpPersonal)
        {
            EvaluationData eval = new EvaluationData();

            eval.Eval = new Evaluation();
            eval.Performance = EvaluationDAL.GetDataEvaluationDetail(0);
            eval.PerformanceDetail = EvaluationDAL.GetDataEvaluationDetail();
            //eval.EmpPersonal = EmployeeBAL.GetEmployeePersonal(intMstEmpPersonal);
            //eval.EmpDTR = EmployeeBAL.GetEmployeeDTR(intMstEmpPersonal);

            return eval;
        }

        public static List<EvalEmployee> GetEvaluationEmployees(string codeMstBranch, int intMstCompany, int intMstPosition)
        {
            return EvaluationDAL.GetDataEvaluationEmployees(codeMstBranch, intMstCompany, intMstPosition);
        }

        public static List<ComboBoxSource> GetTransferData(string strData)
        {
            string query = string.Empty;
            var list = new List<ComboBoxSource>();

            switch (strData)
            {
                case "Position":
                    query = "SELECT intMstPosition, PositionName FROM tblMstPosition ORDER BY PositionName";
                    break;

                case "Department":
                    query = "SELECT intMstDepartment,DepartmentName FROM tblMstDepartment ORDER BY DepartmentName";
                    break;

                case "Branch":
                    query = "SELECT BranchCode,BranchName FROM tblMstBranch BranchName";
                    break;
            };

            return UtilitiesDAL.GetDataCombo(query);
        }

        public static string SaveEvaluation(Evaluation eval)
        {
            string strResult = string.Empty;
            double finalScore = 0.0;
            DataTable dt = new DataTable();
            dt.Columns.Add("intOlnEvaluationDetail");
            dt.Columns.Add("intOlnEvaluation");
            dt.Columns.Add("intMstPerformance");
            dt.Columns.Add("Score");
            dt.Columns.Add("Comments");

            try
            {
                foreach (var item in eval.Details)
                {
                    finalScore += item.Score;

                    dt.Rows.Add(item.intOlnEvaluationDetail, item.intOlnEvaluation, item.intMstPerformance, item.Score, item.Comments);
                }

                if (finalScore == 0)
                {
                    throw new Exception("Final score should be greater than zero(0).");
                }
                else 
                {
                    strResult = EvaluationDAL.SaveEvaluation(eval, dt);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }

            return strResult;
        }

        public static List<EvalList> GetEvaluationList(string EvaluateBy)
        {
            return EvaluationDAL.GetDataEvaluationList(EvaluateBy);
        }
    }
}
