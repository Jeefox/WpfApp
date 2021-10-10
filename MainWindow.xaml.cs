using System.Windows;
using WpfApp2.Models;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class TimeJob : IJob
    {
        Task IJob.Execute(IJobExecutionContext context)
        {
            var timeStr = $"{DateTime.UtcNow.Hour}/{DateTime.UtcNow.Day}/{DateTime.UtcNow.Year}";
            MainWindow.AddDataToTable(timeStr);
            return Task.CompletedTask;
        }
    }

    public partial class MainWindow : Window
    {

        private Page1 page = new Page1();
        public static string data = "";
        public MainWindow()
        {
            var timeStr = "";
            InitializeComponent();
            MainFrame.Content = new Page1();
            AddDataToTable(timeStr = $"{DateTime.UtcNow.Hour}/{DateTime.UtcNow.Day}/{DateTime.UtcNow.Year}");
            page.getInitData();
        }

        async Task Trigger()
        {
            //var job = new JobDetail("dumbJob", null, typeof(Quartz.Jobs.NativeJob));
            //job.JobDataMap.Put(Quartz.Jobs.NativeJob.PropertyCommand, "echo \"hi\" >> foobar.txt");

            //var trigger1 = TriggerUtils.MakeSecondlyTrigger(5);
            //trigger1.Name = "dumbTrigger";
            //await scheduler.ScheduleJob(job, trigger);

            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group1")
                .WithCronSchedule("0 0 0/1 1/1 * ? *")
                .ForJob("myTimeJob")
                .Build();

            var job = new JobDetailImpl("myTimeJob", typeof(TimeJob));
            scheduler.AddJob(job, true);
            scheduler.TriggerJob(trigger.JobKey);
            await scheduler.Start();

        }

        private void ButtClick1(object sender, RoutedEventArgs e)
        {
        }

        public static void AddDataToTable(string timeStr)
        {
            
            using (ValuteContext db = new ValuteContext())
            {

                var URLString = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + timeStr;
                var reader = new XmlTextReader(URLString);

                var masObj = new List<Valute>();
                var z = new Valute();
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

