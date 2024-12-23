using SchoolERP_V_0_1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolERP_V_0_1.Forms
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            TabPageVisibleFalse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabPageVisibleTrue();
        }

        private void TabPageVisibleTrue()
        {
            if (rdbntTeacher.Checked == true)
            {
                TabPageVisibleFalse();
                tabControl1.Controls.Add(tabPage1);
            }
            if (rdbntStudent.Checked == true)
            {
                TabPageVisibleFalse();
                tabControl1.Controls.Add(tabPage2);
            }
            if (rdbntClass.Checked == true)
            {
                TabPageVisibleFalse();
                tabControl1.Controls.Add(tabPage3);
                GetInfoCombobocTeacher();
            }
        }


        private void TabPageVisibleFalse()
        {
            tabControl1.Controls.Remove(tabPage1);
            tabControl1.Controls.Remove(tabPage2);
            tabControl1.Controls.Remove(tabPage3);
        }
        void GetInfoCombobocTeacher()
        {
            string query = "select TeacherID,(Name+' '+Surname)FIO from Teachers";
            using (SqlConnection con = new SqlConnection(Halper.ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "FIO";
                comboBox1.ValueMember = "TeacherID";
                con.Close();
            }
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            GetClassesFullShow();
        }

        private void GetClassesFullShow()
        {
            Classes cla = new Classes();
            SqlConnection con = new SqlConnection(Halper.ConnectionString);
            con.Open();
            string query = "select * from Classes where IsActive=1";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                cla.ClassesID = (int)reader["ClassID"];
                cla.TeacherID = int.Parse(reader["TeacherID"].ToString());
                cla.ClassName = reader["ClassName"].ToString();
                cla.ClassCapeste = int.Parse(reader["ClassCapeste"].ToString());
                cla.Description = reader["Description"].ToString();
                cla.IsActive = true;
                dataGridView1.Rows.Add(cla.ClassesID, cla.TeacherID, cla.ClassName, cla.ClassCapeste, cla.Description, cla.IsActive);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex(@"\d+");
            Classes classes = new Classes();
            classes.TeacherID = Convert.ToInt32(comboBox1.SelectedValue);
            classes.ClassName = textBox1.Text;
            if (rx.IsMatch(textBox2.Text))
            {
                classes.ClassCapeste = int.Parse(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Sinifin tutumuna sadece tam eded daxil ede bilersiniz \n" +
                    " Zehmet olmasa duzgun formada daxil edin", "Bildiris", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            classes.Description = richTextBox1.Text;
            classes.IsActive = true;
            string querty = "insert into  Classes (TeacherID,ClassName,ClassCapeste,Description,IsActive) values (@p1,@p2,@p3,@p4,@p5)";
            using (SqlConnection con = new SqlConnection(Halper.ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand(querty, con);
                com.Parameters.AddWithValue("@p1", classes.TeacherID);
                com.Parameters.AddWithValue("@p2", classes.ClassName);
                com.Parameters.AddWithValue("@p3", classes.ClassCapeste);
                com.Parameters.AddWithValue("@p4", classes.Description);
                com.Parameters.AddWithValue("@p5", classes.IsActive);
                com.ExecuteNonQuery();
                con.Close();
            }
            dataGridView1.Rows.Clear();
            GetClassesFullShow();
        }
    }
}
