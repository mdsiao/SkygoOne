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
    public class ContactsBAL
    {
        ContactDAL CD = new ContactDAL();

        public DataSet DbAccess(string Id)
        {
            return CD.ViewDetails(Id);
        }

        public DataSet SelectAllData()
        {
            return CD.SelectAllData();
        }

        public DataSet ViewDetails(string Id)
        {
            return CD.ViewDetails(Id);
        }
        public DataSet EditDetails(string Id)
        {

            return CD.EditDetails(Id);
        }
        public string UpdateDetails(Contacts con)
        {
            return CD.UpdateData(con);
        }

        public DataSet Login(string Id)
        {
            return CD.Login(Id);
        }

    }
}
