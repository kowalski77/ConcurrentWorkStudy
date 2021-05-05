using System;
using Microsoft.Extensions.DependencyInjection;

namespace ConcurrentWorkGrpc.Driver.Server
{
    public static class DriverServerExtensions
    {
        public static IServiceCollection AddDriverService(
            this IServiceCollection services,
            Action<IServiceProvider, DriverServerConfiguration> options)
        {
            var configuration = new DriverServerConfiguration();
            options.Invoke(services.BuildServiceProvider(), configuration);

            services.AddSingleton(configuration);
            services.AddSingleton<DriverServer>();

            return services;
        }
    }
}