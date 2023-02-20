using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

namespace Game_of_Life_old
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Page _view;

        public Page View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                if (PropertyChanged !=null)
                    PropertyChanged(this, new PropertyChangedEventArgs("View"));
            }
        }
        private List<Page> _pages;
        public MainWindow(IEnumerable<Page> page)
        {
            _pages = page.ToList();
            View = _pages.First(x => x is Spiel);
            InitializeComponent();

            
        }

     public event PropertyChangedEventHandler? PropertyChanged;

        private void Spiel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

