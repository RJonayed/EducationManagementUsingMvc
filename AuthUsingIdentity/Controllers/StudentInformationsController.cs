using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthUsingIdentity.Models;
using PagedList;

namespace AuthUsingIdentity.Controllers
{
    public class StudentInformationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //[HttpPost]
        //public ActionResult Index(string StudnetName)
        //{
        //    return View(db.StudentInformations.Select(e=>e.StName==StudnetName).ToList());
        //}
        //[HttpGet]
        //public JsonResult IsEmployeeNameAvailable(string StudentName)
        //{
        //    return Json(!db.StudentInformations.Any(e => e.StName == StudentName), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult Index(string Sorting_Order, string Search_Data)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            ViewBag.SortingDate = Sorting_Order == "Date_Enroll" ? "Date_Description" : "Date";

            var students = from stu in db.StudentInformations select stu;
            {
                students = students.Where(stu => stu.StName.ToUpper().Contains(Search_Data.ToUpper())
                    || stu.Phone.ToUpper().Contains(Search_Data.ToUpper()));
            }
            switch (Sorting_Order)
            {
                case "Name_Description":
                    students = students.OrderByDescending(stu => stu.StName);
                    break;
                case "Date_Enroll":
                    students = students.OrderBy(stu => stu.DateOfbirth);
                    break;
                case "Date_Description":
                    students = students.OrderByDescending(stu => stu.DateOfbirth);
                    break;
                default:
                    students = students.OrderBy(stu => stu.StName);
                    break;
            }

            return View(students.ToList());
        }

        //[HttpPost]
        //public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        //{
        //    System.Threading.Thread.Sleep(3000);
        //    if (SearchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        SearchString = CurrentFilter;
        //    }
        //    ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
        //    ViewBag.CurrentFilter = SearchString;
        //    db.StudentInformations.ToList();
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        db.StudentInformations.Where(e => e.StName.ToUpper().Contains(SearchString.ToUpper())).ToList();
        //    }
        //    switch (SortOrder)
        //    {
        //        case "name_desc":
        //            db.StudentInformations.OrderByDescending(e => e.StName).ToList();
        //            break;
        //        default:
        //            db.StudentInformations.OrderBy(e => e.StName).ToList();
        //            break;
        //    }
        //    //int pageSize = 3;
        //    //int pageNumber = (page ?? 1);
        //    //return View(db.StudentInformations.Select(e=>e.StName).FirstOrDefault().ToUpper.(pageNumber, pageSize));
        //    return View(db.StudentInformations.Select(e=>e.StName== SearchString));
        //}
        public ActionResult Index()
        {
            return View(db.StudentInformations.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            if (studentInformation == null)
            {
                return HttpNotFound();
            }
            return View(studentInformation);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,StName,Phone,DateOfbirth,Email,Addresss,ImageName,ImageData,Remarks")] StudentInformation studentInformation)
        {
            if (ModelState.IsValid)
            {
                db.StudentInformations.Add(studentInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentInformation);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            if (studentInformation == null)
            {
                return HttpNotFound();
            }
            return View(studentInformation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,StName,Phone,DateOfbirth,Email,Addresss,ImageName,ImageData,Remarks")] StudentInformation studentInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentInformation);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            if (studentInformation == null)
            {
                return HttpNotFound();
            }
            return View(studentInformation);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentInformation studentInformation = db.StudentInformations.Find(id);
            db.StudentInformations.Remove(studentInformation);
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
        [ChildActionOnly]
        [OutputCache(Duration = 10)]

        public string GetStudentCount()
        {
            return "Total Student :" + db.StudentInformations.ToList().Count().ToString();
        }
    }
}
