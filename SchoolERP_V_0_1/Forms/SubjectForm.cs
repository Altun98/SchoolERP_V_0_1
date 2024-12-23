using SchoolERP_V_0_1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolERP_V_0_1.Forms
{
    public partial class SubjectForm : Form
    {
        DataTable table = new DataTable();
        int index;
        int? SubjectID;
        public SubjectForm()
        {
            InitializeComponent();
            SubjectShow();
        }

        private void SubjectShow()
        {
            Subject sub = new Subject();
            SqlConnection con = new SqlConnection(Halper.ConnectionString);
            con.Open();
            string query = "select * from Subject where IsActive=1";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                sub.SubjectID = (int)reader["SubjectID"];
                sub.SubjectName = reader["SubjectName"].ToString();
                sub.Description = reader["Description"].ToString();
                sub.IsActive = true;
                dataGridView1.Rows.Add(sub.SubjectID, sub.SubjectName, sub.Description, sub.IsActive);
            }
            con.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Subject sbj = new Subject();
                DataGridViewRow row = dataGridView1.Rows[index];
                sbj.SubjectID = (int)row.Cells[0].Value;
                sbj.SubjectName = row.Cells[1].Value.ToString();
                sbj.Description = row.Cells[2].Value.ToString();
                sbj.IsActive = true;
                txtSubjectName.Text = sbj.SubjectName;
                txtSubjectDescription.Text = sbj.Description;
                SubjectID = sbj.SubjectID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (SubjectID > 0)
                {
                    using (SqlConnection con = new SqlConnection(Halper.ConnectionString))
                    {
                        con.Open();
                        string querty = "update Subject set SubjectName = @subName,Description = @subDesc,IsActive = 1 where SubjectID = @subID";
                        SqlCommand com = new SqlCommand(querty, con);
                        com.Parameters.AddWithValue("@subName", txtSubjectName.Text);
                        com.Parameters.AddWithValue("@subDesc", txtSubjectDescription.Text);
                        com.Parameters.AddWithValue("@subID", SubjectID);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    dataGridView1.Rows.Clear();
                    SubjectShow();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.StackTrace);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntAdd_Click(object sender, EventArgs e)
        {
            Subject sbj = new Subject();
            sbj.SubjectName = txtSubjectName.Text;
            sbj.Description = txtSubjectDescription.Text;
            sbj.IsActive = true;
            string query = "insert into Subject (SubjectName,Description,IsActive) values(@p1,@p2,@p3)";
            using (SqlConnection con = new SqlConnection(Halper.ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@p1", sbj.SubjectName);
                com.Parameters.AddWithValue("@p2", sbj.Description);
                com.Parameters.AddWithValue("@p3", sbj.IsActive);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show($"{sbj.SubjectName} fəəni əlavə edildi");
                dataGridView1.Rows.Clear();
                SubjectShow();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SubjectID > 0)
            {
                using (SqlConnection con = new SqlConnection(Halper.ConnectionString))
                {
                    con.Open();
                    string querty = "update Subject set IsActive = @IsActive where SubjectID = @subID";
                    SqlCommand com = new SqlCommand(querty, con);
                    com.Parameters.AddWithValue("@IsActive", false);
                    com.Parameters.AddWithValue("@subID", SubjectID);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                dataGridView1.Rows.Clear();
                SubjectShow();
            }
        }
    }
}
