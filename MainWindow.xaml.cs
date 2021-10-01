using System.Windows;
using WpfApp2.Models;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();
          
        }     

        private void ButtClick1(object sender, RoutedEventArgs e)
        {          
            using(ValuteContext db=new ValuteContext())
            {
                Valute tableRow = new Valute { Id = "1", NumCode = "word", CharCode = "123", Nominal = 123, Name = "123", Value = 1.23 };
                db.Valutes.Add(tableRow);
                db.SaveChanges();
                var Word = db.Valutes;
                foreach (Valute v in Word)
                {
                    MessageBox.Show($"{v.Id}, {v.NumCode}");
                }
                

            }
        
        }

        private void ButtClick2(object sender, RoutedEventArgs e)
        {

        }
    }
}
