using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConcurrentWorkStudy
{
    internal static class Program
    {
        private static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddSingleton(typeof(ChannelService<>));
                    services.AddHostedService<ReceiverHostedService>();
                    services.AddHostedService<SenderHostedService>();
                });

            await host.RunConsoleAsync();
        }
    }
}