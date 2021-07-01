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

            using (DataTable Place = SQLbase.Select($"select count(*) from Payment"))
            {
                CountAll.Content = $"Весь поток: {Place.Rows[0][0]}";
            }

            using (DataTable Place = SQLbase.Select($"select count(*) from Payment where departure >= '{s.Year}-{s.Month}-{s.Day}' and departure <= '{s1.Year}-{s1.Month}-{s1.Day}'"))
            { 
                CountOfDay.Content = $"Сегодняшний поток: {Place.Rows[0][0]}";
            }

            using (DataTable Place = SQLbase.Select($"select sum(pay) from Payment"))
            {
                string str;
           
                if (Place.Rows[0][0].ToString() == null || Place.Rows[0][0].ToString() == "" || Place.Rows[0][0].ToString() == " ")
                {

                    str = "0";
                }else
                {
                    str = String.Format("{0:C}", Place.Rows[0][0]);
                }
                MoneyAll.Content = $"Общая прибыль: {str}";
            }

            using (DataTable Place = SQLbase.Select($"select sum(pay) from Payment where departure >= '{s.Year}-{s.Month}-{s.Day}' and departure <= '{s1.Year}-{s1.Month}-{s1.Day}'"))
            {

                string str;
                if (Place.Rows[0][0].ToString() == null || Place.Rows[0][0].ToString() == "" || Place.Rows[0][0].ToString() == " ")
                {
                    str = "0";
                }
                else
                {
                    str = String.Format("{0:C}",Place.Rows[0][0]);
                }
                MoneyOfDay.Content = $"Сегодняшняя прибыль: {str}";
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
