using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISOnline.Objects
{
    public class CashCountCollection
    {
        public string EmployeeID { get; set; }
        public string strDate { get; set; }
        public string intCashCountCollectionSKYGO { get; set; }
        public string intCashCountAccountabilitySKYGO { get; set; }
        public string datCollectionDateSKYGO { get; set; }
        public string TimeCollectionDateSKYGO { get; set; }
        public string intCashCountCollectionSNDC { get; set; }
        public string intCashCountAccountabilitySNDC { get; set; }
        public string datCollectionDateSNDC { get; set; }
        public string TimeCollectionDateSNDC { get; set; }

    }

    public class CashCountPettyCash
    {
        public string EmployeeID { get; set; }
        public string intCashCountPettyCashSKYGO { get; set; }
        public string intCashCountExpensesSKYGO { get; set; }
        public string PettyCashTimeSKYGO { get; set; }
        public string intCashCountPettyCashSNDC { get; set; }
        public string intCashCountExpensesSNDC { get; set; }
        public string PettyCashTimeSNDC { get; set; }
    }

    public class ReviewVsDeposits
    {
        public string EmployeeID { get; set; }
        public string intReviewVsDepositsCashCountSKYGO { get; set; }
        public string intReviewVsDepositsAccountabilitySKYGO { get; set; }
        public string datDateReceiptSKYGO { get; set; }
        public string intReviewVsDepositsCashCountSNDC { get; set; }
        public string intReviewVsDepositsAccountabilitySNDC { get; set; }
        public string datDateReceiptSNDC { get; set; }
    }

    public class ValDepositVsAbstDeposit
    {
        public string EmployeeID { get; set; }
        public string intCashValidatedSKYGO { get; set; }
        public string datValidationDateSKYGO { get; set; }
        public string intCashValidatedSNDC { get; set; }
        public string datValidationDateSNDC { get; set; }
    }

    public class PartsCountandRecon
    {
        public string EmployeeID { get; set; }
        public int totalLineItem { get; set; }
        public int totalActualQty { get; set; }
    }

    public class MCUnitCount
    {
        public string EmployeeID { get; set; }
        public string intMCUnitCount { get; set; }
    }

    public class SpartPartsSampling
    {
        public string EmployeeID { get; set; }
        public string datTransationDate { get; set; }
        public string strSparePartSampling { get; set; }
        public string strpartsDesc { get; set; }
        public string intSpartPartsSampling { get; set; }

        public List<CashCountCollection> TaskCashCountCollection { get; set; }
        public List<CashCountPettyCash> TaskCashCountPettyCash { get; set; }
        public List<ReviewVsDeposits> TaskReviewVsDeposits { get; set; }
        public List<ValDepositVsAbstDeposit> TaskValDepositVsAbstDeposit { get; set; }
        public List<PartsCountandRecon> TaskPartsCountandRecon { get; set; }
        public List<MCUnitCount> TaskMCUnitCount { get; set; }

        //public List<SpartPartsSampling> TaskSpartPartsSampling { get; set; }
        
        
    }

    //public class TaskMonitoringData
    //{
    //    public List<CashCountCollection> TaskCashCountCollection { get; set; }
    //    public List<CashCountPettyCash> TaskCashCountPettyCash { get; set; }
    //    public List<ReviewVsDeposits> TaskReviewVsDeposits { get; set; }
    //    public List<ValDepositVsAbstDeposit> TaskValDepositVsAbstDeposit { get; set; }
    //    public List<PartsCountandRecon> TaskPartsCountandRecon { get; set; }
    //    public List<MCUnitCount> TaskMCUnitCount { get; set; }
    //    public List<SpartPartsSampling> TaskSpartPartsSampling { get; set; }
    //}
}
