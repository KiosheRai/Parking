using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Entrance.xaml
    /// </summary>
    public partial class Entrance : Page
    {
        public Entrance()
        {
            InitializeComponent();

            CheckFreePlace();
        }

        private void CheckFreePlace()// Проверка присутствия свободных мест
        {
            DataTable table = SQLbase.Select($"select * from Place where status = 'Свободно'");

            ComboBoxView.ItemsSource = table.DefaultView;

            if (table.Rows.Count == 0)
            {
                //IEnumerable<Button> collection = grid1.Children.OfType<Button>();
                //foreach(Button x in collection)
                //{
                //    if(x.Content.ToString() == "Продолжить")
                //    {
                //        x.IsVisible = false;
                //    }
                //}
                ButtonContinue.Visibility = Visibility.Hidden;
                NumberInput.Foreground = Brushes.Red;
                NumberInput.IsReadOnly = true;
                NumberInput.Text = "Мест нет!";
                return;
            }

            NumberInput.IsReadOnly = false;
            ButtonContinue.Visibility = Visibility.Visible;
            NumberInput.Foreground = Brushes.Black;
        }

        private void Resume(object sender, RoutedEventArgs e)
        {
            string num = NumberInput.Text.ToUpper();

            bool b = Regex.IsMatch(num, @"^\d{4}\s[A-Z]{2}-+[1-7]$");
            bool stations = Regex.IsMatch(num, @"^\d{4}\s[A-Z]{2}-+[0]$");

            if (stations)
            {
                MessageBox.Show("Гос номера не обслуживаем!");
                Application.Current.Shutdown();
                return;
            }

            //Проверка правильности ввода значений
            if (!b)
            {
                NumberInput.Foreground = Brushes.Red;
                NumberInput.ToolTip = "Неверный формат";
            }
            else
            {
                NumberInput.Foreground = Brushes.Black;
                NumberInput.ToolTip = "";
            }

            //if(ComboBoxView.SelectedIndex == -1)
            //{
            //    ComboBoxView.Foreground = Brushes.Red;
            //    ComboBoxView.ToolTip = "Выберите место";
            //}
            //else
            //{
            //    ComboBoxView.Foreground = Brushes.Black;
            //    ComboBoxView.ToolTip = "";
            //}

            //Занесение значений в бд
            if (b && ComboBoxView.SelectedIndex != -1)
            {
                DataTable tableReport = SQLbase.Select($"select * from Report where car = '{num}'");

                if(tableReport.Rows.Count >= 1)
                {
                    NumberInput.Foreground = Brushes.Red;
                    NumberInput.ToolTip = "Машина уже присутствует.";
                    return;
                }
                else
                {
                    NumberInput.Foreground = Brushes.Black;
                    NumberInput.ToolTip = "";
                }

                int index = ComboBoxView.SelectedIndex + 1;
                string s = ComboBoxView.Text;

                string time = DateTime.Now.ToString();

                SQLbase.Insert($"INSERT INTO Report(car, place, arrival) values('{num}', {s}, '{time}');");
                SQLbase.Insert($"UPDATE Place SET status = 'Занято' where id = {s}");

                //Переход на другую страницу с параметром
                NavigationService nav;
                nav = NavigationService.GetNavigationService(this);

                Confirmation nextPage = new Confirmation(num, s, time);
                nav.Navigate(nextPage);
            }
        }
    }
}
