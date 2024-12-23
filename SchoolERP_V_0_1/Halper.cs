using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolERP_V_0_1
{
    public static class Halper
    {

        public static string ConnectionString = "Data Source=DESKTOP-74LA6BD;Initial Catalog=School_DB;Persist Security Info=True;User ID=sa;Password=2016;TrustServerCertificate=True";

        public static bool UserControl(string login, string password)
        {

            string query = "SELECT COUNT(*) FROM Users WHERE Userlogin = @username AND UserPassword = @password";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", login);
                cmd.Parameters.AddWithValue("@password", password);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public static List<int> UserRollers(string login, string password)
        {
            List<int> listRoll = new List<int>();
            string query = "Select top(1) UsersID from Users where UserLogin=@login and UserPassword=@password";
            string query1 = "select PrivelegesID from UserPriveleges where UserID=@P1";
            int userID = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                com.Parameters.AddWithValue("@login", login);
                com.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    userID = reader.GetInt32(0);
                }
                connection.Close();
                connection.Open();
                SqlCommand com1 = new SqlCommand(query1, connection);
                com1.Parameters.AddWithValue("@P1", userID);
                SqlDataReader reader1 = com1.ExecuteReader();
                while (reader1.Read())
                {
                    listRoll.Add(reader1.GetInt32(0));
                }
            }
            return listRoll;
        }
        public static List<int> FormControl(string login, string password)
        {
            var rollers = UserRollers(login, password);
            string query = "select FormID from FormsController where PrivelegesID=@Pr and IsActive=1";
            List<int> ListForm = new List<int>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                foreach (var item in rollers)
                {
                    com.Parameters.AddWithValue("@Pr", item);
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        ListForm.Add(reader.GetInt32(0));
                    }
                }
            }
            return ListForm;
        }
    }
}
