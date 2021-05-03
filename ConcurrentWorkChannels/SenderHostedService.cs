using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConcurrentWorkChannels
{
    public class SenderHostedService : BackgroundService
    {
        private readonly IFixture fixture;
        private readonly ChannelService<LogInfo> channelService;
        private readonly ILogger<SenderHostedService> logger;

        public SenderHostedService(
            ChannelService<LogInfo> channelService,
            ILogger<SenderHostedService> logger)
        {
            this.fixture = new Fixture();
            this.channelService = channelService;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(500, stoppingToken);

                var logInfo = this.fixture.Create<LogInfo>();
                await this.channelService.Add(logInfo, stoppingToken);

                this.logger.LogInformation($"Sending log info id: {logInfo.Id}");
            }
        }
    }
}