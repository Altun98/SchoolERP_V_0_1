using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolERP_V_0_1
{
    public partial class StudentHome : Form
    {
        List<int > students;
        public StudentHome(List<int> frm)
        {          
            InitializeComponent();
            this.students = frm;
        }
    }
}
