using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalReportUsingMVCAndWebForm.ViewObjs
{
    public class EmployeeInfoViewObj
    {
        public int AdmissionID { get; set; }
        public int clsRoll { get; set; }
        public int TeacherId { get; set; }
        public int StudentsId { get; set; }
        public int CourseDtlId { get; set; }
        public int ModuleDtlId { get; set; }
        public decimal AdsCost { get; set; }
        public decimal CourseCost { get; set; }
        public System.DateTime AdsDate { get; set; }
        public string Remarks { get; set; }
        public int TspId { get; set; }
        public string StName { get; set; }
        public string CrsName { get; set; }
        public string ShiftName { get; set; }
        public string MdlName { get; set; }
        public string TchName { get; set; }

    }
}