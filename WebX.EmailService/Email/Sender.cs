using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using WebX.EmailService.Configuration;

namespace WebX.EmailService.Email
{
    public interface ISender
    {
        void SendEmail(Message message);
    }
    public class Sender : ISender
    {
        private readonly AccessConfig _accessConfig;

        public Sender(AccessConfig accessConfig)
        {
            _accessConfig = accessConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmail(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmail(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_accessConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            if (message.ContentTextType == TextFormat.Html)
            {
                bodyBuilder.HtmlBody = message.Content;
            }
            else
                bodyBuilder.TextBody = message.Content;

            foreach (var attachment in message.Attachments)
            {
                bodyBuilder.Attachments.Add(attachment);
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(_accessConfig.SmtpServer, _accessConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_accessConfig.Username, _accessConfig.Password);
                    client.Send(mailMessage);
                }
                catch (Exception e)
                {
                    //log an error message or throw an exception or both.
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
