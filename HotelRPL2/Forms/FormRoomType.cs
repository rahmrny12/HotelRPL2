using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelRPL2
{
    public partial class FormRoomType : Form
    {
        int? selectedRoomTypeId;
        HotelRPLModel db = new HotelRPLModel();

        FormRoom formRoom;

        public FormRoomType(FormRoom formRoom)
        {
            InitializeComponent();
            this.formRoom = formRoom;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image|*.jpg;*.jpeg;*.png" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxRoomType.ImageLocation = ofd.FileName;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            selectedRoomTypeId = null;
            unlockInputs();
        }

        private void unlockInputs()
        {
            inputName.Enabled = true;
            inputCapacity.Enabled = true;
            inputRoomPrice.Enabled = true;
            btnBrowse.Enabled = true;
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void lockInputs()
        {
            inputName.Enabled = false;
            inputCapacity.Enabled = false;
            inputRoomPrice.Enabled = false;
            btnBrowse.Enabled = false;
            btnSave.Enabled = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            ClearInputs();
        }

        private void ClearInputs()
        {
            selectedRoomTypeId = null;
            inputName.Text = string.Empty;
            inputCapacity.Text = string.Empty;
            inputRoomPrice.Text = string.Empty;
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoomTypeId == null)
            {
                MessageBox.Show("Klik tipe kamar yang ingin diedit.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (selectedRoomTypeId == null)
                {
                    RoomType roomType = new RoomType();
                    roomType.Name = inputName.Text;
                    roomType.Capacity = (int)inputCapacity.Value;
                    roomType.RoomPrice = Convert.ToInt32(inputRoomPrice.Text);

                    roomType.Photo = pictureBoxRoomType.ImageLocation;
                    db.RoomTypes.Add(roomType);
                    db.SaveChanges();
                    MessageBox.Show("Berhasil menambahkan tipe kamar baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnLoad(e);
                }
                else
                {
                    RoomType roomType = db.RoomTypes.FirstOrDefault(x => x.Id == selectedRoomTypeId);
                    if (roomType != null)
                    {
                        roomType.Name = inputName.Text;
                        roomType.Capacity = (int)inputCapacity.Value;
                        roomType.RoomPrice = Convert.ToInt32(inputRoomPrice.Text);

                        roomType.Photo = pictureBoxRoomType.ImageLocation;
                        db.Entry(roomType).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Berhasil mengedit tipe kamar.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                inputCapacity.Text == string.Empty ||
                inputRoomPrice.Text == string.Empty
                )
            {
                MessageBox.Show("Lengkapi semua input.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!int.TryParse(inputRoomPrice.Text, out price))
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
            if (selectedRoomTypeId == null)
            {
                MessageBox.Show("Klik tipe kamar yang ingin dihapus.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (
                MessageBox.Show("Yakin ingin menghapus tipe kamar?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK
                    )
                {
                    var roomTypes = db.RoomTypes.FirstOrDefault(x => x.Id == selectedRoomTypeId);
                    if (roomTypes != null)
                    {
                        db.RoomTypes.Remove(roomTypes);
                        db.SaveChanges();
                        MessageBox.Show("Berhasil menambahkan tipe kamar baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e);
                    }
                }
            }
        }

        private void dataGridViewRoomType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRoomType.Rows[e.RowIndex];

                selectedRoomTypeId = (int)row.Cells["Id"].Value;
                inputName.Text = row.Cells["Name"].Value.ToString();
                inputCapacity.Text = row.Cells["Capacity"].Value.ToString();
                inputRoomPrice.Text = row.Cells["RoomPrice"].Value.ToString();

                var path = (string)row.Cells["Photo"].Value;
                if (File.Exists(path))
                {
                    pictureBoxRoomType.ImageLocation = (string)row.Cells["Photo"].Value;
                }
                else
                {
                    pictureBoxRoomType.ImageLocation = null;
                }
            }
        }

        private void FormRoomType_Load(object sender, EventArgs e)
        {
            lockInputs();
            var RoomTypes = db.RoomTypes
                .Select(x => new
            {
                x.Id,
                x.Photo,
                x.Name,
                x.Capacity,
                x.RoomPrice,
            }).ToList();

            dataGridViewRoomType.DataSource = RoomTypes;
            dataGridViewRoomType.Columns["Id"].Visible = false;
            dataGridViewRoomType.Columns["Photo"].Visible = false;
        }

        private void FormRoomType_FormClosed(object sender, FormClosedEventArgs e)
        {
            formRoom.loadRoomTypes();
        }
    }
}
