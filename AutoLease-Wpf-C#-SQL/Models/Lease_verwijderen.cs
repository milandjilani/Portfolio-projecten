using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ideavoorstel_auto_lease.Models
{
   
    internal class Lease_verwijderen
    {
       
        public class ComboItem
        {
            public int ID { get; set; }
            public string Naam { get; set; }

            public DateTime start { get; set; }

            public DateTime eind { get; set; }

            public override string ToString()
            {
                return $"{Naam} | {start:dd-MM-yyyy} t/m {eind:dd-MM-yyyy}";
            }

            

        }

        

        public string connect = "Server=localhost;Database=autopagina;Uid=root;Pwd=;";

        public void opzeggen(Paginas.Autolease pagina) 
        {
            List<ComboItem> items = new List<ComboItem>();
            

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = @"
                    SELECT 
                        a.autoID,
                        a.merk,
                        l.startdatum,
                        l.einddatum
                    FROM leasecontracten l
                    JOIN autogegevens a ON l.autoID = a.autoID";


                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader()) 
                { 
                    while (dr.Read()) 
                    {
                        items.Add(new ComboItem
                        {
                            ID = dr.GetInt32("autoID"),
                            Naam = dr.GetString("merk"),
                            start = dr.GetDateTime("startdatum"),
                            eind = dr.GetDateTime("einddatum")


                        });
                    
                    }
                
                }
            }
            pagina.verwijderbox.ItemsSource = items;
        }



        

    }
}
