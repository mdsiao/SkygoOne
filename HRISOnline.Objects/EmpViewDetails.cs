using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class EmpViewDetails
    {     
        public List<EmployeeProf> EmployeeProf { get; set; }
        public List<GeneralInfo> GeneralInformation { get; set; }
        public IEnumerable<EducationalAttainment> Educ_attained { get; set; }
        public IEnumerable<WorkExperience> Work_Exp { get; set; }
        public IEnumerable<TrainingAndSeminars> Train_Sem { get; set; }
        public List<TrainingAndSeminarsReason> TrainingReason { get; set; }
        public List<WorkReason> WorkReason { get; set; }
        public List<EducationalReason> EducReason { get; set; }
    }

    public class EmployeeProf
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string HomeNum { get; set; }
        public string MobileNum { get; set; }
        public string PermanentAdd { get; set; }
        public string PresentAdd { get; set; }
        public string ProvincialAdd { get; set; }
        public string PersonToNofify { get; set; }
        public string Relation { get; set; }
        public string ContactNum { get; set; }
        public string DateInserted { get; set; }

        public string FirstLevelSuperior { get; set; }
        public string strFirstSuperior { get; set; }
        public int isApprovedFirst { get; set; }
        public string SecondLevelSuperior { get; set; }
        public string strSecondSuperior { get; set; }
        public int isApprovedSecond { get; set; }
        public string DisApproveReason { get; set; }
        
    }

    public class GeneralInfo
    {
        public int ID { get; set; }    
        public string intMstEmpPersonal { get; set; }
        public int isYes1 { get; set; }
        public string hasDetail1 { get; set; }
        public int isYes2 { get; set; }
        public string hasDetail2 { get; set; }
        public int isYes3 { get; set; }
        public string hasDetail3 { get; set; }
        public int isYes4 { get; set; }
        public string hasDetail4 { get; set; }
        public int isYes5 { get; set; }
        public string hasDetail5 { get; set; }
        public int isYes6 { get; set; }
        public string hasDetail6 { get; set; }
        public int isYes7 { get; set; }
        public string hasDetail7 { get; set; }

        public string FirstLevelSuperior { get; set; }
        public string strFirstSuperior { get; set; }
        public int isApprovedFirst { get; set; }
        public string SecondLevelSuperior { get; set; }
        public string strSecondSuperior { get; set; }
        public int isApprovedSecond { get; set; }
        public string DisApproveReason { get; set; }

    }

    public class EducationalAttainment
    {
        public int ID { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EducationalAttained { get; set; }
        public string NameOfSchool { get; set; }
        public string Degree { get; set; }
        //public string YearAttended { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Honors { get; set; }      
    }

    public class EducationalReason
    {
        public string FirstLevelSuperior { get; set; }
        public string strFirstSuperior { get; set; }
        public int isApprovedFirst { get; set; }
        public string SecondLevelSuperior { get; set; }
        public string strSecondSuperior { get; set; }
        public int isApprovedSecond { get; set; }
        public string DisApproveReason { get; set; }
    }

    public class WorkExperience
    {
        public int ID { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string strDateEmployeed { get; set; }
        public string strPosition { get; set; }
        public string strEmployerName { get; set; }     
        //public string strEmploymentStatus { get; set; }
        public string strEmployerContactNum { get; set; }
        public string strReason { get; set; }
    }

    public class WorkReason
    {
        public string FirstLevelSuperior { get; set; }
        public string strFirstSuperior { get; set; }
        public int isApprovedFirst { get; set; }
        public string SecondLevelSuperior { get; set; }
        public string strSecondSuperior { get; set; }
        public int isApprovedSecond { get; set; }
        public string DisApproveReason { get; set; }
    }

    public class TrainingAndSeminars
    {
        public int ID { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string strTraining { get; set; }
        public string strSponsor { get; set; }
        public string strDateTraining { get; set; }
        public string Place { get; set; }     
    }

    public class TrainingAndSeminarsReason
    {
        public string FirstLevelSuperior { get; set; }
        public string strFirstSuperior { get; set; }
        public int isApprovedFirst { get; set; }
        public string SecondLevelSuperior { get; set; }
        public string strSecondSuperior { get; set; }
        public int isApprovedSecond { get; set; }
        public string DisApproveReason { get; set; }
    }
}
