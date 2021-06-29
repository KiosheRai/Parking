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
using System.Windows.Threading;

namespace Course_Project
{
    /// <summary>
    /// Логика взаимодействия для Receipt.xaml
    /// </summary>
    public partial class Receipt : Page
    {
        public Receipt()
        {
            InitializeComponent();
        }

        public Receipt(string num)
        {
            InitializeComponent();

            StartCloseTimer(5);
            ShowOnTime(num);
        }

        private void StartCloseTimer(int s)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(s);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            NavigationService.Navigate(new Uri("/Payment.xaml", UriKind.Relative));
        }


        private void ShowOnTime(string num)
        {
            DataTable table = SQLbase.Select($"select * from Payment where car = '{num}'");
            DataTable t = SQLbase.Select($"select arrival, place from Report where car = '{num}'");

            NumberOutput.Text = num;
            TimeInOutput.Text = t.Rows[0][0].ToString();
            TimeOutOutput.Text = table.Rows[0][1].ToString();
            SumOfPay.Text = String.Format("{0:C}", table.Rows[0][2]);

            SQLbase.Insert($"delete Report where car = '{num}'");
            SQLbase.Insert($"update Place set status = 'Свободно' where id = {t.Rows[0][1]}");
        }
    }
}
