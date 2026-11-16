using MySql.Data.MySqlClient;
using schoolbeheer.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static schoolbeheer.Models.Docenten;

namespace schoolbeheer.Models
{
    internal class Vakken
    {
        
        private string connect = "Server=localhost;Database=schoolbeheer;Uid=root;Pwd=;";

        public void VoegVakToe( string vak, string beschrijving, int studiepunten) 
        {
            using (MySqlConnection conn = new MySqlConnection(connect)) 
            {
                conn.Open();
                string sql = "insert into vakken(Vaknaam, Beschrijving, Studiepunten)" +
                    "values(@Vaknaam, @Beschrijving, @Studiepunten)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn)) 
                {
                    cmd.Parameters.AddWithValue("@Vaknaam", vak);
                    cmd.Parameters.AddWithValue("@Beschrijving", beschrijving);
                    cmd.Parameters.AddWithValue("@Studiepunten", studiepunten);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                MessageBox.Show("Vak is toegevoegd");
                
            
            }

           

        }

        public class DatabaseVakVW
        {
            public int VakID { get; set; }
            public string Vaknaam { get; set; }
            public int Studiepunten { get; set; }

            public string Vak => $"{Vaknaam}, aantal studiepunten: {Studiepunten}";


        }

        public ObservableCollection<DatabaseVakVW> LaatVakZien()
        {
            ObservableCollection<DatabaseVakVW> vakje =
                new ObservableCollection<DatabaseVakVW>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "select vakid, vaknaam, studiepunten from vakken";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader subject = cmd.ExecuteReader();

                while (subject.Read())
                {
                    vakje.Add(new DatabaseVakVW
                    {
                        VakID = subject.GetInt32("VakID"),
                        Vaknaam = subject.GetString("Vaknaam"),
                        Studiepunten = subject.GetInt32("Studiepunten")

                    });
                }
                
            }
            return vakje;

        }

        public void VerwijderVak(int VakID)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "DELETE FROM vakken WHERE VakID = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", VakID);

                cmd.ExecuteNonQuery();
            }


        }
    }
}
