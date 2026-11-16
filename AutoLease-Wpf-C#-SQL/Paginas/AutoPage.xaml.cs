using MySqlX.XDevAPI;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ideavoorstel_auto_lease.Paginas
{
    /// <summary>
    /// Interaction logic for AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {

        public AutoPage()
        {

            InitializeComponent();
            LoadMerken();
            statusloading();


        }

        private void LoadMerken()
        {
            string[] merken = {
        "Abarth","Acura","Alfa Romeo","Alpine","Aston Martin","Audi","Bentley","BMW","Bugatti",
        "Buick","Cadillac","Chevrolet","Chrysler","Citroën","Cupra","Dacia","Daewoo","Daihatsu",
        "Dodge","Donkervoort","DS Automobiles","Ferrari","Fiat","Fisker","Ford","Genesis",
        "GMC","Honda","Hummer","Hyundai","Infiniti","Isuzu","Jaguar","Jeep","Kia","Koenigsegg",
        "KTM","Lada","Lamborghini","Lancia","Land Rover","Lexus","Lincoln","Lotus","Lucid",
        "Maserati","Maybach","Mazda","McLaren","Mercedes-Benz","MG","Mini","Mitsubishi",
        "Nissan","Opel","Peugeot","Polestar","Pontiac","Porsche","RAM","Renault","Rolls-Royce",
        "Rover","Saab","Seat","Škoda","Smart","SsangYong","Subaru","Suzuki","Tesla","Toyota",
        "Vauxhall","Volkswagen","Volvo","Wiesmann"
    };

            foreach (var merk in merken)
                cbMerk.Items.Add(merk);
        }


        private void statusloading()
        {
            string[] dealerStatus = {
                    "Aangeboden",
                    "Taxatie gepland",
                    "Taxatie uitgevoerd",
                    "In afwachting van bod",
                    "Bod uitgebracht",
                    "Bod geaccepteerd",
                    "Bod geweigerd",
                    "Onderhandeling gaande",
                    "Koopovereenkomst opgesteld",
                    "Koopovereenkomst getekend",
                    "Wacht op betaling",
                    "Betaald",
                    "Wacht op overdracht",
                    "Overgedragen aan dealer",
                    "Inruilauto",
                    "Particuliere inkoop",
                    "Zakelijke inkoop",
                    "overig"
            };

            foreach (var item in dealerStatus)
            {
                tbStatus.Items.Add(item);
                
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbMerk.Text))
            {
                MessageBox.Show("Vul het merk in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbModel.Text))
            {
                MessageBox.Show("Vul het model in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbBouwjaar.Text))
            {
                MessageBox.Show("Vul het bouwjaar in.");
                return;
            }

            if (!int.TryParse(tbBouwjaar.Text, out _))
            {
                MessageBox.Show("Bouwjaar moet een getal zijn.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbKmStand.Text))
            {
                MessageBox.Show("Vul de kilometerstand in.");
                return;
            }

            if (!int.TryParse(tbKmStand.Text, out _))
            {
                MessageBox.Show("Kilometerstand moet een getal zijn.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbKenteken.Text))
            {
                MessageBox.Show("Vul het kenteken in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbStatus.Text))
            {
                MessageBox.Show("Vul de status in.");
                return;
            }

            else
            {
                Models.AutoPage ap = new Models.AutoPage();
                Models.Leasecontractscherm LSCH = new Models.Leasecontractscherm();

                string kenteken = tbKenteken.Text;

                if (kenteken.Any(char.IsLetter))
                {
                    kenteken = kenteken.ToUpper();
                }

                tbKenteken.Text = kenteken;

                
                ap.InsertAuto(
                    cbMerk.SelectedItem.ToString(),
                    tbModel.Text,
                    tbBouwjaar.Text,
                    tbKmStand.Text,
                    kenteken,
                    tbStatus.Text
                );
                

                





            }

        }
    }
}
