using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AuthUsingIdentity.Models;
using CrystalReportUsingMVCAndWebForm.ViewObjs;

namespace AuthUsingIdentity.Controllers
{
    public class AdmissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var admissions = db.Admissions.Include(a => a.CourseDtl).Include(a => a.ModuleDtl).Include(a => a.Teacher);
            return View(admissions.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }
        public ActionResult Create()
        {
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName");
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TchName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdmissionID,clsRoll,TeacherId,StudentsId,CourseDtlId,ModuleDtlId,AdsCost,CourseCost,AdsDate,Remarks,TspId")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Admissions.Add(admission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", admission.CourseDtlId);
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName", admission.ModuleDtlId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TchName", admission.TeacherId);
            return View(admission);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", admission.CourseDtlId);
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName", admission.ModuleDtlId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TchName", admission.TeacherId);
            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdmissionID,clsRoll,TeacherId,StudentsId,CourseDtlId,ModuleDtlId,AdsCost,CourseCost,AdsDate,Remarks,TspId")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseDtlId = new SelectList(db.CourseDtls, "CourseDtlId", "CrsName", admission.CourseDtlId);
            ViewBag.ModuleDtlId = new SelectList(db.ModuleDtls, "ModuleDtlId", "MdlName", admission.ModuleDtlId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TchName", admission.TeacherId);
            return View(admission);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admission admission = db.Admissions.Find(id);
            db.Admissions.Remove(admission);
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

        public string GetEmployeeCount()
        {
            return "Total Admission :" + db.Admissions.ToList().Count().ToString();
        }
        public ActionResult EmployeeReport()
        {
            return View();
        }
        public void GenerateEmployeeReport()
        {
            ReportParam<EmployeeInfoViewObj> reportParam = new ReportParam<EmployeeInfoViewObj>();
            reportParam.DataSource = GetAllData();
            reportParam.RptFileName = "EmployeeReport.rpt";
            this.HttpContext.Session["ReportType"] = "EmployeeReport";
            this.HttpContext.Session["ReportParam"] = reportParam;
        }
        public List<EmployeeInfoViewObj> GetAllData()
        {
            string conStr = ConfigurationManager.ConnectionStrings["StudentDBContext"].ConnectionString;
            DataTable dt = new DataTable();
            string sqlQuery = "Select a.AdmissionID,a.AdsDate,s.StName,a.CourseCost,c.CrsName,c.CrsFee,a.Remarks,m.MdlName,a.AdsCost,t.TchName from Admissions a,StudentInformations s,CourseDtls c, ModuleDtls m,Teachers t,Shifts sc where a.StudentsId = s.StudentId and a.CourseDtlId = c.CourseDtlId and a.ModuleDtlId = m.ModuleDtlId and a.TeacherId = t.TeacherId";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            var list = ConverDataTableToList<EmployeeInfoViewObj>(dt);
            return list;
        }
        public static List<T> ConverDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow row)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in row.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, row[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
