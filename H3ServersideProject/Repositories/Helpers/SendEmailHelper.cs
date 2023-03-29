using H3ServersideProject.Models;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;

namespace H3ServersideProject.Repositories.Helpers
{
    public class SendEmailHelper
    {
        static bool mailSent = false;

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        public void SendMail(string email, string newPassword, User user)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("Nakzul@gmail.com", "pcbvizfcdewikqjl"),
                EnableSsl = true,
            };
            // Command-line argument must be the SMTP host.
            SmtpClient client = new SmtpClient();
            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("noreply@bestbio.dk",
               "Best Bio",
            System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(email);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Nyt kodeord på bestbio.dk";
            message.Body = "Hej " + user.Name + "\n\nHer er dit nye password til BestBio. For at logge ind, skal du gå til www.bestbio.dk/login og så indtaste din nye kode.\n\n" + newPassword + "\n\nVi glæder os til at se dig igen!";
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            smtpClient.Send(message);
            // Clean up.
            message.Dispose();
        }
    }
}
