using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HRISOnline.Objects;
using HRISOnline.Business;
using HRISOnline.Models;

namespace HRISOnline.Controllers
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/

        [CheckSessionOut]
        public ActionResult Index(Contacts con)
        {
            ContactsBAL CB = new ContactsBAL();
            con.StoreAllData = CB.SelectAllData();

            string Id = Session["intMstEmpPersonal"].ToString();
            DataSet ds = CB.DbAccess(Id);

            con.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["EmployeeId"].ToString());
            con.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
            con.Branch = ds.Tables[0].Rows[0]["BranchName"].ToString();
            con.Department = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
            con.Position = ds.Tables[0].Rows[0]["PositionName"].ToString();
            con.LocalNo = ds.Tables[0].Rows[0]["LocalNo"].ToString();
            con.ServicePhone = ds.Tables[0].Rows[0]["ServicePhone"].ToString();
            con.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            con.SkypeEmail = ds.Tables[0].Rows[0]["SkypeAccount"].ToString();
            con.Company = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                

            ViewBag.MyTitle = "Directory";
            return View(con);
        }

        //public ActionResult ContactList(Contacts con)
        //{
        //    ContactsBAL CB = new ContactsBAL();
        //    con.StoreAllData = CB.SelectAllData();
        //    ViewBag.MyTitle = "Contact Lists";

        //    return View(con);
        //}

        //public ActionResult ViewDetails(string Id)
        //{
        //    ContactsBAL CB = new ContactsBAL();
        //    DataSet ds = CB.ViewDetails(Id);

        //    Contacts con = new Contacts();

        //    con.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["EmployeeId"].ToString());
        //    con.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
        //    con.Branch = ds.Tables[0].Rows[0]["BranchName"].ToString();
        //    con.Department = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
        //    con.Position = ds.Tables[0].Rows[0]["PositionName"].ToString();
        //    con.LocalNo = ds.Tables[0].Rows[0]["LocalNo"].ToString();
        //    con.ServicePhone = ds.Tables[0].Rows[0]["ServicePhone"].ToString();
        //    con.Email = ds.Tables[0].Rows[0]["Email"].ToString();
        //    con.SkypeEmail = ds.Tables[0].Rows[0]["SkypeAccount"].ToString();

        //    return View(CB);
        //}

        [CheckSessionOut]
        public ActionResult EditDetails(string Id)
        {
            ContactsBAL CB = new ContactsBAL();
            DataSet ds = CB.EditDetails(Id);

            Contacts con = new Contacts();

            con.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["EmployeeId"].ToString());
            con.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
            con.Branch = ds.Tables[0].Rows[0]["BranchName"].ToString();
            con.Department = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
            con.Position = ds.Tables[0].Rows[0]["PositionName"].ToString();
            con.LocalNo = ds.Tables[0].Rows[0]["LocalNo"].ToString();
            con.ServicePhone = ds.Tables[0].Rows[0]["ServicePhone"].ToString();
            con.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            con.SkypeEmail = ds.Tables[0].Rows[0]["SkypeAccount"].ToString();

            ViewBag.MyTitle = "Edit Contacts";
            return View(con);
        }

        [HttpPost]
        public ActionResult EditDetails(Contacts con)
        {
            ContactsBAL CB = new ContactsBAL();
            string result = CB.UpdateDetails(con);
            ViewData["resultUpdate"] = result;
            ViewBag.MyTitle = "Edit Contacts";
            return View();

        }

        [CheckSessionOut]
        public ActionResult Login(string Id)
        {
            return RedirectToAction("Index");
        }

    }
}
