using MySql.Data.MySqlClient;
using schoolbeheer.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static schoolbeheer.Models.Studenten;

namespace schoolbeheer.Models
{
    internal class Docenten
    {
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public void Voegdocent(string docentnaam, string docentachternaam, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string sql = "insert into docenten(Voornaam, Achternaam, Email)" +
                    "values(@voornaam, @achternaam, @mail)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@voornaam", docentnaam);
                    cmd.Parameters.AddWithValue("@achternaam", docentachternaam);
                    cmd.Parameters.AddWithValue("@mail", email);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                MessageBox.Show("docent is toegevoegd");


            }
        }


        public class DatabaseDocentVW
        {
            public int DocentID { get; set; }
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }
            public string Email { get; set; }

            public string VolledigeNaam => $"Naam docent: {Voornaam} {Achternaam}";
        

        }

        public ObservableCollection<DatabaseDocentVW> LaatDocentZien()
        {
            ObservableCollection<DatabaseDocentVW> docent =
                new ObservableCollection<DatabaseDocentVW>();

            using (MySqlConnection conn = new MySqlConnection(connect)) 
            {
                conn.Open();

                string sql = "select * from docenten";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader leraar = cmd.ExecuteReader();

                while (leraar.Read()) 
                {
                    docent.Add(new DatabaseDocentVW
                    {
                        DocentID = leraar.GetInt32("DocentID"),
                        Voornaam = leraar.GetString("Voornaam"),
                        Achternaam = leraar.GetString("Achternaam"),
                        Email = leraar.GetString("Email")
                    });         
                }
            }

            return docent;

        }

        public void VerwijderDocent(int DocentID)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "DELETE FROM docenten WHERE DocentID = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", DocentID);

                cmd.ExecuteNonQuery();
            }

            
        }

    }
}
