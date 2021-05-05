using System.Collections.Generic;
using Grpc.Core;

namespace ConcurrentWorkGrpc.Driver.Server
{
    public class DriverServerConfiguration
    {
        public List<ServerServiceDefinition> ServerServiceCollection { get; set; } = new();

        public string Host { get; set; } = "localhost";

        public int Port { get; set; } = 50051;

        public ServerCredentials ServerCredentials { get; set; } = ServerCredentials.Insecure;
    }
}