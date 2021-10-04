using System.Windows;
using WpfApp2.Models;
using System.Xml;
using System;
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
            /* using (ValuteContext db = new ValuteContext())
             {
                 Valute tableRow = new Valute { };
                 db.Valutes.Add(tableRow);
                 db.SaveChanges();
                 var Word = db.Valutes;
                 db.Valutes.Add(masObj);
                 foreach (Valute v in Word)
                 {
                     MessageBox.Show($"{v.Id}, {v.NumCode}");
                 }
             }*/

        }

        private void AddDataToTable()
        {
            using (ValuteContext db = new ValuteContext())
            {
                int count = 0;
                string URLString = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=04/10/2021";
                XmlReader reader = new XmlTextReader(URLString);

                List<Valute> masObj = new List<Valute>();
                Valute z = new Valute();

                reader.MoveToAttribute("NumCode");
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        /*case "ValCurs":
                            while (reader.NodeType != XmlNodeType.)
                            {
                                reader.Read();
                            }
                            string data = reader.Value;
                            break;*/
                        case "Valute":
                            
                            z.Id = (string)reader.GetAttribute("ID");
                            count++;
                            break;
                        case "NumCode":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();

                            }
                            z.NumCode = (reader.Value);
                            count++;
                            break;
                        case "CharCode":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();

                            }
                            z.CharCode = (reader.Value);
                            count++;
                            break;

                        case "Nominal":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();

                            }
                            z.Nominal = reader.Value;
                            count++;
                            break;

                        case "Name":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();

                            }
                            z.Name = (reader.Value);
                            count++;
                            break;

                        case "Value":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();

                            }
                            z.Value = reader.Value;
                            count++;
                            break;
                    }

                    if (count > 4)
                    {
                        masObj.Add(z);
                        count = 0;
                        z = new Valute();
                    }
                }
                db.Valutes.AddRange(masObj);
                db.SaveChanges();
            }
        }
    }
}
