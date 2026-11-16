using ideavoorstel_auto_lease.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ideavoorstel_auto_lease.Models.Lease_verwijderen;

namespace ideavoorstel_auto_lease.Paginas
{
    public partial class Autolease : Page
    {
        public string connect = "Server=localhost;Database=autopagina;Uid=root;Pwd=;";
        Models.AutoLease al = new Models.AutoLease();

      
        public ObservableCollection<LeaseContract> contract
            = new ObservableCollection<LeaseContract>();

        public Autolease()
        {
            InitializeComponent();

           
            dgLeaseOverzicht.ItemsSource = contract;

            RefreshLeaseOverzicht();
            RefreshVerwijderBox();
        }

        public void RefreshVerwijderBox()
        {
            Lease_verwijderen lv = new Lease_verwijderen();
            lv.opzeggen(this);   
        }

    
        public void RefreshLeaseOverzicht()
        {
            contract.Clear();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = @"
                    SELECT 
                        a.merk AS AutoNaam,
                        l.startdatum,
                        l.einddatum,
                        l.autoID
                    FROM leasecontracten l
                    JOIN autogegevens a ON l.autoID = a.autoID";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        contract.Add(new LeaseContract
                        {
                            AutoNaam = dr.GetString("AutoNaam"),
                            startdatum = dr.GetDateTime("startdatum"),
                            einddatum = dr.GetDateTime("einddatum"),
                            autoID = dr.GetInt32("autoID")
                        });
                    }
                }
            }
        }

        
        private void dgLeaseOverzicht_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgLeaseOverzicht.SelectedItem is LeaseContract selected)
            {
                LeaseEditWindow win = new LeaseEditWindow(selected);
                win.ShowDialog();

                RefreshLeaseOverzicht();
                RefreshVerwijderBox();
            }
        }

       
        private void verwijdercc(object sender, RoutedEventArgs e)
        {
            if (verwijderbox.SelectedItem == null)
            {
                MessageBox.Show("selecteer eerst een contract");
                return;
            }

            ComboItem item = (ComboItem)verwijderbox.SelectedItem;

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string sql = "DELETE FROM leasecontracten WHERE autoID = @id";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", item.ID);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBoxResult result = MessageBox.Show(
                "Weet je zeker dat je dit contract wilt verwijderen?",
                "Bevestiging",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("contract verwijderd");

                
                RefreshLeaseOverzicht();
                RefreshVerwijderBox();
            }
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshLeaseOverzicht();
            RefreshVerwijderBox();
            MessageBox.Show("scherm is geupdated, doe dit elke keer als je een wijziging uitvoert");
        }
    }

    public class LeaseContract
    {
        public string AutoNaam { get; set; }
        public int autoID { get; set; }
        public System.DateTime startdatum { get; set; }
        public System.DateTime einddatum { get; set; }
    }
}

