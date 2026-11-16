using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace eindopdracht_csharp
{
    public partial class MainWindow : Window
    {
        Random randomizer = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
       

        List<Ellipse> fishes = new List<Ellipse>();
        List<Ellipse> kleurvis = new List<Ellipse>();
        List<Rectangle> plantjes = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += moveFish;
            timer2.Tick += moveFishtwee;
            


            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer2.Interval = TimeSpan.FromMilliseconds(500);
          

            


        }

        private void Button_Click(object sender, EventArgs e)
        {
            timer.Start();
            addFishToCanvas();
        }

        void moveFish(object sender, EventArgs e)
        {
            for (int i = 0; i < fishes.Count; i++)
            {
                Ellipse fish = fishes[i];
                Thickness margin = fish.Margin;

                if (margin.Left <= 0)
                {
                    int[] direction = (int[])fish.Tag;
                    direction[0] = direction[0] * -1;

                    fish.Tag = direction;
                }
                else if (margin.Left >= (aquarium.ActualWidth - fish.Width))
                {
                    int[] direction = (int[])fish.Tag;
                    direction[0] = direction[0] * -1;

                    fish.Tag = direction;
                }

                if (margin.Top <= 0)
                {
                    int[] direction = (int[])fish.Tag;
                    direction[1] = direction[1] * -1;

                    fish.Tag = direction;
                }
                else if (margin.Top >= (aquarium.ActualHeight - fish.Height))
                {
                    int[] direction = (int[])fish.Tag;
                    direction[1] = direction[1] * -1;

                    fish.Tag = direction;
                }

                margin.Left += ((int[])fish.Tag)[0];
                margin.Top += ((int[])fish.Tag)[1];

                fish.Margin = margin;


            }


        }

        void addFishToCanvas()
        {
            Ellipse fish = new Ellipse();
            fish.Width = 25;
            fish.Height = 25;
            fish.Fill = System.Windows.Media.Brushes.Black;


            int maxLeft = (int)aquarium.ActualWidth - (int)fish.Width;
            int left = randomizer.Next(0, maxLeft);

            int maxHeight = (int)aquarium.ActualHeight - (int)fish.Height;
            int top = randomizer.Next(0, maxHeight);

            int[] direction = new int[2];
            direction[0] = randomizer.Next(-20, 20);
            direction[1] = randomizer.Next(-20, 20);

            fish.Tag = direction;
            fish.Margin = new Thickness(left, top, 0, 0);
            fishes.Add(fish);

            aquarium.Children.Add(fish);
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = naam.Text.Trim(); 
            bool isHappy = ja.IsChecked == null ? false : (bool)ja.IsChecked;
            SolidColorBrush color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(kleurkiezer.SelectedColorText)); 
            double width = double.TryParse(breedte.Text, out double w) ? w : 25;
            double height = double.TryParse(groot.Text, out double h) ? h : 25;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Ongeldige naam. Voer een geldige naam in.");
                return;
            }

            if (ja.IsChecked == false && nee.IsChecked == false) 
            {
                MessageBox.Show("kies of je vrolijk bent of niet.");
                return;
            
            }

            MessageBoxResult button = MessageBox.Show("Weet je zeker dat je deze vis wilt toevoegen", "Kleur bevestigen", MessageBoxButton.YesNo);
            if (button == MessageBoxResult.No)
            {
                MessageBox.Show("Vis niet toegevoegd.");
                return;
            }

           
           
            Ellipse fish = buildVis(color, width, height);
            addMovementToFish(fish, isHappy);
            nametolist(name, height, width, color);
            MessageBox.Show("Vis toegevoegd!");
            timer2.Start();







        }

        private Ellipse buildVis(SolidColorBrush color, double width, double height)
        {
            Ellipse fish = new Ellipse();
            fish.Fill = color;

            fish.Width = width;
            fish.Height = height;

            int maxLeft = (int)aquarium.ActualWidth - (int)fish.Width;
            int left = randomizer.Next(0, maxLeft);

            int maxTop = (int)aquarium.ActualHeight - (int)fish.Height;
            int top = randomizer.Next(0, maxTop);

            int[] direction = new int[2];
            direction[0] = randomizer.Next(-20, 20);
            direction[1] = randomizer.Next(-20, 20);

            fish.Tag = direction;
            fish.Margin = new Thickness(left, top, 0, 0);

            aquarium.Children.Add(fish);

            
            kleurvis.Add(fish);

            return fish;
        }


        private void addMovementToFish(Ellipse fish, bool isHappy)
        {
            if (isHappy == true) 
            {
                timer2.Interval = TimeSpan.FromMilliseconds(1);

            }
            else
            {
                timer2.Interval = TimeSpan.FromMilliseconds(500);
            }

           

        }

        private void nametolist(string name, double height, double width, SolidColorBrush color)
        {

            string pad = "gegevens.txt";
            File.AppendAllText(pad, $"De vis heet {name}, hij is {height} groot en {width} breed. De kleur is {color}" + Environment.NewLine);
            lijst.Items.Add($"De vis heet {name}, hij is {height} groot en {width} breed. De kleur is {color}");
        }



        void moveFishtwee(object sender, EventArgs e)
        {
            foreach (Ellipse fish in kleurvis.ToList())
            {
                int[] direction = (int[])fish.Tag;
                Thickness margin = fish.Margin;

                if (margin.Left <= 0 || margin.Left >= aquarium.ActualWidth - fish.Width)
                    direction[0] = -direction[0];

                if (margin.Top <= 0 || margin.Top >= aquarium.ActualHeight - fish.Height)
                    direction[1] = -direction[1];

                margin.Left += direction[0];
                margin.Top += direction[1];

                fish.Margin = margin;
                fish.Tag = direction;
            }
        }











        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            maakplant();
        }

        private void maakplant()
        {
            string name = naam.Text;
            string big = groot.Text;
            string lang = breedte.Text;
            string kleur = kleurkiezer.SelectedColorText;


            Rectangle plant = new Rectangle();
            plant.Width = double.TryParse(breedte.Text, out double width) ? width : 25;
            plant.Height = double.TryParse(groot.Text, out double height) ? height : 25;

            plant.Fill = Brushes.Green;

            if (!string.IsNullOrEmpty(naam.Text))
            {
                MessageBox.Show("Een plant heeft een standaard formaat van 25x25");
                lijst.Items.Add($"Er is een plant met de naam: {name} en de kleur green. voer bij verwijderen green in om de planten te verwijderen.");
  
                groot.Text = "25";
                breedte.Text = "25";

                groot.IsEnabled = false;
                breedte.IsEnabled = false;
            }
            if (string.IsNullOrEmpty(naam.Text))
            {
                lijst.Items.Add("plant, typ green als je het wilt verwijderen. ");

                groot.IsEnabled = false;
                breedte.IsEnabled = false;

                groot.Text = "25";
                breedte.Text = "25";
            }
            


            
           


            int maxLeft = (int)aquarium.ActualWidth - (int)plant.Width;
            int left = randomizer.Next(0, maxLeft);

            int maxTop = (int)aquarium.ActualHeight - (int)plant.Height;
            int top = randomizer.Next(0, maxTop);

            plant.Margin = new Thickness(left, top, 0, 0);


            int[] richting = new int[2];
            richting[0] = randomizer.Next(-10, 10);
            richting[1] = randomizer.Next(-10, 10);

            plant.Tag = richting;

            plantjes.Add(plant);
            aquarium.Children.Add(plant);



        }

        private void beweegplant(object sender, EventArgs e)
        {
            foreach (Rectangle plant in plantjes)
            {
                int[] direction = (int[])plant.Tag;
                Thickness margin = plant.Margin;


                if (margin.Left <= 0 || margin.Left >= aquarium.ActualWidth - plant.Width)
                    direction[0] = -direction[0];


                if (margin.Top <= 0 || margin.Top >= aquarium.ActualHeight - plant.Height)
                    direction[1] = -direction[1];


                margin.Left += direction[0];
                margin.Top += direction[1];

                plant.Margin = margin;
                plant.Tag = direction;
            }


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            lijst.Items.Clear();
            aquarium.Children.Clear();

            groot.IsEnabled = true;
            breedte.IsEnabled = true;

            groot.Text = string.Empty;
            breedte.Text = string.Empty;

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            verwijderen();
            verwijderen2();
        }

        private void verwijderen() 
        {
            try
            {
                Color targetColor = (Color)ColorConverter.ConvertFromString(kleurwisser.Text.ToLower());

                string name = naam.Text.Trim();

                SolidColorBrush color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(kleurkiezer.SelectedColorText));


                double width = double.TryParse(breedte.Text, out double w) ? w : 25;
                double height = double.TryParse(groot.Text, out double h) ? h : 25;

                string hoi = $"De vis heet {name}, hij is {height} groot en {width} breed. De kleur is {color}";


                foreach (Ellipse ellips in aquarium.Children.OfType<Ellipse>().ToList())
                {

                    if (ellips.Fill is SolidColorBrush brush && brush.Color == targetColor)
                    {
                        aquarium.Children.Remove(ellips);
                    }


                    string invoer = kleurwisser.Text.ToLower().Trim();

                    for (int i = 0; i < lijst.Items.Count; i++)
                    {
                        string item = lijst.Items[i].ToString().ToLower();

                        if (item.Contains(invoer))
                        {

                            lijst.Items.RemoveAt(i);
                        }
                        
                    }
                }
            }

            catch
            {
                MessageBox.Show("Ongeldige kleur. Voer een geldige kleur in.");
                
                return;
            }


            
            }




        

        private void verwijderen2() 
        {
            try
            {
                Color targetColor = (Color)ColorConverter.ConvertFromString(kleurwisser.Text.ToLower());

                foreach (Rectangle vier in aquarium.Children.OfType<Rectangle>().ToList())
                {

                    if (vier.Fill is SolidColorBrush brush && brush.Color == targetColor)
                    {
                        aquarium.Children.Remove(vier);
                    }


                    string invoerplant = kleurwisser.Text.ToLower().Trim();
                    bool itemFound = false;

                    for (int i = 0; i < lijst.Items.Count; i++)
                    {
                        string item = lijst.Items[i].ToString().ToLower();

                        if (item.Contains(invoerplant))
                        {
                            
                            itemFound = true;
                            


                        }
                        lijst.Items.RemoveAt(i);
                        break;
                    }

                }
            }
            catch 
            {
                MessageBox.Show("Ongeldige kleur. Voer een geldige kleur in.");
                return;

            }
           

                

               
                
            }


        }
    }



