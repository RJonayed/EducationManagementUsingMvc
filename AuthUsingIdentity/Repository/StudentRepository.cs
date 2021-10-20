using AuthUsingIdentity.Models;
using AuthUsingIdentity.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AuthUsingIdentity.Repository
{  
    public class StudentRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<StudentViewModel> GetAllStudent()
        {
            List<StudentViewModel> list = db.StudentInformations.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                StName = s.StName,
                Phone = s.Phone,
                DateOfbirth = s.DateOfbirth,
                Email = s.Email,
                Addresss = s.Addresss,
                ImageName = s.ImageName,
                ImageData = s.ImageData,
                Remarks = s.Remarks
            }).ToList();
            return list;
            
        }
        public StudentInformation GetStudent(int id)
        {
            StudentInformation obj = db.StudentInformations.SingleOrDefault(s => s.StudentId == id);
            return obj;
        }
        public void SaveStudent(StudentInformation obj)
        {
            db.StudentInformations.Add(obj);
            db.SaveChanges();
        }
        public void UpdateStudent(StudentInformation Upobj)
        {
            db.Entry(Upobj).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            StudentInformation delSt =GetStudent(id);
            db.StudentInformations.Remove(delSt);
            db.SaveChanges();
            
        }
    }
}