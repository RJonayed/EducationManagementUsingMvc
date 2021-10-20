using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthUsingIdentity.Models;

namespace AuthUsingIdentity.Controllers
{
    public class CourseDtlsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var courseDtls = db.CourseDtls.Include(c => c.Shift);
            return View(courseDtls.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseDtl courseDtl = db.CourseDtls.Find(id);
            if (courseDtl == null)
            {
                return HttpNotFound();
            }
            return View(courseDtl);
        }

        public ActionResult Create()
        {
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseDtlId,CrsName,CrsDuratin,ShiftId,ShiftName,CrsFee,Remarks,IsActive")] CourseDtl courseDtl)
        {
            if (ModelState.IsValid)
            {
                db.CourseDtls.Add(courseDtl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", courseDtl.ShiftId);
            return View(courseDtl);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseDtl courseDtl = db.CourseDtls.Find(id);
            if (courseDtl == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", courseDtl.ShiftId);
            return View(courseDtl);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseDtlId,CrsName,CrsDuratin,ShiftId,ShiftName,CrsFee,Remarks,IsActive")] CourseDtl courseDtl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseDtl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", courseDtl.ShiftId);
            return View(courseDtl);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseDtl courseDtl = db.CourseDtls.Find(id);
            if (courseDtl == null)
            {
                return HttpNotFound();
            }
            return View(courseDtl);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseDtl courseDtl = db.CourseDtls.Find(id);
            db.CourseDtls.Remove(courseDtl);
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
