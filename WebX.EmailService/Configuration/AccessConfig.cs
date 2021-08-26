using System;

namespace WebX.EmailService.Configuration
{
    public class AccessConfig
    {
        public static readonly string ConfigNodeName = "WebX.EmailService.AccessConfig";
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
