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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace schoolbeheer.Pages
{
    /// <summary>
    /// Interaction logic for Docenten.xaml
    /// </summary>
    public partial class Docenten : Page
    {
        Models.Docenten doc = new Models.Docenten();
        public Docenten()
        {
            InitializeComponent();
            cmbDocenten.ItemsSource = doc.LaatDocentZien();
        }

        private void btnVoegDocentToe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVoornaam.Text))
            {
                MessageBox.Show("Voornaam is verplicht.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAchternaam.Text))
            {
                MessageBox.Show("Achternaam is verplicht.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-mailadres is verplicht.");
                return;
            }
            else 
            {
                Models.Docenten juf = new Models.Docenten();
                Models.Docentklas doc = new Models.Docentklas();
                juf.Voegdocent(txtVoornaam.Text, txtAchternaam.Text, txtEmail.Text);
                cmbDocenten.ItemsSource = juf.LaatDocentZien();
                

                txtVoornaam.Text = string.Empty;
                txtAchternaam.Text = string.Empty;
                txtEmail.Text = string.Empty;

                


            }

            

        }

       

        private void btnVerwijderD_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmbDocenten.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een docent om te verwijderen.");
                return;
            }
            else
            {
                MessageBoxResult keuze = MessageBox.Show(
                    "Weet je zeker dat je deze docent wilt verwijderen?",
                    "Waarschuwing",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (keuze != MessageBoxResult.Yes)
                {
                    return;
                }

                if (keuze == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Docent verwijderd");
                }

                Models.Docenten doc = new Models.Docenten();

                int id = (int)cmbDocenten.SelectedValue;

                doc.VerwijderDocent(id);

                cmbDocenten.ItemsSource = doc.LaatDocentZien();
            }

        }
    }
}
