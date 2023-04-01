using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHungerAssignment.Models;

namespace ZeroHungerAssignment.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["user"] == null || Session["type"].ToString() != "employee") return RedirectToAction("Index", "Home");

            var db = new ZeroDB();
            var CollectionRequest = db.Collection_Requests.Where(x => x.Status == "Pending").ToList();
            return View(CollectionRequest);
        }
        public ActionResult Distribute(int id)
        {
            if (Session["user"] == null || Session["type"].ToString() != "employee") return RedirectToAction("Index", "Home");
            var db = new ZeroDB();
            var collection = db.Collection_Requests.Find(id);
            var user = Session["user"] as Employee;
            collection.EmployeeID = user.Id;
            collection.Status = "Distributed";
            var history = new History
            {
                Collection_Request = collection,
                Date = DateTime.Now
            };
            db.Histories.Add(history);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult History()
        {
            if (Session["user"] == null || Session["type"].ToString() != "employee") return RedirectToAction("Index", "Home");
            var db = new ZeroDB();
            var user = Session["user"] as Employee;
            var history = db.Histories.Where(x => x.Collection_Request.EmployeeID == user.Id).ToList();
            return View(history);
        }
    }
}