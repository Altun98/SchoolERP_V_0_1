using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP_V_0_1.Models
{
    public class Classes
    {
        public int ClassesID { get; set; }
        public int TeacherID { get; set; }
        public string ClassName { get; set; }
        public int ClassCapeste { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

