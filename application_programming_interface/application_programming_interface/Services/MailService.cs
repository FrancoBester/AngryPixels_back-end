using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using System.IO;
using MailKit.Net.Smtp;
using MailKit.Search;
using application_programming_interface.Models;
using application_programming_interface.Interfaces;
using Microsoft.Extensions.Options;

namespace application_programming_interface.Services
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set;}
        public string Password { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailsettings)
        {
            _mailSettings = mailsettings.Value;
        }

        public Task SendEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach(var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);

            return null;
        }
    }
}
