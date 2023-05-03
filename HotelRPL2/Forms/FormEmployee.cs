using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HotelRPL2.Forms
{
    public partial class FormEmployee : UserControl
    {
        HotelRPLModel db = new HotelRPLModel();
        int? selectedEmployeeId;
        public FormEmployee()
        {
            InitializeComponent();
            inputJob.DataSource = db.Jobs.ToList();
            inputJob.DisplayMember = "Name";
            inputJob.ValueMember = "Id";
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            var employees = from em in db.Employees
                            join j in db.Jobs
                            on em.JobId equals j.Id
                            select new
                            {
                                em.Id,
                                em.JobId,
                                em.Photo,
                                em.Username,
                                em.Password,
                                em.Name,
                                em.Email,
                                em.DateOfBirth,
                                JobName = j.Name,
                                em.Address,
                            };
            dataGridViewEmployee.DataSource = employees.ToList();
            dataGridViewEmployee.Columns["Id"].Visible = false;
            dataGridViewEmployee.Columns["JobId"].Visible = false;
            dataGridViewEmployee.Columns["Photo"].Visible = false;
            dataGridViewEmployee.Columns["Password"].Visible = false;
            lockInputs();
        }

        private void lockInputs()
        {
            inputUsername.Enabled = false;
            inputPassword.Enabled = false;
            inputConfirmPass.Enabled = false;
            inputName.Enabled = false;
            inputEmail.Enabled = false;
            inputDateOfBirth.Enabled = false;
            inputJob.Enabled = false;
            inputAddress.Enabled = false;
            btnBrowse.Enabled = false;
            btnSave.Enabled = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;


            clearInputs();
        }
        private void unlockInputs()
        {
            inputUsername.Enabled = true;
            inputPassword.Enabled = true;
            inputConfirmPass.Enabled = true;
            inputName.Enabled = true;
            inputEmail.Enabled = true;
            inputDateOfBirth.Enabled = true;
            inputJob.Enabled = true;
            inputAddress.Enabled = true;
            btnBrowse.Enabled = true;
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;



        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            selectedEmployeeId = null;
            unlockInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Klik karyawan yang ingin diedit.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                unlockInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Klik karyawan yang ingin dihapus.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (
                MessageBox.Show("Yakin ingin menghapus karyawan?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK
                    )
                {
                    var employee = db.Employees.FirstOrDefault(x => x.Id == selectedEmployeeId);
                    if (employee != null)
                    {
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                        MessageBox.Show("Berhasil menambahkan karyawan baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lockInputs();
        }

        private void clearInputs()
        {
            selectedEmployeeId = null;
            inputUsername.Text = string.Empty;
            inputPassword.Text = string.Empty;
            inputConfirmPass.Text = string.Empty;
            inputName.Text = string.Empty;
            inputEmail.Text = string.Empty;
            inputJob.SelectedIndex = -1;
            inputDateOfBirth.Text = string.Empty;
            inputAddress.Text = string.Empty;
            pictureBoxPhoto.Image = null;
        }

        private void dataGridViewEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewEmployee.Rows[e.RowIndex];

                selectedEmployeeId = (int)row.Cells["Id"].Value;
                inputUsername.Text = (string)row.Cells["Username"].Value;
                inputPassword.Text = (string)row.Cells["Password"].Value;
                inputConfirmPass.Text = (string)row.Cells["Password"].Value;
                inputName.Text = (string)row.Cells["Name"].Value;
                inputEmail.Text = (string)row.Cells["Email"].Value;
                inputDateOfBirth.Value = (DateTime)row.Cells["DateOfBirth"].Value;
                inputJob.SelectedValue = row.Cells["JobId"].Value;
                inputAddress.Text = (string)row.Cells["Address"].Value;

                var path = (string)row.Cells["Photo"].Value;
                if (File.Exists(path))
                {
                    pictureBoxPhoto.ImageLocation = (string)row.Cells["Photo"].Value;
                } else
                {
                    pictureBoxPhoto.ImageLocation = null;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (selectedEmployeeId == null)
                {
                    Employee employee = new Employee();
                    employee.Username = inputUsername.Text;
                    employee.Password = inputPassword.Text;
                    employee.Name = inputName.Text;
                    employee.Email = inputEmail.Text;
                    employee.DateOfBirth = inputDateOfBirth.Value;
                    employee.JobId = (int)inputJob.SelectedValue;
                    employee.Address = inputAddress.Text;
                    employee.Photo = pictureBoxPhoto.ImageLocation;
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    MessageBox.Show("Berhasil menambahkan karyawan baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnLoad(e);
                }
                else
                {
                    Employee employee = db.Employees.FirstOrDefault(x => x.Id == selectedEmployeeId);
                    employee.Username = inputUsername.Text;
                    employee.Password = inputPassword.Text;
                    employee.Name = inputName.Text;
                    employee.Email = inputEmail.Text;
                    employee.DateOfBirth = inputDateOfBirth.Value;
                    employee.JobId = (int)inputJob.SelectedValue;
                    employee.Address = inputAddress.Text;
                    employee.Photo = pictureBoxPhoto.ImageLocation;
                    db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Berhasil mengedit karyawan.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnLoad(e);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (
                inputUsername.Text == string.Empty ||
                inputPassword.Text == string.Empty ||
                inputName.Text == string.Empty ||
                inputEmail.Text == string.Empty ||
                inputJob.SelectedValue == null ||
                inputAddress.Text == string.Empty
                )
            {
                MessageBox.Show("Lengkapi semua input.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (
                inputPassword.Text != inputConfirmPass.Text
                )
            {
                MessageBox.Show("Konfirmasi password tidak sesuai.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image|*.jpg;*.png;*.jpg" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxPhoto.ImageLocation = ofd.FileName;
                }
            }
        }
    }
}
