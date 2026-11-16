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
    /// Interaction logic for Vakken.xaml
    /// </summary>
    public partial class Vakken : Page
    {
        Models.Vakken groep2 = new Models.Vakken();

        public Vakken()
        {
            InitializeComponent();
            cmbVaknaam.ItemsSource = schoolVakken;
            cmbStudiepunten.ItemsSource = studiepunten;

            
            cmbVakken.ItemsSource = groep2.LaatVakZien();
        }

        string[] schoolVakken = {
            "Nederlands",
            "Engels",
            "Wiskunde",
            "Rekenen",
            "Burgerschap",
            "Loopbaan & Ontwikkeling",
            "Economie",
            "Bedrijfsadministratie",
            "Marketing",
            "Communicatie",
            "Geschiedenis",
            "Aardrijkskunde",
            "Biologie",
            "Natuurkunde",
            "Scheikunde",
            "Informatica",
            "Programmeren",
            "Digitale Vaardigheden",
            "Projectmanagement",
            "Sociale Vaardigheden",
            "Mens & Maatschappij",
            "Klantcontact",
            "Commercie",
            "Ondernemerschap",
            "Financiën",
            "HR‑vaardigheden",
            "Medische Basiskennis",
            "Onderzoeksvaardigheden"
        };
        int[] studiepunten = { 1, 2, 3, 4, 5, 6};
        


        private void btnVoegVakToe_Click(object sender, object e)
        {
            if (cmbVaknaam.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een vaknaam.");
                return;
            }

            if (txtBeschrijving.Text.Length > 500)
            {
                MessageBox.Show("Beschrijving is te lang.");
                return;
            }

            if (cmbStudiepunten.SelectedItem == null)
            {
                MessageBox.Show("Selecteer het aantal studiepunten.");
                return;
            }

            else 
            {
                Models.Vakken subjects = new Models.Vakken();
                subjects.VoegVakToe(cmbVaknaam.Text, txtBeschrijving.Text, (int)cmbStudiepunten.SelectedItem);
                cmbVakken.ItemsSource = subjects.LaatVakZien();
                cmbVaknaam.Text = string.Empty;
                if (txtBeschrijving.Text != string.Empty)
                {
                    txtBeschrijving.Text = string.Empty;
                }
                cmbStudiepunten.SelectedItem = null;

            }

           
        }

        private void btnVerwijderVak_Click(object sender, RoutedEventArgs e)
        {
            if (cmbVakken.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een vak om te verwijderen.");
                return;
            }
            else
            {
                MessageBoxResult keuze = MessageBox.Show(
                    "Weet je zeker dat je dit vak wilt verwijderen?",
                    "Waarschuwing",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (keuze != MessageBoxResult.Yes)
                {
                    return;     
                }
                if(keuze == MessageBoxResult.Yes) 
                {
                    MessageBox.Show("Vak verwijderd");
                
                }
                
                Models.Vakken doc = new Models.Vakken();

                int id = (int)cmbVakken.SelectedValue;

                doc.VerwijderVak(id);

                cmbVakken.ItemsSource = doc.LaatVakZien();
            }
        


        }
    }
    
}
