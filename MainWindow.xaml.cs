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
        private Page1 page = new Page1();
        public static string data = "";
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Page1();
            AddDataToTable();
            page.getInitData();

        }

        private void ButtClick1(object sender, RoutedEventArgs e)
        {
           
        }

        private void AddDataToTable()
        {
            using (ValuteContext db = new ValuteContext())
            {
                
                string URLString = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=04/10/2021";
                XmlReader reader = new XmlTextReader(URLString);

                List<Valute> masObj = new List<Valute>();
                Valute z = new Valute();
                
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement) reader.Skip();
                    switch (reader.Name)
                    {
                        case "ValCurs":                            
                            data = reader.GetAttribute("Date");
                            break;
                        case "Valute":                           
                            z.Id = (string)reader.GetAttribute("ID");
                            break;
                        case "NumCode":
                            
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();
                            }
                            z.NumCode = (reader.Value);

                            break;
                        case "CharCode":

                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();
                            }
                            z.CharCode = reader.Value;
                            break;

                        case "Nominal":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();
                            }
                            z.Nominal = Convert.ToInt32(reader.Value);                            
                            break;

                        case "Name":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();
                            }
                            z.Name = reader.Value;                            
                            break;

                        case "Value":
                            while (reader.NodeType != XmlNodeType.Text)
                            {
                                reader.Read();
                            }
                            z.Value = float.Parse(reader.Value);
                            masObj.Add(z);
                            z = new Valute();
                            break;
                    }                                                         
                }
                db.Valutes.AddRange(masObj);
                db.SaveChanges();
            }
        }
    }
}
