using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthUsingIdentity.Models;
using AuthUsingIdentity.Models.ViewModel;

namespace AuthUsingIdentity.Controllers
{
    public class TspInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string GetTspCount()
        {
            return "Total Institution :" + db.TspInfos.ToList().Count().ToString();
        }
        public ActionResult Index(TspInfo obj)
        {
            return View(db.TspInfos.ToList());
           
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TspInfo tspInfo = db.TspInfos.Find(id);
            if (tspInfo == null)
            {
                return HttpNotFound();
            }
            return View(tspInfo);
        }

        // GET: TspInfoes/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TspName,TspPhone,TspEmail,TspAddress,SrtDate,Remarks")] TspInfo tspInfo)
        {
            if (ModelState.IsValid)
            {
                db.TspInfos.Add(tspInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tspInfo);
        }

        // GET: TspInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TspInfo tspInfo = db.TspInfos.Find(id);
            if (tspInfo == null)
            {
                return HttpNotFound();
            }
            return View(tspInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TspName,TspPhone,TspEmail,TspAddress,SrtDate,Remarks")] TspInfo tspInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tspInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tspInfo);
        }

        // GET: TspInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TspInfo tspInfo = db.TspInfos.Find(id);
            if (tspInfo == null)
            {
                return HttpNotFound();
            }
            return View(tspInfo);
        }

        // POST: TspInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TspInfo tspInfo = db.TspInfos.Find(id);
            db.TspInfos.Remove(tspInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public string TspCount()
        {
            string count = db.Database.ExecuteSqlCommand("Select count(ID)as Id from TspInfoes").ToString();
            return count;
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
