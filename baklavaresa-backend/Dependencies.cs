using System;
using Application.Services;
using Application.UseCases.Fakes;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases;

public class Dependencies
{
    public IServiceProvider ServiceProvider { get; private set; }
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