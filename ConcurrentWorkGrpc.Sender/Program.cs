using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ConcurrentWorkGrpc.Sender
{
    internal static class Program
    {
        private static IServiceProvider? serviceProvider;

        private static async Task Main()
        {
            ConfigureServices();

            Console.WriteLine("Press a key to start sending samples...");
            Console.ReadKey();


            DisposeServices();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

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