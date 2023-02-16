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
using System.Windows.Threading;

namespace Game_of_Life
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
        const int ZellenBreite = 100;
        const int ZellenHöhe = 100;
        Rectangle[,] felder = new Rectangle[ZellenBreite, ZellenHöhe];
        DispatcherTimer timer = new DispatcherTimer();

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }
        public void Start()
        {
            Random würfel = new Random();

            for (int i = 0; i < ZellenHöhe; i++)
            {
                for (int j = 0; j < ZellenBreite; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = Zeichenfläche.ActualWidth / ZellenBreite -2.0;
                    r.Height = Zeichenfläche.ActualHeight / ZellenHöhe -2.0;

                    r.Fill = (würfel.Next(0, 2) == 1) ? Brushes.BlueViolet : Brushes.Beige;
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
            ((Rectangle)sender).Fill=  (((Rectangle)sender).Fill == Brushes.BlueViolet) ? Brushes.Beige : Brushes.BlueViolet;
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
                        felder[i, j].Fill= Brushes.BlueViolet;
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

    }

}
}
