using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            User u = new User();
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            var db = new ZeroHungerEntities();
            var ext = (from user in db.Users
                       where user.UserName == u.UserName || user.Password == u.Password
                       select user).SingleOrDefault();


               if (ext.UserName==u.UserName && ext.UserType=="Restaurant" && ext.Password==u.Password)
               {
                Session["restaurant"] = ext.UserName;
                return RedirectToAction("Restaurant");
            }
              if(ext.UserName == u.UserName && ext.UserType == "Admin" && ext.Password == u.Password)
            {
                Session["admin"] = ext.UserName;
                return RedirectToAction("Admin");
            }
            if (ext.UserName == u.UserName && ext.UserType == "Employee" && ext.Password == u.Password)
            {
                Session["employee"] = ext.UserName;
                return RedirectToAction("Employee");
            }
            else
              {
                ViewBag.msg = "Invalid User";
              return View("Index");
              }

            //return View("Index");
        }

        [HttpGet]
        public ActionResult Restaurant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Restaurant(Request request)
        {
            var db = new ZeroHungerEntities();
            db.Requests.Add(request);
            db.SaveChanges();
            TempData["msg"] = "Request Successfully";
            return RedirectToAction("Index");
        }
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (user == null)
            {
                TempData["msg"] = "Inserted Failed";
                return View();
            }
            else
            {
                var db = new ZeroHungerEntities();
                db.Users.Add(user);
                db.SaveChanges();
                TempData["msg"] = "Inserted Successfully";
                return RedirectToAction("Index");
            }
        }

        // GET: User/Edit/5
        public ActionResult Admin()
        {

            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        [HttpPost]
        public ActionResult Employee()
        {
            var db=new ZeroHungerEntities();
            var requests=db.Requests.ToList();
            return View(requests);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
//@HttpContext.Current.Session["restaurant"].ToString()
//@HttpContext.Current.Session["admin"].ToString()
//@HttpContext.Current.Session["employee"].ToString()