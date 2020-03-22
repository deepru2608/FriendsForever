using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace FriendsForever_App.Helpers
{
    public class MailHelper
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<MailHelper> logger;

        public MailHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public MailHelper(ILogger<MailHelper> logger)
        {
            this.logger = logger;
        }

        public bool Send(string from, string to, string subject, string content)
        {
            try
            {
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = content;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(ex.Message);
                builder.AppendLine();
                builder.AppendLine("Stack trace---------");
                builder.Append(ex.StackTrace);
                builder.AppendLine();
                builder.AppendLine("Inner Exception---------");
                builder.Append(ex.InnerException);
                logger.Log(LogLevel.Information, builder.ToString());
                return false;
            }
        }
    }
}
