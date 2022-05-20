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
    public class GroupInsuranceBAL
    {
        GroupInsuranceDAL GroupInsurance = new GroupInsuranceDAL();

        public string UpdateGeneralInfoRecord(GroupEmployeeDetails GeneralInfo)
        {
            return GroupInsurance.UpdateGeneralInfoRecord(GeneralInfo);
        }

        public string UpdatePrimaryBenificiary(string primary, string intMstEmpPersonal)
        {
            return GroupInsurance.UpdatePrimaryBenificiary(primary, intMstEmpPersonal);
        }

        public string UpdateSecondaryBenificiary(string secondary, string intMstEmpPersonal)
        {
            return GroupInsurance.UpdateSecondaryBenificiary(secondary, intMstEmpPersonal);
        }

        public string UpdateDependents(string depends, string intMstEmpPersonal)
        {
            return GroupInsurance.UpdateDependents(depends, intMstEmpPersonal);
        }

        public string CancelTransaction(string intMstEmpPersonal)
        {
            return GroupInsurance.CancelTransaction(intMstEmpPersonal);
        }
    }
}
