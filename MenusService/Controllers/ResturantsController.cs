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
    public class ResturantsController : Controller
    {
        private MenusServiceContext db = new MenusServiceContext();

        // GET: Resturants
        public ActionResult Index()
        {
            return View(db.Resturants.ToList());
        }

        // GET: Resturants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return HttpNotFound();
            }
            return View(resturant);
        }

        // GET: Resturants/Create
        public ActionResult Create()
        {
            Resturant res = new Resturant();
            return View(res);
            
        }

        // POST: Resturants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,guid,name,imgPath,imageUpload")] Resturant resturant)
        {
            if (ModelState.IsValid)
            {
                String fName = "";
                var srcPath = @"C:\Users\sibongisenib\documents\qrp\MenusService\MenusService\Content\imgs\";
                var destPath = @"C:\Users\sibongisenib\documents\qrp\test\www\images";
                if (resturant.imageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(resturant.imageUpload.FileName);
                    string extention = Path.GetExtension(resturant.imageUpload.FileName);
                    fileName = resturant.name + DateTime.Now.ToString("yymmssfff") + extention;
                    fName = fileName;
                    resturant.imgPath = "~/Content/imgs/" + fileName;
                    resturant.imageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/imgs/"), fileName));
                }

                Helper.CopyToAppimages(srcPath, fName, destPath);

                db.Resturants.Add(resturant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resturant);
        }

        // GET: Resturants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return HttpNotFound();
            }
            return View(resturant);
        }

        // POST: Resturants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,guid,resturant,imgPath")] Resturant resturant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resturant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resturant);
        }

        // GET: Resturants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return HttpNotFound();
            }
            return View(resturant);
        }

        // POST: Resturants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            db.Resturants.Remove(resturant);
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
