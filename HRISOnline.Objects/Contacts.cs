using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRISOnline.Objects
{
    public class Contacts
    {
        public int Id { get; set; }
        //public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string LocalNo { get; set; }
        public string ServicePhone { get; set; }
        public string Email { get; set; }
        public string SkypeEmail { get; set; }
        public DataSet StoreAllData { get; set; }
        public string Company { get; set; }
    }
}
