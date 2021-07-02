using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private string login;

        public AdminPage(string login)
        {
            this.login = login;
            InitializeComponent();
            showInfo();
        }

        private void showInfo()
        {
            listReports.ItemsSource = SQLbase.Select($"SELECT * FROM Report").DefaultView;

            DateTime s = DateTime.Today;
            DateTime s1 = s.AddDays(1);

            using (DataTable Place = SQLbase.Select($"select count(*) from Place where status = 'Занято'")) {
                DataTable PlaceCount = SQLbase.Select($"select count(*) from Place");
                ParkPlace.Content = $"{Place.Rows[0][0]}/{PlaceCount.Rows[0][0]}";
             }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string[] s = login.Split(' ');
            SQLbase.Insert($"delete Operator where name = '{s[0]}' and surname = '{s[1]}'");

            MessageBox.Show("Аккаунт удалён");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Reg(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Reg.xaml", UriKind.Relative));
        }

        private void GoToEdit(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            Price nextPage = new Price(login);
            nav.Navigate(nextPage);
        }
    }
}
