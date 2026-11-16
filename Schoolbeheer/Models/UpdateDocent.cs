using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schoolbeheer.Models.Updatestd;



namespace schoolbeheer.Models
{
    internal class UpdateDocent
    {
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public class Leraar
        {

            public int DocentID { get; set; }
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }
            public string Email { get; set; }
            public string VolledigeNaam => $"{Voornaam} {Achternaam}, {Email}";

        }

        public ObservableCollection<Leraar> showdocent()
        {
            ObservableCollection<Leraar> kind =
                new ObservableCollection<Leraar>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string sql = "select DocentID, Voornaam, Achternaam, Email from docenten ";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    kind.Add(new Leraar
                    {
                       
                        DocentID = reader.GetInt32("DocentID"),
                        Voornaam = reader.GetString("Voornaam"),
                        Achternaam = reader.GetString("Achternaam"), 
                        Email = reader.GetString("Email")
                    });

                }


            }
            return kind;

        }

        public void Updatedocent(Leraar L)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = @"UPDATE docenten 
                       SET Voornaam=@v,
                           Achternaam=@a,
                           Email=@e
                       WHERE DocentID=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@v", L.Voornaam);
                cmd.Parameters.AddWithValue("@a", L.Achternaam);
                cmd.Parameters.AddWithValue("@e", L.Email);
                cmd.Parameters.AddWithValue("@id", L.DocentID);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
