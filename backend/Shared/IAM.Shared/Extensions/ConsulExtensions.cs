using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IAM.Shared.Extensions;

public static class ConsulExtensions
{
    public static IServiceCollection AddConsulConfig(this IServiceCollection services, string serviceName)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var address = "http://consul:8500";
            consulConfig.Address = new Uri(address);
        }));

        return services;
    }

    public static IApplicationBuilder UseConsul(this IApplicationBuilder app, string serviceName)
    {
        var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("ConsulRegistration");
        var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        var registration = new AgentServiceRegistration()
        {
            ID = $"{serviceName}-{Guid.NewGuid()}",
            Name = serviceName,
            Address = "localhost",
            Port = 5000,
            Check = new AgentServiceCheck()
            {
                HTTP = $"http://localhost:5000/health",
                Interval = TimeSpan.FromSeconds(10)
            }
        };

        logger.LogInformation("Registering with Consul");
        consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        consulClient.Agent.ServiceRegister(registration).Wait();

        lifetime.ApplicationStopping.Register(() =>
        {
            logger.LogInformation("Unregistering from Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        });

        return app;
    }
}
