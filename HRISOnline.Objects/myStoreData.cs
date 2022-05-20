using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISOnline.Objects
{
    [Serializable]
    public class myStoreData
    {
        public string EmployeeId { get; set; }
        public string  EmployeeName {get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string  HomePhone { get; set; }
        public string MobileNum  { get; set; }
        public string PermanentAdd  { get; set; }
        public string PresentAdd  { get; set; }
        public string ProvincialAdd  { get; set; }
        public string PersonNotify  { get; set; }
        public string Relation  { get; set; }
        public string ContactNum { get; set; }

        public int check1 { get; set; }
        public string detail1 { get; set; }
        public int check2 { get; set; }
        public string detail2 { get; set; }
        public int check3 { get; set; }
        public string detail3 { get; set; }
        public int check4 { get; set; }
        public string detail4 { get; set; }
        public int check5 { get; set; }
        public string detail5 { get; set; }
        public int check6 { get; set; }
        public string detail6 { get; set; }
        public int check7 { get; set; }
        public string detail7 { get; set; }


        public string[] EducRecord { get; set; }
        public string[] WorkRecord { get; set; }
        public string[] TrainRecord { get; set; }
    }

    //public class EmployeeRecord
    //{
    //    public int EmployeeId { get; set; }
    //    public string EmployeeName { get; set; }
    //    public string Email { get; set; }
    //    public string BloodType { get; set; }
    //    public string HomePhone { get; set; }
    //    public string MobileNum { get; set; }
    //    public string PermanentAdd { get; set; }
    //    public string PresentAdd { get; set; }
    //    public string ProvincialAdd { get; set; }
    //    public string PersonNotify { get; set; }
    //    public string Relation { get; set; }
    //    public string ContactNum { get; set; }
    //}

    //public class LegalRecord 
    //{
    //    public int check1 { get; set; }
    //    public string detail1 { get; set; }
    //    public int check2 { get; set; }
    //    public string detail2 { get; set; }
    //    public int check3 { get; set; }
    //    public string detail3 { get; set; }
    //    public int check4 { get; set; }
    //    public string detail4 { get; set; }
    //    public int check5 { get; set; }
    //    public string detail5 { get; set; }
    //    public int check6 { get; set; }
    //    public string detail6 { get; set; }
    //    public int check7 { get; set; }
    //    public string detail7 { get; set; }
    //}

    public class EducationalRecord
    {
        public int ID { get; set; }
        public string school_attained { get; set; }
        public string name_school { get; set; }
        public string degree { get; set; }
        public string year_attended { get; set; }
        public string honors { get; set; }
    }

    public class WorkExpRecord
    {
        public int ID { get; set; }
        public string datemployed { get; set; }
        public string position { get ; set; }
        public string employer { get; set; }
        public string emp_status { get; set; }
        public string reason { get; set; }
    }

    public class TrainingRecord
    {
        public int ID { get; set; }
        public string training { get; set; }
        public string sponsor { get; set; }
        public string datTraining { get; set; }
        public string place { get; set; }
    }
}
