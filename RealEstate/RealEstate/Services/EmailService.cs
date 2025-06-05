using System.Net.Mail;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class EmailService
    {
        private readonly string fromEmail = "akshatadhale123@gmail.com";
        private readonly string fromPassword = "puvo veod lura zwxa";

        public async Task SendCancellationEmail(string toEmail, string buyerName)
        {
            var fromAddress = new MailAddress(fromEmail, "RealEstate App");
            var toAddress = new MailAddress(toEmail, buyerName);
            const string subject = "Appointment Cancelled";
            string body = $"Dear {buyerName},\n\nYour appointment has been cancelled by the agent. Please contact support if you have any questions.\n\nRegards,\nRealEstate Team";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(message);
                }
            }
        }
    }
}
