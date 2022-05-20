using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISOnline.Objects
{
    public class GroupInsuranceDetails
    {
        public List<GroupEmployeeDetails> EmpProfile { get; set; }
        public List<GroupPrimaryDetials> EmpPrimary { get; set; }
        public List<GroupSecondaryDetails> EmpSecondary { get; set; }
        public List<GroupDependents> EmpDependents { get; set; }
        public List<GroupInsuranceStatus> EmpGroupInsuranceStatus { get; set; }
    }

    public class GroupEmployeeDetails
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Birthdate { get; set; }
        public string BirthPlace { get; set; }
        public string Age { get; set; }
        public string Nationality { get; set; }
        public string ResidenceAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string HomeNo { get; set; }
        public string CellNo { get; set; }       
        public string Occupation { get; set; }
        public string TIN { get; set; }
        public string SSS { get; set; }
        public string EffectiveDate { get; set; }

        public string NameOfEmployer { get; set; }
        public string dateEmployed { get; set; }
        public string BResidence { get; set; }
        public string BCity { get; set; }
        public string BProvince { get; set; }
        public string BCountry { get; set; }
        public string BZipcode { get; set; }
        public string BPhoneNo { get; set; }
        public string BEmail { get; set; }
        public int intTranStatus { get; set; }
        public int hasCancel { get; set; }
        public int isProcess { get; set; }

      
    }

    public class GroupPrimaryDetials
    {
        public int ID { get; set; }
        //public int intITHeader { get; set; }
        //public string intMstEmpPersonal { get; set; }
        public string PrimaryFirstName { get; set; }
        public string PrimaryLastName { get; set; }
        public string PrimaryGender { get; set; }
        public string DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public int intStatus { get; set; }
    }
    public class GroupSecondaryDetails
    {
        public int ID { get; set; }
        public string SecondaryFirstName { get; set; }
        public string SecondaryLastName { get; set; }
        public string SecondaryGender { get; set; }
        public string DateOfBirth { get; set; }
        public string Relationship { get; set; }
        public int intStatus { get; set; }

    }

    public class GroupDependents
    {
        public int ID { get; set; }
        public string DependentsFirstName { get; set; }
        public string DependentsLastName { get; set; }
        public string DependentsGender { get; set; }
        public string DateOfBirth { get; set; }
        public string Age { get; set; }
        public string Relationship { get; set; }
        public int intStatus { get; set; }
    }

    public class GroupInsuranceStatus
    {
        public int ID { get; set; }
        public int intStatus { get; set; }
        public int isProcess { get; set; }
    }


}
