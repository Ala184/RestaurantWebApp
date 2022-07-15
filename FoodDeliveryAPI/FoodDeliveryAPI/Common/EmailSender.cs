using FoodDeliveryAPI.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace FoodDeliveryAPI.Common
{
    public class EmailSender : IEmailSender
    {

        private readonly string host;
        private readonly string sendersEmailAddress;
        private readonly string password;
        public EmailSender(IConfiguration config) 
        {
            host = config.GetSection("EmailHost").Value;
            sendersEmailAddress = config.GetSection("EmailSenderAddress").Value;
            password = config.GetSection("EmailPassword").Value;
        } 

        public void SendMail(string subject, string text, string usersEmail)
        {
            // for testing purposes we don't use users email
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(sendersEmailAddress));
            // email.To.Add(MailboxAddress.Parse(usersEmail));  
            email.To.Add(MailboxAddress.Parse(sendersEmailAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = text };

            using var smtp = new SmtpClient();
            smtp.Connect(host, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(sendersEmailAddress, password);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
