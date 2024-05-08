using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SendMailExample;

public class Mailer
{
    public void SendEmail(string email)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Baklava", "Baklava@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Réservation Baklavarésa";
        message.Body = new TextPart("plain") { Text = "Merci pour votre réservation" };

        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        client.Authenticate("baklavaresa@gmail.com", "tqmc vyfz awzq pxti");
        client.Send(message);
        client.Disconnect(true);
    }
}