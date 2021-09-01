using System;
using System.Threading.Tasks;

namespace ConcurrentConstrainThreads
{
    public static class Support
    {
        public static readonly string[] Urls ={
            "https://github.com/naudio/NAudio",
            "https://twitter.com/mark_heath",
            "https://github.com/markheath/azure-functions-links",
            "https://pluralsight.com/authors/mark-heath",
            "https://github.com/markheath/advent-of-code-js",
            "http://stackoverflow.com/users/7532/mark-heath",
            "https://mvp.microsoft.com/en-us/mvp/Mark%20%20Heath-5002551",
            "https://github.com/markheath/func-todo-backend",
            "https://github.com/markheath/typescript-tetris",
        };

        public static async Task<string> GetStringAsync(string url)
        {
            Console.WriteLine($"Processing url ${url}");
            await Task.Delay(1000);

            return url + "Processed!";
        }
    }
}