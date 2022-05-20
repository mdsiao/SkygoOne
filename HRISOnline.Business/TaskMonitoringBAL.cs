using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRISOnline.Data;
using HRISOnline.Objects;
using System.Data;


namespace HRISOnline.Business
{
    public class TaskMonitoringBAL
    {
        TaskMonitoringDAL taskmonitoring = new TaskMonitoringDAL();

       public DataSet BindDDLSpareparts()
        {
            return taskmonitoring.BindDDLSpareparts();
        }

        public string SaveCashCountCollection(CashCountCollection ccCollection)
        {
            return taskmonitoring.SaveCashCountCollection(ccCollection);
        }

        public string SaveCashCountPettyCash(CashCountPettyCash ccPettyCash)
        {
            return taskmonitoring.SaveCashCountPettyCash(ccPettyCash);
        }

        public string SaveReviewVsDeposits(ReviewVsDeposits ReviewDeposits)
        {
           return taskmonitoring.SaveReviewVsDeposits(ReviewDeposits);
        }

        public string SaveValidateDepositVsAbstactDeposit(ValDepositVsAbstDeposit vdVSad)
        {
            return taskmonitoring.SaveValidateDepositVsAbstactDeposit(vdVSad);
        }
        public string SavePartsCountandRecon(PartsCountandRecon partscountrecon)
        {
            return taskmonitoring.SavePartsCountandRecon(partscountrecon);
        }
        public string SaveMCUnitCount(MCUnitCount MCCount)
        {
            return taskmonitoring.SaveMCUnitCount(MCCount);
        }
        public string SaveSpartPartsSampling(SpartPartsSampling partssamp)
        {
            return taskmonitoring.SaveSpartPartsSampling(partssamp);
        }
    }
}
