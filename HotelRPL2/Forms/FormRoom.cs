using HotelRPL2.Forms;
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
    public partial class FormRoom : UserControl
    {
        int? selectedRoomId;
        HotelRPLModel db = new HotelRPLModel();

        public FormRoom()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            selectedRoomId = null;
            unlockInputs();
        }

        private void unlockInputs()
        {
            inputRoomNumber.Enabled = true;
            inputRoomType.Enabled = true;
            inputRoomFloor.Enabled = true;
            inputDescription.Enabled = true;
            btnSave.Enabled = true;
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void lockInputs()
        {
            inputRoomNumber.Enabled = false;
            inputRoomType.Enabled = false;
            inputRoomFloor.Enabled = false;
            inputDescription.Enabled = false;
            btnSave.Enabled = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            ClearInputs();
        }

        private void ClearInputs()
        {
            selectedRoomId = null;
            inputRoomNumber.Text = string.Empty;
            inputRoomType.SelectedIndex = -1;
            inputRoomFloor.Text = string.Empty;
            inputDescription.Text = string.Empty;
        }

        private void Rooms_Load(object sender, EventArgs e)
        {
            lockInputs();
            var Rooms = from r in db.Rooms
                        join rt in db.RoomTypes on r.RoomTypeId equals rt.Id
                        select new
                        {
                            r.Id,
                            r.RoomNumber,
                            r.RoomTypeId,
                            RoomType = rt.Name,
                            r.RoomFloor,
                            r.Description,
                        };

            dataGridViewRoom.DataSource = Rooms.ToList();
            dataGridViewRoom.Columns["Id"].Visible = false;
            dataGridViewRoom.Columns["RoomTypeId"].Visible = false;

            
            inputRoomType.DataSource = db.RoomTypes.ToList();
            inputRoomType.DisplayMember = "Name";
            inputRoomType.ValueMember = "Id";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == null)
            {
                MessageBox.Show("Klik kamar yang ingin diedit.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (selectedRoomId == null)
                {
                    Room room = new Room();
                    room.RoomNumber = inputRoomNumber.Text;
                    room.RoomTypeId = (int)inputRoomType.SelectedValue;
                    room.RoomFloor = inputRoomFloor.Text;
                    room.Description = inputDescription.Text;

                    db.Rooms.Add(room);
                    db.SaveChanges();
                    MessageBox.Show("Berhasil menambahkan kamar baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnLoad(e);
                }
                else
                {
                    Room room = db.Rooms.FirstOrDefault(x => x.Id == selectedRoomId);
                    if (room != null)
                    {

                        room.RoomNumber = inputRoomNumber.Text;
                        room.RoomTypeId = (int)inputRoomType.SelectedValue;
                        room.RoomFloor = inputRoomFloor.Text;
                        room.Description = inputDescription.Text;

                        db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Berhasil mengedit kamar.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e);
                    }
                }
            }
        }

        private bool ValidateInputs()
        {
            int price;
            if (
                inputRoomNumber.Text == string.Empty ||
                inputRoomType.Text == string.Empty ||
                inputRoomFloor.Text == string.Empty ||
                inputDescription.Text == string.Empty
                )
            {
                MessageBox.Show("Lengkapi semua input.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!int.TryParse(inputRoomFloor.Text, out price))
            {
                MessageBox.Show("Input lantai kamar harus berisi angka.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (selectedRoomId == null)
            {
                MessageBox.Show("Klik kamar yang ingin dihapus.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (
                MessageBox.Show("Yakin ingin menghapus kamar?", "Pesan", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK
                    )
                {
                    var room = db.Rooms.FirstOrDefault(x => x.Id == selectedRoomId);
                    if (room != null)
                    {
                        db.Rooms.Remove(room);
                        db.SaveChanges();
                        MessageBox.Show("Berhasil menambahkan kamar baru.", "Pesan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnLoad(e);
                    }
                }
            }
        }

        private void dataGridViewRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRoom.Rows[e.RowIndex];

                selectedRoomId = (int)row.Cells["Id"].Value;
                inputRoomNumber.Text = row.Cells["RoomNumber"].Value.ToString();
                inputRoomType.SelectedValue = row.Cells["RoomTypeId"].Value;
                inputRoomFloor.Text = row.Cells["RoomFloor"].Value.ToString();
                inputDescription.Text = row.Cells["Description"].Value.ToString();
            }
        }
        private void btnAddRoomType_Click(object sender, EventArgs e)
        {
            FormRoomType form = new FormRoomType(this);
            form.ShowDialog();
        }

        internal void loadRoomTypes()
        {
            inputRoomType.DataSource = db.RoomTypes.ToList();
        }
    }
}
