using System.IO;
using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public static class CompressionAlgorithmExtensions
    {
        public static async Task<Stream> Compress(this ICompressionAlgorithm algorithm, Stream stream)
        {
            Guard.AgainstNull(algorithm, nameof(algorithm));
            Guard.AgainstNull(stream, nameof(stream));

            using var ms = new MemoryStream();
            
            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return new MemoryStream(await algorithm.Compress(ms.ToArray()));
        }
        public static async Task<Stream> Decompress(this ICompressionAlgorithm algorithm, Stream stream)
        {
            Guard.AgainstNull(algorithm, nameof(algorithm));
            Guard.AgainstNull(stream, nameof(stream));

            using var ms = new MemoryStream();
            
            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return new MemoryStream(await algorithm.Decompress(ms.ToArray()));
        }
    }
}