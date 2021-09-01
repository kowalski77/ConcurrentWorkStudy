using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentConstrainThreads
{
    public static class SemaphoreSlimTechnique
    {
        public static async Task RunAsync()
        {
            const int maxThreads = 4;
            var throttler = new SemaphoreSlim(maxThreads);

            var allTasks = Support.Urls.Select(async url =>
            {
                await throttler.WaitAsync();
                try
                {
                    var html = await Support.GetStringAsync(url);
                    Console.WriteLine($"retrieved {html.Length} characters from {url}");
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(allTasks);
        }
    }
}