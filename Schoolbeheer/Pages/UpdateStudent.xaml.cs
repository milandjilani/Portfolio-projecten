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
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static schoolbeheer.Models.Updatestd;

namespace schoolbeheer.Pages
{
    /// <summary>
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Page
    {
        Models.Updatestd update = new Models.Updatestd();
        public UpdateStudent()
        {
            InitializeComponent();
            dgStudent.ItemsSource = update.showstudent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudent.SelectedItem is Student s)
            {
                update.UpdateStudent(s);

                dgStudent.ItemsSource = update.showstudent(); 
                MessageBox.Show("Student geüpdatet!");
            }
            else
            {
                MessageBox.Show("Selecteer een leerling.");
            }
        }
    }
}
