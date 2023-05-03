using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelRPL2.Forms
{
    public partial class FormEmployeeReport : Form
    {
        HotelRPLModel db = new HotelRPLModel();
        public FormEmployeeReport()
        {
            InitializeComponent();
        }

        private void FormEmployeeReport_Load(object sender, EventArgs e)
        {
            var employees = db.Employees.ToList();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "EmployeeReport.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("HotelRPLDataSet", employees));
            this.reportViewer1.RefreshReport();
        }
    }
}
