using Application.Services;
using Application.UnitTests.Fakes;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UnitTests;

public class Dependencies
{
    protected readonly IServiceCollection Services;
    public Dependencies()
    {
        Services = new ServiceCollection();
        
        Services.ApplicationMediator();
        Services.AddScoped<IEmailService, FakeEmailService>();
    }
}