using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_of_Life_old
{
    /// <summary>
    /// Interaktionslogik für Spiel.xaml
    /// </summary>
    public partial class Spiel : Page
    {
        
        public Spiel()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
        }
        const int ZellenBreite = 60;
        const int ZellenHöhe = 60;
        Rectangle[,] felder = new Rectangle[ZellenBreite, ZellenHöhe];
        DispatcherTimer timer = new DispatcherTimer();
        bool Violet= false;
        bool black = true;

        public void FarbeRandom(Rectangle r)
        {   Random würfel = new Random();
            if (Violet)
            {
            r.Fill = (würfel.Next(0, 2) == 1) ? Brushes.BlueViolet : Brushes.Beige;
            }
            if(black)
            {
            r.Fill = (würfel.Next(0, 2) == 1) ? Brushes.Black : Brushes.Beige;
            }
        }
        public void Farbeklick(object sender) 
        { 
            if(Violet)
            {
                ((Rectangle)sender).Fill=  (((Rectangle)sender).Fill == Brushes.BlueViolet) ? Brushes.Beige : Brushes.BlueViolet;
            }
            if (black)
            {
                ((Rectangle)sender).Fill=  (((Rectangle)sender).Fill == Brushes.Black) ? Brushes.Beige : Brushes.BlueViolet;
            }
        }
    
    
        private void start_Click(object sender, RoutedEventArgs e)
        {  

            

            for (int i = 0; i < ZellenHöhe; i++)
            {
                for (int j = 0; j < ZellenBreite; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = Zeichenfläche.ActualWidth / ZellenBreite -2.0;
                    r.Height = Zeichenfläche.ActualHeight / ZellenHöhe -2.0;
                    FarbeRandom(r);
                    Zeichenfläche.Children.Add(r);
                    Canvas.SetLeft(r, j *  Zeichenfläche.ActualWidth / ZellenBreite);
                    Canvas.SetTop(r, i* Zeichenfläche.ActualHeight / ZellenHöhe);

                    r.MouseDown += R_MouseDown; //

                    felder[i, j] = r;
                }
            }
        }
        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Farbeklick(sender);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            int[,] anzahlNachbahr = new int[ZellenHöhe, ZellenBreite];

            for (int i = 0; i < ZellenHöhe; i++)
            {
                for (int j = 0; j < ZellenBreite; j++)
                {
                    int Nachbahr = 0;

                    int iDarüber = i -1;
                    if (iDarüber < 0) iDarüber = ZellenHöhe -1;

                    int iDarunter = i +1;
                    if (iDarunter >= ZellenHöhe) iDarunter = 0;

                    int jLinks = j -1;
                    if (jLinks < 0) jLinks = ZellenBreite -1;

                    int jRechts = j+1;
                    if (jRechts >= ZellenBreite) jRechts = 0;


                    //int[,] nachbarAbfrage = new int[,]
                    //{
                    //    {iDarüber,jLinks},
                    //    {iDarüber,j},
                    //    {iDarüber,jRechts},
                    //    {i,jLinks},
                    //    {i, jRechts},
                    //    {iDarunter,jLinks},
                    //    {iDarunter,j},
                    //    {iDarunter,jRechts}
                    //};
                    //
                    //
                    //for (int k = 0; k < nachbarAbfrage.GetLength(0); k++)
                    //{
                    //    for (int l = 0; l < nachbarAbfrage.GetLength(1)-1; l++)
                    //    {
                    //        if (felder[k, l].Fill == Brushes.Beige)
                    //        {
                    //            Nachbahr++;
                    //        }
                    //      } 
                    //  }
                    if (felder[iDarüber, jLinks].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[iDarüber, j].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[iDarüber, jRechts].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[i, jLinks].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[i, jRechts].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[iDarunter, jLinks].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[iDarunter, j].Fill == Brushes.Beige) Nachbahr++;
                    if (felder[iDarunter, jRechts].Fill == Brushes.Beige) Nachbahr++;


                    anzahlNachbahr[i, j] = Nachbahr;
                }
            }
            for (int i = 0; i < ZellenHöhe; i++)
            {
                for (int j = 0; j < ZellenBreite; j++)
                {
                    if (anzahlNachbahr[i, j] < 2 || anzahlNachbahr[i, j] > 3)
                    {
                        if (Violet)
                        {
                            felder[i, j].Fill= Brushes.BlueViolet;
                        }
                        if (black)
                        {
                            felder[i, j].Fill= Brushes.Black;
                        }

                    }
                    else if (anzahlNachbahr[i, j] == 3)
                    {
                        felder[i, j].Fill= Brushes.Beige;
                    }
                }
            }
        }

        private void Animation_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                Animation.Content = "Starte Animation";
            }
            else
            {
                timer.Start();
                Animation.Content = "Stop Animation";
            }
        }

        private void Black(object sender, RoutedEventArgs e)
        {
            black = true;
            Violet = false;
            FViolet.IsChecked = false;
        }

        private void violet(object sender, RoutedEventArgs e)
        {
            black= false;
            Violet = true;
            FBlack.IsChecked = false;

        }

    }
}

