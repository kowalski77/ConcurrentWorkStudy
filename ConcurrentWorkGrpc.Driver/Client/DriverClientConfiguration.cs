using Grpc.Core;

namespace ConcurrentWorkGrpc.Driver.Client
{
    public class DriverClientConfiguration
    {
        public string Target { get; set; } = "127.0.0.1:50051";

        public ChannelCredentials ChannelCredentials { get; set; } = ChannelCredentials.Insecure;
    }
}