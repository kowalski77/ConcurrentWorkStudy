using System;
using System.Collections.Concurrent;
using Grpc.Core;

namespace ConcurrentWorkGrpc.Driver.Client
{
    public sealed class DriverClient : IDisposable
    {
        private static readonly ConcurrentDictionary<Type, object?> Clients = new();
        private readonly Channel channel;

        public DriverClient(DriverClientConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.channel = new Channel(configuration.Target, configuration.ChannelCredentials);
        }

        public T CreateClient<T>()
        {
            var client = Clients.GetOrAdd(typeof(T), type => Activator.CreateInstance(type, this.channel));

            return (T)(client ?? throw new InvalidOperationException($"Could not create a client of type {typeof(T).Name}"));
        }

        public void Dispose()
        {
            this.channel.ShutdownAsync().Wait();
        }
    }
}