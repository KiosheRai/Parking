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
    /// Логика взаимодействия для Payment.xaml
    /// </summary>
    public partial class Payment : Page
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void Resume(object sender, RoutedEventArgs e)
        {
            string num = NumberInput.Text.ToUpper();

            if (num.Length == 0)
            {
                NumberInput.Foreground = Brushes.Red;
                NumberInput.ToolTip = "Заполните поле";
            }
            else
            {
                NumberInput.Foreground = Brushes.Black;
                NumberInput.ToolTip = "";
            }

            DataTable table = SQLbase.Select($"select * from Report where car = '{num}'");

            if (table.Rows.Count >= 1)
            {
                string dateInput = table.Rows[0][3].ToString();

                DateTime dateIn = DateTime.Parse(dateInput);
                DateTime dateExit = DateTime.Now;

                DataTable t = SQLbase.Select($"select pay, ddate from Rate order by id desc");

                decimal rate = 0;

                if (t.Rows.Count != 0)
                {
                    int i = 0;
                    try
                    {
                        while (DateTime.Parse(t.Rows[i][1].ToString()) > DateTime.Parse(table.Rows[0][3].ToString()))
                        {
                            rate = Convert.ToDecimal(t.Rows[0][0]);
                            i++;
                        }
                    }
                    catch
                    {
                        rate = 0;
                    }
                }

                int hour = Convert.ToInt32((dateExit - dateIn).TotalHours) - 1;

                if(hour < 0)
                {
                    hour = 0;
                }

                decimal sum = hour * rate;
                
                SQLbase.Insert($"insert into Payment(departure, pay, car) values('{dateExit.ToString()}', {sum.ToString().Replace(",",".")}, '{num}');");

                NavigationService nav;
                nav = NavigationService.GetNavigationService(this);

                Receipt nextPage = new Receipt(num);
                nav.Navigate(nextPage);
            }
            else
            {
                NumberInput.Foreground = Brushes.Red;
                NumberInput.ToolTip = "Неверный номер машины";
            }
        }
    }
}
