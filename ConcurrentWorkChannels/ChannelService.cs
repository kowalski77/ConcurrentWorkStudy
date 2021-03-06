using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConcurrentWorkChannels
{
    public class ChannelService<TMessage> where TMessage : class, new()
    {
        private readonly Channel<TMessage> serviceChannel;

        public ChannelService()
        {
            this.serviceChannel = Channel.CreateBounded<TMessage>(new BoundedChannelOptions(5000)
            {
                SingleReader = false,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.DropWrite
            });
        }

        public async Task Add(TMessage model, CancellationToken cancellationToken)
        {
            await this.serviceChannel.Writer.WriteAsync(model, cancellationToken);
        }

        public IAsyncEnumerable<TMessage> Get(CancellationToken cancellationToken)
        {
            return this.serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}