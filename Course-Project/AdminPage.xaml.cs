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

            using (DataTable Place = SQLbase.Select($"select count(*) from Place where status = 'Занято'")) {
                DataTable PlaceCount = SQLbase.Select($"select count(*) from Place");
                ParkPlace.Content = $"{Place.Rows[0][0]}/{PlaceCount.Rows[0][0]}";
             }

            using (DataTable Place = SQLbase.Select($"select count(*) from Payment"))
            {
                CountAll.Content = $"Весь поток: {Place.Rows[0][0]}";
            }

            using (DataTable Place = SQLbase.Select($"select count(*) from Payment where departure > '{DateTime.Today}' and departure < '{DateTime.Today.AddDays(1)}'"))
            {
                CountOfDay.Content = $"Сегодняшний поток: {Place.Rows[0][0]}";
            }

            using (DataTable Place = SQLbase.Select($"select sum(pay) from Payment"))
            {
                string s;
           
                if (Place.Rows[0][0].ToString() == null || Place.Rows[0][0].ToString() == "" || Place.Rows[0][0].ToString() == " ")
                {

                    s = "0";
                }else
                {
                    s = String.Format("{0:C}", Place.Rows[0][0]);
                }
                MoneyAll.Content = $"Общая прибыль: {s}";
            }

            using (DataTable Place = SQLbase.Select($"select sum(pay) from Payment where departure > '{DateTime.Today}' and departure < '{DateTime.Today.AddDays(1)}'"))
            {

                string s;
                if (Place.Rows[0][0].ToString() == null || Place.Rows[0][0].ToString() == "" || Place.Rows[0][0].ToString() == " ")
                {
                    s = "0";
                }
                else
                {
                    s = String.Format("{0:C}",Place.Rows[0][0]);
                }
                MoneyOfDay.Content = $"Сегодняшняя прибыль: {s}";
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
    }
}
