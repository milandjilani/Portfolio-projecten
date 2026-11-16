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
    /// Interaction logic for Klassen.xaml
    /// </summary>
    public partial class Klassen : Page
    {
        Models.Klassen klsn = new Models.Klassen();
        public Klassen()
        {
            InitializeComponent();
            cmbOpleiding.ItemsSource = mbo4Opleidingen;
            cmbLeerjaar.ItemsSource = leerjaar;
            cmbKlassen.ItemsSource = klsn.LaatKlasZien();
        }

        string[] mbo4Opleidingen =
        {
            "Software Development",
            "ICT System Engineer",
            "Sociaal Werker",
            "Persoonlijk Begeleider Maatschappelijke Zorg",
            "Gespecialiseerd Pedagogisch Medewerker",
            "Onderwijsassistent",
            "Mediavormgeving",
            "Marketing & Communication Specialist",
            "E-commerce Specialist",
            "Business Administration & Control Specialist",
            "Office & Management Support Specialist",
            "Logistiek Supervisor",
            "Specialist Transport en Logistiek",
            "Technisch Specialist Autotechniek",
            "Technisch Specialist Bedrijfsautotechniek",
            "Technicus Engineering",
            "Middenkaderfunctionaris Bouw",
            "Middenkaderfunctionaris Infra",
            "Operator C",
            "Apothekersassistent",
            "Doktersassistent",
            "Mbo-Verpleegkundige",
            "Dierenartsassistent Paraveterinair",
            "Recht en HR",
            "Bedrijfskunde",
            "Ondernemer Handel en/of Diensten",
            "Medewerker Evenementenorganisatie",
            "Applicatie- en mediaontwikkelaar",
            "Financieel Administratief Medewerker",
            "Junior Accountmanager",
            "Manager Retail",
            "Zelfstandig Werkend Kok",
            "Manager Ondernemer Horeca",
            "Sport- en Bewegingscoördinator",
            "Beveiliger",
            "Juridisch Administratief Dienstverlener",
            "Technicus Mechatronica",
            "Technicus Elektrotechniek",
            "Dierverzorger",
            "Hovenier",
            "Fotografie",
            "AV-specialist",
            "Luchtvaartdienstverlener",
            "Tandartsassistent",
            "Opticien",
            "Audicien",
            "Pedagogisch Educatief Professional",
            "Leidinggevende Recreatie",
            "Hotelmanager",
            "Allround Kapper",
            "Retailmanager",
            "Commercieel Manager",
            "Accountmanager",
            "Financieel Specialist",
            "Bedrijfsadministrateur",
            "Expediteur",
            "Manager Transport en Logistiek",
            "Maritiem Officier",
            "Tandtechnicus",
            "Orthopedisch Technisch Medewerker",
            "Medewerker Maatschappelijke Zorg",
            "Begeleider Specifieke Doelgroepen",
            "Pedagogisch Medewerker Kinderopvang",
            "Ruimtelijk Vormgever",
            "Grafisch Vormgever",
            "Signspecialist",
            "Game Artist",
            "Fashion Designer",
            "Interieuradviseur",
            "Leidinggevende Keuken",
            "Gespecialiseerd Kok",
            "Patissier",
            "Travel & Hospitality",
            "Leisure & Events",
            "Handhaver Toezicht en Veiligheid",
            "Coördinator Beveiliging",
            "Bouwkundig Tekenaar",
            "Werkvoorbereider Bouw",
            "Calculator Bouw",
            "Technicus Klimaat- en Koudetechniek",
            "Technicus Service en Onderhoud",
            "Eerste Autotechnicus",
            "Autoschadehersteller",
            "Procesoperator",
            "Onderhoudstechnicus",
            "Analist",
            "Chemisch-Fysisch Analist",
            "Biologisch Medisch Analist",
            "Dierenmanagement",
            "Bos- en Natuurbeheer",
            "Agrarisch Ondernemer",
            "Tuinbouw",
            "Paardenhouderij",
            "Voedingsmiddelentechnologie",
            "Schoonheidsspecialist",
            "Salonmanager",
            "Ondernemer Retail",
            "Ondernemer Bakkerij",
            "Ondernemer Slagerij",
            "Manager Hotel",
            "Leidinggevende Toerisme",
            "Technicus Industriële Automatisering",
            "Technicus Werktuigbouwkunde",
            "Technicus Elektrotechnische Installaties",
            "Technicus Procestechniek",
            "Luchtvaartdienstverlener",
            "Medewerker Marketing en Communicatie",
   
    
        };

        int[] leerjaar = {1,2,3,4 };

        private void btnVoegKlasToe_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKlasnaam.Text))
            {
                MessageBox.Show("Klasnaam is verplicht.");
                return;
            }

            if (cmbLeerjaar.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een leerjaar.");
                return;
            }

            if (cmbOpleiding.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een opleiding.");
                return;
            }
            else 
            {
                Models.Klassen groep8 = new Models.Klassen();
                groep8.VoegKlas(txtKlasnaam.Text, cmbLeerjaar.Text, cmbOpleiding.Text);
                cmbKlassen.ItemsSource = groep8.LaatKlasZien();
                txtKlasnaam.Text = string.Empty;
                cmbLeerjaar.Text = string.Empty;
                cmbOpleiding.Text = string.Empty;

            }

            

        }

        private void btnVerwijderKlas_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKlassen.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een klas om te verwijderen.");
                return;
            }
            else
            {
                MessageBoxResult keuze = MessageBox.Show(
                    "Weet je zeker dat je deze klas wilt verwijderen?",
                    "Waarschuwing",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (keuze != MessageBoxResult.Yes)
                {
                    return;
                }

                if (keuze == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Klas verwijderd");

                }

                Models.Klassen doc = new Models.Klassen();

                int id = (int)cmbKlassen.SelectedValue;

                doc.Verwijderklas(id);

                cmbKlassen.ItemsSource = doc.LaatKlasZien();
            }
        

        }
    }

    
}
