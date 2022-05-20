using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRISOnline.Business;
using HRISOnline.Data;
using HRISOnline.Objects;

namespace HRISOnline.Business
{
    public class MissedPunchBAL
    {
        MissedPunchDAL MissedPunch = new MissedPunchDAL();

        public string InsertMissedPunch(MissingPunch punch)
        {
            return MissedPunch.InsertMissedPunch(punch);
        }

        public List<MissingPunchList> ListOfMissingPunch(string intMstEmpPersonal)
        {
            return MissedPunch.ListOfMissingPunch(intMstEmpPersonal);
        }

        public string CancelPunch(string ID)
        {
            return MissedPunch.CancelPunch(ID);
        }
        public List<MissingPunchAppAndDis> MissingPunchApproval(string intMstEmpPersonal)
        {
            return MissedPunch.MissingPunchApproval(intMstEmpPersonal);
        }

        public string ApproveMissingPunch(string Details, string EmployeeId)
        {
            return MissedPunch.ApproveMissingPunch(Details, EmployeeId);
        }

        public string DisapproveMissingPunch(string Details, string Reason, string Id)
        {

            return MissedPunch.DisapproveMissingPunch(Details, Reason, Id);
        }

        //public List<MissingPunchList> ViewDetals(int ID)
        //{
        //    return MissedPunch.ViewDetals(ID);
        //}

      

    }
}
