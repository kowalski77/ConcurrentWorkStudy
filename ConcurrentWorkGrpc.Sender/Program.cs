using System;
using ConcurrentWorkGrpc.Contracts;
using ConcurrentWorkGrpc.Driver.Server;
using ConcurrentWorkGrpc.Sender.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConcurrentWorkGrpc.Sender
{
    internal static class Program
    {
        private static IServiceProvider? serviceProvider;

        private static void Main()
        {
            ConfigureServices();

            var driverServer = serviceProvider!.GetRequiredService<DriverServer>();
            driverServer.Start();

            Console.WriteLine("Press a key to stop the service.");
            Console.ReadKey();

            DisposeServices();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton<SamplesProviderService>();
            services.AddSingleton<ISamplesRepository, SamplesRepository>();

            services.AddDriverService((provider, options) =>
            {
                options.Host = "127.0.0.1";
                options.Port = 50053;
                options.ServerServiceCollection.Add(
                    SamplesService.BindService(provider.GetRequiredService<SamplesProviderService>()));
            });

            serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            Console.WriteLine("Disposing services, please wait...");
            switch (serviceProvider)
            {
                case null:
                    return;
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }

            Console.WriteLine("Services disposed.");
        }
    }
}