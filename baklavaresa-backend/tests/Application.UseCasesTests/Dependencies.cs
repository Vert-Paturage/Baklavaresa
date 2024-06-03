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
        Services.AddScoped<IEmailService, FakeEmailService>();
        
        Services.SetupInMemoryDatabase(Guid.NewGuid().ToString());
        
        ServiceProvider = Services.BuildServiceProvider();
    }
}