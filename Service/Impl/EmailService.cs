using Service.Interface;
using Shared.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Service.Impl
{
    public  class EmailService : IEmailService
    {
        public static void CreateAndSendMessage(EmailConfigurationModel emailConfig)
        {
            using var message = new MailMessage(emailConfig.From.Address, emailConfig.To.Address)
            {
                Subject = emailConfig.Subject,
                Body = emailConfig.Body
            };
            emailConfig.SmtpServer.Send(message);
        }

        public void SendCode(EmailConfigurationModel _emailConfig)
        {
            EmailCredentialsModel credentials = new EmailCredentialsModel();
            int SmtpPort = 587;
            string SmtpServer = "smtp.yandex.com";
            MailAddress sender = new MailAddress(_emailConfig.From.Address, "New Site Administration", System.Text.Encoding.UTF8);
            MailAddress recipient = new MailAddress(_emailConfig.To.Address, "User", System.Text.Encoding.UTF8);
            MailMessage email = new MailMessage(sender, recipient);
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_emailConfig.Body, null, MediaTypeNames.Text.Html);
            email.AlternateViews.Clear();
            email.AlternateViews.Add(htmlView);
            email.Subject = _emailConfig.Title;
            SmtpClient SMTP = new SmtpClient();
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network; 
            SMTP.UseDefaultCredentials = false;
            SMTP.Host = SmtpServer;
            SMTP.Port = SmtpPort;
            SMTP.EnableSsl = true;
            SMTP.Credentials = new NetworkCredential(_emailConfig.From.Address, _emailConfig.Password);

            SMTP.Send(email);
        }
    }
}