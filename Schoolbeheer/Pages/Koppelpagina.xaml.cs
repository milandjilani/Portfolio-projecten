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
    /// Interaction logic for Koppelpagina.xaml
    /// </summary>
    public partial class Koppelpagina : Page
    {
        Models.Docentklas dckls = new Models.Docentklas();
        public Koppelpagina()
        {
            InitializeComponent();

            
            cmbDocent.ItemsSource = dckls.ComboboxDoc();
            cmbKlas.ItemsSource = dckls.Comboboxklas();
          
            dgKoppeling.ItemsSource = dckls.AlleKoppelingen();
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDocent.SelectedItem == null || cmbKlas.SelectedItem == null)
            {
                MessageBox.Show("Selecteer een docent en een klas.");
                return;
            }

            int docentID = (int)cmbDocent.SelectedValue;
            int klasID = (int)cmbKlas.SelectedValue;

           
            dckls.VoegKoppelingToe(docentID, klasID);
            dgKoppeling.ItemsSource = dckls.AlleKoppelingen();
        }




    }
}
