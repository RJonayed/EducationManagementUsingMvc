using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CrystalReportUsingMVCAndWebForm.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];
            ReportDocument rd = new ReportDocument();       
            string path = Server.MapPath("~") + "Report//Rpt//" + reportParam.RptFileName;
            rd.Load(path);
            var datasource = reportParam.DataSource;
            rd.SetDataSource(datasource);
            CrystalReportViewer1.ReportSource = rd;
        }
    }
}