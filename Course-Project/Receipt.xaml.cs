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
    /// Логика взаимодействия для Receipt.xaml
    /// </summary>
    public partial class Receipt : Page
    {
        private string Num;
        private string Start;
        private string End;
        private string Price;

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

            this.Num = num;
            this.Start = t.Rows[0][0].ToString();
            this.End = table.Rows[0][1].ToString();
            this.Price = String.Format("{0:C}", table.Rows[0][2]);

            NumberOutput.Text = Num;
            TimeInOutput.Text = Start;
            TimeOutOutput.Text = End;
            SumOfPay.Text = Price;

            SQLbase.Insert($"delete Report where car = '{num}'");
            SQLbase.Insert($"update Place set status = 'Свободно' where id = {t.Rows[0][1]}");

            Report();
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

                var worddoc = word.Documents.Open($"{Environment.CurrentDirectory}/PaymentMaket.DOCX");

                ReplaceWordStub("{num}", Num, worddoc);
                ReplaceWordStub("{start}", Start, worddoc);
                ReplaceWordStub("{end}", End, worddoc);
                ReplaceWordStub("{price}", Price, worddoc);

                worddoc.SaveAs2($"{Environment.CurrentDirectory}/Payment.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
