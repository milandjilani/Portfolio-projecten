using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static schoolbeheer.Models.Docenten;

namespace schoolbeheer.Models
{
    internal class Klassen
    {
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";
        public void VoegKlas(string klasnaam, string leerjaar, string opleiding)
        {

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string sql = "insert into klassen (Klasnaam, Leerjaar, Opleiding)" +
                    "values(@klas, @jaar, @opleiding)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@klas", klasnaam);
                    cmd.Parameters.AddWithValue("@jaar", leerjaar);
                    cmd.Parameters.AddWithValue("@opleiding", opleiding);
                    

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                MessageBox.Show("klas is toegevoegd");

            }

        }

        public class DatabaseKlasVW
        {
            public int KlasID { get; set; }
            public string Klasnaam { get; set; }
            public int Leerjaar { get; set; }
            public string Opleiding { get; set; }

            public string VolledigeNaam => $"Klas {Klasnaam}, opleiding: {Opleiding}";

        }

        public ObservableCollection<DatabaseKlasVW> LaatKlasZien()
        {
            ObservableCollection<DatabaseKlasVW> klas =
                new ObservableCollection<DatabaseKlasVW>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "select * from klassen";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader groep8 = cmd.ExecuteReader();

                while (groep8.Read())
                {
                    klas.Add(new DatabaseKlasVW
                    {
                        KlasID = groep8.GetInt32("KlasID"),
                        Klasnaam = groep8.GetString("Klasnaam"),
                        Leerjaar = groep8.GetInt32("Leerjaar"),
                        Opleiding = groep8.GetString("Opleiding")
                    });
                   
                }
                conn.Close();
            }
            return klas;

        }


        public void Verwijderklas(int KlasID)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "DELETE FROM klassen WHERE KlasID = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", KlasID);

                cmd.ExecuteNonQuery();
            }


        }


    }
}
