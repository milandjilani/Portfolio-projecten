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
using MySql.Data.MySqlClient;

namespace ideavoorstel_auto_lease
{
    
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Paginas.Autolease Al = new Paginas.Autolease();
        Paginas.AutoPage AP = new Paginas.AutoPage();
        Paginas.KlantenPage KP = new Paginas.KlantenPage();
        Paginas.Leasecontractscherm LCS = new Paginas.Leasecontractscherm();
        Models.Leasecontractscherm LSCH = new Models.Leasecontractscherm();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void CarPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(AP);
        }

        private void KlantPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(KP);
        }

        private void AddPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(LCS);


        }

        private void leasePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(Al);

        }
    }
}
