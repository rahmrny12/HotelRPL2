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
    public partial class FormReservation : UserControl
    {
        HotelRPLModel db = new HotelRPLModel();
        DataTable selectedRooms = new DataTable();
        DataTable availableRooms = new DataTable();

        public FormReservation()
        {
            InitializeComponent();
            DataGridViewCheckBoxColumn btnChoose = new DataGridViewCheckBoxColumn();
            btnChoose.HeaderText = "Choose";
            btnChoose.Name = "Choose";
            dataGridViewCustomer.Columns.Insert(0, btnChoose);

            availableRooms.Columns.Add("Id");
            availableRooms.Columns.Add("RoomNumber");
            availableRooms.Columns.Add("RoomFloor");
            availableRooms.Columns.Add("RoomPrice");
            availableRooms.Columns.Add("Description");

            selectedRooms.Columns.Add("Id");
            selectedRooms.Columns.Add("RoomNumber");
            selectedRooms.Columns.Add("RoomFloor");
            selectedRooms.Columns.Add("RoomPrice");
            selectedRooms.Columns.Add("Description");
        }

        private void FormReservation_Load(object sender, EventArgs e)
        {
            LoadCustomers();

            inputRoomType.DataSource = db.RoomTypes.ToList();
            inputRoomType.DisplayMember = "Name";
            inputRoomType.ValueMember = "Id";
        }

        private void LoadCustomers()
        {
            var customers = db.Customers.Select(x => new
            {
                x.Id,
                x.Name,
                x.Email,
                x.Gender,
            });
            dataGridViewCustomer.DataSource = customers.ToList();
            dataGridViewCustomer.Columns["id"].Visible = false;
        }

        private void btnSearchCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (btnSearchCustomer.Checked)
            {
                LoadCustomers();
                dataGridViewCustomer.Visible = true;
            }
        }

        private void btnAddCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAddCustomer.Checked)
            {
                dataGridViewCustomer.Visible = false;
            }
        }

        private void btnSearchRooms_Click(object sender, EventArgs e)
        {
            availableRooms.Rows.Clear();

            var rooms = (from r in db.Rooms
                        join rt in db.RoomTypes on r.RoomTypeId equals rt.Id
                        where r.RoomTypeId == (int)inputRoomType.SelectedValue
                        select new {
                            r.Id,
                            r.RoomNumber,
                            r.RoomFloor,
                            rt.RoomPrice,
                            r.Description,
                        }).ToList();

            foreach (var room in rooms)
            {
                DataRow row = availableRooms.NewRow();
                if (!selectedRooms.AsEnumerable().Any(x => x.Field<string>("Id") == room.Id.ToString()))
                {
                    row["Id"] = room.Id;
                    row["RoomNumber"] = room.RoomNumber;
                    row["RoomFloor"] = room.RoomFloor;
                    row["RoomPrice"] = room.RoomPrice;
                    row["Description"] = room.Description;
                    availableRooms.Rows.Add(row);
                }
            }

            dataGridViewRooms.DataSource = availableRooms;
            dataGridViewRooms.Columns["Id"].Visible = false;

            dataGridViewSelectedRoom.DataSource = selectedRooms;
            dataGridViewSelectedRoom.Columns["Id"].Visible = false;
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewRooms.CurrentRow;

            if (row != null)
            {
                var room = selectedRooms.NewRow();
                room["Id"] = row.Cells["Id"].Value.ToString();
                room["RoomNumber"] = row.Cells["RoomNumber"].Value.ToString();
                room["RoomFloor"] = row.Cells["RoomFloor"].Value.ToString();
                room["RoomPrice"] = row.Cells["RoomPrice"].Value.ToString();
                room["Description"] = row.Cells["Description"].Value.ToString();
                selectedRooms.Rows.Add(room);
                selectedRooms.AcceptChanges();

                availableRooms.Rows.RemoveAt(row.Index);
                dataGridViewRooms.Refresh();

                dataGridViewSelectedRoom.Refresh(); 
            }
        }

        private void btnRemoveRoom_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewSelectedRoom.CurrentRow;

            if (row != null)
            {
                var room = availableRooms.NewRow();
                room["Id"] = row.Cells["Id"].Value.ToString();
                room["RoomNumber"] = row.Cells["RoomNumber"].Value.ToString();
                room["RoomFloor"] = row.Cells["RoomFloor"].Value.ToString();
                room["RoomPrice"] = row.Cells["RoomPrice"].Value.ToString();
                room["Description"] = row.Cells["Description"].Value.ToString();
                availableRooms.Rows.Add(room);
                availableRooms.AcceptChanges();

                selectedRooms.Rows.RemoveAt(row.Index);
                dataGridViewSelectedRoom.Refresh();

                dataGridViewRooms.Refresh();
            }
        }
    }
}
