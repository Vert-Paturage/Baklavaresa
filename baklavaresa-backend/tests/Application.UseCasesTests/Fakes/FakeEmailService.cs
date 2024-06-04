using Application.Services;

namespace Application.UseCasesTests.Fakes;

public record FakeEmail(string To, string Body); 

public class FakeEmailService: IEmailService
{
    public List<FakeEmail> Emails { get; } = new();
    public void Send(string to, string body)
    {
        Emails.Add(new FakeEmail(to, body));
    }
}