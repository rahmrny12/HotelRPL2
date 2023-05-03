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
    public partial class FormReport : UserControl
    {
        public FormReport()
        {
            InitializeComponent();
        }

        private void btnEmployeeReport_Click(object sender, EventArgs e)
        {
            FormEmployeeReport form = new FormEmployeeReport();
            form.ShowDialog();
        }
    }
}
