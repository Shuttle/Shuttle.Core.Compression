using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public static class CompressionServiceExtensions
    {
        public static async Task<byte[]> Compress(this ICompressionService compressionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).Compress(bytes);
        }

        public static async Task<byte[]> Decompress(this ICompressionService compressionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).Decompress(bytes);
        }
    }
}