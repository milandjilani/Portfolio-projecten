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

namespace ideavoorstel_auto_lease.Paginas
{
    /// <summary>
    /// Interaction logic for KlantenPage.xaml
    /// </summary>
    public partial class KlantenPage : Page
    {
        public KlantenPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNaam.Text))
            {
                MessageBox.Show("Vul de naam in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbTelefoon.Text))
            {
                MessageBox.Show("Vul het telefoonnummer in.");
                return;
            }

            // Telefoon moet cijfers bevatten
            if (!tbTelefoon.Text.All(char.IsDigit))
            {
                MessageBox.Show("Telefoonnummer mag alleen cijfers bevatten.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Vul het e-mailadres in.");
                return;
            }

        
            if (!tbEmail.Text.Contains("@") || !tbEmail.Text.Contains("."))
            {
                MessageBox.Show("E-mailadres is ongeldig.");
                return;
            }
            else 
            {
                Models.KlantenPage kp = new Models.KlantenPage();

                string tekst = tbNaam.Text;
                
                if (!string.IsNullOrEmpty(tekst))
                {
                    tekst = char.ToUpper(tekst[0]) + tekst.Substring(1); 
                }
                tbNaam.Text = tekst;


                string tekst2 = tbEmail.Text;

                if (!string.IsNullOrEmpty(tekst2))
                {
                    tekst2 = char.ToUpper(tekst[0]) + tekst2.Substring(1);
                }
                tbEmail.Text = tekst2;


                kp.voegklant(tekst, tbTelefoon.Text, tekst2);




            }

        }
    }
}
