using Google.Protobuf.WellKnownTypes;
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
    /// Interaction logic for Studenten.xaml
    /// </summary>
    public partial class Studenten : Page
    {
        Models.Studenten groep5 = new Models.Studenten();
        public Studenten()
        {
            InitializeComponent();

            
            cmbStudenten.ItemsSource = groep5.LaatStudentZien();
        }

        private void btnVoegStudentToe_Click(object sender, RoutedEventArgs e)
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

            if (dpGeboortedatum.SelectedDate == null)
            {
                MessageBox.Show("Geboortedatum is verplicht.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("E-mailadres is verplicht.");
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("E-mailadres is ongeldig.");
                return;
            }

           
            else 
            {

                Models.Studenten study = new Models.Studenten();

                study.VoegStudent(txtVoornaam.Text, txtAchternaam.Text, dpGeboortedatum.SelectedDate.Value, txtEmail.Text);
                cmbStudenten.ItemsSource = study.LaatStudentZien();

                txtVoornaam.Text = string.Empty;
                txtAchternaam.Text = string.Empty;
                dpGeboortedatum.SelectedDate = null;
                txtEmail.Text = string.Empty;
                


            }




        }

        private void btnVerwijderStudent_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStudenten.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een student om te verwijderen.");
                return;
            }
            else
            {
                MessageBoxResult keuze = MessageBox.Show(
                    "Weet je zeker dat je deze student wilt verwijderen?",
                    "Waarschuwing",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (keuze != MessageBoxResult.Yes)
                {
                    return;
                }
                if (keuze == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Student verwijderd");

                }
                Models.Studenten doc = new Models.Studenten();

                int id = (int)cmbStudenten.SelectedValue;

                doc.VerwijderStudent(id);

                cmbStudenten.ItemsSource = doc.LaatStudentZien();
            }


        }
    }
    }
