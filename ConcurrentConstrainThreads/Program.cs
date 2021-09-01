using System.Threading.Tasks;

namespace ConcurrentConstrainThreads
{
    internal static class Program
    {
        private static async Task Main()
        {
            //await ConcurrentQueueTechnique.RunAsync();
            await SemaphoreSlimTechnique.RunAsync();
        }
    }
}