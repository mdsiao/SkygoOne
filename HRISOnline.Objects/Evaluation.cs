using System;
using System.Collections.Generic;

namespace HRISOnline.Objects
{
    public class Performance
    {
        public int intMstPerformance { get; set; }
        public string PerformanceName { get; set; }
        public string Description { get; set; }
    }

    public class PerformanceDetail
    {
        public int intMstPerformanceDetail { get; set; }
        public int intMstPerformance { get; set; }
        public int OrderNo { get; set; }
        public string PerformanceDescription { get; set; }
    }

    public class Evaluation
    {
        public int intOlnEvaluation { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string EvaluateBy { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string PeriodCoveredFrom { get; set; }
        public string PeriodCoveredTo { get; set; }
        public DateTime EffectivityDate { get; set; }
        public int FinalScore { get; set; }
        public bool isWithMeritIncrease { get; set; }
        public bool isWithoutMeritIncrease { get; set; }
        public bool isTransfer { get; set; }
        public string TransferOption { get; set; }
        public string TransferTo { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<EvaluationDetail> Details { get; set; }
    }

    public class EvaluationDetail
    {
        public int intOlnEvaluationDetail { get; set; }
        public int intOlnEvaluation { get; set; }
        public int intMstPerformance { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
    }
}
