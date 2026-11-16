using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ideavoorstel_auto_lease.Models
{
    internal class KlantenPage
    {
        private string connectionString = "Server=localhost;Database=autopagina;Uid=root;Pwd=;";

        public void voegklant(string naam, string telefoonnummer, string email) 
        {
            try 
            {
                

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string sql = "INSERT INTO klanten (naam, telefoon, email) " +
                                 "VALUES (@naam, @telefoon, @email)";

                    conn.Open();
                    

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@naam", naam);
                        cmd.Parameters.AddWithValue("@telefoon", telefoonnummer);
                        cmd.Parameters.AddWithValue("@email", email);

                        cmd.ExecuteNonQuery();
                    }

                   

                    conn.Close();
                }

                MessageBox.Show("klant is succesvol toegevoegd");


            }

            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            
            }
            
            
        
        }
    }
}
