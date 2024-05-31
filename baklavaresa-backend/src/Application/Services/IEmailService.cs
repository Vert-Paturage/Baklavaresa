namespace Application.Services;

public interface IEmailService
{
    public void Send(string to, string body);
}