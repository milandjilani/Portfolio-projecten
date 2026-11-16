using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace schoolbeheer.Models
{
    
    internal class Studenten
    {
        
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public class Studentklas
        {
            public int KlasID { get; set; }
            public string KlasNaam { get; set; }
        }

     
        public ObservableCollection<Studentklas> KlasIDinStudent()
        {
            ObservableCollection<Studentklas> klassen =
                new ObservableCollection<Studentklas>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "select KlasID, Klasnaam from klassen";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader lezer = cmd.ExecuteReader();

                while (lezer.Read())
                {
                    klassen.Add(new Studentklas
                    {
                        KlasID = lezer.GetInt32("KlasID"),
                        KlasNaam = lezer.GetString("KlasNaam")
                    });
                }
            }

            return klassen;
        }

        public void VoegStudent(string voornaam, string achternaam, DateTime datum, string mail)
        {

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string sql = "insert into studenten (Voornaam, Achternaam, Geboortedatum, Email)" +
                    "values(@voornaam, @achternaam, @date, @mail)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@voornaam", voornaam);
                    cmd.Parameters.AddWithValue("@achternaam", achternaam);
                    cmd.Parameters.AddWithValue("@date", datum);
                    cmd.Parameters.AddWithValue("@mail", mail);
                   

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                MessageBox.Show("student is toegevoegd");

            }

        }


        public class DatabaseStudentVW
        {
            public int StudentID { get; set; }
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }

            public DateTime geboortedatum { get; set; }

           
            public string Email { get; set; }

            public string VolledigeNaam => $"{Voornaam} {Achternaam}, {geboortedatum:dd-MM-yyyy}, {Email}";


        }

        public ObservableCollection<DatabaseStudentVW> LaatStudentZien()
        {
            ObservableCollection<DatabaseStudentVW> student =
                new ObservableCollection<DatabaseStudentVW>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "select studentid, voornaam, achternaam, geboortedatum, email from studenten";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader kind = cmd.ExecuteReader();

                while (kind.Read())
                {
                    student.Add(new DatabaseStudentVW
                    {
                        StudentID = kind.GetInt32("StudentID"),
                        Voornaam = kind.GetString("Voornaam"),
                        Achternaam = kind.GetString("Achternaam"),
                        geboortedatum = kind.GetDateTime("geboortedatum"),
                        Email = kind.GetString("Email")
                    });
                }
                conn.Close();
            }

            return student;

        }


        public void VerwijderStudent(int StudentID)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "DELETE FROM studenten WHERE StudentID = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", StudentID);

                cmd.ExecuteNonQuery();
            }


        }
    }
}
