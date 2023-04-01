using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHungerAssignment.Models;

namespace ZeroHungerAssignment.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurent
        public ActionResult Index()
        {
            if (Session["user"] == null || Session["type"].ToString() != "restaurant") return RedirectToAction("Index", "Home");

            var db = new ZeroDB();
            var user = Session["user"] as Restaurant;
            var CollectionRequest = db.Collection_Requests.Where(x => x.RestaurantID == user.Id && x.Status == "Pending").ToList();
            return View(CollectionRequest);
        }
        public ActionResult Create()
        {
            if (Session["user"] == null || Session["type"].ToString() != "restaurant") return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult Details(int id)
        {
            if (Session["user"] == null || Session["type"].ToString() != "restaurant") return RedirectToAction("Index", "Home");
            var db = new ZeroDB();
            var collection = db.Collection_Requests.Find(id);
            return View(collection);
        }
        public ActionResult Edit(int id)
        {
            if (Session["user"] == null || Session["type"].ToString() != "restaurant") return RedirectToAction("Index", "Home");
            var db = new ZeroDB();
            var collection = db.Collection_Requests.Find(id);
            return View(collection);
        }
        public ActionResult Delete(int id)
        {
            if (Session["user"] == null || Session["type"].ToString() != "restaurant") return RedirectToAction("Index", "Home");
            var db = new ZeroDB();
            var collection = db.Collection_Requests.Find(id);
            db.Collection_Requests.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(Collection_Request collection)
        {
            var db = new ZeroDB();
            var user = Session["user"] as Restaurant;
            var donation = new Collection_Request
            {
                RestaurantID = user.Id,
                Food_Details = collection.Food_Details,
                Quantity = collection.Quantity,
                Status = "Pending"
            };
            db.Collection_Requests.Add(donation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Collection_Request _collection)
        {
            var db = new ZeroDB();
            var collection = db.Collection_Requests.Find(_collection.Id);
            collection.Food_Details = _collection.Food_Details;
            collection.Quantity = _collection.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult History()
        {
            if (Session["user"] == null || Session["type"].ToString() != "restaurant") return RedirectToAction("Index", "Home");
            var db = new ZeroDB();
            var user = Session["user"] as Restaurant;
            var history = db.Histories.Where(x => x.Collection_Request.EmployeeID == user.Id).ToList();
            return View(history);
        }
    }

}