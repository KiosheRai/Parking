using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using Word = Microsoft.Office.Interop.Word;

namespace Course_Project
{
    /// <summary>
    /// Логика взаимодействия для Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Page
    {
        private string num;
        private string s;
        private string time;


        public Confirmation(string num, string s, string time)
        {
            InitializeComponent();
            this.num = num;
            this.s = s;
            this.time = time;

            Report();

            StartCloseTimer(5);
            ShowOnTime();
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


        private void ShowOnTime()
        {
            NumberOutput.Text = num;
            PlaceOutput.Text = s;
            TimeOutput.Text = time;
        }

        private void Report()
        {
            Thread t = new Thread(ReportGo);
            t.Start();
        }

        private void ReplaceWordStub(String stubToReplace, String Text, Word.Document word)
        {
            var range = word.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: Text);
        }

        private void ReportGo()
        {
            try
            {
                var word = new Word.Application();
                word.Visible = false;

                var worddoc = word.Documents.Open($"{Environment.CurrentDirectory}/ReportMaket.DOCX");

                ReplaceWordStub("{num}", num, worddoc);
                ReplaceWordStub("{s}", s, worddoc);
                ReplaceWordStub("{time}", time, worddoc);

                worddoc.SaveAs2($"{Environment.CurrentDirectory}/report.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
