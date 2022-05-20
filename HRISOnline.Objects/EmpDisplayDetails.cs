using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace HRISOnline.Objects
{
    public class EmpDisplayDetails
    {
        public List<EmpPersonalProfile> EmpProfile { get; set; }
        public List<EmpGeneralInformation> EmpGenInfo { get; set; }
        public IEnumerable<EmpEducationAttainment> EmpEducational { get; set; }
        public IEnumerable<EmpWorkExperiences> EmpWorkExp { get; set; }
        public IEnumerable<EmpTrainingAndSemenars> EmpTraining { get; set; }
        public List<EmpUpdateLogs> EmpUpdateLogings { get; set; }
    }

    public class EmpPersonalProfile
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
        public int alreadyupdated { get; set; }
    }

    public class EmpGeneralInformation
    {
     
        public string intMstEmpPersonal { get; set; }
        public bool isYes1 { get; set; }
        public string hasDetail1 { get; set; }
        public bool isYes2 { get; set; }
        public string hasDetail2 { get; set; }
        public bool isYes3 { get; set; }
        public string hasDetail3 { get; set; }
        public bool isYes4 { get; set; }
        public string hasDetail4 { get; set; }
        public bool isYes5 { get; set; }
        public string hasDetail5 { get; set; }
        public bool isYes6 { get; set; }
        public string hasDetail6 { get; set; }
        public bool isYes7 { get; set; }
        public string hasDetail7 { get; set; }
        public int alreadyupdated { get; set; }
    }

    public class EmpEducationAttainment
    {
        public int ID { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string EducationalAttained { get; set; }
        public string NameOfSchool { get; set; }
        public string Degree { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Honors { get; set; }
        public int alreadyupdated { get; set; }
    }

    public class EmpWorkExperiences
    {
        public int ID { get; set; }     
        public string intMstEmpPersonal { get; set; }
        public string strDateEmployeed { get; set; }
        public string strPosition { get; set; }
        public string strEmployerName { get; set; }
        public string EmployerContact { get; set; }
        //public string strEmploymentStatus { get; set; }
        public string strReason { get; set; }
        public int alreadyupdated { get; set; }
    }
    public class EmpTrainingAndSemenars
    {
        public int ID { get; set; }
        public string intMstEmpPersonal { get; set; }
        public string strTraining { get; set; }
        public string strSponsor { get; set; }
        public string strDateTraining { get; set; }
        public string Place { get; set; }
        public int alreadyupdated { get; set; }
    }

    public class EmpUpdateLogs
    { 
        public int alreadyUpdatePersonal { get; set; }
        public int alreadyUpdateLegal { get; set; }
        public int alreadyUpdateEducational { get; set; }
        public int alreadyUpdateWork { get; set; }
        public int alreadyUpdateTraining { get; set; }
    }


}
