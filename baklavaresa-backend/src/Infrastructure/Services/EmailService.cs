using Application.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService: IEmailService
{
    public void Send(string to, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Baklava", "Baklava@gmail.com"));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = "Réservation Baklavarésa";
        message.Body = new TextPart("plain")
        {
            Text = body
        };
        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        client.Authenticate("baklavaresa@gmail.com", "tqmc vyfz awzq pxti");
        client.Send(message);
        client.Disconnect(true);
    }
}