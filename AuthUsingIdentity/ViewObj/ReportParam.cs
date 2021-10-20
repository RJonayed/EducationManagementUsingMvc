using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalReportUsingMVCAndWebForm.ViewObjs
{
    public class ReportParam<T>
    {
        public string RptFileName { get; set; }
        public string ReportTitle { get; set; }
        public List<EmployeeInfoViewObj> DataSource { get; set; }
        public bool IsPassParamToCr { get; set; }
    }
}