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
    public class ResultMakesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var resultMakes = db.ResultMakes.Include(r => r.CourseDtl).Include(r => r.ModuleDtl).Include(r => r.StudentInformation);
            return View(resultMakes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultMake resultMake = db.ResultMakes.Find(id);
            if (resultMake == null)
            {
                return HttpNotFound();
            }
            return View(resultMake);
        }

        public ActionResult Create()
        {
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName");
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName");
            ViewBag.StudentId = new SelectList(db.StudentInformations, "StudentId", "StName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultMakeId,StudentId,MdlMarks,MdlPoint,MdlGrade,ResultDate,Remarks,CourseDtlId,ModuleDtlId")] ResultMake resultMake)
        {
            if (ModelState.IsValid)
            {
                db.ResultMakes.Add(resultMake);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", resultMake.CourseDtlId);
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName", resultMake.ModuleDtlId);
            ViewBag.StudentId = new SelectList(db.StudentInformations, "StudentId", "StName", resultMake.StudentId);
            return View(resultMake);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultMake resultMake = db.ResultMakes.Find(id);
            if (resultMake == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", resultMake.CourseDtlId);
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName", resultMake.ModuleDtlId);
            ViewBag.StudentId = new SelectList(db.StudentInformations, "StudentId", "StName", resultMake.StudentId);
            return View(resultMake);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultMakeId,StudentId,MdlMarks,MdlPoint,MdlGrade,ResultDate,Remarks,CourseDtlId,ModuleDtlId")] ResultMake resultMake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultMake).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", resultMake.CourseDtlId);
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName", resultMake.ModuleDtlId);
            ViewBag.StudentId = new SelectList(db.StudentInformations, "StudentId", "StName", resultMake.StudentId);
            return View(resultMake);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultMake resultMake = db.ResultMakes.Find(id);
            if (resultMake == null)
            {
                return HttpNotFound();
            }
            return View(resultMake);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultMake resultMake = db.ResultMakes.Find(id);
            db.ResultMakes.Remove(resultMake);
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
