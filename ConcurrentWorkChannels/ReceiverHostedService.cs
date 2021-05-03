using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConcurrentWorkChannels
{
    public class ReceiverHostedService : BackgroundService
    {
        private readonly ChannelService<LogInfo> channelService;
        private readonly ILogger<ReceiverHostedService> logger;

        public ReceiverHostedService(
            ILogger<ReceiverHostedService> logger,
            ChannelService<LogInfo> channelService)
        {
            this.logger = logger;
            this.channelService = channelService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var log in this.channelService.Get(stoppingToken))
                {
                    await this.LongRunningMethod(log);
                }
            }
        }

        private async Task LongRunningMethod(LogInfo logInfo)
        {
            this.logger.LogInformation($"Processing log info with id: {logInfo.Id} ...");
            await Task.Delay(2000);
            this.logger.LogInformation($"Log info with id: {logInfo.Id} processed.");
        }
    }
}