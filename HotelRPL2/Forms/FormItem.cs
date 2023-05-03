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
    public partial class FormItem : UserControl
    {
        int? selectedItemId;
        HotelRPLModel db = new HotelRPLModel();

        public FormItem()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            selectedItemId = null;
            unlockInputs();
        }

        private void unlockInputs()
        {
            inputName.Enabled = true;
            inputRequestPrice.Enabled = true;
            inputCompensationFee.Enabled = true;
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void lockInputs()
        {
            inputName.Enabled = false;
            inputRequestPrice.Enabled = false;
            inputCompensationFee.Enabled = false;
            btnSave.Enabled = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            ClearInputs();
        }

        private void ClearInputs()
        {
            selectedItemId = null;
            inputName.Text = string.Empty;
            inputRequestPrice.Text = string.Empty;
            inputCompensationFee.Text = string.Empty;
        }

        private void Items_Load(object sender, EventArgs e)
        {
            lockInputs();
            var Items = from r in db.Items
                        select new
                        {
                            r.Id,
                            r.Name,
                            r.RequestPrice,
                            r.CompensationFee,
                        };

            dataGridViewItem.DataSource = Items.ToList();
            dataGridViewItem.Columns["Id"].Visible = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedItemId == null)
            {
                MessageBox.Show("Klik item yang ingin diedit.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (selectedItemId == null)
                {
                    Item item = new Item();
                    item.Name = inputName.Text;
                    item.RequestPrice = Convert.ToInt32(inputRequestPrice.Text);
                    item.CompensationFee = Convert.ToInt32(inputCompensationFee.Text);

                    db.Items.Add(item);
                    db.SaveChanges();
                    MessageBox.Show("Berhasil menambahkan item baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnLoad(e);
                }
                else
                {
                    Item item = db.Items.FirstOrDefault(x => x.Id == selectedItemId);
                    if (item != null)
                    {
                        item.Name = inputName.Text;
                        item.RequestPrice = Convert.ToInt32(inputRequestPrice.Text);
                        item.CompensationFee = Convert.ToInt32(inputCompensationFee.Text);

                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Berhasil mengedit item.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                inputRequestPrice.Text == string.Empty ||
                inputCompensationFee.Text == string.Empty
                )
            {
                MessageBox.Show("Lengkapi semua input.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!int.TryParse(inputRequestPrice.Text, out price) && !int.TryParse(inputCompensationFee.Text, out price))
            {
                MessageBox.Show("Input harga item harus berisi angka.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (selectedItemId == null)
            {
                MessageBox.Show("Klik item yang ingin dihapus.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (
                MessageBox.Show("Yakin ingin menghapus item?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK
                    )
                {
                    var Item = db.Items.FirstOrDefault(x => x.Id == selectedItemId);
                    if (Item != null)
                    {
                        db.Items.Remove(Item);
                        db.SaveChanges();
                        MessageBox.Show("Berhasil menambahkan item baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e);
                    }
                }
            }
        }

        private void dataGridViewItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewItem.Rows[e.RowIndex];

                selectedItemId = (int)row.Cells["Id"].Value;
                inputName.Text = row.Cells["Name"].Value.ToString();
                inputRequestPrice.Text = row.Cells["RequestPrice"].Value.ToString();
                inputCompensationFee.Text = row.Cells["CompensationFee"].Value.ToString();
            }
        }
    }
}
