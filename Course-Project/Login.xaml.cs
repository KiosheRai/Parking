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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Resume(object sender, RoutedEventArgs e)
        {
            string login = LoginInput.Text.Trim().ToUpper();
            string password = PasswordInput.Password;

            string[] Fio = login.Split(' ');

            bool isGood = true;
            if (login.Length < 1 || Fio.Length < 2)
            {
                isGood = false;
                LoginInput.ToolTip = "Введите логин";
                LoginInput.Foreground = Brushes.Red;
            }
            else
            {
                LoginInput.ToolTip = " ";
                LoginInput.Foreground = Brushes.Black;
            }

            if (password.Length < 1)
            {
                isGood = false;
                PasswordInput.ToolTip = "Введите пароль";
                PasswordInput.Foreground = Brushes.Red;
            }
            else
            {
                PasswordInput.ToolTip = " ";
                PasswordInput.Foreground = Brushes.Black;
            }

            if (isGood == true)
            {
                DataTable table = SQLbase.Select($"select name, surname, pass from Operator where name = '{Fio[0]}' and surname = '{Fio[1]}' and pass = '{password}'");

                if (table.Rows.Count > 0)
                {
                    NavigationService nav;
                    nav = NavigationService.GetNavigationService(this);

                    AdminPage nextPage = new AdminPage(login);
                    nav.Navigate(nextPage);
                }
                else
                {
                    LoginInput.ToolTip = "Ошибка входа";
                    LoginInput.Foreground = Brushes.Red;
                    PasswordInput.ToolTip = "Ошибка входа";
                    PasswordInput.Foreground = Brushes.Red;
                }
            }
        }
    }
}
