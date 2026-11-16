using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ideavoorstel_auto_lease.Models
{
    
    internal class AutoPage
    {
        private string connectionString = "Server=localhost;Database=autopagina;Uid=root;Pwd=;";

        public void InsertAuto(string merk, string model, string bouwjaar, string aantalKm, string kenteken, string status)
        {
            try {
                string sql = "INSERT INTO autogegevens (merk, model, bouwjaar, aantal_km, kenteken, status) " +
                             "VALUES (@merk, @model, @bouwjaar, @aantal_km, @kenteken, @status)";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@merk", merk);
                        cmd.Parameters.AddWithValue("@model", model);
                        cmd.Parameters.AddWithValue("@bouwjaar", bouwjaar);
                        cmd.Parameters.AddWithValue("@aantal_km", aantalKm);
                        cmd.Parameters.AddWithValue("@kenteken", kenteken);
                        cmd.Parameters.AddWithValue("@status", status);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                MessageBox.Show("Auto is succesvol toegevoegd");
            }
                catch(Exception e) 

                { 
                    MessageBox.Show(e.Message); }
                }
        

    }
}
