using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class CompressionServiceExtensions
{
    extension(ICompressionService compressionService)
    {
        public async Task<byte[]> CompressAsync(string name, byte[] bytes)
        {
            return await Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).CompressAsync(bytes);
        }

        public async Task<byte[]> DecompressAsync(string name, byte[] bytes)
        {
            return await Guard.AgainstNull(compressionService, nameof(compressionService)).Get(name).DecompressAsync(bytes);
        }
    }
}