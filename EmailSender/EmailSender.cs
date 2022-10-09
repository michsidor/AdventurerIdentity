using System.Net;
using System.Net.Mail;

namespace AdventurerOfficialProject.EmailSender
{
    public class SendingEmails : Randomizer
    {
        private string _authorEmail = "netemailsenderapplication@gmail.com";
        private string _authorEmailPassword = "dcpmupllxaagvsix";
        private string _host = "smtp.gmail.com";
        private int _port = 587;
        private bool _ssl = true;
        private bool _credentials = false;

        private string _subject;
        private string _body;
        private string _recepientEmails;

        public string Subject { get { return _subject; } set { _subject = value; } }
        public string Body { get { return _body; } set { _body = value; } }
        public string RecepientEmails { get { return _recepientEmails; } set { _recepientEmails = value; } }

        public static readonly string Code = Randomizer.RandomCode();

        public void EmailSender()
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(_authorEmail);
            message.To.Add(new MailAddress(_recepientEmails)); // tutaj sam wyrzuca wyjatek jak jest zly email adres, ale musze zmienic ten wyjatek na blad w programie.
            message.Subject = _subject;
            message.Body = _body;

            smtp.Port = _port;
            smtp.Host = _host;
            smtp.EnableSsl = _ssl;
            smtp.UseDefaultCredentials = _credentials;
            smtp.Credentials = new NetworkCredential(_authorEmail, _authorEmailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }
    }
}
