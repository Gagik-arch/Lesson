using System.Net;
using System.Net.Mail;

namespace Lesson.Extensions
{
    public class SMTP
    {
        private MailMessage _mailMessage;
        public SmtpClient _client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("gagik.chilingaryan2020@gmail.com", "ejao tyag akgw cfjy"),
            EnableSsl = true,
        };

        public SMTP(
                string from = "gagik.chilingaryan2020@gmail.com",
                string subject = "",
                string body = ""
            )
        {
            _mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
        }

        public void Add(string to)
        {
            _mailMessage.To.Add(new MailAddress(to));
        }

        public async Task SendAsync()
        {
            try
            {
                await _client.SendMailAsync(_mailMessage);
            }
            catch (Exception)
            {
                throw ;
            }
        }
    }
}
