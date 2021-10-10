using System.Windows;
using WpfApp2.Models;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp2
{

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
            /*StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger3", "group1")
            .WithCronSchedule("0 0 0/1 1/1 * ? *")
            .ForJob("myJob", "group1")
            .Build();*/
            using (ValuteContext db = new ValuteContext())
            {

                string URLString = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=04/10/2021";
                XmlReader reader = new XmlTextReader(URLString);

                List<Valute> masObj = new List<Valute>();
                Valute z = new Valute();
                if (!db.Valutes.Any())
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.EndElement) reader.Skip();
                        switch (reader.Name)
                        {
                            case "ValCurs":
                                data = reader.GetAttribute("Date");
                                break;
                            case "Valute":
                                z.ValuteId = (string)reader.GetAttribute("ID");
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
                else 
                    {
                    string id = null;
                    float kurs = 0;
                    while (reader.Read())
                    {                        
                        if (reader.NodeType == XmlNodeType.EndElement) reader.Skip();
                        switch (reader.Name)
                        {
                            case "ValCurs":
                                data = reader.GetAttribute("Date");
                                break;
                            case "Valute":
                                id = reader.GetAttribute("ID");
                                break;
                            case "Value":
                                while (reader.NodeType != XmlNodeType.Text)
                                {
                                    reader.Read();
                                }
                                kurs = float.Parse(reader.Value);                                                                
                                break;
                        }
                        var query =
                        from ord in db.Valutes
                        where ord.ValuteId == id
                        select ord;
                        foreach (Valute val in query)
                        {
                            val.Value = kurs;
                        }                        
                            db.SaveChanges();                        
                    }
                }                                                          
            }

        }

    }
}

