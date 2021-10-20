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
    public class ModuleDtlsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var moduleDtls = db.ModuleDtls.Include(m => m.CourseDtl);
            return View(moduleDtls.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDtl moduleDtl = db.ModuleDtls.Find(id);
            if (moduleDtl == null)
            {
                return HttpNotFound();
            }
            return View(moduleDtl);
        }

        public ActionResult Create()
        {
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleDtlId,CourseDtlId,MdlName,MdlFees,Remarks")] ModuleDtl moduleDtl)
        {
            if (ModelState.IsValid)
            {
                db.ModuleDtls.Add(moduleDtl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", moduleDtl.CourseDtlId);
            return View(moduleDtl);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDtl moduleDtl = db.ModuleDtls.Find(id);
            if (moduleDtl == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", moduleDtl.CourseDtlId);
            return View(moduleDtl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleDtlId,CourseDtlId,MdlName,MdlFees,Remarks")] ModuleDtl moduleDtl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleDtl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", moduleDtl.CourseDtlId);
            return View(moduleDtl);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDtl moduleDtl = db.ModuleDtls.Find(id);
            if (moduleDtl == null)
            {
                return HttpNotFound();
            }
            return View(moduleDtl);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModuleDtl moduleDtl = db.ModuleDtls.Find(id);
            db.ModuleDtls.Remove(moduleDtl);
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
