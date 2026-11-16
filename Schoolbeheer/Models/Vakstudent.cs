using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolbeheer.Models
{
    internal class Vakstudent
    {
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public class vak
        {
            public int VakID { get; set; }
            public int studiepunten { get; set; }
            public string Vaknaam { get; set; }


            public string Volledig => $"{Vaknaam}, aantal SP: {studiepunten}";

        }

        public class Docent
        {
            public int StudentID { get; set; }
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }

            public string Volledig => $"{StudentID}, {Voornaam} {Achternaam}";

        }

        public class StudentVakView
        {
            public int ID { get; set; }
            public string Student { get; set; }
            public string Vak { get; set; }
            public int Studiepunten { get; set; }
        }

        public ObservableCollection<Docent> showleraar() 
        {
            ObservableCollection<Docent> jufrouw = 
                new ObservableCollection<Docent>();

            using (MySqlConnection conn = new MySqlConnection(connect)) 
            {
                conn.Open();
                string sql = "select studentID, Voornaam, Achternaam from studenten";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    jufrouw.Add(new Docent 
                    { 
                        StudentID = reader.GetInt32("StudentID"),
                        Voornaam = reader.GetString("Voornaam"),
                        Achternaam = reader.GetString("Achternaam"),

                        
                    }); 
                
                }

            }
            return jufrouw;
        
        }

        public ObservableCollection<vak> showvak()
        {
            ObservableCollection<vak> meneer =
                new ObservableCollection<vak>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string sql = "select vakID, vaknaam, studiepunten from vakken";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    meneer.Add(new vak
                    {
                        VakID = reader.GetInt32("vakID"),
                        studiepunten = reader.GetInt32("Studiepunten"),
                        Vaknaam = reader.GetString("Vaknaam"),

                    });

                }

            }
            return meneer;

        }

        public ObservableCollection<StudentVakView> ShowStudentVak()
        {
            ObservableCollection<StudentVakView> lijst =
                new ObservableCollection<StudentVakView>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql =
                    "SELECT sv.ID, " +
                    "CONCAT(s.Voornaam, ' ', s.Achternaam) AS Student, " +
                    "v.Vaknaam AS Vak, " +
                    "v.Studiepunten " +
                    "FROM student_vak sv " +
                    "JOIN studenten s ON s.StudentID = sv.StudentID " +
                    "JOIN vakken v ON v.VakID = sv.VakID;";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lijst.Add(new StudentVakView
                    {
                        ID = reader.GetInt32("ID"),
                        Student = reader.GetString("Student"),
                        Vak = reader.GetString("Vak"),
                        Studiepunten = reader.GetInt32("Studiepunten")
                    });
                }
            }

            return lijst;
        }

        public void KoppelStudentVak(int studentID, int vakID)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "INSERT INTO student_vak (StudentID, VakID) VALUES (@s, @v)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@s", studentID);
                cmd.Parameters.AddWithValue("@v", vakID);

                cmd.ExecuteNonQuery();
            }
        }




    }
}
