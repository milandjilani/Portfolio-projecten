using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolbeheer.Models
{
    internal class Updatestd
    {
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public class Student 
        {

            public int StudentID { get; set; }
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }

            public DateTime Geboortedatum { get; set; }
            public string Email { get; set; }
            public string VolledigeNaam => $"{Voornaam} {Achternaam}, {Geboortedatum:dd-MM-yyyy}, {Email}";

        }

        public ObservableCollection<Student> showstudent() 
        {
            ObservableCollection<Student> kind =
                new ObservableCollection<Student>();

            using (MySqlConnection conn = new MySqlConnection(connect)) 
            {
                conn.Open();
                string sql = "select StudentID, Voornaam, Achternaam, geboortedatum, Email from studenten ";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    kind.Add(new Student
                    {
                        StudentID = reader.GetInt32("StudentID"),
                        Voornaam = reader.GetString("Voornaam"),
                        Achternaam = reader.GetString("Achternaam"),
                        Geboortedatum = reader.GetDateTime("Geboortedatum"),
                        Email = reader.GetString("Email")
                    });

                }

            
            }
            return kind;
        
        }

        public void UpdateStudent(Student s)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = @"UPDATE studenten 
                       SET Voornaam=@v,
                           Achternaam=@a,
                           Geboortedatum=@g,
                           Email=@e
                       WHERE StudentID=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@v", s.Voornaam);
                cmd.Parameters.AddWithValue("@a", s.Achternaam);
                cmd.Parameters.AddWithValue("@g", s.Geboortedatum);
                cmd.Parameters.AddWithValue("@e", s.Email);
                cmd.Parameters.AddWithValue("@id", s.StudentID);

                cmd.ExecuteNonQuery();
            }
        }


    }
}
