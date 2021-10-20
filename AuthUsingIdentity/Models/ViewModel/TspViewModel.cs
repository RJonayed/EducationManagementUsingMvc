using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AuthUsingIdentity.Models.ViewModel
{
    public class TspViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<TspInfo> GetAllTsp()
        {
            List<TspInfo> list = db.TspInfos.Select(s => new TspInfo
            {
                ID = s.ID,
                TspName = s.TspName,
                TspPhone = s.TspPhone,
                TspEmail = s.TspEmail,
                TspAddress = s.TspAddress,
                SrtDate = s.SrtDate,
                Remarks = s.Remarks

            }).ToList();
            return list;
        }

        public TspInfo GetTsp(int id)
        {
            TspInfo obj = db.TspInfos.SingleOrDefault(e => e.ID == id);
            return obj;
        }
        public void SaveTsp(TspInfo obj)
        {
            db.TspInfos.Add(obj);
            db.SaveChanges();
        }
        public void UpdateTYsp(TspInfo Upobj)
        {
            db.Entry(Upobj).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteTsp(int id)
        {
            TspInfo delTsp = GetTsp(id);
            db.TspInfos.Remove(delTsp);
            db.SaveChanges();
        }
    }
}