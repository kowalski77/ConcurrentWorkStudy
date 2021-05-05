using System.Collections.Generic;
using System.Threading.Tasks;
using ConcurrentWorkGrpc.Contracts;
using ConcurrentWorkGrpc.Sender.Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Sample = ConcurrentWorkGrpc.Contracts.Sample;

namespace ConcurrentWorkGrpc.Sender
{
    public class SamplesProviderService: SamplesService.SamplesServiceBase
    {
        private readonly ISamplesRepository repository;

        public SamplesProviderService(ISamplesRepository repository)
        {
            this.repository = repository;
        }

        public override async Task GetSamples(Empty request, IServerStreamWriter<Sample> responseStream, ServerCallContext context)
        {
            var contextCancellationToken = context.CancellationToken;
            var samples = this.GetAllSamples();
            
            await foreach (var sample in samples.WithCancellation(contextCancellationToken))
            {
                if (contextCancellationToken.IsCancellationRequested)
                {
                    return;
                }

                await responseStream.WriteAsync(sample);
            }
        }

        private async IAsyncEnumerable<Sample> GetAllSamples()
        {
            var domainSamples = await this.repository.GetAllAsync();
            foreach (var domainSample in domainSamples)
            {
                await Task.Delay(250); // Simulate delay 
                yield return new Sample
                {
                    Id = domainSample.Id,
                    Name = domainSample.Name,
                    Active = domainSample.IsActive
                };
            }
        }
    }
}