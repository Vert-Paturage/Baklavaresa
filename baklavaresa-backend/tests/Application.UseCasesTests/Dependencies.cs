using Application.Services;
using Application.UseCasesTests.Fakes;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCasesTests;

public class Dependencies
{
    public IServiceProvider ServiceProvider { get; }
    protected readonly IServiceCollection Services;
    
    public Dependencies()
    {
        Services = new ServiceCollection();
        
        Services.ApplicationMediator();
        Services.AddRepositories();
        Services.AddScoped<IEmailService, FakeEmailService>();
        Services.AddSingleton<IClockService>(new FakeClockService(new DateTime(1970, 1, 1)));
        
        Services.SetupInMemoryDatabase(Guid.NewGuid().ToString());
        
        ServiceProvider = Services.BuildServiceProvider();
    }
}