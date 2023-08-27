using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public static class CompressionServiceExtensions
    {
        public static byte[] Compress(this ICompressionService compressionService, string name, byte[] bytes)
        {
            return Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).Compress(bytes);
        }

        public static byte[] Decompress(this ICompressionService compressionService, string name, byte[] bytes)
        {
            return Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).Decompress(bytes);
        }

        public static async Task<byte[]> CompressAsync(this ICompressionService compressionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).CompressAsync(bytes);
        }

        public static async Task<byte[]> DecompressAsync(this ICompressionService compressionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).DecompressAsync(bytes);
        }
    }
}