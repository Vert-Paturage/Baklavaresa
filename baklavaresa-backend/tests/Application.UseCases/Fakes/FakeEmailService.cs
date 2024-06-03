using Application.Services;

namespace Application.UnitTests.Fakes;

internal record FakeEmail(string To, string Body);

internal class FakeEmailService: IEmailService
{
    public List<FakeEmail> Emails { get; } = [];
    public void Send(string to, string body)
    {
        Emails.Add(new FakeEmail(to, body)); 
    }
}