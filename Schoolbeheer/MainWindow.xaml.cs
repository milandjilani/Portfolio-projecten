using schoolbeheer.Pages;
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
using System.Windows.Markup;

namespace schoolbeheer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Pages.Vakken vak = new Pages.Vakken();
        Pages.Klassen klas = new Pages.Klassen();
        Pages.Studenten student = new Pages.Studenten();
        Pages.Docenten docent = new Pages.Docenten();
        Pages.VakStudent vk = new Pages.VakStudent();

        Pages.Updatedocent updoc = new Pages.Updatedocent();
        Pages.UpdateStudent upstd = new Pages.UpdateStudent();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void vakklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(vak);
            
        }

        private void studentklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(student);
        }

        private void docentklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(docent);
        }

        private void klasklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(klas);
        }

        private void voegklik(object sender, RoutedEventArgs e)
        {
            
            Mainframe.Navigate(new Pages.Koppelpagina());
        }

        private void koppelklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(vk);
        }

        private void Docentenklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(new Pages.Updatedocent());
        }

        private void Leerlingklik(object sender, RoutedEventArgs e)
        {
            Mainframe.Navigate(new Pages.UpdateStudent());
        }
    }

}
