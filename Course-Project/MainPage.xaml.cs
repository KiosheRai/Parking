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

namespace Course_Project
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GoToEntrance(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Entrance.xaml", UriKind.Relative));
        }

        private void GoToExit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Payment.xaml", UriKind.Relative));
        }

        private void GoToAdmin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
    }
}

