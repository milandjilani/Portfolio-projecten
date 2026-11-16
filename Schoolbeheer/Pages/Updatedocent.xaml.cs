using Mysqlx.Crud;
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
using static schoolbeheer.Models.Updatestd;



namespace schoolbeheer.Pages
{
    /// <summary>
    /// Interaction logic for Updatedocent.xaml
    /// </summary>
    public partial class Updatedocent : Page
    {
        Models.UpdateDocent Docent = new Models.UpdateDocent();
        public Updatedocent()
        {
            InitializeComponent();
            dgDocent.ItemsSource = Docent.showdocent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dgDocent.SelectedItem is Models.UpdateDocent.Leraar L)
            {
                Docent.Updatedocent(L);

                dgDocent.ItemsSource = Docent.showdocent();
                MessageBox.Show("Docentgegevens aangepast.");
            }
            else
            {
                MessageBox.Show("Selecteer een Docent.");
            }
        }
    }
}
