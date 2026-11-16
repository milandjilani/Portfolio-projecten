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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ideavoorstel_auto_lease.Models.Leasecontractscherm;

namespace ideavoorstel_auto_lease.Paginas
{
    public partial class Leasecontractscherm : Page
    {
        private Models.Leasecontractscherm data;

        public Leasecontractscherm()
        {
            InitializeComponent();

            data = new Models.Leasecontractscherm();
            DataContext = data;
            data.RefreshAutos();
            data.RefreshKlanten();
        }


       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            if (cbAutoID.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een auto.");
                return;
            }

            if (cbKlantID.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een klant.");
                return;
            }

          
            if (tbStartdatum.SelectedDate == null)
            {
                MessageBox.Show("Selecteer een startdatum.");
                return;
            }

        
            if (tbEinddatum.SelectedDate == null)
            {
                MessageBox.Show("Selecteer een einddatum.");
                return;
            }

           
            if (tbEinddatum.SelectedDate < tbStartdatum.SelectedDate)
            {
                MessageBox.Show("Einddatum mag niet vóór startdatum liggen.");
                return;
            }

            
            if (string.IsNullOrWhiteSpace(tbMaandbedrag.Text))
            {
                MessageBox.Show("Vul een maandbedrag in.");
                return;
            }

         
            if (!decimal.TryParse(tbMaandbedrag.Text, out decimal bedrag))
            {
                MessageBox.Show("Maandbedrag moet een geldig getal zijn.");
                return;
            }

            
            data.Insertcontract(
                (int)cbAutoID.SelectedValue,
                (int)cbKlantID.SelectedValue,
                tbStartdatum.SelectedDate.Value,
                tbEinddatum.SelectedDate.Value,
                bedrag
            );
        }


        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
         
            data.RefreshAutos();

            data.RefreshKlanten();

            MessageBox.Show("De gegevens zijn vernieuwd.");
        }
    }
}




