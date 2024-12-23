using SchoolERP_V_0_1.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolERP_V_0_1
{
    public partial class AdminHome : Form
    {
        List<int> list = new List<int>();
        public AdminHome(List<int> frm)
        {

            InitializeComponent();
            this.list = frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (int i in list)
            {
                if (i == 1005)
                {
                    RegistrationForm fr = new RegistrationForm();
                    fr.ShowDialog();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Çıxmaq istəyirsiniz?", "Bildiris", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubjectForm fr = new SubjectForm();
            fr.ShowDialog();
        }
    }
}
