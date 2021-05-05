using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Grpc.Core;

namespace ConcurrentWorkGrpc.Driver.Support
{
    public static class AsyncStreamReaderExtensions
    {
        public static IAsyncEnumerable<T> ReadAllAsync<T>(this IAsyncStreamReader<T> streamReader, CancellationToken cancellationToken = default)
        {
            if (streamReader == null) throw new ArgumentNullException(nameof(streamReader));

            return ReadAllAsyncCore(streamReader, cancellationToken);
        }

        private static async IAsyncEnumerable<T> ReadAllAsyncCore<T>(IAsyncStreamReader<T> streamReader, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (await streamReader.MoveNext(cancellationToken).ConfigureAwait(false))
            {
                yield return streamReader.Current;
            }
        }
    }
}