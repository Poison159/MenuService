using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MenusService.Models;
using System.IO;

namespace MenusService.Controllers
{
    public class Meals1Controller : Controller
    {
        private MenusServiceContext db = new MenusServiceContext();

        // GET: Meals1
        public ActionResult Index()
        {
            return View(db.Meals.ToList());
        }

        // GET: Meals1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // GET: Meals1/Create
        public ActionResult Create()
        {
            ViewBag.resId = new SelectList(db.Resturants, "id", "name");
            Meal player = new Meal();
            return View(player);
        }

        // POST: Meals1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,resturantId,price,name,description," +
            "startDate,endDate,imgPath,imageUpload")] Meal meal)
        {
            ViewBag.resId = new SelectList(db.Resturants, "id", "name", meal.resturantId);

            if (ModelState.IsValid)
            {
                if (meal.imageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(meal.imageUpload.FileName);
                    string extention = Path.GetExtension(meal.imageUpload.FileName);
                    fileName = meal.name + extention;
                    meal.imgPath = "~/Content/imgs/" + fileName;
                    meal.imageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/imgs/"), fileName));
                }

                db.Meals.Add(meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal);
        }

        // GET: Meals1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,resId,price,name,description,startDate,endDate,imgPath")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal);
        }

        // GET: Meals1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.Meals.Find(id);
            db.Meals.Remove(meal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
