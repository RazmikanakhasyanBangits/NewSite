using System.Net.Mail;

namespace Shared.Models
{
    public class EmailConfigurationModel
    {
        public MailAddress From { get; set; }

        public MailAddress To { get; set; }

        public SmtpClient SmtpServer { get; set; }
        public string Title { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Password { get; set; }
    }
}
