using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NguyenMinhTri_20110422_LOGIN_BUOI1
{
    public partial class UpdateDeleteStudentForm : Form
    {   
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }
        STUDENT student = new STUDENT();
        private void search_btn_Click(object sender, EventArgs e)
        {
            int id = int.Parse(id_text.Text);
            SqlCommand command = new SqlCommand("SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE id = " + id);
            DataTable table = student.getStudents(command);
            if(table.Rows.Count > 0)
            {
                fname_text.Text = table.Rows[0]["fname"].ToString();
                lname_text.Text = table.Rows[0]["lname"].ToString();
                bdate_text.Value = (DateTime)table.Rows[0]["bdate"];
                if(table.Rows[0]["gender"].ToString() == "Female")
                {
                    male_btn.Checked = true;
                } else
                {
                    female_btn.Checked = true;
                }

                phone_text.Text = table.Rows[0]["phone"].ToString();
                address_text.Text = table.Rows[0]["address"].ToString();

                byte[] pic = (byte[])table.Rows[0]["picture"];
                MemoryStream picture = new MemoryStream(pic);
                pictureBox.Image = Image.FromStream(picture);


            } else
            {
                MessageBox.Show("not found", "Find student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //remove student
        private void remove_btn_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("You want delete this student ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                try
                {
                    if (student.DeleteStudent(Int32.Parse(id_text.Text)))
                    {
                        MessageBox.Show("Delete succesfully", "Detele student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error!", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        

        //check information
        public bool checkInfor()
        {
            if (id_text.Text == "")
            {
                MessageBox.Show("Input the id ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id_text.Focus();
                return false;
            }
            if (fname_text.Text == "")
            {
                MessageBox.Show("Input the first name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fname_text.Focus();
                return false;
            }
            if (lname_text.Text == "")
            {
                MessageBox.Show("Input the last name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lname_text.Focus();
                return false;
            }
            if (phone_text.Text == "")
            {
                MessageBox.Show("Input the phone", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                phone_text.Focus();
                return false;
            }
            if (address_text.Text == "")
            {
                MessageBox.Show("Input the address", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                address_text.Focus();
                return false;
            }
            return true;
        }
        //update student
        private void edit_btn_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(id_text.Text);
            string fname = fname_text.Text;
            string lname = lname_text.Text;
            DateTime bdate = bdate_text.Value;
            string phone = phone_text.Text;
            string adrs = address_text.Text;
            string gender = "Male";
            if (female_btn.Checked)
            {
                gender = "Female";
            }
            MemoryStream PIC = new MemoryStream();
            try
            {
                if (student.EditStudent(id, fname, lname, bdate, phone, adrs, gender, PIC))
                {
                    MessageBox.Show("Delete succesfully", "Detele student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error!", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show("Edit / Update information succesfully", "Edit student", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
