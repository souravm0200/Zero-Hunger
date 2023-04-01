using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHungerAssignment.Models;

namespace ZeroHungerAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                if (Session["type"].ToString() == "employee")
                {
                    return RedirectToAction("Index", "Employee");
                }
                else if (Session["type"].ToString() == "restaurant")
                {
                    return RedirectToAction("Index", "Restaurant");
                }
            }
            return View();
        }

        public ActionResult RestaurantRegitration()
        {
            if (Session["user"] != null)
            {
                if (Session["type"].ToString() == "employee")
                {
                    return RedirectToAction("Index", "Employee");
                }
                else if (Session["type"].ToString() == "restaurant")
                {
                    return RedirectToAction("Index", "Restaurant");
                }
            }
            return View();
        }

        public ActionResult EmployeeRegistration()
        {
            if (Session["user"] != null)
            {
                if (Session["type"].ToString() == "employee")
                {
                    return RedirectToAction("Index", "Employee");
                }
                else if (Session["type"].ToString() == "restaurant")
                {
                    return RedirectToAction("Index", "Restaurant");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeRegistration(Employee employee)
        {
            var db = new ZeroDB();
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RestaurantRegitration(Restaurant restaurant)
        {
            var db = new ZeroDB();
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var db = new ZeroDB();
            var email = form["email"];
            var password = form["password"];
            var emp = db.Employees.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (emp != null)
            {
                Session["user"] = emp;
                Session["type"] = "employee";
                return RedirectToAction("Index", "Employee");
            }
            else if (emp == null)
            {
                var res = db.Restaurants.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (res != null)
                {
                    Session["user"] = res;
                    Session["type"] = "restaurant";
                    return RedirectToAction("Index", "Restaurant");
                }
            }
            ViewBag.Email = email;
            ViewBag.Password = password;
            ViewBag.Message = "Invalid Email or Password";
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["type"] = null;
            return RedirectToAction("Index");
        }
    }
}