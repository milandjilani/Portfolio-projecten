using ideavoorstel_auto_lease.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ideavoorstel_auto_lease.Paginas
{
    public partial class LeaseEditWindow : Window
    {
        private LeaseContract contract;

        public LeaseEditWindow(LeaseContract c)
        {
            InitializeComponent();
            contract = c;

            dpStart.SelectedDate = c.startdatum;
            dpEind.SelectedDate = c.einddatum;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dpStart.SelectedDate == null || dpEind.SelectedDate == null)
            {
                MessageBox.Show("Selecteer beide datums.");
                return;
            }

            if (dpEind.SelectedDate < dpStart.SelectedDate)
            {
                MessageBox.Show("Einddatum moet later zijn dan startdatum.");
                return;
            }

            if (dpEind.SelectedDate == dpStart.SelectedDate)
            {
                MessageBox.Show("Einddatum mag niet gelijk zijn aan startdatum.");
                return;
            }
            else
            {

                try
                {
                    using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=autopagina;Uid=root;Pwd=;"))
                    {
                        conn.Open();

                        string sql = @"
            UPDATE leasecontracten
            SET startdatum=@newStart, einddatum=@newEind
            WHERE autoID=@id AND startdatum=@oldStart AND einddatum=@oldEind";

                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@newStart", dpStart.SelectedDate);
                            cmd.Parameters.AddWithValue("@newEind", dpEind.SelectedDate);

                            cmd.Parameters.AddWithValue("@id", contract.autoID);
                            cmd.Parameters.AddWithValue("@oldStart", contract.startdatum);
                            cmd.Parameters.AddWithValue("@oldEind", contract.einddatum);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Datums gewijzigd!");
                    this.Close();
                }
                catch (Exception Ka)
                {
                    MessageBox.Show(Ka.Message);

                    MessageBox.Show("Storing, datums kunnen niet gewijzigd worden. Neem contact op met de klantenservice.");
                }
            }
        }

    }
}