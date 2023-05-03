using HotelRPL2.Forms;
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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Reservation";
            panelForm.Controls.Clear();
            FormReservation form = new FormReservation();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Reservation";
            panelForm.Controls.Clear();
            FormReservation form = new FormReservation();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnRequestAdditionalItem_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Items";
            panelForm.Controls.Clear();
            FormItem form = new FormItem();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Reservation";
            panelForm.Controls.Clear();
            FormReservation form = new FormReservation();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Employee";
            panelForm.Controls.Clear();
            FormEmployee form = new FormEmployee();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnFoodAndDrink_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Foods And Drinks";
            panelForm.Controls.Clear();
            FormFoodsAndDrinks form = new FormFoodsAndDrinks();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Items";
            panelForm.Controls.Clear();
            FormItem form = new FormItem();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Room";
            panelForm.Controls.Clear();
            FormRoom form = new FormRoom();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void timerCurrentTime_Tick(object sender, EventArgs e)
        {
            labelCurrentTime.Text = DateTime.Now.ToString();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timerCurrentTime.Start();
            labelUserName.Text = "Welcome, " + LoggedInUser.name;

            labelFormTitle.Text = "Reservation";
            panelForm.Controls.Clear();
            FormReservation form = new FormReservation();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Hide();
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            labelFormTitle.Text = "Report";
            panelForm.Controls.Clear();
            FormReport form = new FormReport();
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
        }
    }
}
