using Application.Services;
using Application.UnitTests.Fakes;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UnitTests;

public class Dependencies
{
    public IServiceProvider ServiceProvider { get; private set; }
    protected readonly IServiceCollection Services;
    public Dependencies()
    {
        Services = new ServiceCollection();
        
        Services.ApplicationMediator();
        Services.AddScoped<IEmailService, FakeEmailService>();
        
        ServiceProvider = Services.BuildServiceProvider();
    }
}