using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class EmpViewDetailsForApproval
    {
        public List<EmployeeProfs> EmployeeProf { get; set; }
        public List<GeneralInfos> GeneralInformation { get; set; }
        public IEnumerable<EducationalAttainments> Educ_attained { get; set; }
        public IEnumerable<WorkExperiences> Work_Exp { get; set; }
        public IEnumerable<TrainingAndSeminar> Train_Sem { get; set; }
        public IEnumerable<EducationalAttainmentsCurrent> Current_EducAttained { get; set; }
        public IEnumerable<WorkExperiencesCurrent> Current_Work { get; set; }
        public IEnumerable<TrainingAndSeminarCurrent> Current_Training { get; set; }  
    }
    public class EmployeeProfs
    {
        public int ID { get; set; }
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

        public string Current_EmployeeID { get; set; }
        public string Current_EmployeeName { get; set; }
        public string Current_Email { get; set; }
        public string Current_BloodType { get; set; }
        public string Current_HomeNum { get; set; }
        public string Current_MobileNum { get; set; }
        public string Current_PermanentAdd { get; set; }
        public string Current_PresentAdd { get; set; }
        public string Current_ProvincialAdd { get; set; }
        public string Current_PersonToNotify { get; set; }
        public string Current_Relation { get; set; }
        public string Current_ContactNum { get; set; }



    }

    public class GeneralInfos
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

        public int current_ID { get; set; }
        public string current_intMstEmpPersonal { get; set; }
        public int current_isYes1 { get; set; }
        public string current_hasDetail1 { get; set; }
        public int current_isYes2 { get; set; }
        public string current_hasDetail2 { get; set; }
        public int current_isYes3 { get; set; }
        public string current_hasDetail3 { get; set; }
        public int current_isYes4 { get; set; }
        public string current_hasDetail4 { get; set; }
        public int current_isYes5 { get; set; }
        public string current_hasDetail5 { get; set; }
        public int current_isYes6 { get; set; }
        public string current_hasDetail6 { get; set; }
        public int current_isYes7 { get; set; }
        public string current_hasDetail7 { get; set; }


    }

    public class EducationalAttainments
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

    public class WorkExperiences
    {
        public int ID { get; set; }     
        public string intMstEmpPersonal { get; set; }
        public string strDateEmployeed { get; set; }
        public string strPosition { get; set; }
        public string strEmployerName { get; set; }
        //public string strEmploymentStatus { get; set; }
        public string strEmployerContactNo { get; set; }
        public string strReason { get; set; }
    }

    public class TrainingAndSeminar
    {
        public int ID { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string strTraining { get; set; }
        public string strSponsor { get; set; }
        public string strDateTraining { get; set; }
        public string Place { get; set; }
    }

    public class EducationalAttainmentsCurrent
    {
        public int current_ID { get; set; }
        public string current_intMstEmpPersonal { get; set; }
        public string current_EducationalAttained { get; set; }
        public string current_NameOfSchool { get; set; }
        public string current_Degree { get; set; }
        public string current_DateFrom { get; set; }
        public string current_DateTo { get; set; }
        public string current_Honors { get; set; }
    }

    public class WorkExperiencesCurrent
    {
        public int current_ID { get; set; }
        public string current_intMstEmpPersonal { get; set; }
        public string current_strDateEmployeed { get; set; }
        public string current_strPosition { get; set; }
        public string current_strEmployerName { get; set; }       
        public string current_strEmployerContactNo { get; set; }
        public string current_strReason { get; set; } 
    }

    public class TrainingAndSeminarCurrent
    {
        public string current_intMstEmpPersonal { get; set; }
        public string current_strTraining { get; set; }
        public string current_strSponsor { get; set; }
        public string current_strDateTraining { get; set; }
        public string current_Place { get; set; }
    }

}
