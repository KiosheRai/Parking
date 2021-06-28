using System;
using System.Collections.Generic;
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
            ComboBoxView.ItemsSource = SQLbase.Select($"select * from Place where status = 'Свободно'").DefaultView;
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
            }

            if (!b)
            {
                
            }
        }
    }
}
