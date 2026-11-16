using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ideavoorstel_auto_lease.Models
{
    internal class AutoLease
    {

        public class LeaseContract
        {
            public string AutoNaam { get; set; }
            public DateTime startdatum { get; set; }
            public DateTime einddatum { get; set; }
        }



        private string connectionString = "Server=localhost;Database=autopagina;Uid=root;Pwd=;";

        public ObservableCollection<LeaseContract> leases 
            = new ObservableCollection<LeaseContract>();

        public ObservableCollection<LeaseContract> LoadLeaseContracten()
        {
            leases.Clear(); 


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
            SELECT 
                CONCAT(a.merk, ' ', a.model) AS AutoNaam,
                l.startdatum,
                l.einddatum
            FROM leasecontracten l
            JOIN autogegevens a ON l.autoID = a.autoID";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LeaseContract lease = new LeaseContract();
                            lease.AutoNaam = dr.GetString("AutoNaam");
                            lease.startdatum = dr.GetDateTime("startdatum");
                            lease.einddatum = dr.GetDateTime("einddatum");

                            leases.Add(lease);
                        }
                    }
                }
            }

            return leases;
        }


    }
}
