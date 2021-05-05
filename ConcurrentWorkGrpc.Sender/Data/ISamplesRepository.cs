using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcurrentWorkGrpc.Sender.Data
{
    public interface ISamplesRepository
    {
        Task<IEnumerable<Sample>>  GetAllAsync();
    }
}