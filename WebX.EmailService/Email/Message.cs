using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using WebX.EmailService.Configuration;

namespace WebX.EmailService.Email
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public List<MailboxAddress> Cc { get; set; }
        public string Subject { get; set; }

        public string Content { get; set; }

        public MimeKit.Text.TextFormat ContentTextType { get; set; }

        public IEnumerable<MimeEntity> Attachments { get; set; }

        public Message(IEnumerable<string> to, IEnumerable<string> cc, string subject, string content, MimeKit.Text.TextFormat contentTextType, IEnumerable<MimeEntity> attachments)
        {
            To = new List<MailboxAddress>();
            Cc = new List<MailboxAddress>();
            Attachments = new List<MimeEntity>();
            ContentTextType = contentTextType;
            To.AddRange(to.Select(MailboxAddress.Parse));

            Cc.AddRange(cc.Select(MailboxAddress.Parse));

            Attachments = attachments;

            Subject = subject;

            Content = content;
        }
    }
}
