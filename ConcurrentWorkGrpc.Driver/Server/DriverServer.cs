using System;
using Grpc.Core;
using GrpcServer = Grpc.Core.Server;

namespace ConcurrentWorkGrpc.Driver.Server
{
    public sealed class DriverServer : IDisposable
    {
        private GrpcServer? server;

        private readonly DriverServerConfiguration configuration;

        public DriverServer(DriverServerConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Start()
        {
            this.server = new GrpcServer
            {
                Ports =
                {
                    new ServerPort(this.configuration.Host, this.configuration.Port,
                        this.configuration.ServerCredentials)
                }
            };

            this.configuration.ServerServiceCollection.ForEach(x => this.server.Services.Add(x));

            this.server.Start();
        }

        public void Dispose()
        {
            this.server?.ShutdownAsync().Wait();
        }
    }
}