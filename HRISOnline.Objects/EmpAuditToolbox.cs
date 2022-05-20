using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISOnline.Objects
{
    public class EmpAuditToolbox
    {
        public int intTrnEmpNotice { get; set; }
        public DateTime EmpNoticeDate { get; set; }
        public string NoticeTypeName { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public bool isOpened { get; set; }
        public DateTime DateOpened { get; set; }
        public bool isAcknowledged { get; set; }
        public string DateAcknowledged { get; set; }
        public int intReaction { get; set; }
        public string strReaction { get; set; }
        public int IsAllowed { get; set; }
        public string DeviationTypeName { get; set; }
    }
}
