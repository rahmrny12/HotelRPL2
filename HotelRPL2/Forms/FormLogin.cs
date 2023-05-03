using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelRPL2
{
    public partial class FormLogin : Form
    {
        HotelRPLModel db = new HotelRPLModel();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Employee employee = db.Employees.Where(x => x.Username == inputUsername.Text && x.Password == inputPassword.Text).FirstOrDefault();
            if (employee != null)
            {
                LoggedInUser.username = employee.Username;
                LoggedInUser.name = employee.Name;
                LoggedInUser.email = employee.Email;
                LoggedInUser.jobId = employee.JobId;

                FormMain formMain = new FormMain();
                formMain.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Username atau password tidak sesuai...", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
