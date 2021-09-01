using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcurrentConstrainThreads
{
    public static class ConcurrentQueueTechnique
    {
        public static async Task RunAsync()
        {
            const int maxThreads = 4;
            var concurrentQueue = new ConcurrentQueue<string>(Support.Urls);
            var tasks = new List<Task>();
            for (var n = 0; n < maxThreads; n++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    while (concurrentQueue.TryDequeue(out var url))
                    {
                        var html = await Support.GetStringAsync(url);
                        Console.WriteLine($"retrieved {html.Length} characters from {url}");
                    }
                }));
            }
            await Task.WhenAll(tasks);
        }
    }
}