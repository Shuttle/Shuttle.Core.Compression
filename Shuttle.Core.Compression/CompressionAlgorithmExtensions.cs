using System.IO;
using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public static class CompressionAlgorithmExtensions
    {
        public static Stream Compress(this ICompressionAlgorithm algorithm, Stream stream)
        {
            Guard.AgainstNull(algorithm, nameof(algorithm));
            Guard.AgainstNull(stream, nameof(stream));

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);

                return new MemoryStream(algorithm.Compress(ms.ToArray()));
            }
        }

        public static Stream Decompress(this ICompressionAlgorithm algorithm, Stream stream)
        {
            Guard.AgainstNull(algorithm, nameof(algorithm));
            Guard.AgainstNull(stream, nameof(stream));

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);

                return new MemoryStream(algorithm.Decompress(ms.ToArray()));
            }
        }

        public static async Task<Stream> CompressAsync(this ICompressionAlgorithm algorithm, Stream stream)
        {
            Guard.AgainstNull(algorithm, nameof(algorithm));
            Guard.AgainstNull(stream, nameof(stream));

            using var ms = new MemoryStream();
            
            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return new MemoryStream(await algorithm.CompressAsync(ms.ToArray()));
        }

        public static async Task<Stream> DecompressAsync(this ICompressionAlgorithm algorithm, Stream stream)
        {
            Guard.AgainstNull(algorithm, nameof(algorithm));
            Guard.AgainstNull(stream, nameof(stream));

            using var ms = new MemoryStream();
            
            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return new MemoryStream(await algorithm.DecompressAsync(ms.ToArray()));
        }
    }
}