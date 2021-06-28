using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Page
    {

        public Confirmation(string num, string s, string time)
        {
            InitializeComponent();


            StartCloseTimer(5);
            ShowOnTime(num, s, time);
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
            NavigationService.Navigate(new Uri("/Entrance.xaml", UriKind.Relative));
        }


        private void ShowOnTime(string num, string s, string time)
        {
            NumberOutput.Text = num;
            PlaceOutput.Text = s;
            TimeOutput.Text = time;
        }
    }
}
