using System;
using System.Reflection;
using CommonService.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CommonService.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services){
            services.AddMassTransit(configure => 
            {
                // This syntax will add consumer to this service so that it can consume the messages
                configure.AddConsumers(Assembly.GetEntryAssembly());
                
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var servicesettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    //This line is responsible for getting host from appsettings and setting it with the setting class property
                    var rabbitMqSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    //Here we are setting host with rabbitMQ configurator
                    configurator.Host(rabbitMqSettings.Host);
                    //Here we are configuring RabbitMQ with Kebab(dashes) format endpoint with service name
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(servicesettings.ServiceName, false));
                });
            });

           //services.AddMassTransitHostedService();  
           return services; 
        }
    }
}
