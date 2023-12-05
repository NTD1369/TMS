//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using AutoMapper;
using Dapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MimeKit;
using StackExchange.Redis;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Security.Authentication;
using TDI.Application.AutoMapper;
using TDI.Application.Implements.RPT;
using TDI.Application.Interfaces.RPT;
using TDI.Data.Entities;
using TDI.Data.Infrastructure;
using TDI.Data.Repositories;
using TDI.Utilities.Constants;
using TDI.Utilities.Dtos;
using TDI.Utilities.Helpers;
//using TDI.Data.Models;

namespace TimeSheet.Intergration // Note: actual namespace depends on the project name.
{
    class Program
    {
        //static rpt_IApprovalTimesheetSummaryReport _reportService;
        //public readonly EmailSettings _emailSettings;


        //public EmailSettings EmailSettings { get; set; }
        static string MailPort { get; set; } = "";
        static string MailServer { get; set; } = "";
        static string SenderName { get; set; } = "";
        static string Sender { get; set; } = "";
        static string Password { get; set; } = "";
        static string ConnectionString { get; set; } = "";


        //~~~ Gmail
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new DomainToViewModelMappingProfile());
                        cfg.AddProfile(new ViewModelToDomainMappingProfile());
                    });
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    var mapper = config.CreateMapper();
                    services.AddSingleton(mapper);
                    services.AddScoped<ConnectionFactory>();
                    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                    services.AddTransient<rpt_IApprovalTimesheetSummaryReport, rpt_ApprovalTimesheetSummary>();
                    services.AddHostedService<MainRun>();
                });
         
    
        // THuật ngữ mô hình 3 lớp MVC : M - Model, V - VIew, C Controller
        [STAThread]
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
             
        }
        public class MainRun : IHostedService
        {
            rpt_IApprovalTimesheetSummaryReport _reportService;
            public MainRun(rpt_IApprovalTimesheetSummaryReport reportService)
            {
                _reportService = reportService;


                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json");
                var config = builder.Build();
                //var app = host.Services.GetRequiredService<IApplicationBuilder>();
                //app.Run();
                //// Build a config object, using env vars and JSON providers.
                //IConfiguration config = new ConfigurationBuilder()
                //    .AddJsonFile("appsettings.json")
                //    .AddEnvironmentVariables()
                //    .Build();

                //var app = builder.Build();
                //var config = app.Configuration;

                MailPort = config["EmailSettings:MailPort"];
                MailServer = config["EmailSettings:MailServer"];
                SenderName = config["EmailSettings:SenderName"];
                Sender = config["EmailSettings:Sender"];
                Password = config["EmailSettings:Password"];
                ConnectionString = Encryptor.DecryptString(config["ConnectionStrings:DefaultConnection"], AppConstants.TEXT_PHRASE); ;


                List<string> cc = new List<string>();
                cc.Add("dat.n@tdiapj.com");
                //cc.Add("lap.n@tdiapj.com");
                //cc.Add("abc.n@tdiapj.com");

                LogUtils.WriteToFile("Start...");
                string toEmail = "dat.n@tdiapj.com";
                string subject = $"Project - WBS have been expired or not enough mandays";

                string rootpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
                string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestTimeEntryTemplate.html";


                //string tempath = "C:\\Users\\ADMIN\\source\\repos\\TDI TimeSheet 2023\\TimeSheet.Intergration\\wwwroot\\Templates\\EmailTemplate\\RequestTimeEntryTemplate.html";
                string emailContent = File.ReadAllText(tempath);

                LogUtils.WriteToFile("Read template completed...");
                //rpt_IApprovalTimesheetSummaryReport _reportService;

                //List<string> ListEmail
                SendMailMissHour();
                //SendEmailAsync(toEmail, subject, emailContent, cc);

                LogUtils.WriteToFile("Finished send email...");
                LogUtils.WriteToFile("----------------------------------");

            }
            //public Worker(ITestInterface testClass)
            //{
            //    testClass.Foo();
            //}
            public  GenericResult SendMailMissHour()
            {

                GenericResult resulGetAll = new GenericResult();
                try
                {
                    var resultData = _reportService.RPT_TimeEntrySummaryReport_Email();
                    if (resultData.Success == true) {
                        var ListData = resultData.Data as List<RPT_TimeEntrySummaryModel>;
                        if (ListData != null && ListData.Count > 0)
                        {
                            //var tabltemp = ListData.Where(x => x.HoursMissing > 0 || x.UnSumitted > 0 || x.HoursApproved > 0 || x.HoursPending > 0);
                            foreach (var item in ListData)
                            {
                                List<string> cc = new List<string>();
                                //cc.Add("dat.n@tdiapj.com");
                                string subject = "Employee missing timesheet";
                                string rootpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
                                string tempath = rootpath + "\\Templates\\EmailTemplate\\RequestTimeEntryTemplate.html";
                                string emailContent = File.ReadAllText(tempath);
                                emailContent = emailContent.Replace("{User}", item.FullName);
                                emailContent = emailContent.Replace("{Unsumitted}", item.UnSumitted.ToString());
                                emailContent = emailContent.Replace("{HoursMissing}", item.HoursMissing.ToString());
                                emailContent = emailContent.Replace("{HoursPending}", item.HoursPending.ToString());
                                emailContent = emailContent.Replace("{HoursAprroved}", item.HoursApproved.ToString());
                                emailContent = emailContent.Replace("{Total}", item.Total.ToString());
                                SendEmailAsync(item.Email, subject, emailContent, cc);
                            }
                        } 
                    }
                }
                catch (Exception ex)
                {
                    resulGetAll.Success = false;
                    resulGetAll.Message = ex.Message;
                }
                return resulGetAll;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                //throw new Task.CompletedTask;
                //throw new NotImplementedException();
                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                //throw new NotImplementedException();
                return Task.CompletedTask;
            }
        }

        //public void gmailTest()
        //{
        //    using (var client = new SmtpClient())
        //    {
        //        client.Host = "smtp.gmail.com";
        //        client.Port = 587;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.EnableSsl = true;
        //        client.Credentials = new NetworkCredential("YOU@GMAIL.COM", "APP_PASSWORD");




        //        using (var message = new MailMessage(
        //            from: new MailAddress("YOU@GMAIL.COM", "YOUR NAME"),
        //            to: new MailAddress("THEM@ANYWHERE.COM", "THEIR NAME")
        //            ))
        //        {

        //            message.Subject = "Hello from code!";

        //            message.Body = "Loremn ipsum dolor sit amet ... <br> stsvsh ";

        //            client.Send(message);
        //        }
        //    }
        //}

        static void SendEmailAsync(string toEmail, string subject, string message, List<string> cc)
        {
            try
            {

                var mimeMessage = new MimeMessage();

                //EmailSettings _emailSettings = new EmailSettings();

                //_emailSettings.MailPort = int.Parse(MailPort);
                //_emailSettings.MailServer = MailServer;// "smtp.office365.com";
                //_emailSettings.SenderName = SenderName; //"Leave Application";
                //_emailSettings.Sender = Sender;//"no-reply_vn@tdiapj.com";
                //_emailSettings.Password = Password;// "Leave@123q4";


                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                mimeMessage.To.Add(new MailboxAddress(string.Empty, toEmail));
                if (cc != null && cc.Any())
                {
                    foreach (var item in cc)
                    {
                        mimeMessage.Cc.Add(new MailboxAddress(string.Empty, item));
                    }
                }

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {


                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

                    client.Connect(MailServer, int.Parse(MailPort), SecureSocketOptions.Auto);
                    //if (_env.IsDevelopment())
                    //{
                    //    // The third parameter is useSSL (true if the client should make an SSL-wrapped
                    //    // connection to the server; otherwise, false).
                    //    client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, SecureSocketOptions.Auto);
                    //}
                    //else
                    //{
                    //    client.ConnectAsync(_emailSettings.MailServer);
                    //}
                    //await client.ConnectAsync(_emailSettings.MailServer);
                    // Note: only needed if the SMTP server requires authentication

                    client.Authenticate(Sender, Password);

                    client.Send(mimeMessage);

                    client.Disconnect(true);

                    Console.WriteLine("Success " + toEmail);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed " + ex.Message);

                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        


    }

}


