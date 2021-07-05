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
    /// Логика взаимодействия для Price.xaml
    /// </summary>
    public partial class Price : Page
    {
        private string login;

        public Price(string login)
        {
            this.login = login;

            InitializeComponent();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string price = inputPrice.Text;

            foreach (Char x in price)
            {
                if (!char.IsDigit(x))
                {
                    inputPrice.ToolTip = "Требуется числовое значение!";
                    inputPrice.Foreground = Brushes.Red;
                    return;
                }
                else
                {
                    inputPrice.ToolTip = "";
                    inputPrice.Foreground = Brushes.Black;
                }
            }

            decimal d = Convert.ToDecimal(price);

            SQLbase.Insert($"insert into Rate(ddate, pay) values('{DateTime.Today}', {d.ToString().Replace(",",".")})");

            MessageBox.Show("Тариф изменён");
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);

            AdminPage nextPage = new AdminPage(login);
            nav.Navigate(nextPage);
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            if(startDate.SelectedDate == null || endDate.SelectedDate == null)
            {
                editButton.Foreground = Brushes.Red;
                editButton.ToolTip = "Выберите обе даты";
                return;
            }
            else
            {   
                editButton.Foreground = Brushes.AliceBlue;
                editButton.ToolTip = "";
            }

            DateTime start = startDate.SelectedDate.Value;
            DateTime end = endDate.SelectedDate.Value;

            using (DataTable Place = SQLbase.Select($"select sum(pay) from Payment where departure >= '{start.Day}-{start.Month}-{start.Year}' and departure <= '{end.Day}-{end.Month}-{end.Year}'"))
            {

                string str;
                if (Place.Rows[0][0].ToString() == null || Place.Rows[0][0].ToString() == "" || Place.Rows[0][0].ToString() == " ")
                {
                    str = "0";
                }
                else
                {
                    str = String.Format("{0:C}", Place.Rows[0][0]);
                }
                answerOutput.Content = $"{str}";
            }
        }
    }
}
