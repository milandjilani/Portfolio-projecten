using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace schoolbeheer.Models
{
    internal class Docentklas
    {
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public class Docent 
        {
            public int DocentID { get; set; }
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }
            public string Email { get; set; }

            public string fullname => $"{Voornaam} {Achternaam}";


        }

        public class groep
        {
            public int KlasID { get; set; }
            public string Klasnaam { get; set; }
            public int Leerjaar { get; set; }
            public string Opleiding { get; set; }

            public string fullname => $"Klas {Klasnaam}, opleiding: {Opleiding}";

        }

        public class KoppelingView
        {
            public int ID { get; set; }
            public int DocentID { get; set; }
            public int KlasID { get; set; }

            public string Docent { get; set; }
            public string Klas { get; set; }
            public string Opleiding { get; set; }
        }


       
        public ObservableCollection<Docent> ComboboxDoc() 
        {
            ObservableCollection<Docent> doc 
                = new ObservableCollection<Docent>();

            using (MySqlConnection conn = new MySqlConnection(connect)) 
            {
                conn.Open();
                string sql = "SELECT * FROM docenten";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader voegen = cmd.ExecuteReader();

                while (voegen.Read()) 
                {
                    doc.Add(new Docent 
                    {
                        DocentID = voegen.GetInt32("DocentID"),
                        Voornaam = voegen.GetString("Voornaam"),
                        Achternaam = voegen.GetString("Achternaam"),
                        Email = voegen.GetString("Email")

                    });
                   
                
                }
                return doc;
            }
            

        
        
        }


        public ObservableCollection<groep> Comboboxklas()
        {
            ObservableCollection<groep> doc
                = new ObservableCollection<groep>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string sql = "SELECT * FROM klassen";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader voegen = cmd.ExecuteReader();

                while (voegen.Read())
                {
                    doc.Add(new groep
                    {
                        KlasID = voegen.GetInt32("KlasID"),
                        Klasnaam = voegen.GetString("Klasnaam"),
                        Leerjaar = voegen.GetInt32("Leerjaar"),
                        Opleiding = voegen.GetString("Opleiding")

                    });


                }
                return doc;
            }




        }

        
        public bool BestaatKoppeling(int docentID, int klasID)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM docent_klas WHERE docentID = @docentID AND klasID = @klasID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@docentID", docentID);
                cmd.Parameters.AddWithValue("@klasID", klasID);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0; // true = bestaat al
            }
        }


        public void VoegKoppelingToe(int docentID, int klasID)
        {

            if (BestaatKoppeling(docentID, klasID))
            {
                MessageBox.Show("Deze koppeling bestaat al.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "INSERT INTO docent_klas (docentID, klasID) VALUES (@docentID, @klasID)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@docentID", docentID);
                cmd.Parameters.AddWithValue("@klasID", klasID);

                cmd.ExecuteNonQuery();
            }
        }


        public ObservableCollection<KoppelingView> AlleKoppelingen()
        {
            ObservableCollection<KoppelingView> lijst = new ObservableCollection<KoppelingView>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql =
                    "SELECT dk.ID, dk.docentID, dk.klasID, " +
                    "CONCAT(d.Voornaam, ' ', d.Achternaam) AS Docent, " +
                    "k.Klasnaam AS Klas, " +
                    "k.Opleiding AS Opleiding " +
                    "FROM docent_klas dk " +
                    "JOIN docenten d ON d.DocentID = dk.docentID " +
                    "JOIN klassen k ON k.KlasID = dk.klasID";


                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lijst.Add(new KoppelingView
                    {
                        ID = reader.GetInt32("ID"),
                        DocentID = reader.GetInt32("docentID"),
                        KlasID = reader.GetInt32("klasID"),
                        Docent = reader.GetString("Docent"),
                        Klas = reader.GetString("Klas"),
                        Opleiding = reader.GetString("Opleiding")
                    });
                }
            }

            return lijst;
        }

        public void update() 
        {
            Pages.Koppelpagina koppeling = new Pages.Koppelpagina();
            koppeling.cmbDocent.ItemsSource = ComboboxDoc();
            
        }

    }
}
