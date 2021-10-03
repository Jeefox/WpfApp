using System.Windows;
using WpfApp2.Models;
using System.Xml;
using System.Collections.Generic;

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
            MainFrame.Content = new Page1();
            AddDataToTable();
        }

        private void ButtClick1(object sender, RoutedEventArgs e)
        {
            using (ValuteContext db = new ValuteContext())
            {
                Valute tableRow = new Valute { };
                db.Valutes.Add(tableRow);
                db.SaveChanges();
                var Word = db.Valutes;
                foreach (Valute v in Word)
                {
                    MessageBox.Show($"{v.Id}, {v.NumCode}");
                }
            }

        }

        private void AddDataToTable()
        {
            string URLString = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=02/03/2002";
            XmlReader reader = new XmlTextReader(URLString);
            List<object> masObj = new List<object>();
            while (reader.Read())
            {
               for (int i = 0; i<6; i++)
                {
                    masObj[i] = reader.Value;
                }
                db.Valutes.Add(masObj);
            }

        }
    }
}
