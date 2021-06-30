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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Continue(object sender, RoutedEventArgs e)
        {

            string name = loginBox.Text;
            string password1 = firstPassBox.Password.Trim();
            string password2 = secondPassBox.Password.Trim();
            string[] s = name.Split(' ');


            bool isGood = true;
            //Проверка поля логина
            if (name.Length < 3 && name.Length > 20)
            {
                isGood = false;
                loginBox.ToolTip = "Длина логина должна быть от 3 до 20 букв!";
                loginBox.Foreground = Brushes.Red;
            }
            else if(s.Length != 2)
            {
                loginBox.ToolTip = "Требуется Имя и Фамилия";
                loginBox.Foreground = Brushes.Red;
            }
            else
            {
                bool stop = false;

                foreach (char x in name)
                {

                    if (Char.IsDigit(x))
                    {
                        stop = true;
                        break;
                    }
                }

                if (stop)
                {
                    isGood = false;
                    loginBox.ToolTip = "Недопустимый ввод символов!";
                    loginBox.Foreground = Brushes.Red;
                }
                else
                {

                    loginBox.ToolTip = " ";
                    loginBox.Foreground = Brushes.Black;

                }
            }


            if (password1.Length < 4 && password1.Length > 16)
            {
                isGood = false;
                firstPassBox.ToolTip = "Длина пароля должна быть от 4 до 16 символов!";
                firstPassBox.Foreground = Brushes.Red;
            }
            else
            {
                firstPassBox.ToolTip = "";
                firstPassBox.Foreground = Brushes.Black;
            }

            //Подтверждение пароля
            if (password1 != password2)
            {
                isGood = false;
                secondPassBox.ToolTip = "Пароли не совпадают";
                secondPassBox.Foreground = Brushes.Red;
            }
            else
            {
                secondPassBox.ToolTip = "";
                secondPassBox.Foreground = Brushes.Black;
            }

            if (isGood == true)
            {
                
                SQLbase.Insert($"insert into Operator(name, surname, pass) values (N'{s[0]}',N'{s[1]}',N'{password1}')");
                MessageBox.Show("Оператор создан");

                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }
    }
}
