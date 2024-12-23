using SchoolERP_V_0_1.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolERP_V_0_1
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Giris  ekranindan cixis etmek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Giriş ekranindan çıxmaq istəyirsiniz?", "Bildiris", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// Sisteme giris etmek emeliyyatlari
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            if (textBox1.Text != null && textBox2.Text != null)
            {
                if (Halper.UserControl(login, password) == true)
                {
                    List<int> FormEnter = Halper.FormControl(login, password);
                    foreach (var item in FormEnter)
                    {
                        if (item == 5)
                        {
                            AdminHome fr = new AdminHome(FormEnter);
                            fr.ShowDialog();                            
                        }
                        if (item == 1004)
                        {
                            StudentHome fr = new StudentHome(FormEnter);
                            fr.ShowDialog();
                        }
                        if (item == 1003)
                        {
                            TeacherHome fr = new TeacherHome(FormEnter);
                            fr.ShowDialog();
                        }                        
                    }
                }
                else
                {
                    Console.WriteLine("Istifadeci  tapilmadi!");
                }
            }
            else
            {
                MessageBox.Show("Zehmet olmasa login ve sifreni daxil edin");
            }
        }
    }
}
