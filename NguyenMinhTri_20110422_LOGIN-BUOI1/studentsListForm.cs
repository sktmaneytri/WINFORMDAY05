using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace NguyenMinhTri_20110422_LOGIN_BUOI1
{
    public partial class studentsListForm : Form
    {
        public studentsListForm()
        {
            InitializeComponent();
        }
        STUDENT student = new STUDENT();
        private void studentsListForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'myDBDataSet.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.myDBDataSet.std);
            SqlCommand command = new SqlCommand("SELECT * FROM std");
            dataGridView1.ReadOnly = true;
            //sử lí hình ảnh
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;

        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM std");
            dataGridView1.ReadOnly = true;
            //sử lí hình ảnh
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm UpdateDeleteform = new UpdateDeleteStudentForm();
            UpdateDeleteform.id_text.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            UpdateDeleteform.fname_text.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            UpdateDeleteform.lname_text.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            UpdateDeleteform.bdate_text.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if(dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Male")
            {
                UpdateDeleteform.male_btn.Checked = true;
            } else
            {
                UpdateDeleteform.female_btn.Checked = true;
            }

            UpdateDeleteform.phone_text.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            UpdateDeleteform.address_text.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //code hinh anh
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            UpdateDeleteform.pictureBox.Image = Image.FromStream(picture);
            this.Show();
            UpdateDeleteform.Show();
        }
    }
}
