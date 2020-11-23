using System.IO;
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
    }
}