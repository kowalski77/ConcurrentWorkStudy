using System;
using Microsoft.Extensions.DependencyInjection;

namespace ConcurrentWorkGrpc.Driver.Client
{
    public static class DriverClientExtensions
    {
        public static IServiceCollection AddDriverClient(
            this IServiceCollection services,
            Action<DriverClientConfiguration> options)
        {
            var configuration = new DriverClientConfiguration();
            options.Invoke(configuration);

            services.AddSingleton(configuration);
            services.AddSingleton<DriverClient>();

            return services;
        }
    }
}