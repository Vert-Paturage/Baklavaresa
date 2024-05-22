using API;

namespace TESTS;

public class FakeMailer: IMailer
{
    public void SendMail(string content)
    {
        MailSent = true;
        Email = content;
    }
    public bool MailSent { get; private set; } = false;
    public string? Email { get; private set; } = null;
}