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
    public class Employee201BAL
    {
        //siao added for task monitoring
        Employee201DAL emp203 = new Employee201DAL();

        public DataSet CheckTaskMonitoringRights(string Id)
        {
            return emp203.CheckTaskMonitoringRights(Id);
        }

        //end of siao for taks monitoring

        //siao added for group insurance rights
        Employee201DAL emp202 = new Employee201DAL();

        public DataSet CheckGroupInsuranceRights(string Id)
        {
            return emp202.CheckGroupInsuranceRights(Id);
        }
     
        // end of siao added for group rightds

        //siao added
        Employee201DAL emp201 = new Employee201DAL();

        public DataSet CheckLogin(string Id)
        {
            return emp201.CheckLogin(Id);
        }

        public DataSet EmpApprover201(string Id)
        {
            return emp201.EmpApprover201(Id);
        }

        //end of siao addded

        public DataSet getMyDetails(string Id)
        {
            return emp201.getYourDetails(Id);
        }

        public string SaveMyDetails(myStoreData myStore)
        {

            return emp201.InsertMyDetails(myStore);
        }

        public DataSet getMyUpdateLogs(string Id)
        {
            return emp201.getMyUpdateLogs(Id);
        }

        public string CancelUpdate(string intITHead)
        {
            return emp201.CancelUpdate(intITHead);
        }

        public string ApproveUpdate(string Details, string EmployeeId)
        {
            return emp201.ApproveUpdate(Details, EmployeeId);
        }
        public string ViewDetails(string intITheader)
        {
            return emp201.ViewDetails(intITheader);
        }
        public string DisApproveEmployeeUpdate(string Details, string EmployeeId, string Reason)
        {
            return emp201.DisApproveEmployeeUpdate(Details, EmployeeId, Reason);
        }

        public string UpdateEducRecord(string values, string intMstEmpPersonal)
        {
            return emp201.UpdateEducRecord(values, intMstEmpPersonal);
        }

        public string UpdatePersonalRecord(EmpPersonalProfile PersonalRecord)
        {
            return emp201.UpdatePersonalRecord(PersonalRecord);
        }

        public string UpdateLegalInfoRecord(EmpGeneralInformation LegalRecord)
        {
            return emp201.UpdateLegalInfoRecord(LegalRecord);
        }

        public string UpdateWorkExpRecord(string values, string intMstEmpPersonal)
        {
            return emp201.UpdateWorkExpRecord(values, intMstEmpPersonal);
        }

        public string UpdateTrainingAndSemenarsRecord(string values, string intMstEmpPersonal)
        {
            return emp201.UpdateTrainingAndSemenarsRecord(values,intMstEmpPersonal);
        }
        public string DelEducRecord(string ID, string intMstEmpPersonal)
        {
            return emp201.DelEducRecord(ID, intMstEmpPersonal);
        }
        public string DelWorkExp(string ID, string intMstEmpPersonal)
        {
            return emp201.DelWorkExp(ID, intMstEmpPersonal);
        }

        public string DelTrainingRecord(string ID, string intMstEmpPersonal)
        {
            return emp201.DelTrainingRecord(ID, intMstEmpPersonal);
        }

        public DataSet MissingPunchApprover(string Id)
        {
            return emp201.MissingPunchApprover(Id);
        }

        public DataSet CheckOvertimeMealsForHO(string Id)
        {
            return emp202.CheckOvertimeMealsForHO(Id);
        }

        public DataSet CheckForReportRightsMeals(string Id)
        {
            return emp202.CheckForReportRightsMeals(Id);
        }
    }
}
