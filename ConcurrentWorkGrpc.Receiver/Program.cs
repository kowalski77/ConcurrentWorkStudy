using System;
using System.Threading;
using System.Threading.Tasks;
using ConcurrentWorkGrpc.Contracts;
using ConcurrentWorkGrpc.Driver.Client;
using ConcurrentWorkGrpc.Driver.Support;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;

namespace ConcurrentWorkGrpc.Receiver
{
    internal static class Program
    {
        private static IServiceProvider? serviceProvider;

        private static async Task Main()
        {
            ConfigureServices();

            Console.WriteLine("Press a key to start receiving samples...");
            Console.ReadKey();

            var driverClient = serviceProvider!.GetRequiredService<DriverClient>();
            using (var streamedSamples = driverClient.CreateClient<SamplesService.SamplesServiceClient>().GetSamples(new Empty()))
            {
                await foreach (var sample in streamedSamples.ResponseStream.ReadAllAsync(CancellationToken.None))
                {
                    Console.WriteLine($"Sample received - Id: {sample.Id} Name: {sample.Name} Active: {sample.Active}");
                }
            }

            DisposeServices();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDriverClient(options => { options.Target = "127.0.0.1:50053"; });

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
