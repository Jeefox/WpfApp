using System.Windows;
using WpfApp2.Models;
using System.Xml;

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


            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "Id": // The node is an element.                        
                        string str = reader.Value;
                        while (reader.MoveToNextAttribute()) // Read the attributes.
                            str = reader.Value;
                        break;
                    case "NumCode": //Display the text in each element.
                        str = reader.Value;
                        break;
                    case "ChaCode": //Display the end of the element.
                        str = reader.Name;
                        break;
                }
            }
        }
    }
}
