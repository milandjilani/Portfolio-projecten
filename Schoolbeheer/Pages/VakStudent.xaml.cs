using schoolbeheer.Models;
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
    /// Interaction logic for VakStudent.xaml
    /// </summary>
    public partial class VakStudent : Page
    {
        Models.Vakstudent vakstudent = new Models.Vakstudent();
        

        public VakStudent()
        {
            InitializeComponent();
            cmbStudent.ItemsSource = vakstudent.showleraar();
            cmbStudent.SelectedValuePath = "StudentID";

            cmbVak.ItemsSource = vakstudent.showvak();
            cmbVak.SelectedValuePath = "VakID";

            dgStudentVak.ItemsSource = vakstudent.ShowStudentVak();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStudent.SelectedValue == null || cmbVak.SelectedValue == null)
            {
                MessageBox.Show("selecteer een student of vak");
                return;
            }
            try 
            {
                int studentID = (int)cmbStudent.SelectedValue;
                int vakID = (int)cmbVak.SelectedValue;


                vakstudent.KoppelStudentVak(studentID, vakID);

                MessageBox.Show("Student gekoppeld aan vak!");


                dgStudentVak.ItemsSource = vakstudent.ShowStudentVak();

            }
            catch 
            {
                MessageBox.Show("werkt niet");
            
            }

            
        }

    }
}
