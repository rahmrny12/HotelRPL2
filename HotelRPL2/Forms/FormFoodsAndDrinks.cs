using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelRPL2
{
    public partial class FormFoodsAndDrinks : UserControl
    {
        int? selectedMenuId;
        HotelRPLModel db = new HotelRPLModel();

        public FormFoodsAndDrinks()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image|*.jpg;*.jpeg;*.png" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxMenu.ImageLocation = ofd.FileName;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            selectedMenuId = null;
            unlockInputs();
        }

        private void unlockInputs()
        {
            inputName.Enabled = true;
            inputType.Enabled = true;
            inputPrice.Enabled = true;
            btnBrowse.Enabled = true;
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void lockInputs()
        {
            inputName.Enabled = false;
            inputType.Enabled = false;
            inputPrice.Enabled = false;
            btnBrowse.Enabled = false;
            btnSave.Enabled = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            ClearInputs();
        }

        private void ClearInputs()
        {
            selectedMenuId = null;
            inputName.Text = string.Empty;
            inputPrice.Text = string.Empty;
        }

        private void FoodsAndDrinks_Load(object sender, EventArgs e)
        {
            lockInputs();
            var foodsAndDrinks = db.FoodsAndDrinks.Select(x => new
            {
                x.Id,
                x.Photo,
                x.Name,
                Type = x.Type == "1" ? "Food" : "Drink",
                x.Price,
            }).ToList();

            dataGridViewMenu.DataSource = foodsAndDrinks;
            dataGridViewMenu.Columns["Id"].Visible = false;
            dataGridViewMenu.Columns["Photo"].Visible = false;

            var items = new[] {
                new { Text = "Food", Value = "1" },
                new { Text = "Drink", Value = "2" }
            };

            inputType.DataSource = items;
            inputType.DisplayMember = "Text";
            inputType.ValueMember = "Value";

        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public ComboBoxItem(string text, object value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMenuId == null)
            {
                MessageBox.Show("Klik menu yang ingin diedit.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                unlockInputs();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (selectedMenuId == null)
                {
                    FoodsAndDrink foodsAndDrink = new FoodsAndDrink();
                    foodsAndDrink.Name = inputName.Text;
                    foodsAndDrink.Type = (string)inputType.SelectedValue;
                    foodsAndDrink.Price = Convert.ToInt32(inputPrice.Text);

                    foodsAndDrink.Photo = pictureBoxMenu.ImageLocation;
                    db.FoodsAndDrinks.Add(foodsAndDrink);
                    db.SaveChanges();
                    MessageBox.Show("Berhasil menambahkan menu baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnLoad(e);
                }
                else
                {
                    FoodsAndDrink foodsAndDrink = db.FoodsAndDrinks.FirstOrDefault(x => x.Id == selectedMenuId);
                    if (foodsAndDrink != null)
                    {
                        foodsAndDrink.Name = inputName.Text;
                        foodsAndDrink.Type = (string)inputType.SelectedValue;
                        foodsAndDrink.Price = Convert.ToInt32(inputPrice.Text);

                        foodsAndDrink.Photo = pictureBoxMenu.ImageLocation;
                        db.Entry(foodsAndDrink).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Berhasil mengedit menu.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e); 
                    }
                }
            }
        }

        private bool ValidateInputs()
        {
            int price;
            if (
                inputName.Text == string.Empty ||
                inputType.Text == string.Empty ||
                inputPrice.Text == string.Empty
                )
            {
                MessageBox.Show("Lengkapi semua input.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } else if (!int.TryParse(inputPrice.Text, out price))
            {
                MessageBox.Show("Harga harus berisi angka.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lockInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMenuId == null)
            {
                MessageBox.Show("Klik menu yang ingin dihapus.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (
                MessageBox.Show("Yakin ingin menghapus menu?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK
                    )
                {
                    var menu = db.Employees.FirstOrDefault(x => x.Id == selectedMenuId);
                    if (menu != null)
                    {
                        db.Employees.Remove(menu);
                        db.SaveChanges();
                        MessageBox.Show("Berhasil menambahkan menu baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e);
                    }
                }
            }
        }

        private void dataGridViewMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewMenu.Rows[e.RowIndex];

                selectedMenuId = (int)row.Cells["Id"].Value;
                inputName.Text = (string)row.Cells["Name"].Value;
                inputType.Text = (string)row.Cells["Type"].Value;
                inputPrice.Text = row.Cells["Price"].Value.ToString();

                var path = (string)row.Cells["Photo"].Value;
                if (File.Exists(path))
                {
                    pictureBoxMenu.ImageLocation = (string)row.Cells["Photo"].Value;
                }
                else
                {
                    pictureBoxMenu.ImageLocation = null;
                }
            }
        }
    }
}
