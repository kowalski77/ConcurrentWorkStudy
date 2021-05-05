using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;

namespace ConcurrentWorkGrpc.Sender.Data
{
    public class SamplesRepository : ISamplesRepository
    {
        private readonly IFixture fixture = new Fixture();

        public Task<IEnumerable<Sample>> GetAllAsync()
        {
            var sampleCollection = this.fixture.CreateMany<Sample>(10);

            return Task.FromResult(sampleCollection);
        }
    }
}