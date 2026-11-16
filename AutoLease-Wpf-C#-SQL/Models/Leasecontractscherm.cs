using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using static ideavoorstel_auto_lease.Models.Leasecontractscherm;


namespace ideavoorstel_auto_lease.Models
{
    public class Leasecontractscherm
    {
        private string connectionString =
            "Server=localhost;Database=autopagina;Uid=root;Pwd=;";


       

        public class Auto
        {
            public int AutoID { get; set; }
            public string Merk { get; set; }
            public string Model { get; set; }

            public string VolledigeNaam => $"{Merk} {Model}";
        }

       
      



        public class Klant
        {
            public int KlantID { get; set; }
            public string naam { get; set; }
            public string telefoon { get; set; }

            public string VolledigeKlant => naam;
        }

       ;


        // =========================
        // LEASECONTRACT TOEVOEGEN
        // =========================

        public void Insertcontract(
            int autoID,
            int klantID,
            DateTime startdatum,
            DateTime einddatum,
            decimal maandbedrag)
        {
            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO leasecontracten
                        (autoID, klantID, startdatum, einddatum, maandbedrag)
                        VALUES
                        (@autoID, @klantID, @startdatum, @einddatum, @maandbedrag)";

                    using (MySqlCommand cmd =
                           new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@autoID", autoID);
                        cmd.Parameters.AddWithValue("@klantID", klantID);
                        cmd.Parameters.AddWithValue("@startdatum", startdatum);
                        cmd.Parameters.AddWithValue("@einddatum", einddatum);
                        cmd.Parameters.AddWithValue("@maandbedrag", maandbedrag);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Contract is toegevoegd");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public ObservableCollection<Auto> Autos { get; set; }
            = new ObservableCollection<Auto>();
        public void RefreshAutos()
        {
            Autos.Clear();

            using (MySqlConnection conn =
                   new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql =
                    "SELECT autoID, merk, model FROM autogegevens";

                using (MySqlCommand cmd =
                       new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Autos.Add(new Auto
                        {
                            AutoID = dr.GetInt32("autoID"),
                            Merk = dr.GetString("merk"),
                            Model = dr.GetString("model")
                        });
                    }
                }
            }
        }



        public ObservableCollection<Klant> Klanten { get; set; }
           = new ObservableCollection<Klant>();
        public void RefreshKlanten()
        {
            Klanten.Clear();

            using (MySqlConnection conn =
                   new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql =
                    "SELECT KlantID, naam, telefoon FROM klanten";

                using (MySqlCommand cmd =
                       new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Klanten.Add(new Klant
                        {
                            KlantID = dr.GetInt32("KlantID"),
                            naam = dr.GetString("naam"),
                            telefoon = dr.GetString("telefoon")
                        });
                    }
                }
            }
        }
    }
}



